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
    public partial class memberDietHome : Form
    {
        public memberDietHome()
        {
            InitializeComponent();
        }

        private void memberDietHome_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            memberCreateDietPlan form2form = new memberCreateDietPlan();
            this.Hide();
            form2form.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            memberFilterDietsHome form2form = new memberFilterDietsHome();
            this.Hide();
            form2form.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            memberHome form2form = new memberHome();
            this.Hide();
            form2form.ShowDialog();

        }

        private void adlogin_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            memberShowUserOwnDietPlan form2form = new memberShowUserOwnDietPlan();
            this.Hide();
            form2form.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            memberShowOthersDietPlans form2form = new memberShowOthersDietPlans();
            this.Hide();
            form2form.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            memberShowTrainersDietPlans form2form = new memberShowTrainersDietPlans();
            this.Hide();
            form2form.ShowDialog();
        }
    }
}
