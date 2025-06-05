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
    public partial class TrainerCreateDietPlan : Form
    {
        public TrainerCreateDietPlan()
        {
            InitializeComponent();
            LoadDataIntoCheckBox();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            trianer2 form2form = new trianer2();
            this.Hide();
            form2form.ShowDialog();
        }

        private void TrainerCreateDietPlan_Load(object sender, EventArgs e)
        {

        }
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
        private void button6_Click(object sender, EventArgs e)
        {
            // Check if a client is selected
            if (checkedListBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a client.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Get the selected client name
            string selectedClientName = checkedListBox1.SelectedItem.ToString();

            // Get the corresponding Member_ID from the database
            int selectedClientID = GetMemberID(selectedClientName);
            TrainerCreateDietPlan2 form2form = new TrainerCreateDietPlan2();
            this.Hide();
            form2form.ShowDialog();
        }

        private int GetMemberID(string memberName)
        {
            int memberID = -1;

            // Establish connection string
            string connectionString = "Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True";

            // Fetch the Member_ID corresponding to the given memberName
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Member_ID FROM MEMBERS WHERE Member_Name = @MemberName";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MemberName", memberName);
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    memberID = Convert.ToInt32(result);
                }
            }
            return memberID;
        }

        private void LoadDataIntoCheckBox()
        {
            // Establish connection string
            string connectionString = "Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True";

            // Clear existing items
            checkedListBox1.Items.Clear();

            // Fetch member names corresponding to the logged-in trainer ID
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                int trainerID = GetLastLoggedInTrainerID();
                string query = @"SELECT DISTINCT M.Member_Name 
                         FROM TRAINING_SESSION TS
                         JOIN MEMBERS M ON TS.Member_ID = M.Member_ID
                         WHERE TS.Trainer_ID = @TrainerID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TrainerID", trainerID);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string memberName = reader["Member_Name"].ToString();
                    checkedListBox1.Items.Add(memberName); // Add Member_Name to the items
                }
                reader.Close();
            }
        }
    }
}
