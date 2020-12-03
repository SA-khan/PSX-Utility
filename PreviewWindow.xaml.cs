using HtmlAgilityPack;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using PSXDataFetchingApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
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
        DateTime ExpiryDate = DateTime.Parse("2020/12/31 15:17:00");
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
        string[] defaultData = new string[2000];
        public delegate void MyDelegate();
        //End 15/10/2020

        public static SpecificScripDetail[] _scrip = null;
        

        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        int countTimer = 0;
        public PreviewWindow(DateTime date, string status, Double Volume, Double Value, Double Trades, List<string> companyName, List<string> companySymbol, List<double> LDCP, List<double> OPEN, List<double> HIGH, List<double> LOW, List<double> CURRENT, List<double> CHANGE, List<double> VOLUME)
        {
            InitializeComponent();

            _scrip = new SpecificScripDetail[companyName.Count];

            for(int i = 0; i < companyName.Count; i++)
            {
                _scrip[i] = new SpecificScripDetail();
                _scrip[i].SECTOR = "";
                _scrip[i].DATE = date;
                _scrip[i].STATUS = status;
                _scrip[i].SCRIP = companyName[i];
                _scrip[i].SYMBOL = companySymbol[i];
                _scrip[i].LDCP = Convert.ToDecimal(LDCP[i]);
                _scrip[i].OPEN = Convert.ToDecimal(OPEN[i]);
                _scrip[i].HIGH = Convert.ToDecimal(HIGH[i]);
                _scrip[i].LOW = Convert.ToDecimal(LOW[i]);
                _scrip[i].CURRENT = Convert.ToDecimal(CURRENT[i]);
                _scrip[i].CHANGE = Convert.ToDouble(CHANGE[i]);
                _scrip[i].VOLUME = Convert.ToDecimal(VOLUME[i]);
            }

            //Client Specific Properties
            try
            {
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["Client"]))
                {
                    if (ConfigurationManager.AppSettings["Client"].Equals("BOP"))
                    {
                        // Header Background Color 
                        var bc = new BrushConverter();
                        header.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#f0a500");

                        //Setting Logo
                        var image = new BitmapImage();
                        image.BeginInit();
                        image.UriSource = ResourceAccessor.Get("Images/BOP.gif");
                        image.EndInit();
                        ImageBehavior.SetAnimatedSource(imgClient, image);
                    }
                    else if (ConfigurationManager.AppSettings["Client"].Equals("HBL"))
                    {
                        // Header Background Color
                        var bc = new BrushConverter();
                        header.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#008269");

                        //Setting Logo
                        var image = new BitmapImage();
                        image.BeginInit();
                        image.UriSource = ResourceAccessor.Get("Images/HBL.gif");
                        image.EndInit();
                        ImageBehavior.SetAnimatedSource(imgClient, image);
                    }
                }
            }
            catch { }

            lblDate.Text +=  date.ToString();
            lblStatus.Text += status.ToString().ToUpper();
            lblVolume.Text += Volume.ToString("#,##0");
            lblValue.Text += Value.ToString("#,##0");
            lblTrades.Text += Trades.ToString("#,##0");

            MarketSummary[] data = new MarketSummary[companyName.Count];

            //GridViewColumn col1 = new GridViewColumn();
            //GridViewColumn col2 = new GridViewColumn();
            //GridViewColumn col3 = new GridViewColumn();
            //GridViewColumn col4 = new GridViewColumn();
            //GridViewColumn col5 = new GridViewColumn();
            //GridViewColumn col6 = new GridViewColumn();
            //GridViewColumn col7 = new GridViewColumn();
            //GridViewColumn col8 = new GridViewColumn();
            //GridViewColumn col9 = new GridViewColumn();


            //gridView.Columns.Add(col1);
            //gridView.Columns.Add(col2);
            //gridView.Columns.Add(col3);
            //gridView.Columns.Add(col4);
            //gridView.Columns.Add(col5);
            //gridView.Columns.Add(col6);
            //gridView.Columns.Add(col7);
            //gridView.Columns.Add(col8);
            //gridView.Columns.Add(col9);



            //col1.DisplayMemberBinding = new Binding("Name");
            //col2.DisplayMemberBinding = new Binding("Symbol");
            //col3.DisplayMemberBinding = new Binding("CURRENT");
            //col4.DisplayMemberBinding = new Binding("LDCP");
            //col5.DisplayMemberBinding = new Binding("OPEN");
            //col6.DisplayMemberBinding = new Binding("HIGH");
            //col7.DisplayMemberBinding = new Binding("LOW");
            //col8.DisplayMemberBinding = new Binding("Change");
            //col9.DisplayMemberBinding = new Binding("Volume");
            //col1.Header = "NAME";
            //col2.Header = "SYMBOL";
            //col3.Header = "CURRENT";
            //col4.Header = "LDCP";
            //col5.Header = "OPEN";
            //col6.Header = "HIGH";
            //col7.Header = "LOW";
            //col8.Header = "CHANGE";
            //col9.Header = "VOLUME";
            int count = 1;
            //lblMessage.Content = "COMPANY NAME - SYMBOL - CURRENT - LDCP - OPEN - HIGH - LOW - CURRENT - CHANGE - VOLUME \n" ;
            for (int i = 0; i < companyName.Count; i++)
            {
                if (companyName[i] == null) { }
                else
                {

                    list1.Items.Add(new MarketSummary {ID = count++ , Name = companyName[i], Symbol = companySymbol[i], CURRENT = String.Format("{0:00.00}", CURRENT[i]),LDCP = String.Format("{0:00.00}", LDCP[i]), OPEN = String.Format("{0:00.00}", OPEN[i]), HIGH = String.Format("{0:00.00}", HIGH[i]), LOW = String.Format("{0:00.00}",LOW[i]), Change = CHANGE[i], Volume = VOLUME[i].ToString("#,##0") });

                }
            }

            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            countTimer++;
            if (countTimer % 180 == 0)
            {
                ButtonAutomationPeer peer = new ButtonAutomationPeer(btnRefresh);
                IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                invokeProv.Invoke();
                //mustWork();

                //Application.Current.Dispatcher.Invoke((Action)delegate {
                //    // your code
                //worker.DoWork += worker_DoWork;
                //worker.ProgressChanged += worker_ProgressChanged;
                //worker.RunWorkerCompleted += worker_RunWorkerCompleted;
                //worker.RunWorkerAsync();
                //});


            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = ResourceAccessor.Get("Images/processing.gif");
            image.EndInit();
            ImageBehavior.SetAnimatedSource(imgStatus, image);
            progressBarContainer.Visibility = Visibility.Visible;
            lblStatusMessage.Text = "Status: Refreshing Data..";

            //MainWindow mainWindow = new MainWindow();
            //mainWindow.Show();
            //mainWindow.Button_Click(sender, e);
            //this.Close();
            Debug.WriteLine("Page Refreshed.");

            //Thread t = new Thread(worker.RunWorkerAsync());
            //t.SetApartmentState(ApartmentState.STA);

            //t.Start();

            Application.Current.Dispatcher.Invoke((Action)delegate {
                // your code
                MyDelegate mdelegate = new MyDelegate(mustWork);
                Thread t = new Thread(mdelegate.Invoke);
                t.SetApartmentState(ApartmentState.STA);

                t.Start();
                //await Task.Run(() => mustWork());
            });

            

            //var image2 = new BitmapImage();
            //image2.BeginInit();
            //image2.UriSource = ResourceAccessor.Get("Images/tick.gif");
            //image2.EndInit();
            //ImageBehavior.SetAnimatedSource(imgStatus, image2);
            //lblStatusMessage.Text = "Status: Ready";

            //Thread t = new Thread(mustWork());
            //t.SetApartmentState(ApartmentState.STA);

            //t.Start();

            //await Task.Run(() => mustWork());

            //mustWork();

            // create a thread  
            //Thread newWindowThread = new Thread(new ThreadStart(() =>
            //{
            //    // create and show the window
            //    worker.DoWork += worker_DoWork;
            //    worker.ProgressChanged += worker_ProgressChanged;
            //    worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            //    worker.RunWorkerAsync();

            //    // start the Dispatcher processing  
            //    System.Windows.Threading.Dispatcher.Run();
            //}));

            //// set the apartment state  
            //newWindowThread.SetApartmentState(ApartmentState.STA);

            //// make the thread a background thread  
            //newWindowThread.IsBackground = true;

            //// start the thread  
            //newWindowThread.Start();


        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
             mustWork();
            //mustWork();
            //worker.DoWork += worker_DoWork;
            //worker.ProgressChanged += worker_ProgressChanged;
            //worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            //worker.RunWorkerAsync();
            //
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            
            //if (isDataSaved)
            //{
            //PreviewWindow window = new PreviewWindow(RequestDate, RequestStatus, RequestValue, RequestVolume, RequestTrades, NAME, SYMBOL, LDCP, OPEN, HIGH, LOW, CURRENT, CHANGE, VOLUME);
            //btnGet.IsEnabled = false;
            //window.Show();
            //this.Hide();
            //}
            //else
            //{
            //    MessageBox.Show("Data Fetch Failed.", "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            //    Debug.WriteLine("Data saved Failed.");
            //    btnGet.IsEnabled = false;
            //}

        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            lblStatusMessage.Text = statusContent;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Hide();
        }

        private async void btnExport_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() => RunExcel());
        }

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
                    for (int index = 0; index < _scrip.Length; index++)
                    {
                        total = index + 2;

                        worksheet.Cells["A" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["A" + total].Value =  countFlag++;
                        worksheet.Cells["B" + total].Value = _scrip[index].SECTOR;
                        worksheet.Cells["C" + total].Value = _scrip[index].DATE;
                        worksheet.Cells["D" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["D" + total].Value = _scrip[index].STATUS;
                        worksheet.Cells["E" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        worksheet.Cells["E" + total].Value = _scrip[index].SCRIP;
                        worksheet.Cells["F" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        worksheet.Cells["F" + total].Value = _scrip[index].SYMBOL;
                        worksheet.Cells["G" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["G" + total].Value = _scrip[index].LDCP;
                        worksheet.Cells["H" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["H" + total].Value = _scrip[index].OPEN;
                        worksheet.Cells["I" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["I" + total].Value = _scrip[index].HIGH;
                        worksheet.Cells["J" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["J" + total].Value = _scrip[index].LOW;
                        worksheet.Cells["K" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["K" + total].Value = _scrip[index].CURRENT;
                        worksheet.Cells["L" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["L" + total].Value = _scrip[index].CHANGE;
                        worksheet.Cells["M" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["M" + total].Value = _scrip[index].VOLUME;
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
                    //using (Stream stream = new FileStream("FundMarketSummary.xlsx", FileMode.Open))
                    //{
                    // File/Stream manipulating code here
                    Process p = new Process();
                    p.StartInfo.UseShellExecute = true;
                    p.StartInfo.FileName = "PSX_MarketSummary_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day.ToString("00") + ".xlsx";
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

        #region MustWorkStart
        public void mustWork()
        {
            try
            {
                MainWindow cl = new MainWindow();
                defaultData = cl.GetDefault();
                RequestDate = DateTime.Parse(defaultData[0]);
                List<String> lMiscellenous = cl.GetStatusAndMiscellenous();

                worker.WorkerReportsProgress = true;
                //int progressPercentage = Convert.ToInt32(((double)i / max) * 100);
                //(sender as BackgroundWorker)worker.ReportProgress(progressPercentage, statusFlag);
                //lblStatusMessage.Text = "Getting General Content..";
                worker.ReportProgress(1);

                DateTime CurrentTime = RequestDate;
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

                    MainWindow cls = new MainWindow();
                    this.Dispatcher.Invoke(() =>
                    {
                        progressBar.Value = 2;
                    });
                    RequestStatus = lMiscellenous[0];
                    this.Dispatcher.Invoke(() =>
                    {
                        progressBar.Value = 3;
                    });
                    //worker.ReportProgress(3);
                    RequestValue = Double.Parse(lMiscellenous[1]);
                    this.Dispatcher.Invoke(() =>
                    {
                        progressBar.Value = 4;
                    });
                    //worker.ReportProgress(4);
                    RequestVolume = Double.Parse(lMiscellenous[2]);
                    this.Dispatcher.Invoke(() =>
                    {
                        progressBar.Value = 5;
                    });
                    //worker.ReportProgress(5);
                    RequestTrades = Double.Parse(lMiscellenous[3]);
                    statusContent = "Status: Getting Names..";
                    this.Dispatcher.Invoke(() =>
                    {
                        lblStatusMessage.Text = statusContent;
                        progressBar.Value = 10;
                    });
                    //worker.ReportProgress(10);
                    NAME = cls.GetMarketSummaryCompanyNames();
                    statusContent = "Status: Getting Symbols..";
                    this.Dispatcher.Invoke(() =>
                    {
                        lblStatusMessage.Text = statusContent;
                        progressBar.Value = 25;
                    });
                    //worker.ReportProgress(25);
                    SYMBOL = cls.GetMarketSummaryCompanySymbols(NAME);
                    statusContent = "Status: Getting LDCP..";
                    this.Dispatcher.Invoke(() =>
                    {
                        lblStatusMessage.Text = statusContent;
                        progressBar.Value = 40;
                    });
                    //worker.ReportProgress(40);
                    string[] getCompanyLDCP = cls.GetMarketSummaryCompanyLDCP();
                    statusContent = "Status: Getting Open..";
                    this.Dispatcher.Invoke(() =>
                    {
                        lblStatusMessage.Text = statusContent;
                        progressBar.Value = 55;
                    });
                    //worker.ReportProgress(55);
                    string[] getCompanyOPEN = cls.GetMarketSummaryCompanyOPEN();
                    statusContent = "Status: Getting High..";
                    this.Dispatcher.Invoke(() =>
                    {
                        lblStatusMessage.Text = statusContent;
                        progressBar.Value = 65;
                    });
                    //worker.ReportProgress(65);
                    string[] getCompanyHIGH = cls.GetMarketSummaryCompanyHIGH();
                    statusContent = "Status: Getting Low..";
                    this.Dispatcher.Invoke(() =>
                    {
                        lblStatusMessage.Text = statusContent;
                        progressBar.Value = 75;
                    });
                    //worker.ReportProgress(75);
                    string[] getCompanyLOW = cls.GetMarketSummaryCompanyLOW();
                    statusContent = "Status: Getting Current..";
                    this.Dispatcher.Invoke(() =>
                    {
                        lblStatusMessage.Text = statusContent;
                        progressBar.Value = 85;
                    });
                    //worker.ReportProgress(85);
                    string[] getCompanyCURRENT = cls.GetMarketSummaryCompanyCURRENT();
                    statusContent = "Status: Getting Change..";
                    this.Dispatcher.Invoke(() =>
                    {
                        lblStatusMessage.Text = statusContent;
                        progressBar.Value = 95;
                    });
                    //worker.ReportProgress(95);
                    string[] getCompanyCHANGE = cls.GetMarketSummaryCompanyCHANGE();
                    statusContent = "Status: Getting Volume..";
                    this.Dispatcher.Invoke(() =>
                    {
                        lblStatusMessage.Text = statusContent;
                        progressBar.Value = 98;
                    });
                    //worker.ReportProgress(98);
                    string[] getCompanyVOLUME = cls.GetMarketSummaryCompanyVOLUME();
                    this.Dispatcher.Invoke(() =>
                    {
                        progressBar.Value = 99;
                    });
                    //worker.ReportProgress(99);
                    double[] CompanyLDCP = new double[getCompanyLDCP.Length];
                    double[] CompanyOPEN = new double[getCompanyLDCP.Length];
                    double[] CompanyHIGH = new double[getCompanyLDCP.Length];
                    double[] CompanyLOW = new double[getCompanyLDCP.Length];
                    double[] CompanyCURRENT = new double[getCompanyLDCP.Length];
                    double[] CompanyCHANGE = new double[getCompanyLDCP.Length];
                    double[] CompanyVOLUME = new double[getCompanyLDCP.Length];
                    //worker.ReportProgress(75);

                    for (int i = 0; i < NAME.Count; i++)
                    {
                        CompanyLDCP[i] = Convert.ToDouble(getCompanyLDCP[i]);
                        CompanyOPEN[i] = Convert.ToDouble(getCompanyOPEN[i]);
                        CompanyHIGH[i] = Convert.ToDouble(getCompanyHIGH[i]);
                        CompanyLOW[i] = Convert.ToDouble(getCompanyLOW[i]);
                        CompanyCURRENT[i] = Convert.ToDouble(getCompanyCURRENT[i]);
                        CompanyCHANGE[i] = Convert.ToDouble(getCompanyCHANGE[i]);
                        CompanyVOLUME[i] = Convert.ToDouble(getCompanyVOLUME[i]);

                        LDCP.Add(CompanyLDCP[i]);
                        OPEN.Add(CompanyOPEN[i]);
                        HIGH.Add(CompanyHIGH[i]);
                        LOW.Add(CompanyLOW[i]);
                        CURRENT.Add(CompanyCURRENT[i]);
                        CHANGE.Add(CompanyCHANGE[i]);
                        VOLUME.Add(CompanyVOLUME[i]);
                    }
                    //worker.ReportProgress(80);
                    //statusContent = "Saving Data..";
                    //worker.ReportProgress(90);
                    //isDataSaved = SavingDataToDatabase(defaultData, NAME, SYMBOL, getCompanyLDCP, getCompanyOPEN, getCompanyHIGH, getCompanyLOW, getCompanyCURRENT, getCompanyCHANGE, getCompanyVOLUME);
                    this.Dispatcher.Invoke(() =>
                    {
                        lblStatusMessage.Text = "Status: Complete..";
                        progressBar.Value = 100;
                    });
                    //worker.ReportProgress(100);
                    //if (isDataSaved)
                    //{
                    //    PreviewWindow window = new PreviewWindow(RequestDate, RequestStatus, RequestValue, RequestVolume, RequestTrades, NAME, SYMBOL, LDCP, OPEN, HIGH, LOW, CURRENT, CHANGE, VOLUME);
                    //    btnGet.IsEnabled = false;
                    //    window.Show();
                    //    this.Hide();
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Data Saved Failed.", "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                    //    Debug.WriteLine("Data saved Failed.");
                    //    progressBar.Visibility = Visibility.Hidden;
                    //    lblProgress.Visibility = Visibility.Hidden;
                    //    lblProgress.Content = "";
                    //}

                    this.Dispatcher.Invoke(() =>
                    {
                        lblDate.Text = DateTime.Parse(defaultData[0]).ToString();
                        lblStatus.Text = defaultData[1].ToUpper();
                        lblVolume.Text = String.Format("{0:#,##0}", defaultData[2]);
                        lblValue.Text = String.Format("{0:#,##0}", defaultData[3]);
                        lblTrades.Text = String.Format("{0:#,##0}", defaultData[4]);

                        list1.Items.Clear();

                        _scrip = new SpecificScripDetail[NAME.Count];
                        int count = 1;
                        for (int i = 0; i < NAME.Count; i++)
                        {
                            _scrip[i] = new SpecificScripDetail();
                            _scrip[i].SECTOR = "";
                            _scrip[i].DATE = Convert.ToDateTime(defaultData[0]);
                            _scrip[i].STATUS = defaultData[1];
                            _scrip[i].SCRIP = NAME[i];
                            _scrip[i].SYMBOL = SYMBOL[i];
                            _scrip[i].LDCP = Convert.ToDecimal(LDCP[i]);
                            _scrip[i].OPEN = Convert.ToDecimal(OPEN[i]);
                            _scrip[i].HIGH = Convert.ToDecimal(HIGH[i]);
                            _scrip[i].LOW = Convert.ToDecimal(LOW[i]);
                            _scrip[i].CURRENT = Convert.ToDecimal(CURRENT[i]);
                            _scrip[i].CHANGE = Convert.ToDouble(CHANGE[i]);
                            _scrip[i].VOLUME = Convert.ToDecimal(VOLUME[i]);
                            list1.Items.Add(new MarketSummary { ID = count++, Name = NAME[i], Symbol = SYMBOL[i], CURRENT = String.Format("{0:00.00}", CURRENT[i]), LDCP = String.Format("{0:00.00}", LDCP[i]), OPEN = String.Format("{0:00.00}", OPEN[i]), HIGH = String.Format("{0:00.00}", HIGH[i]), LOW = String.Format("{0:00.00}", LOW[i]), Change = CHANGE[i], Volume = VOLUME[i].ToString("#,##0") });
                        }
                        progressBarContainer.Visibility = Visibility.Hidden;
                        var image2 = new BitmapImage();
                        image2.BeginInit();
                        image2.UriSource = ResourceAccessor.Get("Images/tick.gif");
                        image2.EndInit();
                        ImageBehavior.SetAnimatedSource(imgStatus, image2);
                        lblStatusMessage.Text = "Status: Ready";

                    });
                    


                    //lblMessage.Content = "COMPANY NAME - SYMBOL - CURRENT - LDCP - OPEN - HIGH - LOW - CURRENT - CHANGE - VOLUME \n" ;
                    //for (int i = 0; i < NAME.Count; i++)
                    //{
                    //    if (NAME[i] == null) { }
                    //    else
                    //    {



                    //    }
                    //}
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

        //Default Data 15/10/2020
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
        public string[] GetDefault()
        {

            HtmlNodeCollection name_nodes = FetchDataFromPSX("https://www.psx.com.pk/market-summary/", "//h4");
            String[] names = new String[name_nodes.Count];
            string[] result = new string[5 + name_nodes.Count];

            int counter = 0;

            //Variable
            string localdatetime = String.Empty;
            string localstatus = String.Empty;
            string localVolume = String.Empty;
            string localValue = String.Empty;
            string localTrades = String.Empty;

            try
            {

                foreach (HtmlAgilityPack.HtmlNode node in name_nodes)
                {
                    if (node.InnerText.ToString().StartsWith("* LDCP")) { }
                    else if (node.InnerText.ToString().StartsWith("2020"))
                    {
                        localdatetime = node.InnerText.ToString();
                    }
                    else if (node.InnerText.ToString().StartsWith("Status"))
                    {
                        localstatus = node.InnerText.ToString().Replace("Status: ", "").Replace(" ", "");
                    }
                    else if (node.InnerText.ToString().StartsWith("Volume"))
                    {
                        localVolume = node.InnerText.ToString().Replace("Volume: ", "");
                    }
                    else if (node.InnerText.ToString().StartsWith("Value"))
                    {
                        localValue = node.InnerText.ToString().Replace("Value : ", "");
                    }
                    else if (node.InnerText.ToString().StartsWith("Trades"))
                    {
                        localTrades = node.InnerText.ToString().Replace("Trades: ", "");
                    }
                    else
                        names[counter++] = node.InnerText.ToString() + "\n";
                }

                result[0] = localdatetime;
                result[1] = localstatus;
                result[2] = localVolume;
                result[3] = localValue;
                result[4] = localTrades;
                for (int i = 0; i < names.Length; i++)
                {
                    result[i + 5] = names[i];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return result;
        }

    }
}
