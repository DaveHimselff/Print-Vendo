using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WIA;
using System.IO;
using MySql.Data.MySqlClient;
using NUnit.Framework;

namespace RVPM
{
    public partial class copy1 : Form
    {
        string printerforlong;
        string printerforshort;
        string printeruse;
        double pcolored;
        double psize;
        double coloredrate;
        double ncrate;
        double longrate;
        double shortrate;
        int PageNumber;
        double topay;
        double topaypass;
        int copies = 1;
        bool iscolored;
        bool islong;
        Boolean printcolored;
        public copy1()
        {
            InitializeComponent();
        }

        public static Image Logo2 = null;

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
                    coloredrate = 0.50;
                    ncrate = 0.30;
                    longrate = 1;
                    shortrate = 1;
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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

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
          
            copy2 copy2 = new copy2();
            Logo2 = pictureBox4.Image;
            copy2.passprinter = printeruse;
            copy2.passingvaluecolor = iscolored;
            copy2.passingvaluesize = islong;
            copy2.passingvalue = copies * psize;
            copy2.passingvaluecolored = printcolored;
            copy2.passingvaluecopies = copies;
            copy2.passingvaluepage = PageNumber;

            copy2.Show();
            this.Hide();
            }
            else
            {
                MessageBox.Show("Please Select Output Color.");
            }
        }

        private void copy1_Load(object sender, EventArgs e)
        {
            iscolored = false;
            islong = false;
            pcolored = coloredrate;
            ListScanners();
            // Set start output folder TMP
        
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
            }
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
          

            ImageFile image = new ImageFile();
            string imageExtension = "";

         
            image = device.ScanPNG();
            imageExtension = ".png";
        


            // Save the image
            var path = Path.Combine(textBox1.Text + imageExtension);
            
            if (File.Exists(path))
            {
            
               File.Delete(path);
                     
               
            }

            image.SaveFile(path);
          
            pictureBox4.Image = new Bitmap(path);
          

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
          
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            fetch();
            copies = (int)numericUpDown1.Value;
            topaypass = copies * psize;
            label5.Text = topaypass.ToString();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            fetch();
            islong = true;
            psize = longrate;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            fetch();
            islong = false;
            psize = shortrate;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
          
           
        }
    }
}
