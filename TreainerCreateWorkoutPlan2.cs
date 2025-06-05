using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PORJDB1
{
    public partial class TreainerCreateWorkoutPlan2 : Form
    {
        string connectionString = "Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True";
        public TreainerCreateWorkoutPlan2()
        {
            InitializeComponent();
            InitializeDataGridView();
            PopulateExercisesAndMachines();
            PopulateDataGridView1();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void PopulateExercisesAndMachines()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                // Populate exercises
                using (SqlCommand cmd = new SqlCommand("SELECT Exercise_Name FROM EXERCISE", conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        checkedListBox1.Items.Add(reader["Exercise_Name"].ToString());
                    }
                    reader.Close();
                }

                // Assuming another CheckListBox for machines: checkedListBox2
                using (SqlCommand cmd = new SqlCommand("SELECT Machine_Name FROM MACHINES", conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        checkedListBox2.Items.Add(reader["Machine_Name"].ToString());
                    }
                    reader.Close();
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void TreainerCreateWorkoutPlan2_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            int planCount = GetPlanCount() + 1; // Get the count of existing plans and increment for the next plan

            // Retrieve Trainer ID from LOGIN_LOG_TRAINERS
            int trainerID = GetTrainerID();

            // Save workout plan details to the database
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    planCount = GetPlanCount() + 1;
                    if (row.Cells["ExerciseName"].Value != null) // Check if the row is not empty
                    {
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO TRAINER_WORKOUT_PLAN (Plan_ID, Plan_Name, Trainer_ID, Exercise_ID, Rest_Intervals, Workout_Goals) VALUES (@PlanID, @PlanName, @TrainerID, @ExerciseID, @RestIntervals, @WorkoutGoals)", conn))
                        {
                            cmd.Parameters.AddWithValue("@PlanID", planCount + 11);
                            cmd.Parameters.AddWithValue("@PlanName", "Trainer Plan" + planCount); // Generate plan name
                            cmd.Parameters.AddWithValue("@TrainerID", trainerID); // Retrieve Trainer ID
                            cmd.Parameters.AddWithValue("@ExerciseID", GetExerciseID(row.Cells["ExerciseName"].Value.ToString(), conn)); // Retrieve Exercise ID
                            cmd.Parameters.AddWithValue("@RestIntervals", 5); // Hardcoded rest intervals
                            cmd.Parameters.AddWithValue("@WorkoutGoals", GetSelectedWorkoutGoals()); // Retrieve workout goals

                            cmd.ExecuteNonQuery();
                        }

                    }
                }
            }
            MessageBox.Show("Workout plan saved successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            TrainerWorkoutPlan form2form = new TrainerWorkoutPlan();
            this.Hide();
            form2form.ShowDialog();
        }

        private int GetPlanCount()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM TRAINER_WORKOUT_PLAN", conn))
                {
                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        private int GetTrainerID()
        {
            int trainerID = -1;
            // Retrieve Trainer ID from LOGIN_LOG_TRAINERS
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 User_ID FROM LOGIN_LOG_TRAINERS ORDER BY TrainerLogin_ID DESC", conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        trainerID = Convert.ToInt32(reader["User_ID"]);
                    }
                    reader.Close();
                }
            }
            return trainerID;
        }

        private int GetExerciseID(string exerciseName, SqlConnection conn)
        {
            // Retrieve Exercise ID from TRAINER_CONSISTS_OF based on Exercise Name
            int exerciseID = -1;
            using (SqlCommand cmd = new SqlCommand("SELECT Exercise_ID FROM TRAINER_CONSISTS_OF WHERE Exercise_ID = (SELECT Exercise_ID FROM EXERCISE WHERE Exercise_Name = @ExerciseName)", conn))
            {
                cmd.Parameters.AddWithValue("@ExerciseName", exerciseName);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    exerciseID = Convert.ToInt32(reader["Exercise_ID"]);
                }
                reader.Close();
            }
            return exerciseID;
        }

        private string GetSelectedWorkoutGoals()
        {
            // Retrieve workout goals from the table
            string workoutGoals = "none decided yet";
            return workoutGoals.ToString();
        }


        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.CheckedItems.Count == 0 || checkedListBox2.CheckedItems.Count == 0 || string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Please select an exercise, machine, and specify duration.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (var item in checkedListBox1.CheckedItems)
            {
                foreach (var machine in checkedListBox2.CheckedItems)
                {
                    int.TryParse(textBox1.Text, out int duration);
                    dataGridView2.Rows.Add(item.ToString(), machine.ToString(), duration);
                }
            }
        }

        private void InitializeDataGridView()
        {
            // Adding columns to dataGridView1
            dataGridView2.Columns.Clear(); // Clear any existing columns

            dataGridView2.Columns.Add("ExerciseName", "Exercise Name");
            dataGridView2.Columns.Add("MachineName", "Machine Name");
            dataGridView2.Columns.Add("Duration", "Duration (minutes)");

            // Optionally, set properties for the columns for better UI experience
            dataGridView2.Columns["ExerciseName"].Width = 150; // Set your desired width
            dataGridView2.Columns["MachineName"].Width = 150;
            dataGridView2.Columns["Duration"].Width = 100;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void PopulateDataGridView1()
        {
            // Establish connection string
            string connectionString = "Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                // Fetch clients' workout goals from the MEMBER_WORKOUT_PLAN table
                string query = "SELECT m.Member_Name, p.Workout_Goals " +
                               "FROM MEMBER_WORKOUT_PLAN p " +
                               "INNER JOIN MEMBERS m ON p.Member_ID = m.Member_ID";
                SqlCommand command = new SqlCommand(query, conn);

                // Create a DataTable to hold the results
                DataTable dataTable = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);

                // Bind the DataTable to the dataGridView1
                dataGridView1.DataSource = dataTable;
            }
        }

    }
}
