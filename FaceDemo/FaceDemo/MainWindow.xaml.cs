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
using Pen = System.Drawing.Pen;
using System.Windows.Threading;
using ViewFaceCore.Model;

namespace FaceDemo;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private MediaPlayer MediaVideoPlayer = new MediaPlayer();
    private FaceTracker? FaceTrack = null;

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

    // 图片处理
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

    // 视频处理
    private void OnClickVideoButton(object sender, RoutedEventArgs e)
    {
        var dialog = new OpenFileDialog();
        dialog.Filter = "视频|*.mp4|全部|*.*";
        dialog.Title = "打开视频";

        if (dialog.ShowDialog() == true)
        {
            // 截帧
            MediaVideoPlayer.Open(new Uri(dialog.FileName));
            MediaVideoPlayer.ScrubbingEnabled = true;
            MediaVideoPlayer.Pause();
            MediaVideoPlayer.MediaOpened += (s, e) =>
            {
                // 初始化轨迹
                FaceTrack = new FaceTracker(
                    new ViewFaceCore.Configs.FaceTrackerConfig(
                        MediaVideoPlayer.NaturalVideoWidth,
                        MediaVideoPlayer.NaturalVideoHeight
                    )
                );
            };

            // UI 显示
            MediaController.Source = new Uri(dialog.FileName);
            MediaController.Play();
        }
    }

    private void captureVideoByVisual()
    {
        // RenderTargetBitmap 在截取 MediaElement 时 Grid 等布局会导致截取的定位出现问题，网传 Margin 也会。
        // 而且比例也会。
        // 此种方式截取的帧是渲染在 UI 的画面并非源视频，所以也不利于分析。
        var bmp = new RenderTargetBitmap(
                (int)MediaFrame.Width,
                (int)MediaFrame.Height,
                96, 96, PixelFormats.Pbgra32
            );
        bmp.Render(MediaController);

        using var ms = new MemoryStream();
        //using var ms = File.Open("new.png", FileMode.Create);
        var coder = new PngBitmapEncoder();
        coder.Interlace = PngInterlaceOption.Off;
        coder.Frames.Add(BitmapFrame.Create(bmp));
        coder.Save(ms);

        VideoImage.Source = bmp;
    }

    private async void OnClickCaptureVideoButton(object sender, RoutedEventArgs e)
    {
        while (true)
        {
            await Task.Delay(100);
            var sw = new Stopwatch();
            sw.Start();
            await CaptureVideo();
            sw.Stop();
            Debug.WriteLine($"Captue Time: ${sw.Elapsed}");
        }
    }

    private async Task CaptureVideo()
    {
        // 确保在加载后才执行以下操作。
        MediaVideoPlayer.Position = MediaController.Position;

        Debug.WriteLine($"Capture Video at: {MediaVideoPlayer.Position}");

        var dv = new DrawingVisual();
        var dc = dv.RenderOpen();
        dc.DrawVideo(MediaVideoPlayer, new Rect(0, 0, MediaVideoPlayer.NaturalVideoWidth, MediaVideoPlayer.NaturalVideoHeight));
        dc.Close();

        var bmp = new RenderTargetBitmap(
            MediaVideoPlayer.NaturalVideoWidth,
            MediaVideoPlayer.NaturalVideoHeight,
            96, 96, PixelFormats.Pbgra32
        );
        bmp.Render(dv);

        //SavePng("new2.png", bmp); // 保存
        //VideoImage.Source = bmp;  // 使用原图

        if (FaceTrack == null)
        {
            Debug.WriteLine("FaceTrack 未初始化");
            return;
        }

        using var bitmap = SavePngSKBitmap(bmp);
        using var faceImage = bitmap.ToFaceImage();

        using var faceMark = new FaceLandmarker();
        var dpx = (float)(bitmap.Width / VideoImage.Width + bitmap.Height / VideoImage.Height);

        using var result = new SKBitmap(bitmap.Width, bitmap.Height);
        using var canvas = new SKCanvas(result);
        using var paint = new SKPaint();
        paint.Color = new SKColor(0, 255, 0);
        paint.TextSize = dpx * 12;
        paint.StrokeWidth = dpx; // 图片太大， 这个太小 显示的时候 像素会被缩放没了。
        paint.Style = SKPaintStyle.Stroke;
        paint.StrokeCap = SKStrokeCap.Round;

        canvas.Clear();
        // canvas.DrawBitmap(bitmap, 0, 0);

        // 人脸追踪的效果好像没有人脸识别的好，经常出现未追踪到任何人脸的情况。
        var trackResult = await FaceTrack.TrackAsync(faceImage);
        if (trackResult?.Any() == true)
        {
            foreach (var item in trackResult)
            {
                var info = item.ToFaceInfo();
                var left = info.Location.X;
                var top = info.Location.Y;
                var right = info.Location.X + info.Location.Width;
                var bottom = info.Location.Y + info.Location.Height;

                // 人脸定位
                canvas.DrawRect(left, top, info.Location.Width, info.Location.Height, paint);
                canvas.DrawText($"{item.Pid}", left, bottom + dpx * 12, paint);
            }
        }
        else
        {
            Debug.WriteLine("未追踪到任何人脸！");
        }

        using var rb = result.ToBitmap();
        VideoImage.Source = ToBitmapImage(rb); // 使用打标签的图片
    }

    private static void SavePng(string path, RenderTargetBitmap bmp)
    {
        using var ms = File.Open(path, FileMode.Create);
        var coder = new PngBitmapEncoder();
        coder.Interlace = PngInterlaceOption.Off;
        coder.Frames.Add(BitmapFrame.Create(bmp));
        coder.Save(ms);
    }

    private static SKBitmap SavePngSKBitmap(RenderTargetBitmap bmp)
    {
        using var ms = new MemoryStream();
        var coder = new PngBitmapEncoder();
        coder.Interlace = PngInterlaceOption.Off;
        coder.Frames.Add(BitmapFrame.Create(bmp));
        coder.Save(ms);
        ms.Seek(0, SeekOrigin.Begin);
        return SKBitmap.Decode(ms.ToArray());
    }

    private static BitmapImage SavePngBitmapImage(RenderTargetBitmap bmp)
    {
        using var ms = new MemoryStream();
        var coder = new PngBitmapEncoder();
        coder.Interlace = PngInterlaceOption.Off;
        coder.Frames.Add(BitmapFrame.Create(bmp));
        coder.Save(ms);
        ms.Seek(0, SeekOrigin.Begin);
        var result = new BitmapImage();
        result.BeginInit();
        result.CacheOption = BitmapCacheOption.OnLoad;
        result.StreamSource = ms;
        result.EndInit();
        return result;
    }
}
