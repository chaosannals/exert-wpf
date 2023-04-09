using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace DiDemo.Views.Widgets;

public class DrawingDemoElement : FrameworkElement
{
    private DrawingVisual? drawing;

    public DrawingDemoElement()
    {
        Loaded += (s, e) =>
        {
            drawing = new DrawingVisual();
            using DrawingContext dc = drawing.RenderOpen();
            Pen p = new Pen(Brushes.BlueViolet, 4);
            dc.PushTransform(new ScaleTransform(0.24, 0.24));
            dc.DrawGeometry(null, p, Geometry.Parse(@"M813.21 367.87a399.57 399.57 0 0 1 112.56 184.06c14.57 91.78-12.25 138.81-12.25 138.81C872.25 777.55 725 834 725 834l-0.25 38.89a46.69 46.69 0 0 1-45.78 44.15 489.13 489.13 0 0 1-85.15-22.76c-63.61 16.36-74 10.64-74 10.64a410.86 410.86 0 0 1-45.46-5.85 343.77 343.77 0 0 1-85.4 16.22c-56.88 1.49-56.66-33.69-56.66-33.69l0.25-39c-19.84-8.46-107.69-56.24-107.69-56.24-130.37-62-123.94-178.6-123.94-178.6a286 286 0 0 1 46.48-155.35C297.24 234 518.28 249.31 518.28 249.31c165.1 5.78 294.93 118.56 294.93 118.56z"));
            dc.DrawGeometry(Brushes.Yellow, p, Geometry.Parse(@"M723.34 322.24c5.11-1.89 10-3.77 14.76-5.65-53.65-30.59-131.7-64.19-219.82-67.28 0 0-221-15.29-370.89 203.16a286 286 0 0 0-46.48 155.35s-6.44 116.65 123.94 178.6c0 0 87.84 47.78 107.69 56.24l-0.25 39s-0.06 13.93 14.06 23.89c-16.35-74.23-66-419.4 376.99-583.31z"));

            // 透明 Transparent 会触发点击命中，null 不会
            dc.DrawGeometry(Brushes.Transparent, p, Geometry.Parse(@"M679.29 377.69a384.74 384.74 0 0 0-156.19-59.07 146.82 146.82 0 0 0-45.88 0 397.53 397.53 0 0 0-161.87 59.12c-49 31.31-98.18 75-96.67 150 0 0-4.53 57.11 34.12 77.8 0 0 65.92 40.71 247.86 45.49 182-4.79 241.15-45.49 241.15-45.49 34.89-19.15 34.12-77.8 34.12-77.8 1.44-75.07-96.64-150.07-96.64-150.07z"));
            dc.DrawGeometry(null, p, Geometry.Parse(@"M624.82 348a384.79 384.79 0 0 0-101.72-29.37 146.81 146.81 0 0 0-45.88 0 397.53 397.53 0 0 0-161.87 59.12c-49 31.31-98.18 75-96.67 150 0 0-4.53 57.11 34.12 77.8 0 0 15.91 9.79 53.54 20.34 0.13-1.89 19.66-250.21 276.33-272.53 16.12-1.36 30.1-3.21 42.15-5.36z"));
            dc.DrawGeometry(null, p, Geometry.Parse(@"M672.81 929.52l-1.4-0.24A505.26 505.26 0 0 1 588 907.41c-37.85 9.5-63.26 12.79-75.77 9.83-14-1.06-28.05-2.83-41.91-5.28a358.75 358.75 0 0 1-85.48 15.77c-26.29 0.7-45.79-5.31-58.54-17.82-13.68-13.41-14-29.13-14-30.88l0.19-29.37c-28.58-13.92-93.31-49.07-99.47-52.41C78.32 733 80.58 614.44 81 604.94a300.21 300.21 0 0 1 48.9-163.13C283 218.59 505 231.27 514.33 231.92 682.27 237.79 812.56 349.35 818 354.1l0.37 0.33a414.45 414.45 0 0 1 116.79 190.95l0.38 1.72c14.52 91.43-10.33 141.68-13.7 148-37.71 78.72-152.71 132.07-187 146.54l-0.18 28.91c-1.77 32.81-28.3 58.41-60.44 59z m-83.23-53.35l4.36 1.6a475.64 475.64 0 0 0 81 21.8 31.89 31.89 0 0 0 29.84-30l0.29-48.37 9.52-3.66C716 817 856.86 762.18 895 681.84l0.51-1c0.19-0.35 23.74-44.22 10.61-128.2a384.57 384.57 0 0 0-107.84-176.12C793 372 667.84 267.18 512.78 261.75c-2.64-0.16-215.12-11.68-358 196.68a270.15 270.15 0 0 0-43.87 147v0.77c-0.21 4.32-3.76 107.65 115.43 164.29l0.73 0.37c30.17 16.41 91.24 49.15 106.41 55.62l9.14 3.9-0.31 48.9c0.11 1.64 2.21 19.7 41.33 18.66a328.33 328.33 0 0 0 81.17-15.49l3.61-1.17 3.73 0.69a397.62 397.62 0 0 0 43.8 5.63l3.27 0.23 1.08 0.6c3.69 0.33 19.51 0.62 64.85-11z m-70.25 12.08z"));
            dc.DrawGeometry(Brushes.Cyan, p, Geometry.Parse(@"M503.14 668.48h-0.39C324.67 663.78 254.68 625 247.9 621c-44.11-23.92-42.16-83.1-41.68-91.15-1.4-85.17 58.41-133.38 103.57-162.24a415.14 415.14 0 0 1 168-61.35 161.25 161.25 0 0 1 50.18 0.05A402 402 0 0 1 690 367.69l0.88 0.62c4.25 3.25 104.07 80.53 102.5 162.22 0 2.27 0.4 66.85-41.33 90.31-7.84 5-72.7 43-248.49 47.63z m-0.55-334.24a131.22 131.22 0 0 0-20.59 1.62 385.52 385.52 0 0 0-156.16 57c-42.5 27.14-91.14 67.3-89.74 137.09v0.74l-0.06 0.74c0 0.43-3.15 47.69 26.28 63.45l0.8 0.46c0.57 0.34 66.07 38.63 240 43.26 173.19-4.6 232.24-42.6 232.81-42.93l1.18-0.71c26.29-14.44 26.38-64 26.37-64.51 1.06-55.56-64.44-118.12-90.36-138.07a372.24 372.24 0 0 0-149.67-56.48 133.91 133.91 0 0 0-20.86-1.66z"));
            dc.DrawGeometry(Brushes.Black, p, Geometry.Parse(@"M622.89 557.69a23.95 23.95 0 0 1-23.94-23.95v-71.83a23.95 23.95 0 1 1 47.89 0v71.84a23.94 23.94 0 0 1-23.95 23.94zM383.44 557.69a23.95 23.95 0 0 1-23.94-23.95v-71.83a23.95 23.95 0 1 1 47.89 0v71.84a23.95 23.95 0 0 1-23.95 23.94zM751.88 322.34l-27.58-11.5c2.2-5.28 55.06-129.27 164.59-129.27v29.88c-89.5 0-136.54 109.78-137.01 110.89z"));
            AddVisualChild(drawing);
            AddLogicalChild(drawing);
        };

        MouseLeftButtonDown += (s, e) =>
        {
            Point p = e.GetPosition(this);

            // 简单选中
            HitTestResult result = VisualTreeHelper.HitTest(this, p);
            if (result.VisualHit == drawing)
            {
                MessageBox.Show($"选中了 hashcode: {this.GetHashCode()}", "选中", MessageBoxButton.OK);
            }

            // 复杂选中
            VisualTreeHelper.HitTest(this, (e) =>
            {
                MessageBox.Show($"过滤 hashcode: {this.GetHashCode()}", "选中", MessageBoxButton.OK);
                return HitTestFilterBehavior.Continue;
            },
            (r) =>
            {
                if (result.VisualHit == drawing)
                {
                    MessageBox.Show($"选中了 drawing hashcode: {this.GetHashCode()}", "选中", MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show($"选中了 其他 hashcode: {this.GetHashCode()}", "选中", MessageBoxButton.OK);
                }
                return HitTestResultBehavior.Continue;
            }, new PointHitTestParameters(p));
        };
    }

    protected override int VisualChildrenCount => 1;
    protected override Visual GetVisualChild(int index)
    {
        if (index < 0 || index >= VisualChildrenCount)
        {
            throw new IndexOutOfRangeException($"{ drawing}");
        }
        return drawing;
    }
}
