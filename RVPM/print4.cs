using System;
using System.Windows.Forms;
using System.Drawing.Printing;
using MySql.Data.MySqlClient;
using Spire.Doc;
using Spire.Pdf;
using System.Threading;
using System.IO.Ports; // Add this for serial port communication

namespace RVPM
{
    public partial class print4 : Form
    {
        public delegate void d1(string indata);
        private static int pulseCount;
        private SerialPort serialPort1; // Ensure this is private

        double topaypass;
        public bool printcolored;
        public bool islong;
        public bool isall;
        public bool iscolored;
        public string allpages;
        public string portname;
        public int copies;
        public int PageNumber;
        public int pfrom;
        public int pto;
        public string filaNamepass;
        public string printeruse;
        public string fileExtenion;
        Document doc;
        PrintDocument printDoc;
        PdfDocument pdffile = new PdfDocument();

        public print4()
        {
            InitializeComponent();
            this.AutoSize = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
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
                    portname = myReader["port"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            myConn.Close();

            serialPort1 = new SerialPort(portname, 9600); // Initialize serial port
            serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);
            serialPort1.Open();
        }

        public double passingvalue
        {
            get { return topaypass; }
            set { topaypass = value; }
        }

        public int passfrom
        {
            get { return pfrom; }
            set { pfrom = value; }
        }

        public int passto
        {
            get { return pto; }
            set { pto = value; }
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

        public bool pages
        {
            get { return isall; }
            set { isall = value; }
        }

        public int passingvaluecopies
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

        public string passingvaluefilextention
        {
            get { return fileExtenion; }
            set { fileExtenion = value; }
        }

        public int passingvaluepage
        {
            get { return PageNumber; }
            set { PageNumber = value; }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
            Files prt3 = new Files();
            prt3.Visible = true;
            this.Hide();
        }

        private void print4_Load(object sender, EventArgs e)
        {
            label9.Text = topaypass.ToString() + ".00";
            label13.Text = filaNamepass;
            label15.Text = PageNumber.ToString();
            label18.Text = copies.ToString();
            label19.Text = printeruse;
            label16.Text = islong ? "LANDSCAPE" : "PORTRAIT";
            allpages = isall ? "all" : "notall";
            label17.Text = iscolored ? "Colored" : "Grayscale";
            pictureBox2.Visible = fileExtenion == ".pdf";
            pictureBox5.Visible = fileExtenion == ".doc" || fileExtenion == ".docx";
            pictureBox6.Visible = !(fileExtenion == ".pdf" || fileExtenion == ".doc" || fileExtenion == ".docx");
        }

        private void pay_Click(object sender, EventArgs e)
        {
            // Payment logic here
        }

        private void print_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
            if (fileExtenion == ".pdf")
            {
                for (int x = 0; x < copies; x++)
                {
                    pdffile = new PdfDocument();
                    pdffile.PrintSettings.PrinterName = printeruse;
                    pdffile.LoadFromFile("D:/" + filaNamepass);
                    pdffile.PrintSettings.Color = printcolored;
                    pdffile.Print();
                }
            }
            else
            {
                for (int x = 0; x < copies; x++)
                {
                    doc = new Document();
                    doc.LoadFromFile("D:/" + filaNamepass);
                    PrintDialog dialog = new PrintDialog();
                    dialog.AllowPrintToFile = true;
                    doc.PrintDialog = dialog;
                    printDoc = doc.PrintDocument;
                    printDoc.PrinterSettings.PrinterName = printeruse;
                    printDoc.PrinterSettings.FromPage = pfrom;
                    printDoc.PrinterSettings.ToPage = pto;
                    printDoc.DefaultPageSettings.Color = printcolored;
                    printDoc.Print();
                }
            }

            Thread.Sleep(5000); // Wait for 5 seconds
            Form1 frm1 = new Form1();
            frm1.Show();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e) { }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string indata = serialPort1.ReadLine().Trim();
            if (indata.StartsWith("p"))
            {
                int coinValue;
                if (int.TryParse(indata.Substring(1), out coinValue))
                {
                    UpdateCoinCount(coinValue);
                }
            }
        }

        private void UpdateCoinCount(int coinValue)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int>(UpdateCoinCount), coinValue);
            }
            else
            {
                pulseCount += coinValue;
                label10.Text = pulseCount.ToString();
                CheckIfPaymentComplete();
            }
        }

        private void CheckIfPaymentComplete()
        {
            if (label10.Text + ".00" == label9.Text)
            {
                // Printing logic here
                if (fileExtenion == ".pdf")
                {
                    for (int x = 0; x < copies; x++)
                    {
                        pdffile = new PdfDocument();
                        pdffile.PrintSettings.PrinterName = printeruse;
                        pdffile.LoadFromFile("D:/" + filaNamepass);
                        pdffile.PrintSettings.Color = printcolored;
                        pdffile.Print();
                    }
                }
                else
                {
                    for (int x = 0; x < copies; x++)
                    {
                        doc = new Document();
                        doc.LoadFromFile("D:/" + filaNamepass);
                        PrintDialog dialog = new PrintDialog();
                        dialog.AllowPrintToFile = true;
                        doc.PrintDialog = dialog;
                        printDoc = doc.PrintDocument;
                        printDoc.PrinterSettings.PrinterName = printeruse;
                        printDoc.PrinterSettings.FromPage = pfrom;
                        printDoc.PrinterSettings.ToPage = pto;
                        printDoc.DefaultPageSettings.Color = printcolored;
                        printDoc.Print();
                    }
                }

                Form1 frm1 = new Form1();
                frm1.Show();
                this.Hide();
            }
        }


        private void label10_Click(object sender, EventArgs e) { }

        private void label11_Click(object sender, EventArgs e) { }

        private void label16_Click(object sender, EventArgs e) { }
    }
}