using System;
using System.Runtime.InteropServices;

namespace GradientBall;

public static class DwmapiTK
{
    [DllImport("Dwmapi.dll", ExactSpelling = true, PreserveSig = false)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool DwmIsCompositionEnabled();
}
