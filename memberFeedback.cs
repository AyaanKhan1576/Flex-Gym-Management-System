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
    public partial class memberFeedback : Form
    {
        public memberFeedback()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            memberHome form2form = new memberHome();
            this.Hide();
            form2form.ShowDialog();

        }

        private void memberFeedback_Load(object sender, EventArgs e)
        {

        }

        private int GetNextRatingID(SqlConnection conn)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT MAX(Rating_ID) FROM RATING_NEW", conn))
            {
                object result = cmd.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    return Convert.ToInt32(result) + 1;
                }
                else
                {
                    return 11; // Start from 11 if no IDs are present
                }
            }
        }

        private int GetMemberID(SqlConnection conn)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 User_ID FROM LOGIN_LOG_MEMBERS ORDER BY Login_ID DESC;\r\n", conn))
            {
                object result = cmd.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    return Convert.ToInt32(result);
                }
                else
                {
                    return 11; // Start from 11 if no IDs are present
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                int ratingId = GetNextRatingID(conn);
                string comment = textBox4.Text;
                int memberID = GetMemberID(conn); ;
                int trainerID;
                if (!int.TryParse(textBox1.Text, out trainerID))
                {
                    MessageBox.Show("Enter Correct ID");
                    return;
                }

                // Check if any item is selected in the checkedListBox3
                int rating = 0;
                if (checkedListBox1.CheckedItems.Count > 0)
                {
                    rating = Convert.ToInt32(checkedListBox1.CheckedItems[0]);
                }


                if (checkedListBox1.CheckedItems.Count > 1)
                {
                    MessageBox.Show("Select Only 1 Option");
                    return;
                }
                
                string query = "INSERT INTO RATING_NEW (Rating_ID, User_ID, Trainer_ID, Rating_Score, Feedback_Comments) VALUES (@RatingID, @UserID, @TrainerID, @RatingScore, @FeedbackComments)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@RatingID", ratingId);
                    cmd.Parameters.AddWithValue("@UserID", memberID);
                    cmd.Parameters.AddWithValue("@TrainerID", trainerID);
                    cmd.Parameters.AddWithValue("@RatingScore", rating);
                    cmd.Parameters.AddWithValue("@FeedbackComments", comment);
                    cmd.ExecuteNonQuery();
                }
            }

            memberHome form2form = new memberHome();
            this.Hide();
            form2form.ShowDialog();
        }
    }
}
