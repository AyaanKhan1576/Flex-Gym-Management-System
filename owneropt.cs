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
    public partial class owneropt : Form
    {
        public owneropt()
        {
            InitializeComponent();
        }

        public readonly string gymname;
        public owneropt(string gymname)
        {
           this.gymname= gymname;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            meberreport form2form = new meberreport();
            this.Hide();
            form2form.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            addtrainer form2form = new addtrainer(gymname);
            this.Hide();
            form2form.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 form2form = new Form1();
            this.Hide();
            form2form.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            removieacc form2form = new removieacc();
            this.Hide();
            form2form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            trainreport form2form = new trainreport();
            this.Hide();
            form2form.ShowDialog();
        }
    }
}
