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
    public partial class allreports : Form
    {
        public allreports()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            R1 form2form = new R1();
            this.Hide();
            form2form.ShowDialog();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            R2 form2form = new R2();
            this.Hide();
            form2form.ShowDialog();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Form1 form2form = new Form1();
            this.Hide();
            form2form.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            R5 form2form = new R5();
            this.Hide();
            form2form.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            R6 form2form = new R6();
            this.Hide();
            form2form.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            R8 form2form = new R8();
            this.Hide();
            form2form.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            R9 form2form = new R9();
            this.Hide();
            form2form.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            R10 form2form = new R10();
            this.Hide();
            form2form.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            R7 form2form = new R7();
            this.Hide();
            form2form.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
          
        }

        private void button10_Click(object sender, EventArgs e)
        {
            R3 form2form = new R3();
            this.Hide();
            form2form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            r11 form2form = new r11();
            this.Hide();
            form2form.ShowDialog();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            r12 form2form = new r12();
            this.Hide();
            form2form.ShowDialog();
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            r13 form2form = new r13();
            this.Hide();
            form2form.ShowDialog();

        }

        private void button17_Click(object sender, EventArgs e)
        {
            r14 form2form = new r14();
            this.Hide();
            form2form.ShowDialog();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            r15 form2form = new r15();
            this.Hide();
            form2form.ShowDialog();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            Report16 form2form = new Report16();
            this.Hide();
            form2form.ShowDialog();

        }

        private void button18_Click(object sender, EventArgs e)
        {
            Report17 form2form = new Report17();
            this.Hide();
            form2form.ShowDialog();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Report18 form2form = new Report18();
            this.Hide();
            form2form.ShowDialog();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Report19 form2form = new Report19();
            this.Hide();
            form2form.ShowDialog();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            r20 form2form = new r20();
            this.Hide();
            form2form.ShowDialog();
        }
    }
}
