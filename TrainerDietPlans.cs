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
    public partial class TrainerDietPlans : Form
    {
        public TrainerDietPlans()
        {
            InitializeComponent();
        }

        private void adlogin_Click(object sender, EventArgs e)
        {

        }

        private void TrainerDietPlans_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            TrainerCreateDietPlan form2form = new TrainerCreateDietPlan();
            this.Hide();
            form2form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TrainerViewDietPlan form2form = new TrainerViewDietPlan();
            this.Hide();
            form2form.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            trianer2 form2form = new trianer2();
            this.Hide();
            form2form.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            TrainersDietFilter form2form = new TrainersDietFilter();
            this.Hide();
            form2form.ShowDialog();
        }
    }
}
