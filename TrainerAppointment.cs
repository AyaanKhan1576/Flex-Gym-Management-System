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
    public partial class TrainerAppointment : Form
    {
        int selectedMemberID = -1;
        int trainerID;
        public TrainerAppointment()
        {
            InitializeComponent();
            checkedListBox1.SelectionMode = SelectionMode.One;
            trainerID = GetLastLoggedInTrainerID();
            LoadDataIntoCheckBox();

        }

        private int GetLastLoggedInTrainerID()
        {
            string connectionString = "Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True";
            string query = "SELECT TOP 1 User_ID FROM LOGIN_LOG_TRAINERS ORDER BY TrainerLogin_ID DESC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                object result = command.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : -1; // Returns -1 if no trainer has logged in yet
            }
        }


        private void TrainerAppointment_Load(object sender, EventArgs e)
        {

        }

     

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // If more than one item is selected, uncheck all except the last selected item
            if (checkedListBox1.CheckedItems.Count > 1)
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if (i != checkedListBox1.SelectedIndex)
                    {
                        checkedListBox1.SetItemChecked(i, false);
                    }
                }
            }
            if (checkedListBox1.SelectedIndex != -1)
            {
                selectedMemberID = GetMemberID(checkedListBox1.SelectedItem.ToString());
            }
            else
            {
                selectedMemberID = -1;
            }
        }

        private void LoadDataIntoCheckBox()
        {
            string connectionString = "Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True";
            string query = @"SELECT M.Member_Name, M.Member_ID, TS.Session_Date, TS.Session_Time
                             FROM MEMBERS M
                             INNER JOIN TRAINING_SESSION TS ON M.Member_ID = TS.Member_ID
                             WHERE TS.Trainer_ID = @TrainerID"; // Filter sessions by Trainer ID

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TrainerID", trainerID); // Use trainerID
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    string memberID = row["Member_ID"].ToString();
                    string memberName = row["Member_Name"].ToString();
                    DateTime sessionDate = Convert.ToDateTime(row["Session_Date"]);
                    TimeSpan sessionTime = TimeSpan.Parse(row["Session_Time"].ToString());
                    string sessionDateTime = sessionDate.ToShortDateString() + " " + sessionTime.ToString();
                    checkedListBox1.Items.Add(memberID + " - " + memberName + " - " + sessionDateTime);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            trainersAppointmentView form2form = new trainersAppointmentView();
            this.Hide();
            form2form.ShowDialog();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.CheckedItems.Count == 1)
            {
                // Get the selected item from the CheckedListBox
                string selectedItem = checkedListBox1.CheckedItems[0].ToString();

                // Extract the Member_ID from the selected item
                int memberId = GetMemberID(selectedItem);
               // MessageBox.Show(memberId.ToString());

                if (memberId != -1)
                {
                    // Get the new date and time selected by the trainer
                    DateTime newDateTime = dateTimePicker1.Value.Date + dateTimePicker1.Value.TimeOfDay;

                    // Update the database with the new session date and time for the selected client
                    UpdateTrainingSession(memberId, newDateTime);

                    // Close the form after rescheduling
                    trainersAppointmentView form2form = new trainersAppointmentView();
                    this.Hide();
                    form2form.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Invalid selected item.");
                }
            }
            else
            {
                MessageBox.Show("Please select one client to reschedule.");
            }
        }

        private int GetMemberID(string selectedItem)
        {
            // Extract the Member_ID from the selected item
            int startIndex = 0;
            int endIndex = 1;
            string memberIDStr = selectedItem.Substring(startIndex, endIndex - startIndex);

            int memberId;
            if (int.TryParse(memberIDStr, out memberId))
            {
                return memberId;
            }
            else
            {
                return -1;
            }
        }



        private void UpdateTrainingSession(int memberId, DateTime newDateTime)
        {
            string connectionString = "Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Extract the time from the newDateTime
                TimeSpan newSessionTime = newDateTime.TimeOfDay;

                // Update the TRAINING_SESSION table with the new date-time for the selected client
                SqlCommand command = new SqlCommand("UPDATE TRAINING_SESSION SET Session_Date = @NewSessionDate, Session_Time = @NewSessionTime WHERE Member_ID = @MemberID", connection);
                command.Parameters.AddWithValue("@NewSessionDate", newDateTime.Date);
                command.Parameters.AddWithValue("@NewSessionTime", newSessionTime); // Use the extracted time
                command.Parameters.AddWithValue("@MemberID", memberId);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Training session rescheduled successfully.");
                }
                else
                {
                    MessageBox.Show("No training session found to reschedule.");
                }
            }
        }


        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
