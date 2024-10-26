using Microsoft.Win32;
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
    /// Interaction logic for FormatSelectWindow.xaml
    /// </summary>
    public partial class FormatSelectWindow : Window
    {
        public string SelectedFormat { get; private set; }
        public string FilePath { get; private set; }

        public FormatSelectWindow()
        {
            InitializeComponent();
        }
        private void JsonButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedFormat = "JSON";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "files|*.json;";
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == true)
            {
                if (!string.IsNullOrEmpty(openFileDialog.FileName))
                {
                    FilePath =  openFileDialog.FileName;
                    DialogResult = true;
                    Close();
                }
            }
            
        }

        private void XmlButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedFormat = "XML";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "files|*.xml;";
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == true)
            {
                if (!string.IsNullOrEmpty(openFileDialog.FileName))
                {
                    FilePath = openFileDialog.FileName;
                    DialogResult = true;
                    Close();
                }
            }
        }
    }
}
