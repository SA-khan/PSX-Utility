using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PSXDataFetchingApp
{
    /// <summary>
    /// Interaction logic for Disclaimer.xaml
    /// </summary>
    public partial class Disclaimer : Window
    {
        public Disclaimer()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow cls = new MainWindow();
            cls.Show();
            this.Hide();
        }
    }
}
