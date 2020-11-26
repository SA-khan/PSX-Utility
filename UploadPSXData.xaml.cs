using HtmlAgilityPack;
using PSXDataFetchingApp.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
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
using WpfAnimatedGif;

namespace PSXDataFetchingApp
{
    /// <summary>
    /// Interaction logic for UploadPSXData.xaml
    /// </summary>
    public partial class UploadPSXData : Window
    {

        //File Date Read From Upload Documnet Variable
        string _Date = String.Empty;

        public UploadPSXData()
        {
            InitializeComponent();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void btnGet_Click(object sender, RoutedEventArgs e)
        {

            FundImage.Visibility = Visibility.Hidden;
            loadingImage.Visibility = Visibility.Visible;
            list1.Visibility = Visibility.Hidden;
            lblStatus.Text = "Status: Processing";
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = ResourceAccessor.Get("Images/exclaimation.png");
            image.EndInit();
            ImageBehavior.SetAnimatedSource(imgStatus, image);


            int _year = datepsxpicker.SelectedDate.Value.Year;
            int _month = datepsxpicker.SelectedDate.Value.Month;
            int _day = datepsxpicker.SelectedDate.Value.Day;
            Debug.WriteLine("Year: " + datepsxpicker.SelectedDate.Value.Year + ", Month: " + datepsxpicker.SelectedDate.Value.Month + ", Day: " + datepsxpicker.SelectedDate.Value.Day);

            list1.Items.Clear();
            _Date = String.Empty;
            List<MarketSummary> _summaryData = new List<MarketSummary>();
            try
            {
                await Task.Run(() => _summaryData = GetFileUploadPSXMarketSummary(_year, _month, _day));
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OKCancel, MessageBoxImage.Information);
            }

            if (_Date != String.Empty)
            {
                txtDate.Text = _Date;
                for (int i = 0; i < _summaryData.Count; i++)
                {
                    list1.Items.Add(new FundMarket { SERIAL = _summaryData[i].ID, NAME = _summaryData[i].Name, SYMBOL = _summaryData[i].Symbol, LDCP = _summaryData[i].LDCP, OPEN = _summaryData[i].OPEN, HIGH = _summaryData[i].HIGH, LOW = _summaryData[i].LOW, CHANGE = _summaryData[i].Change.ToString(), VOLUME = _summaryData[i].Volume });
                }
                FundImage.Visibility = Visibility.Hidden;
                loadingImage.Visibility = Visibility.Hidden;
                list1.Visibility = Visibility.Visible;
            }
            else
            {
                FundImage.Visibility = Visibility.Visible;
                loadingImage.Visibility = Visibility.Hidden;
                list1.Visibility = Visibility.Hidden;
            }

            

            lblStatus.Text = "Status: Ready";
            var image2 = new BitmapImage();
            image2.BeginInit();
            image2.UriSource = ResourceAccessor.Get("Images/tick.gif");
            image2.EndInit();
            ImageBehavior.SetAnimatedSource(imgStatus, image2);
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

        public List<MarketSummary> GetFileUploadPSXMarketSummary(int _year, int _month, int _day)
        {
            List<MarketSummary> items = new List<MarketSummary>();
            string _fileName = "MarketSummary.rar";
            string _folderName = "PSXDownloadDirectory";
            string _unzipLisFile = _folderName + "\\closing" + _month + ".lis";
            if (File.Exists(_fileName))
            {
                File.Delete(_fileName);
            }
            if (Directory.Exists(_folderName))
            {
                if (File.Exists(_unzipLisFile))
                {
                    File.Delete(_unzipLisFile);
                }
                //System.Threading.Thread.Sleep(1000);
                Directory.Delete(_folderName);
            }
            if (!File.Exists(_fileName) && !Directory.Exists(_folderName) && !File.Exists(_unzipLisFile))
            {
                    using (WebClient wc = new WebClient())
                    {
                        //wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                        wc.DownloadFileAsync(
                        // Param1 = Link of file

                        new System.Uri("https://dps.psx.com.pk/download/mkt_summary/" + _year + "-" + _month + "-" + _day + ".Z"),
                            // Param2 = Path to save
                            _fileName
                        );
                    }
                
                if (File.Exists(_fileName))
                {
                    System.Threading.Thread.Sleep(3500);
                    bool boolUnzip = false;
                    try
                    {
                        boolUnzip = UnzipPSXFile(_fileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("File is being used by another process.\n" + ex.Message, "PSX File Is Being Used", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    //Debug.WriteLine("Unzip File Status: " + boolUnzip);
                    if (boolUnzip)
                    {
                        using (var reader = new StreamReader(_unzipLisFile))
                        {
                            string line;
                            int counter = 0;
                            while ((line = reader.ReadLine()) != null)
                            {
                                // Do stuff with your line here, it will be called for each 
                                // line of text in your file.
                                counter++;
                                string[] data = line.Split("|");
                                //Debug.WriteLine("Length: " + data.Length);
                                _Date = data[0];
                                //Debug.WriteLine("Date: " + data[0].ToString());
                                string lSymbol = data[1].ToString();
                                //Debug.WriteLine("Symbol: " + lSymbol);
                                //Debug.WriteLine("Category: " + data[2].ToString());
                                string lName = data[3].ToString();
                                //Debug.WriteLine("Name: " + lName);
                                decimal lLdcp = Convert.ToDecimal(data[9]);
                                //Debug.WriteLine("LDCP: " + lLdcp.ToString("N2"));
                                decimal lOpen = Convert.ToDecimal(data[4]);
                                //Debug.WriteLine("OPEN: " + lOpen.ToString("N2"));
                                decimal lHigh = Convert.ToDecimal(data[5]);
                                //Debug.WriteLine("HIGH: " + lHigh.ToString("N2"));
                                decimal lLow = Convert.ToDecimal(data[6]);
                                //Debug.WriteLine("LOW: " + lLow.ToString("N2"));
                                decimal lClosing = Convert.ToDecimal(data[7]);
                                //Debug.WriteLine("Closing: " + lClosing.ToString("N2"));
                                decimal lVolume = Convert.ToDecimal(data[8]);
                                //Debug.WriteLine("VOLUME: " + lVolume.ToString("N0"));
                                items.Add(new MarketSummary { ID = counter, Name = lName, Symbol = lSymbol, LDCP = lLdcp.ToString("N2"), OPEN = lOpen.ToString("N2"), HIGH = lHigh.ToString("N2"), LOW = lLow.ToString("N2"), Volume = lVolume.ToString("N0"), Change = Convert.ToDouble(lClosing.ToString("N2")) });
                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show("No Market Summary File Exist. Please check Pakistan Stock Exchange Site for more information.", "PSX File Not Found", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }

                
            }

            else
            {
                MessageBox.Show("Files and Folders Are Not Deleted.\n Please Delete Market Summary.rar and PSXDownload Folder from Root Directory.", "PSX File Not Found", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (File.Exists(_fileName))
            {
                File.Delete(_fileName);
            }
            if (Directory.Exists(_folderName))
            {
                if (File.Exists(_unzipLisFile))
                {
                    File.Delete(_unzipLisFile);
                }
                Directory.Delete(_folderName);
            }

            return items;
            //Code Runner End
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

        public bool UnzipPSXFile(string path)
        {
            bool result = false;
            try
            {
                using (ZipArchive za = ZipFile.OpenRead(path))
                {
                    //do something with `za.Entries` or with other properties
                    za.ExtractToDirectory("PSXDownloadDirectory/");
                }
                result = true;

                return result;
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Unzipping File Exception Occured: " + ex.Message);
                return result;
            }
        }

        private void btnPsxLink_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
