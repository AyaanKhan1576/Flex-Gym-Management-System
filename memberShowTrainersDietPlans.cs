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
    public partial class memberShowTrainersDietPlans : Form
    {
        string connectionString = "Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True";
        private void DisplayData()
        {
            // Create a DataTable to hold the query results
            DataTable dt = new DataTable();

            // Create a SqlConnection
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Open the connection
                connection.Open();

                // Get the memberId using the GetMemberID method
                //int memberId = GetMemberID(connection);
                //int memberId = 1;
                // SQL query to fetch the data
                string query = @"
            
                    SELECT 
                        dp.Diet_Plan_ID,
                        dp.Plan_Name,
                        t.Trainer_ID,
                        t.Trainer_Name,
                        dp.Diet_Goal AS Plan_Description,
                        meals.Meal_Name,
                        meals.Nutrition_Info,
                        allergens.Allergen_Name
                    FROM 
                        TRAINER_DIET_PLAN_NEW dp
                    INNER JOIN 
                        TRAINERS t ON dp.Trainer_ID = t.Trainer_ID
                    LEFT JOIN 
                        TRAINER_CONSISTS_OF_DIET_NEW2 mcod ON dp.Diet_Plan_ID = mcod.Diet_Plan_ID
                    LEFT JOIN 
                        MEALS_NEW meals ON mcod.Meal_ID = meals.Meal_ID
                    LEFT JOIN 
                        MEAL_ALLERGENS_NEW2 ma ON meals.Meal_ID = ma.Meal_ID
                    LEFT JOIN 
                        ALLERGENS allergens ON ma.Allergen_ID = allergens.Allergen_ID;
";

                // Create a SqlCommand
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameter
                    // command.Parameters.AddWithValue("@MemberId", memberId);

                    // Create a SqlDataAdapter
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        // Fill the DataTable with the query results
                        adapter.Fill(dt);

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = dt;
                    }
                }
            }
        }
        public memberShowTrainersDietPlans()
        {
            InitializeComponent();
            DisplayData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void memberShowTrainersDietPlans_Load(object sender, EventArgs e)
        {

        }
    }
}
