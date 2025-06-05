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
    public partial class R6 : Form
    {
        public R6()
        {
            InitializeComponent();
            PopulateDataGridView();
        }

        private void PopulateDataGridView()
        {
            string connectionString = "Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True";
            string query = @"SELECT dp.Diet_Plan_ID, dp.Plan_Name, dp.Member_ID, dp.Trainer_ID, dp.Meal_ID, dp.Diet_Goal
                FROM DIET_PLAN dp
                JOIN MEALS m ON dp.Meal_ID = m.Meal_ID
                 JOIN NUTRITIONAL_INFO ni ON m.Meal_ID = ni.Meal_ID
                GROUP BY dp.Diet_Plan_ID, dp.Plan_Name, dp.Member_ID, dp.Trainer_ID, dp.Meal_ID, dp.Diet_Goal
                HAVING SUM(ni.Carbohydrates) < 300;
";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            allreports form2form = new allreports();
            this.Hide();
            form2form.ShowDialog();
        }
    }
}
