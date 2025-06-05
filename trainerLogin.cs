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
    public partial class trainerLogin : Form
    {
        public trainerLogin()
        {
            InitializeComponent();
        }

        private void trainerLogin_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            trainerSignuo form2form = new trainerSignuo();
            this.Hide();
            form2form.ShowDialog();
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

            string query = "SELECT Trainer_ID FROM TRAINERS WHERE Trainer_Name = '" + username + "' AND Trainer_Password ='" + password + "'";
            cm = new SqlCommand(query, conn);
            int result = (int)cm.ExecuteScalar();

            if (result >= 1)
                MessageBox.Show("User found");
            else
                MessageBox.Show("Enter correct email and password");



            if (result >= 1)
            {
                // Define your SQL query with parameters
                string insertQuery = "INSERT INTO LOGIN_LOG_TRAINERS (User_ID) VALUES (@TrainerID)";

                // Create a connection to the database
                // Create a command object with the insert query and the connection
                using (SqlCommand command = new SqlCommand(insertQuery, conn))
                {
                    // Add a parameter for the memberID
                    command.Parameters.AddWithValue("@TrainerID", result);

                    // Open the connection

                    // Execute the insert query
                    command.ExecuteNonQuery();
                }


                cm.Dispose();
                conn.Close();


                trianer2 form2form = new trianer2();
                this.Hide();
                form2form.ShowDialog();
            }

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }
    }
}
