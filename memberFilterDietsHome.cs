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
    public partial class memberFilterDietsHome : Form
    {
        public memberFilterDietsHome()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            memberDietHome form2form = new memberDietHome();
            this.Hide();
            form2form.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            memberFilterDietsByUsers form2form = new memberFilterDietsByUsers();
            this.Hide();
            form2form.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            memberFilterDiets form2form = new memberFilterDiets();
            this.Hide();
            form2form.ShowDialog();
        }
    }
}
