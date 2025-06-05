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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void admin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            adminlogin forming2 = new adminlogin();
            forming2.Show();
            this.Hide();
        }

        private void owner_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            owner forming2 = new owner();
            forming2.Show();
            this.Hide();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)//trainer
        {
            trainerLogin form2form = new trainerLogin();
            this.Hide();
            form2form.ShowDialog();
        }

        private void mem_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            memberlogin form2form = new memberlogin();
            this.Hide();
            form2form.ShowDialog();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            allreports form2form = new allreports();
            this.Hide();
            form2form.ShowDialog();
        }
    }
}
