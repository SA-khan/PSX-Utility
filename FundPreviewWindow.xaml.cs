using PSXDataFetchingApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net.Cache;
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
using System.Windows.Threading;
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
        //public static SHARE[] share;
        public FundPreviewWindow()
        {
            InitializeComponent();

            txtDate.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm tt");

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


        public  SHARE[] getShareDetail ()
        {
            List<string> NAME = mainClass.GetMarketSummaryCompanyNames();
            List<string> SYMBOL = mainClass.GetMarketSummaryCompanySymbols(NAME);
            string[] CURRENT = mainClass.GetMarketSummaryCompanyCURRENT();

            SHARE[] share = new SHARE[NAME.Count];
            for(int i = 0; i < NAME.Count; i++)
            {
                share[i] = new SHARE { SHARE_NAME = NAME[i], SHARE_SYMBOL = SYMBOL[i], SHARE_CURRENT = CURRENT[i] };
                //Debug.WriteLine("FNAME: " + NAME[i] + ", FSYMBOL: " + SYMBOL[i] + ", FCURRENT: " + CURRENT[i]);
            }
            return share;
        }

        private void btnGet_Click(object sender, RoutedEventArgs e)
        {
            imgStatus.Source = new BitmapImage(ResourceAccessor.Get("Images/Exclaimation.png"));
            FundImage.Visibility = Visibility.Hidden;
            list1.Visibility = Visibility.Hidden;
            loadingImage.Visibility = Visibility.Visible;

            FundImage.Source = new BitmapImage(ResourceAccessor.Get("Images/exclaimation.png"));
            lblStatus.Text = "Status: Processing";

            for (int i = 0; i < 10; i++)
            {
                this.InvalidateVisual();
                this.Dispatcher.Invoke(delegate () { }, DispatcherPriority.Render);
                this.Dispatcher.Invoke(delegate () { }, DispatcherPriority.Render);
            }

            mustWork();
            //worker.DoWork += worker_DoWork;

            //worker.DoWork += worker_DoWork;
            //worker.ProgressChanged += worker_ProgressChanged;
            //worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            //worker.RunWorkerAsync();

            //Task.Run(async delegate
            //{
            //        await Task.Yield(); // fork the continuation into a separate work item
            //});
            lblStatus.Text = "Status: Ready";

        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            //myDelegate delegate1 = new myDelegate(mustWork);
            mustWork();
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

        

        private void mustWork()
        {
            int FUND_ID = 0;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connIpams = new SqlConnection();
            connIpams.ConnectionString = ConfigurationManager.ConnectionStrings["IpamsConnection"].ConnectionString;

            conn.Open();
            // 1.  create a command object identifying the stored procedure
            SqlCommand cmd = new SqlCommand("spGetFundIDByParamNAME", conn);

            // 2. set the command object so it knows to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // 3. add parameter to command, which will be passed to the stored procedure
            cmd.Parameters.Add(new SqlParameter("@FUND_NAME", comboFund.Text));

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

            connIpams.Open();

            // 1.  create a command object identifying the stored procedure
            SqlCommand cmdforFetchingShare = new SqlCommand("spGetShareDetailByParamFundIdAndDate", connIpams);

            // 2. set the command object so it knows to execute a stored procedure
            cmdforFetchingShare.CommandType = CommandType.StoredProcedure;

            // 3. add parameter to command, which will be passed to the stored procedure
            cmdforFetchingShare.Parameters.Add(new SqlParameter("@FUND_ID", FUND_ID));
            //cmdforFetchingShare.Parameters.Add(new SqlParameter("@DATE", DateTime.Now.AddYears(-1)));
            cmdforFetchingShare.Parameters.Add(new SqlParameter("@DATE", DateTime.Now));
            List<String> Share_Name = new List<String>();
            List<String> Share_Symbol = new List<String>();
            List<String> DateCostLastUpdated = new List<String>();
            List<String> LastUpdatedPerUnitCost = new List<String>();
            List<String> LastUpdatedCost = new List<String>();
            List<String> LastUpdatedHolding = new List<String>();
            List<String> LastUpdatedMarketPriceDate = new List<String>();
            List<String> MarketSymbol = new List<String>();
            List<String> MarketPriceCurrent = new List<String>();
            List<String> MarketValue = new List<String>();
            List<String> Appreciation_Depreciation = new List<String>();
            SHARE[] testShare = getShareDetail();
            // execute the command
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
                            LastUpdatedPerUnitCost.Add(rdr.GetDecimal(3).ToString("#.##"));
                            LastUpdatedCost.Add(Math.Round(rdr.GetDecimal(4)).ToString("#,##0"));
                            double holding = Convert.ToDouble(rdr.GetDecimal(5));
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
                                        localValue = Convert.ToDecimal(testShare[i].SHARE_CURRENT) * rdr.GetDecimal(5);
                                    }
                                }
                            }
                            MarketSymbol.Add(localSymbol);
                            MarketPriceCurrent.Add(localCurrent);
                            MarketValue.Add(Convert.ToInt32(Math.Round(localValue)).ToString("#,##0"));
                            decimal appreciation = localValue - rdr.GetDecimal(4);
                            string localAppreciate = Math.Round(appreciation, 2).ToString("#,##0");
                            if(appreciation < 0)
                            {
                                localAppreciate = "(" + localAppreciate.Replace("-","") + ")";
                                
                                
                            }
                                
                            Appreciation_Depreciation.Add(localAppreciate.ToString());
                        }
                    }
                    else { }
                }
            }

            connIpams.Close();

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

            
            

            FundImage.Visibility = Visibility.Hidden;
            loadingImage.Visibility = Visibility.Hidden;
            list1.Visibility = Visibility.Visible;
            for (int i = 0; i < Share_Name.Count; i++)
            {
                if (Share_Name[i] == null) { }
                else
                {
                    list1.Items.Add(new FundMarket { SERIAL = i + 1, NAME = Share_Name[i], SYMBOL = Share_Symbol[i], CURRENT = LastUpdatedHolding[i], LDCP = LastUpdatedPerUnitCost[i], OPEN = LastUpdatedCost[i], HIGH = "", LOW = "", CHANGE = MarketPriceCurrent[i].Trim(), VOLUME = MarketValue[i].Trim(), APPRECIATION_DEPRECIATION = Appreciation_Depreciation[i].Trim() });
                }
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
            try
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
                    comboFund.SelectedIndex = 0;
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
            catch(Exception ex )
            {
                Debug.WriteLine("When comboBox reloads: " + ex.Message);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
