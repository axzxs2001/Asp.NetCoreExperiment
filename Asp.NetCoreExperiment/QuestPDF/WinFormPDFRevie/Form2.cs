using Ghostscript.NET.Rasterizer;
using Ghostscript.NET;
using QuestPDF.Fluent;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ghostscript.NET.Rasterizer;
using Ghostscript.NET;
using System.Drawing.Imaging;

namespace WinFormPDFRevie
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConvertPdfToImages(Directory.GetCurrentDirectory() + "//test.pdf", Directory.GetCurrentDirectory(), 1000);

        }


        public void ConvertPdfToImages(string inputFile, string outputDirectory, int dpi)
        {
            using (GhostscriptRasterizer rasterizer = new GhostscriptRasterizer())
            {
                var version = GhostscriptVersionInfo.GetLastInstalledVersion();
                {
                    rasterizer.Open(inputFile, version, false);

                    for (int pageNumber = 1; pageNumber <= rasterizer.PageCount; pageNumber++)
                    {
                        string pageFilePath = Path.Combine(outputDirectory, $"page{pageNumber}.png");

                        Image img = rasterizer.GetPage(dpi, pageNumber);
                        img.Save(pageFilePath, ImageFormat.Png);
                    }
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}
