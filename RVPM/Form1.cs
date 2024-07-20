using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RVPM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Normal;
            this.AutoSize = true;

            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void size(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            SETTINGS_LOGIN settings = new SETTINGS_LOGIN();
            settings.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
          
            print prnt = new print();
            prnt.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
          
            copy1 copy = new copy1();
            copy.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
           
            scan1 scan = new scan1();
            scan.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }
    }
}
