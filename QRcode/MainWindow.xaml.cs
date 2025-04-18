using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
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
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace QRcode
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BarcodeGenerator QRCodeGenerator = new BarcodeGenerator(EncodeTypes.QR)
            {
                CodeText = textBox1.Text
            };

            MemoryStream save = new MemoryStream();

            QRCodeGenerator.Save(save, BarCodeImageFormat.Png);
            save.Position = 0;

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = save;
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();

            Image1.Source = bitmap;

            save.Dispose();
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            textBox1.Text = null;
            Image1.Source = null;
            textBox2.Text = null;
            textBox3.Text = null;
            textBox4.Text = null;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string filepath = textBox2.Text;
            string filename = textBox3.Text;
            filepath = filepath + "\\" + filename + ".png";

            BarcodeGenerator QRCodeGenerator = new BarcodeGenerator(EncodeTypes.QR)
            {
                CodeText = textBox1.Text
            };

            QRCodeGenerator.Save(filepath, BarCodeImageFormat.Png);

            MessageBox.Show($"Qr-code сохранен: {filepath}");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string filepath = textBox4.Text;

            BitmapImage bitmap = new BitmapImage();

            bitmap.BeginInit();
            bitmap.UriSource = new Uri(filepath);
            bitmap.EndInit();

            Image1.Source = bitmap;
        }
    }
}
