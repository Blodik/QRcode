using Microsoft.Win32.SafeHandles;
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

namespace QRcode
{
    /// <summary>
    /// Логика взаимодействия для Autorizator.xaml
    /// </summary>
    public partial class Autorizator : Window
    {
        public Autorizator()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string login = textBox1.Text;
            string password = textBox2.Password;
            if (login == "admin" && password == "1admin")
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();

                this.Close();
            }
            else
                MessageBox.Show("Не верный логин или пароль");

        }
    }
}
