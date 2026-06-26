using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using SkiaSharp.Views.WPF;

namespace PcbCamStudio.UI.Views
{
    public partial class Viewport2D : UserControl
    {
        private float _zoom = 1f;
        private SKPoint _pan = new(0, 0);
        private Point _lastMouse;
        private bool _isPanning;

        public Viewport2D()
        {
            InitializeComponent();
        }

        private void SkiaCanvas_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            canvas.Clear(new SKColor(0x11, 0x11, 0x11));

            canvas.Save();
            canvas.Translate(_pan.X, _pan.Y);
            canvas.Scale(_zoom);

            // TODO: render layers (copper=gold, outline=white, drill=cyan, toolpaths=red)

            canvas.Restore();
        }

        private void SkiaCanvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            float factor = e.Delta > 0 ? 1.1f : 0.9f;
            _zoom = System.Math.Clamp(_zoom * factor, 0.1f, 50f);
            ZoomText.Text = $"{_zoom * 100:F0}%";
            SkiaCanvas.InvalidateVisual();
        }

        private void SkiaCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.MiddleButton == MouseButtonState.Pressed || e.RightButton == MouseButtonState.Pressed)
            {
                _isPanning = true;
                _lastMouse = e.GetPosition(this);
                SkiaCanvas.CaptureMouse();
            }
        }

        private void SkiaCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            var pos = e.GetPosition(this);
            CoordsText.Text = $"X: {(pos.X - _pan.X) / _zoom:F3}   Y: {(pos.Y - _pan.Y) / _zoom:F3}";

            if (_isPanning)
            {
                _pan.X += (float)(pos.X - _lastMouse.X);
                _pan.Y += (float)(pos.Y - _lastMouse.Y);
                _lastMouse = pos;
                SkiaCanvas.InvalidateVisual();
            }
        }

        private void SkiaCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _isPanning = false;
            SkiaCanvas.ReleaseMouseCapture();
        }

        public void Refresh() => SkiaCanvas.InvalidateVisual();
    }
}
