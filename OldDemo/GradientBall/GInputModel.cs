using System;
using System.Windows;
using ReactiveUI;

namespace GradientBall;

public class GInputModel : ReactiveObject
{
    public double Width { get; set; } = 300;
    public double Height { get; set; } = 60;

    public Rect BigRect => new Rect(0, 0, Width, Height);
    public double BigRadius => Height * 0.5;

    public double SmallLeft => Width * 0.2;
    public double SmallTop => Height * 0.2;
    public double SmallWidth => Width * 0.666;
    public double SmallHeight => Height * 0.5;
    public Rect SmallRect => new Rect(0, 0, SmallWidth, SmallHeight);
    public double SmallRadius => SmallHeight * 0.5;

    public double InputLeft => SmallLeft + SmallWidth * 0.1;
    public double InputTop => SmallTop + SmallHeight * 0.2;
    public double InputWidth => SmallWidth * 0.6;
    public double InputHeight => SmallHeight * 0.6;
}
