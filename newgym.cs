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
    public partial class newgym : Form
    {
        public newgym()
        {
            InitializeComponent();
        }

        private void newgym_Load(object sender, EventArgs e)
        {
        }


        private int GetNextreqId(SqlConnection conn)
        {
            if (conn.State == ConnectionState.Closed)
                conn.Open();

            using (SqlCommand cmd = new SqlCommand("SELECT MAX(Request_ID) FROM GYM_REQUESTS", conn))
            {
                object result = cmd.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    return Convert.ToInt32(result) + 1;
                }
                else
                {
                    return 1; // Start from 1 if no IDs are present
                }
            }
        }

      
        private void button1_Click(object sender, EventArgs e)
        {
            string gymName = textBox1.Text;
            string location = textBox2.Text;

            // Check if both textboxes have values
            if (!string.IsNullOrEmpty(gymName) && !string.IsNullOrEmpty(location))
            {
                string connectionString = "Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Check if the gym name already exists in GYM_REQUESTS table
                    connection.Open();
                    string checkQuery = "SELECT COUNT(*) FROM GYM_REQUESTS WHERE Gym_Name = @gymName";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@gymName", gymName);
                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                        if (count > 0)
                        {
                            MessageBox.Show("Gym name already exists in GYM_REQUESTS table.");
                            return;
                        }
                    }

                    // Check if the gym name already exists in GYM table
                    string checkQuery2 = "SELECT COUNT(*) FROM GYM WHERE Gym_Name = @gymName";
                    using (SqlCommand checkCommand2 = new SqlCommand(checkQuery2, connection))
                    {
                        checkCommand2.Parameters.AddWithValue("@gymName", gymName);
                        int count = Convert.ToInt32(checkCommand2.ExecuteScalar());
                        if (count > 0)
                        {
                            MessageBox.Show("Gym name already exists in GYM table.");
                            return;
                        }
                    }

                    // If the gym name doesn't exist in either table, proceed with insertion
                    int reqid = GetNextreqId(connection);
                    string insertQuery = "INSERT INTO GYM_REQUESTS (Request_ID, Gym_Name, Location, Request_Status) VALUES (@reqid, @gymName, @location, 'Pending')";

                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@gymName", gymName);
                        insertCommand.Parameters.AddWithValue("@location", location);
                        insertCommand.Parameters.AddWithValue("@reqid", reqid);
                        insertCommand.ExecuteNonQuery();
                    }

                    MessageBox.Show("Request added successfully!");
                    Form1 form2form = new Form1();
                    this.Hide();
                    form2form.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Please fill in both gym name and location.");
            }
        }
    }
}
