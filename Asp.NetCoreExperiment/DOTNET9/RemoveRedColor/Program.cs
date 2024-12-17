using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using System.Xml.Linq;
using SkiaSharp;

string inputPath = @"C:\\myfile\a.png"; // 输入图像路径
string outputPath = "output.png"; // 输出图像路径


// 加载图像
using (Bitmap bitmap = new Bitmap(inputPath))
{
    // 遍历像素
    for (int y = 0; y < bitmap.Height; y++)
    {
        for (int x = 0; x < bitmap.Width; x++)
        {
            Color originalColor = bitmap.GetPixel(x, y);

            // 判断是否是黑、白或灰色
            if (IsBlackWhiteOrGray(originalColor))
            {
                // 保留黑白或灰色
                continue;
            }
            else
            {
                // 替换为白色或透明
                bitmap.SetPixel(x, y, Color.White); // 替换为白色
            }
        }
    }

    // 保存处理后的图像
    bitmap.Save(outputPath, System.Drawing.Imaging.ImageFormat.Png);
}
static bool IsBlackWhiteOrGray(Color color)
{
    // 判断灰色（R≈G≈B）
    int tolerance = 10; // 允许的偏差范围
    return Math.Abs(color.R - color.G) < tolerance &&
           Math.Abs(color.R - color.B) < tolerance;
}
//// 加载图像
//using (SKBitmap bitmap = SKBitmap.Decode(inputPath))
//{
//    // 遍历像素
//    for (int y = 0; y < bitmap.Height; y++)
//    {
//        for (int x = 0; x < bitmap.Width; x++)
//        {
//            SKColor originalColor = bitmap.GetPixel(x, y);

//            // 设置红色分量为0
//            SKColor newColor = new SKColor(0, originalColor.Green, originalColor.Blue, originalColor.Alpha);
//            bitmap.SetPixel(x, y, newColor);
//        }
//    }

//    // 保存处理后的图像
//    using (SKImage image = SKImage.FromBitmap(bitmap))
//    using (SKData data = image.Encode(SKEncodedImageFormat.Jpeg, 100))
//    {
//        System.IO.File.WriteAllBytes(outputPath, data.ToArray());
//    }
//}