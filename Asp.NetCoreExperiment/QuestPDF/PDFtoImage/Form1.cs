using Ghostscript.NET;
using Ghostscript.NET.Rasterizer;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;

namespace PDFtoImage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConvertPdfToImages(Directory.GetCurrentDirectory() + "//test.pdf", Directory.GetCurrentDirectory(), 100);
        }


        public void ConvertPdfToImages(string inputFile, string outputDirectory, int dpi)
        {
            using (GhostscriptRasterizer ghostscriptRasterizer = new GhostscriptRasterizer())
            {
                GhostscriptVersionInfo installedVersion = GhostscriptVersionInfo.GetLastInstalledVersion();
                ghostscriptRasterizer.Open(inputFile, installedVersion, false);
                for (int pageNumber = 1; pageNumber <= ghostscriptRasterizer.PageCount; ++pageNumber)
                {
                    string path1 = outputDirectory;
                    DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(8, 1);
                    interpolatedStringHandler.AppendLiteral("page");
                    interpolatedStringHandler.AppendFormatted<int>(pageNumber);
                    interpolatedStringHandler.AppendLiteral(".png");
                    string stringAndClear = interpolatedStringHandler.ToStringAndClear();
                    string filename = Path.Combine(path1, stringAndClear);
                    ghostscriptRasterizer.GetPage(dpi, pageNumber).Save(filename, ImageFormat.Png);
                }
            }
        }
    }
}