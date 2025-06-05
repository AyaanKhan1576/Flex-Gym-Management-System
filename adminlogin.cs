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
    public partial class adminlogin : Form
    {
        public adminlogin()
        {
            InitializeComponent();
        }

        private void adminlogin_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True");
            conn.Open();
            MessageBox.Show("Connection Open");
            SqlCommand cm;

            string username = textBox2.Text;
            string password = textBox1.Text;

            string query = "SELECT COUNT(*) FROM ADMIN WHERE Admin_Name = '" + username + "' AND Admin_Password ='" + password + "'";
            cm = new SqlCommand(query, conn);
            int result = (int)cm.ExecuteScalar();

            if (result >= 1)
                MessageBox.Show("User found");
            else
                MessageBox.Show("Enter correct username and password");

            cm.Dispose();
            conn.Close();

            if (result >= 1)
            {
                
                adminopt form2form = new adminopt();
                this.Hide();
                form2form.ShowDialog();
            }

        }
    }
}
