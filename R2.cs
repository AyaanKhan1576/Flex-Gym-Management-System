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
    public partial class R2 : Form
    {
        string connectionString = "Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True";
        public R2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            allreports form2form = new allreports();
            this.Hide();
            form2form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string DietPlan = textBox1.Text;

            
            string query = "SELECT * FROM MEMBER_DIET_PLAN_NEW WHERE Plan_Name = @DietPlan";

          
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Open the connection
                connection.Open();

              
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    
                    command.Parameters.AddWithValue("@DietPlan", DietPlan);

                   
                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    
                    DataTable dataTable = new DataTable();


                    adapter.Fill(dataTable);

                    // Bind the data table to the DataGridView
                    dataGridView1.DataSource = dataTable;
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void R2_Load(object sender, EventArgs e)
        {

        }
    }
}
