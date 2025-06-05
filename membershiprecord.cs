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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace PORJDB1
{
    public partial class membershiprecord : Form
    {
        public membershiprecord()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private int GetNextRecordId(SqlConnection conn)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Record_ID FROM MEMBERS_RECORD ORDER BY Record_ID DESC;\r\n", conn))
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
            using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Member_ID FROM MEMBERS ORDER BY Member_ID DESC;\r\n", conn))
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

                int recordID = GetNextRecordId(conn);

                // Check if any item is selected in the checkedListBox3
                int duration = 0;
                if (checkedListBox3.CheckedItems.Count > 0)
                {
                    duration = Convert.ToInt32(checkedListBox3.CheckedItems[0]);
                }

                string paymentType = checkedListBox1.CheckedItems[0].ToString();
                string membershipType = checkedListBox2.CheckedItems[0].ToString();
                string starting_date = dateTimePicker1.Value.ToString();

                if (checkedListBox3.CheckedItems.Count > 1 || checkedListBox1.CheckedItems.Count > 1 || checkedListBox2.CheckedItems.Count > 1)
                {
                    MessageBox.Show("Select Only 1 Option");
                    return;
                }
                    // Insert into MEMBERS_RECORD table
                    string query = "INSERT INTO MEMBERS_RECORD (Record_ID, Membership_Duration, Payment, Membership_Type, Starting_Date) VALUES (@RecordID, @Duration, @Payment_Type, @Membership_Type, @StartingDate)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@RecordID", recordID);
                    cmd.Parameters.AddWithValue("@StartingDate", starting_date);
                    cmd.Parameters.AddWithValue("@Duration", duration);
                    cmd.Parameters.AddWithValue("@Payment_Type", paymentType);
                    cmd.Parameters.AddWithValue("@Membership_Type", membershipType);
                    cmd.ExecuteNonQuery();
                }

                // Insert into MEMBERS table with condition to specify the member
                int memberId = GetMemberID(conn);
                query = "UPDATE MEMBERS SET Record_ID = @RecordID WHERE Member_ID = @MemberID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@RecordID", recordID);
                    cmd.Parameters.AddWithValue("@MemberID", memberId);
                    cmd.ExecuteNonQuery();
                }
            }

            memberHome form2form = new memberHome();
            this.Hide();
            form2form.ShowDialog();
        }

        private void membershiprecord_Load(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
