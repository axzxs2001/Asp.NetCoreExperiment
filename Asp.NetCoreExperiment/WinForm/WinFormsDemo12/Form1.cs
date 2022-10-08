using QRCoder;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace WinFormsDemo12
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        void Draw(Graphics graphics)
        {
            var y = 15;
            using var logo = new Bitmap(Directory.GetCurrentDirectory() + "/aeon.png");
            graphics.DrawImage(MakeGrayscale(logo), 60, y, 200, 80);
            graphics.DrawLine(new Pen(Color.Black, 2), 10, y += 80, 310, y);
            graphics.DrawLine(new Pen(Color.Black, 2), 10, y += 4, 310, y);
            var font = new Font("黑体", 10);
            var brush = new SolidBrush(Color.Black);
            graphics.DrawString("分店：012", font, brush, 10, y += 1);
            graphics.DrawString("店员：张三", font, brush, 160, y);
            graphics.DrawString($"时间：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}", font, brush, 10, y += 20);
            var no = "000000000001";
            graphics.DrawString($"流水号：{no}", font, brush, 10, y += 20);
            graphics.DrawLine(new Pen(Color.Black, 2), 10, y += 25, 310, y);
            graphics.DrawString("名称       数量  单价  金额", font, brush, 10, y += 5);
            graphics.DrawString("西红柿     500g  26.00 15.00", font, brush, 10, y += 20);
            graphics.DrawString("西葫芦    1000g  23.00 23.00", font, brush, 10, y += 20);
            graphics.DrawString("茄子       500g  50.00 25.00", font, brush, 10, y += 20);
            graphics.DrawString("豆角       500g  38.00 19.00", font, brush, 10, y += 20);
            graphics.DrawLine(new Pen(Color.Black, 2), 10, y += 20, 310, y);
            graphics.DrawLine(new Pen(Color.Black, 2), 10, y += 4, 310, y);
            var sumfont = new Font("黑体", 12);
            graphics.DrawString("              小计：82", sumfont, brush, 10, y += 5);

            var qrCodeAsBitmapByteArr = PngByteQRCodeHelper.GetQRCode(no, QRCodeGenerator.ECCLevel.Q, 20, false);
            using var qrcode = Image.FromStream(new MemoryStream(qrCodeAsBitmapByteArr));
            graphics.DrawImage(qrcode, 100, y += 50, 120, 120);
        }

        public static Bitmap MakeGrayscale(Bitmap original)
        {
            var newBitmap = new Bitmap(original.Width, original.Height);
            var g = Graphics.FromImage(newBitmap);
            var colorMatrix = new System.Drawing.Imaging.ColorMatrix(
               new float[][]
              {
                  new float[] {.3f, .3f, .3f, 0, 0},
                  new float[] {.59f, .59f, .59f, 0, 0},
                  new float[] {.11f, .11f, .11f, 0, 0},
                  new float[] {0, 0, 0, 1, 0},
                  new float[] {0, 0, 0, 0, 1}
              });
            var attributes = new System.Drawing.Imaging.ImageAttributes();
            attributes.SetColorMatrix(colorMatrix);
            g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height), 0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
            g.Dispose();
            return newBitmap;
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            if (e.Graphics != null)
            {
                Draw(e.Graphics);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var graphics = this.CreateGraphics();
            Draw(graphics);
        }
    }
}