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
    public partial class trainersAppointmentView : Form
    {
        //private int trainerID;
        public trainersAppointmentView()
        {
            InitializeComponent();
            //this.trainerID = GetLastLoggedInTrainerID();
            PopulateDataGridView();
            AddButtonColumns();
        }

        private void PopulateDataGridView()
        {
            int trainerID = GetLastLoggedInTrainerID();
            string connectionString = "Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True";
            string query = @"SELECT M.Member_Name, M.Member_ID, TS.Session_Date, TS.Session_Time
                         FROM MEMBERS M
                         INNER JOIN TRAINING_SESSION TS ON M.Member_ID = TS.Member_ID
                         WHERE TS.Trainer_ID = @TrainerID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TrainerID", trainerID);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }



        private void AddButtonColumns()
        {
            DataGridViewButtonColumn cancelColumn = new DataGridViewButtonColumn();
            cancelColumn.HeaderText = "Cancel";
            cancelColumn.Text = "Cancel";
            cancelColumn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(cancelColumn);

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void trainersAppointmentView_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
                {
                    int memberId;
                    DateTime sessionDate;
                    TimeSpan sessionTime;

                    // Retrieve data from the selected row in the DataGridView
                    memberId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Member_ID"].Value);
                    sessionDate = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells["Session_Date"].Value);
                    sessionTime = TimeSpan.Parse(dataGridView1.Rows[e.RowIndex].Cells["Session_Time"].Value.ToString());

                    // Handle button clicks based on column header text
                    if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "Cancel")
                    {
                        CancelTrainingSession(memberId, sessionDate, sessionTime);
                    }
                }
            }
        }

        // Method to retrieve the last logged-in trainer's ID
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

        private void CancelTrainingSession(int memberId, DateTime sessionDate, TimeSpan sessionTime)
        {
            // Your cancellation logic here, using memberId, sessionDate, and sessionTime
            // For example:
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True"))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("DELETE FROM TRAINING_SESSION WHERE Member_ID = @MemberID AND Session_Date = @SessionDate AND Session_Time = @SessionTime", connection);
                command.Parameters.AddWithValue("@MemberID", memberId);
                command.Parameters.AddWithValue("@SessionDate", sessionDate);
                command.Parameters.AddWithValue("@SessionTime", sessionTime);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Training session canceled successfully.");
                    PopulateDataGridView(); // Refresh the DataGridView after cancellation
                }
                else
                {
                    MessageBox.Show("No training session found to cancel.");
                }
            }
        }

        private void RescheduleTrainingSession(int memberId, DateTime newSessionDate, TimeSpan newSessionTime)
        {
            int trainerID = GetLastLoggedInTrainerID();
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True"))
            {
                connection.Open();

                SqlCommand updateCommand = new SqlCommand("UPDATE TRAINING_SESSION SET Session_Date = @NewSessionDate, Session_Time = @NewSessionTime WHERE Member_ID = @MemberID", connection);
                updateCommand.Parameters.AddWithValue("@NewSessionDate", newSessionDate);
                updateCommand.Parameters.AddWithValue("@NewSessionTime", newSessionTime);
                updateCommand.Parameters.AddWithValue("@MemberID", memberId);

                int rowsAffected = updateCommand.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Training session rescheduled successfully.");
                    PopulateDataGridView(); // Refresh the DataGridView after rescheduling
                }
                else
                {
                    MessageBox.Show("No training session found to reschedule.");
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            TrainerAppointment form2form = new TrainerAppointment();
            this.Hide();
            form2form.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            trianer2 form2form = new trianer2();
            this.Hide();
            form2form.ShowDialog();
        }
    }
}
