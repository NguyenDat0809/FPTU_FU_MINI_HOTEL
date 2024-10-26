using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace MiniHotelManagement
{
    /// <summary>
    /// Interaction logic for ImageShowWindow.xaml
    /// </summary>
    public partial class ImageShowWindow : Window
    {
        private readonly BitmapImage _image;
        public ImageShowWindow(string path)
        {
            InitializeComponent();
            _image = new BitmapImage(new Uri(path));
        }
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            imageSource.Source = _image;
        }
    }
}
