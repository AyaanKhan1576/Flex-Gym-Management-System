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
    public partial class R1 : Form
    {
        public R1()
        {
            InitializeComponent();
            
        }

        private void PopulateDataGridView(int gymId, int trainerId)
        {
            string connectionString = "Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True";
            string query = @"
                SELECT m.Member_Name, m.Email, m.Phone_Number
                FROM MEMBERS m
                INNER JOIN TRAINERS t ON m.Member_ID = t.Trainer_ID
                WHERE t.Trainer_ID = @trainerID AND t.Gym_ID = @gymID;
            ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@trainerID", trainerId);
                command.Parameters.AddWithValue("@gymID", gymId);

                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            allreports form2form = new allreports();
            this.Hide();
            form2form.ShowDialog();
        }

        private int GetGymId(string gymName)
        {
            string connectionString = "Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True";
            string query = "SELECT Gym_ID FROM GYM WHERE Gym_Name = @gymName";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@gymName", gymName);

                connection.Open();
                object result = command.ExecuteScalar();
                return result == null ? -1 : Convert.ToInt32(result);
            }
        }

        private int GetTrainerId(string trainerName)
        {
            string connectionString = "Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True";
            string query = "SELECT Trainer_ID FROM TRAINERS WHERE Trainer_Name = @trainerName";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@trainerName", trainerName);

                connection.Open();
                object result = command.ExecuteScalar();
                return result == null ? -1 : Convert.ToInt32(result);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string gymName = textBox1.Text;
            string trainerName = textBox2.Text;

            int gymId = GetGymId(gymName);
            int trainerId = GetTrainerId(trainerName);

            if (gymId == -1 || trainerId == -1)
            {
                MessageBox.Show("Invalid gym or trainer name.");
                return;
            }

            PopulateDataGridView(gymId, trainerId);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
