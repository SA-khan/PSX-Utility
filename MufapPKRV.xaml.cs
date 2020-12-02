using HtmlAgilityPack;
using PSXDataFetchingApp.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
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
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Threading;

namespace PSXDataFetchingApp
{
    /// <summary>
    /// Interaction logic for MufapPKRV.xaml
    /// </summary>
    public partial class MufapPKRV : Window
    {

        string _Date = String.Empty;
        public List<PKRV> _tempItems = new List<PKRV>();
        public MufapPKRV()
        {
            InitializeComponent();
            pkrvDatepicker.SelectedDate = DateTime.Now;

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

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
            
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            HtmlNodeCollection result = doc.DocumentNode.SelectNodes(param);
            return result;
        }

        private async void btnGet_Click(object sender, RoutedEventArgs e)
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

            //Debug.WriteLine(pkrvDatepicker.SelectedDate.Value);

            int _year = pkrvDatepicker.SelectedDate.Value.Year;
            int _month = pkrvDatepicker.SelectedDate.Value.Month;
            string _day = String.Empty;
            if (pkrvDatepicker.SelectedDate.Value.Day.ToString().Length == 1)
            {
                _day = pkrvDatepicker.SelectedDate.Value.Day.ToString("0#");
            }
            else
            {
                _day = pkrvDatepicker.SelectedDate.Value.Day.ToString();
            }

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

            

            if (_year != 0 && _month != 0 && _day != "0" && pkrvDatepicker.SelectedDate.Value.ToString() != "")
            {
                list1.Items.Clear();
                _Date = pkrvDatepicker.SelectedDate.Value.ToString("dddd, dd MMMM yyyy");
                string url = "http://mufap.com.pk/pdf/PKRVs/" + pkrvDatepicker.SelectedDate.Value.Year + "/" + _monthString + "/PKRV" + _day + _month + _year + ".csv";
                //Debug.WriteLine("URL: " +url );
                List<PKRV> _summaryData = new List<PKRV>();
                try
                {
                    await Task.Run(() => _summaryData = GetFileUploadMufapPKRV(url));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("The File does not exist on Mufap Site.\nDetails: " + ex.Message, "File Not Found", MessageBoxButton.OKCancel, MessageBoxImage.Information);
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
                        list1.Items.Add(new PKRV { Id = ++counter, Tenor = _summaryData[i].Tenor, MidRate = _summaryData[i].MidRate, Change = _summaryData[i].Change });
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

        public List<PKRV> GetFileUploadMufapPKRV(string url)
        {
            //using (WebClient wc = new WebClient())
            //{
            //    wc.DownloadFile(
            //    new System.Uri(url),
            //        "PKRV.csv"
            //    );
            //}
            List<PKRV> items = new List<PKRV>();
            ////string URL = "http://www.mufap.com.pk/industry.php?tab=" + DateTime.Today.Year.ToString() + "1";
            string _fileName = "PKRV.csv";
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

                        ClearMufapPKRVSummary();

                        ///
                        /// Iterating Each Line of Market Summary Closing File
                        ///

                        while ((line = reader.ReadLine()) != null)
                        {

                            ///
                            /// Splitting Each Line Into Variables
                            ///

                            string[] data = line.Split(",");

                            if (data[0].ToString().Contains("Tenor")) { }
                            else
                            {
                                ///
                                /// Incrementing Counter to Show On List View Serial Number
                                ///

                                counter++;

                                ///
                                /// Saving Date Value to Global Variable
                                ///

                                //_Date = ;

                                ///
                                /// Getting Values from File
                                ///
                               // Debug.WriteLine("=> Tenor: " + data[0].ToString() + ", Mid Rate: " + data[1].ToString() + ", Change: " + data[2].ToString());

                                string lTenor = data[0].ToString();
                                string lMidRate = data[1].ToString();
                                string lChange = data[2].ToString();
                                //Debug.WriteLine("=> Tenor: " + lTenor + ", Mid Rate: " + lMidRate + ", Change: " + lChange);
                                PKRV Item = new PKRV { Id = counter, Tenor = lTenor, MidRate = lMidRate, Change = lChange };

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

                                int _DbStatus = SavingPKRVSummary(Item);

                                if (_DbStatus == 0)
                                {
                                    Debug.WriteLine("Database Insertion Status: " + _DbStatus + "\n At Name: " + Item.Id);
                                }
                            }

                        }
                    }

                }


            }

            else
            {
                MessageBox.Show("Files not found.\n Please verify that correct file "+_fileName+" exist in root location.", "PSX File Not Found", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (File.Exists(_fileName))
            {
                File.Delete(_fileName);
            }

            return items;
            //Code Runner End
        }

        public bool UnzipPSXFile(string path)
        {
            bool result = false;
            try
            {
                using (ZipArchive za = ZipFile.OpenRead(path))
                {
                    za.ExtractToDirectory("MufapDownloadDirectory/");
                }
                result = true;

                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unzipping File Exception Occured: " + ex.Message);
                return result;
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            lblStatus.Text = "Status: Processing";
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = ResourceAccessor.Get("Images/processing.gif");
            image.EndInit();
            ImageBehavior.SetAnimatedSource(imgStatus, image);

            if (_Date != "")
            {
                await Task.Run(() => RunExcel());
            }
            else
            {
                MessageBox.Show("Please fetch the PKRV price first.", "Data Not Found", MessageBoxButton.OK, MessageBoxImage.Information );
            }

            lblStatus.Text = "Status: Ready";
            var image2 = new BitmapImage();
            image2.BeginInit();
            image2.UriSource = ResourceAccessor.Get("Images/tick.gif");
            image2.EndInit();
            ImageBehavior.SetAnimatedSource(imgStatus, image2);
        }

        private int ClearMufapPKRVSummary()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            conn.Open();
            int status = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("spTRUNCATE_PKRV_SUMMARY", conn);
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
                MessageBox.Show(ex.Message, "Clearing Data Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Debug.WriteLine("General Exception: " + ex.Message);
                return 0;
            }
            finally
            {
                conn.Close();
            }

        }

        public int SavingPKRVSummary(PKRV _summary)
        {
            int status = 0;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("spINSERT_PKRV_SUMMARY", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PKRV_SelectedDate", SqlDbType.Date);
                cmd.Parameters["@PKRV_SelectedDate"].Value = DateTime.Now;
                cmd.Parameters.Add("@PKRV_Tenor", SqlDbType.VarChar, -1);
                cmd.Parameters["@PKRV_Tenor"].Value = _summary.Tenor;
                cmd.Parameters.Add("@PKRV_MidRate", SqlDbType.Decimal);
                cmd.Parameters["@PKRV_MidRate"].Value = _summary.MidRate;
                cmd.Parameters.Add("@PKRV_Change", SqlDbType.Float);
                cmd.Parameters["@PKRV_Change"].Value = Convert.ToDouble(_summary.Change);
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
                MessageBox.Show(ex.Message, "Saving Data Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            string _fileName = "MufapPKRVSummary.xlsx";
            ExcelPackage package = null;
            Stream xlFile = null;
            try
            {
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                using (package = new ExcelPackage())
                {

                    //Add a new worksheet to the empty workbook
                    var worksheet = package.Workbook.Worksheets.Add("MUFAP PKRV Summary");
                    //Add the headers
                    worksheet.Cells[1, 1].Value = "Id";
                    worksheet.Cells[1, 2].Value = "Selected Date";
                    worksheet.Cells[1, 3].Value = "Tenor";
                    worksheet.Cells[1, 4].Value = "Mid Rate";
                    worksheet.Cells[1, 5].Value = "Change";

                    //Add some items...
                    List<PKRV> items = getMufapPKRVSummary();

                    int total = 1;
                    for (int index = 0; index < items.Count; index++)
                    {
                        total = index + 2;
                        worksheet.Cells["A" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["A" + total].Value = items[index].Id.ToString();
                        worksheet.Cells["B" + total].Value = DateTime.Now.ToString();
                        worksheet.Cells["C" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["C" + total].Value = items[index].Tenor.ToString();
                        worksheet.Cells["D" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["D" + total].Value = items[index].MidRate.ToString();
                        worksheet.Cells["E" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["E" + total].Value = items[index].Change.ToString();
                    }

                    //EndTest

                    //Ok now format the values;
                    using (var range = worksheet.Cells[1, 1, 1, 5])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.DarkBlue);
                        range.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    }

                    //Create an autofilter for the range
                    worksheet.Cells["A1:E4"].AutoFilter = true;

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
                    package.Workbook.Properties.Title = "Mufap PKRV Summary Details";
                    package.Workbook.Properties.Author = "Saad Ahmed";
                    package.Workbook.Properties.Comments = "This is the MUFAP PKRV summary detail report.";

                    // Set some extended property values
                    package.Workbook.Properties.Company = "EPPlus Software AB";

                    // Set some custom property values
                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", "Jan Källman");
                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", "EPPlus");

                    //var xlFile = FileOutputUtil.GetFileInfo("01-GettingStarted.xlsx");
                    string path = _fileName;
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
            //Debug.WriteLine("Excel Sheet Created.");
            Thread.Sleep(500);
            if (File.Exists(_fileName))
            {
                try
                {
                    Process p = new Process();
                    p.StartInfo.UseShellExecute = true;
                    p.StartInfo.FileName = _fileName;
                    p.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

        }

        public List<PKRV> getMufapPKRVSummary()
        {
            List<PKRV> items = new List<PKRV>();

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            conn.Open();
            // 1.  create a command object identifying the stored procedure
            SqlCommand cmd = new SqlCommand("spGET_PKRV_SUMMARY", conn);

            // 2. set the command object so it knows to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // execute the command
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                // iterate through results, printing each to console
                while (rdr.Read())
                {
                    Debug.WriteLine("Hello ");
                    items.Add(new PKRV { Id = rdr.GetInt64(0), Tenor = rdr.GetString(2), MidRate = rdr.GetDecimal(3).ToString(), Change = rdr.GetDouble(4).ToString() });
                    //Debug.WriteLine("Change: " + rdr.GetFloat(4).ToString());
                }
            }

            conn.Close();

            return items;
        }

        private void pkrvDatepicker_DateValidationError(object sender, DatePickerDateValidationErrorEventArgs e)
        {
            MessageBox.Show("Please select a valid date first..", "No Valid Date Selected!", MessageBoxButton.OK, MessageBoxImage.Hand);
        }
    }
}
