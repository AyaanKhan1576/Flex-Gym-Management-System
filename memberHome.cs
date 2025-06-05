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
    public partial class memberHome : Form
    {
        public memberHome()
        {
            InitializeComponent();
        }

        private void adlogin_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            memberBookTrainer form2form = new memberBookTrainer();
            this.Hide();
            form2form.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            memberDietHome form2form = new memberDietHome();
            this.Hide();
            form2form.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            memberWorkoutHome form2form = new memberWorkoutHome();
            this.Hide();
            form2form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            memberFeedback form2form = new memberFeedback();
            this.Hide();
            form2form.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 form2form = new Form1();
            this.Hide();
            form2form.ShowDialog();
        }
    }
}
