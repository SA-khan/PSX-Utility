using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace PSXDataFetchingApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //License Date 
        DateTime ExpiryDate = DateTime.Parse("2020/09/16 15:17:00");
        public IConfiguration Configuration { get; set; }

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

        private delegate void myDelegate();
        public MainWindow()
        {
            InitializeComponent();
        }

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
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            HtmlNodeCollection result = doc.DocumentNode.SelectNodes(param);
            return result;
        }

        public string[] GetDefault()
        {
            
            HtmlNodeCollection name_nodes = FetchDataFromPSX("https://www.psx.com.pk/market-summary/", "//h4");
            String[] names = new String[name_nodes.Count];
            string[] result = new string[ 5 + name_nodes.Count];
            
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
                for (int i = 0; i < names.Count(); i++)
                {
                    result[i + 5] = names[i];
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return result;
        }

        private string[] GetNewData()
        {
            HtmlNodeCollection name_nodes = FetchDataFromPSX("https://www.psx.com.pk/market-summary/", "//td");
            String[] names = new String[name_nodes.Count];
            string[] result = new string[5 + name_nodes.Count];

            int counter = 0;

            //Variable
            string localdatetime = String.Empty;
            string localstatus = String.Empty;
            string localVolume = String.Empty;
            string localValue = String.Empty;
            string localTrades = String.Empty;

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
            for (int i = 0; i < names.Count(); i++)
            {
                result[i + 5] = names[i];
            }
            return result;
        }

        private string[] GetFeaturedData()
        {
            HtmlNodeCollection name_nodes = FetchDataFromPSX("https://www.psx.com.pk/market-summary/", "//td");
            string[] result = new string[name_nodes.Count];

            int counter = 0;
            int StartCapturingflag = 0;

            foreach (HtmlAgilityPack.HtmlNode node in name_nodes)
            {
                if (StartCapturingflag == 1)
                {
                    result[counter++] = node.InnerText.ToString() + "\n";
                }
                else if (node.InnerText.ToString().Trim().Equals("VOLUME"))
                {
                    StartCapturingflag = 1;
                }
                else
                {
                    
                }
            }
            return result;
        }

        public List<string> GetMarketSummaryCompanyNames()
        {
            // Local List of string Variable to store return value 
            List<string> result = new List<string>();

            // Get All Nodes of td tag
            HtmlNodeCollection name_nodes = FetchDataFromPSX("https://www.psx.com.pk/market-summary/", "//td");

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

            // return company names
            return result;
        }

        public List<string> GetMarketSummaryCompanySymbols(List<string> CompanyName)
        {
            List<string> result = new List<string>( new string[NAME.Count]);
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
                        cmd.Parameters.Add("@CompanyName", SqlDbType.VarChar,500);
                        cmd.Parameters["@CompanyName"].Value = CompanyName[i];
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                result.Insert(i,rdr.GetString(0)); 
                            }
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
            finally {
                conn.Close();
            }
            return result;
        }

        //private string[] GetMarketSummaryCompanySymbols()
        //{
        //    HtmlNodeCollection name_nodes = FetchDataFromPSX("//td");
        //    string[] result = new string[name_nodes.Count];
        //    string[] AllTableRowData = new string[name_nodes.Count];

        //    int counter = 0;
        //    int StartCapturingflag = 0;

        //    foreach (HtmlAgilityPack.HtmlNode node in name_nodes)
        //    {
        //        if (StartCapturingflag == 1)
        //        {
        //            AllTableRowData[counter++] = node.InnerText.ToString() + "\n";
        //        }
        //        else if (node.InnerText.ToString().Trim().Equals("VOLUME"))
        //        {
        //            StartCapturingflag = 1;
        //        }
        //        else
        //        {

        //        }
        //    }
        //    int counter2 = 0;
        //    for (int j = 0; j < AllTableRowData.Count(); j++)
        //    {
        //        if (j % 8 == 0)
        //        {
        //            if (AllTableRowData[j] != null)
        //            {
        //                if (AllTableRowData[j].ToString().Contains("SCRIP"))
        //                {

        //                }
        //                else
        //                {
        //                    result[counter2++] += GetMarketSummaryCompanySymbols(AllTableRowData[j]);

        //                }
        //            }
        //        }
        //    }
        //    return result;
        //}

        private string[] GetMarketSummaryCompanyLDCP()
        {
            HtmlNodeCollection name_nodes = FetchDataFromPSX("https://www.psx.com.pk/market-summary/", "//td");
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
                    }
                }
            }
            return result;
        }

        private string[] GetMarketSummaryCompanyOPEN()
        {
            HtmlNodeCollection name_nodes = FetchDataFromPSX("https://www.psx.com.pk/market-summary/", "//td");
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
                    }
                }
            }
            return result;
        }

        private string[] GetMarketSummaryCompanyHIGH()
        {
            HtmlNodeCollection name_nodes = FetchDataFromPSX("https://www.psx.com.pk/market-summary/", "//td");
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
                    }
                }
            }
            return result;
        }

        private string[] GetMarketSummaryCompanyLOW()
        {
            HtmlNodeCollection name_nodes = FetchDataFromPSX("https://www.psx.com.pk/market-summary/", "//td");
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

        public string[] GetMarketSummaryCompanyCURRENT()
        {
            HtmlNodeCollection name_nodes = FetchDataFromPSX("https://www.psx.com.pk/market-summary/", "//td");
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
            return result;
        }

        private string[] GetMarketSummaryCompanyCHANGE()
        {
            HtmlNodeCollection name_nodes = FetchDataFromPSX("https://www.psx.com.pk/market-summary/", "//td");
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

        private string[] GetMarketSummaryCompanyVOLUME()
        {
            HtmlNodeCollection name_nodes = FetchDataFromPSX("https://www.psx.com.pk/market-summary/", "//td");
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

        private bool SavingDataToDatabase(string[] defaultData, List<string> companyName, List<string> companySymbol, string[] LDCP, string[] OPEN, string[] HIGH, string[] LOW, string[] CURRENT, string[] CHANGE, string[] VOLUME)
        {
            if(defaultData != null)
            {

                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                conn.Open();

                int isDataCleared = 0;

                int i = 0;
                int k = 0;
                int j = 0;
                int l = 0;

                //Clear Data

                SqlCommand cmdspClearTableData = new SqlCommand("ClearTableData", conn);
                cmdspClearTableData.CommandType = CommandType.StoredProcedure;

                //

                SqlCommand cmdspInsertMarketSummaryOverview = new SqlCommand("spInsertMarketSummaryOverview", conn);
                cmdspInsertMarketSummaryOverview.CommandType = CommandType.StoredProcedure;
                cmdspInsertMarketSummaryOverview.Parameters.Add("@DATE", SqlDbType.DateTime);
                cmdspInsertMarketSummaryOverview.Parameters["@DATE"].Value = RequestDate;
                cmdspInsertMarketSummaryOverview.Parameters.Add("@STATUS", SqlDbType.VarChar, 300);
                cmdspInsertMarketSummaryOverview.Parameters["@STATUS"].Value = RequestStatus;
                cmdspInsertMarketSummaryOverview.Parameters.Add("@VOLUME", SqlDbType.Float);
                cmdspInsertMarketSummaryOverview.Parameters["@VOLUME"].Value = RequestVolume;
                cmdspInsertMarketSummaryOverview.Parameters.Add("@VALUE", SqlDbType.Float);
                cmdspInsertMarketSummaryOverview.Parameters["@VALUE"].Value = RequestValue;
                cmdspInsertMarketSummaryOverview.Parameters.Add("@TRADES", SqlDbType.Float);
                cmdspInsertMarketSummaryOverview.Parameters["@TRADES"].Value = RequestTrades;

                //

                SqlCommand cmdspInsertMarketSummaryOverviewHistory = new SqlCommand("spInsertMarketSummaryOverviewHistory", conn);
                cmdspInsertMarketSummaryOverviewHistory.CommandType = CommandType.StoredProcedure;
                cmdspInsertMarketSummaryOverviewHistory.Parameters.Add("@DATE", SqlDbType.DateTime);
                cmdspInsertMarketSummaryOverviewHistory.Parameters["@DATE"].Value = RequestDate;
                cmdspInsertMarketSummaryOverviewHistory.Parameters.Add("@STATUS", SqlDbType.VarChar, 300);
                cmdspInsertMarketSummaryOverviewHistory.Parameters["@STATUS"].Value = RequestStatus;

                cmdspInsertMarketSummaryOverviewHistory.Parameters.Add("@VOLUME", SqlDbType.Float);
                cmdspInsertMarketSummaryOverviewHistory.Parameters["@VOLUME"].Value = RequestVolume;
                cmdspInsertMarketSummaryOverviewHistory.Parameters.Add("@VALUE", SqlDbType.Float);
                cmdspInsertMarketSummaryOverviewHistory.Parameters["@VALUE"].Value = RequestValue;

                cmdspInsertMarketSummaryOverviewHistory.Parameters.Add("@TRADES", SqlDbType.Float);
                cmdspInsertMarketSummaryOverviewHistory.Parameters["@TRADES"].Value = RequestTrades;

                try
                {
                    
                    isDataCleared = cmdspClearTableData.ExecuteNonQuery();
                    i = cmdspInsertMarketSummaryOverviewHistory.ExecuteNonQuery();
                    k = cmdspInsertMarketSummaryOverview.ExecuteNonQuery();
                    
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine("SQL Exception Occured: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Exception: " + ex.Message);
                }

                for (int m = 0; m < NAME.Count(); m++)
                {
                    try
                    {
                        //
                        if (LDCP[m] == null)
                        {

                        }
                        else
                        {
                            SqlCommand cmdspInsertMarketSummary = new SqlCommand("spInsertMarketSummary", conn);
                            cmdspInsertMarketSummary.CommandType = CommandType.StoredProcedure;
                            cmdspInsertMarketSummary.Parameters.Add("@COMPANY_NAME", SqlDbType.VarChar, 300);
                            cmdspInsertMarketSummary.Parameters["@COMPANY_NAME"].Value = companyName[m];
                            cmdspInsertMarketSummary.Parameters.Add("@COMPANY_SYMBOL", SqlDbType.VarChar, 300);
                            cmdspInsertMarketSummary.Parameters["@COMPANY_SYMBOL"].Value = companySymbol[m];

                            cmdspInsertMarketSummary.Parameters.Add("@COMPANY_LDCP", SqlDbType.Float);
                            cmdspInsertMarketSummary.Parameters["@COMPANY_LDCP"].Value = Double.Parse(LDCP[m]);
                            cmdspInsertMarketSummary.Parameters.Add("@COMPANY_OPEN", SqlDbType.Float);
                            cmdspInsertMarketSummary.Parameters["@COMPANY_OPEN"].Value = Double.Parse(OPEN[m]);

                            cmdspInsertMarketSummary.Parameters.Add("@COMPANY_HIGH", SqlDbType.Float);
                            cmdspInsertMarketSummary.Parameters["@COMPANY_HIGH"].Value = Double.Parse(HIGH[m]);
                            cmdspInsertMarketSummary.Parameters.Add("@COMPANY_LOW", SqlDbType.Float);
                            cmdspInsertMarketSummary.Parameters["@COMPANY_LOW"].Value = Double.Parse(LOW[m]);

                            cmdspInsertMarketSummary.Parameters.Add("@COMPANY_CURRENT", SqlDbType.Float);
                            cmdspInsertMarketSummary.Parameters["@COMPANY_CURRENT"].Value = Double.Parse(CURRENT[m]);
                            cmdspInsertMarketSummary.Parameters.Add("@COMPANY_CHANGE", SqlDbType.Float);
                            cmdspInsertMarketSummary.Parameters["@COMPANY_CHANGE"].Value = Double.Parse(CHANGE[m]);
                            cmdspInsertMarketSummary.Parameters.Add("@COMPANY_VOLUME", SqlDbType.Float);
                            cmdspInsertMarketSummary.Parameters["@COMPANY_VOLUME"].Value = Double.Parse(VOLUME[m]);

                            //

                            int nameFlag = 5;

                            SqlCommand cmdForspInsertMarketSummaryHistory = new SqlCommand("spInsertMarketSummaryHistory", conn);
                            cmdForspInsertMarketSummaryHistory.CommandType = CommandType.StoredProcedure;
                            cmdForspInsertMarketSummaryHistory.Parameters.Add("@DATE", SqlDbType.DateTime);
                            cmdForspInsertMarketSummaryHistory.Parameters["@DATE"].Value = RequestDate;
                            cmdForspInsertMarketSummaryHistory.Parameters.Add("@STATUS", SqlDbType.VarChar, 500);
                            cmdForspInsertMarketSummaryHistory.Parameters["@STATUS"].Value = RequestStatus;

                            cmdForspInsertMarketSummaryHistory.Parameters.Add("@SECTOR", SqlDbType.VarChar, 500);
                            cmdForspInsertMarketSummaryHistory.Parameters["@SECTOR"].Value = defaultData[nameFlag];
                            cmdForspInsertMarketSummaryHistory.Parameters.Add("@COMPANY_NAME", SqlDbType.VarChar, 500);
                            cmdForspInsertMarketSummaryHistory.Parameters["@COMPANY_NAME"].Value = companyName[m];

                            cmdForspInsertMarketSummaryHistory.Parameters.Add("@COMPANY_SYMBOL", SqlDbType.VarChar, 500);
                            cmdForspInsertMarketSummaryHistory.Parameters["@COMPANY_SYMBOL"].Value = companySymbol[m];
                            cmdForspInsertMarketSummaryHistory.Parameters.Add("@COMPANY_LDCP", SqlDbType.Float);
                            cmdForspInsertMarketSummaryHistory.Parameters["@COMPANY_LDCP"].Value = Double.Parse(LDCP[m]);

                            cmdForspInsertMarketSummaryHistory.Parameters.Add("@COMPANY_OPEN", SqlDbType.Float);
                            cmdForspInsertMarketSummaryHistory.Parameters["@COMPANY_OPEN"].Value = Double.Parse(OPEN[m]);
                            cmdForspInsertMarketSummaryHistory.Parameters.Add("@COMPANY_HIGH", SqlDbType.Float);
                            cmdForspInsertMarketSummaryHistory.Parameters["@COMPANY_HIGH"].Value = Double.Parse(HIGH[m]);
                            cmdForspInsertMarketSummaryHistory.Parameters.Add("@COMPANY_LOW", SqlDbType.Float);
                            cmdForspInsertMarketSummaryHistory.Parameters["@COMPANY_LOW"].Value = Double.Parse(LOW[0]);

                            cmdForspInsertMarketSummaryHistory.Parameters.Add("@COMPANY_CURRENT", SqlDbType.Float);
                            cmdForspInsertMarketSummaryHistory.Parameters["@COMPANY_CURRENT"].Value = Double.Parse(CURRENT[m]);
                            cmdForspInsertMarketSummaryHistory.Parameters.Add("@COMPANY_CHANGE", SqlDbType.Float);
                            cmdForspInsertMarketSummaryHistory.Parameters["@COMPANY_CHANGE"].Value = Double.Parse(CHANGE[m]);
                            cmdForspInsertMarketSummaryHistory.Parameters.Add("@COMPANY_VOLUME", SqlDbType.Float);
                            cmdForspInsertMarketSummaryHistory.Parameters["@COMPANY_VOLUME"].Value = Double.Parse(VOLUME[m]);


                            j = cmdspInsertMarketSummary.ExecuteNonQuery();
                            l = cmdForspInsertMarketSummaryHistory.ExecuteNonQuery();

                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message, "SQL Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                        Debug.WriteLine("SQL Exception: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                        Debug.WriteLine("Exception: " + ex.Message);
                    }
                }

                //

                if (i == 1 && j == 1 && k == 1 && l == 1)
                {
                    Debug.WriteLine("Data successfully saved.");
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
        private BackgroundWorker worker = new BackgroundWorker();

        public void Button_Click(object sender, RoutedEventArgs e)
        {

            DateTime CurrentTime = DateTime.Now;
            Debug.WriteLine(CurrentTime);
            DateTime ExpiredTime = ExpiryDate;
            Debug.WriteLine(ExpiredTime);
            if (ExpiredTime <= CurrentTime)
            {
                MessageBox.Show("Application is expired.");
            }
            else
            {
                //MessageBox.Show("Application is not expired.");
                imgWebScrap.Visibility = Visibility.Hidden;
                progressBar.Visibility = Visibility.Visible;
                lblProgress.Content = "Processing..";
                btnGet.IsEnabled = false;
                progressBar.Value = 1;
                //BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += worker_DoWork;
                worker.ProgressChanged += worker_ProgressChanged;
                worker.RunWorkerCompleted += worker_RunWorkerCompleted;
                worker.RunWorkerAsync();
            }

        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //if (isDataSaved)
            //{
                PreviewWindow window = new PreviewWindow(RequestDate, RequestStatus, RequestValue, RequestVolume, RequestTrades, NAME, SYMBOL, LDCP, OPEN, HIGH, LOW, CURRENT, CHANGE, VOLUME);
                btnGet.IsEnabled = false;
                window.Show();
                this.Hide();
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
            lblProgress.Content = statusContent;
        }

        #region MustWorkStart
        public void mustWork()
        {
            worker.WorkerReportsProgress = true;
            //int progressPercentage = Convert.ToInt32(((double)i / max) * 100);
            //(sender as BackgroundWorker)worker.ReportProgress(progressPercentage, statusFlag);
            statusContent = "Getting General Content..";
            worker.ReportProgress(1);
            string[] defaultData = GetDefault();
            RequestDate = DateTime.Parse(defaultData[0]);
            worker.ReportProgress(2);
            RequestStatus = defaultData[1];
            worker.ReportProgress(3);
            RequestValue = Double.Parse(defaultData[2]);
            worker.ReportProgress(4);
            RequestVolume = Double.Parse(defaultData[3]);
            worker.ReportProgress(5);
            RequestTrades = Double.Parse(defaultData[4]);
            statusContent = "Getting Company Names..";
            worker.ReportProgress(10);
            NAME = GetMarketSummaryCompanyNames();
            statusContent = "Getting Company Symbols..";
            worker.ReportProgress(25);
            SYMBOL = GetMarketSummaryCompanySymbols(NAME);
            statusContent = "Getting LDCP..";
            worker.ReportProgress(40);
            string[] getCompanyLDCP = GetMarketSummaryCompanyLDCP();
            statusContent = "Getting Open..";
            worker.ReportProgress(55);
            string[] getCompanyOPEN = GetMarketSummaryCompanyOPEN();
            statusContent = "Getting High..";
            worker.ReportProgress(65);
            string[] getCompanyHIGH = GetMarketSummaryCompanyHIGH();
            statusContent = "Getting Low..";
            worker.ReportProgress(75);
            string[] getCompanyLOW = GetMarketSummaryCompanyLOW();
            statusContent = "Getting Current..";
            worker.ReportProgress(85);
            string[] getCompanyCURRENT = GetMarketSummaryCompanyCURRENT();
            statusContent = "Getting Change..";
            worker.ReportProgress(95);
            string[] getCompanyCHANGE = GetMarketSummaryCompanyCHANGE();
            statusContent = "Getting Volume..";
            worker.ReportProgress(98);
            string[] getCompanyVOLUME = GetMarketSummaryCompanyVOLUME();
            worker.ReportProgress(99);
            double[] CompanyLDCP = new double[getCompanyLDCP.Length];
            double[] CompanyOPEN = new double[getCompanyLDCP.Length];
            double[] CompanyHIGH = new double[getCompanyLDCP.Length];
            double[] CompanyLOW = new double[getCompanyLDCP.Length];
            double[] CompanyCURRENT = new double[getCompanyLDCP.Length];
            double[] CompanyCHANGE = new double[getCompanyLDCP.Length];
            double[] CompanyVOLUME = new double[getCompanyLDCP.Length];
            //worker.ReportProgress(75);

            for (int i = 0; i < NAME.Count(); i++)
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
            worker.ReportProgress(100);
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

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            //myDelegate delegate1 = new myDelegate(mustWork);
            mustWork();
        }

        private void btnGetV3_Click(object sender, RoutedEventArgs e)
        {
            HtmlNodeCollection name_nodes = FetchDataFromPSX("https://dps.psx.com.pk/downloads", "//td");
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
            //return result;
        }

        private void btnMufapGetPKRV_Click(object sender, RoutedEventArgs e)
        {
            string URL = "http://www.mufap.com.pk/industry.php?tab=" + DateTime.Today.Year.ToString() + "1";
            HtmlNodeCollection name_nodes = FetchDataFromPSX(URL, "//div");
            string[] result = new string[name_nodes.Count];
            string text = String.Empty;

            foreach (HtmlAgilityPack.HtmlNode node in name_nodes)
            {
                if(node.InnerText != null)
                 text += node.InnerText.ToString();

            }
            
            for(int i = 0; i < result.Length; i++)
            {
                text += result[i] + "\n";
            }

            MessageBox.Show(text);

        }

        private void btnMufapGetPKFRV_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(DateTime.Today.Year.ToString());
        }

        private void btnMufapGetMarketSummary_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Click!");
            //
            getMUFAPMarketSummaryRates("Money Market");
            //MessageBox.Show(text);
            Debug.WriteLine("End!");
        }

        public void getMUFAPMarketSummaryRates(string tag)
        {
            string URL = "http://www.mufap.com.pk/nav_returns_performance.php?tab=01";
            HtmlNodeCollection name_nodes = FetchDataFromPSX(URL, "//td");

            string[] result = new string[name_nodes.Count];
            string text = String.Empty;
            int startCapture = 0;

            foreach (HtmlAgilityPack.HtmlNode node in name_nodes)
            {
                if (node.InnerText != null)
                {
                    //if (node.InnerText.Contains(tag))
                    //{
                        if (node.InnerText.ToString() == "S&M**")
                        {
                            startCapture = 1;
                        }
                        else if (node.InnerText.ToString().Trim().Contains("Capital") && node.InnerText.ToString().Trim().Contains("Protected") && node.InnerText.ToString().Trim().Contains("Absolute") && node.InnerText.ToString().Trim().Contains("Return"))
                        {
                            startCapture = 0;
                            break;
                        }
                        else if (startCapture == 1)
                        {
                            text += node.InnerText.ToString();
                        }
                    //}

                }

            }

            for (int i = 0; i < result.Length; i++)
            {
                text += result[i] + "\n";

            }
            Debug.WriteLine("Response: " + text);
        }

        private void btnGetV2_Click(object sender, RoutedEventArgs e)
        {
            DateTime CurrentTime = DateTime.Now;
            Debug.WriteLine(CurrentTime);
            DateTime ExpiredTime = ExpiryDate;
            Debug.WriteLine(ExpiredTime);
            if (ExpiredTime <= CurrentTime)
            {
                MessageBox.Show("Application is expired.");
            }
            else
            {
                FundPreviewWindow fundPreviewWindow = new FundPreviewWindow();
                fundPreviewWindow.Show();
                this.Hide();
            }
        }
    }
}
