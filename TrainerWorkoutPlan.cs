using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PORJDB1
{
    public partial class TrainerWorkoutPlan : Form
    {
        public TrainerWorkoutPlan()
        {
            InitializeComponent();
        }

        private void adlogin_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            trianer2 form2form = new trianer2();
            this.Hide();
            form2form.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            trainerViewWorkoutPlan form2form = new trainerViewWorkoutPlan();
            this.Hide();
            form2form.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TainerCreateWorkoutPlan form2form = new TainerCreateWorkoutPlan();
            this.Hide();
            form2form.ShowDialog();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            TrainerWorkoutFilter form2form = new TrainerWorkoutFilter();
            this.Hide();
            form2form.ShowDialog();
        }

        private void TrainerWorkoutPlan_Load(object sender, EventArgs e)
        {

        }
    }
}
