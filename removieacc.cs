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
    public partial class removieacc : Form
    {
        public removieacc()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            owneropt form2form = new owneropt();
            this.Hide();
            form2form.ShowDialog();
        }

        private bool ValidateMember(string memberId, string accName)
        {
            string connectionString = "Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Construct the query to validate member details
                string query = "SELECT COUNT(*) FROM MEMBERS WHERE Member_ID = @memberId AND Member_Name = @accName";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@memberId", memberId);
                    command.Parameters.AddWithValue("@accName", accName);

                    int count = Convert.ToInt32(command.ExecuteScalar());

                    // If count > 0, the details are valid
                    return count > 0;
                }
            }
        }

        private bool ValidateTrainer(string trainerId, string accName)
        {
            string connectionString = "Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Construct the query to validate trainer details
                string query = "SELECT COUNT(*) FROM TRAINERS WHERE Trainer_ID = @trainerId AND Trainer_Name = @accName";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@trainerId", trainerId);
                    command.Parameters.AddWithValue("@accName", accName);

                    int count = Convert.ToInt32(command.ExecuteScalar());

                    // If count > 0, the details are valid
                    return count > 0;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True"))
            {
                conn.Open();
                MessageBox.Show("Connection Open");
                string role = textBox3.Text;

                if (role == "Member")
                {
                    string memberId = textBox2.Text;
                    string accname = textBox4.Text;

                    // Validate input against members table
                    if (ValidateMember(memberId, accname))
                    {
                        if (MessageBox.Show("Are you sure you want to permanently remove this Member's membership? This action cannot be undone.", "Confirm:", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            string getmemberIdQuery = "SELECT Member_ID FROM MEMBERS WHERE Member_Name = @accname";
                            SqlCommand getmemberId = new SqlCommand(getmemberIdQuery, conn);
                            getmemberId.Parameters.AddWithValue("@accname", accname);
                            object memIdObj = getmemberId.ExecuteScalar();

                            string deleteQuery = "DELETE FROM MEMBERS WHERE Member_ID = @memIdObj";
                            SqlCommand deleteCommand = new SqlCommand(deleteQuery, conn);
                            deleteCommand.Parameters.AddWithValue("@memIdObj", memIdObj);
                            int rowsAffected = deleteCommand.ExecuteNonQuery();

                            if (rowsAffected > 0)

                            {
                                
                                string deleteQuery1 = "DELETE FROM ALLERGIES WHERE Member_ID = @memIdObj";
                                SqlCommand deleteCommand1 = new SqlCommand(deleteQuery1, conn);
                                deleteCommand.Parameters.AddWithValue("@memIdObj", memIdObj);

                                string deleteQuery2 = "DELETE FROM RATING_NEW WHERE Member_ID = @memIdObj";
                                SqlCommand deleteCommand2 = new SqlCommand(deleteQuery2, conn);
                                deleteCommand.Parameters.AddWithValue("@memIdObj", memIdObj);

                                string deleteQuery3 = "DELETE FROM LOGIN_LOG_MEMBERS WHERE Member_ID = @memIdObj";
                                SqlCommand deleteCommand3 = new SqlCommand(deleteQuery3, conn);
                                deleteCommand.Parameters.AddWithValue("@memIdObj", memIdObj);

                                string deleteQuery4 = "DELETE FROM TRAINING_SESSION WHERE Member_ID = @memIdObj";
                                SqlCommand deleteCommand4 = new SqlCommand(deleteQuery4, conn);
                                deleteCommand.Parameters.AddWithValue("@memIdObj", memIdObj);

                                string deleteQuery5 = "DELETE FROM DIET_PLAN WHERE Member_ID = @memIdObj";
                                SqlCommand deleteCommand5 = new SqlCommand(deleteQuery5, conn);
                                deleteCommand.Parameters.AddWithValue("@memIdObj", memIdObj);
                            }

                            MessageBox.Show("Member account removed successfully!");
                        }
                       
                    }
                    else
                    {
                        MessageBox.Show("Invalid member details. Please check the input.");
                    }
                }
                else if (role == "Trainer")
                {
                    string trainerId = textBox2.Text;
                    string accname = textBox4.Text;

                    
                    if (ValidateTrainer(trainerId, accname))
                    {
                        if (MessageBox.Show("Are you sure you want to permanently remove this Trainer's account? This action cannot be undone.", "Confirm:", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            string gettrainerIdQuery = "SELECT Trainer_ID FROM TRAINERS WHERE Trainer_Name = @accname";
                            SqlCommand gettrainerid = new SqlCommand(gettrainerIdQuery, conn);
                            gettrainerid.Parameters.AddWithValue("@accname", accname);
                            object traineridObj = gettrainerid.ExecuteScalar();

                            string deleteQuery = "DELETE FROM TRAINERS WHERE Trainer_ID = @traineridObj";
                            SqlCommand deleteCommand = new SqlCommand(deleteQuery, conn);
                            deleteCommand.Parameters.AddWithValue("@traineridObj", traineridObj);
                            int rowsAffected = deleteCommand.ExecuteNonQuery();

                            if (rowsAffected > 0)

                            {

                                string deleteQuery1 = "DELETE FROM TRAINER_WORKOUT_PLAN WHERE Trainer_ID = @traineridObj";
                                SqlCommand deleteCommand1 = new SqlCommand(deleteQuery1, conn);
                                deleteCommand.Parameters.AddWithValue("@traineridObj", traineridObj);

                                string deleteQuery2 = "DELETE FROM RATING_NEW WHERE Trainer_ID = @traineridObj";
                                SqlCommand deleteCommand2 = new SqlCommand(deleteQuery2, conn);
                                deleteCommand.Parameters.AddWithValue("@traineridObj", traineridObj);

                                string deleteQuery3 = "DELETE FROM CREATES_CUSTOMISE_PLAN WHERE Trainer_ID = @traineridObj";
                                SqlCommand deleteCommand3 = new SqlCommand(deleteQuery3, conn);
                                deleteCommand.Parameters.AddWithValue("@traineridObj", traineridObj);

                                string deleteQuery4 = "DELETE FROM TRAINING_SESSION WHERE Trainer_ID = @traineridObj";
                                SqlCommand deleteCommand4 = new SqlCommand(deleteQuery4, conn);
                                deleteCommand.Parameters.AddWithValue("@traineridObj", traineridObj);

                                string deleteQuery5 = "DELETE FROM DIET_PLAN WHERE Trainer_ID = @traineridObj";
                                SqlCommand deleteCommand5 = new SqlCommand(deleteQuery5, conn);
                                deleteCommand.Parameters.AddWithValue("@traineridObj", traineridObj);


                                string deleteQuery6 = "DELETE FROM CREATES_CUSTOMISEZ WHERE Trainer_ID = @traineridObj";
                                SqlCommand deleteCommand6 = new SqlCommand(deleteQuery6, conn);
                                deleteCommand.Parameters.AddWithValue("@traineridObj", traineridObj);
                            }
                            MessageBox.Show("Trainer account removed successfully!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid trainer details. Please check the input.");
                    }
                }
                else
                {
                    MessageBox.Show("Invalid role. Please enter 'member' or 'trainer'.");
                }
            }

        }
    }
}