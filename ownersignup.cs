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
    public partial class ownersignup : Form
    {
        public ownersignup()
        {
            InitializeComponent();
        }

        public ownersignup(string gymName)
        {
            InitializeComponent();
            // Set the textbox text to the passed gym name
            textBox5.Text = gymName;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            owner form2form = new owner();
            this.Hide();
            form2form.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void ownersignup_Load(object sender, EventArgs e)
        {

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

        private void button1_Click(object sender, EventArgs e)
        {// Validation
            if (!IsValidEmail(textBox4.Text))
            {
                MessageBox.Show("Invalid email format.");
                return;
            }

            if (!IsValidPhoneNumber(textBox3.Text))
            {
                MessageBox.Show("Invalid phone number format.");
                return;
            }

            string connectionString = "Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                MessageBox.Show("Connection Open");

                // Retrieve values from textboxes
                string name = textBox1.Text;
                string email = textBox4.Text;
                string password = textBox2.Text; // Assuming textBox4 is for password
                string phoneNumber = textBox3.Text;

                // Generate owner ID
                int ownerID = GetNextOwnerId(conn);

                // SQL query to insert data into the OWNER table
                string query = "INSERT INTO OWNER (Owner_ID, Owner_Name, Email, Phone_Number, Owner_Password) VALUES (@OwnerID, @Name, @Email, @PhoneNumber, @Password)";

                // Create SqlCommand object with parameters
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OwnerID", ownerID);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    cmd.Parameters.AddWithValue("@Password", password);

                    // Execute the query
                    cmd.ExecuteNonQuery();
                  
                }
            }
            MessageBox.Show("User Added");
            owneropt form2form = new owneropt();
            this.Hide();
            form2form.ShowDialog();
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

        private bool IsValidPhoneNumber(string number)
        {
            return Regex.IsMatch(number, @"^\d{10}$");  // Simple pattern: exactly 10 digits
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
