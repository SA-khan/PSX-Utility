using HtmlAgilityPack;
using Microsoft.Data.Sqlite;
using Npgsql;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Oracle.ManagedDataAccess.Client;
using PSXDataFetchingApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using WpfAnimatedGif;

namespace PSXDataFetchingApp
{
    /// <summary>
    /// Interaction logic for PreviewWindow.xaml
    /// </summary>
    public partial class PreviewWindow : Window
    {

        //15/10/2020
        DateTime ExpiryDate = DateTime.Parse("2025/03/25 15:17:00");

        public DataContext _context;

        //Get Time Reset Interval
        public static decimal Interval = Convert.ToDecimal(ConfigurationManager.AppSettings["Interval"]);

        //Market Date, Status and other Variable Declarations
        public Dictionary<string, string> _miscellenousData = new Dictionary<string, string>(){
                                                            {"DATE", ""},
                                                            {"STATUS", ""},
                                                            {"VOLUME", ""},
                                                            {"TRADES", ""},
                                                            {"VALUE", ""}
                                                        };

        //Main Scrip List Variable
        public static List<CurrentMarketSummary> _scrip = new List<CurrentMarketSummary>();

        // Web Data Variable
        public HtmlNodeCollection _webDataCollection = null;

        public List<string> Exceptions = new List<string>();

        //List Categories
        public List<string> _categoryList = new List<string>() { "", "AUTOMOBILE ASSEMBLER", "AUTOMOBILE PARTS & ACCESSORIES", "CABLE & ELECTRICAL GOODS", "CEMENT", "CHEMICAL", "CLOSE - END MUTUAL FUND", "COMMERCIAL BANKS", "ENGINEERING", "FERTILIZER", "FOOD & PERSONAL CARE PRODUCTS", "GLASS & CERAMICS", "INSURANCE", "INV. BANKS / INV. COS. / SECURITIES COS.", "LEASING COMPANIES", "LEATHER & TANNERIES", "MISCELLANEOUS", "MODARABAS", "OIL & GAS EXPLORATION COMPANIES", "OIL & GAS MARKETING COMPANIES", "PAPER & BOARD", "PHARMACEUTICALS", "POWER GENERATION & DISTRIBUTION", "REFINERY", "SUGAR & ALLIED INDUSTRIES", "SYNTHETIC & RAYON", "TECHNOLOGY & COMMUNICATION", "TEXTILE COMPOSITE", "TEXTILE SPINNING", "TEXTILE WEAVING", "TRANSPORT", "VANASPATI & ALLIED INDUSTRIES", "WOOLLEN", "REAL ESTATE INVESTMENT TRUST", "EXCHANGE TRADED FUNDS", "FUTURE CONTRACTS" };
        //public static List<string> _categoryList = new List<string>();
        public List<string> _categoryList2 = new List<string>() { "AUTOMOBILE ASSEMBLER", "AUTOMOBILE PARTS & ACCESSORIES", "CABLE & ELECTRICAL GOODS", "CEMENT", "CHEMICAL", "CLOSE - END MUTUAL FUND", "COMMERCIAL BANKS", "ENGINEERING", "FERTILIZER", "FOOD & PERSONAL CARE PRODUCTS", "GLASS & CERAMICS", "INSURANCE", "INV. BANKS / INV. COS. / SECURITIES COS.", "LEASING COMPANIES", "LEATHER & TANNERIES", "MISCELLANEOUS", "MODARABAS", "OIL & GAS EXPLORATION COMPANIES", "OIL & GAS MARKETING COMPANIES", "PAPER & BOARD", "PHARMACEUTICALS", "POWER GENERATION & DISTRIBUTION", "REFINERY", "SUGAR & ALLIED INDUSTRIES", "SYNTHETIC & RAYON", "TECHNOLOGY & COMMUNICATION", "TEXTILE COMPOSITE", "TEXTILE SPINNING", "TEXTILE WEAVING", "TOBACCO", "TRANSPORT", "VANASPATI & ALLIED INDUSTRIES", "WOOLLEN", "REAL ESTATE INVESTMENT TRUST", "EXCHANGE TRADED FUNDS", "FUTURE CONTRACTS" };

        public static bool isDataSaved = false;
        public static DateTime RequestDate = DateTime.Now;
        public static string RequestStatus = String.Empty;
        public static double RequestValue = 0;
        public static double RequestVolume = 0;
        public static double RequestTrades = 0;
        public static List<string> NAME = new List<string>();
        public static List<string> SYMBOL = new List<string>();
        public static List<Double> LDCP = new List<double>();
        public static List<Double> OPEN = new List<double>();
        public static List<Double> HIGH = new List<double>();
        public static List<Double> LOW = new List<double>();
        public static List<Double> CURRENT = new List<double>();
        public static List<Double> CHANGE = new List<double>();
        public static List<Double> VOLUME = new List<double>();
        public static int statusFlag = 0;
        public static string statusContent = "";
        private BackgroundWorker worker = new BackgroundWorker();
        List<string> defaultData = new List<string>();
        public delegate void MyDelegate();
    

        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        int countTimer = 0;


        #region ClientSideProperties

        public void ClientSideProperties()
        {
            //Client Specific Properties
            try
            {
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["Client"]))
                {
                    #region ClientSideProperties_General

                    if (ConfigurationManager.AppSettings["Client"].Equals("General"))
                    {
                        // Header Background Color 
                        var bc = new BrushConverter();
                        //CriteriaSection.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#b9c9d3");
                        header.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#76b0cc");
                        Footer.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#76b0cc");

                        //Setting Logo
                        var image = new BitmapImage();
                        image.BeginInit();
                        image.UriSource = ResourceAccessor.Get("Images/astech.png");
                        image.EndInit();
                        ImageBehavior.SetAnimatedSource(imgClient, image);
                    }

                    #endregion

                    #region ClientSideProperties_BOP

                    else if (ConfigurationManager.AppSettings["Client"].Equals("BOP"))
                    {
                        // Header Background Color 
                        var bc = new BrushConverter();
                        header.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#f0a500");
                        Footer.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#f0a500");

                        //Setting Logo
                        var image = new BitmapImage();
                        image.BeginInit();
                        image.UriSource = ResourceAccessor.Get("Images/BOP.gif");
                        image.EndInit();
                        ImageBehavior.SetAnimatedSource(imgClient, image);
                    }

                    #endregion

                    #region ClientSideProperties_HBL

                    else if (ConfigurationManager.AppSettings["Client"].Equals("HBL"))
                    {
                        // Header Background Color
                        var bc = new BrushConverter();
                        header.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#008269");
                        Footer.Background = (Brush)bc.ConvertFrom("#008269");

                        //Setting Logo
                        var image = new BitmapImage();
                        image.BeginInit();
                        image.UriSource = ResourceAccessor.Get("Images/HBL.gif");
                        image.EndInit();
                        ImageBehavior.SetAnimatedSource(imgClient, image);
                    }

                    #endregion

                    #region ClientSideProperties_EFU

                    if (ConfigurationManager.AppSettings["Client"].Equals("EFU"))
                    {
                        // Header Background Color 
                        var bc = new BrushConverter();
                        header.Background = (Brush)bc.ConvertFrom("#01808d");
                        Footer.Background = (Brush)bc.ConvertFrom("#01808d");

                        //Setting Logo
                        var image = new BitmapImage();
                        image.BeginInit();
                        image.UriSource = ResourceAccessor.Get("Images/efu.png");
                        image.EndInit();
                        ImageBehavior.SetAnimatedSource(imgClient, image);
                    }

                    #endregion
                }
            }
            catch { }
        }

        #endregion

        public PreviewWindow(string date, string status, string Volume, string Value, string Trades, List<CurrentMarketSummary> ScripList)
        {
            InitializeComponent();
            Debug.WriteLine("Preview Screen Initialized..");
            
            ClientSideProperties();

            _miscellenousData["DATE"] = DateTime.Parse(date).ToString("dddd, dd MMMM yyyy hh:mm tt");
            _miscellenousData["STATUS"] = status.ToString().ToUpper();
            _miscellenousData["VOLUME"] = Convert.ToDouble(Volume).ToString("#,##0");
            _miscellenousData["VALUE"] = Convert.ToDouble(Value).ToString("#,##0");
            _miscellenousData["TRADES"] = Convert.ToDouble(Trades).ToString("#,##0");

            lblDate.Text += _miscellenousData["DATE"];
            lblStatus.Text += _miscellenousData["STATUS"];
            lblVolume.Text += Convert.ToDouble(_miscellenousData["VOLUME"]).ToString("#,##0");
            lblValue.Text += Convert.ToDouble(_miscellenousData["VALUE"]).ToString("#,##0");
            lblTrades.Text += Convert.ToDouble(_miscellenousData["TRADES"]).ToString("#,##0");

            if (ScripList.Count > 0)
            {
                List<string> categoryList = new List<string>();
                categoryList.Add("All Categories");
                comboCategory.SelectedIndex = 0;
                categoryList.Add(ScripList[0].Category.Trim());
                string tempcategory = ScripList[0].Category;
                Truncate_CURRENT_MARKET_OVERVIEW_DB();
                INSERT_CURRENT_MARKET_OVERVIEW_DB(_miscellenousData["DATE"], _miscellenousData["STATUS"], _miscellenousData["VOLUME"], _miscellenousData["VALUE"], _miscellenousData["TRADES"]);
                Truncate_CURRENT_MARKET_SUMMARY_DB();
                int total = 1;
                for (int i = 0; i < ScripList.Count; i++)
                {
                    if (ScripList[i].Volume.Trim() == "CHANGE") { }
                    else
                    {
                        if (ScripList[i].Category != tempcategory)
                        {
                            categoryList.Add(ScripList[i].Category.Trim());
                            tempcategory = ScripList[i].Category;
                        }
                        list1.Items.Add(ScripList[i]);
                        _scrip.Add(new CurrentMarketSummary { CurrentMarketSummaryId = total++, Name = ScripList[i].Name, Symbol = ScripList[i].Symbol, Category = ScripList[i].Category, Ldcp = ScripList[i].Ldcp, Open = ScripList[i].Open, High = ScripList[i].High, Low = ScripList[i].Low, Current = ScripList[i].Current, Change = ScripList[i].Change, Volume = ScripList[i].Volume });
                        SavingScrip(_miscellenousData["DATE"], _miscellenousData["STATUS"], _miscellenousData["VOLUME"], _miscellenousData["VALUE"], _miscellenousData["TRADES"], ScripList[i]);
                    }
                }
                comboCategory.ItemsSource = categoryList;
            }

            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

        }



        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            countTimer++;
            if (countTimer % Interval == 0)
            {
                ButtonAutomationPeer peer = new ButtonAutomationPeer(btnRefresh);
                IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                invokeProv.Invoke();
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = ResourceAccessor.Get("Images/gear.gif");
            image.EndInit();
            ImageBehavior.SetAnimatedSource(imgStatus, image);
            progressBarContainer.Visibility = Visibility.Visible;
            lblStatusMessage.Text = "Status: Refreshing Data..";

            Application.Current.Dispatcher.Invoke((Action)delegate {
                MyDelegate mdelegate = new MyDelegate(mustWork);
                Thread t = new Thread(mdelegate.Invoke);
                t.SetApartmentState(ApartmentState.STA);
                t.Start();
            });

            progressBarContainer.Visibility = Visibility.Hidden;
            var image2 = new BitmapImage();
            image2.BeginInit();
            image2.UriSource = ResourceAccessor.Get("Images/tick.gif");
            image2.EndInit();
            ImageBehavior.SetAnimatedSource(imgStatus, image2);
            lblStatusMessage.Text = "Status: Ready";

        }

        #region worker_process

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
             mustWork();
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            lblStatusMessage.Text = statusContent;
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        #endregion

        #region btnBack_Click

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow(_context);
            window.Show();
            this.Hide();
        }

        #endregion

        #region btnExport_Click

        private async void btnExport_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() => RunExcel());
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
                    var worksheet = package.Workbook.Worksheets.Add("Pakistan Stock Exchange Market Summary");
                    //Add the headers
                    worksheet.Cells[1, 1].Value = "Sr. No.";
                    worksheet.Cells[1, 2].Value = "SECTOR";
                    worksheet.Cells[1, 3].Value = "DATE";
                    worksheet.Cells[1, 4].Value = "STATUS";
                    worksheet.Cells[1, 5].Value = "SCRIP";
                    worksheet.Cells[1, 6].Value = "SYMBOL";
                    worksheet.Cells[1, 7].Value = "LDCP";
                    worksheet.Cells[1, 8].Value = "OPEN";
                    worksheet.Cells[1, 9].Value = "HIGH";
                    worksheet.Cells[1, 10].Value = "LOW";
                    worksheet.Cells[1, 11].Value = "CURRENT";
                    worksheet.Cells[1, 12].Value = "CHANGE";
                    worksheet.Cells[1, 13].Value = "VOLUME";

                    //Add some items...
                    int countFlag = 1;
                    int total = 1;
                    for (int index = 0; index < _scrip.Count; index++)
                    {
                        total = index + 2;

                        worksheet.Cells["A" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["A" + total].Value =  countFlag++;
                        worksheet.Cells["B" + total].Value = _scrip[index].Category;
                        worksheet.Cells["C" + total].Value = _miscellenousData["DATE"];
                        worksheet.Cells["D" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["D" + total].Value = _miscellenousData["DATE"];
                        worksheet.Cells["E" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        worksheet.Cells["E" + total].Value = _scrip[index].Name;
                        worksheet.Cells["F" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        worksheet.Cells["F" + total].Value = _scrip[index].Symbol;
                        worksheet.Cells["G" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["G" + total].Value = _scrip[index].Ldcp;
                        worksheet.Cells["H" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["H" + total].Value = _scrip[index].Open;
                        worksheet.Cells["I" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["I" + total].Value = _scrip[index].High;
                        worksheet.Cells["J" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["J" + total].Value = _scrip[index].Low;
                        worksheet.Cells["K" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["K" + total].Value = _scrip[index].Current;
                        worksheet.Cells["L" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["L" + total].Value = _scrip[index].Change;
                        worksheet.Cells["M" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["M" + total].Value = _scrip[index].Volume;
                    }

                    //EndTest

                    //Ok now format the values;
                    using (var range = worksheet.Cells[1, 1, 1, 13])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.DarkBlue);
                        range.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    }

                    //Create an autofilter for the range
                    worksheet.Cells["A1:M4"].AutoFilter = true;

                    worksheet.Cells["A2:A2"].Style.Numberformat.Format = "@";   //Format as text
                    worksheet.Cells["C2:C3000"].Style.Numberformat.Format = "HH:mm am/pm MMMM dd, yyyy";

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
                    package.Workbook.Properties.Title = "Pakistan Stock Exchange Market Summary";
                    package.Workbook.Properties.Author = "Saad Ahmed";
                    package.Workbook.Properties.Comments = "This is the Pakistan Stock Exchange Market Summary Export File.";

                    // Set some extended property values
                    package.Workbook.Properties.Company = "EPPlus Software AB";

                    // Set some custom property values
                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", "Saad Ahmed");
                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", "EPPlus");

                    //var xlFile = FileOutputUtil.GetFileInfo("01-GettingStarted.xlsx");
                    string path = "PSX_MarketSummary_"+DateTime.Now.Year+DateTime.Now.Month+DateTime.Now.Day.ToString("00")+".xlsx";
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
                xlFile.Dispose();
                package.Dispose();
            }
            Debug.WriteLine("Excel Sheet Created.");
            Thread.Sleep(2000);
            if (File.Exists("PSX_MarketSummary_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day.ToString("00") + ".xlsx"))
            {
                try
                {
                    Process p = new Process();
                    p.StartInfo.UseShellExecute = true;
                    p.StartInfo.FileName = "PSX_MarketSummary_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day.ToString("00") + ".xlsx";
                    p.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

        }

        #endregion

        #region Truncate_CURRENT_MARKET_SUMMARY_DB

        private int Truncate_CURRENT_MARKET_SUMMARY_DB()
        {
            #region Truncate_CURRENT_MARKET_SUMMARY_DB_MSSQLSERVER
            if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("MSSQLSERVER"))
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                conn.Open();
                int isDataCleared = 0;
                //Clear Truncate Current Market Summary Data
                SqlCommand cmd = new SqlCommand("spTRUNCATE_CURRENT_MARKET_SUMMARY", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    isDataCleared = cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine("SQL Exception in SavingDataToDatabase Method: " + ex.Message);
                    Exceptions.Add("SQL Exception in SavingDataToDatabase Method: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("SQL Exception in SavingDataToDatabase Method: " + ex.Message);
                    Exceptions.Add("SQL Exception in SavingDataToDatabase Method: " + ex.Message);
                }
                conn.Close();
                return isDataCleared;
            }

            #endregion

            #region Truncate_CURRENT_MARKET_SUMMARY_DB_ORACLE
            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("ORACLE"))
            {
                OracleConnection conn = new OracleConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                conn.Open();
                int isDataCleared = 0;
                //Clear Truncate Current Market Summary Data
                OracleCommand cmd = new OracleCommand("TRUNCATE TABLE TRUNCATE_CURRENT_MARKET_SUMMARY;", conn);
                cmd.CommandType = CommandType.Text;
                try
                {
                    isDataCleared = cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine("Oracle Exception in SavingDataToDatabase Method: " + ex.Message);
                    Exceptions.Add("Oracle Exception in SavingDataToDatabase Method: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("SQL Exception in SavingDataToDatabase Method: " + ex.Message);
                    Exceptions.Add("SQL Exception in SavingDataToDatabase Method: " + ex.Message);
                }
                conn.Close();
                return isDataCleared;
            }
            #endregion

            #region Truncate_CURRENT_MARKET_SUMMARY_DB_SQLITE
            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("SQLITE"))
            {
                SqliteConnection conn = new SqliteConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                conn.Open();
                int isDataCleared = 0;
                //Clear Truncate Current Market Summary Data
                SqliteCommand cmd = new SqliteCommand("TRUNCATE TABLE TRUNCATE_CURRENT_MARKET_SUMMARY;", conn);
                cmd.CommandType = CommandType.Text;
                try
                {
                    isDataCleared = cmd.ExecuteNonQuery();
                }
                catch (SqliteException ex)
                {
                    Debug.WriteLine("SQLite Exception in SavingDataToDatabase Method: " + ex.Message);
                    Exceptions.Add("SQLite Exception in SavingDataToDatabase Method: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("SQL Exception in SavingDataToDatabase Method: " + ex.Message);
                    Exceptions.Add("SQL Exception in SavingDataToDatabase Method: " + ex.Message);
                }
                conn.Close();
                return isDataCleared;
            }
            #endregion

            #region Truncate_CURRENT_MARKET_SUMMARY_DB_POSTGRE
            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("POSTGRE"))
            {
                NpgsqlConnection conn = new NpgsqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                conn.Open();
                int isDataCleared = 0;
                //Clear Truncate Current Market Summary Data
                NpgsqlCommand cmd = new NpgsqlCommand("TRUNCATE TABLE TRUNCATE_CURRENT_MARKET_SUMMARY;", conn);
                cmd.CommandType = CommandType.Text;
                try
                {
                    isDataCleared = cmd.ExecuteNonQuery();
                }
                catch (NpgsqlException ex)
                {
                    Debug.WriteLine("SQLite Exception in SavingDataToDatabase Method: " + ex.Message);
                    Exceptions.Add("SQLite Exception in SavingDataToDatabase Method: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("SQL Exception in SavingDataToDatabase Method: " + ex.Message);
                    Exceptions.Add("SQL Exception in SavingDataToDatabase Method: " + ex.Message);
                }
                conn.Close();
                return isDataCleared;
            }
            #endregion

            #region Truncate_CURRENT_MARKET_SUMMARY_DB_Elasticsearch
            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("ELASTICSEARCH"))
            {
                //NpgsqlConnection conn = new NpgsqlConnection();
                //conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                //conn.Open();
                int isDataCleared = 0;
                //Clear Truncate Current Market Summary Data
                //NpgsqlCommand cmd = new NpgsqlCommand("TRUNCATE TABLE TRUNCATE_CURRENT_MARKET_SUMMARY;", conn);
                //cmd.CommandType = CommandType.Text;
                //try
                //{
                //    isDataCleared = cmd.ExecuteNonQuery();
                //}
                //catch (NpgsqlException ex)
                //{
                //    Debug.WriteLine("SQLite Exception in SavingDataToDatabase Method: " + ex.Message);
                //    Exceptions.Add("SQLite Exception in SavingDataToDatabase Method: " + ex.Message);
                //}
                //catch (Exception ex)
                //{
                //    Debug.WriteLine("SQL Exception in SavingDataToDatabase Method: " + ex.Message);
                //    Exceptions.Add("SQL Exception in SavingDataToDatabase Method: " + ex.Message);
                //}
                //conn.Close();
                return isDataCleared;
            }
            #endregion

            #region ELSE

            else
            {
                return 0;
            }

            #endregion

        }

        #endregion

        #region Truncate_CURRENT_MARKET_OVERVIEW_DB

        private int Truncate_CURRENT_MARKET_OVERVIEW_DB()
        {
            #region Truncate_CURRENT_MARKET_OVERVIEW_DB_MSSQLSERVER
            if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("MSSQLSERVER"))
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                conn.Open();
                int isDataCleared = 0;
                //Clear Truncate Current Market Summary Data
                SqlCommand cmd = new SqlCommand("spTRUNCATE_CURRENT_MARKET_OVERVIEW", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    isDataCleared = cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine("SQL Exception in SavingDataToDatabase Method: " + ex.Message);
                    Exceptions.Add("SQL Exception in SavingDataToDatabase Method: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("SQL Exception in SavingDataToDatabase Method: " + ex.Message);
                    Exceptions.Add("SQL Exception in SavingDataToDatabase Method: " + ex.Message);
                }
                conn.Close();
                return isDataCleared;
            }

            #endregion

            #region SavingDataToDatabase_ORACLE
            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("ORACLE"))
            {
                OracleConnection conn = new OracleConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                conn.Open();
                int isDataCleared = 0;
                //Clear Truncate Current Market Summary Data
                OracleCommand cmd = new OracleCommand("TRUNCATE TABLE TRUNCATE_CURRENT_MARKET_SUMMARY;", conn);
                cmd.CommandType = CommandType.Text;
                try
                {
                    isDataCleared = cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine("Oracle Exception in SavingDataToDatabase Method: " + ex.Message);
                    Exceptions.Add("Oracle Exception in SavingDataToDatabase Method: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("SQL Exception in SavingDataToDatabase Method: " + ex.Message);
                    Exceptions.Add("SQL Exception in SavingDataToDatabase Method: " + ex.Message);
                }
                conn.Close();
                return isDataCleared;
            }
            #endregion

            #region SavingDataToDatabase_SQLITE
            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("SQLITE"))
            {
                SqliteConnection conn = new SqliteConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                conn.Open();
                int isDataCleared = 0;
                //Clear Truncate Current Market Summary Data
                SqliteCommand cmd = new SqliteCommand("TRUNCATE TABLE TRUNCATE_CURRENT_MARKET_SUMMARY;", conn);
                cmd.CommandType = CommandType.Text;
                try
                {
                    isDataCleared = cmd.ExecuteNonQuery();
                }
                catch (SqliteException ex)
                {
                    Debug.WriteLine("SQLite Exception in SavingDataToDatabase Method: " + ex.Message);
                    Exceptions.Add("SQLite Exception in SavingDataToDatabase Method: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("SQL Exception in SavingDataToDatabase Method: " + ex.Message);
                    Exceptions.Add("SQL Exception in SavingDataToDatabase Method: " + ex.Message);
                }
                conn.Close();
                return isDataCleared;
            }
            #endregion

            #region SavingDataToDatabase_POSTGRE
            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("POSTGRE"))
            {
                NpgsqlConnection conn = new NpgsqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                conn.Open();
                int isDataCleared = 0;
                //Clear Truncate Current Market Summary Data
                NpgsqlCommand cmd = new NpgsqlCommand("TRUNCATE TABLE TRUNCATE_CURRENT_MARKET_SUMMARY;", conn);
                cmd.CommandType = CommandType.Text;
                try
                {
                    isDataCleared = cmd.ExecuteNonQuery();
                }
                catch (NpgsqlException ex)
                {
                    Debug.WriteLine("SQLite Exception in SavingDataToDatabase Method: " + ex.Message);
                    Exceptions.Add("SQLite Exception in SavingDataToDatabase Method: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("SQL Exception in SavingDataToDatabase Method: " + ex.Message);
                    Exceptions.Add("SQL Exception in SavingDataToDatabase Method: " + ex.Message);
                }
                conn.Close();
                return isDataCleared;
            }
            #endregion

            #region SavingDataToDatabase_Elasticsearch
            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("ELASTICSEARCH"))
            {
                //NpgsqlConnection conn = new NpgsqlConnection();
                //conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                //conn.Open();
                int isDataCleared = 0;
                //Clear Truncate Current Market Summary Data
                //NpgsqlCommand cmd = new NpgsqlCommand("TRUNCATE TABLE TRUNCATE_CURRENT_MARKET_SUMMARY;", conn);
                //cmd.CommandType = CommandType.Text;
                //try
                //{
                //    isDataCleared = cmd.ExecuteNonQuery();
                //}
                //catch (NpgsqlException ex)
                //{
                //    Debug.WriteLine("SQLite Exception in SavingDataToDatabase Method: " + ex.Message);
                //    Exceptions.Add("SQLite Exception in SavingDataToDatabase Method: " + ex.Message);
                //}
                //catch (Exception ex)
                //{
                //    Debug.WriteLine("SQL Exception in SavingDataToDatabase Method: " + ex.Message);
                //    Exceptions.Add("SQL Exception in SavingDataToDatabase Method: " + ex.Message);
                //}
                //conn.Close();
                return isDataCleared;
            }
            #endregion

            #region ELSE

            else
            {
                return 0;
            }

            #endregion

        }

        #endregion

        #region INSERT_CURRENT_MARKET_OVERVIEW_DB

        private int INSERT_CURRENT_MARKET_OVERVIEW_DB(string _date, string _status, string _volume, string _value, string _trade)
        {
            #region INSERT_CURRENT_MARKET_OVERVIEW_DB_MSSQLSERVER
            if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("MSSQLSERVER"))
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                conn.Open();
                int isMetaDataSaved = 0;
                SqlCommand cmdspInsertMarketSummaryOverview = new SqlCommand("spINSERT_CURRENT_MARKET_OVERVIEW", conn);
                cmdspInsertMarketSummaryOverview.CommandType = CommandType.StoredProcedure;
                cmdspInsertMarketSummaryOverview.Parameters.Add("@DATE", SqlDbType.DateTime);
                cmdspInsertMarketSummaryOverview.Parameters["@DATE"].Value = Convert.ToDateTime(_date);
                cmdspInsertMarketSummaryOverview.Parameters.Add("@STATUS", SqlDbType.VarChar, 300);
                cmdspInsertMarketSummaryOverview.Parameters["@STATUS"].Value = _status;
                cmdspInsertMarketSummaryOverview.Parameters.Add("@VOLUME", SqlDbType.Float);
                cmdspInsertMarketSummaryOverview.Parameters["@VOLUME"].Value = Convert.ToDouble(_volume.Replace(",", ""));
                cmdspInsertMarketSummaryOverview.Parameters.Add("@VALUE", SqlDbType.Float);
                cmdspInsertMarketSummaryOverview.Parameters["@VALUE"].Value = Convert.ToDouble(_value.Replace(",", ""));
                cmdspInsertMarketSummaryOverview.Parameters.Add("@TRADE", SqlDbType.Float);
                cmdspInsertMarketSummaryOverview.Parameters["@TRADE"].Value = Convert.ToDouble(_trade.Replace(",", ""));
                try
                {
                    isMetaDataSaved = cmdspInsertMarketSummaryOverview.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine("SQL Exception in SavingScrip Method: " + ex.Message);
                    Exceptions.Add("SQL Exception in SavingScrip Method: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Exception in SavingScrip Method: " + ex.Message);
                    Exceptions.Add("Exception in SavingScrip Method: " + ex.Message);
                }
                conn.Close();

                return isMetaDataSaved;
            }

            #endregion

            #region INSERT_CURRENT_MARKET_OVERVIEW_DB_ORACLE
            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("ORACLE"))
            {
                OracleConnection conn = new OracleConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                conn.Open();
                int isMetaDataSaved = 0;
                OracleCommand cmdspInsertMarketSummaryOverview = new OracleCommand("spINSERT_CURRENT_MARKET_OVERVIEW", conn);
                cmdspInsertMarketSummaryOverview.CommandType = CommandType.StoredProcedure;
                cmdspInsertMarketSummaryOverview.Parameters.Add("@DATE", OracleDbType.Date);
                cmdspInsertMarketSummaryOverview.Parameters["@DATE"].Value = Convert.ToDateTime(_date);
                cmdspInsertMarketSummaryOverview.Parameters.Add("@STATUS", OracleDbType.Varchar2);
                cmdspInsertMarketSummaryOverview.Parameters["@STATUS"].Value = _status;
                cmdspInsertMarketSummaryOverview.Parameters.Add("@VOLUME", OracleDbType.Double);
                cmdspInsertMarketSummaryOverview.Parameters["@VOLUME"].Value = Convert.ToDouble(_volume.Replace(",", ""));
                cmdspInsertMarketSummaryOverview.Parameters.Add("@VALUE", OracleDbType.Double);
                cmdspInsertMarketSummaryOverview.Parameters["@VALUE"].Value = Convert.ToDouble(_value.Replace(",", ""));
                cmdspInsertMarketSummaryOverview.Parameters.Add("@TRADE", OracleDbType.Double);
                cmdspInsertMarketSummaryOverview.Parameters["@TRADE"].Value = Convert.ToDouble(_trade.Replace(",", ""));
                try
                {
                    isMetaDataSaved = cmdspInsertMarketSummaryOverview.ExecuteNonQuery();
                }
                catch (OracleException ex)
                {
                    Debug.WriteLine("SQL Exception in SavingScrip Method: " + ex.Message);
                    Exceptions.Add("SQL Exception in SavingScrip Method: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Exception in SavingScrip Method: " + ex.Message);
                    Exceptions.Add("Exception in SavingScrip Method: " + ex.Message);
                }
                conn.Close();

                return isMetaDataSaved;
            }
            #endregion

            #region INSERT_CURRENT_MARKET_OVERVIEW_DB_SQLITE
            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("SQLITE"))
            {
                SqliteConnection conn = new SqliteConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                conn.Open();
                int isDataCleared = 0;
                //Clear Truncate Current Market Summary Data
                SqliteCommand cmd = new SqliteCommand("TRUNCATE TABLE TRUNCATE_CURRENT_MARKET_SUMMARY;", conn);
                cmd.CommandType = CommandType.Text;
                try
                {
                    isDataCleared = cmd.ExecuteNonQuery();
                }
                catch (SqliteException ex)
                {
                    Debug.WriteLine("SQLite Exception in SavingDataToDatabase Method: " + ex.Message);
                    Exceptions.Add("SQLite Exception in SavingDataToDatabase Method: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("SQL Exception in SavingDataToDatabase Method: " + ex.Message);
                    Exceptions.Add("SQL Exception in SavingDataToDatabase Method: " + ex.Message);
                }
                conn.Close();
                return isDataCleared;
            }
            #endregion

            #region INSERT_CURRENT_MARKET_OVERVIEW_DB_POSTGRE
            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("POSTGRE"))
            {
                NpgsqlConnection conn = new NpgsqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                conn.Open();
                int isDataCleared = 0;
                //Clear Truncate Current Market Summary Data
                NpgsqlCommand cmd = new NpgsqlCommand("TRUNCATE TABLE TRUNCATE_CURRENT_MARKET_SUMMARY;", conn);
                cmd.CommandType = CommandType.Text;
                try
                {
                    isDataCleared = cmd.ExecuteNonQuery();
                }
                catch (NpgsqlException ex)
                {
                    Debug.WriteLine("SQLite Exception in SavingDataToDatabase Method: " + ex.Message);
                    Exceptions.Add("SQLite Exception in SavingDataToDatabase Method: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("SQL Exception in SavingDataToDatabase Method: " + ex.Message);
                    Exceptions.Add("SQL Exception in SavingDataToDatabase Method: " + ex.Message);
                }
                conn.Close();
                return isDataCleared;
            }
            #endregion

            #region INSERT_CURRENT_MARKET_OVERVIEW_DB_Elasticsearch
            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("ELASTICSEARCH"))
            {
                //NpgsqlConnection conn = new NpgsqlConnection();
                //conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                //conn.Open();
                int isDataCleared = 0;
                //Clear Truncate Current Market Summary Data
                //NpgsqlCommand cmd = new NpgsqlCommand("TRUNCATE TABLE TRUNCATE_CURRENT_MARKET_SUMMARY;", conn);
                //cmd.CommandType = CommandType.Text;
                //try
                //{
                //    isDataCleared = cmd.ExecuteNonQuery();
                //}
                //catch (NpgsqlException ex)
                //{
                //    Debug.WriteLine("SQLite Exception in SavingDataToDatabase Method: " + ex.Message);
                //    Exceptions.Add("SQLite Exception in SavingDataToDatabase Method: " + ex.Message);
                //}
                //catch (Exception ex)
                //{
                //    Debug.WriteLine("SQL Exception in SavingDataToDatabase Method: " + ex.Message);
                //    Exceptions.Add("SQL Exception in SavingDataToDatabase Method: " + ex.Message);
                //}
                //conn.Close();
                return isDataCleared;
            }
            #endregion

            #region ELSE

            else
            {
                return 0;
            }

            #endregion

        }

        #endregion

        #region MustWorkStart
        public void mustWork()
        {
            try
            {
                GetDefault();
                worker.WorkerReportsProgress = true;
                worker.ReportProgress(1);

                DateTime CurrentTime = DateTime.Parse(_miscellenousData["DATE"]);
                Debug.WriteLine(CurrentTime);
                DateTime ExpiredTime = ExpiryDate;
                Debug.WriteLine(ExpiredTime);
                if (ExpiredTime <= CurrentTime)
                {
                    MessageBox.Show("The license of Data Extractor Utility is expired. Please contact your system administrator or software vendor.", "License Expired", MessageBoxButton.OK, MessageBoxImage.Information);
                    //MessageBox.Show("Application is expired.");
                }
                else
                {
                    statusContent = "Status: Refreshing Scrips..";
                    this.Dispatcher.Invoke(() =>
                    {
                        var image = new BitmapImage();
                        image.BeginInit();
                        image.UriSource = ResourceAccessor.Get("Images/gear.gif");
                        image.EndInit();
                        ImageBehavior.SetAnimatedSource(imgStatus, image);
                        lblStatusMessage.Text = statusContent;
                        progressBar.Value = 10;
                    });
                    
                    GetScripDetails();

                    this.Dispatcher.Invoke(() =>
                    {
                        lblDate.Text = DateTime.Parse(_miscellenousData["DATE"]).ToString("dddd, dd MMMM yyyy hh:mm tt");
                        lblStatus.Text = _miscellenousData["STATUS"].ToUpper();
                        lblVolume.Text = String.Format("{0:#,##0}", _miscellenousData["VOLUME"]);
                        lblValue.Text = String.Format("{0:#,##0}", _miscellenousData["VALUE"]);
                        lblTrades.Text = String.Format("{0:#,##0}", _miscellenousData["TRADES"]);

                        list1.Items.Clear();

                        Truncate_CURRENT_MARKET_SUMMARY_DB();
                        Truncate_CURRENT_MARKET_OVERVIEW_DB();
                        INSERT_CURRENT_MARKET_OVERVIEW_DB(_miscellenousData["DATE"], _miscellenousData["STATUS"], _miscellenousData["VOLUME"], _miscellenousData["VALUE"], _miscellenousData["TRADES"]);
                        int count = 1;
                        for (int i = 0; i < _scrip.Count; i++)
                        {
                            if (_scrip[i] == null) { }
                            else if (_scrip[i].Volume.Trim() == "CHANGE") { }
                            else
                            {
                                _scrip[i].CurrentMarketSummaryId = count++;
                                //
                                string _keyword = txtSearch.Text.ToLower().Trim();
                                string _category = comboCategory.SelectedItem.ToString().ToLower().Trim();
                                string _categoryKind = comboCategory.SelectedItem.ToString().Trim();
                                if ((_keyword == "") && _category == "all categories")
                                {
                                    list1.Items.Clear();
                                    for (int j = 0; j < _scrip.Count; j++)
                                    {
                                        if (_scrip[j] == null) { }
                                        else if (_scrip[j].Volume.Trim() == "CHANGE") { }
                                        else
                                        {
                                            list1.Items.Add(_scrip[j]);
                                        }
                                    }
                                }
                                else
                                {
                                    IEnumerable<CurrentMarketSummary> _query = _scrip;
                                    if (_category != "all categories")
                                    {
                                        _query = _scrip.Where(q => q.Category.ToLower().Trim().Contains(_category));
                                    }
                                    _query = _query.Where(q => (q.Name.ToString().ToLower().Trim().Contains(_keyword)) || (q.Category.ToLower().Trim().Contains(_keyword)) || (q.Symbol.ToLower().Trim().Contains(_keyword)) || (q.Category.ToLower().Trim().Contains(_category) && _keyword == "") || (q.Category.ToLower().Trim().Contains(_category) && q.Name.ToLower().Trim().Contains(_keyword)));
                                    List<CurrentMarketSummary> _tempscrip = _query.ToList();
                                    list1.Items.Clear();
                                    for (int j = 0; j < _tempscrip.Count; j++)
                                    {
                                        if (_tempscrip[j] == null) { }
                                        else if (_tempscrip[j].Volume.Trim() == "CHANGE") { }
                                        else
                                        {
                                            list1.Items.Add(_tempscrip[j]);
                                        }
                                    }
                                }
                                //
                                SavingScrip(_miscellenousData["DATE"], _miscellenousData["STATUS"], _miscellenousData["VOLUME"], _miscellenousData["VALUE"], _miscellenousData["TRADES"], _scrip[i]);
                            }
                        }

                    });
                    
                }
            }
            catch (WebException ex)
            {
                MessageBox.Show(ex.Message, "Internet Connectivity Problem", MessageBoxButton.OK, MessageBoxImage.Information);
                Debug.WriteLine("Internet Exception: " + ex.Message);
            }

            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Database Connectivity Problem", MessageBoxButton.OK, MessageBoxImage.Information);
                Debug.WriteLine("SQL Exception: " + ex.Message);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "General Problem", MessageBoxButton.OK, MessageBoxImage.Information);
                Debug.WriteLine("General Exception: " + ex.Message);
            }

        }

        #endregion

        #region SavingScrip

        private int SavingScrip(string _date, string _status, string _volume, string _value, string _trades, CurrentMarketSummary _scrip)
        {
            #region SavingScrip_MSSQLSERVER
            if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("MSSQLSERVER"))
            {
                if (_date != null)
                {
                    SqlConnection conn = new SqlConnection();
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    conn.Open();
                    int isDataSaved = 0;
                    int isDataHistorySaved = 0;

                    SqlCommand cmdspInsertMarketSummary = new SqlCommand("spINSERT_CURRENT_MARKET_SUMMARY", conn);
                    cmdspInsertMarketSummary.CommandType = CommandType.StoredProcedure;
                    cmdspInsertMarketSummary.Parameters.Add("@COMPANY_SECTOR", SqlDbType.VarChar, -1);
                    cmdspInsertMarketSummary.Parameters["@COMPANY_SECTOR"].Value = _scrip.Category;
                    cmdspInsertMarketSummary.Parameters.Add("@COMPANY_NAME", SqlDbType.VarChar, -1);
                    cmdspInsertMarketSummary.Parameters["@COMPANY_NAME"].Value = _scrip.Name;
                    cmdspInsertMarketSummary.Parameters.Add("@COMPANY_SYMBOL", SqlDbType.VarChar, -1);
                    cmdspInsertMarketSummary.Parameters["@COMPANY_SYMBOL"].Value = _scrip.Symbol;
                    cmdspInsertMarketSummary.Parameters.Add("@COMPANY_LDCP", SqlDbType.VarChar, -1);
                    cmdspInsertMarketSummary.Parameters["@COMPANY_LDCP"].Value = _scrip.Ldcp;
                    cmdspInsertMarketSummary.Parameters.Add("@COMPANY_OPEN", SqlDbType.VarChar, -1);
                    cmdspInsertMarketSummary.Parameters["@COMPANY_OPEN"].Value = _scrip.Open;
                    cmdspInsertMarketSummary.Parameters.Add("@COMPANY_HIGH", SqlDbType.VarChar, -1);
                    cmdspInsertMarketSummary.Parameters["@COMPANY_HIGH"].Value = _scrip.High;
                    cmdspInsertMarketSummary.Parameters.Add("@COMPANY_LOW", SqlDbType.VarChar, -1);
                    cmdspInsertMarketSummary.Parameters["@COMPANY_LOW"].Value = _scrip.Low;
                    cmdspInsertMarketSummary.Parameters.Add("@COMPANY_CURRENT", SqlDbType.VarChar, -1);
                    cmdspInsertMarketSummary.Parameters["@COMPANY_CURRENT"].Value = _scrip.Current;
                    cmdspInsertMarketSummary.Parameters.Add("@COMPANY_CHANGE", SqlDbType.VarChar, -1);
                    cmdspInsertMarketSummary.Parameters["@COMPANY_CHANGE"].Value = _scrip.Change;
                    cmdspInsertMarketSummary.Parameters.Add("@COMPANY_VOLUME", SqlDbType.VarChar, -1);
                    cmdspInsertMarketSummary.Parameters["@COMPANY_VOLUME"].Value = _scrip.Volume;

                    //

                    SqlCommand cmdForspInsertMarketSummaryHistory = new SqlCommand("spINSERT_MARKET_SUMMARY_HISTORY", conn);
                    cmdForspInsertMarketSummaryHistory.CommandType = CommandType.StoredProcedure;
                    cmdForspInsertMarketSummaryHistory.Parameters.Add("@MSH_DATE", SqlDbType.DateTime);
                    cmdForspInsertMarketSummaryHistory.Parameters["@MSH_DATE"].Value = _miscellenousData["DATE"];
                    cmdForspInsertMarketSummaryHistory.Parameters.Add("@MSH_STATUS", SqlDbType.VarChar, 500);
                    cmdForspInsertMarketSummaryHistory.Parameters["@MSH_STATUS"].Value = _miscellenousData["STATUS"];
                    cmdForspInsertMarketSummaryHistory.Parameters.Add("@MSH_SECTOR", SqlDbType.VarChar, 500);
                    cmdForspInsertMarketSummaryHistory.Parameters["@MSH_SECTOR"].Value = _scrip.Category;
                    cmdForspInsertMarketSummaryHistory.Parameters.Add("@MSH_SCRIP_NAME", SqlDbType.VarChar, 500);
                    cmdForspInsertMarketSummaryHistory.Parameters["@MSH_SCRIP_NAME"].Value = _scrip.Name;
                    cmdForspInsertMarketSummaryHistory.Parameters.Add("@MSH_SYMBOL", SqlDbType.VarChar, 500);
                    cmdForspInsertMarketSummaryHistory.Parameters["@MSH_SYMBOL"].Value = _scrip.Symbol;
                    cmdForspInsertMarketSummaryHistory.Parameters.Add("@MSH_COMPANY_LDCP", SqlDbType.VarChar, -1);
                    cmdForspInsertMarketSummaryHistory.Parameters["@MSH_COMPANY_LDCP"].Value = _scrip.Ldcp;
                    cmdForspInsertMarketSummaryHistory.Parameters.Add("@MSH_COMPANY_OPEN", SqlDbType.VarChar, -1);
                    cmdForspInsertMarketSummaryHistory.Parameters["@MSH_COMPANY_OPEN"].Value = _scrip.Open;
                    cmdForspInsertMarketSummaryHistory.Parameters.Add("@MSH_COMPANY_HIGH", SqlDbType.VarChar, -1);
                    cmdForspInsertMarketSummaryHistory.Parameters["@MSH_COMPANY_HIGH"].Value = _scrip.High;
                    cmdForspInsertMarketSummaryHistory.Parameters.Add("@MSH_COMPANY_LOW", SqlDbType.VarChar, -1);
                    cmdForspInsertMarketSummaryHistory.Parameters["@MSH_COMPANY_LOW"].Value = _scrip.Low;
                    cmdForspInsertMarketSummaryHistory.Parameters.Add("@MSH_COMPANY_CURRENT", SqlDbType.VarChar, -1);
                    cmdForspInsertMarketSummaryHistory.Parameters["@MSH_COMPANY_CURRENT"].Value = _scrip.Current;
                    cmdForspInsertMarketSummaryHistory.Parameters.Add("@MSH_COMPANY_CHANGE", SqlDbType.VarChar, -1);
                    cmdForspInsertMarketSummaryHistory.Parameters["@MSH_COMPANY_CHANGE"].Value = _scrip.Change;
                    cmdForspInsertMarketSummaryHistory.Parameters.Add("@MSH_COMPANY_VOLUME", SqlDbType.VarChar, -1);
                    cmdForspInsertMarketSummaryHistory.Parameters["@MSH_COMPANY_VOLUME"].Value = _scrip.Volume;

                    try
                    {
                        if (_scrip != null)
                        {
                            isDataSaved = cmdspInsertMarketSummary.ExecuteNonQuery();
                            isDataHistorySaved = cmdForspInsertMarketSummaryHistory.ExecuteNonQuery();
                        }
                    }
                    catch (SqlException ex)
                    {
                        Debug.WriteLine("SQL Exception in SavingScrip Method: " + ex.Message);
                        Exceptions.Add("SQL Exception in SavingScrip Method: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Exception in SavingScrip Method: " + ex.Message);
                        Exceptions.Add("Exception in SavingScrip Method: " + ex.Message);
                    }
                    conn.Close();

                    return isDataSaved;
                }
                else
                {
                    return 0;
                }
            }

            #endregion

            #region SavingScrip_ORACLE
            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("ORACLE"))
            {
                return 0;
            }
            #endregion

            #region SavingScrip_SQLITE
            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("SQLITE"))
            {
                return 0;
            }
            #endregion

            #region SavingScrip_POSTGRE
            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("POSTGRE"))
            {
                return 0;
            }
            #endregion

            #region SavingScrip_Elasticsearch
            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("ELASTICSEARCH"))
            {
                return 0;
            }
            #endregion

            #region ELSE

            else
            {
                return 0;
            }

            #endregion

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

        #region GetDefault
        public void GetDefault()
        {
            try
            {
                HtmlNodeCollection name_nodes = null;
                HtmlNodeCollection temp = null;
                var xpath = "//*[self::h4 or self::td]";
                temp = FetchDataFromPSX("https://www.psx.com.pk/market-summary/", xpath);
                _webDataCollection = temp;
                name_nodes = temp != null ? temp : null;
                if (name_nodes != null)
                {
                    foreach (HtmlAgilityPack.HtmlNode node in name_nodes)
                    {
                        if (node.InnerText.ToString().StartsWith("* LDCP")) { }
                        else if (node.InnerText.ToString().StartsWith(DateTime.Now.Year.ToString()))
                        {
                            _miscellenousData["DATE"] = node.InnerText.ToString();
                        }
                        else if (node.InnerText.ToString().StartsWith("Status"))
                        {
                            _miscellenousData["STATUS"] = node.InnerText.ToString().Replace("Status: ", "").Replace(" ", "");
                        }
                        else if (node.InnerText.ToString().StartsWith("Volume"))
                        {
                            _miscellenousData["VOLUME"] = node.InnerText.ToString().Replace("Volume: ", "");
                        }
                        else if (node.InnerText.ToString().StartsWith("Value"))
                        {
                            _miscellenousData["VALUE"] = node.InnerText.ToString().Replace("Value: ", "");
                        }
                        else if (node.InnerText.ToString().StartsWith("Trades"))
                        {
                            _miscellenousData["TRADES"] = node.InnerText.ToString().Replace("Trades: ", "");
                        }
                    }
                    _categoryList = GetCategoryData();
                }
                else
                {
                    Debug.WriteLine("Exception in GetDefault Method: Null Values Existed.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception in GetDefault Method: " + ex.Message);
                Exceptions.Add("Exception in GetDefault Method: " + ex.Message);
            }

        }

        #endregion

        #region GetCategoryData

        private List<string> GetCategoryData()
        {
            List<string> _categoryData = new List<string>();
            try
            {
                if (_webDataCollection != null)
                {

                    int counter = 0;
                    foreach (HtmlAgilityPack.HtmlNode node in _webDataCollection)
                    {
                        if (counter == 1 && !node.InnerText.ToString().StartsWith("* LDCP"))
                        {
                            _categoryData.Add(node.InnerText.ToString());
                        }
                        else if (node.InnerText.ToString().Contains("Data refreshes"))
                        {
                            counter = 1;
                        }
                    }
                }
                else
                {
                    Debug.WriteLine("Exception in GetDefault Method: Null Values Existed.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception in GetDefault Method: " + ex.Message);
                Exceptions.Add("Exception in GetDefault Method: " + ex.Message);
            }
            return _categoryData;
        }

        #endregion

        #region GetCompanySymbols

        public string GetCompanySymbols(string CompanyName)
        {
            string result = String.Empty;

            #region GetCompanySymbols_MSSQLSERVER
            if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("MSSQLSERVER"))
            {
                SqlConnection conn = new SqlConnection();
                try
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    using (conn)
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("spGET_SCRIP_SYMBOL_FROM_SCRIP_NAME", conn); // Read user-> stored procedure name
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@CompanyName", SqlDbType.VarChar, 500);
                        cmd.Parameters["@CompanyName"].Value = CompanyName.Trim();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                result = rdr.GetString(0);
                            }
                        }
                        conn.Close();
                    }
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine("SQL Exception in GetCompanySymbols_MSSQLSERVER Method: " + ex.Message);
                    Exceptions.Add("SQL Exception in GetCompanySymbols_MSSQLSERVER Method: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Exception in GetStatusAndMiscellenous Method: " + ex.Message);
                    Exceptions.Add("Exception in GetStatusAndMiscellenous Method: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            #endregion

            #region GetMarketSummaryCompanySymbols_ORACLE
            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("ORACLE"))
            {
                //string connectionString = Configuration.GetConnectionString("DefaultConnection");
                //Debug.WriteLine(connectionString);
                //Debug.WriteLine(" "+ConfigurationManager.ConnectionStrings["DefaultConnection"].CurrentConfiguration.ToString());
                OracleConnection con = new OracleConnection();//"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=128.1.101.32)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));load balancing=false;ha events=false;User Id=IPAMS_TEST;Password=IPAMS_TEST;"
                con.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                con.Open();
                try
                {
                    OracleCommand cmd = new OracleCommand()
                    {
                        CommandText = "SPGETSYMBOLFROMCOMPANYNAME",
                        Connection = con,
                        CommandType = CommandType.StoredProcedure,
                    };
                    OracleParameter input = cmd.Parameters.Add("CompanyName", OracleDbType.Varchar2, 500);
                    OracleParameter output = cmd.Parameters.Add("C_SYMBOL", OracleDbType.Varchar2, 500);
                    input.Direction = ParameterDirection.Input;
                    output.Direction = ParameterDirection.Output;
                    input.Value = CompanyName.Trim().ToLower();
                    cmd.ExecuteNonQuery();
                    result = Convert.ToString(output.Value);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("==" + ex.Message);
                }
                con.Close();
            }


            #endregion

            #region GetMarketSummaryCompanySymbols_SQLITE

            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("SQLITE"))
            {
                //List<ScripInfo> scrips = _context.ScripInfo.ToList();
                ////IQueryable<ScripInfo> scrips = _context.ScripInfo.Where(sc => sc.Name.Contains(_nameDivider[0] + " " + _nameDivider[1]) || sc.Name.Contains(_nameDivider[0]));
                string[] _nameDivider = CompanyName.Split(" ");
                IQueryable<ScripInfo> scrips = _context.ScripInfo.Where(sc => sc.Name.Contains(_nameDivider[0]) || sc.Name.Contains(_nameDivider.Length > 1 ? _nameDivider[0] + " " + _nameDivider[1] : _nameDivider[0] + " " + ""));
                //    if (scrips.Count() > 0)
                //    {
                for (int j = 0; j < scrips.Count(); j++)
                {
                    //Debug.WriteLine("Database Record: " + scrips.ToList()[j].Name + ", Internet Record: " + NAME[i]);
                    //if (scrips.ToList()[j].Name == NAME[i])
                    int _nameLength = _nameDivider.Length;
                    string _tempName = String.Empty;
                    switch (_nameLength)
                    {
                        case 1:
                            _tempName = _nameDivider[0];
                            break;
                        case 2:
                            _tempName = _nameDivider[0] + " " + _nameDivider[1];
                            break;
                        case 3:
                            _tempName = _nameDivider[0] + " " + _nameDivider[1] + " " + _nameDivider[2];
                            break;
                        //case 4:
                        //    _tempName = _nameDivider[0] + " " + _nameDivider[1] + " " + _nameDivider[2] + " " + _nameDivider[3];
                        //    break;
                        //case 5:
                        //    _tempName = _nameDivider[0] + " " + _nameDivider[1] + " " + _nameDivider[2] + " " + _nameDivider[3] + " " + _nameDivider[4];
                        //    break;
                        //case 6:
                        //    _tempName = _nameDivider[0] + " " + _nameDivider[1] + " " + _nameDivider[2] + " " + _nameDivider[3] + " " + _nameDivider[4] + " " + _nameDivider[5];
                        //    break;
                        //case 7:
                        //    _tempName = _nameDivider[0] + " " + _nameDivider[1] + " " + _nameDivider[2] + " " + _nameDivider[3] + " " + _nameDivider[4] + " " + _nameDivider[5] + " " + _nameDivider[6];
                        //    break;
                        default:
                            _tempName = _nameDivider[0] + " " + _nameDivider[1];
                            break;
                    }
                    if (scrips.ToList()[j].Name.Contains(_tempName))
                    {
                        result = scrips.ToList()[j].Symbol;
                        break;
                    }
                }
                //    }
                //result.Insert(i, " - ");
                //}

            }

            #endregion

            #region GetMarketSummaryCompanySymbols_POSTGRE

            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("POSTGRE"))
            {
                result = "-";
            }

            #endregion

            return result;
        }

        #endregion

        #region CheckCategoryStatus

        public Boolean CheckCategoryStatus(string param)
        {
            Boolean status = false;
            foreach (string item in _categoryList)
            {
                if (item.Equals(param))
                {
                    status = true;
                    //break;
                }
            }
            return status;
        }

        #endregion

        #region GetScripDetails

        public void GetScripDetails()
        {
            HtmlNodeCollection nodes = _webDataCollection;
            List<string> _filteredData = new List<string>();
            List<int> _categoryCount = new List<int>();
            int _counter = 0;
            int _captureflag = 0;
            foreach (HtmlAgilityPack.HtmlNode node in nodes)
            {
                for (int i = 0; i < _categoryList.Count; i++)
                {
                    if (node.InnerText.Equals(_categoryList[i]))
                    {
                        _categoryCount.Add(0);
                        _captureflag = 1;
                    }

                    //else if (node.InnerText.ToString().Trim().Equals("VOLUME") && _captureflag == 0)
                    //{

                    //}
                }
                if (_captureflag == 1)
                {
                    _filteredData.Add(node.InnerText);
                }

            }

            int flagger = 0;
            int _scriptor = 2;
            int volumeOccurance = 0;
            string _tempCategory = String.Empty;
            string _tempScrip = String.Empty;
            _scrip.Clear();
            for (int j = 0; j < _filteredData.Count; j++)
            {
                if (j > 0)
                {

                    if (_filteredData[j].Equals("VOLUME"))
                    {
                        //Debug.WriteLine("Category -> " + _filteredData[j - 8]);
                        _tempCategory = _filteredData[j - 8];
                        if (volumeOccurance > 0)
                        {
                            flagger++;
                        }
                    }
                    else if (!CheckCategoryStatus(_filteredData[j])) { }
                    else
                    {
                        if (_filteredData[j - 1].Equals("VOLUME"))
                        {
                            volumeOccurance++;
                            _scriptor = j;
                            _scriptor++;
                            _tempScrip = _filteredData[j];
                            if (_filteredData[j].Equals(_tempCategory))
                            {

                            }
                            else
                            {
                                //Debug.WriteLine("Scrip Name: " + _filteredData[j] + ", Category: " + _tempCategory + ", LDCP: " + _filteredData[_scriptor] + ", OPEN: " + _filteredData[_scriptor + 1] + ", HIGH: " + _filteredData[_scriptor + 2] + ", LOW: " + _filteredData[_scriptor + 3] + ", CURRENT: " + _filteredData[_scriptor + 4] + ", CHANGE: " + _filteredData[_scriptor + 5] + ", VOLUME: " + _filteredData[_scriptor + 6]);
                                _scrip.Add(new CurrentMarketSummary { Name = _filteredData[j], Symbol = GetCompanySymbols(_filteredData[j]), Category = _tempCategory, Ldcp = _filteredData[_scriptor], Open = _filteredData[_scriptor + 1], High = _filteredData[_scriptor + 2], Low = _filteredData[_scriptor + 3], Current = _filteredData[_scriptor + 4], Change = _filteredData[_scriptor + 5].Trim().Replace(" ", ""), Volume = _filteredData[_scriptor + 6] });
                            }
                            _scriptor = 2;
                        }
                        if (j > 7)
                        {
                            if (_filteredData[j - 8] == _tempScrip)
                            {
                                _scriptor = j;
                                _scriptor++;
                                _tempScrip = _filteredData[j];
                                if (_filteredData[j].Equals(_tempCategory))
                                {

                                }
                                else
                                {
                                    //Debug.WriteLine("Scrip Name: " + _filteredData[j] + ", Category: " + _tempCategory + ", LDCP: " + _filteredData[_scriptor] + ", OPEN: " + _filteredData[_scriptor + 1] + ", HIGH: " + _filteredData[_scriptor + 2] + ", LOW: " + _filteredData[_scriptor + 3] + ", CURRENT: " + _filteredData[_scriptor + 4] + ", CHANGE: " + _filteredData[_scriptor + 5] + ", VOLUME: " + _filteredData[_scriptor + 6]);
                                    _scrip.Add(new CurrentMarketSummary { Name = _filteredData[j], Symbol = GetCompanySymbols(_filteredData[j]), Category = _tempCategory, Ldcp = _filteredData[_scriptor], Open = _filteredData[_scriptor + 1], High = _filteredData[_scriptor + 2], Low = _filteredData[_scriptor + 3], Current = _filteredData[_scriptor + 4], Change = _filteredData[_scriptor + 5].Trim().Replace(" ", ""), Volume = _filteredData[_scriptor + 6] });
                                }
                                _scriptor = 2;
                            }
                        }
                    }
                }
            }

            //counter = 0;
            //for (int j = 1, openIndex = 2, highIndex = 3, lowIndex = 4, currentIndex = 5, changeIndex = 6, volumeIndex = 7; j < AllTableRowData.Count() || openIndex < AllTableRowData.Count() || highIndex < AllTableRowData.Count() || lowIndex < AllTableRowData.Count() || currentIndex < AllTableRowData.Count() || changeIndex < AllTableRowData.Count() ||  volumeIndex < AllTableRowData.Count() ; j += 8, openIndex += 8, highIndex = highIndex + 8, lowIndex = lowIndex + 8, currentIndex += 8, changeIndex += 8, volumeIndex += 8)
            //{
            //    if (AllTableRowData[j] != null)
            //    {
            //        if (AllTableRowData[j].Trim().Equals("LDCP"))
            //        {
            //        }
            //        else
            //        {
            //            _scripList[counter].Ldcp = AllTableRowData[j].Trim().Replace("\n","");
            //        }
            //    }

            //    //Test Method Open
            //    if (AllTableRowData[openIndex] != null)
            //    {
            //        if (AllTableRowData[openIndex].Trim().Equals("OPEN"))
            //        {
            //        }
            //        else
            //        {
            //            _scripList[counter].Open = AllTableRowData[openIndex].Trim().Replace("\n", "");
            //        }
            //    }

            //    //Test Method High
            //    if (AllTableRowData[highIndex] != null)
            //    {
            //        if (AllTableRowData[highIndex].Trim().Equals("HIGH")) { }
            //        else
            //        {
            //            _scripList[counter].High = AllTableRowData[highIndex].Trim().Replace("\n", "");
            //        }
            //    }

            //    //Test Method Low
            //    if (AllTableRowData[lowIndex] != null)
            //    {
            //        if (AllTableRowData[lowIndex].Trim().Equals("LOW")) { }
            //        else
            //        {
            //            _scripList[counter].Low = AllTableRowData[lowIndex].Trim().Replace("\n", "");
            //        }
            //    }

            //    //Test Method Current
            //    if (AllTableRowData[currentIndex] != null)
            //    {
            //        if (AllTableRowData[currentIndex].Trim().Equals("CURRENT")) { }
            //        else
            //        {
            //            _scripList[counter].Current = AllTableRowData[currentIndex].Trim().Replace("\n", "");
            //        }
            //    }

            //    //Test Method Change
            //    if (AllTableRowData[changeIndex] != null)
            //    {
            //        if (AllTableRowData[changeIndex].Trim().Equals("CHANGE")) { }
            //        else
            //        {
            //            _scripList[counter].Change = Convert.ToDouble(AllTableRowData[changeIndex].Trim().Replace("\n", ""));
            //        }
            //    }

            //    //Test Method Volume
            //    if (AllTableRowData[volumeIndex] != null)
            //    {
            //        if (AllTableRowData[volumeIndex].Trim().Equals("VOLUME")) { }
            //        else
            //        {
            //            _scripList[counter].Volume = AllTableRowData[volumeIndex].Trim().Replace("\n", "");
            //        }
            //    }

            //    //Increment counter
            //    counter++;

            //}
        }

        #endregion

        #region GetScripNames

        public List<string> GetMarketSummaryCompanyNames()
        {
            // Local List of string Variable to store return value 
            List<string> result = new List<string>();

            // Get All Nodes of td tag
            HtmlNodeCollection name_nodes = null;
            HtmlNodeCollection temp = null;
            temp = FetchDataFromPSX("https://www.psx.com.pk/market-summary/", "//td");
            _webDataCollection = temp;
            name_nodes = temp != null ? temp : null;
            if (name_nodes != null)
            {
                // Get All Data of TD Tags
                List<string> RowData = new List<string>();
                //int counter = 0;

                // Start capturing relevant data flag
                int StartCapturingflag = 0;

                // Loops through all nodes
                foreach (HtmlAgilityPack.HtmlNode node in name_nodes)
                {
                    // Tap to get desired data control flow
                    if (StartCapturingflag == 1)
                    {
                        RowData.Add(node.InnerText.ToString());
                    }

                    // Signal the flag to start capturing data when it gets Header VOLUME
                    else if (node.InnerText.ToString().Trim().Equals("VOLUME"))
                    {
                        StartCapturingflag = 1;
                    }
                    else
                    {

                    }
                }
                for (int j = 0; j < RowData.Count; j++)
                {
                    if (j % 8 == 0)
                    {
                        if (RowData[j] != null)
                        {
                            if (RowData[j].Contains("SCRIP"))
                            {

                            }
                            else
                            {
                                result.Add(RowData[j]);
                            }
                        }
                    }
                }
            }
            else
            {
                Debug.WriteLine("Null Values in Market Names Method.");
            }

            // return company names
            return result;
        }

        #endregion

        #region GetScripOthers

        public string[] GetMarketSummaryCompanyLDCP()
        {
            // HtmlNodeCollection name_nodes = FetchDataFromPSX("https://www.psx.com.pk/market-summary/", "//td");
            HtmlNodeCollection name_nodes = _webDataCollection;
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
            for (int j = 1; j < AllTableRowData.Length; j = j + 8)
            {
                if (AllTableRowData[j] != null)
                {
                    if (AllTableRowData[j].Trim().Equals("LDCP"))
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

        public string[] GetMarketSummaryCompanyOPEN()
        {
            //HtmlNodeCollection name_nodes = FetchDataFromPSX("https://www.psx.com.pk/market-summary/", "//td");
            HtmlNodeCollection name_nodes = _webDataCollection;
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
            for (int j = 2; j < AllTableRowData.Length; j = j + 8)
            {
                if (AllTableRowData[j] != null)
                {
                    if (AllTableRowData[j].Trim().Equals("OPEN"))
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

        public string[] GetMarketSummaryCompanyHIGH()
        {
            //HtmlNodeCollection name_nodes = FetchDataFromPSX("https://www.psx.com.pk/market-summary/", "//td");
            HtmlNodeCollection name_nodes = _webDataCollection;
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
            for (int j = 3; j < AllTableRowData.Length; j = j + 8)
            {
                if (AllTableRowData[j] != null)
                {
                    if (AllTableRowData[j].Trim().Equals("HIGH"))
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

        public string[] GetMarketSummaryCompanyLOW()
        {
            //HtmlNodeCollection name_nodes = FetchDataFromPSX("https://www.psx.com.pk/market-summary/", "//td");
            HtmlNodeCollection name_nodes = _webDataCollection;
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
            for (int j = 4; j < AllTableRowData.Length; j = j + 8)
            {
                if (AllTableRowData[j] != null)
                {
                    if (AllTableRowData[j].Trim().Equals("LOW"))
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

        public string[] GetMarketSummaryCompanyCURRENT()
        {
            string[] result = null;
            HtmlNodeCollection name_nodes = null;
            //HtmlNodeCollection temp = null;
            //temp = FetchDataFromPSX("https://www.psx.com.pk/market-summary/", "//td");
            name_nodes = _webDataCollection != null ? _webDataCollection : name_nodes;
            if (name_nodes != null)
            {
                //HtmlNodeCollection name_nodes = _webDataCollection;
                result = new string[name_nodes.Count];
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
                for (int j = 5; j < AllTableRowData.Length; j = j + 8)
                {
                    if (AllTableRowData[j] != null)
                    {
                        if (AllTableRowData[j].Trim().Equals("CURRENT"))
                        {
                            //result[counter2++] += AllTableRowData[j];
                        }
                        else
                        {
                            result[counter2++] += AllTableRowData[j];
                        }
                    }
                }
            }

            return result;
        }

        public string[] GetMarketSummaryCompanyCHANGE()
        {
            //HtmlNodeCollection name_nodes = FetchDataFromPSX("https://www.psx.com.pk/market-summary/", "//td");
            HtmlNodeCollection name_nodes = _webDataCollection;
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
            for (int j = 6; j < AllTableRowData.Length; j = j + 8)
            {
                if (AllTableRowData[j] != null)
                {
                    if (AllTableRowData[j].Trim().Equals("CHANGE"))
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

        public string[] GetMarketSummaryCompanyVOLUME()
        {
            //HtmlNodeCollection name_nodes = FetchDataFromPSX("https://www.psx.com.pk/market-summary/", "//td");
            HtmlNodeCollection name_nodes = _webDataCollection;
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

        #endregion

        #region btnSearch_Click
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string _keyword = txtSearch.Text.ToLower().Trim();
            string _category = comboCategory.SelectedItem.ToString().ToLower().Trim();
            string _categoryKind = comboCategory.SelectedItem.ToString().Trim();
            if ( _keyword == ""  && _category == "all categories")
            {
                list1.Items.Clear();
                for (int i = 0; i < _scrip.Count; i++)
                {
                    if (_scrip[i] == null) { }
                    else if (_scrip[i].Volume.Trim() == "CHANGE") { }
                    else
                    {
                        list1.Items.Add(_scrip[i]);
                    }
                }
            }
            else
            {
                IEnumerable<CurrentMarketSummary> _query = _scrip;
                if (_category != "all categories")
                {
                    _query = _scrip.Where(q => q.Category.ToLower().Trim().Contains(_category));
                }
                _query = _query.Where(q => (q.Name.ToString().ToLower().Trim().Contains(_keyword)) || (q.Category.ToLower().Trim().Contains(_keyword)) || (q.Symbol.ToLower().Trim().Contains(_keyword)) || (q.Category.ToLower().Trim().Contains(_category) && _keyword == "") || (q.Category.ToLower().Trim().Contains(_category) && q.Name.ToLower().Trim().Contains(_keyword)) );
                List<CurrentMarketSummary> _tempscrip = _query.ToList();
                list1.Items.Clear();
                for (int i = 0; i < _tempscrip.Count; i++)
                {
                    if (_tempscrip[i] == null) { }
                    else if (_tempscrip[i].Volume.Trim() == "CHANGE") { }
                    else
                    {
                        list1.Items.Add(_tempscrip[i]);
                    }
                }
            }

        }

        #endregion

        #region txtSearch_TextChanged
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearch.Text.Trim() == "") { }
            else
            {
                string _keyword = txtSearch.Text.ToLower().Trim();
                string _category = comboCategory.SelectedItem.ToString().ToLower().Trim();
                string _categoryKind = comboCategory.SelectedItem.ToString().Trim();
                if ((_keyword == "" || _keyword == "search here..") && _category == "all categories")
                {
                    list1.Items.Clear();
                    for (int i = 0; i < _scrip.Count; i++)
                    {
                        if (_scrip[i] == null) { }
                        else if (_scrip[i].Volume.Trim() == "CHANGE") { }
                        else
                        {
                            list1.Items.Add(_scrip[i]);
                        }
                    }
                }
                else
                {
                    IEnumerable<CurrentMarketSummary> _query = _scrip;
                    if (_category != "all categories")
                    {
                        _query = _scrip.Where(q => q.Category.ToLower().Trim().Contains(_category));
                    }
                    _query = _query.Where(q => (q.Name.ToString().ToLower().Trim().Contains(_keyword)) || (q.Category.ToLower().Trim().Contains(_keyword)) || (q.Symbol.ToLower().Trim().Contains(_keyword)) || (q.Category.ToLower().Trim().Contains(_category) && _keyword == "") || (q.Category.ToLower().Trim().Contains(_category) && _keyword == "") || (q.Category.ToLower().Trim().Contains(_category) && q.Name.ToLower().Trim().Contains(_keyword)));
                    List<CurrentMarketSummary> _tempscrip = _query.ToList();
                    list1.Items.Clear();
                    for (int i = 0; i < _tempscrip.Count; i++)
                    {
                        if (_tempscrip[i] == null) { }
                        else if (_tempscrip[i].Volume.Trim() == "CHANGE") { }
                        else
                        {
                            list1.Items.Add(_tempscrip[i]);
                        }
                    }
                }
            }
        }

        #endregion

        #region comboCategory_SelectionChanged

        private void comboCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string _keyword = txtSearch.Text.ToLower().Trim();
            string _category = comboCategory.SelectedItem.ToString().ToLower().Trim();
            string _categoryKind = comboCategory.SelectedItem.ToString().Trim();
            if ((_keyword == "") && _category == "all categories")
            {
                list1.Items.Clear();
                for (int i = 0; i < _scrip.Count; i++)
                {
                    if (_scrip[i] == null) { }
                    else if (_scrip[i].Volume.Trim() == "CHANGE") { }
                    else
                    {
                        list1.Items.Add(_scrip[i]);
                    }
                }
            }
            else
            {
                IEnumerable<CurrentMarketSummary> _query = _scrip;
                if (_category != "all categories")
                {
                    _query = _scrip.Where(q => q.Category.ToLower().Trim().Contains(_category));
                }
                _query = _query.Where(q => (q.Name.ToString().ToLower().Trim().Contains(_keyword)) || (q.Category.ToLower().Trim().Contains(_keyword)) || (q.Symbol.ToLower().Trim().Contains(_keyword)) || (q.Category.ToLower().Trim().Contains(_category) && _keyword == "") || (q.Category.ToLower().Trim().Contains(_category) && q.Name.ToLower().Trim().Contains(_keyword)));
                List<CurrentMarketSummary> _tempscrip = _query.ToList();
                list1.Items.Clear();
                for (int i = 0; i < _tempscrip.Count; i++)
                {
                    if (_tempscrip[i] == null) { }
                    else if (_tempscrip[i].Volume.Trim() == "CHANGE") { }
                    else
                    {
                        list1.Items.Add(_tempscrip[i]);
                    }
                }
            }
        }

        #endregion

    }
}
