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
    public partial class Report18 : Form
    {
        string connectionString = "Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True";
        public Report18()
        {
            InitializeComponent();
            DisplayData();
        }

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
            
                   SELECT TOP 1 a.Allergen_Name, COUNT(*) AS Total_Meals
                    FROM MEALS_NEW m
                    JOIN MEAL_ALLERGENS_NEW2 ma ON m.Meal_ID = ma.Meal_ID
                    JOIN ALLERGENS a ON ma.Allergen_ID = a.Allergen_ID
                    GROUP BY a.Allergen_Name
                    ORDER BY Total_Meals DESC


                            ";

                // Create a SqlCommand
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameter
                    //command.Parameters.AddWithValue("@MemberId", memberId);

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
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            allreports form2form = new allreports();
            this.Hide();
            form2form.ShowDialog();
        }

        private void Report18_Load(object sender, EventArgs e)
        {

        }
    }
}
