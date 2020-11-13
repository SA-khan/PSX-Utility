using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PSXDataFetchingApp
{
    /// <summary>
    /// Interaction logic for MUFAPMarketSummary.xaml
    /// </summary>
    public partial class MUFAPMarketSummary : Window
    {
        public MUFAPMarketSummary()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnGet_Click(object sender, RoutedEventArgs e)
        {
            string response =  getMUFAPMarketSummaryRates("Money Market");
            MessageBox.Show(response);
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

        public string getMUFAPMarketSummaryRates(string tag)
        {

            string URL = "http://www.mufap.com.pk/nav_returns_performance.php?tab=01";
            HtmlNodeCollection name_nodes = FetchDataFromPSX(URL, "//td");

            string[] result = new string[name_nodes.Count];
            string text = String.Empty;
            int startCapture = 0;

            foreach (HtmlAgilityPack.HtmlNode node in name_nodes)
            {
                if (node.InnerText != null)
                {
                    //if (node.InnerText.Contains(tag))
                    //{
                    if (node.InnerText.ToString() == "S&M**")
                    {
                        startCapture = 1;
                    }
                    else if (node.InnerText.ToString().Trim().Contains("Capital") && node.InnerText.ToString().Trim().Contains("Protected") && node.InnerText.ToString().Trim().Contains("Absolute") && node.InnerText.ToString().Trim().Contains("Return"))
                    {
                        startCapture = 0;
                        break;
                    }
                    else if (startCapture == 1)
                    {
                        text += node.InnerText.ToString();
                    }
                    //}

                }

            }

            for (int i = 0; i < result.Length; i++)
            {
                text += result[i] + "\n";

            }
            return text;
        }

    }
}
