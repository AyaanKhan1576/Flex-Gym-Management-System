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
    public partial class r20 : Form
    {
        public r20()
        {
            InitializeComponent();
            PopulateDataGridView();
        }
        private void PopulateDataGridView()
        {
            string connectionString = "Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True";
            string query = @"
SELECT t.Trainer_Name, COUNT(*) AS Cardio_Plans
FROM TRAINER_WORKOUT_PLAN twp
JOIN TRAINERS t ON twp.Trainer_ID = t.Trainer_ID
WHERE Workout_Goals = 'Cardiovascular fitness'
GROUP BY t.Trainer_Name
ORDER BY Cardio_Plans DESC;


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
        private void r20_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            allreports form2form = new allreports();
            this.Hide();
            form2form.ShowDialog();
        }
    }
}
