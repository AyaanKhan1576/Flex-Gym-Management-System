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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PORJDB1
{
    public partial class membersignup : Form
    {
        public membersignup()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void membersignup_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            memberlogin form2form = new memberlogin();
            this.Hide();
            form2form.ShowDialog();
        }
        private int GetNextMemberId(SqlConnection conn)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT MAX(Member_ID) FROM MEMBERS", conn))
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

        private void button1_Click(object sender, EventArgs e)
        {
            // Validation
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

                // Retrieve values from textboxes
                string name = textBox1.Text;
                string email = textBox2.Text;
                string password = textBox5.Text;
                string phoneNumber = textBox4.Text;

                // Generate member ID
                int memberID = GetNextMemberId(conn);

                // SQL query to insert data into the MEMBERS table
                string query = "INSERT INTO MEMBERS (Member_ID, Member_Name, Email, Phone_Number, Member_Password) VALUES (@MemberID, @Name, @Email, @PhoneNumber, @Password)";

                // Create SqlCommand object with parameters
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MemberID", memberID);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    cmd.Parameters.AddWithValue("@Password", password);

                    // Execute the query
                    cmd.ExecuteNonQuery();
                }
            }

            membershiprecord form2form = new membershiprecord();
            this.Hide();
            form2form.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
