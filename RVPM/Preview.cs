using System;
using System;
//using Microsoft.Office.Interop.Word;
using System.Windows.Forms;
using Spire.Pdf;
using Spire.Doc;

using System.Windows.Forms;

namespace RVPM
{
    
    public partial class Preview : Form { 
    public string filaNamepass;

    public string fileExtenion;
    public string passingvaluefilename
    {

        get { return filaNamepass; }
        set { filaNamepass = value; }
    }
   
    public string passingvaluefilextention
    {

        get { return fileExtenion; }
        set { fileExtenion = value; }
    }
    
        public Preview()
        {
            InitializeComponent();
        }

        private void printPreviewControl1_Click(object sender, EventArgs e)
        {
         

        }

        private void Preview_Load(object sender, EventArgs e)
        {
            label1.Text = filaNamepass;

            // Construct the full file path
            string filePath = $"D:/{filaNamepass}";

            // Check if the file exists
            if (!System.IO.File.Exists(filePath))
            {
                MessageBox.Show("The specified file doesn't exist.", "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Load and display PDF or Word document based on the file extension
            if (fileExtenion == ".pdf")
            {
                LoadPdfDocument(filePath);
            }
            else if (fileExtenion == ".doc" || fileExtenion == ".docx")
            {
                LoadWordDocument(filePath);
            }
            else
            {
                MessageBox.Show("Unsupported file format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPdfDocument(string filePath)
        {
            PdfDocument pdf = new PdfDocument();
            try
            {
                // Load the PDF document
                pdf.LoadFromFile(filePath);

                // Set the Columns property to 1
                this.printPreviewControl1.Columns = 2;
                this.printPreviewControl1.Rows = 3;

                // Adjust the height of the control to fit multiple pages
                int totalHeight = 0;
                foreach (PdfPageBase page in pdf.Pages)
                {
                    totalHeight += (int)(page.Size.Height * printPreviewControl1.Zoom);
                }
                printPreviewControl1.ClientSize = new System.Drawing.Size(printPreviewControl1.ClientSize.Width, totalHeight);

                // Preview the pdf file
                pdf.Preview(printPreviewControl1);

                // Set zoom mode to fit the entire page
                foreach (Control c in printPreviewControl1.Controls)
                {
                    if (c is ToolStrip)
                    {
                        ToolStrip ts = (ToolStrip)c;
                        foreach (ToolStripItem item in ts.Items)
                        {
                            if (item is ToolStripButton && item.ToolTipText == "Zoom To Whole Page")
                            {
                                item.PerformClick();
                                break;
                            }
                        }
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading the PDF file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadWordDocument(string filePath)
        {
            try
            {
                // Load the Word document
                Document doc = new Document();
                doc.LoadFromFile(filePath);

                // Convert Word document to PDF for previewing
                PdfDocument pdf = new PdfDocument();
                using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
                {
                    doc.SaveToStream(stream, Spire.Doc.FileFormat.PDF);
                    stream.Seek(0, System.IO.SeekOrigin.Begin);
                    pdf.LoadFromStream(stream);
                }

                // Set the Columns property to 1
                this.printPreviewControl1.Columns = 2;
                this.printPreviewControl1.Rows = 3;

                // Adjust the height of the control to fit multiple pages
                int totalHeight = 0;
                foreach (PdfPageBase page in pdf.Pages)
                {
                    totalHeight += (int)(page.Size.Height * printPreviewControl1.Zoom);
                }
                printPreviewControl1.ClientSize = new System.Drawing.Size(printPreviewControl1.ClientSize.Width, totalHeight);

                // Preview the pdf file
                pdf.Preview(printPreviewControl1);

                // Set zoom mode to fit the entire page
                foreach (Control c in printPreviewControl1.Controls)
                {
                    if (c is ToolStrip)
                    {
                        ToolStrip ts = (ToolStrip)c;
                        foreach (ToolStripItem item in ts.Items)
                        {
                            if (item is ToolStripButton && item.ToolTipText == "Zoom To Whole Page")
                            {
                                item.PerformClick();
                                break;
                            }
                        }
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading the Word document: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
