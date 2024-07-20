using System;
using System.Windows.Forms;
using System.Drawing.Printing;
using MySql.Data.MySqlClient;
using Spire.Doc;
using Spire.Pdf;
using System.Threading;

namespace RVPM
{
    public partial class print4 : Form
    {
        public delegate void d1(string indata);
        private static int pulseCount;

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

            //serialPort1.PortName = portname;
           //  serialPort1.Open();
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
      //  public string passport
      //  {

       //     get { return portname; }
       //     set { portname = value; }
      //  }
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

private void pictureBox1_Click(object sender, EventArgs e)      {
            serialPort1.Close();
            Files prt3 = new Files();
            prt3.Visible= true;
            this.Hide();
            
        }

        private void print4_Load(object sender, EventArgs e)
        {


            label9.Text = topaypass.ToString() + ".00";
            label13.Text = filaNamepass;
            label15.Text = PageNumber.ToString();
            label18.Text = copies.ToString();
            label19.Text = printeruse;
            if (islong.Equals(true))
            {
                label16.Text = "LANDSCAPE";
            }
            else
            {
                label16.Text = "PORTRAIT";
            }
            if (isall.Equals(false))
            {
                allpages = "notall";
            }

            if (iscolored.Equals(true))
            {
                label17.Text = "Colored";
            }
            else
            {
                label17.Text = "Grayscale";
            }
            if (fileExtenion == ".pdf")
            {
                pictureBox2.Visible = true;
            }
            else if (fileExtenion == ".doc" || fileExtenion == ".docx")
            {
                pictureBox5.Visible = true;
            }
            else
            {
                pictureBox6.Visible = true;
            }    
        }

        private void pay_Click(object sender, EventArgs e)
        {

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

                    if (allpages == "notall")
                    {
                        pdffile.PrintSettings.SelectPageRange(pfrom + 1, pto + 1); // Pages are 1-based in Spire.Pdf
                    }

                    pdffile.PrintSettings.Color = printcolored; // Set color based on printcolored variable

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

                    if (allpages == "notall")
                    {
                        printDoc.PrinterSettings.FromPage = pfrom + 1; // Pages are 1-based in Spire.Doc
                        printDoc.PrinterSettings.ToPage = pto + 1;     // Pages are 1-based in Spire.Doc
                    }

                    if (printcolored)
                    {
                        printDoc.DefaultPageSettings.Color = true;
                    }
                    else
                    {
                        if (printDoc.PrinterSettings.SupportsColor)
                        {
                            printDoc.DefaultPageSettings.Color = false;
                        }
                    }

                    printDoc.Print();
                }
            }

            // Add a delay after printing if needed
            int milliseconds = 5000;
            Thread.Sleep(milliseconds);

            // Show Form1 again
            Form1 frm1 = new Form1();
            frm1.Show();
            this.Hide();
        }




        private void label9_Click(object sender, EventArgs e)
        {

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
                    
                    label10.Text = Convert.ToString(pulseCount);
            if ((label10.ToString() + ".00") == label9.ToString())
            {
                // print5 prt5 = new print5();
                if (fileExtenion == ".pdf")
                {
                    //    prt5.Show();
                    for (int x = 0; x < copies; x++)
                    {
                        pdffile = new PdfDocument();
                        pdffile.PrintSettings.PrinterName = printeruse;
                        pdffile.LoadFromFile("D:/" + filaNamepass);
                        if (allpages == "notall")
                        {
                            // pdffile.PrintSettings.FromPage = pfrom;
                            //pdffile.PrintSettings.FromPage = pto;
                        }
                        if (printcolored == true)
                        {


                            pdffile.PrintSettings.Color = printcolored;

                        }
                        else
                        {

                            pdffile.PrintSettings.Color = printcolored;



                        }
                        pdffile.Print();
                      
                    }
                    Form1 frm1 = new Form1();
                    frm1.Show();
                           
                            this.Hide();
                        
                        }
                else
                {
                    // prt5.Show();
                    for (int x = 0; x < copies; x++)
                    {


                        doc = new Document();
                        doc.LoadFromFile("D:/" + filaNamepass);
                        PrintDialog dialog = new PrintDialog();
                        dialog.AllowPrintToFile = true;
                        doc.PrintDialog = dialog;
                        printDoc = doc.PrintDocument;
                        printDoc.PrinterSettings.PrinterName = printeruse;
                        if (allpages == "notall")
                        {
                            printDoc.PrinterSettings.FromPage = pfrom;
                            printDoc.PrinterSettings.FromPage = pto;
                        }
                        if (printcolored == true)
                        {

                            printDoc.DefaultPageSettings.Color = printcolored;

                        }
                        else
                        {
                            if (printDoc.PrinterSettings.SupportsColor)
                            {
                                printDoc.DefaultPageSettings.Color = printcolored;
                            }


                        }
                        printDoc.Print();
                      
                    }
                    Form1 frm1 = new Form1();
                    frm1.Show();
                          
                            this.Hide();
                   
                        }

                //  prt5.Hide();
            }
            break;
                   
                   
            }
        }
        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

    }
}
