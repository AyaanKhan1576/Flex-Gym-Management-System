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
    public partial class memberShowOwnWorkoutPlan : Form
    {

        string connectionString = "Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True";

        //public memberViewOwnWorkoutPlan()
        //{
        //    InitializeComponent();
        //}

        private int GetMemberID(SqlConnection conn)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 User_ID FROM LOGIN_LOG_MEMBERS ORDER BY Login_ID DESC;\r\n", conn))
            {
                object result = cmd.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    return Convert.ToInt32(result);
                }
                else
                {
                    return 11; // Start from 11 if no IDs are present
                }
            }
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
                int memberId = GetMemberID(connection);
                //int memberId = 1;
                // SQL query to fetch the data
                string query = @"
            
                        SELECT 
                            wp.Plan_ID,
                            wp.Plan_Name,
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
                            MACHINES m ON eum.Machine_ID = m.Machine_ID
                        WHERE 
                            m1.Member_ID = @MemberId;";

                // Create a SqlCommand
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameter
                    command.Parameters.AddWithValue("@MemberId", memberId);

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

        //private void DisplayData(int memberId)
        //{
        //    // Create a DataTable to hold the query results
        //    DataTable dt = new DataTable();

        //    // SQL query to fetch the data
        //    string query = @"
        //        SELECT
        //            W.Plan_ID,
        //            W.Plan_Name,
        //            W.Description,
        //            E.Exercise_ID,
        //            E.Exercise_Name,
        //            M.Machine_Name
        //        FROM
        //            WORKOUT_PLAN W
        //        INNER JOIN
        //            CONSISTS_OF C ON W.Plan_ID = C.Plan_ID
        //        INNER JOIN
        //            EXERCISE E ON C.Exercise_ID = E.Exercise_ID
        //        INNER JOIN
        //            EXERCISE_USES_MACHINES EU ON E.Exercise_ID = EU.Exercise_ID
        //        INNER JOIN
        //            MACHINES M ON EU.Machine_ID = M.Machine_ID
        //        WHERE
        //            W.Member_ID = @MemberId";

        //    // Create a SqlConnection
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        memberId = GetMemberID(connection);
        //        // Create a SqlCommand
        //        using (SqlCommand command = new SqlCommand(query, connection))
        //        {
        //            // Add parameter
        //            command.Parameters.AddWithValue("@MemberId", memberId);

        //            // Create a SqlDataAdapter
        //            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
        //            {
        //                // Open the connection
        //                connection.Open();

        //                // Fill the DataTable with the query results
        //                adapter.Fill(dt);

        //                // Bind the DataTable to the DataGridView
        //                dataGridView1.DataSource = dt;
        //            }
        //        }
        //    }
        //}
        public memberShowOwnWorkoutPlan()
        {
            InitializeComponent();
            DisplayData();
        }

        private void memberViewOwnWorkoutPlan_Load(object sender, EventArgs e)
        {
            // Call the DisplayData method with the member ID
            //DisplayData(1); // Replace 1 with the actual member ID
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //DisplayData(1);
        }
    }
}
