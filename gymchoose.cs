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
    public partial class select_gym : Form
    {
        public select_gym()
        {
            InitializeComponent();
            PopulateDataGridView();
          //  AddButtonColumns();
        }

        private void PopulateDataGridView()
        {
            string connectionString = "Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True";
            string query = @"SELECT Gym_ID, Gym_Name, Location, Registration_status
                     FROM GYM";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string gymName = textBox1.Text;

            if (!string.IsNullOrEmpty(gymName))
            {
                string connectionString = "Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string checkQuery = "SELECT COUNT(*) FROM GYM WHERE Gym_Name = @gymName";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@gymName", gymName);
                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                        if (count > 0)
                        {

                            MessageBox.Show("Gym selected successfully");
                            addtrainer addTrainerForm = new addtrainer(gymName); 
                            addTrainerForm.Show();
                            ownersignup form2form = new ownersignup(gymName);
                            this.Hide();
                            form2form.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("Gym name doesn't exists in Database.Choose again");

                        }
                    }
                }
            }


           
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            newgym form2form = new newgym();
            this.Hide();
            form2form.ShowDialog();
        }
    }
}
