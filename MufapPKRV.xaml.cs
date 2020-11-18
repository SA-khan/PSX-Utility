using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net;
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
    /// Interaction logic for MufapPKRV.xaml
    /// </summary>
    public partial class MufapPKRV : Window
    {
        public MufapPKRV()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
            
        }

        private HtmlNodeCollection FetchDataFromPSX(string url, string param)
        {
            string URL = url;
            HtmlDocument doc = new HtmlDocument();
            WebClient client = new WebClient();
            try
            {
                string html = client.DownloadString(URL);
                doc.LoadHtml(html);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            HtmlNodeCollection result = doc.DocumentNode.SelectNodes(param);
            return result;
        }

        private void btnGet_Click(object sender, RoutedEventArgs e)
        {
            string URL = "http://www.mufap.com.pk/industry.php?tab=" + DateTime.Today.Year.ToString() + "1";
            HtmlNodeCollection name_nodes = FetchDataFromPSX(URL, "//div");
            string[] result = new string[name_nodes.Count];
            string text = String.Empty;

            foreach (HtmlAgilityPack.HtmlNode node in name_nodes)
            {
                if (node.InnerText != null)
                    text += node.InnerText.ToString();

            }

            for (int i = 0; i < result.Length; i++)
            {
                text += result[i] + "\n";
            }

            MessageBox.Show(text);
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
