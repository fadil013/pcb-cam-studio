using System.Windows.Controls;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;

namespace PcbCamStudio.UI.Views
{
    public partial class Viewport3D : UserControl
    {
        public Viewport3D()
        {
            InitializeComponent();
        }

        public void AddModel(Model3D model)
        {
            var visual = new ModelVisual3D { Content = model };
            Viewport.Children.Add(visual);
        }

        public void ClearModels()
        {
            Viewport.Children.Clear();
        }

        public void ZoomToFit() => Viewport.ZoomExtents();
    }
}
