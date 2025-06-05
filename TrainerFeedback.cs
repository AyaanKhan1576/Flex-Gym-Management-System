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
    public partial class TrainerFeedback : Form
    {
        private int trainerID;
        string connectionString = "Data Source=DESKTOP-SAVDUA5\\SQLEXPRESS;Initial Catalog=PROJ_2;Integrated Security=True"; // Update with your actual connection string
        public TrainerFeedback()
        {
            InitializeComponent();
            this.trainerID = GetTrainerID();
            LoadTrainerFeedbacks(trainerID);
        }
        private void LoadTrainerFeedbacks(int trainerID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT m.Member_Name, r.Rating_Score, r.Feedback_Comments
            FROM MEMBERS m
            JOIN RATING_NEW r ON m.Member_ID = r.User_ID
            WHERE r.Trainer_ID = @TrainerID;
        ";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TrainerID", trainerID);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Calculate the average rating
                    double totalRating = 0;
                    int feedbackCount = dt.Rows.Count;
                    foreach (DataRow row in dt.Rows)
                    {
                        totalRating += Convert.ToInt32(row["Rating_Score"]);
                    }
                    double avgRating = feedbackCount > 0 ? totalRating / feedbackCount : 0;

                    // Add a new row to display the average rating
                    DataRow avgRow = dt.NewRow();
                    avgRow["Member_Name"] = "Average Rating";
                    avgRow["Rating_Score"] = avgRating.ToString("0.##"); // Round to 2 decimal places
                    avgRow["Feedback_Comments"] = "Average rating based on feedback from all members.";
                    dt.Rows.Add(avgRow);

                    // Update the DataGridView with the updated DataTable
                    dataGridView1.DataSource = dt;
                }
            }
        }

        private int GetTrainerID()
        {
            int trainerID = -1;
            // Retrieve Trainer ID from LOGIN_LOG_TRAINERS
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 User_ID FROM LOGIN_LOG_TRAINERS ORDER BY TrainerLogin_ID DESC", conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        trainerID = Convert.ToInt32(reader["User_ID"]);
                    }
                    reader.Close();
                }
            }
            return trainerID;
        }

        private void TrainerFeedback_Load(object sender, EventArgs e)
        {
        }

        private void adlogin_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            trianer2 form2form = new trianer2();
            this.Hide();
            form2form.ShowDialog();
        }
    }
}
