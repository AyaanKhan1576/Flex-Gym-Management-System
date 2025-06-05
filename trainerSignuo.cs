using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;   
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PORJDB1
{
    public partial class trainerSignuo : Form
    {
        public trainerSignuo()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void trainerSignuo_Load(object sender, EventArgs e)
        {
            PopulateGyms();
        }

        private void PopulateGyms()
        {
            string connectionString = "Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Gym_ID, Gym_Name FROM GYM";
                SqlCommand cmd = new SqlCommand(query, conn);
                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        // Here we are adding Gym_Name as the display member and Gym_ID as the value member
                        checkedListBox2.Items.Add(reader["Gym_Name"].ToString(), false);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to populate gyms: " + ex.Message);
                }
            }
        }
        private int GetNextTrainerId(SqlConnection conn)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT MAX(Trainer_ID) FROM TRAINERS", conn))
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

        // Utility functions for validation
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidPhoneNumber(string number)
        {
            return Regex.IsMatch(number, @"^\d{10}$");  // Simple pattern: exactly 10 digits
        }

        private int GetGymIdByName(string gymName, SqlConnection conn)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT Gym_ID FROM GYM WHERE Gym_Name = @GymName", conn))
            {
                cmd.Parameters.AddWithValue("@GymName", gymName);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!IsValidEmail(textBox2.Text))
            {
                MessageBox.Show("Invalid email format.");
                return;
            }

            if (!IsValidPhoneNumber(textBox4.Text))
            {
                MessageBox.Show("Invalid phone number format.");
                return;
            }

            string connectionString = "Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string name = textBox1.Text;
                string email = textBox2.Text;
                string password = textBox3.Text;
                string phoneNumber = textBox4.Text;

                int trainerID = GetNextTrainerId(conn);
                string selectedGymName = checkedListBox1.CheckedItems[0].ToString(); // Assuming only one gym can be selected

                int gymID = GetGymIdByName(selectedGymName, conn);

                string specialization = "";
                foreach (object item in checkedListBox2.CheckedItems)
                {
                    specialization += item.ToString() + ", ";
                }
                specialization = string.IsNullOrEmpty(specialization) ? "" : specialization.Remove(specialization.Length - 2);

                string query = "INSERT INTO TRAINERS (Trainer_ID, Trainer_Name, Email, Phone_Number, Gym_ID, Specialization, Trainer_Password) VALUES (@TrainerID, @Name, @Email, @PhoneNumber, @GymID, @Specialization, @Password)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TrainerID", trainerID);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    cmd.Parameters.AddWithValue("@GymID", gymID);
                    cmd.Parameters.AddWithValue("@Specialization", specialization);
                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.ExecuteNonQuery();
                }
            }

            trianer2 form2form = new trianer2();
            this.Hide();
            form2form.ShowDialog();
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void checkedListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
