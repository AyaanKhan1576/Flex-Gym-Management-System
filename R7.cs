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
    public partial class R7 : Form
    {
        public R7()
        {
            InitializeComponent();
          //  PopulateDataGridView();
        }

        private void PopulateDataGridView(int machineID)
        {
            string connectionString = "Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True";
            string query = @"
                SELECT DISTINCT mwp.Plan_ID, mwp.Plan_Name
        FROM MEMBER_WORKOUT_PLAN mwp
        LEFT JOIN EXERCISE e ON mwp.Exercise_ID = e.Exercise_ID
        LEFT JOIN MACHINES m ON e.Machine_ID = m.Machine_ID
        WHERE e.Machine_ID IS NULL OR m.Machine_ID != @machineID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@machineID", machineID);
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private int GetMachineID(string machineName)
        {
            int machineID = -1;
            string connectionString = "Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True";
            string query = "SELECT Machine_ID FROM MACHINES WHERE Machine_Name = @machineName";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@machineName", machineName);

                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                {
                    machineID = Convert.ToInt32(result);
                }
            }

            return machineID;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string machineName = textBox1.Text;
            int machineID = GetMachineID(machineName);

            if (machineID != -1)
            {
                PopulateDataGridView(machineID);
            }
            else
            {
                MessageBox.Show("Machine not found.");
            }
        }
    }
}
