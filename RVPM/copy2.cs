using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace RVPM
{
    public partial class copy2 : Form
    {
        public delegate void d1(string indata);
        private static int pulseCount;

        public double topaypass;
        public bool printcolored;
        public bool islong;
        public bool iscolored;
        public double copies;
        public double PageNumber;
        public string filaNamepass;
        public string printeruse;
        public copy2()
        {
            InitializeComponent();
            serialPort1.Open();
        }
        public double passingvalue
        {

            get { return topaypass; }
            set { topaypass = value; }
        }
        public bool passingvaluecolored
        {

            get { return printcolored; }
            set { printcolored = value; }
        }
        public bool passingvaluesize
        {

            get { return islong; }
            set { islong = value; }
        }
        public bool passingvaluecolor
        {

            get { return iscolored; }
            set { iscolored = value; }
        }
        public double passingvaluecopies
        {

            get { return copies; }
            set { copies = value; }

        }

        public string passingvaluefilename
        {

            get { return filaNamepass; }
            set { filaNamepass = value; }
        }
        public string passprinter
        {

            get { return printeruse; }
            set { printeruse = value; }
        }
       
        public double passingvaluepage
        {

            get { return PageNumber; }
            set { PageNumber = value; }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            copy1 copy = new copy1();
            copy.Show();
            this.Hide();
        }

        private void copy2_Load(object sender, EventArgs e)
        {
            
            label13.Text = topaypass.ToString() + ".00";
            pictureBox4.Image = copy1.Logo2;
            label12.Text = copies.ToString();
            if (islong.Equals(true))
            {
                label6.Text = "LANDSCAPE";
            }
            else
            {
                label6.Text = "PORTRAIT";
            }
            if (iscolored.Equals(true))
            {
                label10.Text = "Colored";
            }
            else
            {
                label10.Text = "Grayscale";
            }

        }
        void pqr(object o, PrintPageEventArgs ex)

        {

            System.Drawing.Image i = this.pictureBox4.Image;

            Point p = new Point(10, 10);

            ex.Graphics.DrawImage(i, p);

        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            string indata = serialPort1.ReadLine();
            d1 writeit = new d1(Write2Form);
            Invoke(writeit, indata);
        }
        public void Write2Form(string indata)
        {

            //function that handles data sent from ARDUINO!!!
            char firstchar;

            Single numdata;
            firstchar = indata[0];
            switch (firstchar)
            {
                case 'p':
                    pulseCount++;

                    label9.Text = pulseCount.ToString();
                    if (label13.ToString() == (label9.ToString() + ".00"))
                    {


                        for (int x = 0; x < copies; x++)       
                        {
                            PrintDocument pdoc = new PrintDocument();
                            pdoc.PrinterSettings.PrinterName = printeruse;
                            pdoc.PrintPage += new PrintPageEventHandler(pqr);



                            pdoc.DefaultPageSettings.Color = true;



                            pdoc.Print();
                        }
                       
                        int milliseconds = 5000;

                        Form1 frm1 = new Form1();
                        frm1.Show();
                    
                        this.Hide();
                    }
                    break;

            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
