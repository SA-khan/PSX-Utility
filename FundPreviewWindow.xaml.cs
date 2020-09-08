using PSXDataFetchingApp.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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
        public static SHARE[] share;
        public FundPreviewWindow()
        {
            InitializeComponent();

            SqlConnection conn = new SqlConnection();
            SqlConnection conn2 = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["IpamsConnection"].ConnectionString;
            conn2.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            List<String> fundName = HasRows(conn2, 2);
            List<String> fundName2 = HasRows(conn, 1);
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
            //comboFund.SelectedItem = fundName[0];
            comboFund.Items.Add("<ADD NEW FUND>");
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

        public bool getSymbolStatus1(string Symbol)
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


        public  SHARE[] getShareDetail ()
        {
            List<string> NAME = mainClass.GetMarketSummaryCompanyNames();
            List<string> SYMBOL = mainClass.GetMarketSummaryCompanySymbols(NAME);
            string[] CURRENT = mainClass.GetMarketSummaryCompanyCURRENT();

            share = new SHARE[NAME.Count];
            for(int i = 0; i < NAME.Count; i++)
            {
                new SHARE { SHARE_NAME = NAME[i], SHARE_SYMBOL = SYMBOL[i], SHARE_CURRENT = CURRENT[i] };
                Debug.WriteLine("FNAME: " + NAME[i] + ", FSYMBOL: " + SYMBOL[i] + ", FCURRENT: " + CURRENT[i]);
            }
            return share;
        }

        private void btnGet_Click(object sender, RoutedEventArgs e)
        {
            int FUND_ID = 0;
            lblStatus.Text = "Processing..";
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
            cmdforFetchingShare.Parameters.Add(new SqlParameter("@DATE", DateTime.Now.AddYears(-1)));

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
            
            for(int j = 0; j < share.Length; j++)
            {
                Debug.WriteLine("NAME: " + share[j].SHARE_NAME + ", SYMBOL: " + share[j].SHARE_SYMBOL + ", CURRENT: " + share[j].SHARE_CURRENT);
            }

            // execute the command
            using (SqlDataReader rdr = cmdforFetchingShare.ExecuteReader())
            {
                while (rdr.Read())
                {
                    if (!rdr.IsDBNull(5))
                    {
                        Share_Name.Add(rdr.GetString(0).ToString());
                        Share_Symbol.Add(rdr.GetString(1).ToString());
                        DateCostLastUpdated.Add(rdr.GetDateTime(2).ToString());
                        LastUpdatedPerUnitCost.Add(rdr.GetDecimal(3).ToString());
                        LastUpdatedCost.Add(rdr.GetDecimal(4).ToString());
                        LastUpdatedHolding.Add(rdr.GetDecimal(5).ToString());
                        LastUpdatedMarketPriceDate.Add(rdr.GetDateTime(6).ToString());
                        string localSymbol = String.Empty;
                        string localCurrent = String.Empty;
                        if(rdr.GetString(1).ToString() == "1") {
                            for(int i = 0; i < share.Length; i++)
                            {
                                if(Share_Symbol.ToString() == share[i].SHARE_SYMBOL.ToString())
                                {
                                    localSymbol = share[i].SHARE_SYMBOL.ToString();
                                    localCurrent = share[i].SHARE_CURRENT;
                                }
                            }
                        }
                        MarketSymbol.Add(localSymbol);
                        MarketPriceCurrent.Add(localCurrent);
                        MarketValue.Add("2");
                        Appreciation_Depreciation.Add("3");
                    }
                    else { }
                }
            }

            connIpams.Close();

            //List<String> MarketSymbol = mainClass.GetMarketSummaryCompanySymbols();

            FundMarket[] data = new FundMarket[Share_Name.Count];

            col1.DisplayMemberBinding = new Binding("NAME");
            col2.DisplayMemberBinding = new Binding("SYMBOL");
            col3.DisplayMemberBinding = new Binding("CURRENT");
            col4.DisplayMemberBinding = new Binding("LDCP");
            col5.DisplayMemberBinding = new Binding("OPEN");
            col6.DisplayMemberBinding = new Binding("HIGH");
            col7.DisplayMemberBinding = new Binding("LOW");
            col8.DisplayMemberBinding = new Binding("CHANGE");
            col9.DisplayMemberBinding = new Binding("VOLUME");
            col10.DisplayMemberBinding = new Binding("APPRECIATION_DEPRECIATION");
            col1.Header = "NAME";
            col2.Header = "SYMBOL";
            col3.Header = "DATE OF COST LAST UPDATE";
            col4.Header = "AVERAGE PRICE";
            col5.Header = "BOOK COST";
            col6.Header = "QUANTITY";
            col7.Header = "LAST UPDATED MARKET PRICE DATE";
            col8.Header = "MARKET SYMBOL";
            col9.Header = "MARKET PRICE";
            col9.Header = "MARKET VALUE";
            col10.Header = "APPRECIATION / DEPRECIATION";
            
            //lblMessage.Content = "COMPANY NAME - SYMBOL - CURRENT - LDCP - OPEN - HIGH - LOW - CURRENT - CHANGE - VOLUME \n" ;
            for (int i = 0; i < Share_Name.Count; i++)
            {
                if (Share_Name[i] == null) { }
                else
                {
                    list1.Items.Add(new FundMarket { NAME = Share_Name[i], SYMBOL = Share_Symbol[i], CURRENT = DateCostLastUpdated[i], LDCP = LastUpdatedPerUnitCost[i], OPEN = LastUpdatedCost[i], HIGH = LastUpdatedHolding[i], LOW = LastUpdatedMarketPriceDate[i], CHANGE = MarketSymbol[i], VOLUME = MarketPriceCurrent[i], APPRECIATION_DEPRECIATION = Appreciation_Depreciation[i] });
                }
            }


            lblStatus.Text = "Ready";


        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {


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
            if (comboFund.SelectedItem.Equals("<ADD NEW FUND>"))
            {
                lblFundID.Foreground = new SolidColorBrush(Colors.Black);
                lblFundNAME.Foreground = new SolidColorBrush(Colors.Black);
                txtFundID.IsEnabled = true;
                txtFund.IsEnabled = true;
                btnAdd.IsEnabled = true;

            }
            else
            {
                lblFundID.Foreground = new SolidColorBrush(Colors.Gray);
                lblFundNAME.Foreground = new SolidColorBrush(Colors.Gray);
                txtFundID.IsEnabled = false;
                txtFund.IsEnabled = false;
                btnAdd.IsEnabled = false;

            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }
    }
}
