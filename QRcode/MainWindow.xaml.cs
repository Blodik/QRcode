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

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Введите текст для генерации QR-кода.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                BarcodeGenerator QRCodeGenerator = new BarcodeGenerator(EncodeTypes.QR)
                {
                    CodeText = textBox1.Text
                };
                using (MemoryStream save = new MemoryStream())
                {
                    await Task.Run(() => QRCodeGenerator.Save(save, BarCodeImageFormat.Png));
                    save.Position = 0;

                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.StreamSource = save;
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                    Image1.Source = bitmap;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            textBox1.Text = null;
            Image1.Source = null;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Image1.Source == null) 
            {
                MessageBox.Show("QR code не создан");
                return;
            }

            var saveDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "PNG Image (*.png)|*.png",
                DefaultExt = ".png"
            };

            if (saveDialog.ShowDialog() != true) return;

            try
            {
                using (var fileStream = new FileStream(saveDialog.FileName, FileMode.Create))
                {
                    var encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create((BitmapSource)Image1.Source));
                    encoder.Save(fileStream);
                    MessageBox.Show("QR code сохранен");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var openDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "PNG Image (*.png)|*.png",
                DefaultExt = ".png"
            };

            if (openDialog.ShowDialog() != true) return;

            try
            {
                Image1.Source = null;
                var bitmap = new BitmapImage();

                using (var fileStream = new FileStream(openDialog.FileName, FileMode.Open))
                {
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = fileStream;
                    bitmap.EndInit();
                }

                Image1.Source = bitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки: {ex.Message}", "Ошибка");
            }
        }
    }
}
