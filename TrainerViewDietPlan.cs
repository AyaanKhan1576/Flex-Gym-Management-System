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
    public partial class TrainerViewDietPlan : Form
    {
        string connectionString = "Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True";
        public TrainerViewDietPlan()
        {
            InitializeComponent();
            InitializeDataGridView();
            LoadDataIntoCheckedListBox();
            checkedListBox3.CheckOnClick = true;
            checkedListBox3.SelectionMode = SelectionMode.One; // Set selection mode to allow only one item to be selected
            checkedListBox3.ItemCheck += CheckedListBox3_ItemCheck; // Subscribe to the ItemCheck event
            PopulateMealsAndAllergens();
        }

        private void PopulateMealsAndAllergens()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                // Populate exercises
                using (SqlCommand cmd = new SqlCommand("SELECT Meal_Name FROM MEALS_NEW", conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        checkedListBox1.Items.Add(reader["Meal_Name"].ToString());
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
        private void InitializeDataGridView()
        {
            // Adding columns to dataGridView1
            dataGridView2.Columns.Clear(); // Clear any existing columns

            dataGridView2.Columns.Add("MealName", "Meal Name");
            dataGridView2.Columns.Add("AllergenName", "Allergen Name");

            // Optionally, set properties for the columns for better UI experience
            dataGridView2.Columns["MealName"].Width = 150; // Set your desired width
            dataGridView2.Columns["AllergenName"].Width = 150;
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
                // Fetch member names and workout goals corresponding to the trainer's clients
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"SELECT m.Member_Name, p.Diet_Goal
                             FROM MEMBER_DIET_PLAN_NEW p 
                             INNER JOIN MEMBERS m ON p.Member_ID = m.Member_ID
                             INNER JOIN TRAINING_SESSION t ON p.Member_ID = t.Member_ID
                             WHERE t.Trainer_ID = @TrainerID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TrainerID", trainerID);

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string memberName = reader["Member_Name"].ToString();
                        string workoutGoals = reader["Diet_Goal"].ToString();
                        checkedListBox3.Items.Add(memberName + ": " + workoutGoals); // Add Member_Name to the items
                    }
                    reader.Close();
                }
            }
            else
            {
                MessageBox.Show("No trainer has logged in yet.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void TrainerViewDietPlan_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.CheckedItems.Count == 0 || checkedListBox2.CheckedItems.Count == 0 )
            {
                MessageBox.Show("Please select an allergen, and specify meal.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (var item in checkedListBox1.CheckedItems)
            {
                foreach (var machine in checkedListBox2.CheckedItems)
                {
                    dataGridView2.Rows.Add(item.ToString(), machine.ToString());
                }
            }
        }

        private int GetPlanCount()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM MEMBER_DIET_PLAN_NEW", conn))
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

        private int GetMealID(string mealName, SqlConnection conn)
        {
            int mealID = -1; // Default to -1 if the meal is not found

            // Prepare the SQL command to retrieve the Meal_ID for the given Meal_Name
            using (SqlCommand cmd = new SqlCommand("SELECT Meal_ID FROM MEALS_NEW WHERE Meal_Name = @MealName", conn))
            {
                cmd.Parameters.AddWithValue("@MealName", mealName);

                // Execute the command and retrieve the Meal_ID
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    mealID = Convert.ToInt32(result); // Convert the result to int and store in mealID
                }
            }

            return mealID; // Return the Meal_ID or -1 if not found
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

        private string GetMemberDietGoals(int memberID)
        {
            string DietGoals = "No workout goals specified"; // Default value if no workout goals are found

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Diet_Goal FROM MEMBER_DIET_PLAN_NEW WHERE Member_ID = @MemberID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MemberID", memberID);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        DietGoals = result.ToString();
                    }
                }
            }

            return DietGoals;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int planCount = GetPlanCount() + 1; // Increment the count of existing plans for the next plan name

            // Retrieve Trainer ID from LOGIN_LOG_TRAINERS
            int trainerID = GetTrainerID();
            List<int> memberIDs = GetMemberIDsAssignedToTrainer(trainerID);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    foreach (DataGridViewRow row in dataGridView2.Rows)
                    {
                        if (row.IsNewRow || row.Cells["MealName"].Value == null) continue; // Skip new or incomplete rows

                        string mealName = row.Cells["MealName"].Value.ToString();
                        if (string.IsNullOrEmpty(mealName))
                        {
                            MessageBox.Show("Meal name is empty in one of the rows.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            continue;
                        }

                        int mealID = GetMealID(mealName, conn); // Retrieve the Meal ID from the database
                        if (mealID == -1)
                        {
                            MessageBox.Show($"Meal ID for '{mealName}' could not be found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            continue;
                        }

                        foreach (int memberID in memberIDs)
                        {
                            using (SqlCommand cmd = new SqlCommand("UPDATE MEMBER_DIET_PLAN_NEW SET Plan_Name = @PlanName, Meal_ID = @MealID, Diet_Goal = @DietGoals WHERE Member_ID = @MemberID", conn))
                            {
                                cmd.Parameters.AddWithValue("@PlanName", "Trainer Plan " + planCount);
                                cmd.Parameters.AddWithValue("@MemberID", memberID);
                                cmd.Parameters.AddWithValue("@MealID", mealID);
                                cmd.Parameters.AddWithValue("@DietGoals", GetMemberDietGoals(memberID));

                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    MessageBox.Show("Diet plans updated successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to update diet plans: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                TrainerDietPlans form2form = new TrainerDietPlans();
                this.Hide();
                form2form.ShowDialog();
            }
        }

    }
}
