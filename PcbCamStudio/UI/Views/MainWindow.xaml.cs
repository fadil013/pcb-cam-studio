using System.Windows;
using PcbCamStudio.UI.ViewModels;

namespace PcbCamStudio.UI.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}
