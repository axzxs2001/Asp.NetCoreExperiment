using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.AxHost;

namespace CheckBoxRendererSample
{
    class Form1 : Form
    {
        public Form1()
            : base()
        {
            Switch CheckBox1 = new Switch();
            Controls.Add(CheckBox1);

            if (Application.RenderWithVisualStyles)
                this.Text = "Visual Styles Enabled";
            else
                this.Text = "Visual Styles Disabled";
        }

        [STAThread]
        static void Main()
        {
            // If you do not call EnableVisualStyles below, then 
            // CheckBoxRenderer.DrawCheckBox automatically detects 
            // this and draws the check box without visual styles.
            Application.EnableVisualStyles();
            Application.Run(new Form1());
        }
    }

    public class CustomCheckBox : Control
    {
        private Rectangle textRectangleValue = new Rectangle();
        private Point clickedLocationValue = new Point();
        private bool clicked = false;
        private CheckBoxState state = CheckBoxState.UncheckedNormal;

        public CustomCheckBox()
            : base()
        {
            this.Location = new Point(50, 50);
            this.Size = new Size(100, 20);
            this.Text = "Click here";
            this.Font = SystemFonts.IconTitleFont;
        }

        // Calculate the text bounds, exluding the check box.
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

        // Draw the check box in the current state.
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            CheckBoxRenderer.DrawCheckBox(e.Graphics,
                ClientRectangle.Location, TextRectangle, this.Text,
                this.Font, TextFormatFlags.HorizontalCenter,
                clicked, state);
        }

        // Draw the check box in the checked or unchecked state, alternately.
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (!clicked)
            {
                clicked = true;
                this.Text = "Clicked!";
                state = CheckBoxState.CheckedPressed;
                Invalidate();
            }
            else
            {
                clicked = false;
                this.Text = "Click here";
                state = CheckBoxState.UncheckedNormal;
                Invalidate();
            }
        }

        // Draw the check box in the hot state. 
        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);
            state = clicked ? CheckBoxState.CheckedHot :
                CheckBoxState.UncheckedHot;
            Invalidate();
        }

        // Draw the check box in the hot state. 
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            this.OnMouseHover(e);
        }

        // Draw the check box in the unpressed state.
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            state = clicked ? CheckBoxState.CheckedNormal :
                CheckBoxState.UncheckedNormal;
            Invalidate();
        }
    }


    public class Switch : Control
    {
        private Rectangle textRectangleValue = new Rectangle();

        private CheckBoxState state = CheckBoxState.UncheckedNormal;

        public Switch()
            : base()
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

        // Draw the check box in the current state.
        protected override void OnPaint(PaintEventArgs e)
        {
            DrawSwitch(e.Graphics);
            base.OnPaint(e);
            //BackgroundImage=DrawSwitch();
            //BackgroundImageLayout = ImageLayout.None;
        }

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