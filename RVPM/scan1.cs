using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WIA;
using System.IO;
using MySql.Data.MySqlClient;

namespace RVPM
{
    public partial class scan1 : Form
    {
        string printerforlong;
        string printerforshort;
        string printeruse;
        double coloredrate;
        double ncrate;
        double longrate;
        double pcolored;
        double psize;
        double scanrate;
        double shortrate;
        double PageNumber;
        double topay;
        double topaypass;
        double copies = 1.0;
        bool iscolored;
        bool islong;
        Boolean printcolored;
        public scan1()
        {
            InitializeComponent();
            this.AutoSize = true;

            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            listBox1.SelectedIndex = 0;
           // }
            fetch();
                label5.Text = coloredrate.ToString() + ".00";
            label6.Text = ncrate.ToString() + ".00";
        }
        public static Image Logo = null;

        public void fetch()
        {
            string myConnection = "datasource=127.0.0.1;port=3306;username=root;password=;database=vendo;";
            MySqlConnection myConn = new MySqlConnection(myConnection);
            MySqlCommand command = myConn.CreateCommand();
            command.CommandText = "Select * FROM adminsettings WHERE id=1";
            MySqlDataReader myReader;

            try
            {
                myConn.Open();
                myReader = command.ExecuteReader();

                while (myReader.Read())
                {
                    coloredrate = myReader.GetInt32("colored");
                    ncrate = myReader.GetInt32("grayscale");
                    longrate = myReader.GetInt32("longbondpaper");
                    scanrate = myReader.GetInt32("scan");
                    shortrate = myReader.GetInt32("shortbondpaper");
                    printerforshort = myReader["printerforshort"].ToString();
                    printerforlong = myReader["printerforlong"].ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            myConn.Close();

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            frm1.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked || radioButton2.Checked)
            {
                fetch();
                topaypass = copies * psize * scanrate;
                scan2 scan = new scan2();
                Logo = pictureBox4.Image;
                scan.passprinter = printeruse;
                scan.passingvaluecolor = iscolored;
                scan.passingvaluesize = islong;
                scan.passingvalue = topaypass;
                scan.passingvaluecolored = printcolored;
                scan.passingvaluecopies = copies;
                scan.passingvaluepage = PageNumber;
             

                scan.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Please Select paper size.");
            }
          
            
        }

        private void scan1_Load(object sender, EventArgs e)
        {
          
            ListScanners();
            // Set start output folder TMP
            textBox1.Text = Path.GetTempPath();
            // Set JPEG as default
            comboBox1.SelectedIndex = 1;

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
        private void ListScanners()
        {
            // Clear the ListBox.
            listBox1.Items.Clear();

            // Create a DeviceManager instance
            var deviceManager = new DeviceManager();

            // Loop through the list of devices and add the name to the listbox
            for (int i = 1; i <= deviceManager.DeviceInfos.Count; i++)
            {
                // Add the device only if it's a scanner
                if (deviceManager.DeviceInfos[i].Type != WiaDeviceType.ScannerDeviceType)
                {
                    continue;
                }

                // Add the Scanner device to the listbox (the entire DeviceInfos object)
                // Important: we store an object of type scanner (which ToString method returns the name of the scanner)
                listBox1.Items.Add(
                    new Scanner(deviceManager.DeviceInfos[i])
                );
                listBox1.SelectedIndex = 0;
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            copies = (double)numericUpDown1.Value;
           
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            fetch();
            psize = longrate;
            printeruse = printerforlong;
           
            if (radioButton2.Checked)
            {

                islong = true;
            }
            else
            {
                islong = false;

            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            fetch();
            psize = shortrate;
            printeruse = printerforshort;
          
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            fetch();

        

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(StartScanning).ContinueWith(result => TriggerScan());
        }
        private void TriggerScan()
        {
            Console.WriteLine("Image succesfully scanned");
        }
        public void StartScanning()
        {
            Scanner device = null;

            this.Invoke(new MethodInvoker(delegate ()
            {
                device = listBox1.SelectedItem as Scanner;
            }));

            if (device == null)
            {
                MessageBox.Show("You need to select first an scanner device from the list",
                                "Warning",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (String.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Provide a filename",
                                "Warning",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ImageFile image = new ImageFile();
            string imageExtension = "";

            this.Invoke(new MethodInvoker(delegate ()
            {
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        image = device.ScanPNG();
                        imageExtension = ".png";
                        break;
                    case 1:
                        image = device.ScanJPEG();
                        imageExtension = ".jpeg";
                        break;
                    case 2:
                        image = device.ScanTIFF();
                        imageExtension = ".tiff";
                        break;
                }
            }));


            // Save the image
            var path = Path.Combine(textBox1.Text, textBox2.Text + imageExtension);

            if (File.Exists(path))      
            {
                File.Delete(path);
            }

            image.SaveFile(path);

            pictureBox4.Image = new Bitmap(path);
        }

        private void button2_Click(object sender, EventArgs e)
        {
              FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            DialogResult result = folderDlg.ShowDialog();

            if (result == DialogResult.OK)
            {
                textBox1.Text = folderDlg.SelectedPath;
            }


          

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //PrintDialog pd = new PrintDialog();

            //pd.PrinterSettings = new PrinterSettings();

            // if (DialogResult.OK == pd.ShowDialog(this))

            //     {
            for (int x = 0; x < (int)numericUpDown1.Value; x++)
            {
                PrintDocument pdoc = new PrintDocument();
                pdoc.PrinterSettings.PrinterName = printeruse;
                pdoc.PrintPage += new PrintPageEventHandler(pqr);
           
                pdoc.Print();
            }

            }
            void pqr(object o, PrintPageEventArgs ex)

            {

                System.Drawing.Image i = this.pictureBox4.Image;

                Point p = new Point(10, 10);

                ex.Graphics.DrawImage(i, p);

            }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton4_CheckedChanged_1(object sender, EventArgs e)
        {
            fetch();

            double Topay = (PageNumber * coloredrate);
            topay = Topay;

            printcolored = true;
            if (radioButton1.Checked)
            {
                topaypass = topay * shortrate;

            }
            else
            {
                topaypass = topay * longrate;
            }
            if (radioButton4.Checked)
            {
                iscolored = true;

            }
            else
            {
                iscolored = false;

            }
            return;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           // listBox1.Focus();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            frm1.Show();
            this.Hide();
        }
    }
}
