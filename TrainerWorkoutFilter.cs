using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PORJDB1
{
    public partial class TrainerWorkoutFilter : Form
    {
        string connectionString = "Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True"; // Update with your actual connection string

        public TrainerWorkoutFilter()
        {
            InitializeComponent();
            PopulateExerciseListBox();
            PopulateMachineListBox();
            PopulateWorkoutGoalListBox();
        }

        private void PopulateExerciseListBox()
        {
            string query = "SELECT Exercise_Name FROM EXERCISE";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    checkedListBox1.Items.Add(reader["Exercise_Name"].ToString());
                }

                reader.Close();
            }
        }

        private void PopulateMachineListBox()
        {
            string query = "SELECT Machine_Name FROM MACHINES";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    checkedListBox2.Items.Add(reader["Machine_Name"].ToString());
                }

                reader.Close();
            }
        }

        private void PopulateWorkoutGoalListBox()
        {
            string query = "SELECT DISTINCT Workout_Goals FROM TRAINER_WORKOUT_PLAN";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    checkedListBox3.Items.Add(reader["Workout_Goals"].ToString());
                }

                reader.Close();
            }
        }

        private void TrainerWorkoutFilter_Load(object sender, EventArgs e)
        {
            // This method is called when the form is loaded
            // You can perform additional actions here if needed
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Event handler for exercise checkedListBox selection change
        }

        private void checkedListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Event handler for machine checkedListBox selection change
            RefreshFilteredResults();
        }

        private void checkedListBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Event handler for workout goals checkedListBox selection change
        }

        private void RefreshFilteredResults()
        {
            string exerciseFilter = GetCheckedItemsAsString(checkedListBox1);
            string machineFilter = GetCheckedItemsAsString(checkedListBox2);
            string workoutGoalFilter = GetCheckedItemsAsString(checkedListBox3);

            string query = @"
        SELECT t.Plan_ID, t.Plan_Name, t.Trainer_ID, t.Exercise_ID, t.Rest_Intervals, t.Workout_Goals, e.Exercise_Name, t.Workout_Goals, m.Machine_Name
        FROM TRAINER_WORKOUT_PLAN t
        JOIN EXERCISE e ON t.Exercise_ID = e.Exercise_ID
        JOIN MACHINES m ON e.Machine_ID = m.Machine_ID
        WHERE 1=1";

            if (!string.IsNullOrEmpty(exerciseFilter))
            {
                query += $" AND e.Exercise_Name IN ({exerciseFilter})";
            }

            if (!string.IsNullOrEmpty(workoutGoalFilter))
            {
                query += $" AND t.Workout_Goals IN ({workoutGoalFilter})";
            }

            if (!string.IsNullOrEmpty(machineFilter))
            {
                query += $" AND m.Machine_Name IN ({machineFilter})";
            }

            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);
            }

            dataGridView1.DataSource = dataTable;
        }



        private void button5_Click(object sender, EventArgs e)
        {
            TrainerWorkoutPlan form2form = new TrainerWorkoutPlan();
            this.Hide();
            form2form.ShowDialog();
        }

        private string GetCheckedItemsAsString(CheckedListBox checkedListBox)
        {
            List<string> checkedItems = new List<string>();
            foreach (var item in checkedListBox.CheckedItems)
            {
                string itemString = item.ToString().Replace("'", "''"); // Escape single quotes by doubling them
                checkedItems.Add($"'{itemString}'");
            }

            return string.Join(",", checkedItems);
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            TrainerWorkoutPlan form2form = new TrainerWorkoutPlan();
            this.Hide();
            form2form.ShowDialog();

        }
    }
}

