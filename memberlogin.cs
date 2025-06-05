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
    public partial class memberlogin : Form
    {
        public memberlogin()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            membersignup form2form = new membersignup();
            this.Hide();
            form2form.ShowDialog();
        }

        private void memberlogin_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True");
            conn.Open();
            MessageBox.Show("Connection Open");
            SqlCommand cm;

            string username = textBox1.Text;
            string password = textBox2.Text;
            //int memberID = -1;

            string query = "SELECT Member_ID FROM MEMBERS WHERE Email = '" + username + "' AND Member_Password ='" + password + "'";
            cm = new SqlCommand(query, conn);
            int result = (int)cm.ExecuteScalar();

            if (result >= 1)
                MessageBox.Show("User found");
            else
                MessageBox.Show("Enter correct email and password");

            

            if (result >= 1)
            {
                // Define your SQL query with parameters
                string insertQuery = "INSERT INTO LOGIN_LOG_MEMBERS (User_ID) VALUES (@MemberID)";

                // Create a connection to the database
                    // Create a command object with the insert query and the connection
                    using (SqlCommand command = new SqlCommand(insertQuery, conn))
                    {
                        // Add a parameter for the memberID
                        command.Parameters.AddWithValue("@MemberID", result);

                        // Open the connection

                        // Execute the insert query
                        command.ExecuteNonQuery();
                    }
                

                cm.Dispose();
                conn.Close();


                memberHome form2form = new memberHome();
                this.Hide();
                form2form.ShowDialog();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
