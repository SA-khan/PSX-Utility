﻿using HtmlAgilityPack;
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
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Threading;
using Oracle.ManagedDataAccess.Client;

namespace PSXDataFetchingApp
{
    /// <summary>
    /// Interaction logic for UploadPSXData.xaml
    /// </summary>
    public partial class UploadPSXData : Window
    {
        public DataContext _context;
        //File Date Read From Upload Documnet Variable
        string _Date = String.Empty;

        //
        public List<MarketSummary> _tempItems = new List<MarketSummary>();

        #region ClientSpecificProperties

        public void ClientSpecificProperties()
        {
            //Client Specific Properties
            try
            {
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["Client"]))
                {

                    #region BOP

                    if (ConfigurationManager.AppSettings["Client"].Equals("BOP"))
                    {
                        // Header Background Color 
                        var bc = new BrushConverter();
                        HeaderColor.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#f0a500");

                        //Setting Logo
                        var image = new BitmapImage();
                        image.BeginInit();
                        image.UriSource = ResourceAccessor.Get("Images/BOP.gif");
                        image.EndInit();
                        ImageBehavior.SetAnimatedSource(HeaderImage, image);
                    }

                    #endregion

                    #region HBL

                    else if (ConfigurationManager.AppSettings["Client"].Equals("HBL"))
                    {
                        // Header Background Color
                        var bc = new BrushConverter();
                        HeaderColor.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#008269");

                        //Setting Logo
                        var image = new BitmapImage();
                        image.BeginInit();
                        image.UriSource = ResourceAccessor.Get("Images/HBL.gif");
                        image.EndInit();
                        ImageBehavior.SetAnimatedSource(HeaderImage, image);
                    }

                    #endregion

                    #region EFU

                    else if (ConfigurationManager.AppSettings["Client"].Equals("EFU"))
                    {
                        // Header Background Color
                        var bc = new BrushConverter();
                        HeaderColor.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#008269");

                        //Setting Logo
                        var image = new BitmapImage();
                        image.BeginInit();
                        image.UriSource = ResourceAccessor.Get("Images/efu.png");
                        image.EndInit();
                        ImageBehavior.SetAnimatedSource(HeaderImage, image);
                    }

                    #endregion

                    #region SIZA

                    else if (ConfigurationManager.AppSettings["Client"].Equals("SIZA"))
                    {
                        // Header Background Color
                        var bc = new BrushConverter();
                        HeaderColor.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#008269");

                        //Setting Logo
                        var image = new BitmapImage();
                        image.BeginInit();
                        image.UriSource = ResourceAccessor.Get("Images/SIZA_PHARMA.png");
                        image.EndInit();
                        ImageBehavior.SetAnimatedSource(HeaderImage, image);
                    }

                    #endregion

                }
            }
            catch { }
        }

        #endregion

        #region UploadPSXData

        public UploadPSXData()
        {
            InitializeComponent();

            datepsxpicker.SelectedDate = DateTime.Now;

            ClientSpecificProperties();

        }

        #endregion

        #region btnReset_Click

        private async void btnReset_Click(object sender, RoutedEventArgs e)
        {
            btnReset.IsEnabled = false;
            FundImage.Visibility = Visibility.Hidden;
            loadingImage.Visibility = Visibility.Visible;
            list1.Visibility = Visibility.Hidden;
            lblStatus.Text = "Status: Processing";
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = ResourceAccessor.Get("Images/processing.gif");
            image.EndInit();
            ImageBehavior.SetAnimatedSource(imgStatus, image);
            Debug.WriteLine("Date Value: ");
            try
            {
                if (datepsxpicker.SelectedDate.Value != null)
                {
                    int _year = datepsxpicker.SelectedDate.Value.Year;
                    int _month = datepsxpicker.SelectedDate.Value.Month;
                    int _day = datepsxpicker.SelectedDate.Value.Day;

                    list1.Items.Clear();
                    _Date = String.Empty;
                    List<MarketSummary> _summaryData = new List<MarketSummary>();
                    try
                    {
                        await Task.Run(() => _summaryData = GetFileUploadPSXMarketSummary(_year, _month, _day));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("There is no file exist on PSX Site.\n" + ex.Message, "File Not Found", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                    }
                    if (_Date != String.Empty)
                    {
                        txtDate.Text = "Date: " + Convert.ToDateTime(_Date).ToString("dddd, dd MMMM yyyy");
                        var _selectedItem = from item in _summaryData
                                            orderby item.Name
                                            select item;
                        _summaryData = _selectedItem.ToList();
                        int counter = 0;
                        for (int i = 0; i < _summaryData.Count; i++)
                        {
                            list1.Items.Add(new FundMarket { SERIAL = ++counter, NAME = _summaryData[i].Name, SYMBOL = _summaryData[i].Symbol, LDCP = _summaryData[i].LDCP, OPEN = _summaryData[i].OPEN, HIGH = _summaryData[i].HIGH, LOW = _summaryData[i].LOW, CHANGE = _summaryData[i].Change.ToString(), VOLUME = _summaryData[i].Volume });
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

                }
                else
                {
                    MessageBox.Show("Please select a valid date to fetch market summary closing data.", "Invalid Selection", MessageBoxButton.OK, MessageBoxImage.Information);
                    FundImage.Visibility = Visibility.Visible;
                    loadingImage.Visibility = Visibility.Hidden;
                    list1.Visibility = Visibility.Hidden;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Please select a valid date to fetch market summary closing data.\nDetails: " + ex.Message, "Invalid Selection", MessageBoxButton.OK, MessageBoxImage.Information);
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
            btnReset.IsEnabled = true;
        }

        #endregion 

        #region btnGet_Click

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

            try
            {

                int _year = datepsxpicker.SelectedDate.Value.Year;
                int _month = datepsxpicker.SelectedDate.Value.Month;
                int _day = datepsxpicker.SelectedDate.Value.Day;
                if (_year != 0 && _month != 0 && _day != 0)
                {
                    list1.Items.Clear();
                    _Date = String.Empty;
                    List<MarketSummary> _summaryData = new List<MarketSummary>();
                    try
                    {
                        await Task.Run(() => _summaryData = GetFileUploadPSXMarketSummary(_year, _month, _day));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("There is no file exist on PSX Site.\n" + ex.Message, "File Not Found", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                    }
                    if (_Date != String.Empty)
                    {
                        txtDate.Text = "Date: " + Convert.ToDateTime(_Date).ToString("dddd, dd MMMM yyyy");
                        var _selectedItem = from item in _summaryData
                                            orderby item.Name
                                            select item;
                        _summaryData = _selectedItem.ToList();

                        int counter = 0;
                        for (int i = 0; i < _summaryData.Count; i++)
                        {
                            list1.Items.Add(new FundMarket { SERIAL = ++counter, NAME = _summaryData[i].Name, SYMBOL = _summaryData[i].Symbol, LDCP = _summaryData[i].LDCP, OPEN = _summaryData[i].OPEN, HIGH = _summaryData[i].HIGH, LOW = _summaryData[i].LOW, CHANGE = _summaryData[i].Change.ToString(), VOLUME = _summaryData[i].Volume });
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

                }
                else
                {
                    MessageBox.Show("Please select a valid date to fetch market summary closing data.", "Invalid Selection", MessageBoxButton.OK, MessageBoxImage.Information);
                    FundImage.Visibility = Visibility.Visible;
                    loadingImage.Visibility = Visibility.Hidden;
                    list1.Visibility = Visibility.Hidden;
                }
            }
            catch(Exception ex)
            {
                FundImage.Visibility = Visibility.Visible;
                loadingImage.Visibility = Visibility.Hidden;
                list1.Visibility = Visibility.Hidden;
                MessageBox.Show("Please select a valid date.\nDetails: " + ex.Message, "Invalid Selection", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            lblStatus.Text = "Status: Ready";
            var image2 = new BitmapImage();
            image2.BeginInit();
            image2.UriSource = ResourceAccessor.Get("Images/tick.gif");
            image2.EndInit();
            ImageBehavior.SetAnimatedSource(imgStatus, image2);
        }

        #endregion

        #region FetchDataFromPSX

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

        #endregion 

        #region GetFileUploadPSXMarketSummary

        public List<MarketSummary> GetFileUploadPSXMarketSummary(int _year, int _month, int _day)
        {
            List<MarketSummary> items = new List<MarketSummary>();
            string _fileName = "MarketSummary.rar";
            string _folderName = "PSXDownloadDirectory";
            string _unzipLisFile = _folderName + "\\closing11.lis";
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
                System.Threading.Thread.Sleep(1000);
                Directory.Delete(_folderName);
            }
            if (!File.Exists(_fileName) && !Directory.Exists(_folderName) && !File.Exists(_unzipLisFile))
            {
                using (WebClient wc = new WebClient())
                {
                    wc.DownloadFile(
                    new System.Uri("https://dps.psx.com.pk/download/mkt_summary/" + _year + "-" + _month + "-" + _day + ".Z"),
                        _fileName
                    );
                }
                if (File.Exists(_fileName))
                {
                    bool boolUnzip = false;
                    try
                    {
                        boolUnzip = UnzipPSXFile(_fileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("File is being used by another process.\n" + ex.Message, "PSX File Is Being Used", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                    ///
                    /// If Condition to Check Whether File is successfull unzip and ready to be read.
                    ///

                    if (boolUnzip)
                    {
                        using (var reader = new StreamReader(_unzipLisFile))
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

                            ClearMarketSummaryClosing();

                            //CLearing In Ipams
                            //ClearMarketSummaryClosingIpams();

                            ///
                            /// Iterating Each Line of Market Summary Closing File
                            ///

                            while ((line = reader.ReadLine()) != null)
                            {

                                ///
                                /// Incrementing Counter to Show On List View Serial Number
                                ///

                                counter++;

                                ///
                                /// Splitting Each Line Into Variables
                                ///

                                string[] data = line.Split("|");

                                ///
                                /// Saving Date Value to Global Variable
                                ///

                                _Date = data[0];

                                ///
                                /// Getting Values from File
                                ///

                                string lSymbol = data[1].ToString();
                                Int64 lCategory = Convert.ToInt64(data[2].ToString());
                                string lName = data[3].ToString();
                                decimal lLdcp = Convert.ToDecimal(data[9]);
                                decimal lOpen = Convert.ToDecimal(data[4]);
                                decimal lHigh = Convert.ToDecimal(data[5]);
                                decimal lLow = Convert.ToDecimal(data[6]);
                                decimal lClosing = Convert.ToDecimal(data[7]);
                                decimal lVolume = Convert.ToDecimal(data[8]);
                                MarketSummary Item = new MarketSummary { ID = counter, Name = lName, Symbol = lSymbol, LDCP = lLdcp.ToString("N2"), OPEN = lOpen.ToString("N2"), HIGH = lHigh.ToString("N2"), LOW = lLow.ToString("N2"), Volume = lVolume.ToString("N0"), Change = Convert.ToDouble(lClosing.ToString("N2")) };

                                ///
                                /// Adding Record to ListView
                                ///

                                items.Add(Item);

                                ///
                                /// Saving Record
                                ///

                                int _DbStatus = SavingMarketSummaryClosing(counter,Convert.ToDateTime(_Date), lCategory, lClosing.ToString("N2"), Item);

                                // Saving in Ipams
                                //SavingMarketSummaryClosingIpams(counter, Convert.ToDateTime(_Date), lCategory, lClosing.ToString("N2"), Item);

                                if (_DbStatus == 0)
                                {
                                    Debug.WriteLine("Database Insertion Status: " + _DbStatus + "\n At Name: "+ lName);
                                }

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

        #endregion

        #region wc_DownloadProgressChanged
        // Event to track the progress
        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        #endregion

        #region btnBack_Click

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow(_context);
            window.Show();
            this.Close();
        }

        #endregion

        #region UnzipPSXFile

        public bool UnzipPSXFile(string path)
        {
            bool result = false;
            try
            {
                using (ZipArchive za = ZipFile.OpenRead(path))
                {
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

        #endregion

        #region btnPsxLink_Click

        private void btnPsxLink_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var destinationurl = "https://dps.psx.com.pk/downloads";
                var sInfo = new System.Diagnostics.ProcessStartInfo(destinationurl)
                {
                    UseShellExecute = true,
                };
                System.Diagnostics.Process.Start(sInfo);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Something went wrong..\nDetails: "+ ex.Message, "Operation Failed", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        #endregion

        #region btnSearch_Click

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (_tempItems.Count == 0)
            {
                MessageBox.Show("Please fetch the closing market summary first.", "No Data To Filter..", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                list1.Items.Clear();
                string _searchKeyword = SearchTermTextBox.Text.ToString().ToLower();
                List<MarketSummary> _searchList = new List<MarketSummary>();
                var _selectedItem = from item in _tempItems
                                    where item.Name.ToLower().Contains(_searchKeyword) || item.Symbol.ToLower().Contains(_searchKeyword)
                                    orderby item.Name
                                    select item;
                _searchList = _selectedItem.ToList();
                int counter = 0;
                for (int i = 0; i < _searchList.Count(); i++)
                {
                    list1.Items.Add(new FundMarket { SERIAL = ++counter, NAME = _searchList[i].Name, SYMBOL = _searchList[i].Symbol, LDCP = _searchList[i].LDCP, OPEN = _searchList[i].OPEN, HIGH = _searchList[i].HIGH, LOW = _searchList[i].LOW, CURRENT = _searchList[i].CURRENT, CHANGE = _searchList[i].Change.ToString(), VOLUME = _searchList[i].Volume });
                }
            }
        }

        #endregion

        #region SearchTermTextBox_TextChanged

        private void SearchTermTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            list1.Items.Clear();
            string _searchKeyword = SearchTermTextBox.Text.ToString().ToLower();
            List<MarketSummary> _searchList = new List<MarketSummary>();
            var _selectedItem = from item in _tempItems
                                where item.Name.ToLower().Contains(_searchKeyword) || item.Symbol.ToLower().Contains(_searchKeyword)
                                orderby item.Name
                                select item;
            _searchList = _selectedItem.ToList();
            int counter = 0;
            for (int i = 0; i < _searchList.Count(); i++)
            {
                list1.Items.Add(new FundMarket { SERIAL = ++counter, NAME = _searchList[i].Name, SYMBOL = _searchList[i].Symbol, LDCP = _searchList[i].LDCP, OPEN = _searchList[i].OPEN, HIGH = _searchList[i].HIGH, LOW = _searchList[i].LOW, CURRENT = _searchList[i].CURRENT, CHANGE = _searchList[i].Change.ToString(), VOLUME = _searchList[i].Volume });
            }
        }

        #endregion

        #region btnSave_Click

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            btnSave.IsEnabled = false;
            lblStatus.Text = "Status: Processing";
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = ResourceAccessor.Get("Images/processing.gif");
            image.EndInit();
            ImageBehavior.SetAnimatedSource(imgStatus, image);

            if (_Date != "")
            {
                await Task.Run(() => RunExcel());
            }
            else
            {
                MessageBox.Show("Please fetch the closing market summary first.", "Data Not Found", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            lblStatus.Text = "Status: Ready";
            var image2 = new BitmapImage();
            image2.BeginInit();
            image2.UriSource = ResourceAccessor.Get("Images/tick.gif");
            image2.EndInit();
            ImageBehavior.SetAnimatedSource(imgStatus, image2);
            btnSave.IsEnabled = true;
        }

        #endregion

        #region ClearMarketSummaryClosing

        private int ClearMarketSummaryClosing()
        {
            int status = 0;
            if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("MSSQLSERVER"))
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                conn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("spTRUNCATE_MARKET_SUMMARY_CLOSING", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    status = cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Database Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Debug.WriteLine("SQL Exception: " + ex.Message);
                    status = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Debug.WriteLine("General Exception: " + ex.Message);
                    status = 0;
                }
                finally
                {
                    conn.Close();
                }
            }

            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("ORACLE"))
            {
                OracleConnection conn = new OracleConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                conn.Open();
                try
                {
                    OracleCommand cmd = new OracleCommand("TRUNCATE TABLE MARKET_SUMMARY_CLOSING", conn);
                    cmd.CommandType = CommandType.Text;
                    status = cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Database Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Debug.WriteLine("SQL Exception: " + ex.Message);
                    status = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Debug.WriteLine("General Exception: " + ex.Message);
                    status = 0;
                }
                finally
                {
                    conn.Close();
                }
            }

            #region SQLITE

            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("SQLITE"))
            {

            }

            #endregion

            return status;

        }

        #endregion

        #region SavingMarketSummaryClosing

        public int SavingMarketSummaryClosing(Int64 _id,DateTime _date, Int64 _category, string _closing, MarketSummary summary)
        {
            int status = 0;

            #region SavingMarketSummaryClosing_MSSQLSERVER

            if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("MSSQLSERVER"))
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                conn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("spINSERT_MARKET_SUMMARY_CLOSING", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@MSC_DATE", SqlDbType.DateTime);
                    cmd.Parameters["@MSC_DATE"].Value = _date;
                    cmd.Parameters.Add("@MSC_SYMBOL", SqlDbType.VarChar, 500);
                    cmd.Parameters["@MSC_SYMBOL"].Value = summary.Symbol;
                    cmd.Parameters.Add("@MSC_CATEGORY", SqlDbType.BigInt);
                    cmd.Parameters["@MSC_CATEGORY"].Value = _category;
                    cmd.Parameters.Add("@MSC_NAME", SqlDbType.VarChar, -1);
                    cmd.Parameters["@MSC_NAME"].Value = summary.Name;
                    cmd.Parameters.Add("@MSC_OPEN", SqlDbType.VarChar, -1);
                    cmd.Parameters["@MSC_OPEN"].Value = summary.OPEN;
                    cmd.Parameters.Add("@MSC_HIGH", SqlDbType.VarChar, -1);
                    cmd.Parameters["@MSC_HIGH"].Value = summary.HIGH;
                    cmd.Parameters.Add("@MSC_LOW", SqlDbType.VarChar, -1);
                    cmd.Parameters["@MSC_LOW"].Value = summary.LOW;
                    cmd.Parameters.Add("@MSC_CLOSING", SqlDbType.VarChar, -1);
                    cmd.Parameters["@MSC_CLOSING"].Value = _closing;
                    cmd.Parameters.Add("@MSC_VOLUME", SqlDbType.VarChar, -1);
                    cmd.Parameters["@MSC_VOLUME"].Value = summary.Volume;
                    cmd.Parameters.Add("@MSC_LDCP", SqlDbType.VarChar, -1);
                    cmd.Parameters["@MSC_LDCP"].Value = summary.LDCP;
                    status = cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Database Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Debug.WriteLine("SQL Exception: " + ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Database Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Debug.WriteLine("General Exception: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }

            #endregion

            #region SavingMarketSummaryClosing_ORACLE

            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("ORACLE"))
            {
                OracleConnection conn = new OracleConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                conn.Open();
                try
                {
                    OracleCommand cmd = new OracleCommand("spINSERT_MARKET_SUMMARY_CLOSE", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("ID", OracleDbType.Int64);
                    cmd.Parameters["ID"].Value = _id;
                    cmd.Parameters["ID"].Direction = ParameterDirection.Input;
                    cmd.Parameters.Add("DATE_", OracleDbType.Date);
                    cmd.Parameters["DATE_"].Value = _date;
                    cmd.Parameters["DATE_"].Direction = ParameterDirection.Input;
                    cmd.Parameters.Add("SYMBOL", OracleDbType.Varchar2, 500);
                    cmd.Parameters["SYMBOL"].Value = summary.Symbol;
                    cmd.Parameters["SYMBOL"].Direction = ParameterDirection.Input;
                    cmd.Parameters.Add("CATEGORY", OracleDbType.Int64);
                    cmd.Parameters["CATEGORY"].Value = _category;
                    cmd.Parameters["CATEGORY"].Direction = ParameterDirection.Input;
                    cmd.Parameters.Add("NAME", OracleDbType.Varchar2, 500);
                    cmd.Parameters["NAME"].Value = summary.Name;
                    cmd.Parameters["NAME"].Direction = ParameterDirection.Input;
                    cmd.Parameters.Add("OPEN", OracleDbType.Varchar2, 500);
                    cmd.Parameters["OPEN"].Value = summary.OPEN;
                    cmd.Parameters["OPEN"].Direction = ParameterDirection.Input;
                    cmd.Parameters.Add("HIGH", OracleDbType.Varchar2, 500);
                    cmd.Parameters["HIGH"].Value = summary.HIGH;
                    cmd.Parameters["HIGH"].Direction = ParameterDirection.Input;
                    cmd.Parameters.Add("LOW", OracleDbType.Varchar2, 500);
                    cmd.Parameters["LOW"].Value = summary.LOW;
                    cmd.Parameters["LOW"].Direction = ParameterDirection.Input;
                    cmd.Parameters.Add("CLOSING", OracleDbType.Varchar2, 500);
                    cmd.Parameters["CLOSING"].Value = _closing;
                    cmd.Parameters["CLOSING"].Direction = ParameterDirection.Input;
                    cmd.Parameters.Add("VOLUME", OracleDbType.Varchar2, 500);
                    cmd.Parameters["VOLUME"].Value = summary.Volume;
                    cmd.Parameters["VOLUME"].Direction = ParameterDirection.Input;
                    cmd.Parameters.Add("LDCP", OracleDbType.Varchar2, 500);
                    cmd.Parameters["LDCP"].Value = summary.LDCP;
                    cmd.Parameters["LDCP"].Direction = ParameterDirection.Input;
                    status = cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Database Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Debug.WriteLine("SQL Exception: " + ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Database Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Debug.WriteLine("General Exception: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }

            #endregion

            #region SavingMarketSummaryClosing_SQLITE

            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("SQLITE"))
            {

            }

            #endregion

            return status;

        }

        #endregion

        #region RunExcel

        public void RunExcel()
        {
            ExcelPackage package = null;
            Stream xlFile = null;
            try
            {
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                using (package = new ExcelPackage())
                {

                    //Add a new worksheet to the empty workbook
                    var worksheet = package.Workbook.Worksheets.Add("Market Summary Closing");
                    //Add the headers
                    worksheet.Cells[1, 1].Value = "Id";
                    worksheet.Cells[1, 2].Value = "Date";
                    worksheet.Cells[1, 3].Value = "Symbol";
                    worksheet.Cells[1, 4].Value = "Category";
                    worksheet.Cells[1, 5].Value = "Name";
                    worksheet.Cells[1, 6].Value = "Open";
                    worksheet.Cells[1, 7].Value = "High";
                    worksheet.Cells[1, 8].Value = "Low";
                    worksheet.Cells[1, 9].Value = "Close";
                    worksheet.Cells[1, 10].Value = "Volume";
                    worksheet.Cells[1, 11].Value = "LDCP";

                    //Add some items...
                    List<MarketSummary> items = getMarketSummaryClosing();

                    int total = 1;
                    for (int index = 0; index < items.Count; index++)
                    {
                        total = index + 2;
                        worksheet.Cells["A" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["A" + total].Value = items[index].ID ;
                        worksheet.Cells["B" + total].Value = items[index].Date;
                        worksheet.Cells["C" + total].Value = items[index].Symbol;
                        worksheet.Cells["D" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["D" + total].Value = items[index].Category;
                        //worksheet.Cells["E" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["E" + total].Value = items[index].Name;
                        worksheet.Cells["F" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["F" + total].Value = items[index].OPEN;
                        worksheet.Cells["G" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["G" + total].Value = items[index].HIGH;
                        worksheet.Cells["H" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["H" + total].Value = items[index].LOW;
                        worksheet.Cells["I" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["I" + total].Value = items[index].Closing;
                        worksheet.Cells["J" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["J" + total].Value = items[index].Volume;
                        worksheet.Cells["K" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["K" + total].Value = items[index].LDCP;
                    }

                    //EndTest

                    //Ok now format the values;
                    using (var range = worksheet.Cells[1, 1, 1, 11])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.DarkBlue);
                        range.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    }

                    //Create an autofilter for the range
                    worksheet.Cells["A1:K4"].AutoFilter = true;

                    worksheet.Cells["A2:A4"].Style.Numberformat.Format = "@";   //Format as text

                    //There is actually no need to calculate, Excel will do it for you, but in some cases it might be useful. 
                    //For example if you link to this workbook from another workbook or you will open the workbook in a program that hasn't a calculation engine or 
                    //you want to use the result of a formula in your program.
                    worksheet.Calculate();

                    worksheet.Cells.AutoFitColumns(0);  //Autofit columns for all cells

                    // Lets set the header text 
                    worksheet.HeaderFooter.OddHeader.CenteredText = "&24&U&\"Arial,Regular Bold\" Fund Market Summary";
                    // Add the page number to the footer plus the total number of pages
                    worksheet.HeaderFooter.OddFooter.RightAlignedText =
                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);
                    // Add the sheet name to the footer
                    worksheet.HeaderFooter.OddFooter.CenteredText = ExcelHeaderFooter.SheetName;
                    // Add the file path to the footer
                    worksheet.HeaderFooter.OddFooter.LeftAlignedText = ExcelHeaderFooter.FilePath + ExcelHeaderFooter.FileName;

                    //worksheet.PrinterSettings.RepeatRows = worksheet.Cells["1:2"];
                    //worksheet.PrinterSettings.RepeatColumns = worksheet.Cells["A:I"];

                    // Change the sheet view to show it in page layout mode
                    //worksheet.View.PageLayoutView = true;

                    // Set some document properties
                    package.Workbook.Properties.Title = "Market Summary Closing Details";
                    package.Workbook.Properties.Author = "Saad Ahmed";
                    package.Workbook.Properties.Comments = "This is the psx market summary closing detail report.";

                    // Set some extended property values
                    package.Workbook.Properties.Company = "EPPlus Software AB";

                    // Set some custom property values
                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", "Jan Källman");
                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", "EPPlus");

                    //var xlFile = FileOutputUtil.GetFileInfo("01-GettingStarted.xlsx");
                    string path = "MarketSummaryClosing.xlsx";
                    xlFile = File.Create(path);


                    // Save our new workbook in the output directory and we are done!
                    package.SaveAs(xlFile);
                    //xlFile.Close();

                    //return xlFile.FullName;
                    package.Stream.Close();

                    xlFile.Dispose();
                    package.Dispose();

                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Problem: ", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                Debug.WriteLine("Exception: " + ex.Message);
            }
            finally
            {
                //xlFile.Dispose();
                //package.Dispose();
            }
            Debug.WriteLine("Excel Sheet Created.");
            Thread.Sleep(1000);
            if (File.Exists("MarketSummaryClosing.xlsx"))
            {
                try
                {
                    //using (Stream stream = new FileStream("FundMarketSummary.xlsx", FileMode.Open))
                    //{
                    // File/Stream manipulating code here
                    Process p = new Process();
                    p.StartInfo.UseShellExecute = true;
                    p.StartInfo.FileName = "MarketSummaryClosing.xlsx";
                    p.Start();
                    //}
                }
                catch (Exception ex)
                {
                    //check here why it failed and ask user to retry if the file is in use.
                    MessageBox.Show(ex.Message);
                }

            }

        }

        #endregion

        #region getMarketSummaryClosing

        public List<MarketSummary> getMarketSummaryClosing()
        {
            List<MarketSummary> items = new List<MarketSummary>();

            #region getMarketSummaryClosing_MSSQLSERVER

            if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("MSSQLSERVER"))
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                conn.Open();
                // 1.  create a command object identifying the stored procedure
                SqlCommand cmd = new SqlCommand("spGET_MARKET_SUMMARY_CLOSING", conn);

                // 2. set the command object so it knows to execute a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                // 3. add parameter to command, which will be passed to the stored procedure
                //cmd.Parameters.Add(new SqlParameter("@FUND_NAME", _fundName));

                // execute the command
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    // iterate through results, printing each to console
                    while (rdr.Read())
                    {
                        items.Add(new MarketSummary { ID = Convert.ToInt32(rdr.GetInt64(0)), Date = rdr.GetDateTime(1), Symbol = rdr.GetString(2), Category = rdr.GetInt64(3), Name = rdr.GetString(4), OPEN = rdr.GetString(5), HIGH = rdr.GetString(6), LOW = rdr.GetString(7), Closing = rdr.GetString(8), Volume = rdr.GetString(9), LDCP = rdr.GetString(10) });
                        //Debug.WriteLine("FUND_ID in Default DB: " + FUND_ID);
                    }
                }

                conn.Close();
            }

            #endregion

            #region getMarketSummaryClosing_ORACLE

            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("ORACLE"))
            {
                OracleConnection conn = new OracleConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                conn.Open();
                // 1.  create a command object identifying the stored procedure
                OracleCommand cmd = new OracleCommand("select * from MARKET_SUMMARY_CLOSING;", conn);

                // 2. set the command object so it knows to execute a stored procedure
                cmd.CommandType = CommandType.Text;

                // 3. add parameter to command, which will be passed to the stored procedure
                //cmd.Parameters.Add(new SqlParameter("@FUND_NAME", _fundName));

                // execute the command
                using (OracleDataReader rdr = cmd.ExecuteReader())
                {
                    // iterate through results, printing each to console
                    while (rdr.Read())
                    {
                        items.Add(new MarketSummary { ID = Convert.ToInt32(rdr.GetInt64(0)), Date = rdr.GetDateTime(1), Symbol = rdr.GetString(2), Category = rdr.GetInt64(3), Name = rdr.GetString(4), OPEN = rdr.GetString(5), HIGH = rdr.GetString(6), LOW = rdr.GetString(7), Closing = rdr.GetString(8), Volume = rdr.GetString(9), LDCP = rdr.GetString(10) });
                        //Debug.WriteLine("FUND_ID in Default DB: " + FUND_ID);
                    }
                }

                conn.Close();
            }

            #endregion

            #region getMarketSummaryClosing_SQLITE

            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("SQLITE"))
            {

            }

            #endregion

            return items;
        }

        #endregion

        #region datepsxpicker_DateValidationError

        private void datepsxpicker_DateValidationError(object sender, DatePickerDateValidationErrorEventArgs e)
        {
            MessageBox.Show("Please select a valid date first..", "No Valid Date Selected!", MessageBoxButton.OK, MessageBoxImage.Hand);
        }

        #endregion

        #region ClearMarketSummaryClosing

        private int ClearMarketSummaryClosingIpams()
        {
            int status = 0;
            if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("MSSQLSERVER"))
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["IpamsConnection"].ConnectionString;
                conn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("spTRUNCATE_MARKET_SUMMARY_CLOSING_IPAMS", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    status = cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Database Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Debug.WriteLine("SQL Exception: " + ex.Message);
                    status = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Debug.WriteLine("General Exception: " + ex.Message);
                    status = 0;
                }
                finally
                {
                    conn.Close();
                }
            }

            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("ORACLE"))
            {
                OracleConnection conn = new OracleConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["IpamsConnection"].ConnectionString;
                conn.Open();
                try
                {
                    OracleCommand cmd = new OracleCommand("TRUNCATE TABLE MARKET_SUMMARY_CLOSING_IPAMS", conn);
                    cmd.CommandType = CommandType.Text;
                    status = cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Database Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Debug.WriteLine("SQL Exception: " + ex.Message);
                    status = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Debug.WriteLine("General Exception: " + ex.Message);
                    status = 0;
                }
                finally
                {
                    conn.Close();
                }
            }

            #region SQLITE

            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("SQLITE"))
            {

            }

            #endregion

            return status;

        }

        #endregion

        #region SavingMarketSummaryClosingIpams

        public int SavingMarketSummaryClosingIpams(Int64 _id, DateTime _date, Int64 _category, string _closing, MarketSummary summary)
        {
            int status = 0;

            #region SavingMarketSummaryClosingIpams_MSSQLSERVER

            if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("MSSQLSERVER"))
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["IpamsConnection"].ConnectionString;

                conn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("spINSERT_MARKET_SUMMARY_CLOSING_IPAMS", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@MSC_DATE", SqlDbType.DateTime);
                    cmd.Parameters["@MSC_DATE"].Value = _date;
                    cmd.Parameters.Add("@MSC_SYMBOL", SqlDbType.VarChar, 500);
                    cmd.Parameters["@MSC_SYMBOL"].Value = summary.Symbol;
                    cmd.Parameters.Add("@MSC_CATEGORY", SqlDbType.VarChar, -1);
                    cmd.Parameters["@MSC_CATEGORY"].Value = _category.ToString();
                    cmd.Parameters.Add("@MSC_NAME", SqlDbType.VarChar, -1);
                    cmd.Parameters["@MSC_NAME"].Value = summary.Name;
                    cmd.Parameters.Add("@MSC_OPEN", SqlDbType.VarChar, -1);
                    cmd.Parameters["@MSC_OPEN"].Value = summary.OPEN;
                    cmd.Parameters.Add("@MSC_HIGH", SqlDbType.VarChar, -1);
                    cmd.Parameters["@MSC_HIGH"].Value = summary.HIGH;
                    cmd.Parameters.Add("@MSC_LOW", SqlDbType.VarChar, -1);
                    cmd.Parameters["@MSC_LOW"].Value = summary.LOW;
                    cmd.Parameters.Add("@MSC_CLOSING", SqlDbType.VarChar, -1);
                    cmd.Parameters["@MSC_CLOSING"].Value = _closing;
                    cmd.Parameters.Add("@MSC_VOLUME", SqlDbType.VarChar, -1);
                    cmd.Parameters["@MSC_VOLUME"].Value = summary.Volume;
                    cmd.Parameters.Add("@MSC_LDCP", SqlDbType.VarChar, -1);
                    cmd.Parameters["@MSC_LDCP"].Value = summary.LDCP;
                    status = cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Database Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Debug.WriteLine("SQL Exception: " + ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Database Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Debug.WriteLine("General Exception: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }

            #endregion

            #region SavingMarketSummaryClosingIpams_ORACLE

            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("ORACLE"))
            {
                OracleConnection conn = new OracleConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["IpamsConnection"].ConnectionString;

                conn.Open();
                try
                {
                    OracleCommand cmd = new OracleCommand("spINSERT_MARKET_SUMMARY_CLOSE_IPAMS", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("ID", OracleDbType.Int64);
                    cmd.Parameters["ID"].Value = _id;
                    cmd.Parameters["ID"].Direction = ParameterDirection.Input;
                    cmd.Parameters.Add("DATE_", OracleDbType.Date);
                    cmd.Parameters["DATE_"].Value = _date;
                    cmd.Parameters["DATE_"].Direction = ParameterDirection.Input;
                    cmd.Parameters.Add("SYMBOL", OracleDbType.Varchar2, 500);
                    cmd.Parameters["SYMBOL"].Value = summary.Symbol;
                    cmd.Parameters["SYMBOL"].Direction = ParameterDirection.Input;
                    cmd.Parameters.Add("CATEGORY", OracleDbType.Varchar2, 500);
                    cmd.Parameters["CATEGORY"].Value = _category.ToString();
                    cmd.Parameters["CATEGORY"].Direction = ParameterDirection.Input;
                    cmd.Parameters.Add("NAME", OracleDbType.Varchar2, 500);
                    cmd.Parameters["NAME"].Value = summary.Name;
                    cmd.Parameters["NAME"].Direction = ParameterDirection.Input;
                    cmd.Parameters.Add("OPEN", OracleDbType.Varchar2, 500);
                    cmd.Parameters["OPEN"].Value = summary.OPEN;
                    cmd.Parameters["OPEN"].Direction = ParameterDirection.Input;
                    cmd.Parameters.Add("HIGH", OracleDbType.Varchar2, 500);
                    cmd.Parameters["HIGH"].Value = summary.HIGH;
                    cmd.Parameters["HIGH"].Direction = ParameterDirection.Input;
                    cmd.Parameters.Add("LOW", OracleDbType.Varchar2, 500);
                    cmd.Parameters["LOW"].Value = summary.LOW;
                    cmd.Parameters["LOW"].Direction = ParameterDirection.Input;
                    cmd.Parameters.Add("CLOSING", OracleDbType.Varchar2, 500);
                    cmd.Parameters["CLOSING"].Value = _closing;
                    cmd.Parameters["CLOSING"].Direction = ParameterDirection.Input;
                    cmd.Parameters.Add("VOLUME", OracleDbType.Varchar2, 500);
                    cmd.Parameters["VOLUME"].Value = summary.Volume;
                    cmd.Parameters["VOLUME"].Direction = ParameterDirection.Input;
                    cmd.Parameters.Add("LDCP", OracleDbType.Varchar2, 500);
                    cmd.Parameters["LDCP"].Value = summary.LDCP;
                    cmd.Parameters["LDCP"].Direction = ParameterDirection.Input;
                    status = cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Database Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Debug.WriteLine("SQL Exception: " + ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Database Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Debug.WriteLine("General Exception: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }

            #endregion

            #region SavingMarketSummaryClosingIpams_SQLITE

            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("SQLITE"))
            {

            }

            #endregion

            return status;

        }

        #endregion

    }
}
