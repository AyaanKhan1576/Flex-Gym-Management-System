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
    public partial class memberFilterWorkoutHome : Form
    {
        public memberFilterWorkoutHome()
        {
            InitializeComponent();
        }

        private void memberFilterWorkoutHome_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            memberHome form2form = new memberHome();
            this.Hide();
            form2form.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            memberFilterWorkoutByUsers form2form = new memberFilterWorkoutByUsers();
            this.Hide();
            form2form.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            memberFilterWorkouts form2form = new memberFilterWorkouts();
            this.Hide();
            form2form.ShowDialog();
        }

        private void adlogin_Click(object sender, EventArgs e)
        {

        }
    }
}
