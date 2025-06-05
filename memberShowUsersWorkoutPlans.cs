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
    public partial class memberShowUsersWorkoutPlans : Form
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
                        wp.Plan_ID,
                        wp.Plan_Name,
                        m1.Member_ID,
                        m1.Member_Name,
                        wp.Workout_Goals AS Plan_Description,
                        m.Machine_Name,
                        e.Exercise_Name,
                        e.Muscle_Group
                    FROM 
                        MEMBER_WORKOUT_PLAN wp
                    INNER JOIN 
                        MEMBERS m1 ON wp.Member_ID = m1.Member_ID
                    LEFT JOIN 
                        MEMBER_CONSISTS_OF co ON wp.Plan_ID = co.Plan_ID
                    LEFT JOIN 
                        EXERCISE e ON co.Exercise_ID = e.Exercise_ID
                    LEFT JOIN 
                        EXERCISE_USES_MACHINES eum ON e.Exercise_ID = eum.Exercise_ID
                    LEFT JOIN 
                        MACHINES m ON eum.Machine_ID = m.Machine_ID";

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
        public memberShowUsersWorkoutPlans()
        {
            InitializeComponent();
            DisplayData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
