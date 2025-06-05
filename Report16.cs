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
    public partial class Report16 : Form
    {
        string connectionString = "Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True";
        public Report16()
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
            
                    SELECT TOP 1 DP.*, COUNT(MDP.Meal_ID) AS Total_Meals
                    FROM TRAINER_DIET_PLAN_NEW DP
                    JOIN MEMBER_DIET_PLAN_NEW MDP ON DP.Diet_Plan_ID = MDP.Diet_Plan_ID
                    GROUP BY DP.Diet_Plan_ID, DP.Plan_Name, DP.Trainer_ID, DP.Meal_ID, DP.Diet_Goal
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

        private void Report16_Load(object sender, EventArgs e)
        {

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
    }
}
