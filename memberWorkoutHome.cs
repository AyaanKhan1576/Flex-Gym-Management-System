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
    public partial class memberWorkoutHome : Form
    {
        public memberWorkoutHome()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            memberFilterWorkouts form2form = new memberFilterWorkouts();
            this.Hide();
            form2form.ShowDialog();

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            memberHome form2form = new memberHome();
            this.Hide();
            form2form.ShowDialog();

        }
    }
}
