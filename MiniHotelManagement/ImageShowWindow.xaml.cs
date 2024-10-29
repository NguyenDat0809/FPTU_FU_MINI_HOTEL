using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace MiniHotelManagement
{
    /// <summary>
    /// Interaction logic for ImageShowWindow.xaml
    /// </summary>
    public partial class ImageShowWindow : Window
    {
        private readonly string _imagePath;
        public ImageShowWindow(string path)
        {
            InitializeComponent();
            _imagePath = path;
        }
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var bitmap = new BitmapImage(new Uri(_imagePath));
                imageSource.Source = bitmap;
            }
            catch (Exception)
            {
                MessageBox.Show("Error occure, Please contact to admin");
            }
        }
    }
}
