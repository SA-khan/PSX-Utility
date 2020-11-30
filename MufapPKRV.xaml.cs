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
using System.Linq;

namespace PSXDataFetchingApp
{
    /// <summary>
    /// Interaction logic for MufapPKRV.xaml
    /// </summary>
    public partial class MufapPKRV : Window
    {

        string _Date = String.Empty;
        public List<PKRV> _tempItems = new List<PKRV>();
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

        private async void btnGet_Click(object sender, RoutedEventArgs e)
        { 
            FundImage.Visibility = Visibility.Hidden;
            loadingImage.Visibility = Visibility.Visible;
            list1.Visibility = Visibility.Hidden;
            lblStatus.Text = "Status: Processing";
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = ResourceAccessor.Get("Images/processing.gif");
            image.EndInit();
            ImageBehavior.SetAnimatedSource(imgStatus, image);

            Debug.WriteLine(pkrvDatepicker.SelectedDate.Value);

            int _year = pkrvDatepicker.SelectedDate.Value.Year;
            int _month = pkrvDatepicker.SelectedDate.Value.Month;
            int _day = pkrvDatepicker.SelectedDate.Value.Day;

            string _monthString = String.Empty;
            switch (_month)
            {
                case 1:
                    _monthString = "Jan";
                    break;
                case 2:
                    _monthString = "Feb";
                    break;
                case 3:
                    _monthString = "Mar";
                    break;
                case 4:
                    _monthString = "Apr";
                    break;
                case 5:
                    _monthString = "May";
                    break;
                case 6:
                    _monthString = "Jun";
                    break;
                case 7:
                    _monthString = "Jul";
                    break;
                case 8:
                    _monthString = "Aug";
                    break;
                case 9:
                    _monthString = "Sep";
                    break;
                case 10:
                    _monthString = "Oct";
                    break;
                case 11:
                    _monthString = "Nov";
                    break;
                case 12:
                    _monthString = "Dec";
                    break;
                default:
                    _monthString = "Unknown";
                    break;
            }

            

            if (_year != 0 && _month != 0 && _day != 0 && pkrvDatepicker.SelectedDate.Value != null)
            {
                list1.Items.Clear();
                _Date = pkrvDatepicker.SelectedDate.Value.ToString("dddd, dd MMMM yyyy");
                string url = "http://mufap.com.pk/pdf/PKRVs/" + pkrvDatepicker.SelectedDate.Value.Year + "/" + _monthString + "/PKRV" + _day + _month + _year + ".csv";
                Debug.WriteLine("URL: " +url );
                List<PKRV> _summaryData = new List<PKRV>();
                try
                {
                    await Task.Run(() => _summaryData = GetFileUploadMufapPKRV(url));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                }
                if (_Date != String.Empty)
                {
                    txtDate.Text = "Date: " + Convert.ToDateTime(_Date).ToString("dddd, dd MMMM yyyy");
                    var _selectedItem = from item in _summaryData
                                        select item;
                    _summaryData = _selectedItem.ToList();
                    int counter = 0;
                    for (int i = 0; i < _summaryData.Count; i++)
                    {
                        list1.Items.Add(new PKRV { Id = ++counter, Tenor = _summaryData[i].Tenor, MidRate = _summaryData[i].MidRate, Change = _summaryData[i].Change });
                    }
                    _tempItems = _summaryData;
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

                //Debug.WriteLine("http://mufap.com.pk/pdf/PKRVs/" + pkrvDatepicker.SelectedDate.Value.Year + "/"+_monthString +"/PKRV"+_day+_month+_year+".csv");

            }
            else
            {
                MessageBox.Show("Please select a valid date to fetch market summary closing data.", "Invalid Selection", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            lblStatus.Text = "Status: Ready";
            var image2 = new BitmapImage();
            image2.BeginInit();
            image2.UriSource = ResourceAccessor.Get("Images/tick.gif");
            image2.EndInit();
            ImageBehavior.SetAnimatedSource(imgStatus, image2);
        }

        public List<PKRV> GetFileUploadMufapPKRV(string url)
        {
            //using (WebClient wc = new WebClient())
            //{
            //    wc.DownloadFile(
            //    new System.Uri(url),
            //        "PKRV.csv"
            //    );
            //}
            List<PKRV> items = new List<PKRV>();
            ////string URL = "http://www.mufap.com.pk/industry.php?tab=" + DateTime.Today.Year.ToString() + "1";
            string _fileName = "PKRV.csv";
            if (File.Exists(_fileName))
            {
                File.Delete(_fileName);
            }
            if (!File.Exists(_fileName))
            {
                using (WebClient wc = new WebClient())
                {
                    wc.DownloadFile(
                    new System.Uri(url),
                        _fileName
                    );
                }
                if (File.Exists(_fileName))
                {
                    using (var reader = new StreamReader(_fileName))
                    {

                        ///
                        /// Initializing Variable to read each line of File
                        ///

                        string line;

                        ///
                        /// Initializing Counter To Show on Serial Number on List View 
                        ///

                        int counter = 0;

                        ///
                        /// Clearing Database Table 
                        ///

                        //ClearMarketSummaryClosing();

                        ///
                        /// Iterating Each Line of Market Summary Closing File
                        ///

                        while ((line = reader.ReadLine()) != null)
                        {

                            ///
                            /// Splitting Each Line Into Variables
                            ///

                            string[] data = line.Split(",");

                            if (data[0].ToString().Contains("Tenor")) { }
                            else
                            {
                                ///
                                /// Incrementing Counter to Show On List View Serial Number
                                ///

                                counter++;

                                ///
                                /// Saving Date Value to Global Variable
                                ///

                                //_Date = ;

                                ///
                                /// Getting Values from File
                                ///
                                Debug.WriteLine("=> Tenor: " + data[0].ToString() + ", Mid Rate: " + data[1].ToString() + ", Change: " + data[2].ToString());

                                string lTenor = data[0].ToString();
                                string lMidRate = data[1].ToString();
                                string lChange = data[2].ToString();
                                Debug.WriteLine("=> Tenor: " + lTenor + ", Mid Rate: " + lMidRate + ", Change: " + lChange);
                                PKRV Item = new PKRV { Id = counter, Tenor = lTenor, MidRate = lMidRate, Change = lChange };

                                ///
                                /// Adding Record to ListView
                                ///

                                //if (lTenor.Equals("Tenor")) { }
                                //else
                                //{
                                items.Add(Item);
                                //}

                                ///
                                /// Saving Record
                                ///

                                //int _DbStatus = SavingMarketSummaryClosing(Convert.ToDateTime(_Date), lCategory, lClosing.ToString("N2"), Item);

                                //if (_DbStatus == 0)
                                //{
                                //    Debug.WriteLine("Database Insertion Status: " + _DbStatus + "\n At Name: " + lName);
                                //}
                            }

                        }
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

            return items;
            //Code Runner End
        }

        public bool UnzipPSXFile(string path)
        {
            bool result = false;
            try
            {
                using (ZipArchive za = ZipFile.OpenRead(path))
                {
                    za.ExtractToDirectory("MufapDownloadDirectory/");
                }
                result = true;

                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unzipping File Exception Occured: " + ex.Message);
                return result;
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
