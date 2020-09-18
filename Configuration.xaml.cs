using PSXDataFetchingApp.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfAnimatedGif;

namespace PSXDataFetchingApp
{
    /// <summary>
    /// Interaction logic for Configuration.xaml
    /// </summary>
    public partial class Configuration : Window
    {
        public Configuration()
        {
            InitializeComponent();

            // Status
            var imageStatus = new BitmapImage();
            imageStatus.BeginInit();
            imageStatus.UriSource = ResourceAccessor.Get("Images/tick.gif");
            imageStatus.EndInit();
            ImageBehavior.SetAnimatedSource(imgStatus, imageStatus);

            //Client Specific Properties
            try
            {
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["Client"]))
                {
                    if (ConfigurationManager.AppSettings["Client"].Equals("BOP"))
                    {
                        // Header Background Color 
                        var bc = new BrushConverter();
                        MainWindow1.Background = (Brush)bc.ConvertFrom("#323280");

                        //lblDemo.Foreground = (Brush)bc.ConvertFrom("#f0a500");

                        //Setting Logo
                        var image = new BitmapImage();
                        image.BeginInit();
                        image.UriSource = ResourceAccessor.Get("Images/BOP.gif");
                        image.EndInit();
                        ImageBehavior.SetAnimatedSource(ClientLogo, image);

                        //Header Background
                        Header.Background = (Brush)bc.ConvertFrom("#f0a500");

                        //Body Background
                        Body.Background = (Brush)bc.ConvertFrom("#ffde80");

                        //Status Bar Background
                        statusBar.Background = (Brush)bc.ConvertFrom("#f0a500");
                    }
                    else if (ConfigurationManager.AppSettings["Client"].Equals("HBL"))
                    {
                        // Header Background Color
                        var bc = new BrushConverter();
                        MainWindow1.Background = (Brush)bc.ConvertFrom("#323280");

                        //lblDemo.Foreground = (Brush)bc.ConvertFrom("#008269");

                        //lblSubHeading.Background = (Brush)bc.ConvertFrom("#008269");

                        //Setting Logo
                        var image = new BitmapImage();
                        image.BeginInit();
                        image.UriSource = ResourceAccessor.Get("Images/HBL.gif");
                        image.EndInit();
                        ImageBehavior.SetAnimatedSource(ClientLogo, image);

                        //Header Background
                        Header.Background = (Brush)bc.ConvertFrom("#008269");

                        //Body Background
                        Body.Background = (Brush)bc.ConvertFrom("#cdcdcd");

                        //Status Bar Background
                        statusBar.Background = (Brush)bc.ConvertFrom("#008269");

                    }
                }
            }
            catch { }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Hide();
        }

        private void btnClient_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPrimaryConnection_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSecondaryConnection_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCompanyCheck_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnInsertCompany_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCompanySymbolCheckSecondary_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
