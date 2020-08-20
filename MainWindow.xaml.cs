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
        public MainWindow()
        {
            InitializeComponent();
        }

        private HtmlNodeCollection FetchDataFromPSX(string param)
        {
            string URL = "https://www.psx.com.pk/market-summary/";
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

        private string[] GetDefault()
        {
            HtmlNodeCollection name_nodes = FetchDataFromPSX("//h4");
            String[] names = new String[name_nodes.Count];
            string[] result = new string[ 5 + name_nodes.Count];
            
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
            for(int i = 0; i < names.Count() ; i++)
            {
                 result[i+5] = names[i];
            }
            return result;
        }

        private string[] GetNewData()
        {
            HtmlNodeCollection name_nodes = FetchDataFromPSX("//td");
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
            HtmlNodeCollection name_nodes = FetchDataFromPSX("//td");
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
            //for(int i = 0; i < result.Count(); i++)
            //{
            //    if (i % 6 == 0) { 
                    
            //    }
            //}
            return result;
        }

        private string[] GetMarketSummaryCompanyNames()
        {
            HtmlNodeCollection name_nodes = FetchDataFromPSX("//td");
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
            for (int j = 0; j < AllTableRowData.Count(); j++)
            {
                if (j % 8 == 0)
                {
                    if (AllTableRowData[j] != null)
                    {
                        if (AllTableRowData[j].ToString().Contains("SCRIP"))
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

        private string GetMarketSummaryCompanySymbols(string CompanyName)
        {
            string result = String.Empty;
            SqlConnection conn = new SqlConnection();
            try
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlDataReader rd;
                using (conn)
                {
                    SqlCommand cmd = new SqlCommand("spGetSymbolFromCompanyName", conn); // Read user-> stored procedure name
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CompanyName", SqlDbType.DateTime);
                    cmd.Parameters["@CompanyName"].Value = CompanyName;
                    conn.Open();
                    rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        result = rd[0].ToString();
                    }
                    rd.Close();
                }
                conn.Close();
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

        private string[] GetMarketSummaryCompanySymbols()
        {
            HtmlNodeCollection name_nodes = FetchDataFromPSX("//td");
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
            for (int j = 0; j < AllTableRowData.Count(); j++)
            {
                if (j % 8 == 0)
                {
                    if (AllTableRowData[j] != null)
                    {
                        if (AllTableRowData[j].ToString().Contains("SCRIP"))
                        {

                        }
                        else
                        {
                            result[counter2++] += GetMarketSummaryCompanySymbols(AllTableRowData[j]);

                        }
                    }
                }
            }
            return result;
            //string[] result = new string[1000];
            //result[0] = "AGTL";
            //result[1] = "ATLH";
            //result[2] = "DFML";
            //result[3] = "GHNI";
            //result[4] = "GNLT";
            //result[5] = "GAIL";
            //result[6] = "HINO";
            //result[7] = "HCAR";
            //result[8] = "INDU";
            //result[9] = "MTL";
            //result[10] = "PSMC";
            //result[11] = "SAZEW";
            //result[12] = "AGIL";
            //result[13] = "ATBA";
            //result[14] = "BWHL";
            //result[15] = "EXID";
            //result[16] = "GTYR";
            //result[17] = "LOADS";
            //result[18] = "THALL";
            //result[19] = "EMCO";
            //result[20] = "JOPP";
            //result[21] = "PAEL";
            //result[22] = "PCAL";
            //result[23] = "SIEG";
            //result[24] = "WAVES";
            //result[25] = "ACPL";
            //result[26] = "BWCL";
            //result[27] = "CHCC";
            //result[28] = "DGKC";
            //result[29] = "DCL";
            //result[30] = "FCCL";
            //result[31] = "FECTC";
            //result[32] = "FLYNG";
            //result[33] = "GWLC";
            //result[34] = "JVDC";
            //result[35] = "KOHC";
            //result[36] = "LCL";
            //result[37] = "MLCF";
            //result[38] = "PIOC";
            //result[39] = "POWE";
            //result[40] = "POWE";
            //result[41] = "SMCPL";
            //result[42] = "THCCL";
            //result[43] = "AGL";
            //result[44] = "ARPL";
            //result[45] = "BAPL";
            //result[46] = "BERG";
            //result[47] = "BIFO";
            //result[48] = "BUXL";
            //result[49] = "COLG";
            //result[50] = "DOL";
            //result[51] = "DYNO";
            //result[52] = "EPCL"; //Engro Polymer & Chemicals Ltd. 
            //result[53] = "GGL"; //Ghani Global Holdings Limited.	
            //result[54] = "ICI"; //ICI Pakistan Limited. 
            //result[55] = "ICL"; //Ittehad Chemical Ltd.
            //result[56] = "LCC"; //Lotte Chemical Pakistan Ltd.
            //result[57] = "NICL"; //Nimir Industrial Chemical Ltd. symbol
            //result[58] = "NRSL"; //Nimir Resins Limited. symbol
            //result[59] = "POL"; //Pakistan Oxygen Limited.
            //result[60] = "SCIL"; //Sitara Chemicals. symbol
            //result[61] = "SIPE"; //Sitara Peroxide Limited symbol
            //result[62] = "WAHN"; //Wah Noble Chemicals Ltd. symbol
            //result[63] = "HGFA"; //HBL Growth Fund symbol
            //result[64] = "HIFA"; //HBL Investment Fund symbol
            //result[65] = "TSMF"; //Tri - Star Mutual Fund Ltd.
            //result[66] = "ABL"; //Allied Bank Ltd.
            //return result;
        }

        private string[] GetMarketSummaryCompanyLDCP()
        {
            HtmlNodeCollection name_nodes = FetchDataFromPSX("//td");
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
            HtmlNodeCollection name_nodes = FetchDataFromPSX("//td");
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
            HtmlNodeCollection name_nodes = FetchDataFromPSX("//td");
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
            HtmlNodeCollection name_nodes = FetchDataFromPSX("//td");
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

        private string[] GetMarketSummaryCompanyCURRENT()
        {
            HtmlNodeCollection name_nodes = FetchDataFromPSX("//td");
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
            HtmlNodeCollection name_nodes = FetchDataFromPSX("//td");
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
            HtmlNodeCollection name_nodes = FetchDataFromPSX("//td");
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

        private bool SavingDataToDatabase(string[] defaultData, string[] companyName, string[] companySymbol, string[] LDCP, string[] OPEN, string[] HIGH, string[] LOW, string[] CURRENT, string[] CHANGE, string[] VOLUME)
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
                cmdspInsertMarketSummaryOverview.Parameters["@DATE"].Value = DateTime.Parse(defaultData[0]);
                cmdspInsertMarketSummaryOverview.Parameters.Add("@STATUS", SqlDbType.VarChar, 300);
                cmdspInsertMarketSummaryOverview.Parameters["@STATUS"].Value = defaultData[1];
                cmdspInsertMarketSummaryOverview.Parameters.Add("@VOLUME", SqlDbType.Float);
                cmdspInsertMarketSummaryOverview.Parameters["@VOLUME"].Value = Double.Parse(defaultData[2]);
                cmdspInsertMarketSummaryOverview.Parameters.Add("@VALUE", SqlDbType.Float);
                cmdspInsertMarketSummaryOverview.Parameters["@VALUE"].Value = Double.Parse(defaultData[3]);
                cmdspInsertMarketSummaryOverview.Parameters.Add("@TRADES", SqlDbType.Float);
                cmdspInsertMarketSummaryOverview.Parameters["@TRADES"].Value = Double.Parse(defaultData[4]);

                //

                SqlCommand cmdspInsertMarketSummaryOverviewHistory = new SqlCommand("spInsertMarketSummaryOverviewHistory", conn);
                cmdspInsertMarketSummaryOverviewHistory.CommandType = CommandType.StoredProcedure;
                cmdspInsertMarketSummaryOverviewHistory.Parameters.Add("@DATE", SqlDbType.DateTime);
                cmdspInsertMarketSummaryOverviewHistory.Parameters["@DATE"].Value = DateTime.Parse(defaultData[0]);
                cmdspInsertMarketSummaryOverviewHistory.Parameters.Add("@STATUS", SqlDbType.VarChar, 300);
                cmdspInsertMarketSummaryOverviewHistory.Parameters["@STATUS"].Value = defaultData[1];

                cmdspInsertMarketSummaryOverviewHistory.Parameters.Add("@VOLUME", SqlDbType.Float);
                cmdspInsertMarketSummaryOverviewHistory.Parameters["@VOLUME"].Value = Double.Parse(defaultData[2]);
                cmdspInsertMarketSummaryOverviewHistory.Parameters.Add("@VALUE", SqlDbType.Float);
                cmdspInsertMarketSummaryOverviewHistory.Parameters["@VALUE"].Value = Double.Parse(defaultData[3]);

                cmdspInsertMarketSummaryOverviewHistory.Parameters.Add("@TRADES", SqlDbType.Float);
                cmdspInsertMarketSummaryOverviewHistory.Parameters["@TRADES"].Value = Double.Parse(defaultData[4]);

                try
                {
                    
                    isDataCleared = cmdspClearTableData.ExecuteNonQuery();
                    i = cmdspInsertMarketSummaryOverviewHistory.ExecuteNonQuery();
                    k = cmdspInsertMarketSummaryOverview.ExecuteNonQuery();
                    
                }
                catch (SqlException)
                {
                    Debug.WriteLine("SQL Exception Occured.");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Exception: " + ex.Message);
                }

                for (int m = 0; m < CURRENT.Length; m++)
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
                        cmdForspInsertMarketSummaryHistory.Parameters["@DATE"].Value = DateTime.Parse(defaultData[0]);
                        cmdForspInsertMarketSummaryHistory.Parameters.Add("@STATUS", SqlDbType.VarChar, 500);
                        cmdForspInsertMarketSummaryHistory.Parameters["@STATUS"].Value = defaultData[1];

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

                        try
                        {
                            
                            j = cmdspInsertMarketSummary.ExecuteNonQuery();
                            l = cmdForspInsertMarketSummaryHistory.ExecuteNonQuery();
                            
                        }
                        catch (SqlException)
                        {
                            Debug.WriteLine("SQL Exception Occured.");
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("Exception: " + ex.Message);
                        }
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
        private readonly BackgroundWorker worker = new BackgroundWorker();

        private void Button_Click(object sender, RoutedEventArgs e)
        {


            btnGet.IsEnabled = false;
            progressBar.Visibility = Visibility.Visible;
            lblProgress.Content = "Please Wait..";

            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync(10000);

            //try
            //{
           
            //

            progressBar.Visibility = Visibility.Hidden;
            lblProgress.Content = "Processing Completed.";
            //MessageBox.Show(text);
            
        //}
        //catch (Exception ex)
        //{
        //    Debug.WriteLine("Exception: " + ex.Message);
        //}

    }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (isDataSaved)
            {
                PreviewWindow window = new PreviewWindow(RequestDate, RequestStatus, RequestValue, RequestVolume, RequestTrades, NAME, SYMBOL, LDCP, OPEN, HIGH, LOW, CURRENT, CHANGE, VOLUME);
                btnGet.IsEnabled = false;
                window.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Data Fetch Failed.", "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                Debug.WriteLine("Data saved Failed.");
                btnGet.IsEnabled = false;
            }
            
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            if (e.UserState != null)
                lblProgress.Content = e.UserState;
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            //lblProgress.Content = "Getting Headers..";
            string[] defaultData = GetDefault();
            RequestDate = DateTime.Parse(defaultData[0]);
            RequestStatus = defaultData[1];
            RequestValue = Double.Parse(defaultData[2]);
            RequestVolume = Double.Parse(defaultData[3]);
            RequestTrades = Double.Parse(defaultData[4]);
            //progressBar.Value = 10;
            //lblProgress.Content = "Getting Company Names..";
            string[] getCompanyNames = GetMarketSummaryCompanyNames();
            //progressBar.Value = 20;
            //lblProgress.Content = "Getting Company Symbols..";
            string[] getCompanySymbols = GetMarketSummaryCompanySymbols();
            //progressBar.Value = 30;
            //lblProgress.Content = "Getting Company LDCP..";
            string[] getCompanyLDCP = GetMarketSummaryCompanyLDCP();
            //progressBar.Value = 40;
            //lblProgress.Content = "Getting Company OPEN..";
            string[] getCompanyOPEN = GetMarketSummaryCompanyOPEN();
            //progressBar.Value = 50;
            //lblProgress.Content = "Getting Company HIGH..";
            string[] getCompanyHIGH = GetMarketSummaryCompanyHIGH();
            //progressBar.Value = 60;
            //lblProgress.Content = "Getting Company LOW..";
            string[] getCompanyLOW = GetMarketSummaryCompanyLOW();
            //progressBar.Value = 70;
            //lblProgress.Content = "Getting Company CURRENT..";
            string[] getCompanyCURRENT = GetMarketSummaryCompanyCURRENT();
            //progressBar.Value = 80;
            //lblProgress.Content = "Getting Company CHANGE..";
            string[] getCompanyCHANGE = GetMarketSummaryCompanyCHANGE();
            //progressBar.Value = 85;
            //lblProgress.Content = "Getting Company VOLUME..";
            string[] getCompanyVOLUME = GetMarketSummaryCompanyVOLUME();
            //progressBar.Value = 90;

            double[] CompanyLDCP = new double[getCompanyLDCP.Length];
            double[] CompanyOPEN = new double[getCompanyLDCP.Length];
            double[] CompanyHIGH = new double[getCompanyLDCP.Length];
            double[] CompanyLOW = new double[getCompanyLDCP.Length];
            double[] CompanyCURRENT = new double[getCompanyLDCP.Length];
            double[] CompanyCHANGE = new double[getCompanyLDCP.Length];
            double[] CompanyVOLUME = new double[getCompanyLDCP.Length];


            for (int i = 0; i < getCompanyNames.Length; i++)
            {
                CompanyLDCP[i] = Convert.ToDouble(getCompanyLDCP[i]);
                CompanyOPEN[i] = Convert.ToDouble(getCompanyOPEN[i]);
                CompanyHIGH[i] = Convert.ToDouble(getCompanyHIGH[i]);
                CompanyLOW[i] = Convert.ToDouble(getCompanyLOW[i]);
                CompanyCURRENT[i] = Convert.ToDouble(getCompanyCURRENT[i]);
                CompanyCHANGE[i] = Convert.ToDouble(getCompanyCHANGE[i]);
                CompanyVOLUME[i] = Convert.ToDouble(getCompanyVOLUME[i]);

                //New Changes
                NAME.Add(getCompanyNames[i]);
                SYMBOL.Add(getCompanyNames[i]);
                LDCP.Add(CompanyLDCP[i]);
                OPEN.Add(CompanyOPEN[i]);
                HIGH.Add(CompanyHIGH[i]);
                LOW.Add(CompanyLOW[i]);
                CURRENT.Add(CompanyCURRENT[i]);
                CHANGE.Add(CompanyCHANGE[i]);
                VOLUME.Add(CompanyVOLUME[i]);

            }


            //lblProgress.Content = "Saving Data..";
            isDataSaved = SavingDataToDatabase(defaultData, getCompanyNames, getCompanySymbols, getCompanyLDCP, getCompanyOPEN, getCompanyHIGH, getCompanyLOW, getCompanyCURRENT, getCompanyCHANGE, getCompanyVOLUME);
            //progressBar.Value = 100;
        }
    }
}
