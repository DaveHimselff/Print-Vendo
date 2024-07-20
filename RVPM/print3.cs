using System;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Spire.Doc;


namespace RVPM
{
    public partial class print3 : Form
    {
        string printerforlong;
        string printerforshort;
        string printeruse;
        string portname;
        double coloredrate;
        double ncrate;
        double longrate;
        int pfrom = 1;
        int pto = 1;
        double shortrate;
        int PageNumber;
        double topay;
        double topaypass;
        bool isall;
        int copies = 1;
        bool iscolored;
        bool islong;
        Boolean printcolored;
        Spire.Pdf.PdfDocument pdffile = new Spire.Pdf.PdfDocument();
        Document doc = new Document();
        public string filaNamepass;
        public string fileExtenion;
        public string passingvalue
        {

            get { return filaNamepass; }
            set { filaNamepass = value; }
        }
        public string passingvaluefilename
        {

            get { return filaNamepass; }
            set { filaNamepass = value; }
        }

        public string passingvalue1
        {

            get { return fileExtenion; }
            set { fileExtenion = value; }


        }

        public string backfilename
        {

            get { return filaNamepass; }
            set { filaNamepass = value; }
        }
        public string backfileExtenion
        {

            get { return fileExtenion; }
            set { fileExtenion = value; }
        }

        public print3()
        {
            InitializeComponent();
            this.AutoSize = true;

            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            fetch();
            label8.Text = coloredrate.ToString() + ".00";
            label10.Text = ncrate.ToString() + ".00";
            this.AutoSize = true;

            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
        }
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
                    // scanrate = myReader.GetInt32("scan");
                    shortrate = myReader.GetInt32("shortbondpaper");
                    portname = myReader["port"].ToString();
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
            Files prnt2 = new Files();
            prnt2.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            print4 prt4 = new print4();

            if (radioButton6.Checked)
            {
                prt4.passingvalue = topaypass;
            }
            else if (radioButton3.Checked)
            {
                int numpages = (pto - pfrom) + 1;
                double totalpage = (numpages * ncrate) * copies;
                prt4.passingvalue = totalpage;
            }

            else
            {
                int numpages = (pto - pfrom) + 1;
                double totalpage = (numpages * coloredrate) * copies;
                prt4.passingvalue = totalpage;
            }

            if (radioButton3.Checked || radioButton4.Checked)
            {




                prt4.pages = isall;
                prt4.passprinter = printeruse;
                prt4.passfrom = pfrom;
                prt4.passto = pto;
                prt4.passingvaluecolor = iscolored;
                prt4.passingvaluesize = islong;

                prt4.passingvaluecolored = printcolored;
                prt4.passingvaluecopies = copies;
                prt4.passingvaluepage = PageNumber;
                prt4.passingvaluefilename = filaNamepass;
                prt4.passingvaluefilextention = fileExtenion;

                prt4.Show();
                this.Visible = false;
            }
            else
            {
                MessageBox.Show("Please Select Output Color.");
            }
        }

        private void print3_Load(object sender, EventArgs e)
        {
            fetch();
            printeruse = printerforshort;
            if (fileExtenion == (".pdf"))
            {
                pictureBox3.Visible = true;
                //pdffile.LoadFromFile("D:/" + filaNamepass);
                //document.DefaultPageSettings.Color = false;
                //PageNumber = pdffile.Pages.Count;
                // label4.Text = PageNumber.ToString();
                PdfReader reader = new PdfReader("D:/" + filaNamepass);

                int coloredPageCount = 0;
                int nocolored = 0;
                for (int page = 1; page <= reader.NumberOfPages; page++)
                {
                    // Create a custom render listener
                    RenderListener listener = new RenderListener();

                    // Parse the text while rendering the PDF page
                    string text = PdfTextExtractor.GetTextFromPage(reader, page, listener);

                    // Check if the page has any colored text
                    if (listener.HasColoredText)
                    {
                        coloredPageCount++;
                    }
                    else
                    {
                        nocolored++;
                    }
                }

                label11.Text = coloredPageCount.ToString();
                int totalpage = coloredPageCount + nocolored;
                label4.Text = totalpage.ToString();
                PageNumber = totalpage;
            }
            else if (fileExtenion == (".doc") || fileExtenion == (".docx"))
            {
                pictureBox2.Visible = true;
                doc.LoadFromFile("D:/" + filaNamepass);
                PrintDialog dialog = new PrintDialog();
                dialog.AllowPrintToFile = true;
                PageNumber = doc.PageCount;
                label4.Text = PageNumber.ToString();
            }
            else
            {
                Files fle = new Files();
                fle.Show();
                this.Hide();
            }
            label3.Text = "D:/" + filaNamepass;
            ////READ COLORED








        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            fetch();

            double Topay = (PageNumber * ncrate);
            topay = Topay;
            printcolored = false;
            if (radioButton1.Checked)
            {
                topaypass = topay * shortrate;

            }
            else
            {

                topaypass = topay * longrate;
            }

            return;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            topaypass = topay * shortrate;
            printeruse = printerforshort;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            printeruse = printerforlong;
            topaypass = topay * longrate;
            if (radioButton2.Checked)
            {

                islong = true;
            }
            else
            {
                islong = false;

            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            copies = (int)numericUpDown1.Value;
            topaypass = topay * copies;
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            isall = true;
            label5.Visible = false;
            label6.Visible = false;
            numericUpDown2.Visible = false;
            numericUpDown3.Visible = false;

        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            isall = false;
            label5.Visible = true;
            label6.Visible = true;
            numericUpDown2.Visible = true;
            numericUpDown3.Visible = true;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            pfrom = (int)numericUpDown2.Value;
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            pto = (int)numericUpDown3.Value;
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Preview prt4 = new Preview();


            prt4.passingvaluefilename = filaNamepass;
            prt4.passingvaluefilextention = fileExtenion;
            prt4.Show();
        }

        private void a4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {

        }
    }

}
