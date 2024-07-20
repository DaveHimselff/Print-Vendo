using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;

using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace RVPM
{
    public partial class AdminSetting : Form
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public AdminSetting()
        {
            InitializeComponent();
            this.AutoSize = true;

            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            frm1.Show();
            this.Hide();
        }

        private void comboBox7_DrawItem(object sender, DrawItemEventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int n1 = (int)numericUpDown1.Value;
            int n2 = (int)numericUpDown2.Value;
            int n3 = (int)numericUpDown3.Value;
            int n4 = (int)numericUpDown4.Value;
            int n5 = (int)numericUpDown5.Value;
            string str1 = textBox1.Text;
            string str2 = textBox2.Text;
            string str3 = comboBox1.Text;
            string str4 = textBox3.Text;

            try
            {

                string MyConnection2 = "datasource=127.0.0.1;port=3306;username=root;password=;database=vendo;";

                string Query = "update adminsettings set colored='" + n1 + "',grayscale='" + n2 + "',scan='" + n3 + "',longbondpaper='" + n4 + "',shortbondpaper='" + n5 + "',printerforlong='" + str1
 + "',printerforshort='" + str2 + "',port='" + str3 + "',location='" + str4 + "' where id='1';";

                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;
                MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();
                MessageBox.Show("Data Updated");
                Form1 reff = new Form1();
                reff.Refresh();
                while (MyReader2.Read())
                {
                }
                MyConn2.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AdminSetting_Load(object sender, EventArgs e)
        {
            string[] availablePorts = SerialPort.GetPortNames();
            comboBox1.Items.Add("Select a Serial Port");
            foreach (string port in availablePorts)
            {
                comboBox1.Items.Add(port);
            }       
            comboBox1.SelectedIndex = 0;


        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            DialogResult result = folderDlg.ShowDialog();

            if (result == DialogResult.OK)
            {
                textBox3.Text = folderDlg.SelectedPath;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
          

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void Admin_Click(object sender, EventArgs e)
        {

        }
    }
}
