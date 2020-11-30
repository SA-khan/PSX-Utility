using PSXDataFetchingApp.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Shapes;
using WpfAnimatedGif;
using System.Linq;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Threading;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace PSXDataFetchingApp
{
    /// <summary>
    /// Interaction logic for MufapPKFRV.xaml
    /// </summary>
    public partial class MufapPKFRV : Window
    {
        string _Date = String.Empty;
        public List<PKFRV> _tempItems = new List<PKFRV>();
        public MufapPKFRV()
        {
            InitializeComponent();
            pkfrvDatepicker.SelectedDate = DateTime.Now;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

        private async void btnGet_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Date: " + pkfrvDatepicker.Text);
            if (pkfrvDatepicker.IsLoaded && pkfrvDatepicker.Text != "")
            {
                FundImage.Visibility = Visibility.Hidden;
                loadingImage.Visibility = Visibility.Visible;
                list1.Visibility = Visibility.Hidden;
                lblStatus.Text = "Status: Processing";
                var image = new BitmapImage();
                image.BeginInit();
                image.UriSource = ResourceAccessor.Get("Images/processing.gif");
                image.EndInit();
                ImageBehavior.SetAnimatedSource(imgStatus, image);

                int _year = pkfrvDatepicker.SelectedDate.Value.Year;
                int _month = pkfrvDatepicker.SelectedDate.Value.Month;
                int _day = pkfrvDatepicker.SelectedDate.Value.Day;

                string _monthString = String.Empty;
                switch (_month)
                {
                    case 1:
                        _monthString = "Jan";
                        break;
                    case 2:
                        _monthString = "Feb";
                        break;
                    case 3:
                        _monthString = "Mar";
                        break;
                    case 4:
                        _monthString = "Apr";
                        break;
                    case 5:
                        _monthString = "May";
                        break;
                    case 6:
                        _monthString = "Jun";
                        break;
                    case 7:
                        _monthString = "Jul";
                        break;
                    case 8:
                        _monthString = "Aug";
                        break;
                    case 9:
                        _monthString = "Sep";
                        break;
                    case 10:
                        _monthString = "Oct";
                        break;
                    case 11:
                        _monthString = "Nov";
                        break;
                    case 12:
                        _monthString = "Dec";
                        break;
                    default:
                        _monthString = "Unknown";
                        break;
                }

                if (_year != 0 && _month != 0 && _day != 0 && pkfrvDatepicker.SelectedDate.Value != null)
                {
                    list1.Items.Clear();
                    _Date = pkfrvDatepicker.SelectedDate.Value.ToString("dddd, dd MMMM yyyy");
                    string url = "http://mufap.com.pk/pdf/PKFRVs/" + pkfrvDatepicker.SelectedDate.Value.Year + "/" + _monthString + "/PKFRV" + _day + _month + _year + ".csv";
                    List<PKFRV> _summaryData = new List<PKFRV>();
                    try
                    {
                        await Task.Run(() => _summaryData = GetFileUploadMufapPKFRV(url));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("No File Found on MUFAP site..\nDetails: " + ex.Message, "No File Found", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                    }

                    if (_Date != String.Empty && _summaryData.Count != 0)
                    {
                        txtDate.Text = "File Date: " + Convert.ToDateTime(_Date).ToString("dddd, dd MMMM yyyy");
                        var _selectedItem = from item in _summaryData
                                            select item;
                        _summaryData = _selectedItem.ToList();
                        int counter = 0;
                        for (int i = 0; i < _summaryData.Count; i++)
                        {
                            list1.Items.Add(new PKFRV { Id = ++counter, FloatingRateBond = _summaryData[i].FloatingRateBond, IssuanceDate = _summaryData[i].IssuanceDate, MaturityDate = _summaryData[i].MaturityDate, CouponFrequency = _summaryData[i].CouponFrequency, BMA = _summaryData[i].BMA, CANDM = _summaryData[i].CANDM, CMKA = _summaryData[i].CMKA, IONE = _summaryData[i].IONE, JSCM = _summaryData[i].JSCM, MCPL = _summaryData[i].MCPL, SCPL = _summaryData[i].SCPL, VCPL = _summaryData[i].VCPL, FMA = _summaryData[i].FMA });
                        }
                        _tempItems = _summaryData;
                        FundImage.Visibility = Visibility.Hidden;
                        loadingImage.Visibility = Visibility.Hidden;
                        list1.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        FundImage.Visibility = Visibility.Visible;
                        loadingImage.Visibility = Visibility.Hidden;
                        list1.Visibility = Visibility.Hidden;
                    }

                    //Debug.WriteLine("http://mufap.com.pk/pdf/PKRVs/" + pkrvDatepicker.SelectedDate.Value.Year + "/"+_monthString +"/PKRV"+_day+_month+_year+".csv");

                }
                else
                {
                    MessageBox.Show("Please select a valid date to fetch market summary closing data.", "Invalid Selection", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                lblStatus.Text = "Status: Ready";
                var image2 = new BitmapImage();
                image2.BeginInit();
                image2.UriSource = ResourceAccessor.Get("Images/tick.gif");
                image2.EndInit();
                ImageBehavior.SetAnimatedSource(imgStatus, image2);
            } 
            else
            {
                MessageBox.Show("Please select a valid date first..", "No Date Selected!", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        public List<PKFRV> GetFileUploadMufapPKFRV(string url)
        {
            //using (WebClient wc = new WebClient())
            //{
            //    wc.DownloadFile(
            //    new System.Uri(url),
            //        "PKRV.csv"
            //    );
            //}
            List<PKFRV> items = new List<PKFRV>();
            ////string URL = "http://www.mufap.com.pk/industry.php?tab=" + DateTime.Today.Year.ToString() + "1";
            string _fileName = "PKFRV.csv";
            if (File.Exists(_fileName))
            {
                File.Delete(_fileName);
            }
            if (!File.Exists(_fileName))
            {
                using (WebClient wc = new WebClient())
                {
                    wc.DownloadFile(
                    new System.Uri(url),
                        _fileName
                    );
                }
                if (File.Exists(_fileName))
                {
                    using (var reader = new StreamReader(_fileName))
                    {

                        ///
                        /// Initializing Variable to read each line of File
                        ///

                        string line;

                        ///
                        /// Initializing Counter To Show on Serial Number on List View 
                        ///

                        int counter = 0;

                        ///
                        /// Clearing Database Table 
                        ///

                        ClearMufapPKFRVSummary();

                        ///
                        /// Iterating Each Line of Market Summary Closing File
                        ///

                        while ((line = reader.ReadLine()) != null)
                        {
                            
                            if (line.ToString().Contains("\"")) {
                                //_Date = line.ToString().Replace(",","");
                            }
                            else if(line.ToString().StartsWith("PKFRV") || line.ToString().StartsWith("Revaluation") )
                            {

                            }
                            else if (line.ToString().StartsWith(",,,"))
                            {

                            }
                            else
                            {

                                ///
                                /// Splitting Each Line Into Variables
                                ///

                                string[] data = line.Split(",");

                                if (data[1].StartsWith("Issue Date")){ }

                                else
                                {
                                    ///
                                    /// Incrementing Counter to Show On List View Serial Number
                                    ///
                                    counter++;

                                    ///
                                    /// Getting Values from File
                                    ///

                                    string lFloatingRateBond = data[0].ToString();
                                    string lIssuanceDate = data[1].ToString();
                                    string lMaturityDate = data[2].ToString();
                                    string lCouponFrequency = data[3].ToString();
                                    string lBMA = data[4].ToString();
                                    string lCANDM = data[5].ToString();
                                    string lCMKA = data[6].ToString();
                                    string lIONE = data[7].ToString();
                                    string lJSCM = data[8].ToString();
                                    string lMCPL = data[9].ToString();
                                    string lSCPL = data[10].ToString();
                                    string lVCPL = data[11].ToString();
                                    string lFMA = data[12].ToString();

                                    //Debug.WriteLine("=> Floating Rate Bond: " + lFloatingRateBond + ", Issuence Date: " + lIssuanceDate + ", Maturity Date: " + lMaturityDate);
                                    PKFRV Item = new PKFRV { Id = counter, FloatingRateBond = lFloatingRateBond, IssuanceDate = lIssuanceDate, MaturityDate = lMaturityDate, CouponFrequency = lCouponFrequency, BMA = lBMA, CANDM = lCANDM, CMKA = lCMKA, IONE = lIONE, JSCM = lJSCM, MCPL = lMCPL, SCPL = lSCPL, VCPL = lVCPL, FMA = lFMA };

                                    ///
                                    /// Adding Record to ListView
                                    ///

                                    //if (lTenor.Equals("Tenor")) { }
                                    //else
                                    //{
                                    items.Add(Item);
                                    //}

                                    ///
                                    /// Saving Record
                                    ///

                                    int _DbStatus = SavingPKFRVSummary(Item);

                                    if (_DbStatus == 0)
                                    {
                                        Debug.WriteLine("Database Insertion Status: " + _DbStatus + "\n At ID: " + Item.Id);
                                    }
                                }
                            }

                        }
                    }

                }


            }

            else
            {
                MessageBox.Show("Files and Folders Are Not Deleted.\n Please Delete Market Summary.rar and PSXDownload Folder from Root Directory.", "PSX File Not Found", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (File.Exists(_fileName))
            {
                File.Delete(_fileName);
            }

            return items;
            //Code Runner End
        }

        // Event to track the progress
        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            //progressBar.Value = e.ProgressPercentage;
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {

        }

        private void pkfrvDatepicker_DateValidationError(object sender, DatePickerDateValidationErrorEventArgs e)
        {
            MessageBox.Show("Please select a valid date first..", "No Valid Date Selected!", MessageBoxButton.OK, MessageBoxImage.Hand);
        }

        private int ClearMufapPKFRVSummary()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            conn.Open();
            int status = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("spTRUNCATE_PKFRV_SUMMARY", conn);
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
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Debug.WriteLine("General Exception: " + ex.Message);
                return 0;
            }
            finally
            {
                conn.Close();
            }

        }

        public int SavingPKFRVSummary(PKFRV _summary)
        {
            int status = 0;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("spINSERT_PKFRV_SUMMARY", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PKFRV_FloatingRateBond", SqlDbType.VarChar, -1);
                cmd.Parameters["@PKFRV_FloatingRateBond"].Value = _summary.FloatingRateBond;
                cmd.Parameters.Add("@PKFRV_SelectedDate", SqlDbType.Date);
                cmd.Parameters["@PKFRV_SelectedDate"].Value = Convert.ToDateTime(_Date.Replace("-", ""));
                cmd.Parameters.Add("@PKFRV_IssueDate", SqlDbType.Date);
                cmd.Parameters["@PKFRV_IssueDate"].Value = Convert.ToDateTime(_summary.IssuanceDate.Replace("-",""));
                cmd.Parameters.Add("@PKFRV_MaturityDate", SqlDbType.Date);
                cmd.Parameters["@PKFRV_MaturityDate"].Value = Convert.ToDateTime(_summary.MaturityDate.Replace("-", ""));
                cmd.Parameters.Add("@PKFRV_CouponFrequency", SqlDbType.VarChar, -1);
                cmd.Parameters["@PKFRV_CouponFrequency"].Value = _summary.CouponFrequency;
                cmd.Parameters.Add("@PKFRV_BMA", SqlDbType.Decimal);
                cmd.Parameters["@PKFRV_BMA"].Value = Convert.ToDecimal(_summary.BMA);
                cmd.Parameters.Add("@PKFRV_CANDM", SqlDbType.Decimal);
                cmd.Parameters["@PKFRV_CANDM"].Value = Convert.ToDecimal(_summary.CANDM);
                cmd.Parameters.Add("@PKFRV_CMKA", SqlDbType.Decimal);
                cmd.Parameters["@PKFRV_CMKA"].Value = Convert.ToDecimal(_summary.CMKA);
                cmd.Parameters.Add("@PKFRV_IONE", SqlDbType.Decimal);
                cmd.Parameters["@PKFRV_IONE"].Value = Convert.ToDecimal(_summary.IONE);
                cmd.Parameters.Add("@PKFRV_JSCM", SqlDbType.Decimal);
                cmd.Parameters["@PKFRV_JSCM"].Value = Convert.ToDecimal(_summary.JSCM);
                cmd.Parameters.Add("@PKFRV_MCPL", SqlDbType.Decimal);
                cmd.Parameters["@PKFRV_MCPL"].Value = Convert.ToDecimal(_summary.MCPL);
                cmd.Parameters.Add("@PKFRV_SCPL", SqlDbType.Decimal);
                cmd.Parameters["@PKFRV_SCPL"].Value = Convert.ToDecimal(_summary.SCPL);
                cmd.Parameters.Add("@PKFRV_VCPL", SqlDbType.Decimal);
                cmd.Parameters["@PKFRV_VCPL"].Value = Convert.ToDecimal(_summary.VCPL);
                cmd.Parameters.Add("@PKFRV_FMA", SqlDbType.Decimal);
                cmd.Parameters["@PKFRV_FMA"].Value = Convert.ToDecimal(_summary.FMA);
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
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Debug.WriteLine("General Exception: " + ex.Message);
                return 0;
            }
            finally
            {
                conn.Close();
            }
        }

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
                    var worksheet = package.Workbook.Worksheets.Add("MUFAP PKFRV Summary");
                    //Add the headers
                    worksheet.Cells[1, 1].Value = "Id";
                    worksheet.Cells[1, 2].Value = "Floating Rate Bond";
                    worksheet.Cells[1, 3].Value = "Issue Date";
                    worksheet.Cells[1, 4].Value = "Maturity Date";
                    worksheet.Cells[1, 5].Value = "Coupon Frequency";
                    worksheet.Cells[1, 6].Value = "BMA";
                    worksheet.Cells[1, 7].Value = "C&M";
                    worksheet.Cells[1, 8].Value = "CMKA";
                    worksheet.Cells[1, 9].Value = "IONE";
                    worksheet.Cells[1, 10].Value = "JSCM";
                    worksheet.Cells[1, 11].Value = "MCPL";
                    worksheet.Cells[1, 12].Value = "SCPL";
                    worksheet.Cells[1, 13].Value = "VCPL";
                    worksheet.Cells[1, 14].Value = "FMA";

                    //Add some items...
                    List<PKFRV> items = getMufapPFKRVSummary();

                    int total = 1;
                    for (int index = 0; index < items.Count; index++)
                    {
                        total = index + 2;
                        worksheet.Cells["A" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["A" + total].Value = items[index].Id;
                        worksheet.Cells["B" + total].Value = items[index].FloatingRateBond;
                        worksheet.Cells["C" + total].Value = items[index].IssuanceDate;
                        worksheet.Cells["D" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["D" + total].Value = items[index].MaturityDate;
                        //worksheet.Cells["E" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["E" + total].Value = items[index].BMA;
                        worksheet.Cells["F" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["F" + total].Value = items[index].CANDM;
                        worksheet.Cells["G" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["G" + total].Value = items[index].CMKA;
                        worksheet.Cells["H" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["H" + total].Value = items[index].IONE;
                        worksheet.Cells["I" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["I" + total].Value = items[index].JSCM;
                        worksheet.Cells["J" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["J" + total].Value = items[index].MCPL;
                        worksheet.Cells["K" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["K" + total].Value = items[index].SCPL;
                        worksheet.Cells["L" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["L" + total].Value = items[index].VCPL;
                        worksheet.Cells["M" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["M" + total].Value = items[index].FMA;
                    }

                    //EndTest

                    //Ok now format the values;
                    using (var range = worksheet.Cells[1, 1, 1, 14])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.DarkBlue);
                        range.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    }

                    //Create an autofilter for the range
                    worksheet.Cells["A1:M4"].AutoFilter = true;

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
                    package.Workbook.Properties.Title = "Mufap PKFRV Summary Details";
                    package.Workbook.Properties.Author = "Saad Ahmed";
                    package.Workbook.Properties.Comments = "This is the psx market summary closing detail report.";

                    // Set some extended property values
                    package.Workbook.Properties.Company = "EPPlus Software AB";

                    // Set some custom property values
                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", "Jan Källman");
                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", "EPPlus");

                    //var xlFile = FileOutputUtil.GetFileInfo("01-GettingStarted.xlsx");
                    string path = "MarketSummaryClosing.xlsx";
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
                MessageBox.Show(ex.Message, "Problem: ", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                Debug.WriteLine("Exception: " + ex.Message);
            }
            finally
            {
                //xlFile.Dispose();
                //package.Dispose();
            }
            Debug.WriteLine("Excel Sheet Created.");
            Thread.Sleep(1000);
            if (File.Exists("MufapPKFRVSummary.xlsx"))
            {
                try
                {
                    //using (Stream stream = new FileStream("FundMarketSummary.xlsx", FileMode.Open))
                    //{
                    // File/Stream manipulating code here
                    Process p = new Process();
                    p.StartInfo.UseShellExecute = true;
                    p.StartInfo.FileName = "MufapPKFRVSummary.xlsx";
                    p.Start();
                    //}
                }
                catch (Exception ex)
                {
                    //check here why it failed and ask user to retry if the file is in use.
                    MessageBox.Show(ex.Message);
                }

            }

        }

        public List<PKFRV> getMufapPFKRVSummary()
        {
            List<PKFRV> items = new List<PKFRV>();

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            conn.Open();
            // 1.  create a command object identifying the stored procedure
            SqlCommand cmd = new SqlCommand("spGET_PKFRV_SUMMARY", conn);

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
                    items.Add(new PKFRV { Id = Convert.ToInt64(rdr.GetInt64(0)), FloatingRateBond = rdr.GetString(1), IssuanceDate = rdr.GetDateTime(2).ToString(), MaturityDate = rdr.GetDateTime(3).ToString(), CouponFrequency = rdr.GetString(4), BMA = rdr.GetDecimal(5).ToString(), CANDM = rdr.GetDecimal(6).ToString(), CMKA = rdr.GetDecimal(7).ToString(), IONE = rdr.GetDecimal(8).ToString(), JSCM = rdr.GetDecimal(9).ToString(), MCPL = rdr.GetDecimal(10).ToString(), SCPL = rdr.GetDecimal(11).ToString(), VCPL = rdr.GetDecimal(12).ToString(), FMA = rdr.GetDecimal(13).ToString() });
                    //Debug.WriteLine("FUND_ID in Default DB: " + FUND_ID);
                }
            }

            conn.Close();

            return items;
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {

            lblStatus.Text = "Status: Processing";
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = ResourceAccessor.Get("Images/processing.gif");
            image.EndInit();
            ImageBehavior.SetAnimatedSource(imgStatus, image);

            await  Task.Run( () => RunExcel() );

            lblStatus.Text = "Status: Ready";
            var image2 = new BitmapImage();
            image2.BeginInit();
            image2.UriSource = ResourceAccessor.Get("Images/tick.gif");
            image2.EndInit();
            ImageBehavior.SetAnimatedSource(imgStatus, image2);

        }
    }
}
