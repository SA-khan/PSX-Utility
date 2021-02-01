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

namespace PSXDataFetchingApp
{
    /// <summary>
    /// Interaction logic for FundBucket.xaml
    /// </summary>
    public partial class FundBucket : Window
    {
        public DataContext _context;
        string defaultConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public FundBucket()
        {
            InitializeComponent();

            List<SpecificFundBucket> items = getFundBucket();
            if (items != null && items.Count > 0) {
                try
                {
                    Debug.WriteLine(items[0].FSB_SHARE_NAME);

                    list1.Items.Clear();

                    FundImage.Visibility = Visibility.Hidden;
                    loadingImage.Visibility = Visibility.Hidden;
                    list1.Visibility = Visibility.Visible;
                    for (int i = 0; i < items.Count; i++) {
                        decimal _quantity = Math.Round(Convert.ToDecimal(items[i].FSB_SHARE_QUANTITY), MidpointRounding.AwayFromZero);
                        decimal _bookcost = Math.Round(Convert.ToDecimal(items[i].FSB_SHARE_BOOK_COST), MidpointRounding.AwayFromZero);
                        decimal _marketvalue = Math.Round(Convert.ToDecimal(items[i].FSB_SHARE_MARKET_VALUE), MidpointRounding.AwayFromZero);
                        decimal _appdep = Math.Round(Convert.ToDecimal(items[i].FSB_SHARE_APP_DEP), MidpointRounding.AwayFromZero);
                        list1.Items.Add(new SpecificFundBucket { SB_ID = Convert.ToInt32(items[i].SB_ID), FSB_FUND_NAME = items[i].FSB_FUND_NAME, FSB_SHARE_NAME = items[i].FSB_SHARE_NAME, FSB_SHARE_SYMBOL = items[i].FSB_SHARE_SYMBOL, FSB_SHARE_QUANTITY = _quantity.ToString("N0"), FSB_SHARE_AVG_PRICE = items[i].FSB_SHARE_AVG_PRICE, FSB_SHARE_BOOK_COST = _bookcost.ToString("N0"), FSB_SHARE_MARKET_PRICE = items[i].FSB_SHARE_MARKET_PRICE, FSB_SHARE_MARKET_VALUE = _marketvalue.ToString("N0"), FSB_SHARE_APP_DEP = _appdep.ToString("N0"), FSB_SHARE_PERCENTAGE_CLOSING = items[i].FSB_SHARE_PERCENTAGE_CLOSING.ToString()+"%"});
                    }
                }
                catch(Exception ex)
                {
                    Debug.WriteLine("Exception In Initialization Fund Bucket: " + ex.Message);
                }
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            FundPreviewWindow window = new FundPreviewWindow(_context);
            window.Show();
            this.Close();
        }

        public List<SpecificFundBucket> getFundBucket()
        {
            List<SpecificFundBucket> bucket = new List<SpecificFundBucket>();
            SqlConnection conn = new SqlConnection();
            try
            {
                conn.ConnectionString = defaultConnectionString;
                conn.Open();
                SqlCommand cmd = new SqlCommand("spGET_FUND_SCRIP_BUCKET", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        bucket.Add(new SpecificFundBucket { SB_ID = rdr.GetInt64(0), FSB_DATE = rdr.GetDateTime(1), FSB_STATUS = rdr.GetString(2), FSB_READING_STATUS = rdr.GetBoolean(3), FSB_FUND_ID = rdr.GetInt64(4), FSB_FUND_NAME = rdr.GetString(5), FSB_SHARE_NAME = rdr.GetString(6), FSB_SHARE_SYMBOL = rdr.GetString(7), FSB_SHARE_QUANTITY = rdr.GetDecimal(8).ToString(), FSB_SHARE_AVG_PRICE = rdr.GetDecimal(9), FSB_SHARE_BOOK_COST = rdr.GetDecimal(10).ToString(), FSB_SHARE_MARKET_PRICE = rdr.GetDecimal(11), FSB_SHARE_MARKET_VALUE = rdr.GetDecimal(12).ToString(), FSB_SHARE_APP_DEP = rdr.GetDecimal(13).ToString(), FSB_SHARE_PERCENTAGE_CLOSING = rdr.GetDecimal(14).ToString() });
                    }
                }   
            }
            catch(SqlException ex)
            {
                Debug.WriteLine("SQL Exception: " + ex.Message);
                MessageBox.Show(ex.Message, "Database Connectivity Exception");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception: " + ex.Message);
                MessageBox.Show(ex.Message, "Exception");
            }
            finally
            {
                conn.Close();
            }
            return bucket;
        }

    }
}
