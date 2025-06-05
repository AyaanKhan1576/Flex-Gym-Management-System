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
    public partial class trianer2 : Form
    {
        public trianer2()
        {
            InitializeComponent();
        }

        private void trianer2_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            TrainerWorkoutPlan form2form = new TrainerWorkoutPlan();
            this.Hide();
            form2form.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            TrainerFeedback form2form = new TrainerFeedback();
            this.Hide();
            form2form.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            trainersAppointmentView form2form = new trainersAppointmentView();
            this.Hide();
            form2form.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 form2form = new Form1();
            this.Hide();
            form2form.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TrainerDietPlans form2form = new TrainerDietPlans();
            this.Hide();
            form2form.ShowDialog();
        }
    }
}
