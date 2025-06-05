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
    public partial class trainerViewWorkoutPlan : Form
    {
        string connectionString = "Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True";
        public trainerViewWorkoutPlan()
        {
            InitializeComponent();
            InitializeDataGridView();
            LoadDataIntoCheckedListBox();
            checkedListBox3.CheckOnClick = true;
            checkedListBox3.SelectionMode = SelectionMode.One; // Set selection mode to allow only one item to be selected
            checkedListBox3.ItemCheck += CheckedListBox3_ItemCheck; // Subscribe to the ItemCheck event
            
            PopulateExercisesAndMachines();
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



        private void CheckedListBox3_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // Uncheck all items except the one being checked
            for (int i = 0; i < checkedListBox3.Items.Count; i++)
            {
                if (i != e.Index)
                {
                    checkedListBox3.SetItemChecked(i, false);
                }
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {

        }
        private int GetLastLoggedInTrainerID()
        {
            string connectionString = "Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True";
            string query = @"SELECT TOP 1 User_ID FROM LOGIN_LOG_TRAINERS ORDER BY TrainerLogin_ID DESC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                object result = command.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : -1; // Return -1 if no trainer has logged in yet
            }
        }


        private void LoadDataIntoCheckedListBox()
        {
            // Establish connection string
            string connectionString = "Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True";

            // Clear existing items
            checkedListBox3.Items.Clear();

            // Retrieve the last logged-in trainer's ID
            int trainerID = GetLastLoggedInTrainerID();
            if (trainerID != -1)
            {
                // Fetch exercises, machines, and rest intervals corresponding to the trainer's workout plans
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"SELECT DISTINCT e.Exercise_Name, m.Machine_Name, p.Rest_Intervals 
                             FROM TRAINER_WORKOUT_PLAN p 
                             INNER JOIN EXERCISE e ON p.Exercise_ID = e.Exercise_ID
                             INNER JOIN MACHINES m ON e.Machine_ID = m.Machine_ID
                             WHERE p.Trainer_ID = @TrainerID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TrainerID", trainerID);

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string exerciseName = reader["Exercise_Name"].ToString();
                        string machineName = reader["Machine_Name"].ToString();
                        int restIntervals = Convert.ToInt32(reader["Rest_Intervals"]);
                        checkedListBox3.Items.Add($"{exerciseName} - {machineName} - Rest Intervals: {restIntervals}");
                    }
                    reader.Close();
                }
            }
            else
            {
                MessageBox.Show("No trainer has logged in yet.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }



        private void button5_Click(object sender, EventArgs e)
        {
            TrainerWorkoutPlan form2form = new TrainerWorkoutPlan();
            this.Hide();
            form2form.ShowDialog();
        }



        private void trainerViewWorkoutPlan_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void adlogin_Click(object sender, EventArgs e)
        {

        }

        private void checkedListBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
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

        private List<int> GetMemberIDsAssignedToTrainer(int trainerID)
        {
            List<int> memberIDs = new List<int>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Member_ID FROM TRAINING_SESSION WHERE Trainer_ID = @TrainerID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TrainerID", trainerID);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        memberIDs.Add(Convert.ToInt32(reader["Member_ID"]));
                    }
                }
            }

            return memberIDs;
        }

        private string GetMemberWorkoutGoals(int memberID)
        {
            string workoutGoals = "No workout goals specified"; // Default value if no workout goals are found

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Workout_Goals FROM MEMBER_WORKOUT_PLAN WHERE Member_ID = @MemberID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MemberID", memberID);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        workoutGoals = result.ToString();
                    }
                }
            }

            return workoutGoals;
        }



        private void button6_Click_1(object sender, EventArgs e)
        {
            int planCount = GetPlanCount() + 1; // Get the count of existing plans and increment for the next plan

            // Retrieve Trainer ID from LOGIN_LOG_TRAINERS
            int trainerID = GetTrainerID();
            List<int> memberIDs = GetMemberIDsAssignedToTrainer(trainerID);

            // Save workout plan details to the database
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                   // if (row.Cells["ExerciseName"].Value != null) // Check if the row is not empty
                    //{
                        foreach (int memberID in memberIDs)
                        {
                        //MessageBox.Show("mem id: ", memberID.ToString());
                        using (SqlCommand cmd = new SqlCommand("UPDATE MEMBER_WORKOUT_PLAN SET Plan_Name = @PlanName, Exercise_ID = @ExerciseID, Rest_Intervals = @RestIntervals, Workout_Goals = @WorkoutGoals WHERE Member_ID = @MemberID", conn))
                            {
                                cmd.Parameters.AddWithValue("@PlanID", planCount + 10);
                                cmd.Parameters.AddWithValue("@PlanName", "Trainer Plan" + planCount); // Generate plan name
                                cmd.Parameters.AddWithValue("@MemberID", memberID); // Retrieve Trainer ID
                                cmd.Parameters.AddWithValue("@ExerciseID", GetExerciseID("Downward Dog", conn)); // Retrieve Exercise ID
                                cmd.Parameters.AddWithValue("@RestIntervals", 5); // Hardcoded rest intervals
                                cmd.Parameters.AddWithValue("@WorkoutGoals", GetMemberWorkoutGoals(memberID)); // Retrieve workout goals

                                cmd.ExecuteNonQuery();
                            }
                        }
                    //}
                    //else MessageBox.Show("didnt work :(");
                }
            }
            MessageBox.Show("Workout plan saved successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            TrainerWorkoutPlan form2form = new TrainerWorkoutPlan();
            this.Hide();
            form2form.ShowDialog();
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

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}