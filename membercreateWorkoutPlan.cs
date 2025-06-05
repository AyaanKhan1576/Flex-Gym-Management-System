using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PORJDB1
{
    public partial class memberCreateWorkoutPlan : Form
    {
        string connectionString = "Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True";
        public memberCreateWorkoutPlan()
        {
            InitializeComponent();
            InitializeDataGridView();
            PopulateExercisesAndMachines();
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

        private void button5_Click(object sender, EventArgs e)
        {
            memberHome form2form = new memberHome();
            this.Hide();
            form2form.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void adlogin_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            memberHome form2form = new memberHome();
            this.Hide();
            form2form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
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
                    dataGridView1.Rows.Add(item.ToString(), machine.ToString(), duration);
                }
            }
        }

        //private void button3_Click(object sender, EventArgs e)
        //{
        //    int planCount = GetPlanCount() + 1; // Get the count of existing plans and increment for the next plan

        //    // Retrieve Trainer ID from LOGIN_LOG_TRAINERS
        //    int memberID = GetMemberID();

        //    // Save workout plan details to the database
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        conn.Open();
        //        foreach (DataGridViewRow row in dataGridView1.Rows)
        //        {
        //            if (row.Cells["ExerciseName"].Value != null) // Check if the row is not empty
        //            {
        //                using (SqlCommand cmd = new SqlCommand("INSERT INTO MEMBER_WORKOUT_PLAN (Plan_ID, Plan_Name, Member_ID, Exercise_ID, Rest_Intervals, Workout_Goals) VALUES (@PlanID, @PlanName, @MemberID, @ExerciseID, @RestIntervals, @WorkoutGoals)", conn))
        //                {
        //                    cmd.Parameters.AddWithValue("@PlanID", planCount);
        //                    cmd.Parameters.AddWithValue("@PlanName", "Plan" + planCount); // Generate plan name
        //                    cmd.Parameters.AddWithValue("@MemberID", memberID); // Retrieve Trainer ID
        //                    cmd.Parameters.AddWithValue("@ExerciseID", GetExerciseID(row.Cells["ExerciseName"].Value.ToString(), conn)); // Retrieve Exercise ID
        //                    cmd.Parameters.AddWithValue("@RestIntervals", 5); // Hardcoded rest intervals
        //                    cmd.Parameters.AddWithValue("@WorkoutGoals", GetSelectedWorkoutGoals()); // Retrieve workout goals

        //                    cmd.ExecuteNonQuery();
        //                }
        //            }
        //        }
        //    }
        //    MessageBox.Show("Workout plan saved successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //}

        private void button3_Click(object sender, EventArgs e)
        {
            int planCount = GetPlanCount() + 1; // Get the count of existing plans and increment for the next plan

            // Retrieve Trainer ID from LOGIN_LOG_TRAINERS
            int memberID = GetMemberID();

            // Save workout plan details to the database
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                int firstExerciseID = -1;
                // Insert selected exercises into MEMBER_CONSISTS_OF table and find the first exercise ID
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["ExerciseName"].Value != null) // Check if the row is not empty
                    {
                        int exerciseID = GetExerciseID(row.Cells["ExerciseName"].Value.ToString(), conn);
                        if (exerciseID != -1)
                        {
                            if (firstExerciseID == -1)
                            {
                                firstExerciseID = exerciseID;
                            }
                        }
                    }
                }

                // Insert workout plan details into MEMBER_WORKOUT_PLAN table
                using (SqlCommand cmd = new SqlCommand("INSERT INTO MEMBER_WORKOUT_PLAN (Plan_ID, Plan_Name, Member_ID, Exercise_ID, Rest_Intervals, Workout_Goals) VALUES (@PlanID, @PlanName, @MemberID, @ExerciseID, @RestIntervals, @WorkoutGoals)", conn))
                {
                    cmd.Parameters.AddWithValue("@PlanID", planCount);
                    cmd.Parameters.AddWithValue("@PlanName", "Plan" + planCount); // Generate plan name
                    cmd.Parameters.AddWithValue("@MemberID", memberID); // Retrieve Trainer ID
                    cmd.Parameters.AddWithValue("@ExerciseID", firstExerciseID); // Insert the first exercise ID
                    cmd.Parameters.AddWithValue("@RestIntervals", 5); // Hardcoded rest intervals
                    cmd.Parameters.AddWithValue("@WorkoutGoals", GetSelectedWorkoutGoals()); // Retrieve workout goals

                    cmd.ExecuteNonQuery();
                }

                // int firstExerciseID = -1;
                firstExerciseID = -1;
                // Insert selected exercises into MEMBER_CONSISTS_OF table and find the first exercise ID
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["ExerciseName"].Value != null) // Check if the row is not empty
                    {
                        int exerciseID = GetExerciseID(row.Cells["ExerciseName"].Value.ToString(), conn);
                        if (exerciseID != -1)
                        {
                            if (firstExerciseID == -1)
                            {
                                firstExerciseID = exerciseID;
                            }
                            // Insert exercise into MEMBER_CONSISTS_OF table
                            using (SqlCommand cmd = new SqlCommand("INSERT INTO MEMBER_CONSISTS_OF (Exercise_ID, Plan_ID) VALUES (@ExerciseID, @PlanID)", conn))
                            {
                                cmd.Parameters.AddWithValue("@ExerciseID", exerciseID);
                                cmd.Parameters.AddWithValue("@PlanID", planCount);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            MessageBox.Show("Workout plan saved successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private int GetPlanCount()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Plan_ID FROM MEMBER_WORKOUT_PLAN ORDER BY Plan_ID DESC", conn))
                {
                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        private int GetMemberID()
        {
            int trainerID = -1;
            // Retrieve Trainer ID from LOGIN_LOG_TRAINERS
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 User_ID FROM LOGIN_LOG_MEMBERS ORDER BY Login_ID DESC", conn))
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
            using (SqlCommand cmd = new SqlCommand("SELECT Exercise_ID FROM MEMBER_CONSISTS_OF WHERE Exercise_ID = (SELECT Exercise_ID FROM EXERCISE WHERE Exercise_Name = @ExerciseName)", conn))
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
            // Retrieve selected workout goals from checkedListBox3
            StringBuilder selectedGoals = new StringBuilder();
            foreach (var item in checkedListBox3.CheckedItems)
            {
                selectedGoals.Append(item.ToString());
                selectedGoals.Append(", ");
            }
            if (selectedGoals.Length > 0)
            {
                selectedGoals.Remove(selectedGoals.Length - 2, 2); // Remove the last comma and space
            }
            return selectedGoals.ToString();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void InitializeDataGridView()
        {
            // Adding columns to dataGridView1
            dataGridView1.Columns.Clear(); // Clear any existing columns

            dataGridView1.Columns.Add("ExerciseName", "Exercise Name");
            dataGridView1.Columns.Add("MachineName", "Machine Name");
            dataGridView1.Columns.Add("Duration", "Duration (minutes)");

            // Optionally, set properties for the columns for better UI experience
            dataGridView1.Columns["ExerciseName"].Width = 150; // Set your desired width
            dataGridView1.Columns["MachineName"].Width = 150;
            dataGridView1.Columns["Duration"].Width = 100;
        }


        private void checkedListBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void memberCreateWorkoutPlan_Load(object sender, EventArgs e)
        {

        }
    }
}
