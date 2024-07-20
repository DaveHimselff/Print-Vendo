using System;
using System.Collections.Generic;
using System.Drawing;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Windows.Forms;

namespace RVPM
{
    public partial class readpdf : Form
    {
        public readpdf()
        {
            InitializeComponent();
        }

        private void readpdf_Load(object sender, EventArgs e)
        {
            PdfReader reader = new PdfReader("C:/Users/GOD IS LOVE/Desktop/SAMPLE.pdf");

            int coloredPageCount = 0;

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
            }

            label1.Text = coloredPageCount.ToString();
        }
    }

    // Custom render listener to capture text chunks and check for color
    class RenderListener : ITextExtractionStrategy
    {
        public bool HasColoredText { get; private set; }

        public void BeginTextBlock() { }
        public void EndTextBlock() { }
        public void RenderImage(ImageRenderInfo renderInfo) { }

        // Check text color and set flag if colored text is found
        public void RenderText(TextRenderInfo renderInfo)
        {
            // Get the color of the text chunk
            iTextSharp.text.BaseColor chunkColor = renderInfo.GetFillColor();

            // Check if the color is not grayscale
            if (chunkColor.R != chunkColor.G || chunkColor.G != chunkColor.B)
            {
                HasColoredText = true;
            }
        }

        // Get the resulting text
        public string GetResultantText()
        {
            return null; // We don't need to return the text
        }
    }
}
