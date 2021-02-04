using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.ComponentModel;
using PSXDataFetchingApp.Model;
using WpfAnimatedGif;
using Oracle.ManagedDataAccess.Client;
using Microsoft.Data.Sqlite;
using Npgsql;
using Nest;

namespace PSXDataFetchingApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //License Date 
        DateTime ExpiryDate = DateTime.ParseExact("22/03/2025", "dd/MM/yyyy", null);
        public IConfiguration Configuration { get; set; }
        public DataContext _context;

        public HtmlNodeCollection _webDataCollection = null;
        public HtmlNodeCollection _webDataCollectionForCategory = null;

        public static bool isDataSaved = false;

        //Market Date, Status and other Variable Declarations
        public Dictionary<string, string> _miscellenousData = new Dictionary<string, string>(){
                                                            {"DATE", ""},
                                                            {"STATUS", ""},
                                                            {"VOLUME", ""},
                                                            {"TRADES", ""},
                                                            {"VALUE", ""}
                                                        };

        //List Categories
        public List<string> _categoryList = new List<string>() { "", "AUTOMOBILE ASSEMBLER", "AUTOMOBILE PARTS & ACCESSORIES", "CABLE & ELECTRICAL GOODS", "CEMENT", "CHEMICAL", "CLOSE - END MUTUAL FUND", "COMMERCIAL BANKS", "ENGINEERING", "FERTILIZER", "FOOD & PERSONAL CARE PRODUCTS", "GLASS & CERAMICS", "INSURANCE", "INV. BANKS / INV. COS. / SECURITIES COS.", "LEASING COMPANIES", "LEATHER & TANNERIES", "MISCELLANEOUS", "MODARABAS", "OIL & GAS EXPLORATION COMPANIES", "OIL & GAS MARKETING COMPANIES", "PAPER & BOARD", "PHARMACEUTICALS", "POWER GENERATION & DISTRIBUTION", "REFINERY", "SUGAR & ALLIED INDUSTRIES", "SYNTHETIC & RAYON", "TECHNOLOGY & COMMUNICATION", "TEXTILE COMPOSITE", "TEXTILE SPINNING", "TEXTILE WEAVING", "TRANSPORT", "VANASPATI & ALLIED INDUSTRIES", "WOOLLEN", "REAL ESTATE INVESTMENT TRUST", "EXCHANGE TRADED FUNDS", "FUTURE CONTRACTS" };
        //public static List<string> _categoryList = new List<string>();
        public List<string> _categoryList2 = new List<string>() { "AUTOMOBILE ASSEMBLER", "AUTOMOBILE PARTS & ACCESSORIES", "CABLE & ELECTRICAL GOODS", "CEMENT", "CHEMICAL", "CLOSE - END MUTUAL FUND", "COMMERCIAL BANKS", "ENGINEERING", "FERTILIZER", "FOOD & PERSONAL CARE PRODUCTS", "GLASS & CERAMICS", "INSURANCE", "INV. BANKS / INV. COS. / SECURITIES COS.", "LEASING COMPANIES", "LEATHER & TANNERIES", "MISCELLANEOUS", "MODARABAS", "OIL & GAS EXPLORATION COMPANIES", "OIL & GAS MARKETING COMPANIES", "PAPER & BOARD", "PHARMACEUTICALS", "POWER GENERATION & DISTRIBUTION", "REFINERY", "SUGAR & ALLIED INDUSTRIES", "SYNTHETIC & RAYON", "TECHNOLOGY & COMMUNICATION", "TEXTILE COMPOSITE", "TEXTILE SPINNING", "TEXTILE WEAVING", "TOBACCO", "TRANSPORT", "VANASPATI & ALLIED INDUSTRIES", "WOOLLEN", "REAL ESTATE INVESTMENT TRUST", "EXCHANGE TRADED FUNDS", "FUTURE CONTRACTS" };

        public static List<CurrentMarketSummary> _scripList = new List<CurrentMarketSummary>();

        private BackgroundWorker worker = new BackgroundWorker();

        public static int statusFlag = 0;
        public static string statusContent = "";
        public List<string> Exceptions = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            ClientSideProperties();
        }

        public MainWindow(DataContext ctx)
        {
            _context = ctx;
            InitializeComponent();
            ClientSideProperties();
        }

        //Client Specific Properties
        #region ClientSideProperties
        public void ClientSideProperties()
        {
            try
            {
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["Client"]))
                {
                    #region ClientSideProperties_General

                    if (ConfigurationManager.AppSettings["Client"].Equals("General"))
                    {
                        // Header Background Color 
                        var bc = new BrushConverter();
                        MainWindow1.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#fff");
                        lblDemo.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#76b0cc");
                        lblSubHeading.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#76b0cc");

                        //Setting Logo
                        var image = new BitmapImage();
                        image.BeginInit();
                        image.UriSource = ResourceAccessor.Get("Images/astech.png");
                        image.EndInit();
                        ImageBehavior.SetAnimatedSource(ClientLogo, image);
                    }

                    #endregion

                    #region ClientSideProperties_BOP

                    else if (ConfigurationManager.AppSettings["Client"].Equals("BOP"))
                    {
                        // Header Background Color 
                        var bc = new BrushConverter();
                        MainWindow1.Background = (Brush)bc.ConvertFrom("#ffde80");

                        lblDemo.Foreground = (Brush)bc.ConvertFrom("#f0a500");

                        lblSubHeading.Background = (Brush)bc.ConvertFrom("#f0a500");

                        //Setting Logo
                        var image = new BitmapImage();
                        image.BeginInit();
                        image.UriSource = ResourceAccessor.Get("Images/BOP.png");
                        image.EndInit();
                        ImageBehavior.SetAnimatedSource(ClientLogo, image);
                    }

                    #endregion

                    #region ClientSideProperties_HBL

                    else if (ConfigurationManager.AppSettings["Client"].Equals("HBL"))
                    {
                        // Header Background Color
                        var bc = new BrushConverter();
                        MainWindow1.Background = (Brush)bc.ConvertFrom("#cdcdcd");

                        lblDemo.Foreground = (Brush)bc.ConvertFrom("#008269");

                        lblSubHeading.Background = (Brush)bc.ConvertFrom("#008269");

                        //Setting Logo
                        var image = new BitmapImage();
                        image.BeginInit();
                        image.UriSource = ResourceAccessor.Get("Images/HBL.png");
                        image.EndInit();
                        ImageBehavior.SetAnimatedSource(ClientLogo, image);
                    }

                    #endregion

                    #region ClientSideProperties_EFU

                    if (ConfigurationManager.AppSettings["Client"].Equals("EFU"))
                    {
                        // Header Background Color 
                        var bc = new BrushConverter();
                        MainWindow1.Background = (Brush)bc.ConvertFrom("#b9c9d3");

                        lblDemo.Foreground = (Brush)bc.ConvertFrom("#01808d");

                        lblSubHeading.Background = (Brush)bc.ConvertFrom("#01808d");

                        btnGet.Background = (Brush)bc.ConvertFrom("#1c5b64");
                        btnGetV2.Background = (Brush)bc.ConvertFrom("#1c5b64");
                        btnGetV3.Background = (Brush)bc.ConvertFrom("#1c5b64");
                        btnMufapGetMarketSummary.Background = (Brush)bc.ConvertFrom("#1c5b64");
                        btnMufapGetPKRV.Background = (Brush)bc.ConvertFrom("#1c5b64");
                        btnMufapGetPKFRV.Background = (Brush)bc.ConvertFrom("#1c5b64");
                        //btnPrivacy.Foreground = (Brush)bc.ConvertFrom("#01808d");
                        //btnTutorial.Foreground = (Brush)bc.ConvertFrom("#01808d");
                        //btnDisclaimer.Foreground = (Brush)bc.ConvertFrom("#01808d");
                        //lblProgress.Foreground = (Brush)bc.ConvertFrom("#01808d");

                        //Setting Logo
                        var image = new BitmapImage();
                        image.BeginInit();
                        image.UriSource = ResourceAccessor.Get("Images/efu.png");
                        image.EndInit();
                        ImageBehavior.SetAnimatedSource(ClientLogo, image);
                    }

                    #endregion

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception in constructor: " + ex.Message);
                Exceptions.Add("Exception in constructor: " + ex.Message);
            } 
        }
        #endregion

        #region ConfigurationManager_ButtonClick
        private void btnConfigure_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime CurrentTime = DateTime.Now;
                DateTime ExpiredTime = ExpiryDate;
                if (ExpiredTime <= CurrentTime)
                {
                    MessageBox.Show("The license of Data Extractor Utility is expired. Please contact your system administrator or software vendor.", "License Expired", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    Configuration Window = new Configuration(_context);
                    Window.Show();
                    this.Hide();
                }
            }
            catch (WebException ex)
            {
                MessageBox.Show(ex.Message, "Internet Connectivity Problem", MessageBoxButton.OK, MessageBoxImage.Information);
                Debug.WriteLine("Internet Exception: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "General Problem", MessageBoxButton.OK, MessageBoxImage.Information);
                Debug.WriteLine("General Exception: " + ex.Message);
            }

        }

        #endregion

        #region PSX_MarketSummary_ButtonClick
        public void Button_Click(object sender, RoutedEventArgs e)
        {

            DateTime CurrentTime = DateTime.Now;
            Debug.WriteLine(CurrentTime);
            DateTime ExpiredTime = ExpiryDate;
            Debug.WriteLine(ExpiredTime);
            if (ExpiredTime <= CurrentTime)
            {
                MessageBox.Show("The license of Data Extractor Utility is expired. Please contact your system administrator or software vendor.", "License Expired", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                imgWebScrap.Visibility = Visibility.Hidden;
                progressBar.Visibility = Visibility.Visible;
                lblProgress.Visibility = Visibility.Visible;
                lblProgress.Content = "Processing..";
                btnGet.IsEnabled = false;
                btnConfigure.IsEnabled = false;
                btnGetV2.IsEnabled = false;
                btnGetV3.IsEnabled = false;
                btnMufapGetMarketSummary.IsEnabled = false;
                btnMufapGetPKRV.IsEnabled = false;
                btnMufapGetPKFRV.IsEnabled = false;
                progressBar.Value = 10;
                worker.DoWork += worker_DoWork;
                worker.ProgressChanged += worker_ProgressChanged;
                worker.RunWorkerCompleted += worker_RunWorkerCompleted;
                worker.RunWorkerAsync();
            }

        }

        #endregion

        #region PSX_FundMarketSummary_ButtonClick
        private void btnGetV2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime CurrentTime = DateTime.Now;
                DateTime ExpiredTime = ExpiryDate;
                if (ExpiredTime <= CurrentTime)
                {
                    MessageBox.Show("The license of Data Extractor Utility is expired. Please contact your system administrator or software vendor.", "License Expired", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    //if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("ORACLE"))
                    //{
                    //    FundPreviewWindow fundPreviewWindow = new FundPreviewWindow();
                    //    fundPreviewWindow.Show();
                    //    this.Hide();
                    //}
                    //else
                    //{
                    FundPreviewWindow fundPreviewWindow = new FundPreviewWindow(_context);
                    fundPreviewWindow.Show();
                    this.Hide();
                    //}
                }
            }
            catch (WebException ex)
            {
                MessageBox.Show(ex.Message, "Internet Connectivity Problem", MessageBoxButton.OK, MessageBoxImage.Information);
                Debug.WriteLine("Internet Exception: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "General Problem", MessageBoxButton.OK, MessageBoxImage.Information);
                Debug.WriteLine("General Exception: " + ex.Message);
            }
        }

        //private HtmlNodeCollection FetchDataFromPSX(string param)
        //{
        //    string URL = "https://www.psx.com.pk/market-summary/";
        //    DateTime CurrentTime = DateTime.Now;
        //    DateTime ExpiredTime = ExpiryDate;
        //    if (ExpiredTime <= CurrentTime)
        //    {
        //        MessageBox.Show("The license of Data Extractor Utility is expired. Please contact your system administrator or software vendor.", "License Expired", MessageBoxButton.OK, MessageBoxImage.Information);
        //    }
        //    else
        //    {
        //        UploadPSXData window = new UploadPSXData();
        //        window.Show();
        //        this.Hide();
        //    }
        //}

        #endregion

        #region MUFAP_MarketSummary_ButtonClick
        private void btnMufapGetMarketSummary_Click(object sender, RoutedEventArgs e)
        {
            DateTime CurrentTime = DateTime.Now;
            DateTime ExpiredTime = ExpiryDate;
            if (ExpiredTime <= CurrentTime)
            {
                MessageBox.Show("The license of Data Extractor Utility is expired. Please contact your system administrator or software vendor.", "License Expired", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MUFAPMarketSummary window = new MUFAPMarketSummary();
                window.Show();
                this.Hide();
            }
        }
        #endregion

        #region MUFAP_PKRVSummary_ButtonClick
        private void btnMufapGetPKRV_Click(object sender, RoutedEventArgs e)
        {
            DateTime CurrentTime = DateTime.Now;
            DateTime ExpiredTime = ExpiryDate;
            if (ExpiredTime <= CurrentTime)
            {
                MessageBox.Show("The license of Data Extractor Utility is expired. Please contact your system administrator or software vendor.", "License Expired", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MufapPKRV window = new MufapPKRV();
                window.Show();
                this.Hide();
            }
        }
        #endregion

        #region MUFAP_PKFRVSummary_ButtonClick
        private void btnMufapGetPKFRV_Click(object sender, RoutedEventArgs e)
        {
            DateTime CurrentTime = DateTime.Now;
            DateTime ExpiredTime = ExpiryDate;
            if (ExpiredTime <= CurrentTime)
            {
                MessageBox.Show("The license of Data Extractor Utility is expired. Please contact your system administrator or software vendor.", "License Expired", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MufapPKFRV window = new MufapPKFRV();
                window.Show();
                this.Hide();
            }
        }
        #endregion

        #region btnTutorial_Click

        private void btnTutorial_Click(object sender, RoutedEventArgs e)
        {
            DateTime CurrentTime = DateTime.Now;
            Debug.WriteLine(CurrentTime);
            DateTime ExpiredTime = ExpiryDate;
            Debug.WriteLine(ExpiredTime);
            if (ExpiredTime <= CurrentTime)
            {
                MessageBox.Show("The license of Data Extractor Utility is expired. Please contact your system administrator or software vendor.", "License Expired", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                Tutorial cls = new Tutorial();
                cls.Show();
                this.Hide();
            }
        }
        #endregion

        #region btnPrivacy_Click
        private void btnPrivacy_Click(object sender, RoutedEventArgs e)
        {
            DateTime CurrentTime = DateTime.Now;
            Debug.WriteLine(CurrentTime);
            DateTime ExpiredTime = ExpiryDate;
            Debug.WriteLine(ExpiredTime);
            if (ExpiredTime <= CurrentTime)
            {
                MessageBox.Show("The license of Data Extractor Utility is expired. Please contact your system administrator or software vendor.", "License Expired", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                Privacy cls = new Privacy();
                cls.Show();
                this.Hide();
            }
        }

        #endregion

        #region btnDisclaimer_Click
        private void btnDisclaimer_Click(object sender, RoutedEventArgs e)
        {
            DateTime CurrentTime = DateTime.Now;
            Debug.WriteLine(CurrentTime);
            DateTime ExpiredTime = ExpiryDate;
            Debug.WriteLine(ExpiredTime);
            if (ExpiredTime <= CurrentTime)
            {
                MessageBox.Show("The license of Data Extractor Utility is expired. Please contact your system administrator or software vendor.", "License Expired", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                Disclaimer cls = new Disclaimer();
                cls.Show();
                this.Hide();
            }
        }

        #endregion

        #region FetchDataFromPSX
        private HtmlNodeCollection FetchDataFromPSX(string url, string param)
        {
            string URL = url;
            HtmlDocument doc = new HtmlDocument();
            WebClient client = new WebClient();
            HtmlNodeCollection result = null;
            try
            {
                string html = client.DownloadString(URL);
                doc.LoadHtml(html);
                result = doc.DocumentNode.SelectNodes(param);
                _webDataCollection = result;
                return result;
            }
            catch (WebException ex)
            {
                Debug.WriteLine("Web Exception in FetchDataFromPSX Method: " + ex.Message);
                Exceptions.Add("Web Exception in FetchDataFromPSX Method: " + ex.Message);
                //MessageBox.Show("There is an internet connectivity issue.\nDetails: " + ex.Message, "Internet Connectivity Issue", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception in FetchDataFromPSX Method: " + ex.Message);
                Exceptions.Add("Exception in FetchDataFromPSX Method: " + ex.Message);
                //MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }

        }
        #endregion
            #region GetDefault

            public void GetDefault()
            {
                //List<string> result = new List<string>();
                try
                {

                    HtmlNodeCollection name_nodes = null;
                    HtmlNodeCollection temp = null;
                    var xpath = "//*[self::h4 or self::td]";
                    temp = FetchDataFromPSX("https://www.psx.com.pk/market-summary/", xpath);
                    _webDataCollectionForCategory = temp;
                    name_nodes = temp != null ? temp : null;
                    if (name_nodes != null)
                    {

                    foreach (HtmlAgilityPack.HtmlNode node in name_nodes)
                        {
                            //Debug.WriteLine("=> Data: " + node.InnerText.ToString() );
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
                        //GetCategoryData();
                        _categoryList = GetCategoryData();
                        //_categoryList = new List<string>() { "", "AUTOMOBILE ASSEMBLER", "AUTOMOBILE PARTS & ACCESSORIES", "CABLE & ELECTRICAL GOODS", "CEMENT", "CHEMICAL", "CLOSE - END MUTUAL FUND", "COMMERCIAL BANKS", "ENGINEERING", "FERTILIZER", "FOOD & PERSONAL CARE PRODUCTS", "GLASS & CERAMICS", "INSURANCE", "INV. BANKS / INV. COS. / SECURITIES COS.", "LEASING COMPANIES", "LEATHER & TANNERIES", "MISCELLANEOUS", "MODARABAS", "OIL & GAS EXPLORATION COMPANIES", "OIL & GAS MARKETING COMPANIES", "PAPER & BOARD", "PHARMACEUTICALS", "POWER GENERATION & DISTRIBUTION", "REFINERY", "SUGAR & ALLIED INDUSTRIES", "SYNTHETIC & RAYON", "TECHNOLOGY & COMMUNICATION", "TEXTILE COMPOSITE", "TEXTILE SPINNING", "TEXTILE WEAVING", "TRANSPORT", "VANASPATI & ALLIED INDUSTRIES", "WOOLLEN", "REAL ESTATE INVESTMENT TRUST", "EXCHANGE TRADED FUNDS", "FUTURE CONTRACTS" };
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
                    if (_webDataCollectionForCategory != null)
                    {

                        int counter = 0;
                        foreach (HtmlAgilityPack.HtmlNode node in _webDataCollectionForCategory)
                        {
                            //Debug.WriteLine("=> Data: " + node.InnerText.ToString());
                            //if (node.InnerText.ToString().StartsWith("* LDCP") || node.InnerText.ToString().StartsWith(DateTime.Now.Year.ToString() ) ){ }
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

        #region GetMarketSummaryCompanyNames
        public List<string> GetMarketSummaryCompanyNames()
            {
                // Local List of string Variable to store return value 
                List<string> result = new List<string>();

                // Get All Nodes of td tag
            HtmlNodeCollection name_nodes = null;
            HtmlNodeCollection temp = null;
            temp = FetchDataFromPSX("https://www.psx.com.pk/market-summary/", "//td");
            name_nodes = temp != null ? temp : null ;
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

                for (int j = 0; j < RowData.Count(); j++)
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
                                _scripList.Add(new CurrentMarketSummary { Name = RowData[j] });
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

        #region GetCompanyNamesAndSymbols
        public void GetCompanyNames()
        {
            HtmlNodeCollection name_nodes = null;
            HtmlNodeCollection temp = null;
            temp = FetchDataFromPSX("https://www.psx.com.pk/market-summary/", "//td");
            name_nodes = temp != null ? temp : null;
            if (name_nodes != null)
            {
                List<string> RowData = new List<string>();
                int StartCapturingflag = 0;
                foreach (HtmlAgilityPack.HtmlNode node in name_nodes)
                {
                    if (StartCapturingflag == 1)
                    {
                        RowData.Add(node.InnerText.ToString());
                    }
                    else if (node.InnerText.ToString().Trim().Equals("VOLUME"))
                    {
                        StartCapturingflag = 1;
                    }
                }
                for (int j = 0; j < RowData.Count(); j++)
                {
                    if (j % 8 == 0)
                    {
                        if (RowData[j] != null)
                        {
                            if (RowData[j].Contains("SCRIP")) { }
                            else
                            {
                                _scripList.Add(new CurrentMarketSummary { Name = RowData[j], Symbol = GetCompanySymbols(RowData[j]) });
                            }
                        }
                        //rd.Close();
                    }
                }
            }
            else
            {
                Debug.WriteLine("Null Values in Market Names Method.");
            }
        }

        #endregion

        #region GetMarketSummaryCompanySymbols

        public List<string> GetMarketSummaryCompanySymbols(List<string> CompanyName)
        {
            List<string> result = new List<string>(new string[_scripList.Count]);

            #region GetMarketSummaryCompanySymbols_MSSQLSERVER
            if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("MSSQLSERVER"))
            {
                SqlConnection conn = new SqlConnection();
                try
                {

                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    using (conn)
                    {
                        conn.Open();
                        for (int i = 0; i < CompanyName.Count(); i++)
                        {
                            SqlCommand cmd = new SqlCommand("spGetSymbolFromCompanyName", conn); // Read user-> stored procedure name
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@CompanyName", SqlDbType.VarChar, 500);
                            cmd.Parameters["@CompanyName"].Value = CompanyName[i];
                            using (SqlDataReader rdr = cmd.ExecuteReader())
                            {
                                while (rdr.Read())
                                {
                                    result.Insert(i, rdr.GetString(0));
                                    _scripList[i].Symbol = rdr.GetString(0);
                                }
                            }
                        }
                        conn.Close();
                    }


                }
                catch (SqlException ex)
                {
                    Debug.WriteLine("SQL Exception in GetStatusAndMiscellenous Method: " + ex.Message);
                    Exceptions.Add("SQL Exception in GetStatusAndMiscellenous Method: " + ex.Message);
                    //Debug.WriteLine("SQL Exception: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Exception in GetStatusAndMiscellenous Method: " + ex.Message);
                    Exceptions.Add("Exception in GetStatusAndMiscellenous Method: " + ex.Message);
                    //Debug.WriteLine("Exception: " + ex.Message);
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
                string connectionString = Configuration.GetConnectionString("DefaultConnection");
                Debug.WriteLine(connectionString);
                //Debug.WriteLine(" "+ConfigurationManager.ConnectionStrings["DefaultConnection"].CurrentConfiguration.ToString());
                OracleConnection con = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=128.1.101.32)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));load balancing=false;ha events=false;User Id=IPAMS_TEST;Password=IPAMS_TEST;");
                con.Open();
                for (int i = 0; i < CompanyName.Count; i++)
                {
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
                        input.Value = CompanyName[i];
                        cmd.ExecuteNonQuery();
                        result.Insert(i, Convert.ToString(output.Value));
                        _scripList[i].Symbol = Convert.ToString(output.Value);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("==" + ex.Message);
                    }
                }
                con.Close();
            }

            #endregion

            #region GetMarketSummaryCompanySymbols_SQLITE

            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("SQLITE"))
            {
                //List<ScripInfo> scrips = _context.ScripInfo.ToList();
                ////IQueryable<ScripInfo> scrips = _context.ScripInfo.Where(sc => sc.Name.Contains(_nameDivider[0] + " " + _nameDivider[1]) || sc.Name.Contains(_nameDivider[0]));
                for (int i = 0; i < CompanyName.Count; i++)
                {
                    string[] _nameDivider = CompanyName[i].Split(" ");
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
                            result.Insert(i, scrips.ToList()[j].Symbol);
                            _scripList[i].Symbol = scrips.ToList()[j].Symbol;
                            break;
                        }
                    }
                    //    }
                    //result.Insert(i, " - ");
                }

            }

            #endregion

            #region GetMarketSummaryCompanySymbols_POSTGRE

            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("POSTGRE"))
            {
                for (int i = 0; i < CompanyName.Count; i++)
                {
                    result.Insert(i, "-");
                }
            }

            #endregion

            return result;
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
                        SqlCommand cmd = new SqlCommand("spGetSymbolFromCompanyName", conn); // Read user-> stored procedure name
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@CompanyName", SqlDbType.VarChar, 500);
                        cmd.Parameters["@CompanyName"].Value = CompanyName;
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

            #region GetCompanySymbols_ELASTICSEARCH

            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("ELASTICSEARCH"))
            {
                var node = new Uri(ConfigurationManager.AppSettings["ELASTICSEARCH_URL"]);
                var settings = new ConnectionSettings(node);
                settings.ThrowExceptions(alwaysThrow: true); // I like exceptions
                settings.PrettyJson(); // Good for DEBUG
                var client = new ElasticClient(settings);

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
            HtmlNodeCollection nodes = _webDataCollectionForCategory;
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
                                _scripList.Add(new CurrentMarketSummary { Name = _filteredData[j], Symbol = GetCompanySymbols(_filteredData[j]), Category = _tempCategory, Ldcp = _filteredData[_scriptor], Open = _filteredData[_scriptor + 1], High = _filteredData[_scriptor + 2], Low = _filteredData[_scriptor + 3], Current = _filteredData[_scriptor + 4], Change = _filteredData[_scriptor + 5].Trim().Replace(" ", ""), Volume = _filteredData[_scriptor + 6] });
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
                                    _scripList.Add(new CurrentMarketSummary { Name = _filteredData[j], Symbol = GetCompanySymbols(_filteredData[j]), Category = _tempCategory, Ldcp = _filteredData[_scriptor], Open = _filteredData[_scriptor + 1], High = _filteredData[_scriptor + 2], Low = _filteredData[_scriptor + 3], Current = _filteredData[_scriptor + 4], Change = _filteredData[_scriptor + 5].Trim().Replace(" ", ""), Volume = _filteredData[_scriptor + 6] });
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

        #region GetMarketSummaryCompanyLDCP

        private string[] GetMarketSummaryCompanyLDCP()
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
            for (int j = 1; j < AllTableRowData.Count(); j = j + 8)
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
                        _scripList[j].Ldcp = AllTableRowData[j];
                    }
                }
            }
            return result;
        }

        #endregion

        #region GetCompanyLDCP

        public void GetCompanyLDCP()
        {
            Debug.WriteLine("GetCompanyLDCP");
            HtmlNodeCollection name_nodes = _webDataCollection;
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
            }
            int counter2 = 0;
            for (int j = 1; j < AllTableRowData.Count(); j = j + 8)
            {
                if (AllTableRowData[j] != null)
                {
                    if (AllTableRowData[j].Trim().Equals("LDCP"))
                    {
                    }
                    else
                    {
                        _scripList[counter2++].Ldcp = AllTableRowData[j].Trim().Replace("\n", "");
                    }
                }
            }
        }

        #endregion

        #region GetMarketSummaryCompanyOPEN
        public string[] GetMarketSummaryCompanyOPEN()
        {
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
            for (int j = 2; j < AllTableRowData.Count(); j = j + 8)
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
                        _scripList[j].Open = AllTableRowData[j];
                    }
                }
            }
            return result;
        }
        #endregion

        #region GetCompanyOPEN
        public void GetCompanyOPEN()
        {
            Debug.WriteLine("GetCompanyOPEN");
            HtmlNodeCollection name_nodes = _webDataCollection;
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
            for (int j = 2; j < AllTableRowData.Count(); j = j + 8)
            {
                if (AllTableRowData[j] != null)
                {
                    if (AllTableRowData[j].Trim().Equals("OPEN"))
                    {
                    }
                    else
                    {
                        _scripList[counter2++].Open = AllTableRowData[j].Trim().Replace("\n", "");
                    }
                }
            }
        }
#endregion

        #region GetMarketSummaryCompanyHIGH

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
            for (int j = 3; j < AllTableRowData.Count(); j = j + 8)
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
                        //_scripList[j].High = AllTableRowData[j];
                    }
                }
            }
            return result;
        }

        #endregion

        #region GetCompanyHIGH
        public void GetCompanyHIGH()
        {
            Debug.WriteLine("GetCompanyHIGH");
            HtmlNodeCollection name_nodes = _webDataCollection;
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
            }
            int counter2 = 0;
            for (int j = 3; j < AllTableRowData.Count(); j = j + 8)
            {
                if (AllTableRowData[j] != null)
                {
                    if (AllTableRowData[j].Trim().Equals("HIGH")) { }
                    else
                    {
                        _scripList[counter2++].High = AllTableRowData[j].Trim().Replace("\n", "");
                    }
                }
            }
        }

        #endregion

        #region GetMarketSummaryCompanyLOW

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
            for (int j = 4; j < AllTableRowData.Count(); j = j + 8)
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

#endregion

        #region GetCompanyLOW
        public void GetCompanyLOW()
        {
            Debug.WriteLine("GetCompanyLOW");
            HtmlNodeCollection name_nodes = _webDataCollection;
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
            for (int j = 4; j < AllTableRowData.Count(); j = j + 8)
            {
                if (AllTableRowData[j] != null)
                {
                    if (AllTableRowData[j].Trim().Equals("LOW")) { }
                    else
                    {
                        _scripList[counter2++].Low = AllTableRowData[j].Trim().Replace("\n", "");
                    }
                }
            }
        }

        #endregion

        #region GetMarketSummaryCompanyCURRENT

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
                for (int j = 5; j < AllTableRowData.Count(); j = j + 8)
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

        #endregion

        #region GetCompanyCURRENT
        public void GetCompanyCURRENT()
        {
            Debug.WriteLine("GetCompanyCURRENT");
            string[] result = null;
            HtmlNodeCollection name_nodes = null;
            name_nodes = _webDataCollection != null ? _webDataCollection : name_nodes;
            if (name_nodes != null)
            {
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
                }
                int counter2 = 0;
                for (int j = 5; j < AllTableRowData.Count(); j = j + 8)
                {
                    if (AllTableRowData[j] != null)
                    {
                        if (AllTableRowData[j].Trim().Equals("CURRENT")) { }
                        else
                        {
                            _scripList[counter2++].Current = AllTableRowData[j].Trim().Replace("\n", "");
                        }
                    }
                }
            }
        }

        #endregion

        #region GetMarketSummaryCompanyCHANGE
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
            for (int j = 6; j < AllTableRowData.Count(); j = j + 8)
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

        #endregion

        #region GetCompanyCHANGE
        public void GetCompanyCHANGE()
        {
            Debug.WriteLine("GetCompanyCHANGE");
            HtmlNodeCollection name_nodes = _webDataCollection;
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
            }
            int counter2 = 0;
            for (int j = 6; j < AllTableRowData.Count(); j = j + 8)
            {
                if (AllTableRowData[j] != null)
                {
                    if (AllTableRowData[j].Trim().Equals("CHANGE")) { }
                    else
                    {
                        _scripList[counter2++].Change = AllTableRowData[j].Trim().Replace("\n", "");
                    }
                }
            }
        }

        #endregion

        #region GetMarketSummaryCompanyVOLUME

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
            for (int j = 7; j < AllTableRowData.Count(); j = j + 8)
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

        #region GetCompanyVOLUME
        public void GetCompanyVOLUME()
        {
            Debug.WriteLine("GetCompanyVOLUME");
            HtmlNodeCollection name_nodes = _webDataCollection;
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
            }
            int counter2 = 0;
            for (int j = 7; j < AllTableRowData.Count(); j = j + 8)
            {
                if (AllTableRowData[j] != null)
                {
                    if (AllTableRowData[j].Trim().Equals("VOLUME")) { }
                    else
                    {
                        _scripList[counter2++].Volume = AllTableRowData[j].Trim().Replace("\n", "");
                    }
                }
            }
            Debug.WriteLine("GetCompanyVOLUME finished");
        }

        #endregion

        #region SavingDataToDatabase

        private bool SavingDataToDatabase(string _date, string _status, string _volume, string _value, string _trades, CurrentMarketSummary _scrip)
        {
            #region SavingDataToDatabase_MSSQLSERVER
            if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("MSSQLSERVER"))
            {
                if (_date != null)
                {

                    SqlConnection conn = new SqlConnection();
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                    conn.Open();

                    int isDataCleared = 0;

                    //int i = 0;
                    int k = 0;
                    int j = 0;
                    int l = 0;

                    //Clear Truncate Current Market Summary Data

                    SqlCommand cmdspClearTableData = new SqlCommand("spTRUNCATE_CURRENT_MARKET_SUMMARY", conn);
                    cmdspClearTableData.CommandType = CommandType.StoredProcedure;

                    //

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
                    cmdspInsertMarketSummaryOverview.Parameters["@TRADE"].Value = Convert.ToDouble(_trades.Replace(",", ""));

                    //

                    try
                    {

                        isDataCleared = cmdspClearTableData.ExecuteNonQuery();
                        k = cmdspInsertMarketSummaryOverview.ExecuteNonQuery();

                    }
                    catch (SqlException ex)
                    {
                        Debug.WriteLine("SQL Exception in SavingDataToDatabase Method: " + ex.Message);
                        Exceptions.Add("SQL Exception in SavingDataToDatabase Method: " + ex.Message);
                        //Debug.WriteLine("SQL Exception Occured: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("SQL Exception in SavingDataToDatabase Method: " + ex.Message);
                        Exceptions.Add("SQL Exception in SavingDataToDatabase Method: " + ex.Message);
                        //Debug.WriteLine("Exception: " + ex.Message);
                    }

                    //for (int m = 0; m < .Count(); m++)
                    //{
                    try
                    {
                        //
                        if (_scripList == null)
                        {

                        }
                        else
                        {
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

                            //int nameFlag = 5;

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


                            j = cmdspInsertMarketSummary.ExecuteNonQuery();
                            l = cmdForspInsertMarketSummaryHistory.ExecuteNonQuery();

                        }
                    }
                    catch (SqlException ex)
                    {
                        Debug.WriteLine("SQL Exception in SavingDataToDatabase Method: " + ex.Message);
                        Exceptions.Add("SQL Exception in SavingDataToDatabase Method: " + ex.Message);
                        //MessageBox.Show(ex.Message, "SQL Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                        //Debug.WriteLine("SQL Exception: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Exception in SavingDataToDatabase Method: " + ex.Message);
                        Exceptions.Add("Exception in SavingDataToDatabase Method: " + ex.Message);
                        //MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                        //Debug.WriteLine("Exception: " + ex.Message);
                    }
                    //}

                    //

                    if (j == 1 && k == 1 && l == 1)
                    {
                        //Debug.WriteLine("Data successfully saved.");
                        return true;
                    }

                    conn.Close();

                    return false;
                }
                else
                {
                    return false;
                }
            }

            #endregion

            #region SavingDataToDatabase_ORACLE
            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("ORACLE"))
            {
                return false;
            }
            #endregion

            #region SavingDataToDatabase_SQLITE
            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("SQLITE"))
            {
                return false;
            }
            #endregion

            #region SavingDataToDatabase_POSTGRE
            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("POSTGRE"))
            {
                return false;
            }
            #endregion

            else
            {
                return false;
            }

        }

        #endregion

        #region Truncate_CURRENT_MARKET_SUMMARY_DB

        private int Truncate_CURRENT_MARKET_SUMMARY_DB()
        {
            #region SavingDataToDatabase_MSSQLSERVER
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
            int isMetaDataSaved = 0;
            #region INSERT_CURRENT_MARKET_OVERVIEW_DB_MSSQLSERVER
            if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("MSSQLSERVER"))
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                conn.Open();
                
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
    //Clear Truncate Current Market Summary Data
    SqliteCommand cmd = new SqliteCommand("TRUNCATE TABLE TRUNCATE_CURRENT_MARKET_SUMMARY;", conn);
    cmd.CommandType = CommandType.Text;
    try
    {
                    isMetaDataSaved = cmd.ExecuteNonQuery();
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
    return isMetaDataSaved;
}
            #endregion

            #region INSERT_CURRENT_MARKET_OVERVIEW_DB_POSTGRE
            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("POSTGRE"))
            {
                NpgsqlConnection conn = new NpgsqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                conn.Open();
                //Clear Truncate Current Market Summary Data
                NpgsqlCommand cmd = new NpgsqlCommand("TRUNCATE TABLE TRUNCATE_CURRENT_MARKET_SUMMARY;", conn);
                cmd.CommandType = CommandType.Text;
                try
                {
                    isMetaDataSaved = cmd.ExecuteNonQuery();
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
                return isMetaDataSaved;
            }
            #endregion

        #region INSERT_CURRENT_MARKET_OVERVIEW_DB_Elasticsearch

    else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("ELASTICSEARCH"))
    {
        //NpgsqlConnection conn = new NpgsqlConnection();
        //conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        //conn.Open();
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
        return isMetaDataSaved;
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
                if (_scripList != null)
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

#region worker_DoWork
private void worker_DoWork(object sender, DoWorkEventArgs e)
{
    mustWork();
}
#endregion

#region worker_ProgressChanged

private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
{
    progressBar.Value = e.ProgressPercentage;
    if (e.UserState != null)
        lblProgress.Content = e.UserState;
}

#endregion

#region worker_RunWorkerCompleted
private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
{
    if (_scripList.Count != 0)
    {
        Debug.WriteLine("worker_RunWorkerCompleted Block");
        PreviewWindow window = new PreviewWindow(_miscellenousData["DATE"], _miscellenousData["STATUS"], _miscellenousData["VALUE"].Replace(",", ""), _miscellenousData["VOLUME"].Replace(",", ""), _miscellenousData["TRADES"].Replace(",", ""), _scripList);
        btnGet.IsEnabled = false;
        window.Show();
        this.Hide();
    }
    else
    {
        MessageBox.Show("Exception: " + Exceptions[Exceptions.Count - 1], "Something went wrong..", MessageBoxButton.OK, MessageBoxImage.Error);
        Debug.WriteLine("Exception Details: " + Exceptions[Exceptions.Count - 1]);
        btnGet.IsEnabled = true;
        btnConfigure.IsEnabled = true;
        btnGetV2.IsEnabled = true;
        btnGetV3.IsEnabled = true;
        btnMufapGetMarketSummary.IsEnabled = true;
        btnMufapGetPKRV.IsEnabled = true;
        btnMufapGetPKFRV.IsEnabled = true;
        progressBar.Visibility = Visibility.Hidden;
        lblProgress.Visibility = Visibility.Hidden;
        imgWebScrap.Visibility = Visibility.Visible;
    }
}
#endregion

#region MustWork
public void mustWork()
{

                GetDefault();
                //List<string> catData = GetCategoryData();
                GetScripDetails();
                //foreach (string item in catData)
                //{
                //    Debug.WriteLine("CatData: " + item);
                //}
                if (_miscellenousData["DATE"] != "")
                {
                    worker.WorkerReportsProgress = true;
                    statusContent = "Getting General Content..";
                    worker.ReportProgress(10);
                    DateTime CurrentTime = DateTime.Parse(_miscellenousData["DATE"]);
                    Debug.WriteLine("Current Date: " + CurrentTime);
                    DateTime ExpiredTime = ExpiryDate;
                    Debug.WriteLine("Expired Date: " + ExpiredTime);
                    if (ExpiredTime <= CurrentTime)
                    {
                        MessageBox.Show("The license of Data Extractor Utility is expired. Please contact your system administrator or software vendor.", "License Expired", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        statusContent = "Getting Scrip Names and Symbols..";
                        worker.ReportProgress(50);
                        //GetCompanyNames();
                        statusContent = "Getting Scrip Details..";
                        worker.ReportProgress(70);
                        //GetCompanyLDCP();
                        //GetCompanyOPEN();
                        //GetCompanyHIGH();
                        //GetCompanyLOW();
                        //GetCompanyCURRENT();
                        //GetCompanyCHANGE();
                        //GetCompanyVOLUME();
                        statusContent = "Dumping in Database..";
                        Truncate_CURRENT_MARKET_SUMMARY_DB();
                        Truncate_CURRENT_MARKET_OVERVIEW_DB();
                        INSERT_CURRENT_MARKET_OVERVIEW_DB(_miscellenousData["DATE"], _miscellenousData["STATUS"], _miscellenousData["VOLUME"], _miscellenousData["VALUE"], _miscellenousData["TRADES"]);
                        worker.ReportProgress(98);
                        for (int i = 0; i < _scripList.Count; i++)
                        {
                            //isDataSaved = SavingDataToDatabase(_miscellenousData["DATE"], _miscellenousData["STATUS"], _miscellenousData["VOLUME"], _miscellenousData["VALUE"], _miscellenousData["TRADES"], _scripList[i]);
                            SavingScrip(_miscellenousData["DATE"], _miscellenousData["STATUS"], _miscellenousData["VOLUME"], _miscellenousData["VALUE"], _miscellenousData["TRADES"], _scripList[i]);
                        }
                        worker.ReportProgress(100);
                    }
                }
                else
                {
                    MessageBox.Show("Data Not Found.", "Web Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            
    //for (int i = 0; i < NAME.Count(); i++)
    //{

    //            Debug.WriteLine("Exception in MustWork Method: " + ex.Message);
    //            Exceptions.Add("Exception in MustWork Method: " + ex.Message);
    //}
    //isDataSaved = SavingDataToDatabase(defaultData, NAME, SYMBOL, getCompanyLDCP, getCompanyOPEN, getCompanyHIGH, getCompanyLOW, getCompanyCURRENT, getCompanyCHANGE, getCompanyVOLUME);
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

}

#endregion

        #region Windows_Closing
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            //MessageBoxResult d;
            //d = MessageBox.Show("Do you really want to close Data Extractor Utility?", "Data Extractor Utility", MessageBoxButton.YesNo, MessageBoxImage.Question);
            //if (d == MessageBoxResult.Yes)
            //{
                Application.Current.Shutdown();
            //}
            //else {
            //    e.Cancel = true;
            //}
        }
        #endregion

        private void btnGetV3_Click(object sender, RoutedEventArgs e)
        {
            UploadPSXData uploadPSX = new UploadPSXData();
            uploadPSX.Show();
            this.Hide();
        }
    }
}
