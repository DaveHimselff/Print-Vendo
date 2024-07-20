using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WIA;
using System.IO;
using System.Windows.Forms;
using System.Management;
using System.IO.Ports;


namespace RVPM
{
    public partial class print : Form
    {
        private const UInt32 WM_DEVICECHANGE = 0x219;
        private const UInt32 DBT_DEVICEARIVAL = 0x8000;
        private const UInt32 DBT_DEVICEREMOVECOMPLETE = 0x8004;
        List<String> serialPortList = null;
        public print()
        {
            InitializeComponent();
            serialPortList = SerialPort.GetPortNames().ToList();
            comboSerial.DataSource = serialPortList;
            this.AutoSize = true;

            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
          
            string textBoxText = textBox1.Text;
            if (!string.IsNullOrWhiteSpace(textBoxText))
            {
                Files prt = new Files();
                prt.Show();
                this.Close();
            }
        }
        protected override void WndProc(ref Message m)
        {
            
            base.WndProc(ref m);
            if (m.Msg != WM_DEVICECHANGE) return;
            switch((UInt32) m.WParam)
            {
                case DBT_DEVICEARIVAL:
                    List<String> list = SerialPort.GetPortNames().ToList();
                    List<String> new_dev = list.Where(p => !serialPortList.Any(p1 => p1 == p)).ToList();
                    if (new_dev?.Count > 0) MessageBox.Show(new_dev[0] + "Has Been Inserted!");
                    serialPortList = list;
                    comboSerial.DataSource = serialPortList;
                    Files prt = new Files();
                    prt.Show();
                    this.Close();
                    break;
                case DBT_DEVICEREMOVECOMPLETE:
                    list = SerialPort.GetPortNames().ToList();
                    new_dev = list.Where(p => !serialPortList.Any(p1 => p1 == p)).ToList();
                    if (new_dev?.Count > 0) MessageBox.Show(new_dev[0] + "Has Been Removed!");
                    serialPortList = list;
                    comboSerial.DataSource = serialPortList;
                    this.Show();
                    break;
                default: break;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            frm1.Show();
            this.Hide();
        }

        private void print_Load(object sender, EventArgs e)
        {
            textBox1.Text = Path.GetTempPath();
            // Set JPEG as default
          

            DriveInfo[] drives = DriveInfo.GetDrives();

            // Clear the existing text in the textbox
            textBox1.Clear();

            // Iterate through each drive and check if it is removable
            foreach (DriveInfo drive in drives)
            {
                if (drive.DriveType == DriveType.Removable)
                {
                    // Append the removable drive to the textbox
                    textBox1.AppendText(drive.Name + Environment.NewLine);
                }
            }
        }
       
    }
}
