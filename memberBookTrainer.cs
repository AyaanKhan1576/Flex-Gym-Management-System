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
    

    public partial class memberBookTrainer : Form
    {
        int selectedTrainerID = -1;
        string connectionString = "Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True";
        public memberBookTrainer()
        {
            InitializeComponent();
            checkedListBox1.SelectionMode = SelectionMode.One;
            //trainerID = GetLastLoggedInTrainerID();
            LoadDataIntoCheckBox();

        }

        private void LoadDataIntoCheckBox()
        {
            string connectionString = "Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True";
            string query = @"SELECT M.Trainer_Name, M.Trainer_ID
                             FROM TRAINERS M
                             "; // Filter sessions by Trainer ID

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                //command.Parameters.AddWithValue("@TrainerID", trainerID); // Use trainerID
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    string memberID = row["Trainer_ID"].ToString();
                    string memberName = row["Trainer_Name"].ToString();
                    //DateTime sessionDate = Convert.ToDateTime(row["Session_Date"]);
                    //TimeSpan sessionTime = TimeSpan.Parse(row["Session_Time"].ToString());
                    //string sessionDateTime = sessionDate.ToShortDateString() + " " + sessionTime.ToString();
                    checkedListBox1.Items.Add(memberID + " - " + memberName + " - ");
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            trainersAppointmentView form2form = new trainersAppointmentView();
            this.Hide();
            form2form.ShowDialog();
        }





        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void memberBookTrainer_Load(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form2form = new Form1();
            this.Hide();
            form2form.ShowDialog();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
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
                selectedTrainerID = GetTrainerID(checkedListBox1.SelectedItem.ToString());
            }
            else
            {
                selectedTrainerID = -1;
            }

        }

        private int GetTrainerID(string selectedItem)
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

        private int GetNextTrainingSessionID(SqlConnection conn)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Session_ID FROM TRAINING_SESSION ORDER BY Session_ID DESC;\r\n", conn))
            {
                object result = cmd.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    return Convert.ToInt32(result) + 1;
                }
                else
                {
                    return 11; // Start from 11 if no IDs are present
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Retrieve values from textboxes
                //string sessionDate = dateTimePicker1.ToString();
                //string sessionTime = dateTimePicker2.ToString();
                string sessionDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                string sessionTime = dateTimePicker2.Value.ToString("HH:mm:ss");

                int duration = 0;
                int.TryParse(textBox2.Text, out duration);

                // Generate member ID
                int memberID = GetMemberID(conn);
                int sessionID = GetNextTrainingSessionID(conn);

                // SQL query to insert data into the MEMBERS table
                string query = "INSERT INTO TRAINING_SESSION (Session_ID, Trainer_ID,  Member_ID, Session_Date, Session_Time, Duration) VALUES (@SessionID, @TrainerID, @MemberID, @SessionDate, @SessionTime, @Duration)";

                // Create SqlCommand object with parameters
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SessionID", sessionID);
                    cmd.Parameters.AddWithValue("@TrainerID", selectedTrainerID);
                    cmd.Parameters.AddWithValue("@MemberID", memberID);
                    cmd.Parameters.AddWithValue("@SessionDate", sessionDate);
                    cmd.Parameters.AddWithValue("@SessionTime", sessionTime);
                    cmd.Parameters.AddWithValue("@Duration", duration);

                    // Execute the query
                    cmd.ExecuteNonQuery();
                }
            }

            memberHome form2form = new memberHome();
            this.Hide();
            form2form.ShowDialog();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
