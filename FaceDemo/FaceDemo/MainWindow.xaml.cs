using Microsoft.Win32;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SkiaSharp.Views.Desktop;
using ViewFaceCore;
using ViewFaceCore.Core;
using System.Drawing.Imaging;
using System.Drawing;
using System.Windows.Media.Media3D;

namespace FaceDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public static BitmapImage ToBitmapImage(Bitmap bitmap)
        {
            using var ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Png);
            ms.Position = 0;
            var bi = new BitmapImage();
            bi.BeginInit();
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.StreamSource = ms;
            bi.EndInit();
            bi.Freeze();
            return bi;
        } 

        private async void OnClickPickPhotoButton(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "图片|*.bmp;*.jpg;*.jpeg;*.webp;*.gif;*.png|全部|*.*";
            dialog.Title = "打开照片";
            if (dialog.ShowDialog() == true)
            {
                // var image = new BitmapImage(new Uri(dialog.FileName));
                
                using var bitmap = SKBitmap.Decode(dialog.FileName);
                
                using var faceDetector = new FaceDetector(); // 框选定位人脸
                using var faceMark = new FaceLandmarker(); // 五官定位
                using var faceAntiSpoofing = new FaceAntiSpoofing(); // 防伪
                using var faceRecognizer = new FaceRecognizer(); // 特征提取比对
                if (bitmap != null && faceDetector != null)
                {
                    var infos = await faceDetector.DetectAsync(bitmap);
                    var faceImage = bitmap.ToFaceImage();
                    Debug.WriteLine($"识别到的人脸数量：{infos.Length} 个人脸信息：");
                    Debug.WriteLine($"No.\t人脸置信度\t位置信息");

                    CountLabel.Content = $"识别个数：{infos.Length}";

                    Debug.WriteLine($"width: {bitmap.Width} height: {bitmap.Height}");

                    var dpx = (float)(bitmap.Width / RawImage.Width + bitmap.Height / RawImage.Height);

                    using var result = new SKBitmap(bitmap.Width, bitmap.Height);
                    using var canvas = new SKCanvas(result);
                    using var paint = new SKPaint();
                    paint.Color = new SKColor(0, 255, 0);
                    paint.TextSize = dpx * 12;
                    paint.StrokeWidth = dpx; // 图片太大， 这个太小 显示的时候 像素会被缩放没了。
                    paint.Style = SKPaintStyle.Stroke;
                    paint.StrokeCap = SKStrokeCap.Round;

                    canvas.Clear();
                    canvas.DrawBitmap(bitmap, 0, 0);

                    for (int i = 0; i < infos.Length; i++)
                    {
                        var info = infos[i];
                        Debug.WriteLine($"{i}\t{info.Score:f8}\t{info.Location}");
                        var left = info.Location.X;
                        var top = info.Location.Y;
                        var right = info.Location.X + info.Location.Width;
                        var bottom = info.Location.Y + info.Location.Height;

                        // 人脸定位
                        Debug.WriteLine($"left: {left} top: {top} right: {right} bottom: {bottom}");

                        canvas.DrawRect(left, top, info.Location.Width, info.Location.Height, paint);
                        canvas.DrawText($"{info.Score:f4}", left, bottom + dpx * 12, paint);

                        // 活体检查
                        var sw = Stopwatch.StartNew();
                        sw.Start();
                        var markPoints = faceMark.Mark(bitmap, info);

                        foreach (var markPoint in markPoints)
                        {
                            canvas.DrawCircle((float)markPoint.X,(float)markPoint.Y, dpx * 2, paint);
                        }

                        var antiResult = faceAntiSpoofing.AntiSpoofing(bitmap, info, markPoints);
                        sw.Stop();
                        Debug.WriteLine($"活体检测，结果：{antiResult.Status}，清晰度:{antiResult.Clarity}，真实度：{antiResult.Reality}，耗时：{sw.ElapsedMilliseconds}ms");

                        // 提取特征
                        var token = faceRecognizer.Extract(faceImage, markPoints);
                        Debug.WriteLine($"提取特征：长度 {token.Length}");
                    }
                    using var rb = result.ToBitmap();
                    RawImage.Source = ToBitmapImage(rb);
                }
                else
                {
                    Debug.WriteLine($"解码失败：{bitmap}  {faceDetector}");
                }
            }
        }
    }
}
