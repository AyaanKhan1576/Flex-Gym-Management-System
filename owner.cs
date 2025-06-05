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
    public partial class owner : Form
    {
        public owner()
        {
            InitializeComponent();
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            select_gym forming2 = new select_gym();
            forming2.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void owner_Load(object sender, EventArgs e)
        {

        }

        private void login_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True"; // Replace with your actual connection string
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string username = usertb.Text;
                string password = passtb.Text;
                string gymName = textBox1.Text; // Change variable name to gymName to represent gym oname
                int ownerId = -1;

                string query = "SELECT Owner_Id FROM OWNER WHERE Owner_Name = @Username AND Owner_Password = @Password";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    connection.Open();
                    MessageBox.Show("Connection Open");
                    var result = command.ExecuteScalar();

                    if (result != null)
                    {
                        ownerId = Convert.ToInt32(result);
                    }
                }

                if (ownerId != -1)
                {
                    query = "SELECT COUNT(*) FROM GYM WHERE Gym_Name = @GymName AND Owner_ID = @OwnerId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@GymName", gymName);
                        command.Parameters.AddWithValue("@OwnerId", ownerId);

                        int count = (int)command.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show("Owner Found");
                            
                            owneropt form2form = new owneropt(gymName);
                            this.Hide();
                            form2form.ShowDialog(); // Owner is associated with the specified gym
                        }
                        else
                            MessageBox.Show("The owner is not associated with the specified gym.");
                    }
                }
                else
                    MessageBox.Show("Enter correct username and password");

                connection.Close();
            }
        }



        private void owner_pass_Click(object sender, EventArgs e)
        {

        }
    }
}
