using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace WinFormDemo02
{
    public class Switch : Control
    {
        private Rectangle textRectangleValue = new Rectangle();

        private CheckBoxState state = CheckBoxState.UncheckedNormal;

        public Switch(): base()
        {
            this.Location = new Point(50, 50);
            this.Size = new Size(50, 25);
            this.Font = SystemFonts.IconTitleFont;
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                        ControlStyles.ResizeRedraw |
                        ControlStyles.AllPaintingInWmPaint, true);
        }
        public bool Checked
        {
            get; set;
        } = false;
        Bitmap DrawSwitch()
        {
            var x = 0;
            var y = 0;
            var width = 25;
            var bmp = new Bitmap(width * 2 - 2, width);
            var g = Graphics.FromImage(bmp);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.InterpolationMode = InterpolationMode.High;
            g.CompositingQuality = CompositingQuality.HighQuality;
            var brush = new SolidBrush(Color.LightSeaGreen);
            g.FillPie(brush, new Rectangle(x, y, width, width), 90, 180);

            g.FillRectangle(brush, new Rectangle(x + width / 2 - 1, y, width, width));

            g.FillPie(brush, new Rectangle(x + width - 2, y, width, width), -90, 180);

            if (Checked)
            {
                var selectBrush = new SolidBrush(Color.White);
                g.FillEllipse(selectBrush, new Rectangle(x + 2, y + 2, width - 4, width - 4));
            }
            else
            {
                var selectBrush = new SolidBrush(Color.White);
                g.FillEllipse(selectBrush, new Rectangle(x + width, y + 2, width - 4, width - 4));
            }
            return bmp;

        }
        void DrawSwitch(Graphics g)
        {
            var x = 0;
            var y = 0;
            var width = 25;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.InterpolationMode = InterpolationMode.High;
            g.CompositingQuality = CompositingQuality.HighQuality;
            SolidBrush brush;
            if (Checked)
            {
                brush = new SolidBrush(Color.MediumSeaGreen);
            }
            else
            {
                brush = new SolidBrush(Color.DarkRed);

            }
            g.FillPie(brush, new Rectangle(x, y, width, width), 90, 180);

            g.FillRectangle(brush, new Rectangle(x + width / 2 - 1, y, width, width));

            g.FillPie(brush, new Rectangle(x + width - 2, y, width, width), -90, 180);

            if (Checked)
            {
                var selectBrush = new SolidBrush(Color.White);
                g.FillEllipse(selectBrush, new Rectangle(x + 2, y + 2, width - 4, width - 4));
            }
            else
            {
                var selectBrush = new SolidBrush(Color.White);
                g.FillEllipse(selectBrush, new Rectangle(x + width, y + 2, width - 4, width - 4));
            }

        }

        public Rectangle TextRectangle
        {
            get
            {
                using (Graphics g = this.CreateGraphics())
                {
                    textRectangleValue.X = ClientRectangle.X +
                        CheckBoxRenderer.GetGlyphSize(g,
                        CheckBoxState.UncheckedNormal).Width;
                    textRectangleValue.Y = ClientRectangle.Y;
                    textRectangleValue.Width = ClientRectangle.Width -
                        CheckBoxRenderer.GetGlyphSize(g,
                        CheckBoxState.UncheckedNormal).Width;
                    textRectangleValue.Height = ClientRectangle.Height;
                }

                return textRectangleValue;
            }
        }
     
        protected override void OnPaint(PaintEventArgs e)
        {
            DrawSwitch(e.Graphics);
            base.OnPaint(e);
            //BackgroundImage=DrawSwitch();
            //BackgroundImageLayout = ImageLayout.None;
        }
        public event EventHandler CheckedChanged;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (!Checked)
            {
                Checked = true;
                state = CheckBoxState.CheckedPressed;
                Invalidate();
            }
            else
            {
                Checked = false;
                state = CheckBoxState.UncheckedNormal;
                Invalidate();
            }
            CheckedChanged(this,new EventArgs());
        }
        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);
            state = Checked ? CheckBoxState.CheckedHot :
                CheckBoxState.UncheckedHot;
            Invalidate();
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            this.OnMouseHover(e);
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            state = Checked ? CheckBoxState.CheckedNormal :
                CheckBoxState.UncheckedNormal;
            Invalidate();
        }
    }
}
