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
    public partial class R3 : Form
    {
        private string connectionString = "Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True";
        public R3()
        {
            InitializeComponent();
        }

        private DataTable GetMembersOfTrainer(string trainerName)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                 SELECT DISTINCT m.Member_ID, m.Member_Name, m.Email, m.Phone_Number
                    FROM MEMBERS m
                    JOIN MEMBER_DIET_PLAN_NEW mdp ON m.Member_ID = mdp.Member_ID
                    JOIN TRAINERS t ON mdp.Member_ID = t.Trainer_ID
                    WHERE t.Trainer_Name = @trainerName";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@trainerName", trainerName);

                    try
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dataTable);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }

            return dataTable;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string trainerName = textBox1.Text;

            DataTable dataTable = GetMembersOfTrainer(trainerName);

            if (dataTable != null)
            {
                dataGridView1.DataSource = dataTable;
            }
            else
            {
                MessageBox.Show("Trainer not found or no members found for the trainer.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            allreports form2form = new allreports();
            this.Hide();
            form2form.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
