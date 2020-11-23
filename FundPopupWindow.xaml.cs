using PSXDataFetchingApp.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Media;
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
    /// Interaction logic for FundPopupWindow.xaml
    /// </summary>
    public partial class FundPopupWindow : Window
    {
        string defaultConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public List<SpecificFundBucket> data;
        static int flag = 0;
        public FundPopupWindow()
        {
            InitializeComponent();
            data = getFundBucketData();
            if (data != null && data[flag].FSB_READING_STATUS != true)
            {
                txtDate.Text = data[flag].FSB_DATE.ToString();
                txtFundName.Text = data[flag].FSB_FUND_NAME;
                txtShareName.Text = data[flag].FSB_SHARE_NAME;
                txtShareSymbol.Text = data[flag].FSB_SHARE_SYMBOL;
                txtShareQuantity.Text = data[flag].FSB_SHARE_QUANTITY.ToString();
                txtShareAveragePrice.Text = data[flag].FSB_SHARE_AVG_PRICE.ToString();
                txtShareBookCost.Text = data[flag].FSB_SHARE_BOOK_COST.ToString();
                txtShareMarketPrice.Text = data[flag].FSB_SHARE_MARKET_PRICE.ToString();
                txtShareMarketValue.Text = data[flag].FSB_SHARE_MARKET_VALUE.ToString();
                txtShareAppDep.Text = data[flag].FSB_SHARE_APP_DEP.ToString();
                txtShareClosingPercentage.Text = data[flag].FSB_SHARE_PERCENTAGE_CLOSING.ToString();
            }
        }
        public FundPopupWindow(long id)
        {
            InitializeComponent();
            SpecificFundBucket bucket = getFundBucketData(id);
            if (bucket != null && bucket.FSB_READING_STATUS != true)
            {
                txtDate.Text = bucket.FSB_DATE.ToString();
                txtFundName.Text = bucket.FSB_FUND_NAME;
                txtShareName.Text = bucket.FSB_SHARE_NAME;
                txtShareSymbol.Text = bucket.FSB_SHARE_SYMBOL;
                txtShareQuantity.Text = bucket.FSB_SHARE_QUANTITY.ToString("F");
                txtShareAveragePrice.Text = bucket.FSB_SHARE_AVG_PRICE.ToString("F");
                txtShareBookCost.Text = bucket.FSB_SHARE_BOOK_COST.ToString("F");
                txtShareMarketPrice.Text = bucket.FSB_SHARE_MARKET_PRICE.ToString("F");
                txtShareMarketValue.Text = bucket.FSB_SHARE_MARKET_VALUE.ToString("F");
                txtShareAppDep.Text = bucket.FSB_SHARE_APP_DEP.ToString("F");
                txtShareClosingPercentage.Text = bucket.FSB_SHARE_PERCENTAGE_CLOSING.ToString("F");
            }

            SoundPlayer player = new SoundPlayer();
            player.SoundLocation = "notification.wav";
            player.Load();
            player.Play();

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (flag > 0)
                {
                    flag--;
                    if (data != null && data[flag].FSB_READING_STATUS != true)
                    {
                        txtDate.Text = data[flag].FSB_DATE.ToString();
                        txtFundName.Text = data[flag].FSB_FUND_NAME;
                        txtShareName.Text = data[flag].FSB_SHARE_NAME;
                        txtShareSymbol.Text = data[flag].FSB_SHARE_SYMBOL;
                        txtShareQuantity.Text = data[flag].FSB_SHARE_QUANTITY.ToString();
                        txtShareAveragePrice.Text = data[flag].FSB_SHARE_AVG_PRICE.ToString();
                        txtShareBookCost.Text = data[flag].FSB_SHARE_BOOK_COST.ToString();
                        txtShareMarketPrice.Text = data[flag].FSB_SHARE_MARKET_PRICE.ToString();
                        txtShareMarketValue.Text = data[flag].FSB_SHARE_MARKET_VALUE.ToString();
                        txtShareAppDep.Text = data[flag].FSB_SHARE_APP_DEP.ToString();
                        txtShareClosingPercentage.Text = data[flag].FSB_SHARE_PERCENTAGE_CLOSING.ToString();
                    }
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Error on Back Button Press, " + ex.Message);
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (flag < data.Count)
                {
                    flag++;
                    if (data != null && data[flag].FSB_READING_STATUS != true)
                    {
                        txtDate.Text = data[flag].FSB_DATE.ToString();
                        txtFundName.Text = data[flag].FSB_FUND_NAME;
                        txtShareName.Text = data[flag].FSB_SHARE_NAME;
                        txtShareSymbol.Text = data[flag].FSB_SHARE_SYMBOL;
                        txtShareQuantity.Text = data[flag].FSB_SHARE_QUANTITY.ToString();
                        txtShareAveragePrice.Text = data[flag].FSB_SHARE_AVG_PRICE.ToString();
                        txtShareBookCost.Text = data[flag].FSB_SHARE_BOOK_COST.ToString();
                        txtShareMarketPrice.Text = data[flag].FSB_SHARE_MARKET_PRICE.ToString();
                        txtShareMarketValue.Text = data[flag].FSB_SHARE_MARKET_VALUE.ToString();
                        txtShareAppDep.Text = data[flag].FSB_SHARE_APP_DEP.ToString();
                        txtShareClosingPercentage.Text = data[flag].FSB_SHARE_PERCENTAGE_CLOSING.ToString();
                    }
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Error on Back Button Press, " + ex.Message);
            }

        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            long id = 0;
            if (data != null && data[flag].SB_ID != 0)
            {
                id = data[flag].SB_ID;
                Debug.WriteLine("ID: " + id);
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                conn.Open();
                int status = 0;
                try
                {
                    SqlCommand cmd = new SqlCommand("spUPDATE_FUND_SCRIP_BUCKET_READING_STATUS", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.BigInt);
                    cmd.Parameters["@ID"].Value = id;
                    status = cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Database Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Debug.WriteLine("SQL Exception: " + ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Debug.WriteLine("General Exception: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }

            }
            this.Close();

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public List<SpecificFundBucket> getFundBucketData()
        {
            List<SpecificFundBucket> bucket = new List<SpecificFundBucket>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = defaultConnectionString;
            conn.Open();
            SqlCommand cmd = new SqlCommand("spGET_FUND_SCRIP_BUCKET", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    bucket.Add(new SpecificFundBucket { SB_ID = rdr.GetInt64(0), FSB_DATE = rdr.GetDateTime(1), FSB_STATUS = rdr.GetString(2), FSB_READING_STATUS = rdr.GetBoolean(3), FSB_FUND_ID = rdr.GetInt64(4), FSB_FUND_NAME = rdr.GetString(5), FSB_SHARE_NAME = rdr.GetString(6), FSB_SHARE_SYMBOL = rdr.GetString(7), FSB_SHARE_QUANTITY = rdr.GetDecimal(8), FSB_SHARE_AVG_PRICE = rdr.GetDecimal(9), FSB_SHARE_BOOK_COST = rdr.GetDecimal(10), FSB_SHARE_MARKET_PRICE = rdr.GetDecimal(11), FSB_SHARE_MARKET_VALUE = rdr.GetDecimal(12), FSB_SHARE_APP_DEP = rdr.GetDecimal(13), FSB_SHARE_PERCENTAGE_CLOSING = rdr.GetDecimal(14) });
                }
            }
            conn.Close();
            return bucket;
        }

        public SpecificFundBucket getFundBucketData(long id)
        {
            SpecificFundBucket bucket = new SpecificFundBucket();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = defaultConnectionString;
            conn.Open();
            SqlCommand cmd = new SqlCommand("spGET_SPECIFIC_FUND_SCRIP_BUCKET", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ID", SqlDbType.BigInt);
            cmd.Parameters["@ID"].Value = id;
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    bucket.SB_ID = rdr.GetInt64(0);
                    bucket.FSB_DATE = rdr.GetDateTime(1);
                    bucket.FSB_STATUS = rdr.GetString(2);
                    bucket.FSB_READING_STATUS = rdr.GetBoolean(3);
                    bucket.FSB_FUND_ID = rdr.GetInt64(4);
                    bucket.FSB_FUND_NAME = rdr.GetString(5);
                    bucket.FSB_SHARE_NAME = rdr.GetString(6);
                    bucket.FSB_SHARE_SYMBOL = rdr.GetString(7);
                    bucket.FSB_SHARE_QUANTITY = rdr.GetDecimal(8);
                    bucket.FSB_SHARE_AVG_PRICE = rdr.GetDecimal(9);
                    bucket.FSB_SHARE_BOOK_COST = rdr.GetDecimal(10);
                    bucket.FSB_SHARE_MARKET_PRICE = rdr.GetDecimal(11);
                    bucket.FSB_SHARE_MARKET_VALUE = rdr.GetDecimal(12);
                    bucket.FSB_SHARE_APP_DEP = rdr.GetDecimal(13);
                    bucket.FSB_SHARE_PERCENTAGE_CLOSING = rdr.GetDecimal(14);
                }
            }
            conn.Close();
            return bucket;
        }

    }
}
