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
    public partial class addtrainer : Form
    {
        public addtrainer()
        {
            InitializeComponent();
        }

        private readonly string gymName;
        public addtrainer(string gymName)
        {
            InitializeComponent();
            this.gymName = gymName;
            textBox2.Text = gymName;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            owneropt form2form = new owneropt();
            this.Hide();
            form2form.ShowDialog();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            addtrainer form2form = new addtrainer();
            this.Hide();
            form2form.ShowDialog();
        }

        private bool TrainerExists(string trainerName, string contact)
        {
            string connectionString = "Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Construct the query to check if the trainer exists
                string query = "SELECT COUNT(*) FROM TRAINERS WHERE Trainer_Name = @trainerName OR Phone_Number = @contact";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@trainerName", trainerName);
                    command.Parameters.AddWithValue("@contact", contact);

                    int count = Convert.ToInt32(command.ExecuteScalar());

                    // If count > 0, the trainer already exists
                    return count > 0;
                }
            }
        }

        private int GetNexttrainerId(SqlConnection conn)
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
                    return 1; // Start from 1 if no IDs are present
                }
            }
        }

        private void AddTrainerToDatabase(string trainerName, string contact)
        {
            string connectionString = "Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                int trainerid = GetNexttrainerId(connection);
                // Construct the query to insert the trainer details
                string query = "INSERT INTO TRAINERS (Trainer_ID,Trainer_Name, Phone_Number) VALUES (@trainerid, @trainerName, @contact)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@trainerName", trainerName);
                    command.Parameters.AddWithValue("@contact", contact);
                    command.Parameters.AddWithValue("@trainerid", trainerid);

                    // Execute the query
                    command.ExecuteNonQuery();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string trainerName = textBox3.Text;
            string contact = textBox1.Text;

            // Check if the trainer already exists
            if (!TrainerExists(trainerName, contact))
            {
                // Trainer doesn't exist, so add the trainer to the table
                AddTrainerToDatabase(trainerName, contact);
                MessageBox.Show("Trainer added successfully!");
            }
            else
            {
                // Trainer already exists, display a message
                MessageBox.Show("Trainer already exists in the database.");
            }
        }
    }
}
