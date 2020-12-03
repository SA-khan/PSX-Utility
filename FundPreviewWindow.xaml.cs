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

namespace PSXDataFetchingApp
{
    /// <summary>
    /// Interaction logic for FundPreviewWindow.xaml
    /// </summary>
    public partial class FundPreviewWindow : Window
    {

        public static decimal CLOSING_PERCENTAGE = Convert.ToDecimal(ConfigurationManager.AppSettings["FundClosingPercentage"]);

        public Configuration configuration;
        public MainWindow mainClass = new MainWindow();
        BackgroundWorker worker = new BackgroundWorker();

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

        DateTime MARKET_DATE;
        String MARKET_STATUS;
        Int64 FUND_ID1;
        String FUND_NAME1;

        List<String> Share_Name;
        List<String> Share_Symbol;
        List<String> DateCostLastUpdated;
        List<String> LastUpdatedPerUnitCost;
        List<String> LastUpdatedCost;
        List<String> LastUpdatedHolding;
        List<String> LastUpdatedMarketPriceDate;
        List<String> MarketSymbol;
        List<String> MarketPriceCurrent;
        List<String> MarketValue;
        List<String> Appreciation_Depreciation;
        static List<SHARE> testShare;

        String[] DefaultData;

        //
        //QUANTITY
        List<Decimal> QUANTITY; //LastUpdatedHolding1
        //AVERAGE PRICE
        List<Decimal> AVERAGE_PRICE; //LastUpdatedPerUnitCost
        //Book Cost
        List<Decimal> BOOK_COST; //LastUpdatedCost
        //MARKET PRICE
        List<Decimal> MARKET_PRICE;
        //MARKET VALUE
        List<Decimal> MARKET_VALUE;
        //APPRECIATION
        List<String> APPRECIATION_DEPRECIATION;


        //public static SHARE[] share;

        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        int countTimer = 0;

        //New Edition 11/17/2020
        //By Saad
        List<SpecificFundDetail> spFundDetial;

        //New Edition 11/18/2020
        //By Saad
        bool resetFlag = false;

        //New Edition 11/23/2020
        public static bool FundPopUpWindowFlag = false;

        public FundPreviewWindow()
        {
            InitializeComponent();

            txtDate.Text = "Date:" + DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm tt");

            //Client Specific Properties
            try
            {
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["Client"]))
                {
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
                }
            }
            catch { }

            SqlConnection connDefault = new SqlConnection();
            SqlConnection connIpams = new SqlConnection();

            connDefault.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            connIpams.ConnectionString = ConfigurationManager.ConnectionStrings["IpamsConnection"].ConnectionString;
            
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

        public List<String> getFundNameList(SqlConnection connectionName, bool defaultDB)
        {
            List<String> result = new List<string>();
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
            return result;
        }

        public bool getSymbolStatus(string Symbol)
        {
            bool result = false;
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
            return result;
        }

        public void getDefault()
        {
            DefaultData = mainClass.GetDefault();
        }


        public List<SHARE> getShareDetail()
        {
            List<string> NAME = mainClass.GetMarketSummaryCompanyNames();
            List<string> SYMBOL = mainClass.GetMarketSummaryCompanySymbols(NAME);
            string[] CURRENT = mainClass.GetMarketSummaryCompanyCURRENT();

            List<SHARE> share = new List<SHARE>();
            for (int i = 0; i < NAME.Count; i++)
            {
                share.Add(new SHARE { SHARE_NAME = NAME[i], SHARE_SYMBOL = SYMBOL[i], SHARE_CURRENT = CURRENT[i] });
                //Debug.WriteLine("FNAME: " + NAME[i] + ", FSYMBOL: " + SYMBOL[i] + ", FCURRENT: " + CURRENT[i]);
            }
            return share;
        }

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
                }

                var image = new BitmapImage();
                image.BeginInit();
                image.UriSource = ResourceAccessor.Get("Images/exclaimation.png");
                image.EndInit();
                ImageBehavior.SetAnimatedSource(imgStatus, image);
                lblStatus.Text = "Status: Processing";

                string FundName = comboFund.Text;
                if (FundName == "Select..")
                {
                    MessageBox.Show("Please Select A Fund from the list.", "Fund Selection Required.", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (FundName == "<ADD NEW FUND>")
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
                    //imgStatus.Source = new BitmapImage(ResourceAccessor.Get("Images/Exclaimation.png"));


                    FundImage.Visibility = Visibility.Hidden;
                    list1.Visibility = Visibility.Hidden;
                    loadingImage.Visibility = Visibility.Visible;

                    //FundImage.Source = new BitmapImage(ResourceAccessor.Get("Images/exclaimation.png"));


                    //for (int i = 0; i < 10; i++)
                    //{
                    //    this.InvalidateVisual();
                    //    this.Dispatcher.Invoke(delegate () { }, DispatcherPriority.Render);
                    //    this.Dispatcher.Invoke(delegate () { }, DispatcherPriority.Render);
                    //}

                    await Task.Run(() => mustWork(FundName));



                    //worker.DoWork += worker_DoWork;

                    //worker.DoWork += worker_DoWork;
                    //worker.ProgressChanged += worker_ProgressChanged;
                    //worker.RunWorkerCompleted += worker_RunWorkerCompleted;
                    //worker.RunWorkerAsync();

                    //Task.Run(async delegate
                    //{
                    //        await Task.Yield(); // fork the continuation into a separate work item
                    //});
                    await Task.Run(() => getDefault());
                    MARKET_DATE = DateTime.Parse(DefaultData[0]);
                    txtDate.Text = "DATE: " + MARKET_DATE.ToString("dddd, dd MMMM yyyy hh:mm tt");
                    MARKET_STATUS = DefaultData[1];
                    txtStatus.Text = "STATUS: " + MARKET_STATUS;



                    //FundMarket[] data = new FundMarket[Share_Name.Count];

                    col1.DisplayMemberBinding = new Binding("SERIAL");
                    //col1.DisplayMemberBinding = new Binding("FundId");
                    col2.DisplayMemberBinding = new Binding("NAME");
                    //col2.DisplayMemberBinding = new Binding("Name");
                    col3.DisplayMemberBinding = new Binding("SYMBOL");
                    //col3.DisplayMemberBinding = new Binding("Symbol");
                    //col4.DisplayMemberBinding = new Binding("Quantity");
                    //col5.DisplayMemberBinding = new Binding("AveragePrice");
                    //col6.DisplayMemberBinding = new Binding("BookCost");
                    //col7.DisplayMemberBinding = new Binding("MarketPrice");
                    //col8.DisplayMemberBinding = new Binding("MarketValue");
                    //col9.DisplayMemberBinding = new Binding("AppDep");

                    //gvc1.DisplayMemberBinding = new Binding("SERIAL");
                    //gvc2.DisplayMemberBinding = new Binding("NAME");
                    //gvc3.DisplayMemberBinding = new Binding("SYMBOL");
                    //gvc4.DisplayMemberBinding = new Binding("CURRENT");
                    //gvc5.DisplayMemberBinding = new Binding("LDCP");
                    //gvc6.DisplayMemberBinding = new Binding("OPEN");
                    //gvc7.DisplayMemberBinding = new Binding("CHANGE");
                    //gvc8.DisplayMemberBinding = new Binding("VOLUME");
                    //gvc9.DisplayMemberBinding = new Binding("APPRECIATION_DEPRECIATION");


                    list1.Items.Clear();

                    FundImage.Visibility = Visibility.Hidden;
                    loadingImage.Visibility = Visibility.Hidden;
                    list1.Visibility = Visibility.Visible;

                    //int total = 0;

                    int flagClearBucket = ClearFundBacket();
                    Debug.WriteLine("Clear Table Status: " + flagClearBucket);
                    //Debug.WriteLine("Clear Fund Bucker Data Count: " + flagClearBucket);
                    if (flagClearBucket == -1)
                    {
                        for (int i = 0; i < spFundDetial.Count; i++)
                        {
                            if (spFundDetial[i].Name == null) { }
                            else
                            {

                                //if (Appreciation_Depreciation[i].StartsWith('('))
                                //{
                                //    //var bc = new BrushConverter();

                                //    //myGridView.AllowsColumnReorder = true;
                                //    //myGridView.ColumnHeaderToolTip = "Fund Market Information";


                                //    //gvc1.DisplayMemberBinding = new Binding("SERIAL");
                                //    //gvc1.Header = "Sr. No.";
                                //    //gvc1.Width = 50;
                                //    //myGridView.Columns.Add(gvc1);
                                //    ////GridViewColumn gvc2 = new GridViewColumn();
                                //    //gvc2.DisplayMemberBinding = new Binding("NAME");
                                //    //gvc2.Header = "NAME";
                                //    //gvc2.Width = 250;
                                //    //myGridView.Columns.Add(gvc2);
                                //    ////GridViewColumn gvc3 = new GridViewColumn();
                                //    //gvc3.DisplayMemberBinding = new Binding("SYMBOL");
                                //    //gvc3.Header = "SYMBOL";
                                //    //gvc3.Width = 60;
                                //    //myGridView.Columns.Add(gvc3);
                                //    ////GridViewColumn gvc4 = new GridViewColumn();
                                //    //gvc4.DisplayMemberBinding = new Binding("CURRENT");
                                //    //gvc4.Header = "Quantity";
                                //    //gvc4.Width = 70;
                                //    //myGridView.Columns.Add(gvc4);
                                //    ////GridViewColumn gvc5 = new GridViewColumn();
                                //    //gvc5.DisplayMemberBinding = new Binding("LDCP");
                                //    //gvc5.Header = "Avg. Price";
                                //    //gvc5.Width = 80;
                                //    //myGridView.Columns.Add(gvc5);
                                //    ////GridViewColumn gvc6 = new GridViewColumn();
                                //    //gvc6.DisplayMemberBinding = new Binding("OPEN");
                                //    //gvc6.Header = "Book Cost";
                                //    //gvc6.Width = 80;
                                //    //myGridView.Columns.Add(gvc6);
                                //    ////GridViewColumn gvc7 = new GridViewColumn();
                                //    //gvc7.DisplayMemberBinding = new Binding("CHANGE");
                                //    //gvc7.Header = "Market Price";
                                //    //gvc7.Width = 85;
                                //    //myGridView.Columns.Add(gvc7);
                                //    ////GridViewColumn gvc8 = new GridViewColumn();
                                //    //gvc8.DisplayMemberBinding = new Binding("VOLUME");
                                //    //gvc8.Header = "Market Value";
                                //    //gvc8.Width = 100;
                                //    //myGridView.Columns.Add(gvc8);
                                //    ////GridViewColumn gvc9 = new GridViewColumn();
                                //    //gvc9.DisplayMemberBinding = new Binding("APPRECIATION_DEPRECIATION");
                                //    //gvc9.Header = "App. / Dep.";
                                //    //gvc9.Width = 80;
                                //    ////gvc9..ItemStyle.BackColor = System.Drawing.Color.Red;
                                //    //myGridView.Columns.Add(gvc9);
                                //    //list1.View = myGridView;
                                //    //HeaderColor.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#f0a500");

                                //    //Style style = new Style();
                                //    //style.TargetType = typeof(GridViewColumn);
                                //    ////style.Setters.Add(new Setter(GridViewColumn.BackgroundProperty, (System.Windows.Media.Brush)bc.ConvertFrom("#f0a500")));
                                //    //list1.ItemContainerStyle = style;

                                //    ////gvc9.S

                                //    //Style style2 = new Style();
                                //    //style2.TargetType = typeof(ListViewItem);
                                //    //style2.Setters.Add(new Setter(ListViewItem.ForegroundProperty, System.Drawing.Brushes.Red));
                                //    //list1.SelectedItem = style2;
                                //    ////ItemsSource is ObservableCollection of EmployeeInfo objects;
                                //    //list1.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#FF0000");
                                //    //list1.Items.Add(new FundMarket { SERIAL = i + 1, NAME = Share_Name[i], SYMBOL = Share_Symbol[i], CURRENT = LastUpdatedHolding[i], LDCP = LastUpdatedPerUnitCost[i], OPEN = LastUpdatedCost[i], HIGH = "", LOW = "", CHANGE = MarketPriceCurrent[i].Trim(), VOLUME = MarketValue[i].Trim(), APPRECIATION_DEPRECIATION = Appreciation_Depreciation[i].Trim() });
                                //    ////list1.Items.Add(new SHARE{ total + 1, Share_Name[i] });
                                //    //list1.View = myGridView;


                                //}
                                //else
                                //{
                                //    //var bc = new BrushConverter();
                                //    //Style style = new Style();
                                //    //style.TargetType = typeof(ListViewItem);
                                //    //style.Setters.Add(new Setter(ListViewItem.ForegroundProperty, Brushes.DarkGray));
                                //    //list1.SelectedItem = style;
                                //    //list1.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#000000");
                                //    //list1.Items.Add(new FundMarket { SERIAL = i + 1, NAME = Share_Name[i], SYMBOL = Share_Symbol[i], CURRENT = LastUpdatedHolding[i], LDCP = LastUpdatedPerUnitCost[i], OPEN = LastUpdatedCost[i], HIGH = "", LOW = "", CHANGE = MarketPriceCurrent[i].Trim(), VOLUME = MarketValue[i].Trim(), APPRECIATION_DEPRECIATION = Appreciation_Depreciation[i].Trim() });

                                //    //list1.View = myGridView;
                                //}
                                decimal lappdepp;
                                Debug.WriteLine("Appreciation_Depreciation[i]: " + Appreciation_Depreciation[i]);
                                if (Appreciation_Depreciation[i].Contains("("))
                                {
                                    lappdepp = Convert.ToDecimal("-" + Appreciation_Depreciation[i].Replace("(", "").Replace(")", "").Replace(",", ""));
                                }
                                else
                                {
                                    lappdepp = Convert.ToDecimal(Appreciation_Depreciation[i].Replace("(", "").Replace(")", "").Replace(",", ""));
                                }
                                Debug.WriteLine("lappdepp: " + lappdepp + ", Type: "+ lappdepp.GetType());
                                Debug.WriteLine("LastUpdatedCost[i]: " + LastUpdatedCost[i] + ", Type: " + LastUpdatedCost[i].GetType());
                                decimal lbookcost = Convert.ToDecimal(LastUpdatedCost[i].Replace(",", ""));
                                Debug.WriteLine("lbookcost: " + lbookcost + ", Type: " + lbookcost.GetType());
                                decimal closing = lappdepp / lbookcost;
                                Debug.WriteLine("closing: " + closing + ", Type: " + closing.GetType());
                                if (closing >= Convert.ToDecimal(CLOSING_PERCENTAGE))
                                {
                                    FundPopUpWindowFlag = true;
                                    try
                                    {
                                        int flagBucket = SavingToFundBacket(Convert.ToDateTime(DefaultData[0]), DefaultData[1].ToUpper(), false, getFundId(FundName), FundName, Share_Name[i], Share_Symbol[i], Convert.ToDecimal(LastUpdatedHolding[i]), Convert.ToDecimal(LastUpdatedPerUnitCost[i]), lbookcost, Convert.ToDecimal(MarketPriceCurrent[i]), Convert.ToDecimal(MarketValue[i]), Convert.ToDecimal(lappdepp), Convert.ToDecimal(closing));
                                    }
                                    catch(Exception ex)
                                    {
                                        Debug.WriteLine("Exception: " + ex.Message);
                                    }
                                    //Debug.WriteLine("Insert Fund Bucker Record Count: " + flagBucket);
                                    //if (flagBucket != 0)
                                    //{

                                    //FundPopupWindow popupWindow = new FundPopupWindow(Convert.ToDateTime(DefaultData[0]), DefaultData[1].ToUpper(), false, FundName, Share_Name[i], Share_Symbol[i], Convert.ToDecimal(LastUpdatedHolding[i]), Convert.ToDecimal(LastUpdatedPerUnitCost[i]), lbookcost, Convert.ToDecimal(MarketPriceCurrent[i]), Convert.ToDecimal(MarketValue[i]), lappdepp, closing);
                                    //popupWindow.Show();

                                    // Debug.WriteLine("Insert Fund Bucker Record Count: " + flagBucket);
                                    //MessageBox.Show("INSERTED!");
                                    //}
                                    //else
                                    //{
                                    //Debug.WriteLine("Insert Fund Bucker Record Count: " + flagBucket);
                                    //MessageBox.Show("NOT INSERTED!");
                                    //}
                                }
                                Debug.WriteLine("LastUpdatedPerUnitCost[i]: " + LastUpdatedPerUnitCost[i] + ", Type: " + LastUpdatedPerUnitCost[i].GetType());
                                decimal laverageprice = Convert.ToDecimal(LastUpdatedPerUnitCost[i]);
                                Debug.WriteLine("LastUpdatedPerUnitCost[i]: " + MarketPriceCurrent[i] + ", Type: " + MarketPriceCurrent[i].GetType());
                                decimal lmarketprice = Convert.ToDecimal(MarketPriceCurrent[i] == "" ? "0.00" : MarketPriceCurrent[i]);
                                list1.Items.Add(new FundMarket { SERIAL = i + 1, NAME = Share_Name[i], SYMBOL = Share_Symbol[i], CURRENT = LastUpdatedHolding[i], LDCP = laverageprice.ToString("N2"), OPEN = LastUpdatedCost[i], HIGH = "", LOW = "", CHANGE = lmarketprice.ToString("N2") == "0.00" ? "Not Listed" : lmarketprice.ToString("N2"), VOLUME = MarketValue[i] == "0" ? "Not Listed" : MarketValue[i], APPRECIATION_DEPRECIATION = Appreciation_Depreciation[i].Trim(), PERCENTAGE_CLOSING = MarketPriceCurrent[i] == "" ? "-" : String.Format("{0:N2}", Math.Round(closing, 2, MidpointRounding.AwayFromZero).ToString("N2") + "%") });
                                //list1.Items.Add(new FundMarket { SERIAL = Convert.ToInt32(spFundDetial[i].FundId), NAME = spFundDetial[i].Name, SYMBOL = spFundDetial[i].Symbol, CURRENT = spFundDetial[i].Quantity.ToString("#"), LDCP = spFundDetial[i].AveragePrice.ToString(), OPEN = spFundDetial[i].BookCost.ToString(), CHANGE = spFundDetial[i].MarketPrice, VOLUME = spFundDetial[i].MarketValue.ToString(), APPRECIATION_DEPRECIATION = spFundDetial[i].AppDep   } );
                                //list1.Items.Add(new SpecificFundDetail { FundId = spFundDetial[i].FundId, Name = spFundDetial[i].Name, Symbol = spFundDetial[i].Symbol, Quantity = spFundDetial[i].Quantity, AveragePrice = spFundDetial[i].AveragePrice, BookCost = spFundDetial[i].BookCost, MarketPrice = spFundDetial[i].MarketPrice, MarketValue = spFundDetial[i].MarketValue, AppDep = spFundDetial[i].AppDep });
                                // 
                            }
                        }
                    }
                    else
                    {

                    }

                    FundImage.Visibility = Visibility.Hidden;
                    list1.Visibility = Visibility.Visible;

                    loadingImage.Visibility = Visibility.Hidden;

                    int clearData = await Task.Run(() => ClearFundMarketSummary());
                    Debug.WriteLine("Fund Market Summary Data Cleared..: " + clearData);
                    if(clearData == -1)
                    {
                        int DataSaved = await Task.Run(() => SavingDataToDatabase(MARKET_STATUS, FUND_ID1, FUND_NAME1, Share_Name, Share_Symbol, QUANTITY, AVERAGE_PRICE, BOOK_COST, MARKET_PRICE, MARKET_VALUE, APPRECIATION_DEPRECIATION));
                        Debug.WriteLine("Fund Market Summary Data Inserted..: " + DataSaved);
                    }

                    txtBucketCount.Text = " (" + getCountFundBucket().ToString() + ") ";
                    btnBorderBucket.Visibility = Visibility.Visible;

                    //if (!DataSaved)
                    //    Debug.WriteLine("Unable to Save Data.");

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

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            countTimer++;
            if (countTimer % 25 == 0 && resetFlag == true)
            {
                ButtonAutomationPeer peer = new ButtonAutomationPeer(btnReset);
                IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                invokeProv.Invoke();
            }
        }
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            //myDelegate delegate1 = new myDelegate(mustWork);
            //mustWork();
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            imgStatus.Source = new BitmapImage(ResourceAccessor.Get("Images/exclaimation.png"));
            lblStatus.Text = "Status: " + e.ProgressPercentage.ToString() + "% Processed";
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            list1.Visibility = Visibility.Visible;
            loadingImage.Visibility = Visibility.Hidden;
            FundImage.Visibility = Visibility.Hidden;
            imgStatus.Source = new BitmapImage(ResourceAccessor.Get("Images/tick.png"));
            lblStatus.Text = "Status: Ready";

        }



        private void mustWork(string FundName)
        {
            int FUND_ID = 0;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connIpams = new SqlConnection();
            connIpams.ConnectionString = ConfigurationManager.ConnectionStrings["IpamsConnection"].ConnectionString;

            spFundDetial = new List<SpecificFundDetail>();

            try
            {

                conn.Open();
                // 1.  create a command object identifying the stored procedure
                SqlCommand cmd = new SqlCommand("spGetFundIDByParamNAME", conn);

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
                        FUND_ID = Convert.ToInt32(rdr["FUND_CODE"]);
                        //Debug.WriteLine("FUND_ID in Default DB: " + FUND_ID);
                    }
                }

                conn.Close();

                if (FUND_ID == 0)
                {

                    connIpams.Open();
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
                            FUND_ID = Convert.ToInt32(rdr2["FUND_CODE"]);
                            //Debug.WriteLine("FUND_ID in Ipams DB: " + FUND_ID);
                        }
                    }

                    connIpams.Close();

                }

                //Store
                FUND_ID1 = FUND_ID;
                FUND_NAME1 = FundName;

                connIpams.Open();

                // 1.  create a command object identifying the stored procedure
                SqlCommand cmdforFetchingShare = new SqlCommand("spGetShareDetailByParamFundIdAndDate", connIpams);

                // 2. set the command object so it knows to execute a stored procedure
                cmdforFetchingShare.CommandType = CommandType.StoredProcedure;

                // 3. add parameter to command, which will be passed to the stored procedure
                cmdforFetchingShare.Parameters.Add(new SqlParameter("@FUND_ID", FUND_ID));
                //cmdforFetchingShare.Parameters.Add(new SqlParameter("@DATE", DateTime.Now.AddYears(-1)));
                cmdforFetchingShare.Parameters.Add(new SqlParameter("@DATE", DateTime.Now));
                Share_Name = new List<String>();
                Share_Symbol = new List<String>();
                DateCostLastUpdated = new List<String>();
                LastUpdatedPerUnitCost = new List<String>();
                LastUpdatedCost = new List<String>();
                LastUpdatedHolding = new List<String>();
                LastUpdatedMarketPriceDate = new List<String>();
                MarketSymbol = new List<String>();
                MarketPriceCurrent = new List<String>();
                MarketValue = new List<String>();
                Appreciation_Depreciation = new List<String>();
                testShare = getShareDetail();
                // execute the command
                QUANTITY = new List<Decimal>();
                AVERAGE_PRICE = new List<Decimal>();
                BOOK_COST = new List<Decimal>();
                MARKET_PRICE = new List<Decimal>();
                MARKET_VALUE = new List<Decimal>();
                APPRECIATION_DEPRECIATION = new List<String>();
                //
                int total = 0;
                using (SqlDataReader rdr = cmdforFetchingShare.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        // Qunatity is not Null
                        if (!rdr.IsDBNull(5))
                        {
                            //Debug.WriteLine(":Name: " + rdr.GetString(0).ToString() + ", Quantity: "+ rdr.GetDecimal(5));
                            // Quantity is not 0
                            if (rdr.GetDecimal(5) != 0)
                            {
                                //Debug.WriteLine(":Name: " + rdr.GetString(0).ToString() + ", Quantity: " + rdr.GetDecimal(5));
                                Debug.WriteLine(":Share Name: " + ", Type: " + Share_Name.GetType() + " - :Name: " + rdr.GetString(0) + ", Type: " + rdr.GetString(0).GetType());
                                Share_Name.Add(rdr.GetString(0).ToString());
                                Debug.WriteLine(":Share Symbol: " + ", Type: " + Share_Symbol.GetType() + " - :Symbol: " + rdr.GetString(1) + ", Type: " + rdr.GetString(1).GetType());
                                Share_Symbol.Add(rdr.GetString(1).ToString());
                                //Debug.WriteLine(":Share Name: " + Share_Name + ", Type: " + Share_Name.GetType() + " - :Name: " + rdr.GetString(0) + ", Type: " + rdr.GetString(0).GetType());
                                DateCostLastUpdated.Add(rdr.GetDateTime(2).ToString());
                                Debug.WriteLine(":AVERAGE_PRICE: " + ", Type: " + AVERAGE_PRICE.GetType() + " - :AVERAGE_PRICE: " + rdr.GetDecimal(3) + ", Type: " + rdr.GetDecimal(3).GetType());
                                AVERAGE_PRICE.Add(rdr.GetDecimal(3).ToString() == null ? 0 : rdr.GetDecimal(3));
                                //AVERAGE_PRICE.Add(33.89M);
                                
                                LastUpdatedPerUnitCost.Add(rdr.GetDecimal(3).ToString("#.##"));
                                Debug.WriteLine(":BOOK_COST: " + ", Type: " + BOOK_COST.GetType() + " - :BOOK_COST: " + rdr.GetDecimal(4) + ", Type: " + rdr.GetDecimal(4).GetType());
                                BOOK_COST.Add(rdr.GetDecimal(4));
                                LastUpdatedCost.Add(Math.Round(rdr.GetDecimal(4)).ToString("#,##0"));
                                double holding = Convert.ToDouble(rdr.GetDecimal(5));
                                Debug.WriteLine(":QUANTITY: " + ", Type: " + QUANTITY.GetType() + " - :QUANTITY: " + rdr.GetDecimal(5) + ", Type: " + rdr.GetDecimal(5).GetType());
                                QUANTITY.Add(rdr.GetDecimal(5));
                                LastUpdatedHolding.Add(holding.ToString("#,##0"));
                                LastUpdatedMarketPriceDate.Add(rdr.GetDateTime(6).ToString("#.##"));
                                string localSymbol = String.Empty;
                                string localCurrent = String.Empty;
                                decimal localValue = 0;
                                if (getSymbolStatus(rdr.GetString(1).ToString()))
                                {
                                    for (int i = 0; i < testShare.Count; i++)
                                    {
                                        if (rdr.GetString(1).ToString().Equals(testShare[i].SHARE_SYMBOL.ToString()))
                                        {
                                            localSymbol = testShare[i].SHARE_SYMBOL.ToString();
                                            Debug.WriteLine(":testShare[i].SHARE_CURRENT: " + ", Type: " + testShare[i].SHARE_CURRENT.GetType());
                                            localCurrent = String.Format("{0:d}",testShare[i].SHARE_CURRENT);
                                            Debug.WriteLine(":MARKET_PRICE: " + ", Type: " + MARKET_PRICE.GetType());
                                            MARKET_PRICE.Add(Decimal.Parse(testShare[i].SHARE_CURRENT));
                                            Debug.WriteLine(":testShare[i].SHARE_CURRENT: " + ", Type: " + testShare[i].SHARE_CURRENT.GetType());
                                            localValue = Convert.ToDecimal(testShare[i].SHARE_CURRENT) * rdr.GetDecimal(5);
                                            Debug.WriteLine(":MARKET_VALUE: " + ", Type: " + MARKET_VALUE.GetType());
                                            MARKET_VALUE.Add(localValue);

                                        }
                                    }
                                }
                                Debug.WriteLine(":MarketSymbol: " + ", Type: " + MarketSymbol.GetType());
                                MarketSymbol.Add(localSymbol);
                                Debug.WriteLine(":MarketPriceCurrent: " + ", Type: " + MarketPriceCurrent.GetType() + ", localCurrent: " + localCurrent + ", Type: " + localCurrent.GetType());
                                MarketPriceCurrent.Add(localCurrent);
                                Debug.WriteLine(":MarketValue: " + ", Type: " + MarketValue.GetType() + ", localCurrent: " + localValue + ", Type: " + localValue.GetType());
                                MarketValue.Add(Convert.ToInt32(Math.Round(localValue)).ToString("#,##0"));
                                decimal appreciation = localValue - rdr.GetDecimal(4);
                                APPRECIATION_DEPRECIATION.Add(appreciation.ToString());
                                string localAppreciate = Math.Round(appreciation, 2).ToString("#,##0");
                                if (appreciation < 0)
                                {
                                    localAppreciate = "(" + localAppreciate.Replace("-", "") + ")";
                                }
                                Debug.WriteLine(":Appreciation_Depreciation: " + ", Type: " + Appreciation_Depreciation.GetType() + ", localCurrent: " + localAppreciate + ", Type: " + localAppreciate.GetType());

                                Appreciation_Depreciation.Add(localAppreciate.ToString());

                                //New Edition 11/17/2020
                                //By Saad
                                total++;
                                spFundDetial.Add( new SpecificFundDetail {
                                        FundId = total,
                                        Name = rdr.GetString(0),
                                        Symbol = rdr.GetString(1),
                                        Quantity = Decimal.Round(rdr.GetDecimal(5), 0),
                                        AveragePrice = Decimal.Round(rdr.GetDecimal(3), 2),
                                        BookCost = Decimal.Round(rdr.GetDecimal(4), 2),
                                        MarketPrice = localCurrent == "" ? "0" : localCurrent,
                                        MarketValue = Decimal.Round(localValue, 2),
                                        AppDep = String.Format("{0:N0}", localAppreciate)  
                                    }
                                );

                            }
                        }
                        else { }
                    }
                }

                connIpams.Close();

                //foreach (SpecificFundDetail detail in spFundDetial)
                //{
                //    Debug.WriteLine("Name: " + detail.Name);
                //}


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



        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            comboFund.SelectedIndex = 0;

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

        private void comboFund_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Hide();
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run( () => RunExcel() );
        }



        private async void btnReset_Click(object sender, RoutedEventArgs e)
        {
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = ResourceAccessor.Get("Images/exclaimation.png");
            image.EndInit();
            ImageBehavior.SetAnimatedSource(imgStatus, image);

            lblStatus.Text = "Status: Processing";

            string FundName = comboFund.Text;
            if (list1.Visibility == Visibility.Visible && resetFlag == true)
            {

                await Task.Run(() => mustWork(FundName));

                FundMarket[] data = new FundMarket[Share_Name.Count];

                col1.DisplayMemberBinding = new Binding("SERIAL");
                //col1.DisplayMemberBinding = new Binding("FundId");
                col2.DisplayMemberBinding = new Binding("NAME");
                //col2.DisplayMemberBinding = new Binding("Name");
                col3.DisplayMemberBinding = new Binding("SYMBOL");
                //col3.DisplayMemberBinding = new Binding("Symbol");
                //col4.DisplayMemberBinding = new Binding("CURRENT");
                //col5.DisplayMemberBinding = new Binding("LDCP");
                //col6.DisplayMemberBinding = new Binding("OPEN");
                //col7.DisplayMemberBinding = new Binding("CHANGE");
                //col8.DisplayMemberBinding = new Binding("VOLUME");
                //col9.DisplayMemberBinding = new Binding("APPRECIATION_DEPRECIATION");

                //gvc1.DisplayMemberBinding = new Binding("SERIAL");
                //gvc2.DisplayMemberBinding = new Binding("NAME");
                //gvc3.DisplayMemberBinding = new Binding("SYMBOL");
                //gvc4.DisplayMemberBinding = new Binding("CURRENT");
                //gvc5.DisplayMemberBinding = new Binding("LDCP");
                //gvc6.DisplayMemberBinding = new Binding("OPEN");
                //gvc7.DisplayMemberBinding = new Binding("CHANGE");
                //gvc8.DisplayMemberBinding = new Binding("VOLUME");
                //gvc9.DisplayMemberBinding = new Binding("APPRECIATION_DEPRECIATION");

                list1.Items.Clear();

                for (int i = 0; i < Share_Name.Count; i++)
                {
                    if (Share_Name[i] == null) { }
                    else
                    {
                        decimal lappdepp;
                        if (Appreciation_Depreciation[i].Contains("("))
                        {
                            lappdepp = Convert.ToDecimal("-" + Appreciation_Depreciation[i].Replace("(", "").Replace(")", "").Replace(",", ""));
                        }
                        else
                        {
                            lappdepp = Convert.ToDecimal(Appreciation_Depreciation[i].Replace("(", "").Replace(")", "").Replace(",", ""));
                        }
                        decimal lbookcost = Convert.ToDecimal(LastUpdatedCost[i].Replace(",", ""));
                        decimal closing = lappdepp / lbookcost;
                        if (closing >= CLOSING_PERCENTAGE)
                        {
                            FundPopUpWindowFlag = true;
                            string getSymbol = getFundSymbolExist(Share_Symbol[i]);
                            Debug.WriteLine("SYMBOL GOT: " + getSymbol);
                            if (getSymbol != "Nil")
                            {
                                Debug.WriteLine("Date: " + DefaultData[0] + " - " + DefaultData[0].GetType());
                                Debug.WriteLine("Status: " + DefaultData[1] + " - " + DefaultData[1].GetType());
                                Debug.WriteLine("Fund Id: " + getFundId(FundName) + " - " + getFundId(FundName).GetType());
                                Debug.WriteLine("Fund Name: " + FundName + " - " + FundName.GetType());
                                Debug.WriteLine("Share Name: " + Share_Name[i] + " - " + Share_Name[i].GetType());
                                Debug.WriteLine("Share Symbol: " + Share_Symbol[i] + " - " + Share_Symbol[i].GetType());
                                Debug.WriteLine("Quantity: " + LastUpdatedHolding[i] + " - " + LastUpdatedHolding[i].GetType());
                                Debug.WriteLine("Average Price: " + LastUpdatedPerUnitCost[i] + " - " + LastUpdatedPerUnitCost[i].GetType());
                                Debug.WriteLine("Book Cost: " + lbookcost + " - " + lbookcost.GetType());
                                Debug.WriteLine("Market Price: " + MarketPriceCurrent[i] + " - " + MarketPriceCurrent[i].GetType());
                                Debug.WriteLine("Market Value: " + MarketValue[i] + " - " + MarketValue[i].GetType());
                                Debug.WriteLine("App Dep: " + lappdepp + " - " + lappdepp.GetType());
                                Debug.WriteLine("Closing Percentage: " + closing + " - " + closing.GetType());
                                int flagBucket = UpdateToFundBacket(Convert.ToDateTime(DefaultData[0]), DefaultData[1].ToUpper(), Convert.ToInt64(getFundId(FundName)), FundName, Share_Name[i], Share_Symbol[i], Convert.ToDecimal(LastUpdatedHolding[i].Replace(",","")), Convert.ToDecimal(LastUpdatedPerUnitCost[i]), lbookcost, Convert.ToDecimal(MarketPriceCurrent[i] == "" ? "0" : MarketPriceCurrent[i]), Convert.ToDecimal(MarketValue[i]), Convert.ToDecimal(lappdepp), Convert.ToDecimal(closing));
                            }
                            else
                            {
                                Debug.WriteLine("Date: " + DefaultData[0] + " - " + DefaultData[0].GetType());
                                Debug.WriteLine("Status: " + DefaultData[1] + " - " + DefaultData[1].GetType());
                                Debug.WriteLine("Fund Id: " + getFundId(FundName) + " - " + getFundId(FundName).GetType());
                                Debug.WriteLine("Fund Name: " + FundName + " - " + FundName.GetType());
                                Debug.WriteLine("Share Name: " + Share_Name[i] + " - " + Share_Name[i].GetType());
                                Debug.WriteLine("Share Symbol: " + Share_Symbol[i] + " - " + Share_Symbol[i].GetType());
                                Debug.WriteLine("Quantity: " + LastUpdatedHolding[i] + " - " + LastUpdatedHolding[i].GetType());
                                Debug.WriteLine("Average Price: " + LastUpdatedPerUnitCost[i] + " - " + LastUpdatedPerUnitCost[i].GetType());
                                Debug.WriteLine("Book Cost: " + lbookcost + " - " + lbookcost.GetType());
                                Debug.WriteLine("Market Price: " + MarketPriceCurrent[i] + " - " + MarketPriceCurrent[i].GetType());
                                Debug.WriteLine("Market Value: " + MarketValue[i] + " - " + MarketValue[i].GetType());
                                Debug.WriteLine("App Dep: " + lappdepp + " - " + lappdepp.GetType());
                                Debug.WriteLine("Closing Percentage: " + closing + " - " + closing.GetType());
                                int flagBucket = SavingToFundBacket(Convert.ToDateTime(DefaultData[0]), DefaultData[1].ToUpper(), false, Convert.ToInt64(getFundId(FundName)), FundName, Share_Name[i], Share_Symbol[i], Convert.ToDecimal(LastUpdatedHolding[i].Replace(",", "")), Convert.ToDecimal(LastUpdatedPerUnitCost[i]), lbookcost, Convert.ToDecimal(MarketPriceCurrent[i] == "" ? "0" : MarketPriceCurrent[i]), Convert.ToDecimal(MarketValue[i]), Convert.ToDecimal(lappdepp), Convert.ToDecimal(closing));
                            }
                            
                        }
                        decimal laverageprice = Convert.ToDecimal(LastUpdatedPerUnitCost[i]);
                        decimal lmarketprice = Convert.ToDecimal(MarketPriceCurrent[i] == "" ? "0.00" : MarketPriceCurrent[i]);
                        list1.Items.Add(new FundMarket { SERIAL = i + 1, NAME = Share_Name[i], SYMBOL = Share_Symbol[i], CURRENT = LastUpdatedHolding[i], LDCP = laverageprice.ToString("N2"), OPEN = LastUpdatedCost[i], HIGH = "", LOW = "", CHANGE = lmarketprice.ToString("N2") == "0.00" ? "Not Listed" : lmarketprice.ToString("N2"), VOLUME = MarketValue[i] == "0" ? "Not Listed" : MarketValue[i], APPRECIATION_DEPRECIATION = Appreciation_Depreciation[i].Trim(), PERCENTAGE_CLOSING = MarketPriceCurrent[i] == "" ? "-" : String.Format("{0:N2}", Math.Round(closing, 2, MidpointRounding.AwayFromZero).ToString("N2") + "%") });
                        
                        //list1.Items.Add(new SpecificFundDetail { FundId = spFundDetial[i].FundId, Name = spFundDetial[i].Name, Symbol = spFundDetial[i].Symbol, Quantity = spFundDetial[i].Quantity, AveragePrice = spFundDetial[i].AveragePrice, BookCost = spFundDetial[i].BookCost, MarketPrice = spFundDetial[i].MarketPrice, MarketValue = spFundDetial[i].MarketValue, AppDep = spFundDetial[i].AppDep, Math.Round(closing, 2).ToString("0.##") + "%" });
                    }
                }


                FundImage.Visibility = Visibility.Hidden;
                list1.Visibility = Visibility.Visible;
                loadingImage.Visibility = Visibility.Hidden;

                await Task.Run(() => getDefault());
                DateTime MarketDate = DateTime.Parse(DefaultData[0]);
                txtDate.Text = "Date: " + MarketDate.ToString("dddd, dd MMMM yyyy hh:mm tt");
                txtStatus.Text = "Status: " + DefaultData[1];

            }

            else
            {
                MessageBox.Show("Run the Fund Selection First.", "No Data Found.", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            txtBucketCount.Text = " (" + getCountFundBucket().ToString() + ") ";

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

        private int SavingDataToDatabase(string MARKET_STATUS, Int64 FUND_ID, string FUND_NAME, List<String> SHARE_NAME, List<String> SHARE_SYMBOL, List<Decimal> QUANTITY, List<Decimal> AVERAGE_PRICE, List<Decimal> BOOK_COST, List<Decimal> MARKET_PRICE, List<Decimal> MARKET_VALUE, List<String> APP_DEP)
        {

            if (MARKET_STATUS != null)
            {

                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                conn.Open();
                int status = 0;
                try
                {
                    for (int i = 0; i < SHARE_NAME.Count; i++)
                    {


                        //
                        //if (MARKET_PRICE[i] == null)
                        //{

                        //}
                        //else
                        //{

                        SqlCommand cmd = new SqlCommand("spINSERT_FUND_MARKET_SUMMARY", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@MARKET_STATUS", SqlDbType.VarChar, 500);
                        cmd.Parameters["@MARKET_STATUS"].Value = MARKET_STATUS;
                        cmd.Parameters.Add("@FUND_ID", SqlDbType.BigInt);
                        cmd.Parameters["@FUND_ID"].Value = FUND_ID;
                        cmd.Parameters.Add("@FUND_NAME", SqlDbType.VarChar, -1);
                        cmd.Parameters["@FUND_NAME"].Value = FUND_NAME;
                        cmd.Parameters.Add("@SHR_NAME", SqlDbType.VarChar, -1);
                        cmd.Parameters["@SHR_NAME"].Value = SHARE_NAME[i];
                        cmd.Parameters.Add("@SHR_SYMBOL", SqlDbType.VarChar, 500);
                        cmd.Parameters["@SHR_SYMBOL"].Value = SHARE_SYMBOL[i];
                        cmd.Parameters.Add("@SHR_QUANTITY", SqlDbType.Decimal);
                        cmd.Parameters["@SHR_QUANTITY"].Value = QUANTITY[i];
                        cmd.Parameters.Add("@SHR_AVG_PRICE", SqlDbType.Decimal);
                        cmd.Parameters["@SHR_AVG_PRICE"].Value = AVERAGE_PRICE[i];
                        cmd.Parameters.Add("@SHR_BOOK_COST", SqlDbType.Decimal);
                        cmd.Parameters["@SHR_BOOK_COST"].Value = BOOK_COST[i];
                        cmd.Parameters.Add("@SHR_MARKET_PRICE", SqlDbType.Decimal);
                        cmd.Parameters["@SHR_MARKET_PRICE"].Value = MARKET_PRICE[i];
                        cmd.Parameters.Add("@SHR_MARKET_VALUE", SqlDbType.Decimal);
                        cmd.Parameters["@SHR_MARKET_VALUE"].Value = MARKET_VALUE[i];
                        cmd.Parameters.Add("@SHR_APP_DEP", SqlDbType.VarChar);
                        cmd.Parameters["@SHR_APP_DEP"].Value = APP_DEP[i];

                        status = cmd.ExecuteNonQuery();
                        //if (status == 1)
                        //{
                        //    Debug.WriteLine("Passed!");
                        //}

                        //}
                    }
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



                //

                //if (status == 1)
                //{
                //    Debug.WriteLine("Data successfully saved.");
                //    return true;
                //}
                finally
                {
                    conn.Close();
                }



            }
            else
            {
                return 0;
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

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
                    List<SpecificFundDetail> items = getFundMarketSummary();

                    FundMarket[] fund = new FundMarket[items.Count];
                    for (int i = 0; i < items.Count; i++)
                    {
                        int count = i;
                        fund[i] = new FundMarket();
                        fund[i].SERIAL = Convert.ToInt32(items[i].FundId);
                        fund[i].NAME = items[i].Name;
                        fund[i].SYMBOL = items[i].Symbol;
                        fund[i].CURRENT = items[i].Quantity.ToString("N0");
                        decimal laverageprice = Convert.ToDecimal(items[i].AveragePrice);
                        fund[i].LDCP = laverageprice.ToString("N2");
                        fund[i].OPEN = items[i].BookCost.ToString("N0");
                        decimal lmarketprice = Convert.ToDecimal(items[i].MarketPrice);
                        fund[i].CHANGE = lmarketprice.ToString("N2");
                        fund[i].VOLUME = items[i].MarketValue.ToString("N0");
                        decimal lappdep = Math.Round(Convert.ToDecimal(items[i].AppDep), MidpointRounding.AwayFromZero);
                        fund[i].APPRECIATION_DEPRECIATION = lappdep.ToString("N0");
                    }

                    int total = 1;
                    for (int index = 0; index < Share_Name.Count; index++)
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

        private int SavingToFundBacket(DateTime _date, string _status, bool _readingStatus, Int64 _fundId, string _fundName, string _shareName, string _shareSymbol, decimal _quantity, decimal _averagePrice, decimal _bookCost, decimal _marketPrice, decimal _marketValue, decimal _appDep, decimal _percentageClosing)
        {

            if (_shareName != null)
            {

                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                conn.Open();
                int status = 0;
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
                    //if (status == 1)
                    //{
                    //    Debug.WriteLine("Passed!");
                    //}
                    //}
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
                return 0;
            }
        }

        public int UpdateToFundBacket(DateTime _date, string _status, Int64 _fundId, string _fundName, string _shareName, string _shareSymbol, decimal _quantity, decimal _averagePrice, decimal _bookCost, decimal _marketPrice, decimal _marketValue, decimal _appDep, decimal _percentageClosing)
        {
            if (_shareName != null)
            {

                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                conn.Open();
                int status = 0;
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
                    //if (status == 1)
                    //{
                    //    Debug.WriteLine("Passed!");
                    //}
                    //}
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
                return 0;
            }
        }

        private int ClearFundBacket()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            conn.Open();
            int status = 0;
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

        private int ClearFundMarketSummary()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            conn.Open();
            int status = 0;
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

        ///Get Fund ID By Fund Name
        ///
        public int getFundId(string _fundName)
        {
            int _id = 0;

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

            return _id;
        }

        public string getFundSymbolExist(string _symbol)
        {
            string _id = String.Empty;

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

            return _id;
        }

        public List<Int64> getFundScripBucketId()
        {
            List<Int64> bucket = new List<Int64>();
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
            return bucket;
        }

        public bool getFundScripBucketReadingStatus(Int64 id)
        {
            bool status = false;
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
            return status;
        }

        public int getCountFundBucket()
        {
            int _id = 0;

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

            return _id;
        }


        private void btnViewBucket_Click(object sender, RoutedEventArgs e)
        {
            FundBucket window = new FundBucket();
            window.Show();
            //this.Hide();
        }

        public List<SpecificFundDetail> getFundMarketSummary()
        {
            List<SpecificFundDetail> items = new List<SpecificFundDetail>();

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
                    items.Add( new SpecificFundDetail { FundId = rdr.GetInt64(0), Name = rdr.GetString(5), Symbol = rdr.GetString(6), Quantity = rdr.GetDecimal(7), AveragePrice = rdr.GetDecimal(8), BookCost = rdr.GetDecimal(9), MarketPrice = rdr.GetDecimal(10).ToString("N2"), MarketValue = rdr.GetDecimal(11), AppDep = rdr.GetString(12) } );
                    //Debug.WriteLine("FUND_ID in Default DB: " + FUND_ID);
                }
            }

            conn.Close();

            return items;
        }

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

    }
    
}
