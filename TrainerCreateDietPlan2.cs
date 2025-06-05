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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PORJDB1
{
    public partial class TrainerCreateDietPlan2 : Form
    {
        string connectionString = "Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True";

        public TrainerCreateDietPlan2()
        {
            InitializeComponent();
            InitializeDataGridView();
            PopulateMealsAndAllergens();
           // PopulateDataGridView1();
        }

        private void PopulateMealsAndAllergens()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                // Populate exercises
                using (SqlCommand cmd = new SqlCommand("SELECT Meal_Name, Nutrition_Info FROM MEALS_NEW", conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        checkedListBox1.Items.Add(reader["Meal_Name"].ToString());
                        dataGridView2.Rows.Add(reader["Meal_Name"].ToString(), reader["Nutrition_Info"].ToString());
                    }
                    reader.Close();
                }

                // Assuming another CheckListBox for machines: checkedListBox2
                using (SqlCommand cmd = new SqlCommand("SELECT Allergen_Name FROM ALLERGENS", conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        checkedListBox2.Items.Add(reader["Allergen_Name"].ToString());
                    }
                    reader.Close();
                }
            }

        }

        private int GetPlanCount()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Diet_Plan_ID FROM TRAINER_DIET_PLAN_NEW ORDER BY Diet_Plan_ID DESC", conn))
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

        private int GetMealID(string mealName, SqlConnection conn)
        {
            // Retrieve Exercise ID from TRAINER_CONSISTS_OF based on Exercise Name
            int mealID = -1;
            using (SqlCommand cmd = new SqlCommand("SELECT Meal_ID FROM MEMBER_CONSISTS_OF_DIET_NEW2 WHERE Meal_ID = (SELECT Meal_ID FROM MEALS_NEW WHERE Meal_Name = @MealName)", conn))
            {
                cmd.Parameters.AddWithValue("@MealName", mealName);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    mealID = Convert.ToInt32(reader["Meal_ID"]);
                }
                reader.Close();
            }
            return mealID;
        }

        private string GetSelectedDietGoals()
        {
            // Retrieve selected diet goals from checkedListBox3
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



        private void checkedListBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void InitializeDataGridView()
        {
            // Adding columns to dataGridView1
            dataGridView1.Columns.Clear(); // Clear any existing columns

            dataGridView1.Columns.Add("MealName", "Meal Name");
            dataGridView1.Columns.Add("AllergenName", "Allergen Name");
            //dataGridView1.Columns.Add("Duration", "Duration (minutes)");

            // Optionally, set properties for the columns for better UI experience
            dataGridView1.Columns["MealName"].Width = 150; // Set your desired width
            dataGridView1.Columns["AllergenName"].Width = 150;
            //dataGridView1.Columns["Duration"].Width = 100;

            // Clear existing columns
            dataGridView2.Columns.Clear();

            // Add columns to dataGridView2
            dataGridView2.Columns.Add("MealName", "Meal Name");
            dataGridView2.Columns.Add("NutritionInfo", "Nutritional Information");

            // Optionally, set properties for the columns for better UI experience
            dataGridView2.Columns["MealName"].Width = 150; // Set your desired width
            dataGridView2.Columns["NutritionInfo"].Width = 300;
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.CheckedItems.Count == 0 || checkedListBox2.CheckedItems.Count == 0)
            {
                MessageBox.Show("Please select a meal and allergen", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (var item in checkedListBox1.CheckedItems)
            {
                foreach (var allergen in checkedListBox2.CheckedItems)
                {
                    dataGridView1.Rows.Add(item.ToString(), allergen.ToString());
                }
            }

        }

        private void TrainerCreateDietPlan2_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

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

        private void button6_Click(object sender, EventArgs e)
        {
            int planCount = GetPlanCount() + 1; // Get the count of existing plans and increment for the next plan

            // Retrieve Trainer ID from LOGIN_LOG_TRAINERS
            int TrainerID = GetTrainerID();

            // Save workout plan details to the database
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                int firstMealID = -1;
                // Insert selected exercises into MEMBER_CONSISTS_OF table and find the first exercise ID
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["MealName"].Value != null) // Check if the row is not empty
                    {
                        int mealID = GetMealID(row.Cells["MealName"].Value.ToString(), conn);
                        if (mealID != -1)
                        {
                            if (firstMealID == -1)
                            {
                                firstMealID = mealID;
                            }
                        }
                    }
                }

                // Insert workout plan details into MEMBER_WORKOUT_PLAN table
                using (SqlCommand cmd = new SqlCommand("INSERT INTO TRAINER_DIET_PLAN_NEW (Diet_Plan_ID, Plan_Name, Trainer_ID, Meal_ID, Diet_Goal) VALUES (@DietPlanID, @PlanName, @TrainerID, @MealID, @DietGoals)", conn))
                {
                    cmd.Parameters.AddWithValue("@DietPlanID", planCount);
                    cmd.Parameters.AddWithValue("@PlanName", "Plan" + planCount); // Generate plan name
                    cmd.Parameters.AddWithValue("@TrainerID", TrainerID); // Retrieve Trainer ID
                    cmd.Parameters.AddWithValue("@MealID", firstMealID); // Insert the first exercise ID
                    //cmd.Parameters.AddWithValue("@RestIntervals", 5); // Hardcoded rest intervals
                    cmd.Parameters.AddWithValue("@DietGoals", GetSelectedDietGoals()); // Retrieve workout goals

                    cmd.ExecuteNonQuery();
                }

                // int firstExerciseID = -1;
                firstMealID = -1;
                // Insert selected exercises into MEMBER_CONSISTS_OF table and find the first exercise ID
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["MealName"].Value != null) // Check if the row is not empty
                    {
                        int mealID = GetMealID(row.Cells["MealName"].Value.ToString(), conn);
                        if (mealID != -1)
                        {
                            if (firstMealID == -1)
                            {
                                firstMealID = mealID;
                            }
                            // Insert exercise into MEMBER_CONSISTS_OF table
                            using (SqlCommand cmd = new SqlCommand("INSERT INTO TRAINER_CONSISTS_OF_DIET_NEW2 (Meal_ID, Diet_Plan_ID) VALUES (@MealID, @DietPlanID)", conn))
                            {
                                cmd.Parameters.AddWithValue("@MealID", mealID);
                                cmd.Parameters.AddWithValue("@DietPlanID", planCount);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            MessageBox.Show("Workout plan saved successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            TrainerDietPlans form2form = new TrainerDietPlans();
            this.Hide();
            form2form.ShowDialog();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            TrainerDietPlans form2form = new TrainerDietPlans();
            this.Hide();
            form2form.ShowDialog();
        }
    }
}


