using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PORJDB1
{
    public partial class endgym : Form
    {
        public endgym()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            adminopt form2form = new adminopt();
            this.Hide();
            form2form.ShowDialog();
        }

       

       
   

        private void button1_Click_1(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True"))
            {
                conn.Open();
                MessageBox.Show("Connection Open");

                string gymname = textBox2.Text;
                string location = textBox3.Text;

                string query = "SELECT COUNT(*) FROM GYM WHERE Gym_Name = @gymname AND Location = @location";
                SqlCommand cm = new SqlCommand(query, conn);
                cm.Parameters.AddWithValue("@gymname", gymname);
                cm.Parameters.AddWithValue("@location", location);
                int result = (int)cm.ExecuteScalar();

                if (result >= 1)
                {
                    string updateQuery = "UPDATE GYM SET Registration_Status = 'Deactivated' WHERE Gym_Name = @gymname AND Location = @location";
                    SqlCommand updateCommand = new SqlCommand(updateQuery, conn);
                    updateCommand.Parameters.AddWithValue("@gymname", gymname);
                    updateCommand.Parameters.AddWithValue("@location", location);
                    int rowsAffected = updateCommand.ExecuteNonQuery();
                    MessageBox.Show("Gym membership successfully deactivated.");
                }
                else
                {
                    MessageBox.Show("Enter correct gym name and location");
                }
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True"))
            {
                conn.Open();
                MessageBox.Show("Connection Open");

                string gymname = textBox2.Text;
                string location = textBox3.Text;

                string query = "SELECT COUNT(*) FROM GYM WHERE Gym_Name = @gymname AND Location = @location";
                SqlCommand cm = new SqlCommand(query, conn);
                cm.Parameters.AddWithValue("@gymname", gymname);
                cm.Parameters.AddWithValue("@location", location);
                int result = (int)cm.ExecuteScalar();

                if (result >= 1)
                {
                    if (MessageBox.Show("Are you sure you want to permanently remove this gym membership? This action cannot be undone.", "Confirm:", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        string getGymIdQuery = "SELECT Gym_ID FROM GYM WHERE Gym_Name = @gymname AND Location = @location";
                        SqlCommand getGymIdCmd = new SqlCommand(getGymIdQuery, conn);
                        getGymIdCmd.Parameters.AddWithValue("@gymname", gymname);
                        getGymIdCmd.Parameters.AddWithValue("@location", location);
                        object gymIdObj = getGymIdCmd.ExecuteScalar();

                        string deleteQuery = "DELETE FROM GYM WHERE Gym_Name = @gymname AND Location = @location"; // Assuming cascade delete is set up
                        SqlCommand deleteCommand = new SqlCommand(deleteQuery, conn);
                        deleteCommand.Parameters.AddWithValue("@gymname", gymname);
                        deleteCommand.Parameters.AddWithValue("@location", location);
                        int rowsAffected = deleteCommand.ExecuteNonQuery();

                        string deleteGymQuery1 = "DELETE FROM PERFORMANCES WHERE Gym_ID = @gymIdObj";
                        SqlCommand deleteGymCmd1 = new SqlCommand(deleteGymQuery1, conn);
                        deleteGymCmd1.Parameters.AddWithValue("@gymIdObj", gymIdObj);
                       

                        string deleteGymQuery2 = "DELETE FROM TRAINERS WHERE Gym_ID = @gymIdObj";
                        SqlCommand deleteGymCmd2 = new SqlCommand(deleteGymQuery2, conn);
                        deleteGymCmd2.Parameters.AddWithValue("@gymIdObj", gymIdObj);
                       
                        MessageBox.Show("Gym membership successfully removed.");
                    }
                }
                else
                {
                    MessageBox.Show("Enter correct gym id and location");
                }
            }
        }
    }
}