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
    public partial class r13 : Form
    {
        public r13()
        {
            InitializeComponent();
            PopulateDataGridView();
        }
        private void PopulateDataGridView()
        {
            string connectionString = "Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True";
            string query = @"
SELECT Plan_Name, Workout_Goals AS Goal
FROM MEMBER_WORKOUT_PLAN
WHERE Workout_Goals = 'Weight loss'
UNION ALL
SELECT Plan_Name, Workout_Goals AS Goal
FROM TRAINER_WORKOUT_PLAN
WHERE Workout_Goals = 'Weight loss';

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
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            allreports form2form = new allreports();
            this.Hide();
            form2form.ShowDialog();

        }
    }
}
