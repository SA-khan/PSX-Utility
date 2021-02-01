using PSXDataFetchingApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using WpfAnimatedGif;
using System.IO;
using System.Data.Common;
using static PSXDataFetchingApp.PreviewWindow;
using ClosedXML.Excel;
using System.Xml;
using OfficeOpenXml;
using System.Drawing;
using OfficeOpenXml.Style;
using System.Threading;
using System.Net.Mail;
using PSXDataFetchingApp.Model.BindingTargets;
using System.Linq;
using HtmlAgilityPack;
using Oracle.ManagedDataAccess.Client;
using System.Windows.Markup;

namespace PSXDataFetchingApp
{
    /// <summary>
    /// Interaction logic for FundPreviewWindow.xaml
    /// </summary>
    public partial class FundPreviewWindow : Window
    {
        //Automation Process Initial
        public DataContext _context;

        //Default MS SQL Connections
        public static SqlConnection connDefault = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        //IPAMS MS SQL Connection
        public static SqlConnection connIpams = new SqlConnection(ConfigurationManager.ConnectionStrings["IpamsConnection"].ConnectionString);

        //Get All Exceptions
        public List<string> Exceptions = new List<string>();

        //Get All HtmlAgilityTags
        public HtmlNodeCollection _webDataCollection = null;

        //Get Closing Percentage from Configuration File
        public static decimal CLOSING_PERCENTAGE = Convert.ToDecimal(ConfigurationManager.AppSettings["FundClosingPercentage"]);

        //Market Date, Status and other Variable Declarations
        public Dictionary<string, string> _miscellenousData = new Dictionary<string, string>(){
                                                            {"DATE", ""},
                                                            {"STATUS", ""},
                                                            {"VOLUME", ""},
                                                            {"TRADES", ""},
                                                            {"VALUE", ""},
                                                            {"FUND_ID", "0"},
                                                            {"FUND_NAME", ""},
                                                        };

        public static List<CurrentMarketSummary> shareList = new List<CurrentMarketSummary>();

        GridView myGridView = new GridView();
        GridViewColumn gvc1 = new GridViewColumn();
        GridViewColumn gvc2 = new GridViewColumn();
        GridViewColumn gvc3 = new GridViewColumn();
        GridViewColumn gvc4 = new GridViewColumn();
        GridViewColumn gvc5 = new GridViewColumn();
        GridViewColumn gvc6 = new GridViewColumn();
        GridViewColumn gvc7 = new GridViewColumn();
        GridViewColumn gvc8 = new GridViewColumn();
        GridViewColumn gvc9 = new GridViewColumn();
        GridViewColumn gvc10 = new GridViewColumn();
        GridViewColumn gvc11 = new GridViewColumn();

        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        int countTimer = 0;

        //New Edition 11/17/2020
        //By Saad
        List<FundwiseMarketSummary> fundList = new List<FundwiseMarketSummary>();

        //New Edition 11/18/2020
        //By Saad
        bool resetFlag = false;

        //New Edition 11/23/2020
        public static bool FundPopUpWindowFlag = false;

        public HtmlNodeCollection _webDataCollectionForCategory = null;

        //List Categories
        public List<string> _categoryList = new List<string>() { "", "AUTOMOBILE ASSEMBLER", "AUTOMOBILE PARTS & ACCESSORIES", "CABLE & ELECTRICAL GOODS", "CEMENT", "CHEMICAL", "CLOSE - END MUTUAL FUND", "COMMERCIAL BANKS", "ENGINEERING", "FERTILIZER", "FOOD & PERSONAL CARE PRODUCTS", "GLASS & CERAMICS", "INSURANCE", "INV. BANKS / INV. COS. / SECURITIES COS.", "LEASING COMPANIES", "LEATHER & TANNERIES", "MISCELLANEOUS", "MODARABAS", "OIL & GAS EXPLORATION COMPANIES", "OIL & GAS MARKETING COMPANIES", "PAPER & BOARD", "PHARMACEUTICALS", "POWER GENERATION & DISTRIBUTION", "REFINERY", "SUGAR & ALLIED INDUSTRIES", "SYNTHETIC & RAYON", "TECHNOLOGY & COMMUNICATION", "TEXTILE COMPOSITE", "TEXTILE SPINNING", "TEXTILE WEAVING", "TRANSPORT", "VANASPATI & ALLIED INDUSTRIES", "WOOLLEN", "REAL ESTATE INVESTMENT TRUST", "EXCHANGE TRADED FUNDS", "FUTURE CONTRACTS" };
        //public static List<string> _categoryList = new List<string>();
        public List<string> _categoryList2 = new List<string>() { "AUTOMOBILE ASSEMBLER", "AUTOMOBILE PARTS & ACCESSORIES", "CABLE & ELECTRICAL GOODS", "CEMENT", "CHEMICAL", "CLOSE - END MUTUAL FUND", "COMMERCIAL BANKS", "ENGINEERING", "FERTILIZER", "FOOD & PERSONAL CARE PRODUCTS", "GLASS & CERAMICS", "INSURANCE", "INV. BANKS / INV. COS. / SECURITIES COS.", "LEASING COMPANIES", "LEATHER & TANNERIES", "MISCELLANEOUS", "MODARABAS", "OIL & GAS EXPLORATION COMPANIES", "OIL & GAS MARKETING COMPANIES", "PAPER & BOARD", "PHARMACEUTICALS", "POWER GENERATION & DISTRIBUTION", "REFINERY", "SUGAR & ALLIED INDUSTRIES", "SYNTHETIC & RAYON", "TECHNOLOGY & COMMUNICATION", "TEXTILE COMPOSITE", "TEXTILE SPINNING", "TEXTILE WEAVING", "TOBACCO", "TRANSPORT", "VANASPATI & ALLIED INDUSTRIES", "WOOLLEN", "REAL ESTATE INVESTMENT TRUST", "EXCHANGE TRADED FUNDS", "FUTURE CONTRACTS" };
        public List<string> _categoryList3 = new List<string>();

        public static List<CurrentMarketSummary> _scripList = new List<CurrentMarketSummary>();

        #region FundPreviewWindow_
        public FundPreviewWindow()
        {
            InitializeComponent();
            txtDate.Text = "Date:" + DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm tt");
            ClientSideProperties();
        }

        #endregion

        #region FundPreviewWindow

        public FundPreviewWindow(DataContext ctx)
        {
            InitializeComponent();
            _context = ctx;
            txtDate.Text = "Date:" + DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm tt");
            ClientSideProperties();

            comboCategory.Items.Add("All Categories");
            comboCategory.SelectedIndex = 0;

            #region FundPreviewWindow_MSSQLSERVER

            if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("MSSQLSERVER"))
            {
                try
                {
                    List<String> fundName = getFundNameList(connDefault, true);
                    fundName.AddRange(getFundNameList(connIpams, false));
                    comboFund.Items.Add("Select..");
                    comboFund.SelectedIndex = 0;
                    for (int i = 0; i < fundName.Count; i++)
                    {
                        comboFund.Items.Add(fundName[i]);
                    }
                    comboFund.Items.Add("<ADD NEW FUND>");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database connectivity failed with the exception.:\nException: " + ex.Message, "Database Connectivity Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    connDefault.Close();
                    connIpams.Close();
                }
            }

            #endregion

            #region FundPreviewWindow_SQLITE

            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("SQLITE"))
            {
                try
                {
                    comboFund.Items.Add("Select..");
                    IEnumerable<FundInfo> fundName = _context.FundInfo;
                    foreach (FundInfo item in fundName)
                        comboFund.Items.Add(item.Name);
                    comboFund.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("No Data Found: " + ex.Message);
                }
                try
                {
                    List<string> fundName = new List<string>();
                    SqlConnection conn = new SqlConnection();
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["IpamsConnection"].ConnectionString;
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("spGET_FUND_NAMES", conn); // Read user-> stored procedure name
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            fundName.Add(rdr.GetString(4));

                        }
                    }
                    conn.Close();
                    foreach (string item in fundName)
                        comboFund.Items.Add(item);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("No Data Found: " + ex.Message);
                }
                comboFund.Items.Add("<ADD NEW FUND>");
            }

            #endregion

            #region FundPreviewWindow_ORACLE

            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("ORACLE"))
            {
                //try
                //{
                comboFund.Items.Add("Select..");
                comboFund.SelectedIndex = 0;
                //    IEnumerable<FundInfo> fundName = _context.FundInfo;
                //    foreach (FundInfo item in fundName)
                //        comboFund.Items.Add(item.Name);
                //    comboFund.SelectedIndex = 0;
                //}
                //catch (Exception ex)
                //{
                //    Debug.WriteLine("No Data Found: " + ex.Message);
                //}
                try
                {
                    List<string> fundName = new List<string>();
                    //Oracle.ManagedDataAccess.Client.OracleConfiguration.LoadBalancing = false;
                    //Oracle.ManagedDataAccess.Client.OracleConfiguration.HAEvents = false;
                    OracleConnection conn = new OracleConnection();
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["IpamsConnection"].ConnectionString;
                    conn.Open();
                    OracleCommand cmd = new OracleCommand("SELECT * FROM TABLE(fnc_Get_funds_Pipelined())", conn);
                    cmd.CommandType = CommandType.Text;
                    using (OracleDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            fundName.Add(rdr.GetString(0));

                        }
                    }
                    conn.Close();
                    foreach (string item in fundName)
                        comboFund.Items.Add(item);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("No Data Found: " + ex.Message);
                }
                comboFund.Items.Add("<ADD NEW FUND>");
            }

            #endregion

        }

        #endregion

        #region Execute

        public void Execute(string queryString, string connectionString)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        #endregion

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
                        CriteriaSection.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#b9c9d3");
                        HeaderColor.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#76b0cc");
                        Footer.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#76b0cc");

                        //Setting Logo
                        var image = new BitmapImage();
                        image.BeginInit();
                        image.UriSource = ResourceAccessor.Get("Images/astech.png");
                        image.EndInit();
                        ImageBehavior.SetAnimatedSource(HeaderImage, image);
                    }

                    #endregion

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

                        Footer.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#f0a500");
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

                        Footer.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#008269");
                    }
                    #endregion

                    #region ClientSideProperties_EFU

                    if (ConfigurationManager.AppSettings["Client"].Equals("EFU"))
                    {
                        // Header Background Color 
                        var bc = new BrushConverter();
                        CriteriaSection.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#b9c9d3");
                        HeaderColor.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#01808d");
                        Footer.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#01808d");

                        //Setting Logo
                        var image = new BitmapImage();
                        image.BeginInit();
                        image.UriSource = ResourceAccessor.Get("Images/efu.gif");
                        image.EndInit();
                        ImageBehavior.SetAnimatedSource(HeaderImage, image);
                    }

                    #endregion

                }
            }
            catch { }
        }
        #endregion

        #region getFundNameList

        public List<String> getFundNameList(SqlConnection connectionName, bool defaultDB)
        {
            List<String> result = new List<string>();
            if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("MSSQLSERVER"))
            {
                try
                {
                    using (connectionName)
                    {
                        connectionName.Open();
                        SqlCommand cmd = new SqlCommand("spGET_FUND_NAMES", connectionName); // Read user-> stored procedure name
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                if (defaultDB)
                                {
                                    result.Add(rdr.GetString(2));
                                }
                                else
                                {
                                    result.Add(rdr.GetString(4));
                                }
                            }
                        }
                        connectionName.Close();
                    }

                }
                catch (SqlException ex)
                {
                    Debug.WriteLine("SQL Exception: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Exception: " + ex.Message);
                }
                finally
                {
                    connectionName.Close();
                }
            }
            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("SQLITE"))
            {

            }
            return result;
        }

        #endregion

        #region getSymbolStatus

        public bool getSymbolStatus(string Symbol)
        {
            bool result = false;

            #region getSymbolStatus_MSSQLSERVER

            if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("MSSQLSERVER"))
            {
                SqlConnection conn = new SqlConnection();
                try
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    using (conn)
                    {
                        conn.Open();

                        SqlCommand cmd = new SqlCommand("spIsGetCompanySymbolExist", conn); // Read user-> stored procedure name
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@SYMBOL", SqlDbType.VarChar, 500);
                        cmd.Parameters["@SYMBOL"].Value = Symbol;
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                result = Convert.ToBoolean(rdr.GetInt64(0));
                            }
                        }

                        conn.Close();
                    }

                }
                catch (SqlException ex)
                {
                    Debug.WriteLine("SQL Exception: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Exception: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }

            #endregion

            #region getSymbolStatus_ORACLE

            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("ORACLE"))
            {
                OracleConnection con = new OracleConnection();//"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=128.1.101.32)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));load balancing=false;ha events=false;User Id=IPAMS_TEST;Password=IPAMS_TEST;"
                con.ConnectionString = ConfigurationManager.ConnectionStrings["IpamsConnection"].ConnectionString;
                con.Open();
                try
                {
                    OracleCommand cmd = new OracleCommand()
                    {
                        CommandText = "spIsGetCompanySymbolExist",
                        Connection = con,
                        CommandType = CommandType.StoredProcedure,
                    };
                    OracleParameter input = cmd.Parameters.Add("SYMBOL", OracleDbType.Varchar2);
                    OracleParameter output = cmd.Parameters.Add("EXIST", OracleDbType.Decimal);
                    input.Direction = ParameterDirection.Input;
                    output.Direction = ParameterDirection.Output;
                    input.Value = Symbol;
                    cmd.ExecuteNonQuery();
                    result = output.Value.ToString() == "1" ? true : false;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("==" + ex.Message);
                }
                con.Close();
            }

            #endregion

            #region getSymbolStatus_SQLITE

            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("SQLITE"))
            {
                IEnumerable<ScripInfo> _scrips_new_list = _context.ScripInfo;
                foreach (ScripInfo item in _scrips_new_list)
                {
                    if (item.Symbol.Equals(Symbol))
                    {
                        result = true;
                        break;
                    }
                }
            }

            #endregion

            return result;
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

        #region getDefault

        public void getDefault()
        {
            HtmlNodeCollection temp = null;
            temp = FetchDataFromPSX("https://www.psx.com.pk/market-summary/", "//h4");
            _webDataCollection = temp != null ? temp : null;
            if (_webDataCollection != null)
            {
                _miscellenousData["DATE"] = "";
                _miscellenousData["STATUS"] = "";
                _miscellenousData["VOLUME"] = "";
                _miscellenousData["VALUE"] = "";
                _miscellenousData["TRADES"] = "";
                foreach (HtmlAgilityPack.HtmlNode node in _webDataCollection)
                {
                    if (node.InnerText.ToString().StartsWith("* LDCP")) { }
                    else if (node.InnerText.ToString().StartsWith(DateTime.Now.Year.ToString()))
                    {
                        DateTime localDate = DateTime.Parse(node.InnerText != null ? node.InnerText : "Nil");
                        _miscellenousData["DATE"] = localDate != null ? localDate.ToString("dddd, dd MMMM yyyy hh:mm tt") : "Nil";
                    }
                    else if (node.InnerText.ToString().StartsWith("Status"))
                    {
                        string localstatus = ""+node.InnerText.Replace("Status: ", "").Replace(" ", "");
                        _miscellenousData["STATUS"] = node.InnerText != null ? localstatus : "Nil";
                    }
                    else if (node.InnerText.ToString().StartsWith("Volume"))
                    {
                        string localVolume = ""+node.InnerText.Replace("Volume: ", "");
                        _miscellenousData["VOLUME"] = localVolume != null ? localVolume : "Nil";
                    }
                    else if (node.InnerText.ToString().StartsWith("Value"))
                    {
                        string localValue = ""+node.InnerText.ToString().Replace("Value : ", "");
                        _miscellenousData["VALUE"] = localValue != null ? localValue : "Nil";
                    }
                    else if (node.InnerText.ToString().StartsWith("Trades"))
                    {
                        string localTrades = node.InnerText.ToString().Replace("Trades: ", "");
                        _miscellenousData["TRADE"] = localTrades != null ? localTrades : "Nil";
                    }
                }

            }

        }

        #endregion

        #region GetMarketSummaryCompanyNames
        public List<string> GetMarketSummaryCompanyNames()
        {
            // Local List of string Variable to store return value 
            List<string> result = new List<string>();

            // Get All Nodes of td tag
            HtmlNodeCollection name_nodes = null;
            _webDataCollection = FetchDataFromPSX("https://www.psx.com.pk/market-summary/", "//td");
            name_nodes = _webDataCollection != null ? _webDataCollection : null;
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

        #region GetCompanyNames
        public void GetCompanyNames()
        {
            HtmlNodeCollection name_nodes = null;
            _webDataCollection = FetchDataFromPSX("https://www.psx.com.pk/market-summary/", "//td");
            name_nodes = _webDataCollection != null ? _webDataCollection : null;
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
                if (RowData != null)  shareList.Clear();
                for (int j = 0; j < RowData.Count(); j++)
                {
                    if (j % 8 == 0)
                    {
                        if (RowData[j] != null)
                        {
                            if (RowData[j].Contains("SCRIP")) { }
                            else
                            {
                                shareList.Add(new CurrentMarketSummary { Name = RowData[j].Trim().Replace(" ", ""), Symbol = GetCompanySymbols(RowData[j]) });
                            }
                        }
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
            List<string> result = new List<string>(new string[CompanyName.Count]);

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

            }

            #endregion

            #region GetMarketSummaryCompanySymbols_SQLITE
            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("SQLITE"))
            {
                List<ScripInfo> scrips = _context.ScripInfo.ToList();
                if (scrips != null)
                {
                    try
                    {
                        for (int i = 0;  i < scrips.Count; i++)
                        {
                            foreach (var internetscrip in CompanyName)
                            {
                                string[] _nameDivider = internetscrip.Split(" ");
                                int _nameLength = _nameDivider.Length;
                                string _tempName = String.Empty;
                                switch (_nameLength)
                                {
                                    case 1:
                                        _tempName = internetscrip;
                                        break;
                                    case 2:
                                        _tempName = _nameDivider[0] + " " + _nameDivider[1];
                                        break;
                                    case 3:
                                        _tempName = _nameDivider[0] + " " + _nameDivider[1] + " " + _nameDivider[2];
                                        break;
                                    default:
                                        _tempName = internetscrip;
                                        break;
                                }
                                if (scrips[i].Name.Contains(_tempName))
                                {
                                    result.Insert(i, scrips[i].Symbol);
                                }
                            }
                        }
                        //for (int i = 0; i < CompanyName.Count(); i++)
                        //{
                        //    string[] _nameDivider = CompanyName[i].Split(" ");
                        //    //int _nameLength = _nameDivider.Length;
                        //    //Debug.WriteLine("Name Length: " + _nameLength);
                        //    //string _tempName = String.Empty;
                        //    //switch (_nameLength)
                        //    //{
                        //    //    case 1:
                        //    //        _tempName = CompanyName[i];
                        //    //        break;
                        //    //    case 2:
                        //    //        _tempName = _nameDivider[0] + " " + _nameDivider[1];
                        //    //        break;
                        //    //    case 3:
                        //    //        _tempName = _nameDivider[0] + " " + _nameDivider[1] + " " + _nameDivider[2];
                        //    //        break;
                        //    //    default:
                        //    //        _tempName = CompanyName[i];
                        //    //        break;
                        //    //}
                        //    Debug.WriteLine("Internet Data: " + CompanyName[i]);
                        //    for (int j = 0; j < scrips.Count(); j++)
                        //    {
                        //        if (scrips[j].Name.Contains(_nameDivider[0]))
                        //        {
                        //            result.Insert(i, scrips[j].Symbol);
                        //            break;
                        //        }
                        //    }
                        //}
                    }
                    catch(Exception ex)
                    {
                        Debug.WriteLine("Here i am: " + ex.Message);
                        Exceptions.Add("Exception in GetMarketSummaryCompanySymbols IN SQLITE Block Method: " + ex.Message);
                    }
                }

            }
            #endregion

            #region GetMarketSummaryCompanySymbols_POSTGRE
            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("POSTGRE"))
            {

            }
            #endregion

            return result;
        }
        #endregion

        #region GetCompanySymbols

        //public string GetCompanySymbols(string CompanyName)
        //{
        //    string result = String.Empty;

        //    #region GetCompanySymbols_MSSQLSERVER
        //    if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("MSSQLSERVER"))
        //    {
        //        SqlConnection conn = new SqlConnection();
        //        try
        //        {
        //            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        //            using (conn)
        //            {
        //                conn.Open();
        //                SqlCommand cmd = new SqlCommand("spGetSymbolFromCompanyName", conn); // Read user-> stored procedure name
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.Add("@CompanyName", SqlDbType.VarChar, 500);
        //                cmd.Parameters["@CompanyName"].Value = CompanyName;
        //                using (SqlDataReader rdr = cmd.ExecuteReader())
        //                {
        //                    while (rdr.Read())
        //                    {
        //                        result = rdr.GetString(0);
        //                    }
        //                }
        //                conn.Close();
        //            }
        //        }
        //        catch (SqlException ex)
        //        {
        //            Debug.WriteLine("SQL Exception in GetCompanySymbols_MSSQLSERVER Method: " + ex.Message);
        //            Exceptions.Add("SQL Exception in GetCompanySymbols_MSSQLSERVER Method: " + ex.Message);
        //        }
        //        catch (Exception ex)
        //        {
        //            Debug.WriteLine("Exception in GetStatusAndMiscellenous Method: " + ex.Message);
        //            Exceptions.Add("Exception in GetStatusAndMiscellenous Method: " + ex.Message);
        //        }
        //        finally
        //        {
        //            conn.Close();
        //        }
        //    }
        //    #endregion

        //    #region GetMarketSummaryCompanySymbols_ORACLE
        //    else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("ORACLE"))
        //    {
        //        //string connectionString = Configuration.GetConnectionString("DefaultConnection");
        //        //Debug.WriteLine(connectionString);
        //        //Debug.WriteLine(" "+ConfigurationManager.ConnectionStrings["DefaultConnection"].CurrentConfiguration.ToString());
        //        OracleConnection con = new OracleConnection();//"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=128.1.101.32)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));load balancing=false;ha events=false;User Id=IPAMS_TEST;Password=IPAMS_TEST;"
        //        con.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        //        con.Open();
        //        try
        //        {
        //            OracleCommand cmd = new OracleCommand()
        //            {
        //                CommandText = "SPGETSYMBOLFROMCOMPANYNAME",
        //                Connection = con,
        //                CommandType = CommandType.StoredProcedure,
        //            };
        //            OracleParameter input = cmd.Parameters.Add("CompanyName", OracleDbType.Varchar2, 500);
        //            OracleParameter output = cmd.Parameters.Add("C_SYMBOL", OracleDbType.Varchar2, 500);
        //            input.Direction = ParameterDirection.Input;
        //            output.Direction = ParameterDirection.Output;
        //            input.Value = CompanyName.Trim().ToLower();
        //            cmd.ExecuteNonQuery();
        //            result = Convert.ToString(output.Value);
        //        }
        //        catch (Exception ex)
        //        {
        //            Debug.WriteLine("==" + ex.Message);
        //        }
        //        con.Close();
        //    }


        //    #endregion

        //    #region GetMarketSummaryCompanySymbols_SQLITE

        //    else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("SQLITE"))
        //    {
        //        //List<ScripInfo> scrips = _context.ScripInfo.ToList();
        //        ////IQueryable<ScripInfo> scrips = _context.ScripInfo.Where(sc => sc.Name.Contains(_nameDivider[0] + " " + _nameDivider[1]) || sc.Name.Contains(_nameDivider[0]));
        //        string[] _nameDivider = CompanyName.Split(" ");
        //        IQueryable<ScripInfo> scrips = _context.ScripInfo.Where(sc => sc.Name.Contains(_nameDivider[0]) || sc.Name.Contains(_nameDivider.Length > 1 ? _nameDivider[0] + " " + _nameDivider[1] : _nameDivider[0] + " " + ""));
        //        //    if (scrips.Count() > 0)
        //        //    {
        //        for (int j = 0; j < scrips.Count(); j++)
        //        {
        //            //Debug.WriteLine("Database Record: " + scrips.ToList()[j].Name + ", Internet Record: " + NAME[i]);
        //            //if (scrips.ToList()[j].Name == NAME[i])
        //            int _nameLength = _nameDivider.Length;
        //            string _tempName = String.Empty;
        //            switch (_nameLength)
        //            {
        //                case 1:
        //                    _tempName = _nameDivider[0];
        //                    break;
        //                case 2:
        //                    _tempName = _nameDivider[0] + " " + _nameDivider[1];
        //                    break;
        //                case 3:
        //                    _tempName = _nameDivider[0] + " " + _nameDivider[1] + " " + _nameDivider[2];
        //                    break;
        //                //case 4:
        //                //    _tempName = _nameDivider[0] + " " + _nameDivider[1] + " " + _nameDivider[2] + " " + _nameDivider[3];
        //                //    break;
        //                //case 5:
        //                //    _tempName = _nameDivider[0] + " " + _nameDivider[1] + " " + _nameDivider[2] + " " + _nameDivider[3] + " " + _nameDivider[4];
        //                //    break;
        //                //case 6:
        //                //    _tempName = _nameDivider[0] + " " + _nameDivider[1] + " " + _nameDivider[2] + " " + _nameDivider[3] + " " + _nameDivider[4] + " " + _nameDivider[5];
        //                //    break;
        //                //case 7:
        //                //    _tempName = _nameDivider[0] + " " + _nameDivider[1] + " " + _nameDivider[2] + " " + _nameDivider[3] + " " + _nameDivider[4] + " " + _nameDivider[5] + " " + _nameDivider[6];
        //                //    break;
        //                default:
        //                    _tempName = _nameDivider[0] + " " + _nameDivider[1];
        //                    break;
        //            }
        //            if (scrips.ToList()[j].Name.Contains(_tempName))
        //            {
        //                result = scrips.ToList()[j].Symbol;
        //                break;
        //            }
        //        }
        //        //    }
        //        //result.Insert(i, " - ");
        //        //}

        //    }

        //    #endregion

        //    #region GetMarketSummaryCompanySymbols_POSTGRE

        //    else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("POSTGRE"))
        //    {
        //        result = "-";
        //    }

        //    #endregion

        //    return result;
        //}

        #endregion

        #region GetMarketSummaryCompanyCURRENT

        public string[] GetMarketSummaryCompanyCURRENT()
        {
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
            HtmlNodeCollection name_nodes = null;
            name_nodes = _webDataCollection != null ? _webDataCollection : name_nodes;
            if (name_nodes != null)
            {
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
                            shareList[counter2++].Current = AllTableRowData[j].Trim().Replace(" ","");
                        }
                    }
                }

                //foreach (CurrentMarketSummary item in shareList) {
                //    Debug.WriteLine("Name: " + item.Name + ", Symbol: " + item.Symbol + ", Current: " + item.Current);
                //}

            }
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

        #region getShareDetail

        public void getShareDetail()
        {
            //GetCompanyNames();
            //GetCompanyCURRENT();
            GetDefault();
            GetScripDetails();

            string tempCategory = _scripList[0].Category;
            _categoryList3.Add(tempCategory);
            for (int i = 0; i < _scripList.Count; i++)
            {
                if (tempCategory != _scripList[i].Category)
                {
                    _categoryList3.Add(_scripList[i].Category);
                    tempCategory = _scripList[i].Category;
                }
            }

        }

        #endregion

        #region btnGet_Click

        private async void btnGet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //setting background task to off
                resetFlag = false;

                //setting bucket buttun to hide
                btnBorderBucket.Visibility = Visibility.Hidden;

                ///Hiding txtboxes and add button
                ///
                if (!comboFund.Text.Contains("ADD NEW FUND"))
                {
                    lblFundID.Visibility = Visibility.Hidden;
                    lblFundNAME.Visibility = Visibility.Hidden;
                    txtFundID.Visibility = Visibility.Hidden;
                    txtFund.Visibility = Visibility.Hidden;
                    btnAdd.Visibility = Visibility.Hidden;
                    txtSearch.Visibility = Visibility.Visible;
                    txtSearch1.Visibility = Visibility.Visible;
                    comboCategory.Visibility = Visibility.Visible;
                    btnSearch.Visibility = Visibility.Visible;
                }

                var image = new BitmapImage();
                image.BeginInit();
                image.UriSource = ResourceAccessor.Get("Images/gear.gif");
                image.EndInit();
                ImageBehavior.SetAnimatedSource(imgStatus, image);
                lblStatus.Text = "Status: Processing";

                _miscellenousData["FUND_NAME"] = comboFund.Text;
                if (_miscellenousData["FUND_NAME"] == "Select..")
                {
                    MessageBox.Show("Please Select A Fund from the list.", "Fund Selection Required.", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (_miscellenousData["FUND_NAME"] == "<ADD NEW FUND>")
                {
                    if (comboFund.SelectedValue.Equals("<ADD NEW FUND>"))
                    {
                        lblFundID.Foreground = new SolidColorBrush(Colors.Black);
                        lblFundNAME.Foreground = new SolidColorBrush(Colors.Black);
                        txtFundID.IsEnabled = true;
                        txtFund.IsEnabled = true;
                        btnAdd.IsEnabled = true;
                        lblFundID.Visibility = Visibility.Visible;
                        txtFundID.Visibility = Visibility.Visible;
                        lblFundNAME.Visibility = Visibility.Visible;
                        txtFund.Visibility = Visibility.Visible;
                        btnAdd.Visibility = Visibility.Visible;
                        txtSearch.Visibility = Visibility.Hidden;
                        txtSearch1.Visibility = Visibility.Hidden;
                        comboCategory.Visibility = Visibility.Hidden;
                        btnSearch.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        lblFundID.Foreground = new SolidColorBrush(Colors.Gray);
                        lblFundNAME.Foreground = new SolidColorBrush(Colors.Gray);
                        txtFundID.IsEnabled = false;
                        txtFund.IsEnabled = false;
                        btnAdd.IsEnabled = false;
                        lblFundID.Visibility = Visibility.Hidden;
                        txtFundID.Visibility = Visibility.Hidden;
                        lblFundNAME.Visibility = Visibility.Hidden;
                        txtFund.Visibility = Visibility.Hidden;
                        btnAdd.Visibility = Visibility.Hidden;
                    }
                }
                else
                {
                    FundImage.Visibility = Visibility.Hidden;
                    list1.Visibility = Visibility.Hidden;
                    loadingImage.Visibility = Visibility.Visible;
                    _miscellenousData["FUND_ID"] = getFundId(_miscellenousData["FUND_NAME"]).ToString();
                    await Task.Run(() => mustWork(_miscellenousData["FUND_NAME"]));
                    //await Task.Run(() => getDefault());
                    txtDate.Text = "Date: " + _miscellenousData["DATE"];
                    txtStatus.Text = "Status: " + _miscellenousData["STATUS"];
                    //col1.DisplayMemberBinding = new Binding("FundwiseMarketSummaryId");
                    //col2.DisplayMemberBinding = new Binding("ShareName");
                    //col3.DisplayMemberBinding = new Binding("Symbol");
                    list1.Items.Clear();
                    FundImage.Visibility = Visibility.Hidden;
                    loadingImage.Visibility = Visibility.Hidden;
                    list1.Visibility = Visibility.Visible;
                    int flagClearBucket = ClearFundBacket();
                    if (flagClearBucket == -1)
                    {
                        if (fundList != null)
                        {
                            var bc = new BrushConverter();

                            myGridView.AllowsColumnReorder = true;
                            myGridView.ColumnHeaderToolTip = "Fund Market Information";

                            #region Serial_Number
                            DataTemplate templateHeader = new DataTemplate();
                            FrameworkElementFactory factoryHeader = new FrameworkElementFactory(typeof(TextBlock));
                            factoryHeader.SetValue(TextBlock.BackgroundProperty, System.Windows.Media.Brushes.DarkSlateGray);
                            factoryHeader.SetValue(TextBlock.ForegroundProperty, System.Windows.Media.Brushes.White);
                            factoryHeader.SetValue(TextBlock.TextProperty, "Sr. No.");
                            factoryHeader.SetValue(TextBlock.FontSizeProperty, 14.0);
                            factoryHeader.SetValue(TextBlock.TextAlignmentProperty, TextAlignment.Center);
                            factoryHeader.SetValue(TextBlock.PaddingProperty, new Thickness(0, 5, 0, 5));
                            factoryHeader.SetValue(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center);
                            factoryHeader.SetValue(TextBlock.WidthProperty, 50.0);
                            factoryHeader.SetValue(TextBlock.HeightProperty, 30.0);
                            templateHeader.VisualTree = factoryHeader;
                            Binding binding = new Binding("FundwiseMarketSummaryId");
                            DataTemplate template = new DataTemplate();
                            FrameworkElementFactory factory = new FrameworkElementFactory(typeof(TextBlock));
                            factory.SetBinding(TextBlock.TextProperty, binding);
                            factory.SetValue(TextBlock.TextAlignmentProperty, TextAlignment.Right);
                            factory.SetValue(TextBlock.WidthProperty, 35.0);
                            factory.SetValue(TextBlock.ForegroundProperty, System.Windows.Media.Brushes.Black);
                            template.VisualTree = factory;
                            gvc1.HeaderTemplate = templateHeader;
                            gvc1.Width = 50;
                            gvc1.CellTemplate = template;
                            myGridView.Columns.Add(gvc1);
                            #endregion

                            #region Name
                            DataTemplate NameHeader = new DataTemplate();
                            FrameworkElementFactory NameFactoryHeader = new FrameworkElementFactory(typeof(TextBlock));
                            NameFactoryHeader.SetValue(TextBlock.BackgroundProperty, System.Windows.Media.Brushes.DarkSlateGray);
                            NameFactoryHeader.SetValue(TextBlock.ForegroundProperty, System.Windows.Media.Brushes.White);
                            NameFactoryHeader.SetValue(TextBlock.TextProperty, "Name");
                            NameFactoryHeader.SetValue(TextBlock.FontSizeProperty, 14.0);
                            NameFactoryHeader.SetValue(TextBlock.TextAlignmentProperty, TextAlignment.Center);
                            NameFactoryHeader.SetValue(TextBlock.PaddingProperty, new Thickness(0, 5, 0, 5));
                            NameFactoryHeader.SetValue(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center);
                            NameFactoryHeader.SetValue(TextBlock.WidthProperty, 220.0);
                            NameFactoryHeader.SetValue(TextBlock.HeightProperty, 30.0);
                            NameHeader.VisualTree = NameFactoryHeader;
                            DataTemplate NameTemplate = new DataTemplate();
                            FrameworkElementFactory NameFactory = new FrameworkElementFactory(typeof(TextBlock));
                            NameFactory.SetBinding(TextBlock.TextProperty, new Binding("ShareName"));
                            NameFactory.SetValue(TextBlock.TextAlignmentProperty, TextAlignment.Left);
                            NameFactory.SetValue(TextBlock.WidthProperty, 220.0);
                            NameFactory.SetValue(TextBlock.ForegroundProperty, System.Windows.Media.Brushes.Black);
                            NameTemplate.VisualTree = NameFactory;
                            gvc2.HeaderTemplate = NameHeader;
                            gvc2.Width = 220;
                            gvc2.CellTemplate = NameTemplate;
                            myGridView.Columns.Add(gvc2);
                            #endregion

                            #region Symbol
                            DataTemplate SymbolHeader = new DataTemplate();
                            FrameworkElementFactory SymbolHeaderFactory = new FrameworkElementFactory(typeof(TextBlock));
                            SymbolHeaderFactory.SetValue(TextBlock.BackgroundProperty, System.Windows.Media.Brushes.DarkSlateGray);
                            SymbolHeaderFactory.SetValue(TextBlock.ForegroundProperty, System.Windows.Media.Brushes.White);
                            SymbolHeaderFactory.SetValue(TextBlock.TextProperty, "Symbol");
                            SymbolHeaderFactory.SetValue(TextBlock.FontSizeProperty, 14.0);
                            SymbolHeaderFactory.SetValue(TextBlock.TextAlignmentProperty, TextAlignment.Center);
                            SymbolHeaderFactory.SetValue(TextBlock.PaddingProperty, new Thickness(0, 5, 0, 5));
                            SymbolHeaderFactory.SetValue(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center);
                            SymbolHeaderFactory.SetValue(TextBlock.WidthProperty, 70.0);
                            SymbolHeaderFactory.SetValue(TextBlock.HeightProperty, 30.0);
                            SymbolHeader.VisualTree = SymbolHeaderFactory;
                            DataTemplate SymbolTemplate = new DataTemplate();
                            FrameworkElementFactory SymbolFactory = new FrameworkElementFactory(typeof(TextBlock));
                            SymbolFactory.SetBinding(TextBlock.TextProperty, new Binding("Symbol"));
                            SymbolFactory.SetValue(TextBlock.TextAlignmentProperty, TextAlignment.Center);
                            SymbolFactory.SetValue(TextBlock.WidthProperty, 70.0);
                            SymbolFactory.SetValue(TextBlock.ForegroundProperty, System.Windows.Media.Brushes.Black);
                            SymbolTemplate.VisualTree = SymbolFactory;
                            gvc3.HeaderTemplate = SymbolHeader;
                            gvc3.Width = 70;
                            gvc3.CellTemplate = SymbolTemplate;
                            myGridView.Columns.Add(gvc3);
                            #endregion

                            #region Category
                            DataTemplate CategoryHeaderTemplate = new DataTemplate();
                            FrameworkElementFactory CategoryHeaderFactory = new FrameworkElementFactory(typeof(TextBlock));
                            CategoryHeaderFactory.SetValue(TextBlock.BackgroundProperty, System.Windows.Media.Brushes.DarkSlateGray);
                            CategoryHeaderFactory.SetValue(TextBlock.ForegroundProperty, System.Windows.Media.Brushes.White);
                            CategoryHeaderFactory.SetValue(TextBlock.TextProperty, "Sector");
                            CategoryHeaderFactory.SetValue(TextBlock.FontSizeProperty, 14.0);
                            CategoryHeaderFactory.SetValue(TextBlock.TextAlignmentProperty, TextAlignment.Center);
                            CategoryHeaderFactory.SetValue(TextBlock.PaddingProperty, new Thickness(0, 5, 0, 5));
                            CategoryHeaderFactory.SetValue(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center);
                            CategoryHeaderFactory.SetValue(TextBlock.WidthProperty, 180.0);
                            CategoryHeaderFactory.SetValue(TextBlock.HeightProperty, 30.0);
                            CategoryHeaderTemplate.VisualTree = CategoryHeaderFactory;
                            DataTemplate CategoryTemplate = new DataTemplate();
                            FrameworkElementFactory CategoryFactory = new FrameworkElementFactory(typeof(TextBlock));
                            CategoryFactory.SetBinding(TextBlock.TextProperty, new Binding("Sector"));
                            CategoryFactory.SetValue(TextBlock.TextAlignmentProperty, TextAlignment.Left);
                            CategoryFactory.SetValue(TextBlock.WidthProperty, 200.0);
                            CategoryFactory.SetValue(TextBlock.ForegroundProperty, System.Windows.Media.Brushes.Black);
                            CategoryTemplate.VisualTree = CategoryFactory;
                            gvc4.HeaderTemplate = CategoryHeaderTemplate;
                            gvc4.Width = 180;
                            gvc4.CellTemplate = CategoryTemplate;
                            myGridView.Columns.Add(gvc4);
                            #endregion

                            #region Quantity
                            DataTemplate QuantityHeaderTemplate = new DataTemplate();
                            FrameworkElementFactory QuantityHeaderFactory = new FrameworkElementFactory(typeof(TextBlock));
                            QuantityHeaderFactory.SetValue(TextBlock.BackgroundProperty, System.Windows.Media.Brushes.DarkSlateGray);
                            QuantityHeaderFactory.SetValue(TextBlock.ForegroundProperty, System.Windows.Media.Brushes.White);
                            QuantityHeaderFactory.SetValue(TextBlock.TextProperty, "Quantity");
                            QuantityHeaderFactory.SetValue(TextBlock.FontSizeProperty, 14.0);
                            QuantityHeaderFactory.SetValue(TextBlock.TextAlignmentProperty, TextAlignment.Center);
                            QuantityHeaderFactory.SetValue(TextBlock.PaddingProperty, new Thickness(0, 5, 0, 5));
                            QuantityHeaderFactory.SetValue(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center);
                            QuantityHeaderFactory.SetValue(TextBlock.WidthProperty, 100.0);
                            QuantityHeaderFactory.SetValue(TextBlock.HeightProperty, 30.0);
                            QuantityHeaderTemplate.VisualTree = QuantityHeaderFactory;
                            DataTemplate QuantityTemplate = new DataTemplate();
                            FrameworkElementFactory QuantityFactory = new FrameworkElementFactory(typeof(TextBlock));
                            QuantityFactory.SetBinding(TextBlock.TextProperty, new Binding("Quantity"));
                            QuantityFactory.SetValue(TextBlock.TextAlignmentProperty, TextAlignment.Right);
                            QuantityFactory.SetValue(TextBlock.WidthProperty, 85.0);
                            QuantityFactory.SetValue(TextBlock.ForegroundProperty, System.Windows.Media.Brushes.Black);
                            QuantityTemplate.VisualTree = QuantityFactory;
                            gvc5.HeaderTemplate = QuantityHeaderTemplate;
                            gvc5.Width = 100;
                            gvc5.CellTemplate = QuantityTemplate;
                            myGridView.Columns.Add(gvc5);
                            #endregion

                            #region AveragePrice
                            DataTemplate AveragePriceHeaderTemplate = new DataTemplate();
                            FrameworkElementFactory AveragePriceHeaderFactory = new FrameworkElementFactory(typeof(TextBlock));
                            AveragePriceHeaderFactory.SetValue(TextBlock.BackgroundProperty, System.Windows.Media.Brushes.DarkSlateGray);
                            AveragePriceHeaderFactory.SetValue(TextBlock.ForegroundProperty, System.Windows.Media.Brushes.White);
                            AveragePriceHeaderFactory.SetValue(TextBlock.TextProperty, "Average Price");
                            AveragePriceHeaderFactory.SetValue(TextBlock.FontSizeProperty, 14.0);
                            AveragePriceHeaderFactory.SetValue(TextBlock.TextAlignmentProperty, TextAlignment.Center);
                            AveragePriceHeaderFactory.SetValue(TextBlock.PaddingProperty, new Thickness(0, 5, 0, 5));
                            AveragePriceHeaderFactory.SetValue(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center);
                            AveragePriceHeaderFactory.SetValue(TextBlock.WidthProperty, 100.0);
                            AveragePriceHeaderFactory.SetValue(TextBlock.HeightProperty, 30.0);
                            AveragePriceHeaderTemplate.VisualTree = AveragePriceHeaderFactory;
                            DataTemplate AveragePriceTemplate = new DataTemplate();
                            FrameworkElementFactory AveragePriceFactory = new FrameworkElementFactory(typeof(TextBlock));
                            AveragePriceFactory.SetBinding(TextBlock.TextProperty, new Binding("AveragePrice"));
                            AveragePriceFactory.SetValue(TextBlock.TextAlignmentProperty, TextAlignment.Right);
                            AveragePriceFactory.SetValue(TextBlock.WidthProperty, 85.0);
                            AveragePriceFactory.SetValue(TextBlock.ForegroundProperty, System.Windows.Media.Brushes.Black);
                            AveragePriceTemplate.VisualTree = AveragePriceFactory;
                            gvc6.HeaderTemplate = AveragePriceHeaderTemplate;
                            gvc6.Width = 100;
                            gvc6.CellTemplate = AveragePriceTemplate;
                            myGridView.Columns.Add(gvc6);
                            #endregion

                            #region BookCost
                            DataTemplate BookCostHeaderTemplate = new DataTemplate();
                            FrameworkElementFactory BookCostHeaderFactory = new FrameworkElementFactory(typeof(TextBlock));
                            BookCostHeaderFactory.SetValue(TextBlock.BackgroundProperty, System.Windows.Media.Brushes.DarkSlateGray);
                            BookCostHeaderFactory.SetValue(TextBlock.ForegroundProperty, System.Windows.Media.Brushes.White);
                            BookCostHeaderFactory.SetValue(TextBlock.TextProperty, "Book Cost");
                            BookCostHeaderFactory.SetValue(TextBlock.FontSizeProperty, 14.0);
                            BookCostHeaderFactory.SetValue(TextBlock.TextAlignmentProperty, TextAlignment.Center);
                            BookCostHeaderFactory.SetValue(TextBlock.PaddingProperty, new Thickness(0, 5, 0, 5));
                            BookCostHeaderFactory.SetValue(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center);
                            BookCostHeaderFactory.SetValue(TextBlock.WidthProperty, 120.0);
                            BookCostHeaderFactory.SetValue(TextBlock.HeightProperty, 30.0);
                            BookCostHeaderTemplate.VisualTree = BookCostHeaderFactory;
                            DataTemplate BookCostTemplate = new DataTemplate();
                            FrameworkElementFactory BookCostFactory = new FrameworkElementFactory(typeof(TextBlock));
                            BookCostFactory.SetBinding(TextBlock.TextProperty, new Binding("BookCost"));
                            BookCostFactory.SetValue(TextBlock.TextAlignmentProperty, TextAlignment.Right);
                            BookCostFactory.SetValue(TextBlock.WidthProperty, 105.0);
                            BookCostFactory.SetValue(TextBlock.ForegroundProperty, System.Windows.Media.Brushes.Black);
                            BookCostTemplate.VisualTree = BookCostFactory;
                            gvc7.HeaderTemplate = BookCostHeaderTemplate;
                            gvc7.Width = 120;
                            gvc7.CellTemplate = BookCostTemplate;
                            myGridView.Columns.Add(gvc7);
                            #endregion

                            #region MarketPrice
                            DataTemplate MarketPriceHeaderTemplate = new DataTemplate();
                            FrameworkElementFactory MarketPriceHeaderFactory = new FrameworkElementFactory(typeof(TextBlock));
                            MarketPriceHeaderFactory.SetValue(TextBlock.BackgroundProperty, System.Windows.Media.Brushes.DarkSlateGray);
                            MarketPriceHeaderFactory.SetValue(TextBlock.ForegroundProperty, System.Windows.Media.Brushes.White);
                            MarketPriceHeaderFactory.SetValue(TextBlock.TextProperty, "Market Price");
                            MarketPriceHeaderFactory.SetValue(TextBlock.FontSizeProperty, 14.0);
                            MarketPriceHeaderFactory.SetValue(TextBlock.TextAlignmentProperty, TextAlignment.Center);
                            MarketPriceHeaderFactory.SetValue(TextBlock.PaddingProperty, new Thickness(0, 5, 0, 5));
                            MarketPriceHeaderFactory.SetValue(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center);
                            MarketPriceHeaderFactory.SetValue(TextBlock.WidthProperty, 120.0);
                            MarketPriceHeaderFactory.SetValue(TextBlock.HeightProperty, 30.0);
                            MarketPriceHeaderTemplate.VisualTree = MarketPriceHeaderFactory;
                            DataTemplate MarketPriceTemplate = new DataTemplate();
                            FrameworkElementFactory MarketPriceFactory = new FrameworkElementFactory(typeof(TextBlock));
                            MarketPriceFactory.SetBinding(TextBlock.TextProperty, new Binding("MarketPrice"));
                            MarketPriceFactory.SetValue(TextBlock.TextAlignmentProperty, TextAlignment.Right);
                            MarketPriceFactory.SetValue(TextBlock.WidthProperty, 105.0);
                            MarketPriceFactory.SetValue(TextBlock.ForegroundProperty, System.Windows.Media.Brushes.Black);
                            MarketPriceTemplate.VisualTree = MarketPriceFactory;
                            gvc8.HeaderTemplate = MarketPriceHeaderTemplate;
                            gvc8.Width = 120;
                            gvc8.CellTemplate = MarketPriceTemplate;
                            myGridView.Columns.Add(gvc8);
                            #endregion

                            #region MarkeValue
                            DataTemplate MarketValueHeaderTemplate = new DataTemplate();
                            FrameworkElementFactory MarketValueHeaderFactory = new FrameworkElementFactory(typeof(TextBlock));
                            MarketValueHeaderFactory.SetValue(TextBlock.BackgroundProperty, System.Windows.Media.Brushes.DarkSlateGray);
                            MarketValueHeaderFactory.SetValue(TextBlock.ForegroundProperty, System.Windows.Media.Brushes.White);
                            MarketValueHeaderFactory.SetValue(TextBlock.TextProperty, "Market Value");
                            MarketValueHeaderFactory.SetValue(TextBlock.FontSizeProperty, 14.0);
                            MarketValueHeaderFactory.SetValue(TextBlock.TextAlignmentProperty, TextAlignment.Center);
                            MarketValueHeaderFactory.SetValue(TextBlock.PaddingProperty, new Thickness(0, 5, 0, 5));
                            MarketValueHeaderFactory.SetValue(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center);
                            MarketValueHeaderFactory.SetValue(TextBlock.WidthProperty, 120.0);
                            MarketValueHeaderFactory.SetValue(TextBlock.HeightProperty, 30.0);
                            MarketValueHeaderTemplate.VisualTree = MarketValueHeaderFactory;
                            DataTemplate MarketValueTemplate = new DataTemplate();
                            FrameworkElementFactory MarketValueFactory = new FrameworkElementFactory(typeof(TextBlock));
                            MarketValueFactory.SetBinding(TextBlock.TextProperty, new Binding("MarketValue"));
                            MarketValueFactory.SetValue(TextBlock.TextAlignmentProperty, TextAlignment.Right);
                            MarketValueFactory.SetValue(TextBlock.WidthProperty, 105.0);
                            MarketValueFactory.SetValue(TextBlock.ForegroundProperty, System.Windows.Media.Brushes.Black);
                            MarketValueTemplate.VisualTree = MarketValueFactory;
                            gvc9.HeaderTemplate = MarketValueHeaderTemplate;
                            gvc9.Width = 120;
                            gvc9.CellTemplate = MarketValueTemplate;
                            myGridView.Columns.Add(gvc9);
                            #endregion

                            #region AppDep
                            DataTemplate AppDepHeaderTemplate = new DataTemplate();
                            FrameworkElementFactory AppDepHeaderFactory = new FrameworkElementFactory(typeof(TextBlock));
                            AppDepHeaderFactory.SetValue(TextBlock.BackgroundProperty, System.Windows.Media.Brushes.DarkSlateGray);
                            AppDepHeaderFactory.SetValue(TextBlock.ForegroundProperty, System.Windows.Media.Brushes.White);
                            AppDepHeaderFactory.SetValue(TextBlock.TextProperty, "App. / (Dep.)");
                            AppDepHeaderFactory.SetValue(TextBlock.FontSizeProperty, 14.0);
                            AppDepHeaderFactory.SetValue(TextBlock.TextAlignmentProperty, TextAlignment.Center);
                            AppDepHeaderFactory.SetValue(TextBlock.PaddingProperty, new Thickness(0, 5, 0, 5));
                            AppDepHeaderFactory.SetValue(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center);
                            AppDepHeaderFactory.SetValue(TextBlock.WidthProperty, 100.0);
                            AppDepHeaderFactory.SetValue(TextBlock.HeightProperty, 30.0);
                            AppDepHeaderTemplate.VisualTree = AppDepHeaderFactory;
                            DataTemplate AppDepTemplate = new DataTemplate();
                            FrameworkElementFactory AppDepFactory = new FrameworkElementFactory(typeof(TextBlock));
                            AppDepFactory.SetBinding(TextBlock.TextProperty, new Binding("AppDep"));
                            AppDepFactory.SetValue(TextBlock.TextAlignmentProperty, TextAlignment.Right);
                            AppDepFactory.SetValue(TextBlock.WidthProperty, 85.0);
                            AppDepFactory.SetValue(TextBlock.ForegroundProperty, System.Windows.Media.Brushes.Black);
                            AppDepTemplate.VisualTree = AppDepFactory;
                            gvc10.HeaderTemplate = AppDepHeaderTemplate;
                            gvc10.Width = 100;
                            gvc10.CellTemplate = AppDepTemplate;
                            myGridView.Columns.Add(gvc10);
                            #endregion

                            #region ClosingPercentage
                            DataTemplate ClosingPercentageHeaderTemplate = new DataTemplate();
                            FrameworkElementFactory ClosingPercentageHeaderFactory = new FrameworkElementFactory(typeof(TextBlock));
                            ClosingPercentageHeaderFactory.SetValue(TextBlock.BackgroundProperty, System.Windows.Media.Brushes.DarkSlateGray);
                            ClosingPercentageHeaderFactory.SetValue(TextBlock.ForegroundProperty, System.Windows.Media.Brushes.White);
                            ClosingPercentageHeaderFactory.SetValue(TextBlock.TextProperty, "Closing Percentage");
                            ClosingPercentageHeaderFactory.SetValue(TextBlock.FontSizeProperty, 14.0);
                            ClosingPercentageHeaderFactory.SetValue(TextBlock.TextAlignmentProperty, TextAlignment.Center);
                            ClosingPercentageHeaderFactory.SetValue(TextBlock.PaddingProperty, new Thickness(0, 5, 0, 5));
                            ClosingPercentageHeaderFactory.SetValue(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center);
                            ClosingPercentageHeaderFactory.SetValue(TextBlock.WidthProperty, 140.0);
                            ClosingPercentageHeaderFactory.SetValue(TextBlock.HeightProperty, 30.0);
                            ClosingPercentageHeaderTemplate.VisualTree = ClosingPercentageHeaderFactory;
                            DataTemplate ClosingPercentageTemplate = new DataTemplate();
                            FrameworkElementFactory ClosingPercentageFactory = new FrameworkElementFactory(typeof(TextBlock));
                            ClosingPercentageFactory.SetBinding(TextBlock.TextProperty, new Binding("ClosingPercentage"));
                            ClosingPercentageFactory.SetValue(TextBlock.TextAlignmentProperty, TextAlignment.Right);
                            ClosingPercentageFactory.SetValue(TextBlock.WidthProperty, 125.0);
                            ClosingPercentageFactory.SetValue(TextBlock.ForegroundProperty, System.Windows.Media.Brushes.Black);
                            ClosingPercentageTemplate.VisualTree = ClosingPercentageFactory;
                            gvc11.HeaderTemplate = ClosingPercentageHeaderTemplate;
                            gvc11.Width = 140;
                            gvc11.CellTemplate = ClosingPercentageTemplate;
                            myGridView.Columns.Add(gvc11);
                            #endregion



                            for (int i = 0; i < fundList.Count; i++)
                            {
                                if (fundList[i].ShareName == null) { }
                                else
                                {
                                    if (fundList[i].AppDep.StartsWith('('))
                                    {
                                        
                                        list1.Items.Add(new FundwiseMarketSummary { FundwiseMarketSummaryId = i + 1, ShareName = fundList[i].ShareName, Symbol = fundList[i].Symbol, Sector = fundList[i].Sector == null ? " NOT LISTED" : fundList[i].Sector, Quantity = fundList[i].Quantity, AveragePrice = fundList[i].AveragePrice, BookCost = fundList[i].BookCost, MarketPrice = fundList[i].MarketPrice == null ? "NOT LISTED" : fundList[i].MarketPrice, MarketValue = fundList[i].MarketValue == "0.00" ? "NOT LISTED" : fundList[i].MarketValue, AppDep = fundList[i].AppDep.Trim(), ClosingPercentage = fundList[i].ClosingPercentage == "" ? "-" : fundList[i].MarketPrice == null ? "-" : String.Format("{0:N2}", Math.Round(Convert.ToDecimal(fundList[i].ClosingPercentage), 2, MidpointRounding.AwayFromZero).ToString("N2") + "%") });
                                        //Style style = new Style();
                                        //style.TargetType = typeof(ListViewItem);
                                        //style.Setters.Add(new Setter(ListViewItem.BackgroundProperty, System.Windows.Media.Brushes.Pink));
                                        //list1.ItemContainerStyle = style;

                                        string xaml = @"
        <DataTemplate xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"" xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""> 
            <TextBlock Text=""{Binding AppDep" + @"}""> 
                <TextBlock.Style>
                    <Style TargetType=""{x:Type TextBlock}"">
                        <Setter Property=""Foreground"" Value=""Red"" />
                    </Style>
                </TextBlock.Style>
            </TextBlock>
            </DataTemplate>";
                                        StringReader stringReader = new StringReader(xaml);
                                        XmlReader xmlReader = XmlReader.Create(stringReader);
                                        gvc10.CellTemplate = XamlReader.Load(xmlReader) as DataTemplate;

                                        //DataTrigger trigger = new DataTrigger();
                                        //trigger.Binding = new Binding("ShareName");
                                        //trigger.Value = "ZEPHYR TEXTILE";
                                        //trigger.Setters.Add(new Setter(ListViewItem.BackgroundProperty, System.Windows.Media.Brushes.Pink));
                                        //Style.Triggers.Add(trigger);
                                        //list1.ItemContainerStyle = Style;


                                    }
                                    else
                                    {
                                        list1.Items.Add(new FundwiseMarketSummary { FundwiseMarketSummaryId = i + 1, ShareName = fundList[i].ShareName, Symbol = fundList[i].Symbol, Sector = fundList[i].Sector, Quantity = fundList[i].Quantity, AveragePrice = fundList[i].AveragePrice, BookCost = fundList[i].BookCost, MarketPrice = fundList[i].MarketPrice.ToString() == "0" ? "NOT LISTED" : fundList[i].MarketPrice, MarketValue = fundList[i].MarketValue == "0.00" ? "NOT LISTED" : fundList[i].MarketValue, AppDep = fundList[i].AppDep.Trim(), ClosingPercentage = fundList[i].ClosingPercentage == "" ? "-" : String.Format("{0:N2}", Math.Round(Convert.ToDecimal(fundList[i].ClosingPercentage), 2, MidpointRounding.AwayFromZero).ToString("N2") + "%") });
                                    }

                                    if (Convert.ToDecimal(fundList[i].ClosingPercentage) >= Convert.ToDecimal(CLOSING_PERCENTAGE))
                                    {
                                        FundPopUpWindowFlag = true;
                                        try
                                        {
                                            //int flagBucket = SavingToFundBacket(_miscellenousData["DATE"], _miscellenousData["STATUS"].ToUpper(), false, Convert.ToInt64(_miscellenousData["FUND_ID"]), _miscellenousData["FUND_NAME"], fundList[i].ShareName, fundList[i].Symbol, Convert.ToDecimal(fundList[i].Quantity), Convert.ToDecimal(fundList[i].AveragePrice), Convert.ToDecimal(fundList[i].BookCost), Convert.ToDecimal(fundList[i].MarketPrice), Convert.ToDecimal(fundList[i].MarketValue), Convert.ToDecimal(fundList[i].AppDep), Convert.ToDecimal(fundList[i].ClosingPercentage));
                                        }
                                        catch (Exception ex)
                                        {
                                            Debug.WriteLine("Exception: " + ex.Message);
                                        }
                                    }

                                    //list1.Items.Add(new FundwiseMarketSummary { FundwiseMarketSummaryId = i + 1, ShareName = fundList[i].ShareName, Symbol = fundList[i].Symbol, Sector = fundList[i].Sector, Quantity = fundList[i].Quantity, AveragePrice = fundList[i].AveragePrice, BookCost = fundList[i].BookCost, MarketPrice = fundList[i].MarketPrice.ToString() == "0" ? "Not Listed" : fundList[i].MarketPrice, MarketValue = fundList[i].MarketValue == "0.00" ? "Not Listed" : fundList[i].MarketValue, AppDep = fundList[i].AppDep.Trim(), ClosingPercentage = fundList[i].ClosingPercentage == "" ? "-" : String.Format("{0:N2}", Math.Round(Convert.ToDecimal(fundList[i].ClosingPercentage), 2, MidpointRounding.AwayFromZero).ToString("N2") + "%") });
                                    //int DataSaved = await Task.Run(() => SavingDataToDatabase(_miscellenousData["DATE"], Convert.ToInt64(_miscellenousData["FUND_ID"]), _miscellenousData["FUND_NAME"], fundList[i].ShareName, fundList[i].Symbol, fundList[i].Quantity, fundList[i].AveragePrice, fundList[i].BookCost, fundList[i].MarketPrice, fundList[i].MarketValue, fundList[i].AppDep, fundList[i].ClosingPercentage) );
                                }
                            }

                            list1.View = myGridView;

                        }
                        else
                        {
                            Debug.WriteLine("spFundDetail variable is null..");
                        }
                    }
                    else
                    {

                    }

                    for (int i = 0; i < _categoryList3.Count; i++)
                    {
                        comboCategory.Items.Add(_categoryList3[i]);
                    }

                    FundImage.Visibility = Visibility.Hidden;
                    list1.Visibility = Visibility.Visible;
                    loadingImage.Visibility = Visibility.Hidden;

                    //int clearData = await Task.Run(() => ClearFundMarketSummary());
                    //Debug.WriteLine("Fund Market Summary Data Cleared..: " + clearData);
                    //if (clearData == -1)
                    //{
                        
                    //    //Debug.WriteLine("Fund Market Summary Data Inserted..: " + DataSaved);
                    //}

                    //txtBucketCount.Text = " (" + getCountFundBucket().ToString() + ") ";
                    btnBorderBucket.Visibility = Visibility.Visible;

                    //if (!DataSaved)
                    //    Debug.WriteLine("Unable to Save Data.");
                    image = new BitmapImage();
                    image.BeginInit();
                    image.UriSource = ResourceAccessor.Get("Images/tick.gif");
                    image.EndInit();
                    ImageBehavior.SetAnimatedSource(imgStatus, image);

                    lblStatus.Text = "Status: Ready";

                    if (ConfigurationManager.AppSettings["PopupAlert"] == "1")
                    {

                        if (FundPopUpWindowFlag)
                        {
                            List<Int64> bucketFundId = getFundScripBucketId();
                            foreach (Int64 id in bucketFundId)
                            {
                                FundPopupWindow popupWindow = new FundPopupWindow(id);
                                popupWindow.Show();
                            }
                        }
                    }

                    if (ConfigurationManager.AppSettings["EmailAlert"] == "1")
                    {

                        if (FundPopUpWindowFlag)
                        {
                            //List<Int64> bucketFundId = getFundScripBucketId();
                            //foreach (Int64 id in bucketFundId)
                            //{
                            try
                            {
                                Email("Hello");
                            }
                            catch(Exception ex)
                            {
                                Debug.WriteLine("Email Exception: " + ex.Message);
                            }
                            //}
                        }
                    }

                    DispatcherTimer dispatcherTimer = new DispatcherTimer();
                    dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
                    dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
                    dispatcherTimer.Start();
                    resetFlag = true;

                }

            }
            catch (WebException ex)
            {
                Debug.WriteLine("Web Exception: " + ex.Message);
            }
            catch (SqlException ex)
            {
                Debug.WriteLine("SQL Exception: " + ex.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception: " + ex.Message);
            }

        }

        #endregion

        #region dispatcherTimer_Tick

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            countTimer++;
            if (countTimer % 180 == 0 && resetFlag == true)
            {
                ButtonAutomationPeer peer = new ButtonAutomationPeer(btnReset);
                IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                invokeProv.Invoke();
            }
        }

        #endregion

        #region CommentedOut

        //private void worker_DoWork(object sender, DoWorkEventArgs e)
        //{
        //}

        //private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //    imgStatus.Source = new BitmapImage(ResourceAccessor.Get("Images/exclaimation.png"));
        //    lblStatus.Text = "Status: " + e.ProgressPercentage.ToString() + "% Processed";
        //}

        //private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    list1.Visibility = Visibility.Visible;
        //    loadingImage.Visibility = Visibility.Hidden;
        //    FundImage.Visibility = Visibility.Hidden;
        //    imgStatus.Source = new BitmapImage(ResourceAccessor.Get("Images/tick.png"));
        //    lblStatus.Text = "Status: Ready";

        //}

        #endregion

        #region getFundId

        ///Get Fund ID By Fund Name
        ///
        public int getFundId(string _fundName)
        {
            int _id = 0;

            #region getFundId_MSSQLSERVER

            if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("MSSQLSERVER"))
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                conn.Open();
                // 1.  create a command object identifying the stored procedure
                SqlCommand cmd = new SqlCommand("spGetFundIDByParamNAME", conn);

                // 2. set the command object so it knows to execute a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                // 3. add parameter to command, which will be passed to the stored procedure
                cmd.Parameters.Add(new SqlParameter("@FUND_NAME", _fundName));

                // execute the command
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    // iterate through results, printing each to console
                    while (rdr.Read())
                    {
                        _id = Convert.ToInt32(rdr["FUND_CODE"]);
                        //Debug.WriteLine("FUND_ID in Default DB: " + FUND_ID);
                    }
                }

                conn.Close();
            }

            #endregion

            #region getFundId_ORACLE

            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("ORACLE"))
            {
                OracleConnection con = new OracleConnection(); //"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=128.1.101.32)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));load balancing=false;ha events=false;User Id=IPAMS_TEST;Password=IPAMS_TEST;"
                con.ConnectionString = ConfigurationManager.ConnectionStrings["IpamsConnection"].ConnectionString;
                con.Open();
                try
                {
                    OracleCommand cmd = new OracleCommand()
                    {
                        CommandText = "spgetfundidbyparamname",
                        Connection = con,
                        CommandType = CommandType.StoredProcedure,
                    };
                    OracleParameter input = cmd.Parameters.Add("NAME", OracleDbType.Varchar2);
                    OracleParameter output = cmd.Parameters.Add("CODE", OracleDbType.Int64);
                    input.Direction = ParameterDirection.Input;
                    output.Direction = ParameterDirection.Output;
                    input.Value = _fundName.Trim().ToLower();
                    cmd.ExecuteNonQuery();
                    _miscellenousData["FUND_ID"] = Convert.ToString(output.Value);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("==" + ex.Message);
                }
                con.Close();
            }

            #endregion

            #region getFundId_SQLITE

            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("SQLITE"))
            {
                IQueryable<FundInfo> _fundSummary = _context.FundInfo.Where(f => f.Name == _fundName);
                _id = Convert.ToInt32(_fundSummary.FirstOrDefault().Code);
            }

            #endregion 

            return _id;
        }

        #endregion

        #region mustWork

        private void mustWork(string FundName)
        {
            _miscellenousData["FUND_ID"] = "0";

            #region mustWork_MSSQLSERVER
            if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("MSSQLSERVER"))
            {
                connDefault = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                connIpams = new SqlConnection(ConfigurationManager.ConnectionStrings["IpamsConnection"].ConnectionString);
                fundList = new List<FundwiseMarketSummary>();
                try
                {

                    connDefault.Open();
                    // 1.  create a command object identifying the stored procedure
                    SqlCommand cmd = new SqlCommand("spGetFundIDByParamNAME", connDefault);

                    // 2. set the command object so it knows to execute a stored procedure
                    cmd.CommandType = CommandType.StoredProcedure;

                    // 3. add parameter to command, which will be passed to the stored procedure
                    cmd.Parameters.Add(new SqlParameter("@FUND_NAME", FundName));

                    // execute the command
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        // iterate through results, printing each to console
                        while (rdr.Read())
                        {
                            _miscellenousData["FUND_ID"] = Convert.ToInt32(rdr["FI_CODE"]).ToString();
                        }
                    }

                    connDefault.Close();

                    getShareDetail();

                    connIpams.Open();

                    if (_miscellenousData["FUND_ID"] == "0")
                    {
                        // 1.  create a command object identifying the stored procedure
                        SqlCommand getFundIdByNameCmd = new SqlCommand("spGetFundIDByParamNAME", connIpams);

                        // 2. set the command object so it knows to execute a stored procedure
                        getFundIdByNameCmd.CommandType = CommandType.StoredProcedure;

                        // 3. add parameter to command, which will be passed to the stored procedure
                        getFundIdByNameCmd.Parameters.Add(new SqlParameter("@FUND_NAME", FundName));

                        // execute the command
                        using (SqlDataReader rdr2 = getFundIdByNameCmd.ExecuteReader())
                        {
                            // iterate through results, printing each to console
                            while (rdr2.Read())
                            {
                                _miscellenousData["FUND_ID"] = Convert.ToInt32(rdr2["FUND_CODE"]).ToString();
                            }
                        }

                    }
                    //connIpams.Close();

                    //connIpams.Open();


                    // 1.  create a command object identifying the stored procedure
                    SqlCommand cmdforFetchingShare = new SqlCommand("spGetShareDetailByParamFundIdAndDate", connIpams);

                    // 2. set the command object so it knows to execute a stored procedure
                    cmdforFetchingShare.CommandType = CommandType.StoredProcedure;

                    // 3. add parameter to command, which will be passed to the stored procedure
                    cmdforFetchingShare.Parameters.Add(new SqlParameter("@FUND_ID", _miscellenousData["FUND_ID"]));
                    //cmdforFetchingShare.Parameters.Add(new SqlParameter("@DATE", DateTime.Now.AddYears(-1)));
                    cmdforFetchingShare.Parameters.Add(new SqlParameter("@DATE", DateTime.Now));
                    //Share_Name = new List<String>();
                    //Share_Symbol = new List<String>();
                    //DateCostLastUpdated = new List<String>();
                    //LastUpdatedPerUnitCost = new List<String>();
                    //LastUpdatedCost = new List<String>();
                    //LastUpdatedHolding = new List<String>();
                    //LastUpdatedMarketPriceDate = new List<String>();
                    //MarketSymbol = new List<String>();
                    //MarketPriceCurrent = new List<String>();
                    //MarketValue = new List<String>();
                    //Appreciation_Depreciation = new List<String>();
                    
                    //// execute the command
                    //QUANTITY = new List<Decimal>();
                    //AVERAGE_PRICE = new List<Decimal>();
                    //BOOK_COST = new List<Decimal>();
                    //MARKET_PRICE = new List<Decimal>();
                    //MARKET_VALUE = new List<Decimal>();
                    //APPRECIATION_DEPRECIATION = new List<String>();
                    int total = 1;
                    using (SqlDataReader rdr = cmdforFetchingShare.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            // Qunatity is not Null
                            if (!rdr.IsDBNull(5))
                            {
                                // Quantity is not 0
                                if (rdr.GetDecimal(5) != 0)
                                {
                                    //Share_Name.Add(rdr.GetString(0).ToString());
                                    //Share_Symbol.Add(rdr.GetString(1).ToString());
                                    //DateCostLastUpdated.Add(rdr.GetDateTime(2).ToString());
                                    //AVERAGE_PRICE.Add(rdr.GetDecimal(3).ToString() == null ? 0 : rdr.GetDecimal(3));
                                    //LastUpdatedPerUnitCost.Add(rdr.GetDecimal(3).ToString("#.##"));
                                    //BOOK_COST.Add(rdr.GetDecimal(4));
                                    //LastUpdatedCost.Add(Math.Round(rdr.GetDecimal(4)).ToString("#,##0"));
                                    //double holding = Convert.ToDouble(rdr.GetDecimal(5));
                                    CurrentMarketSummary _currentMarketSummary = new CurrentMarketSummary();
                                    string localSymbol = String.Empty;
                                    string localCurrent = String.Empty;
                                    decimal localValue = 0;
                                    //QUANTITY.Add(rdr.GetDecimal(5));
                                    //LastUpdatedHolding.Add(holding.ToString("#,##0"));
                                    //LastUpdatedMarketPriceDate.Add(rdr.GetDateTime(6).ToString("#.##"));
                                    if (getSymbolStatus(rdr.GetString(1).ToString()))
                                    {
                                        for (int i = 0; i < _scripList.Count; i++)
                                        {
                                            if (rdr.GetString(1).Equals(_scripList[i].Symbol == null ? "" : _scripList[i].Symbol))
                                            {
                                                _currentMarketSummary.Name = _scripList[i].Name;
                                                _currentMarketSummary.Category = _scripList[i].Category;
                                                _currentMarketSummary.Symbol = _scripList[i].Symbol;
                                                _currentMarketSummary.Ldcp = _scripList[i].Ldcp;
                                                _currentMarketSummary.Open = _scripList[i].Open;
                                                _currentMarketSummary.High = _scripList[i].High;
                                                _currentMarketSummary.Low = _scripList[i].Low;
                                                _currentMarketSummary.Current = _scripList[i].Current;
                                                //Debug.WriteLine("| Name: " + _scripList[i].Name + "| Category: " + _scripList[i].Category + "| Price: " + _scripList[i].Current);
                                                _currentMarketSummary.Change = _scripList[i].Change;
                                                _currentMarketSummary.Volume = _scripList[i].Volume;
                                                //localSymbol = _scripList[i].Symbol;
                                                //localCurrent = String.Format("{0:N2}", _scripList[i].Current);
                                                //MARKET_PRICE.Add(Decimal.Parse(shareList[i].Current));
                                                //localValue = Convert.ToDecimal(_scripList[i].Current) * rdr.GetDecimal(5);
                                                //MARKET_VALUE.Add(localValue);

                                            }
                                        }
                                    }
                                    //MarketSymbol.Add(localSymbol);
                                    //MarketPriceCurrent.Add(localCurrent);
                                    //MarketValue.Add(Convert.ToInt32(Math.Round(localValue)).ToString("#,##0"));
                                    //decimal appreciation = localValue - rdr.GetDecimal(4);
                                    decimal appreciation = (Convert.ToDecimal(_currentMarketSummary.Current) * rdr.GetDecimal(5)) - rdr.GetDecimal(4);
                                    //APPRECIATION_DEPRECIATION.Add(appreciation.ToString());
                                    string localAppreciate = Math.Round(appreciation, 2).ToString("#,##0");
                                    if (appreciation < 0)
                                    {
                                        localAppreciate = "(" + localAppreciate.Replace("-", "") + ")";
                                    }
                                    //Appreciation_Depreciation.Add(localAppreciate);

                                    //Closing Percentage Calculation
                                    //decimal lappdepp;
                                    //if (fundList[i].AppDep.Contains("("))
                                    //{
                                    //    Debug.WriteLine("fundList[i].AppDep: " + fundList[i].AppDep.Replace("(", "").Replace(")", "").Replace(",", ""));
                                    //    lappdepp = Convert.ToDecimal("-" + fundList[i].AppDep.Replace("(", "").Replace(")", "").Replace(",", ""));
                                    //}
                                    //else
                                    //{
                                    //    lappdepp = Convert.ToDecimal(fundList[i].AppDep.Replace("(", "").Replace(")", "").Replace(",", ""));
                                    //}
                                    //Debug.WriteLine("lappdepp: " + lappdepp);
                                    //decimal lbookcost = Convert.ToDecimal(fundList[i].BookCost.Replace(",", "").Replace(" ", ""));
                                    //Debug.WriteLine("lbookcost: " + lbookcost);
                                    decimal closing = 0;
                                    if (rdr.GetDecimal(4) > 0)
                                        closing = appreciation / rdr.GetDecimal(4);
                                    //Debug.WriteLine("closing: " + closing);
                                    //fundList[i].ClosingPercentage = closing.ToString();

                                    FundwiseMarketSummary item = new FundwiseMarketSummary();
                                    item.FundwiseMarketSummaryId = total++;
                                    item.ShareName = rdr.GetString(0);
                                    item.Sector = _currentMarketSummary.Category;
                                    item.Symbol = rdr.GetString(1);
                                    item.Quantity = rdr.GetDecimal(5).ToString("#,##0");
                                    item.AveragePrice = Decimal.Round(rdr.GetDecimal(3), 2).ToString("N2");
                                    item.BookCost = Decimal.Round(rdr.GetDecimal(4), 2).ToString("N2");
                                    item.MarketPrice = _currentMarketSummary.Current == "" ? "0" : _currentMarketSummary.Current;
                                    item.MarketValue = Decimal.Round((Convert.ToDecimal(_currentMarketSummary.Current) * rdr.GetDecimal(5)), 2).ToString("N2");
                                    item.AppDep = String.Format("{0:N0}", localAppreciate);
                                    item.ClosingPercentage = closing.ToString();
                                    fundList.Add(item);
                                }
                            }
                            else { }
                        }
                    }
                    connIpams.Close();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Database Connectivity Problem", MessageBoxButton.OK, MessageBoxImage.Information);
                    Debug.WriteLine("SQL Exception DB: " + ex.Message);

                }
                catch (WebException ex)
                {
                    MessageBox.Show(ex.Message, "Internet Connectivity Problem", MessageBoxButton.OK, MessageBoxImage.Information);
                    Debug.WriteLine("Internet Exception: " + ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "General Problem", MessageBoxButton.OK, MessageBoxImage.Error);
                    Debug.WriteLine("Exception: " + ex.Message);
                }
            }

            #endregion

            #region mustWork_ORACLE

            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("ORACLE"))
            {
                OracleConnection con = new OracleConnection(); //"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=128.1.101.32)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));load balancing=false;ha events=false;User Id=IPAMS_TEST;Password=IPAMS_TEST;"
                con.ConnectionString = ConfigurationManager.ConnectionStrings["IpamsConnection"].ConnectionString;
                con.Open();
                try
                {
                    OracleCommand cmd = new OracleCommand()
                    {
                        CommandText = "spgetfundidbyparamname",
                        Connection = con,
                        CommandType = CommandType.StoredProcedure,
                    };
                    OracleParameter input = cmd.Parameters.Add("name", OracleDbType.Varchar2);
                    OracleParameter output = cmd.Parameters.Add("code", OracleDbType.Int64);
                    input.Direction = ParameterDirection.Input;
                    output.Direction = ParameterDirection.Output;
                    input.Value = FundName.Trim().ToLower();
                    cmd.ExecuteNonQuery();
                    _miscellenousData["FUND_ID"] = Convert.ToString(output.Value);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("==" + ex.Message);
                }

                OracleCommand cmdSecond = new OracleCommand("select * from table(fnc_get_shares_pipelined("+ _miscellenousData["FUND_ID"] + "))", con);
                cmdSecond.CommandType = CommandType.Text;
                //cmdSecond.Parameters.Add(new OracleParameter("@FUND_ID", _miscellenousData["FUND_ID"]));
                //cmdSecond.Parameters.Add(new OracleParameter("@DATE", DateTime.Now));
                //List<CurrentMarketSummary> lmethod = getShareDetail();
                //if (lmethod != null)
                //{
                //    shareList = getShareDetail();
                //}
                getShareDetail();
                int total = 0;
                using (OracleDataReader rdr = cmdSecond.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        // Qunatity is not Null
                        if (!rdr.IsDBNull(5))
                        {
                            // Quantity is not 0
                            if (rdr.GetString(5) != null && rdr.GetString(5) != "0" && rdr.GetString(5) != "")
                            {
                                string localSymbol = String.Empty;
                                string localCurrent = String.Empty;
                                decimal localValue = 0;
                                if (getSymbolStatus(rdr.GetString(1).ToString()))
                                {
                                    for (int i = 0; i < shareList.Count; i++)
                                    {
                                        if (rdr.GetString(1).Equals(shareList[i].Symbol == null ? "" : shareList[i].Symbol))
                                        {
                                            localSymbol = shareList[i].Symbol;
                                            localCurrent = String.Format("{0:N2}", shareList[i].Current);
                                            localValue = Convert.ToDecimal(shareList[i].Current) * Convert.ToDecimal(rdr.GetString(5));
                                        }
                                    }
                                }
                                decimal appreciation = localValue - Convert.ToDecimal(rdr.GetString(4));
                                string localAppreciate = Math.Round(appreciation, 2).ToString("#,##0");
                                if (appreciation < 0)
                                {
                                    localAppreciate = "(" + localAppreciate.Replace("-", "") + ")";
                                }
                                decimal closing = 0;
                                if (Convert.ToDecimal(rdr.GetString(4)) > 0)
                                    closing = appreciation / Convert.ToDecimal(rdr.GetString(4));
                                total++;
                                FundwiseMarketSummary item = new FundwiseMarketSummary();
                                item.FundwiseMarketSummaryId = total;
                                item.ShareName = rdr.GetString(0);
                                item.Symbol = rdr.GetString(1);
                                item.Quantity = Convert.ToDecimal(rdr.GetString(5)).ToString("#,##0");
                                item.AveragePrice = Decimal.Round(Convert.ToDecimal(rdr.GetString(3)), 2).ToString("N2");
                                item.BookCost = Decimal.Round(Convert.ToDecimal(rdr.GetString(4)), 2).ToString("N2");
                                item.MarketPrice = localCurrent == "" ? "0" : localCurrent;
                                item.MarketValue = Decimal.Round(localValue, 2).ToString("N2");
                                item.AppDep = String.Format("{0:N0}", localAppreciate);
                                item.ClosingPercentage = closing.ToString();
                                fundList.Add(item);
                            }
                        }
                    }
                }

                con.Close();
            }

            #endregion

            #region mustWork_SQLITE

            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("SQLITE"))
            {
                //IPAMS Connection String
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["IpamsConnection"].ConnectionString;
                List<FundwiseMarketSummary> _fundwiseMarket = new List<FundwiseMarketSummary>();
                fundList = new List<FundwiseMarketSummary>();
                //Getting Fund ID from Default Sqlite Database
                IQueryable<FundInfo> funds = _context.FundInfo.Where(f => f.Name.Equals(FundName));
                FundInfo fundFromDefaultDb = funds.SingleOrDefault();
                if (fundFromDefaultDb != null)
                {
                    _miscellenousData["FUND_ID"] = Convert.ToInt32(fundFromDefaultDb.Code).ToString();
                }
                //If Fund ID is not found on Default Sqlite Database Then Find it on iPAMS MS SQL Server Database
                else
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("spGetFundIDByParamNAME", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FUND_NAME", FundName));
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            _miscellenousData["FUND_ID"] = Convert.ToInt32(rdr["FUND_CODE"]).ToString();
                        }
                    }
                    conn.Close();
                }

                //Getting Shares Data from iPAMS MS SQL Server
                connIpams.Open();
                SqlCommand cmdforFetchingShare = new SqlCommand("spGetShareDetailByParamFundIdAndDate", conn);
                cmdforFetchingShare.CommandType = CommandType.StoredProcedure;
                cmdforFetchingShare.Parameters.Add(new SqlParameter("@FUND_ID", _miscellenousData["FUND_ID"]));
                cmdforFetchingShare.Parameters.Add(new SqlParameter("@DATE", DateTime.Now));
                //Share_Name = new List<String>();
                //Share_Symbol = new List<String>();
                //DateCostLastUpdated = new List<String>();
                //LastUpdatedPerUnitCost = new List<String>();
                //LastUpdatedCost = new List<String>();
                //LastUpdatedHolding = new List<String>();
                //LastUpdatedMarketPriceDate = new List<String>();
                //MarketSymbol = new List<String>();
                //MarketPriceCurrent = new List<String>();
                //MarketValue = new List<String>();
                //Appreciation_Depreciation = new List<String>();
                getShareDetail();
                // execute the command
                //QUANTITY = new List<Decimal>();
                //AVERAGE_PRICE = new List<Decimal>();
                //BOOK_COST = new List<Decimal>();
                //MARKET_PRICE = new List<Decimal>();
                //MARKET_VALUE = new List<Decimal>();
                //APPRECIATION_DEPRECIATION = new List<String>();
                //
                int total = 0;
                using (SqlDataReader rdr = cmdforFetchingShare.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        if (!rdr.IsDBNull(5))
                        {
                            if (rdr.GetDecimal(5) != 0)
                            {
                                //Share_Name.Add(rdr.GetString(0).ToString());
                                //Share_Symbol.Add(rdr.GetString(1).ToString());
                                //DateCostLastUpdated.Add(rdr.GetDateTime(2).ToString());
                                //AVERAGE_PRICE.Add(rdr.GetDecimal(3).ToString() == null ? 0 : rdr.GetDecimal(3));
                                //LastUpdatedPerUnitCost.Add(rdr.GetDecimal(3).ToString("#.##"));
                                //BOOK_COST.Add(rdr.GetDecimal(4));
                                //LastUpdatedCost.Add(Math.Round(rdr.GetDecimal(4)).ToString("#,##0"));
                                //double holding = Convert.ToDouble(rdr.GetDecimal(5));
                                //QUANTITY.Add(rdr.GetDecimal(5));
                                //LastUpdatedHolding.Add(holding.ToString("#,##0"));
                                //LastUpdatedMarketPriceDate.Add(rdr.GetDateTime(6).ToString("#.##"));
                                string localSymbol = String.Empty;
                                string localCurrent = String.Empty;
                                decimal localValue = 0;
                                if (getSymbolStatus(rdr.GetString(1).ToString()))
                                {
                                    for (int i = 0; i < shareList.Count; i++)
                                    {
                                        if (shareList[i] != null && shareList[i].Symbol != null && rdr.GetString(1).Equals(shareList[i].Symbol == null ? "" : shareList[i].Symbol))
                                        {
                                            localSymbol = shareList[i].Symbol;
                                            localCurrent = String.Format("{0:d}", shareList[i].Current);
                                            //MARKET_PRICE.Add(Decimal.Parse(shareList[i].Current));
                                            localValue = Convert.ToDecimal(shareList[i].Current) * rdr.GetDecimal(5);
                                            //MARKET_VALUE.Add(localValue);
                                        }
                                    }
                                }
                                //MarketSymbol.Add(localSymbol);
                                //MarketPriceCurrent.Add(localCurrent);
                                //MarketValue.Add(Convert.ToInt32(Math.Round(localValue)).ToString("#,##0"));
                                decimal appreciation = localValue - rdr.GetDecimal(4);
                                //APPRECIATION_DEPRECIATION.Add(appreciation.ToString());
                                string localAppreciate = Math.Round(appreciation, 2).ToString("#,##0");
                                if (appreciation < 0)
                                {
                                    localAppreciate = "(" + localAppreciate.Replace("-", "") + ")";
                                }
                                //Appreciation_Depreciation.Add(localAppreciate.ToString());
                                total++;
                                FundwiseMarketSummary item = new FundwiseMarketSummary
                                {
                                    ShareName = rdr.GetString(0),
                                    Symbol = rdr.GetString(1),
                                    Quantity = Convert.ToString(Decimal.Round(rdr.GetDecimal(5), 0)),
                                    AveragePrice = Convert.ToString(Decimal.Round(rdr.GetDecimal(3), 2)),
                                    BookCost = Convert.ToString(Decimal.Round(rdr.GetDecimal(4), 2)),
                                    MarketPrice = localCurrent == "" ? "0" : localCurrent,
                                    MarketValue = Convert.ToString(Decimal.Round(localValue, 2)),
                                    AppDep = String.Format("{0:N0}", localAppreciate)
                                };
                                fundList.Add(item);
                            }
                        }
                        else { }
                    }
                }

                connIpams.Close();

            }

            #endregion mustWork_SQLITE

        }

        #endregion

        #region btnAdd_Click

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            comboFund.SelectedIndex = 0;
            if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("MSSQLSERVER"))
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                conn.Open();
                SqlCommand cmd = new SqlCommand("spINSERT_FUND_INFO", conn);
                cmd.Parameters.AddWithValue("@FI_CODE", txtFundID.Text);
                cmd.Parameters.AddWithValue("@FI_NAME", txtFund.Text);
                cmd.Parameters.AddWithValue("@FI_SYMBOL", txtFund.Text);
                cmd.Parameters.AddWithValue("@FI_DESC", txtFund.Text);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    cmd.ExecuteReader();
                    var bc = new BrushConverter();
                    btnAdd.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#32CD32");
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine("SQL Exception Occured: " + ex.Message);
                    var bc = new BrushConverter();
                    btnAdd.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#FF6347");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Exception: " + ex.Message);
                    var bc = new BrushConverter();
                    btnAdd.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#FF6347");
                }
                finally
                {
                    conn.Close();
                }

                comboFund.Items.Clear();

                SqlConnection connDefault = new SqlConnection();
                SqlConnection connIpams = new SqlConnection();

                connDefault.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                connIpams.ConnectionString = ConfigurationManager.ConnectionStrings["IpamsConnection"].ConnectionString;

                List<String> fundName = getFundNameList(connDefault, true);
                fundName.AddRange(getFundNameList(connIpams, false));

                for (int i = 0; i < fundName.Count; i++)
                {
                    comboFund.Items.Add(fundName[i]);
                }

                comboFund.SelectedItem = fundName[0];

            }
            else if(ConfigurationManager.AppSettings["DatabaseVendor"].Equals("SQLITE")) {
                FundInfo _fund = new FundInfo();
                _fund.Code = Convert.ToInt64(txtFundID.Text);
                _fund.Name = txtFund.Text.ToString();
                _context.FundInfo.Add(_fund);
                _context.SaveChanges();
            }

            
        }

        #endregion

        #region comboFund_SelectionChanged

        private void comboFund_SelectionChanged(object sender, SelectionChangedEventArgs e)
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

        #region btnSave_Click

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run( () => RunExcel() );
        }

        #endregion

        #region btnReset_Click

        private async void btnReset_Click(object sender, RoutedEventArgs e)
        {
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = ResourceAccessor.Get("Images/gear.gif");
            image.EndInit();
            ImageBehavior.SetAnimatedSource(imgStatus, image);

            lblStatus.Text = "Status: Processing";

            if (list1.Visibility == Visibility.Visible && resetFlag == true )
            {
                fundList.Clear();
                _miscellenousData["FUND_NAME"] = comboFund.Text;
                //_miscellenousData["FUND_ID"] = getFundId(_miscellenousData["FUND_NAME"]).ToString();
                await Task.Run(() => mustWork(_miscellenousData["FUND_NAME"]));
                //await Task.Run(() => getDefault());
                //txtDate.Text = "DATE: " + _miscellenousData["DATE"];
                //txtStatus.Text = "STATUS: " + _miscellenousData["STATUS"];
                //col1.DisplayMemberBinding = new Binding("FundwiseMarketSummaryId");
                //col2.DisplayMemberBinding = new Binding("ShareName");
                //col3.DisplayMemberBinding = new Binding("Symbol");
                list1.Items.Clear();
                if (list1.Items.Count == 0 && fundList.Count > 0)
                {
                    for (int i = 0; i < fundList.Count; i++)
                    {
                        if (fundList[i].ShareName == null) { }
                        else
                        {

                            //if (Convert.ToDecimal(fundList[i].ClosingPercentage) >= Convert.ToDecimal(CLOSING_PERCENTAGE))
                            //{
                            //    FundPopUpWindowFlag = true;
                            //    string getSymbol = getFundSymbolExist(fundList[i].Symbol);
                            //    if (getSymbol != "Nil")
                            //    {
                            //        int flagBucket = UpdateToFundBacket(Convert.ToDateTime(_miscellenousData["DATE"]), _miscellenousData["STATUS"].ToUpper(), Convert.ToInt64(getFundId(FundName)), FundName, fundList[i].ShareName, fundList[i].Symbol, Convert.ToDecimal(fundList[i].Quantity.Replace(",", "")), Convert.ToDecimal(fundList[i].AveragePrice), Convert.ToDecimal(fundList[i].BookCost), Convert.ToDecimal(fundList[i].MarketPrice == "" ? "0" : fundList[i].MarketPrice), Convert.ToDecimal(fundList[i].MarketValue), Convert.ToDecimal(fundList[i].AppDep.Trim()), Convert.ToDecimal(fundList[i].ClosingPercentage));
                            //    }
                            //    else
                            //    {
                            //        int flagBucket = SavingToFundBacket(_miscellenousData["DATE"], _miscellenousData["STATUS"].ToUpper(), false, Convert.ToInt64(getFundId(FundName)), FundName, fundList[i].ShareName, fundList[i].Symbol, Convert.ToDecimal(fundList[i].Quantity.Replace(",", "")), Convert.ToDecimal(fundList[i].AveragePrice), Convert.ToDecimal(fundList[i].BookCost), Convert.ToDecimal(fundList[i].MarketPrice == "" ? "0" : fundList[i].MarketPrice), Convert.ToDecimal(fundList[i].MarketValue), Convert.ToDecimal(fundList[i].AppDep.Trim()), Convert.ToDecimal(fundList[i].ClosingPercentage));
                            //    }
                            //}
                            list1.Items.Add(new FundwiseMarketSummary { FundwiseMarketSummaryId = i + 1, ShareName = fundList[i].ShareName, Symbol = fundList[i].Symbol, Sector = fundList[i].Sector, Quantity = fundList[i].Quantity, AveragePrice = fundList[i].AveragePrice, BookCost = fundList[i].BookCost, MarketPrice = fundList[i].MarketPrice == "0" ? "Not Listed" : fundList[i].MarketPrice, MarketValue = fundList[i].MarketValue == "0.00" ? "Not Listed" : fundList[i].MarketValue, AppDep = fundList[i].AppDep.Trim(), ClosingPercentage = fundList[i].ClosingPercentage == "-1.00%" ? "-" : String.Format("{0:N2}", Math.Round(Convert.ToDecimal(fundList[i].ClosingPercentage), 2, MidpointRounding.AwayFromZero).ToString("N2") + "%") });

                            //decimal lappdepp;
                            //if (Appreciation_Depreciation[i].Contains("("))
                            //{
                            //    lappdepp = Convert.ToDecimal("-" + Appreciation_Depreciation[i].Replace("(", "").Replace(")", "").Replace(",", ""));
                            //}
                            //else
                            //{
                            //    lappdepp = Convert.ToDecimal(Appreciation_Depreciation[i].Replace("(", "").Replace(")", "").Replace(",", ""));
                            //}
                            //decimal lbookcost = Convert.ToDecimal(LastUpdatedCost[i].Replace(",", ""));
                            //decimal closing = lappdepp / lbookcost;
                            //if (closing >= CLOSING_PERCENTAGE)
                            //{
                            //    FundPopUpWindowFlag = true;
                            //    string getSymbol = getFundSymbolExist(Share_Symbol[i]);
                            //    if (getSymbol != "Nil")
                            //    {
                            //        int flagBucket = UpdateToFundBacket(Convert.ToDateTime(_miscellenousData["DATE"]), _miscellenousData["STATUS"].ToUpper(), Convert.ToInt64(getFundId(FundName)), FundName, Share_Name[i], Share_Symbol[i], Convert.ToDecimal(LastUpdatedHolding[i].Replace(",", "")), Convert.ToDecimal(LastUpdatedPerUnitCost[i]), lbookcost, Convert.ToDecimal(MarketPriceCurrent[i] == "" ? "0" : MarketPriceCurrent[i]), Convert.ToDecimal(MarketValue[i]), Convert.ToDecimal(lappdepp), Convert.ToDecimal(closing));
                            //    }
                            //    else
                            //    {
                            //        int flagBucket = SavingToFundBacket(Convert.ToDateTime(_miscellenousData["DATE"]), _miscellenousData["STATUS"].ToUpper(), false, Convert.ToInt64(getFundId(FundName)), FundName, Share_Name[i], Share_Symbol[i], Convert.ToDecimal(LastUpdatedHolding[i].Replace(",", "")), Convert.ToDecimal(LastUpdatedPerUnitCost[i]), lbookcost, Convert.ToDecimal(MarketPriceCurrent[i] == "" ? "0" : MarketPriceCurrent[i]), Convert.ToDecimal(MarketValue[i]), Convert.ToDecimal(lappdepp), Convert.ToDecimal(closing));
                            //    }

                            //}
                            //decimal laverageprice = Convert.ToDecimal(LastUpdatedPerUnitCost[i]);
                            //decimal lmarketprice = Convert.ToDecimal(MarketPriceCurrent[i] == "" ? "0.00" : MarketPriceCurrent[i]);
                            ////list1.Items.Add(new FundwiseMarketSummary { FundId = fundList[i].FundId, ShareName = fundList[i].ShareName, Symbol = fundList[i].Symbol, Quantity = fundList[i].Quantity, AveragePrice = fundList[i].AveragePrice, BookCost = fundList[i].BookCost, MarketPrice = fundList[i].MarketPrice, MarketValue = fundList[i].MarketValue, AppDep = fundList[i].AppDep });

                            //list1.Items.Add(new FundMarket { SERIAL = i + 1, NAME = Share_Name[i], SYMBOL = Share_Symbol[i], CURRENT = LastUpdatedHolding[i], LDCP = laverageprice.ToString("N2"), OPEN = LastUpdatedCost[i], HIGH = "", LOW = "", CHANGE = lmarketprice.ToString("N2") == "0.00" ? "Not Listed" : lmarketprice.ToString("N2"), VOLUME = MarketValue[i] == "0" ? "Not Listed" : MarketValue[i], APPRECIATION_DEPRECIATION = Appreciation_Depreciation[i].Trim(), PERCENTAGE_CLOSING = MarketPriceCurrent[i] == "" ? "-" : String.Format("{0:N2}", Math.Round(closing, 2, MidpointRounding.AwayFromZero).ToString("N2") + "%") });

                        }

                    }
                }
                else
                {
                    Debug.WriteLine("Already Items Located.");
                }

                FundImage.Visibility = Visibility.Hidden;
                list1.Visibility = Visibility.Visible;
                loadingImage.Visibility = Visibility.Hidden;

                txtDate.Text = "Date: " + _miscellenousData["DATE"];
                txtStatus.Text = "Status: " + _miscellenousData["STATUS"];

            }

            else
            {
                MessageBox.Show("Run the Fund Selection First.", "No Data Found.", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            //txtBucketCount.Text = " (" + getCountFundBucket().ToString() + ") ";

            var image2 = new BitmapImage();
            image2.BeginInit();
            image2.UriSource = ResourceAccessor.Get("Images/tick.gif");
            image2.EndInit();
            ImageBehavior.SetAnimatedSource(imgStatus, image2);

            lblStatus.Text = "Status: Ready";

            if (ConfigurationManager.AppSettings["PopupAlert"] == "1")
            {

                if (FundPopUpWindowFlag)
                {
                    List<Int64> bucketFundId = getFundScripBucketId();
                    foreach (Int64 id in bucketFundId)
                    {
                        if (!getFundScripBucketReadingStatus(id))
                        {
                            FundPopupWindow popupWindow = new FundPopupWindow(id);
                            popupWindow.Show();
                        }
                    }
                }
            }

        }

        #endregion

        #region SavingDataToDatabase

        private int SavingDataToDatabase(string MARKET_STATUS, Int64 FUND_ID, string FUND_NAME, string SHARE_NAME, string SHARE_SYMBOL, string QUANTITY, string AVERAGE_PRICE, string BOOK_COST, string MARKET_PRICE, string MARKET_VALUE, string APP_DEP, string ClosingPercentage)
        {
            int status = 0;
            if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("MSSQLSERVER")) {
                try
                {
                    if (SHARE_NAME != null)
                    {
                        if (MARKET_STATUS != null)
                        {
                            SqlConnection conn = new SqlConnection();
                            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                            conn.Open();
                            try
                            {
                                SqlCommand cmd = new SqlCommand("spINSERT_FUND_MARKET_SUMMARY", conn);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add("@MARKET_STATUS", SqlDbType.VarChar, 500);
                                cmd.Parameters["@MARKET_STATUS"].Value = MARKET_STATUS;
                                cmd.Parameters.Add("@FUND_ID", SqlDbType.BigInt);
                                cmd.Parameters["@FUND_ID"].Value = FUND_ID;
                                cmd.Parameters.Add("@FUND_NAME", SqlDbType.VarChar, -1);
                                cmd.Parameters["@FUND_NAME"].Value = FUND_NAME;
                                cmd.Parameters.Add("@SHR_NAME", SqlDbType.VarChar, -1);
                                cmd.Parameters["@SHR_NAME"].Value = SHARE_NAME;
                                cmd.Parameters.Add("@SHR_SYMBOL", SqlDbType.VarChar, 500);
                                cmd.Parameters["@SHR_SYMBOL"].Value = SHARE_SYMBOL;
                                cmd.Parameters.Add("@SHR_QUANTITY", SqlDbType.Decimal);
                                cmd.Parameters["@SHR_QUANTITY"].Value = QUANTITY;
                                cmd.Parameters.Add("@SHR_AVG_PRICE", SqlDbType.Decimal);
                                cmd.Parameters["@SHR_AVG_PRICE"].Value = AVERAGE_PRICE;
                                cmd.Parameters.Add("@SHR_BOOK_COST", SqlDbType.Decimal);
                                cmd.Parameters["@SHR_BOOK_COST"].Value = BOOK_COST;
                                cmd.Parameters.Add("@SHR_MARKET_PRICE", SqlDbType.Decimal);
                                cmd.Parameters["@SHR_MARKET_PRICE"].Value = MARKET_PRICE;
                                cmd.Parameters.Add("@SHR_MARKET_VALUE", SqlDbType.Decimal);
                                cmd.Parameters["@SHR_MARKET_VALUE"].Value = MARKET_VALUE;
                                cmd.Parameters.Add("@SHR_APP_DEP", SqlDbType.VarChar);
                                cmd.Parameters["@SHR_APP_DEP"].Value = APP_DEP;
                                cmd.Parameters.Add("@SHR_CLOSING_PERCENTAGE", SqlDbType.VarChar);
                                cmd.Parameters["@SHR_CLOSING_PERCENTAGE"].Value = ClosingPercentage;
                                status = cmd.ExecuteNonQuery();
                                return status;
                            }
                            catch (SqlException ex)
                            {
                                MessageBox.Show(ex.Message, "Database Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                Debug.WriteLine("SQL Exception: " + ex.Message);
                                return 0;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Database Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                Debug.WriteLine("General Exception: " + ex.Message);
                                return 0;
                            }
                            finally
                            {
                                conn.Close();
                            }
                        }
                        else
                        {
                            status = 0;
                        }
                    }
                    else
                    {
                        status = 0;
                    }
                }
                catch (NullReferenceException ex)
                {
                    MessageBox.Show("Null Reference Exception Occured.." + ex.Message);
                    status = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("General Exception on Saving Data in Database..: " + ex.Message);
                    status = 0;
                }
            }
            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("SQLITE"))
            {
                FundwiseMarketSummary item = new FundwiseMarketSummary();
                item.Status = MARKET_STATUS.ToString();
                item.FundId = FUND_ID.ToString();
                item.ShareName = SHARE_NAME;
                item.Symbol = SHARE_SYMBOL;
                item.Quantity = QUANTITY.ToString();
                item.AveragePrice = AVERAGE_PRICE.ToString();
                item.BookCost = BOOK_COST.ToString();
                item.MarketPrice = MARKET_PRICE.ToString();
                item.MarketValue = MARKET_VALUE.ToString();
                item.AppDep = APP_DEP.ToString();
                item.ClosingPercentage = ClosingPercentage.ToString();
                _context.FundwiseMarketSummary.Add(item);
                _context.SaveChanges();
            }
            return status;
        }

        #endregion

        #region RunExcel

        /// <summary>
        /// Sample 1 - Simply creates a new workbook from scratch.
        /// The workbook contains one worksheet with a simple invertory list
        /// Data is loaded manually via the Cells property of the Worksheet.
        /// </summary>
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
                    var worksheet = package.Workbook.Worksheets.Add("Fund Market Summary");
                    //Add the headers
                    worksheet.Cells[1, 1].Value = "Sr. No.";
                    worksheet.Cells[1, 2].Value = "NAME";
                    worksheet.Cells[1, 3].Value = "SYMBOL";
                    worksheet.Cells[1, 4].Value = "QUANTITY";
                    worksheet.Cells[1, 5].Value = "AVERAGE PRICE";
                    worksheet.Cells[1, 6].Value = "BOOK COST";
                    worksheet.Cells[1, 7].Value = "MARKET PRICE";
                    worksheet.Cells[1, 8].Value = "MARKET VALUE";
                    worksheet.Cells[1, 9].Value = "APPRECIATION / DEPRECIATION";
                    worksheet.Cells[1, 10].Value = "(%) Closing";

                    //Add some items...
                    List<FundwiseMarketSummary> items = getFundMarketSummary();

                    FundMarket[] fund = new FundMarket[items.Count];
                    for (int i = 0; i < items.Count; i++)
                    {
                        int count = i;
                        fund[i] = new FundMarket();
                        fund[i].SERIAL = Convert.ToInt32(items[i].FundId);
                        fund[i].NAME = items[i].ShareName;
                        fund[i].SYMBOL = items[i].Symbol;
                        fund[i].CURRENT = items[i].Quantity;
                        decimal laverageprice = Convert.ToDecimal(items[i].AveragePrice);
                        fund[i].LDCP = laverageprice.ToString();
                        fund[i].OPEN = items[i].BookCost;
                        decimal lmarketprice = Convert.ToDecimal(items[i].MarketPrice);
                        fund[i].CHANGE = lmarketprice.ToString("N2");
                        fund[i].VOLUME = items[i].MarketValue;
                        decimal lappdep = Math.Round(Convert.ToDecimal(items[i].AppDep), MidpointRounding.AwayFromZero);
                        fund[i].APPRECIATION_DEPRECIATION = lappdep.ToString("N0");
                    }

                    int total = 1;
                    for (int index = 0; index < items.Count; index++)
                    {
                        total = index + 2;
                        worksheet.Cells["A" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["A" + total].Value = fund[index].SERIAL + 1;
                        worksheet.Cells["B" + total].Value = fund[index].NAME;
                        worksheet.Cells["C" + total].Value = fund[index].SYMBOL;
                        worksheet.Cells["D" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["D" + total].Value = fund[index].CURRENT;
                        worksheet.Cells["E" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["E" + total].Value = fund[index].LDCP;
                        worksheet.Cells["F" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["F" + total].Value = fund[index].OPEN;
                        worksheet.Cells["G" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["G" + total].Value = fund[index].CHANGE;
                        worksheet.Cells["H" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["H" + total].Value = fund[index].VOLUME;
                        worksheet.Cells["I" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["I" + total].Value = fund[index].APPRECIATION_DEPRECIATION;
                    }

                    //EndTest

                    //Ok now format the values;
                    using (var range = worksheet.Cells[1, 1, 1, 10])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.DarkBlue);
                        range.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    }

                    //Create an autofilter for the range
                    worksheet.Cells["A1:I4"].AutoFilter = true;

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
                    package.Workbook.Properties.Title = "Fund Market Summary";
                    package.Workbook.Properties.Author = "Saad Ahmed";
                    package.Workbook.Properties.Comments = "This is the fund market summary report.";

                    // Set some extended property values
                    package.Workbook.Properties.Company = "EPPlus Software AB";

                    // Set some custom property values
                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", "Jan Källman");
                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", "EPPlus");

                    //var xlFile = FileOutputUtil.GetFileInfo("01-GettingStarted.xlsx");
                    string path = "FundMarketSummary.xlsx";
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
                MessageBox.Show(ex.Message,"Problem: ",MessageBoxButton.OK, MessageBoxImage.Exclamation);
                Debug.WriteLine("Exception: " + ex.Message);
            }
            finally
            {
                //xlFile.Dispose();
                //package.Dispose();
            }
            Debug.WriteLine("Excel Sheet Created.");
            Thread.Sleep(1000);
            if (File.Exists("FundMarketSummary.xlsx"))
            {
                try
                {
                    //using (Stream stream = new FileStream("FundMarketSummary.xlsx", FileMode.Open))
                    //{
                    // File/Stream manipulating code here
                    Process p = new Process();
                    p.StartInfo.UseShellExecute = true;
                    p.StartInfo.FileName = "FundMarketSummary.xlsx";
                    p.Start();
                    //}
                }
                catch(Exception ex)
                {
                    //check here why it failed and ask user to retry if the file is in use.
                    MessageBox.Show(ex.Message);
                }
                
            }

        }

        #endregion

        #region KeyExists

        public bool KeyExists(string strKey)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(AppDomain.CurrentDomain.BaseDirectory + "~/App.config");

            XmlNode appSettingsNode = xmlDoc.SelectSingleNode("configuration/appSettings");

            foreach (XmlNode childNode in appSettingsNode)
            {
                if (childNode.Attributes["key"].Value == strKey)
                    return true;
            }
            return false;
        }

        #endregion

        #region SavingToFundBacket

        private int SavingToFundBacket(string _date, string _status, bool _readingStatus, Int64 _fundId, string _fundName, string _shareName, string _shareSymbol, decimal _quantity, decimal _averagePrice, decimal _bookCost, decimal _marketPrice, decimal _marketValue, decimal _appDep, decimal _percentageClosing)
        {
            int status = 0;
            if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("MSSQLSERVER"))
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                conn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("spINSERT_FUND_SCRIP_BUCKET", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@FSB_DATE", SqlDbType.DateTime);
                    cmd.Parameters["@FSB_DATE"].Value = _date;
                    cmd.Parameters.Add("@FSB_STATUS", SqlDbType.VarChar, -1);
                    cmd.Parameters["@FSB_STATUS"].Value = _status;
                    cmd.Parameters.Add("@FSB_READING_STATUS", SqlDbType.Bit);
                    cmd.Parameters["@FSB_READING_STATUS"].Value = _readingStatus;
                    cmd.Parameters.Add("@FSB_FUND_ID", SqlDbType.BigInt);
                    cmd.Parameters["@FSB_FUND_ID"].Value = _fundId;
                    cmd.Parameters.Add("@FSB_FUND_NAME", SqlDbType.VarChar, -1);
                    cmd.Parameters["@FSB_FUND_NAME"].Value = _fundName;
                    cmd.Parameters.Add("@FSB_SHARE_NAME", SqlDbType.VarChar, -1);
                    cmd.Parameters["@FSB_SHARE_NAME"].Value = _shareName;
                    cmd.Parameters.Add("@FSB_SHARE_SYMBOL", SqlDbType.VarChar, 500);
                    cmd.Parameters["@FSB_SHARE_SYMBOL"].Value = _shareSymbol;
                    cmd.Parameters.Add("@FSB_SHARE_QUANTITY", SqlDbType.Decimal);
                    cmd.Parameters["@FSB_SHARE_QUANTITY"].Value = _quantity;
                    cmd.Parameters.Add("@FSB_SHARE_AVG_PRICE", SqlDbType.Decimal);
                    cmd.Parameters["@FSB_SHARE_AVG_PRICE"].Value = _averagePrice;
                    cmd.Parameters.Add("@FSB_SHARE_BOOK_COST", SqlDbType.Decimal);
                    cmd.Parameters["@FSB_SHARE_BOOK_COST"].Value = _bookCost;
                    cmd.Parameters.Add("@FSB_SHARE_MARKET_PRICE", SqlDbType.Decimal);
                    cmd.Parameters["@FSB_SHARE_MARKET_PRICE"].Value = _marketPrice;
                    cmd.Parameters.Add("@FSB_SHARE_MARKET_VALUE", SqlDbType.Decimal);
                    cmd.Parameters["@FSB_SHARE_MARKET_VALUE"].Value = _marketValue;
                    cmd.Parameters.Add("@FSB_SHARE_APP_DEP", SqlDbType.Decimal);
                    cmd.Parameters["@FSB_SHARE_APP_DEP"].Value = _appDep;
                    cmd.Parameters.Add("@FSB_SHARE_PERCENTAGE_CLOSING", SqlDbType.Decimal);
                    cmd.Parameters["@FSB_SHARE_PERCENTAGE_CLOSING"].Value = _percentageClosing;

                    status = cmd.ExecuteNonQuery();
                    return status;

                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Database Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Debug.WriteLine("SQL Exception: " + ex.Message);
                    return status;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Database Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Debug.WriteLine("General Exception: " + ex.Message);
                    return status;
                }
                finally
                {
                    conn.Close();
                }
                
            }
            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("SQLITE"))
            {
                _context.FundwiseBucketMarketSummary.Add(new FundwiseBucketMarketSummary { Date = Convert.ToDateTime(_date),FundId = _fundId, AppDep = _appDep.ToString(), AveragePrice = _averagePrice.ToString(), BookCost = _bookCost.ToString(), ClosingPercentage = _percentageClosing.ToString(), MarketPrice = _marketPrice.ToString(), MarketValue = _marketValue.ToString(), Quantity = _quantity.ToString(), ReadingStatus = _readingStatus.ToString(), ShareName = _shareName, Status = _status, Symbol = _shareSymbol  });
                _context.SaveChanges();
                status = 1;
            }
            return status;
        }

        #endregion

        #region UpdateToFundBacket

        public int UpdateToFundBacket(DateTime _date, string _status, Int64 _fundId, string _fundName, string _shareName, string _shareSymbol, decimal _quantity, decimal _averagePrice, decimal _bookCost, decimal _marketPrice, decimal _marketValue, decimal _appDep, decimal _percentageClosing)
        {
            int status = 0;

            if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("MSSQLSERVER"))
            {
                if (_shareName != null)
                {
                    SqlConnection conn = new SqlConnection();
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    conn.Open();
                    try
                    {
                        SqlCommand cmd = new SqlCommand("spUPDATE_FUND_SCRIP_BUCKET", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@FSB_DATE", SqlDbType.DateTime);
                        cmd.Parameters["@FSB_DATE"].Value = _date;
                        cmd.Parameters.Add("@FSB_STATUS", SqlDbType.VarChar, -1);
                        cmd.Parameters["@FSB_STATUS"].Value = _status;
                        cmd.Parameters.Add("@FSB_FUND_ID", SqlDbType.BigInt);
                        cmd.Parameters["@FSB_FUND_ID"].Value = _fundId;
                        cmd.Parameters.Add("@FSB_FUND_NAME", SqlDbType.VarChar, -1);
                        cmd.Parameters["@FSB_FUND_NAME"].Value = _fundName;
                        cmd.Parameters.Add("@FSB_SHARE_NAME", SqlDbType.VarChar, -1);
                        cmd.Parameters["@FSB_SHARE_NAME"].Value = _shareName;
                        cmd.Parameters.Add("@FSB_SHARE_SYMBOL", SqlDbType.VarChar, 500);
                        cmd.Parameters["@FSB_SHARE_SYMBOL"].Value = _shareSymbol;
                        cmd.Parameters.Add("@FSB_SHARE_QUANTITY", SqlDbType.Decimal);
                        cmd.Parameters["@FSB_SHARE_QUANTITY"].Value = _quantity;
                        cmd.Parameters.Add("@FSB_SHARE_AVG_PRICE", SqlDbType.Decimal);
                        cmd.Parameters["@FSB_SHARE_AVG_PRICE"].Value = _averagePrice;
                        cmd.Parameters.Add("@FSB_SHARE_BOOK_COST", SqlDbType.Decimal);
                        cmd.Parameters["@FSB_SHARE_BOOK_COST"].Value = _bookCost;
                        cmd.Parameters.Add("@FSB_SHARE_MARKET_PRICE", SqlDbType.Decimal);
                        cmd.Parameters["@FSB_SHARE_MARKET_PRICE"].Value = _marketPrice;
                        cmd.Parameters.Add("@FSB_SHARE_MARKET_VALUE", SqlDbType.Decimal);
                        cmd.Parameters["@FSB_SHARE_MARKET_VALUE"].Value = _marketValue;
                        cmd.Parameters.Add("@FSB_SHARE_APP_DEP", SqlDbType.Decimal);
                        cmd.Parameters["@FSB_SHARE_APP_DEP"].Value = _appDep;
                        cmd.Parameters.Add("@FSB_SHARE_PERCENTAGE_CLOSING", SqlDbType.Decimal);
                        cmd.Parameters["@FSB_SHARE_PERCENTAGE_CLOSING"].Value = _percentageClosing;

                        status = cmd.ExecuteNonQuery();
                        return status;

                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message, "Database Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        Debug.WriteLine("SQL Exception: " + ex.Message);
                        status = 0;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Database Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        Debug.WriteLine("General Exception: " + ex.Message);
                        status = 0;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else
                {
                    status = 0;
                }
            }
            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("SQLITE"))
            {
                _context.FundwiseBucketMarketSummary.Add(new FundwiseBucketMarketSummary { Date = _date, FundId = _fundId, AppDep = _appDep.ToString(), AveragePrice = _averagePrice.ToString(), BookCost = _bookCost.ToString(), ClosingPercentage = _percentageClosing.ToString(), MarketPrice = _marketPrice.ToString(), MarketValue = _marketValue.ToString(), Quantity = _quantity.ToString(), ShareName = _shareName, Status = _status, Symbol = _shareSymbol });
                _context.SaveChanges();
                status = 1;
            }
            return status;
        }

        #endregion

        #region ClearFundBacket

        private int ClearFundBacket()
        {
            int status = 0;

            #region ClearFundBacket_MSSQLSERVER
            if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("MSSQLSERVER"))
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                conn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("spTRUNCATE_FUND_SCRIP_BUCKET", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    status = cmd.ExecuteNonQuery();
                    return status;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Database Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Debug.WriteLine("SQL Exception: " + ex.Message);
                    status = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Database Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Debug.WriteLine("General Exception: " + ex.Message);
                    status = 0;
                }
                finally
                {
                    conn.Close();
                }
            }
            #endregion

            #region ClearFundBacket_ORACLE

            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("ORACLE"))
            {
                OracleConnection conn = new OracleConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                conn.Open();
                try
                {
                    OracleCommand cmd = new OracleCommand("TRUNCATE TABLE FUND_SCRIP_BUCKET", conn);
                    cmd.CommandType = CommandType.Text;
                    status = cmd.ExecuteNonQuery();
                    return status;
                }
                catch (OracleException ex)
                {
                    MessageBox.Show(ex.Message, "Database Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Debug.WriteLine("SQL Exception: " + ex.Message);
                    status = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Database Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Debug.WriteLine("General Exception: " + ex.Message);
                    status = 0;
                }
                finally
                {
                    conn.Close();
                }
            }

            #endregion

            #region ClearFundBacket_SQLITE

            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("SQLITE"))
            {
                _context.FundwiseBucketMarketSummary.RemoveRange();
                _context.SaveChanges();
                status = -1;
            }

            #endregion

            return status;
        }

        #endregion

        #region ClearFundMarketSummary

        private int ClearFundMarketSummary()
        {
            int status = 0;
            if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("MSSQLSERVER")) {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                conn.Open();

                try
                {
                    SqlCommand cmd = new SqlCommand("spTRUNCATE_FUND_MARKET_SUMMARY", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    status = cmd.ExecuteNonQuery();
                    return status;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Database Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Debug.WriteLine("SQL Exception: " + ex.Message);
                    status = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Database Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Debug.WriteLine("General Exception: " + ex.Message);
                    status = 0;
                }
                finally
                {
                    conn.Close();
                }
            }
            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("SQLITE"))
            {
                _context.FundwiseBucketMarketSummary.RemoveRange();
                _context.SaveChanges();
                status = 1;
            }
            return status;

        }

        #endregion



        #region getFundSymbolExist

        public string getFundSymbolExist(string _symbol)
        {
            string _id = String.Empty;
            if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("MSSQLSERVER")) {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                conn.Open();
                // 1.  create a command object identifying the stored procedure
                SqlCommand cmd = new SqlCommand("spGET_FUND_SCRIP_BUCKET_SYMBOL_EXIST", conn);

                // 2. set the command object so it knows to execute a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                // 3. add parameter to command, which will be passed to the stored procedure
                cmd.Parameters.Add(new SqlParameter("@Symbol", _symbol));

                // execute the command
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    // iterate through results, printing each to console
                    while (rdr.Read())
                    {
                        _id = rdr.GetString(0);
                        //Debug.WriteLine("FUND_ID in Default DB: " + FUND_ID);
                    }
                }

                conn.Close();
            }
            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("SQLITE"))
            {
                IQueryable<FundwiseBucketMarketSummary> item = _context.FundwiseBucketMarketSummary.Where(fsb => fsb.Symbol == _symbol);
                List<FundwiseBucketMarketSummary> fsb_list = item.ToList();
                if(fsb_list.Count > 0)
                {
                    _id = fsb_list.First().FundwiseBucketMarketSummaryId.ToString();
                }
                else
                {
                    _id = "Nil";
                }
            }
            return _id;
        }

        #endregion

        #region getFundScripBucketId

        public List<Int64> getFundScripBucketId()
        {
            List<Int64> bucket = new List<Int64>();
            if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("MSSQLSERVER"))
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                conn.Open();
                SqlCommand cmd = new SqlCommand("spGET_SB_FUND_SCRIP_BUCKET", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        bucket.Add(rdr.GetInt64(0));
                    }
                }
                conn.Close();
            }
            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("SQLITE"))
            {
                IEnumerable<FundwiseBucketMarketSummary> _bucketFund = _context.FundwiseBucketMarketSummary;
                foreach(FundwiseBucketMarketSummary item in _bucketFund)
                {
                    bucket.Add(item.FundwiseBucketMarketSummaryId);
                }
            }
            return bucket;
        }

        #endregion

        #region getFundScripBucketReadingStatus

        public bool getFundScripBucketReadingStatus(Int64 id)
        {
            bool status = false;
            if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("MSSQLSERVER"))
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                conn.Open();
                SqlCommand cmd = new SqlCommand("spGET_FUND_SCRIP_BUCKET_READING_STATUS", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", id));
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        Debug.WriteLine("Reading Status: " + rdr.GetBoolean(0));
                        status = rdr.GetBoolean(0);
                    }
                }
                conn.Close();
            }
            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("SQLITE"))
            {
                status = Convert.ToBoolean(_context.FundwiseBucketMarketSummary.Find(id).ReadingStatus);
            }

            return status;
        }

        #endregion

        #region getCountFundBucket

        public int getCountFundBucket()
        {
            int _id = 0;
            if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("MSSQLSERVER"))
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                conn.Open();
                // 1.  create a command object identifying the stored procedure
                SqlCommand cmd = new SqlCommand("spCOUNT_FUND_SCRIP_BUCKET", conn);

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
                        _id = rdr.GetInt32(0);
                        //Debug.WriteLine("FUND_ID in Default DB: " + FUND_ID);
                    }
                }

                conn.Close();
            }
            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("ORACLE"))
            {
                OracleConnection conn = new OracleConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                conn.Open();
                // 1.  create a command object identifying the stored procedure
                OracleCommand cmd = new OracleCommand("spCOUNT_FUND_SCRIP_BUCKET", conn);

                // 2. set the command object so it knows to execute a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                // 3. add parameter to command, which will be passed to the stored procedure
                //cmd.Parameters.Add(new SqlParameter("@FUND_NAME", _fundName));

                // execute the command
                using (OracleDataReader rdr = cmd.ExecuteReader())
                {
                    // iterate through results, printing each to console
                    while (rdr.Read())
                    {
                        _id = rdr.GetInt32(0);
                        //Debug.WriteLine("FUND_ID in Default DB: " + FUND_ID);
                    }
                }

                conn.Close();
            }
            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("SQLITE"))
            {
                List<FundwiseBucketMarketSummary> items = _context.FundwiseBucketMarketSummary.ToList();
                _id = items.Count;
            }
            return _id;
        }

        #endregion

        #region btnViewBucket_Click

        private void btnViewBucket_Click(object sender, RoutedEventArgs e)
        {
            FundBucket window = new FundBucket();
            window.Show();
            //this.Hide();
        }

        #endregion

        #region getFundMarketSummary

        public List<FundwiseMarketSummary> getFundMarketSummary()
        {
            List<FundwiseMarketSummary> items = new List<FundwiseMarketSummary>();
            if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("MSSQLSERVER")) {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                conn.Open();
                // 1.  create a command object identifying the stored procedure
                SqlCommand cmd = new SqlCommand("spGET_FUND_MARKET_SUMMARY", conn);

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
                        items.Add(new FundwiseMarketSummary { FundId = rdr.GetInt64(0).ToString(), ShareName = rdr.GetString(5), Symbol = rdr.GetString(6), Quantity = rdr.GetDecimal(7).ToString(), AveragePrice = rdr.GetDecimal(8).ToString(), BookCost = rdr.GetDecimal(9).ToString(), MarketPrice = rdr.GetDecimal(10).ToString("N2"), MarketValue = rdr.GetDecimal(11).ToString(), AppDep = rdr.GetString(12) });
                        //Debug.WriteLine("FUND_ID in Default DB: " + FUND_ID);
                    }
                }

                conn.Close();
            }
            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("SQLITE"))
            {
                //IEnumerable<FundwiseMarketSummary> data = _context.FundwiseMarketSummary.ToList();
                //items.AddRange(data);
            }
            return items;
        }

        #endregion

        #region Email

        public static void Email(string htmlString)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(ConfigurationManager.AppSettings["MailFrom"].ToString());
                message.To.Add(new MailAddress(ConfigurationManager.AppSettings["MailTo"].ToString()));
                message.Subject = "Test";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = htmlString;
                smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"].ToString());
                smtp.Host = ConfigurationManager.AppSettings["host"].ToString(); //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["MailFrom"].ToString(), ConfigurationManager.AppSettings["Password"].ToString());
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception) { }
        }

        #endregion

        #region Window_Closing

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            //MessageBoxResult d;
            //d = MessageBox.Show("Do you really want to close Data Extractor Utility?", "Data Extractor Utility", MessageBoxButton.YesNo, MessageBoxImage.Question);
            //if (d == MessageBoxResult.Yes)
            //{
            Application.Current.Shutdown();
            //Application.Current.MainWindow.Show();
            //}
            //else
            //{
            //    e.Cancel = true;
            //}
        }

        #endregion

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

        }
    }

}
