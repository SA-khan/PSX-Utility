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
            List<string> result = FirstMethod();
            //foreach(string data in result)
            //{
            //    text += "=> " + data + " - "; 
            //}
            //MessageBox.Show(text);
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

        public List<string> FirstMethod()
        {
            //https://dps.psx.com.pk/download/mkt_summary/2020-11-18.Z
            //HtmlNodeCollection nodes = FetchDataFromPSX("https://dps.psx.com.pk/downloads", "//div");
            //List<string> result = new List<string>();
            //List<string> AllTableRowData = new List<string>();

            //int StartCapturingflag = 0;
            //if (nodes != null)
            //{
            //    foreach (HtmlAgilityPack.HtmlNode node in nodes)
            //    {
            //        if (StartCapturingflag == 1)
            //        {
            //            //AllTableRowData.Add( node.InnerText.ToString() + "\n");
            //        }
            //        else if (node.InnerText.ToString().Trim().Equals("VOLUME"))
            //        {
            //            StartCapturingflag = 1;
            //        }
            //        else
            //        {
            //            AllTableRowData.Add(node.InnerText.ToString() + "\n");
            //        }
            //    }
            //}

            //for (int j = 7; j < AllTableRowData.Count; j = j + 8)
            //{
            //    if (AllTableRowData[j] != null)
            //    {
            //        if (AllTableRowData[j].Trim().Equals("VOLUME"))
            //        {
            //            //result[counter2++] += AllTableRowData[j];
            //        }
            //        else
            //        {
            //            result.Add(AllTableRowData[j]);
            //        }
            //    }
            //}
            //return result;
            using (WebClient wc = new WebClient())
            {
                wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                wc.DownloadFileAsync(
                    // Param1 = Link of file
                    new System.Uri("https://dps.psx.com.pk/download/mkt_summary/2020-11-18.Z"),
                    // Param2 = Path to save
                    ".\\Downloads\\current.rar"
                );
            }
            return null;
        }

        // Event to track the progress
        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }
    }
}
