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
using static PSXDataFetchingApp.PreviewWindow;

namespace PSXDataFetchingApp
{
    /// <summary>
    /// Interaction logic for FundPreviewWindow.xaml
    /// </summary>
    public partial class FundPreviewWindow : Window
    {
        public Configuration configuration;
        public MainWindow mainClass = new MainWindow();
        BackgroundWorker worker = new BackgroundWorker();

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
        SHARE[] testShare;

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

        public FundPreviewWindow()
        {
            InitializeComponent();

            txtDate.Text = "Date:" + DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm tt");

            SqlConnection conn = new SqlConnection();
            SqlConnection conn2 = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["IpamsConnection"].ConnectionString;
            conn2.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            List<String> fundName;
            List<String> fundName2;
            try
            {
                fundName = HasRows(conn2, 2);
                fundName2 = HasRows(conn, 1);

                comboFund.Items.Add("Select..");
                comboFund.SelectedIndex = 0;
                for (int i = 0; i < fundName.Count; i++)
                {
                    comboFund.Items.Add(fundName[i]);
                }
                for (int i = 0; i < fundName2.Count; i++)
                {
                    comboFund.Items.Add(fundName2[i]);
                }
                comboFund.Items.Add("<ADD NEW FUND>");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database connectivity failed with the exception.:\nException: " + ex.Message, "Database Connectivity Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                conn.Close();
                conn2.Close();
            }
        }

        static List<String> HasRows(SqlConnection connection, int flag)
        {
            List<String> result;
            using (connection)
            {
                SqlCommand command;
                if (flag == 1)
                {
                    command = new SqlCommand(
                      "SELECT [fund_code], [fund_name] FROM [ipams_db].[dbo].[pms_funds] order by fund_code",
                      connection);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    result = new List<String>(reader.FieldCount);
                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            result.Add(reader.GetString(1));
                        }
                    }
                    else
                    {
                        Debug.WriteLine("No rows found.");
                    }
                    reader.Close();
                }
                else
                {
                    command = new SqlCommand(
                      "SELECT [FUND_ID],[FUND_CODE],[FUND_NAME],[FUND_SYMBOL],[FUND_DESC] FROM [dbo].[FUND] order by [FUND_ID]",
                      connection);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    result = new List<String>(reader.FieldCount);
                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            result.Add(reader.GetString(2));
                            //Debug.WriteLine("{0}\t{1}", reader.GetInt32(0),
                            //    reader.GetString(1));

                        }
                    }
                    else
                    {
                        Debug.WriteLine("No rows found.");
                    }
                    reader.Close();
                }

                return result;

            }

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

        //public bool getSymbolStatus1(string Symbol)
        //{
        //    bool result = false;
        //    SqlConnection conn = new SqlConnection();
        //    try
        //    {
        //        conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        //        using (conn)
        //        {
        //            conn.Open();

        //            SqlCommand cmd = new SqlCommand("spIsGetCompanySymbolExist", conn); // Read user-> stored procedure name
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.Add("@SYMBOL", SqlDbType.VarChar, 500);
        //            cmd.Parameters["@SYMBOL"].Value = Symbol;
        //            using (SqlDataReader rdr = cmd.ExecuteReader())
        //            {
        //                while (rdr.Read())
        //                {
        //                    result = Convert.ToBoolean(rdr.GetInt64(0));
        //                }
        //            }

        //            conn.Close();
        //        }

        //    }
        //    catch (SqlException ex)
        //    {
        //        Debug.WriteLine("SQL Exception: " + ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine("Exception: " + ex.Message);
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //    return result;
        //}

        public void getDefault()
        {
            DefaultData = mainClass.GetDefault();
        }


        public SHARE[] getShareDetail()
        {
            List<string> NAME = mainClass.GetMarketSummaryCompanyNames();
            List<string> SYMBOL = mainClass.GetMarketSummaryCompanySymbols(NAME);
            string[] CURRENT = mainClass.GetMarketSummaryCompanyCURRENT();

            SHARE[] share = new SHARE[NAME.Count];
            for (int i = 0; i < NAME.Count; i++)
            {
                share[i] = new SHARE { SHARE_NAME = NAME[i], SHARE_SYMBOL = SYMBOL[i], SHARE_CURRENT = CURRENT[i] };
                //Debug.WriteLine("FNAME: " + NAME[i] + ", FSYMBOL: " + SYMBOL[i] + ", FCURRENT: " + CURRENT[i]);
            }
            return share;
        }

        private async void btnGet_Click(object sender, RoutedEventArgs e)
        {

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

                

                FundMarket[] data = new FundMarket[Share_Name.Count];

                col1.DisplayMemberBinding = new Binding("SERIAL");
                col2.DisplayMemberBinding = new Binding("NAME");
                col3.DisplayMemberBinding = new Binding("SYMBOL");
                col4.DisplayMemberBinding = new Binding("CURRENT");
                col5.DisplayMemberBinding = new Binding("LDCP");
                col6.DisplayMemberBinding = new Binding("OPEN");
                col7.DisplayMemberBinding = new Binding("CHANGE");
                col8.DisplayMemberBinding = new Binding("VOLUME");
                col9.DisplayMemberBinding = new Binding("APPRECIATION_DEPRECIATION");
                

                list1.Items.Clear();

                FundImage.Visibility = Visibility.Hidden;
                loadingImage.Visibility = Visibility.Hidden;
                list1.Visibility = Visibility.Visible;
                
                for (int i = 0; i < Share_Name.Count; i++)
                {
                    if (Share_Name[i] == null) { }
                    else
                    {
                        if (Appreciation_Depreciation[i].StartsWith('('))
                        {
                            //GridView myGridView = new GridView();
                            //myGridView.AllowsColumnReorder = true;
                            //myGridView.ColumnHeaderToolTip = "Fund Market Information";

                            //GridViewColumn gvc1 = new GridViewColumn();
                            //gvc1.DisplayMemberBinding = new Binding("SERIAL");
                            //gvc1.Header = "Sr. No.";
                            //gvc1.Width = 50;
                            //myGridView.Columns.Add(gvc1);
                            //GridViewColumn gvc2 = new GridViewColumn();
                            //gvc2.DisplayMemberBinding = new Binding("NAME");
                            //gvc2.Header = "NAME";
                            //gvc2.Width = 250;
                            //myGridView.Columns.Add(gvc2);
                            //GridViewColumn gvc3 = new GridViewColumn();
                            //gvc3.DisplayMemberBinding = new Binding("SYMBOL");
                            //gvc3.Header = "SYMBOL";
                            //gvc3.Width = 60;
                            //myGridView.Columns.Add(gvc3);
                            //GridViewColumn gvc4 = new GridViewColumn();
                            //gvc4.DisplayMemberBinding = new Binding("CURRENT");
                            //gvc4.Header = "Quantity";
                            //gvc4.Width = 70;
                            //myGridView.Columns.Add(gvc4);
                            //GridViewColumn gvc5 = new GridViewColumn();
                            //gvc5.DisplayMemberBinding = new Binding("LDCP");
                            //gvc5.Header = "Avg. Price";
                            //gvc5.Width = 80;
                            //myGridView.Columns.Add(gvc5);
                            //GridViewColumn gvc6 = new GridViewColumn();
                            //gvc6.DisplayMemberBinding = new Binding("OPEN");
                            //gvc6.Header = "Book Cost";
                            //gvc6.Width = 80;
                            //myGridView.Columns.Add(gvc6);
                            //GridViewColumn gvc7 = new GridViewColumn();
                            //gvc7.DisplayMemberBinding = new Binding("CHANGE");
                            //gvc7.Header = "Market Price";
                            //gvc7.Width = 85;
                            //myGridView.Columns.Add(gvc7);
                            //GridViewColumn gvc8 = new GridViewColumn();
                            //gvc8.DisplayMemberBinding = new Binding("VOLUME");
                            //gvc8.Header = "Market Value";
                            //gvc8.Width = 100;
                            //myGridView.Columns.Add(gvc8);
                            //GridViewColumn gvc9 = new GridViewColumn();
                            //gvc9.DisplayMemberBinding = new Binding("APPRECIATION_DEPRECIATION");
                            //gvc9.Header = "App. / Dep.";
                            //gvc9.Width = 80;
                            //myGridView.Columns.Add(gvc9);

                            ////ItemsSource is ObservableCollection of EmployeeInfo objects
                            //myListView.ItemsSource = new myEmployees();
                            //myListView.View = myGridView;

                        }
                        else
                        {
                            //Style style = new Style();
                            //style.TargetType = typeof(ListViewItem);
                            //style.Setters.Add(new Setter(ListViewItem.ForegroundProperty, Brushes.Black));
                            //list1.SelectedItem = style;
                        }
                        list1.Items.Add(new FundMarket { SERIAL = i + 1, NAME = Share_Name[i], SYMBOL = Share_Symbol[i], CURRENT = LastUpdatedHolding[i], LDCP = LastUpdatedPerUnitCost[i], OPEN = LastUpdatedCost[i], HIGH = "", LOW = "", CHANGE = MarketPriceCurrent[i].Trim(), VOLUME = MarketValue[i].Trim(), APPRECIATION_DEPRECIATION = Appreciation_Depreciation[i].Trim() });
                    }
                }



                FundImage.Visibility = Visibility.Hidden;
                list1.Visibility = Visibility.Visible;

                loadingImage.Visibility = Visibility.Hidden;

                


            }

            bool DataSaved = await Task.Run(()=> SavingDataToDatabase(MARKET_STATUS, FUND_ID1, FUND_NAME1, Share_Name, Share_Symbol, QUANTITY, AVERAGE_PRICE, BOOK_COST, MARKET_PRICE, MARKET_VALUE, APPRECIATION_DEPRECIATION));

            if (!DataSaved)
                Debug.WriteLine("Unable to Save Data.");

            var image2 = new BitmapImage();
            image2.BeginInit();
            image2.UriSource = ResourceAccessor.Get("Images/tick.gif");
            image2.EndInit();
            ImageBehavior.SetAnimatedSource(imgStatus, image2);

            lblStatus.Text = "Status: Ready";

            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            countTimer++;
            if(countTimer % 25 == 0)
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
                    }
                }

                conn.Close();

                if (FUND_ID == 0)
                {

                    conn.Open();
                    // 1.  create a command object identifying the stored procedure
                    SqlCommand getFundIdByNameCmd = new SqlCommand("spGetFundIDByParamNAME", connIpams);

                    // 2. set the command object so it knows to execute a stored procedure
                    getFundIdByNameCmd.CommandType = CommandType.StoredProcedure;

                    // 3. add parameter to command, which will be passed to the stored procedure
                    getFundIdByNameCmd.Parameters.Add(new SqlParameter("@FUND_NAME", FundName));

                    // execute the command
                    using (SqlDataReader rdr2 = cmd.ExecuteReader())
                    {
                        // iterate through results, printing each to console
                        while (rdr2.Read())
                        {
                            FUND_ID = Convert.ToInt32(rdr2["FUND_CODE"]);
                        }
                    }

                }


                connIpams.Open();

                //Store
                FUND_ID1 = FUND_ID;
                FUND_NAME1 = FundName;

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
                                Share_Name.Add(rdr.GetString(0).ToString());
                                Share_Symbol.Add(rdr.GetString(1).ToString());
                                DateCostLastUpdated.Add(rdr.GetDateTime(2).ToString());
                                AVERAGE_PRICE.Add(rdr.GetDecimal(3).ToString() == null ? 0 : rdr.GetDecimal(3));
                                //AVERAGE_PRICE.Add(33.89M);
                                LastUpdatedPerUnitCost.Add(rdr.GetDecimal(3).ToString("#.##"));
                                BOOK_COST.Add(rdr.GetDecimal(4));
                                LastUpdatedCost.Add(Math.Round(rdr.GetDecimal(4)).ToString("#,##0"));
                                double holding = Convert.ToDouble(rdr.GetDecimal(5));
                                QUANTITY.Add(rdr.GetDecimal(5));
                                LastUpdatedHolding.Add(holding.ToString("#,##0"));
                                LastUpdatedMarketPriceDate.Add(rdr.GetDateTime(6).ToString("#.##"));
                                string localSymbol = String.Empty;
                                string localCurrent = String.Empty;
                                decimal localValue = 0;
                                if (getSymbolStatus(rdr.GetString(1).ToString()))
                                {
                                    for (int i = 0; i < testShare.Length; i++)
                                    {
                                        if (rdr.GetString(1).ToString().Equals(testShare[i].SHARE_SYMBOL.ToString()))
                                        {
                                            localSymbol = testShare[i].SHARE_SYMBOL.ToString();
                                            localCurrent = testShare[i].SHARE_CURRENT.ToString();
                                            MARKET_PRICE.Add(Decimal.Parse(testShare[i].SHARE_CURRENT));
                                            localValue = Convert.ToDecimal(testShare[i].SHARE_CURRENT) * rdr.GetDecimal(5);
                                            MARKET_VALUE.Add(localValue);
                                        }
                                    }
                                }
                                MarketSymbol.Add(localSymbol);

                                MarketPriceCurrent.Add(localCurrent);
                                MarketValue.Add(Convert.ToInt32(Math.Round(localValue)).ToString("#,##0"));
                                decimal appreciation = localValue - rdr.GetDecimal(4);
                                APPRECIATION_DEPRECIATION.Add(appreciation.ToString());
                                string localAppreciate = Math.Round(appreciation, 2).ToString("#,##0");
                                if (appreciation < 0)
                                {
                                    localAppreciate = "(" + localAppreciate.Replace("-", "") + ")";


                                }

                                Appreciation_Depreciation.Add(localAppreciate.ToString());
                            }
                        }
                        else { }
                    }
                }

                connIpams.Close();

            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message, "Database Connectivity Problem", MessageBoxButton.OK, MessageBoxImage.Information);
                Debug.WriteLine("SQL Exception: " + ex.Message);

            }
            catch (WebException ex)
            {
                MessageBox.Show(ex.Message, "Internet Connectivity Problem", MessageBoxButton.OK, MessageBoxImage.Information);
                Debug.WriteLine("Internet Exception: " + ex.Message);
            }
            catch(Exception ex)
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
            SqlCommand cmd = new SqlCommand("spInsertFund", conn);
            cmd.Parameters.AddWithValue("@FUND_CODE", txtFundID.Text);
            cmd.Parameters.AddWithValue("@FUND_NAME", txtFund.Text);
            cmd.Parameters.AddWithValue("@FUND_SYMBOL", txtFund.Text);
            cmd.Parameters.AddWithValue("@FUND_DESC", txtFund.Text);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                cmd.ExecuteReader();
            }

            catch (SqlException ex)
            {
                Debug.WriteLine("SQL Exception Occured: " + ex.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            comboFund.Items.Clear();

            SqlConnection conn3 = new SqlConnection();
            SqlConnection conn2 = new SqlConnection();
            conn3.ConnectionString = ConfigurationManager.ConnectionStrings["IpamsConnection"].ConnectionString;
            conn2.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            List<String> fundName = HasRows(conn2, 2);
            List<String> fundName2 = HasRows(conn3, 1);
            for (int i = 0; i < fundName.Count; i++)
            {
                comboFund.Items.Add(fundName[i]);
            }
            for (int i = 0; i < fundName2.Count; i++)
            {
                comboFund.Items.Add(fundName2[i]);
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
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Report report = new Report();
            report.Show();
            this.Close();
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
            if (list1.Visibility == Visibility.Visible)
            {
                
                await Task.Run(() => mustWork(FundName));

                FundMarket[] data = new FundMarket[Share_Name.Count];

                col1.DisplayMemberBinding = new Binding("SERIAL");
                col2.DisplayMemberBinding = new Binding("NAME");
                col3.DisplayMemberBinding = new Binding("SYMBOL");
                col4.DisplayMemberBinding = new Binding("CURRENT");
                col5.DisplayMemberBinding = new Binding("LDCP");
                col6.DisplayMemberBinding = new Binding("OPEN");
                col7.DisplayMemberBinding = new Binding("CHANGE");
                col8.DisplayMemberBinding = new Binding("VOLUME");
                col9.DisplayMemberBinding = new Binding("APPRECIATION_DEPRECIATION");

                list1.Items.Clear();

                for (int i = 0; i < Share_Name.Count; i++)
                {
                    if (Share_Name[i] == null) { }
                    else
                    {
                        list1.Items.Add(new FundMarket { SERIAL = i + 1, NAME = Share_Name[i], SYMBOL = Share_Symbol[i], CURRENT = LastUpdatedHolding[i], LDCP = LastUpdatedPerUnitCost[i], OPEN = LastUpdatedCost[i], HIGH = "", LOW = "", CHANGE = MarketPriceCurrent[i].Trim(), VOLUME = MarketValue[i].Trim(), APPRECIATION_DEPRECIATION = Appreciation_Depreciation[i].Trim() });
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



            var image2 = new BitmapImage();
            image2.BeginInit();
            image2.UriSource = ResourceAccessor.Get("Images/tick.gif");
            image2.EndInit();
            ImageBehavior.SetAnimatedSource(imgStatus, image2);

            lblStatus.Text = "Status: Ready";
        }

        private bool SavingDataToDatabase(string MARKET_STATUS, Int64 FUND_ID, string FUND_NAME, List<String> SHARE_NAME, List<String> SHARE_SYMBOL, List<Decimal> QUANTITY, List<Decimal> AVERAGE_PRICE, List<Decimal> BOOK_COST, List<Decimal> MARKET_PRICE, List<Decimal> MARKET_VALUE, List<String> APP_DEP)
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

                        SqlCommand cmd = new SqlCommand("spInsertFundMarketSummary", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@MARKET_STATUS", SqlDbType.VarChar, 500);
                        cmd.Parameters["@MARKET_STATUS"].Value = MARKET_STATUS;
                        cmd.Parameters.Add("@FUND_ID", SqlDbType.BigInt);
                        cmd.Parameters["@FUND_ID"].Value = FUND_ID;
                        cmd.Parameters.Add("@FUND_NAME", SqlDbType.VarChar,500);
                        cmd.Parameters["@FUND_NAME"].Value = FUND_NAME;
                        cmd.Parameters.Add("@SHR_NAME", SqlDbType.VarChar,500);
                        cmd.Parameters["@SHR_NAME"].Value = SHARE_NAME[i];
                        cmd.Parameters.Add("@SHR_SYMBOL", SqlDbType.VarChar,500);
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
                    return true;

                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Database Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Debug.WriteLine("SQL Exception: " + ex.Message);
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Database Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Debug.WriteLine("General Exception: " + ex.Message);
                    return false;
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
                return false;
            }
        }



    }
    
    
}
