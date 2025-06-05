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
    public partial class gymreq : Form
    {
        public gymreq()
        {
            InitializeComponent();
            PopulateDataGridView();
            InitializeDataGridView();
        }


        private void PopulateDataGridView()
        {
            string connectionString = "Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True";
            string query = @"SELECT Request_ID, Gym_Name, Location, Owner_Name
                     FROM GYM_REQUESTS";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
        }

        private void InitializeDataGridView()
        {
           
            DataGridViewCheckBoxColumn approveColumn = new DataGridViewCheckBoxColumn();
            approveColumn.HeaderText = "Approve";
            approveColumn.Name = "Approve";
            dataGridView1.Columns.Add(approveColumn);

            DataGridViewCheckBoxColumn disapproveColumn = new DataGridViewCheckBoxColumn();
            disapproveColumn.HeaderText = "Disapprove";
            disapproveColumn.Name = "Disapprove";
            dataGridView1.Columns.Add(disapproveColumn);
        }

        private int GetNextgymId(SqlConnection conn)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT MAX(Gym_ID) FROM GYM", conn))
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


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Handle checkbox click event
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn)
                {
                    string columnName = dataGridView1.Columns[e.ColumnIndex].Name; // Get the column name
                    string connectionString = "Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True";

                    DataGridViewCheckBoxCell checkBoxCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewCheckBoxCell;
                     bool isChecked = (bool)checkBoxCell.EditedFormattedValue;

                    if (isChecked)
                    {
                       
                        if (columnName == "Approve")
                        {
                            // Retrieve gym information
                            string gymName = dataGridView1.Rows[e.RowIndex].Cells["Gym_Name"].Value?.ToString();
                            string location = dataGridView1.Rows[e.RowIndex].Cells["Location"].Value?.ToString();
                            string ownerName = dataGridView1.Rows[e.RowIndex].Cells["Owner_Name"].Value?.ToString();
                            int requestId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Request_ID"].Value);


                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {
                                connection.Open();
                                int gymid = GetNextgymId(connection);
                                int ownerID = GetNextOwnerId(connection);


                                if (gymName != null && location != null && ownerName != null)
                                {
                                    // Insert into Gym
                                    InsertGym(gymid, gymName, location, connection);
                                    InsertOwner(ownerID,ownerName, connection);

                                    // Update Gym Request table (Approved)
                                    UpdateGymRequestStatus1(requestId, "Approved", connection);

                                    // Refresh DataGridView
                                    
                                    PopulateDataGridView();

                                    MessageBox.Show("Gym approved successfully!");
                                   
                                }
                                else
                                {
                                    MessageBox.Show(" Cannot approve.");
                                }

                            }
                            
                        }
                        else if (columnName == "Disapprove")
                        {
                                int requestId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Request_ID"].Value);
                                UpdateGymRequestStatus(requestId, "Disapproved");

                                // Refresh DataGridView
                                PopulateDataGridView();

                                MessageBox.Show("Gym request disapproved.");
                        }

                    }
                }
            }
        }
        private int GetNextOwnerId(SqlConnection conn)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT MAX(Owner_ID) FROM OWNER", conn))
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

        private void InsertOwner(int ownerID,string ownerName, SqlConnection conn)
        {
            string insertQuery = "INSERT INTO OWNER (Owner_ID, Owner_Name) VALUES (@ownerID, @OwnerName)";

            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True"))
            {
                connection.Open();
                SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@ownerName", ownerName);
                insertCommand.Parameters.AddWithValue("@ownerID", ownerID);
                insertCommand.ExecuteNonQuery();
            }
        }

        // Function to insert into Gym table
        private void InsertGym(int gymid,string gymName, string location, SqlConnection conn)
        {
            string insertQuery = "INSERT INTO GYM (Gym_ID,Gym_Name, Location, Registration_Status) VALUES (@gymid, @gymName, @location, 'Active')";

            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True"))
            {
                connection.Open();
                SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@gymName", gymName);
                insertCommand.Parameters.AddWithValue("@location", location);
                insertCommand.Parameters.AddWithValue("@gymid", gymid);
                insertCommand.ExecuteNonQuery();
               
            }
        }

        // Function to update Gym Request table status
        private void UpdateGymRequestStatus1(int requestId, string status, SqlConnection conn)
        {
            string updateQuery = "DELETE FROM GYM_REQUESTS WHERE Request_ID = @requestId";

            using (SqlCommand updateCommand = new SqlCommand(updateQuery, conn))
            {
                updateCommand.Parameters.AddWithValue("@requestId", requestId);
                updateCommand.ExecuteNonQuery();
            }
        }

        private void UpdateGymRequestStatus(int requestId, string status)
        {
            string updateQuery = "UPDATE GYM_REQUESTS SET Request_Status  = @Status WHERE Request_ID = @requestId";

            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True"))
            {
                connection.Open();
                SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                updateCommand.Parameters.AddWithValue("@requestId", requestId);
                updateCommand.Parameters.AddWithValue("@Status", status);
                updateCommand.ExecuteNonQuery();
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            adminopt form2form = new adminopt();
            this.Hide();
            form2form.ShowDialog();
        }



    }
}
