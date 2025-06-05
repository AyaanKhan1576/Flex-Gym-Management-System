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
    public partial class R8 : Form
    {
        public R8()
        {
            InitializeComponent();
            PopulateDataGridView();
        }

        private void PopulateDataGridView()
        {
            string connectionString = "Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True";
            string query = @"SELECT DISTINCT dp.Plan_Name
FROM MEMBER_DIET_PLAN_NEW dp
LEFT JOIN MEALS_NEW mn ON dp.Meal_ID = mn.Meal_ID
LEFT JOIN ALLERGENS a ON mn.Allergen_ID = a.Allergen_ID
WHERE a.Allergen_Name IS NULL OR a.Allergen_Name <> 'Peanuts';
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

        private void R8_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
          allreports form2form = new allreports();
            this.Hide();
            form2form.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
