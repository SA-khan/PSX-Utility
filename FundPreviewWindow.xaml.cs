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
        public FundPreviewWindow()
        {
            InitializeComponent();

            SqlConnection conn = new SqlConnection();
            SqlConnection conn2 = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["IpamsConnection"].ConnectionString;
            conn2.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            List<String> fundName = HasRows(conn2, 2);
            List<String> fundName2 = HasRows(conn, 1);
            for (int i = 0; i < fundName.Count; i++)
            {
                comboFund.Items.Add(fundName[i]);
            }
            for (int i = 0; i < fundName2.Count; i++)
            {
                comboFund.Items.Add(fundName2[i]);
            }
            //comboFund.SelectedItem = fundName[0];
            
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

            // execute the command
            using (SqlDataReader rdr = cmdforFetchingShare.ExecuteReader())
            {
                // iterate through results, printing each to console
                while (rdr.Read())
                {
                    //Debug.WriteLine(rdr.GetString(0));
                    Share_Name.Add(rdr.GetString(0));
                    Share_Symbol.Add(rdr.GetString(1));
                    //if(rdr.GetDateTime(2) != null)
                    //DateCostLastUpdated.Add(rdr.GetDateTime(2).ToString());
                    //if (rdr.GetDecimal(3).ToString() != null)
                    //    LastUpdatedPerUnitCost.Add(rdr.GetDecimal(3).ToString());
                    //LastUpdatedCost.Add(rdr.GetString(4).ToString());
                    //LastUpdatedHolding.Add(rdr.GetString(5).ToString());
                    //if (rdr.GetDateTime(6) != null)
                    //    LastUpdatedMarketPriceDate.Add(rdr.GetDateTime(6).ToString());
                }
            }

            connIpams.Close();

            FundMarket[] data = new FundMarket[Share_Name.Count];

            col1.DisplayMemberBinding = new Binding("Share_Name");
            col2.DisplayMemberBinding = new Binding("Share_Symbol");
            col3.DisplayMemberBinding = new Binding("DateCostLastUpdated");
            col4.DisplayMemberBinding = new Binding("LastUpdatedPerUnitCost");
            col5.DisplayMemberBinding = new Binding("LastUpdatedCost");
            col6.DisplayMemberBinding = new Binding("LastUpdatedHolding");
            col7.DisplayMemberBinding = new Binding("LastUpdatedMarketPriceDate");
            //col8.DisplayMemberBinding = new Binding("Change");
            //col9.DisplayMemberBinding = new Binding("Volume");
            col1.Header = "SHARE NAME";
            col2.Header = "SYMBOL";
            col3.Header = "DateCostLastUpdated";
            col4.Header = "LastUpdatedPerUnitCost";
            col5.Header = "LastUpdatedCost";
            col6.Header = "LastUpdatedHolding";
            col7.Header = "LastUpdatedMarketPriceDate";
            //col8.Header = "CHANGE";
            //col9.Header = "VOLUME";

            //lblMessage.Content = "COMPANY NAME - SYMBOL - CURRENT - LDCP - OPEN - HIGH - LOW - CURRENT - CHANGE - VOLUME \n" ;
            for (int i = 0; i < 1; i++)
            {
                if (Share_Name[i] == null) { }
                else
                {

                    list1.Items.Add(new FundMarket { NAME = Share_Name[i], SYMBOL = Share_Symbol[i], CURRENT = DateCostLastUpdated[i], LDCP = LastUpdatedPerUnitCost[i], OPEN = LastUpdatedCost[i], HIGH = LastUpdatedHolding[i], LOW = LastUpdatedMarketPriceDate[i] });
                }
            }


            lblStatus.Text = "Ready";



            //conn.Open();
            //SqlCommand cmd = new SqlCommand("[dbo].[spGetFundIDByParamNAME]", conn);
            ////cmd.Parameters.Add("@FUND_NAME", SqlDbType.VarChar, 500);
            //cmd.Parameters.AddWithValue("@FUND_NAME", "A FUND");
            //cmd.CommandType = CommandType.StoredProcedure;
            ////SqlDataReader reader;
            //try
            //{
            //    using (var reader = cmd.ExecuteReader())
            //    {
            //        if (reader.HasRows)
            //        {
            //            lblStatus.Text += reader.GetInt32(0).ToString();
            //        }
            //    }
            //}

            //catch (SqlException ex)
            //{
            //    Debug.WriteLine("SQL Exception Occured: " + ex.Message);
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine("Exception: " + ex.Message);
            //}
            //finally
            //{
            //    conn.Close();
            //}
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
    }
}
