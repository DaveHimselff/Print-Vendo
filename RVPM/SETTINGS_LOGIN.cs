using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace RVPM
{
    public partial class SETTINGS_LOGIN : Form
    {
        public SETTINGS_LOGIN()
        {
            InitializeComponent();
            this.AutoSize = true;

            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=vendo;";
        public void login()
        {

            string query = "SELECT * FROM adminsettings WHERE username='" + Username.Text + "'AND password='" + password.Text + "'";
            MySqlConnection databaseconection = new MySqlConnection(connectionString);
            MySqlCommand commanddatabase = new MySqlCommand(query, databaseconection);
            commanddatabase.CommandTimeout = 60;
            MySqlDataReader reader;
            try
            {
                databaseconection.Open();
                reader = commanddatabase.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        MessageBox.Show("Welcome AdminUser");
                        AdminSetting setting = new AdminSetting();
                        setting.Show();
                        this.Hide();
                    }

                }
                else
                {
                    MessageBox.Show("SOMETHING WENT WRONG!");
                }
                databaseconection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
           


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Username_Click(object sender, EventArgs e)
        {
            Username.Clear();

        }

        private void password_Click(object sender, EventArgs e)
        {
            password.Clear();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            frm1.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e){


          login();
      
        }

        private void SETTINGS_LOGIN_Load(object sender, EventArgs e)
        {

        }
    }
}
