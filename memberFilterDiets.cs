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
    public partial class memberFilterDiets : Form
    {
        string connectionString = "Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True";
        public memberFilterDiets()
        {
            InitializeComponent();
            PopulateExerciseListBox();
            PopulateMachineListBox();
            PopulateWorkoutGoalListBox();
        }

        private void PopulateExerciseListBox()
        {
            string query = "SELECT Meal_Name FROM MEALS_NEW";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    checkedListBox1.Items.Add(reader["Meal_Name"].ToString());
                }

                reader.Close();
            }
        }

        private void PopulateMachineListBox()
        {
            string query = "SELECT Allergen_Name FROM ALLERGENS";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    checkedListBox2.Items.Add(reader["Allergen_Name"].ToString());
                }

                reader.Close();
            }
        }

        private void PopulateWorkoutGoalListBox()
        {
            string query = "SELECT DISTINCT Diet_Goal FROM TRAINER_DIET_PLAN_NEW";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    checkedListBox3.Items.Add(reader["Diet_Goal"].ToString());
                }

                reader.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            memberDietHome form2form = new memberDietHome();
            this.Hide();
            form2form.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void memberFilterDiets_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            memberFilterDietsHome form2form = new memberFilterDietsHome();
            this.Hide();
            form2form.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            memberDietHome form2form = new memberDietHome();
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

        private void RefreshFilteredResults()
        {
            string mealFilter = GetCheckedItemsAsString(checkedListBox1);
            string allergenFilter = GetCheckedItemsAsString(checkedListBox2);
            string dietGoalFilter = GetCheckedItemsAsString(checkedListBox3);

            string query = @"
        SELECT t.Diet_Plan_ID, t.Plan_Name, t.Trainer_ID, t.Meal_ID, t.Diet_Goal, e.Meal_Name, m.Allergen_Name
        FROM TRAINER_DIET_PLAN_NEW t
        JOIN MEALS_NEW e ON t.Meal_ID = e.Meal_ID
        JOIN ALLERGENS m ON e.Allergen_ID = m.Allergen_ID
        WHERE 1=1";

            if (!string.IsNullOrEmpty(mealFilter))
            {
                query += $" AND e.Meal_Name IN ({mealFilter})";
            }

            if (!string.IsNullOrEmpty(dietGoalFilter))
            {
                query += $" AND t.Diet_Goal IN ({dietGoalFilter})";
            }

            if (!string.IsNullOrEmpty(allergenFilter))
            {
                query += $" AND m.Allergen_Name IN ({allergenFilter})";
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
    }
}
