
using Sdcb.PaddleInference;
using Sdcb.PaddleOCR;
using Sdcb.PaddleOCR.Models;
using System.Windows.Forms;
using Sdcb.PaddleOCR.Models.LocalV3;
using Sdcb.PaddleOCR.Models.Details;
using OpenCvSharp;
using Sdcb.PaddleOCR.Models.Local;
using OpenCvSharp.Aruco;

namespace WinFormsDemo18
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            FullOcrModel model = LocalFullModels.EnglishV4;
            using (PaddleOcrAll all = new PaddleOcrAll(model, PaddleDevice.Mkldnn())
            {
                AllowRotateDetection = true, /* 允许识别有角度的文字 */
                Enable180Classification = false, /* 允许识别旋转角度大于90度的文字 */
            })
            {
                using (Mat src = Cv2.ImRead(Directory.GetCurrentDirectory() + @"/img.png", ImreadModes.Color))
                {
                    PaddleOcrResult result = all.Run(src);
                    MessageBox.Show("识别结果: " + result.Text);                  
                }
            }
        }
    }
}
