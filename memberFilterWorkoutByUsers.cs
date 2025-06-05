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
    public partial class memberFilterWorkoutByUsers : Form
    {
        string connectionString = "Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True";
        public memberFilterWorkoutByUsers()
        {
            InitializeComponent();
            PopulateExerciseListBox();
            PopulateMachineListBox();
            PopulateWorkoutGoalListBox();
        }

        private void memberFilterWorkoutByUsers_Load(object sender, EventArgs e)
        {

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
            string query = "SELECT DISTINCT Workout_Goals FROM MEMBER_WORKOUT_PLAN";

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

        private void button5_Click(object sender, EventArgs e)
        {
            memberFilterWorkoutHome form2form = new memberFilterWorkoutHome();
            this.Hide();
            form2form.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            memberHome form2form = new memberHome();
            this.Hide();
            form2form.ShowDialog();
        }

        private void checkedListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshFilteredResults();
        }

        private void checkedListBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshFilteredResults();
        }

        private void RefreshFilteredResults()
        {
            string exerciseFilter = GetCheckedItemsAsString(checkedListBox1);
            string machineFilter = GetCheckedItemsAsString(checkedListBox2);
            string workoutGoalFilter = GetCheckedItemsAsString(checkedListBox3);

            string query = @"
            SELECT t.Plan_ID, t.Plan_Name, t.Member_ID, t.Exercise_ID, t.Rest_Intervals, t.Workout_Goals, e.Exercise_Name, t.Workout_Goals, m.Machine_Name
            FROM MEMBER_WORKOUT_PLAN t
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
    }
}
