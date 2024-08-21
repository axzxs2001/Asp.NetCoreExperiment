
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
                AllowRotateDetection = true, /* ����ʶ���нǶȵ����� */
                Enable180Classification = false, /* ����ʶ����ת�Ƕȴ���90�ȵ����� */
            })
            {
                using (Mat src = Cv2.ImRead(Directory.GetCurrentDirectory() + @"/img.png", ImreadModes.Color))
                {
                    PaddleOcrResult result = all.Run(src);
                    MessageBox.Show("ʶ����: " + result.Text);                  
                }
            }
        }
    }
}
