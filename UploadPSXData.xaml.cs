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
    /// Interaction logic for UploadPSXData.xaml
    /// </summary>
    public partial class UploadPSXData : Window
    {
        public UploadPSXData()
        {
            InitializeComponent();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnGet_Click(object sender, RoutedEventArgs e)
        {
            string text = String.Empty;
            String[] result = FirstMethod();
            foreach(string data in result)
            {
                text += "=> " + data + " - "; 
            }
            MessageBox.Show(text);
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

        public String[] FirstMethod()
        {
            HtmlNodeCollection name_nodes = FetchDataFromPSX("https://dps.psx.com.pk/downloads", "//td");
            string[] result = new string[name_nodes.Count];
            string[] AllTableRowData = new string[name_nodes.Count];

            int counter = 0;
            int StartCapturingflag = 0;

            foreach (HtmlAgilityPack.HtmlNode node in name_nodes)
            {
                if (StartCapturingflag == 1)
                {
                    AllTableRowData[counter++] = node.InnerText.ToString() + "\n";
                }
                else if (node.InnerText.ToString().Trim().Equals("VOLUME"))
                {
                    StartCapturingflag = 1;
                }
                else
                {

                }
            }
            int counter2 = 0;
            for (int j = 7; j < AllTableRowData.Length; j = j + 8)
            {
                if (AllTableRowData[j] != null)
                {
                    if (AllTableRowData[j].Trim().Equals("VOLUME"))
                    {
                        //result[counter2++] += AllTableRowData[j];
                    }
                    else
                    {
                        result[counter2++] += AllTableRowData[j];
                    }
                }
            }
            return result;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }
    }
}
