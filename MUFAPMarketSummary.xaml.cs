using HtmlAgilityPack;
using PSXDataFetchingApp.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using WpfAnimatedGif;
using System.Linq;
using OfficeOpenXml;
using System.IO;
using System.Threading;
using OfficeOpenXml.Style;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Microsoft.EntityFrameworkCore;
using PSXDataFetchingApp.Model.BindingTargets;

namespace PSXDataFetchingApp
{
    /// <summary>
    /// Interaction logic for MUFAPMarketSummary.xaml
    /// </summary>
    public partial class MUFAPMarketSummary : Window
    {
        public DataContext _context;
        List<String> _HeaderText = new List<string>();
        public static List<MufapMarketSummary> __fundSummaryDetails = null;
        List<String> _category = new List<string> {     
                                                        "Money Market",
                                                        "Capital Protected",
                                                        "Capital Protected - Income",
                                                        "Fund of Funds - CPPI",
                                                        "Income",
                                                        "Aggressive Fixed Income",
                                                        "Balanced",
                                                        "Asset Allocation",
                                                        "Fund of Funds",
                                                        "Index Tracker",
                                                        "Equity",
                                                        "Shariah Compliant Money Market",
                                                        "Shariah Compliant Capital Protected Fund",
                                                        "Shariah Compliant Capital Protected-Income",
                                                        "Shariah Compliant Fund of Funds-CPPI",
                                                        "Shariah Compliant Income",
                                                        "Shariah Compliant Aggressive Fixed Income",
                                                        "Shariah Compliant Balanced Fund",
                                                        "Shariah Compliant Asset Allocation",
                                                        "Shariah Compliant Fund of Funds",
                                                        "Shariah Compliant Fund of Funds - Income",
                                                        "Shariah Compliant Index Tracker",
                                                        "Shariah Compliant Commodities",
                                                        "Shariah Compliant Equity"
                                        };
        public MUFAPMarketSummary()
        {
            InitializeComponent();
            comboCategory.Items.Add("All Categories");
            foreach(string _item in _category)
            comboCategory.Items.Add(_item);
            comboCategory.SelectedIndex = 0;
            //comboCategory.IsEnabled = false;
            //btnGet.IsEnabled = false;
            btnSave.IsEnabled = false;
            btnReset.IsEnabled = false;
            SearchTermTextBox.IsEnabled = false;
            btnSearch.IsEnabled = false;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow(_context);
            window.Show();
            this.Close();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            FundImage.Visibility = Visibility.Visible;
            loadingImage.Visibility = Visibility.Hidden;
            list1.Items.Clear();
            list1.Visibility = Visibility.Hidden;
            txtSearch.Text = "";
            comboCategory.SelectedIndex = 0;
        }

        private async void btnGet_Click(object sender, RoutedEventArgs e)
        {

            list1.Items.Clear();

            btnBack.IsEnabled = false;
            comboCategory.IsEnabled = false;
            btnGet.IsEnabled = false;
            btnSave.IsEnabled = false;
            btnReset.IsEnabled = false;
            txtSearch.IsEnabled = false;
            btnSearch.IsEnabled = false;

            lblStatus.Text = "Status: Processing";
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = ResourceAccessor.Get("Images/processing.gif");
            image.EndInit();
            ImageBehavior.SetAnimatedSource(imgStatus, image);

            FundImage.Visibility = Visibility.Hidden;
            loadingImage.Visibility = Visibility.Visible;
            list1.Visibility = Visibility.Hidden;

            list1.Items.Clear();

            string _selectedCategory = comboCategory.Text.ToString();
            //Debug.WriteLine(_selectedCategory);

            await Task.Run(() => __fundSummaryDetails = GetMUFAPFundSummaryData(_selectedCategory));

            int total = 1;
            foreach (MufapMarketSummary item in __fundSummaryDetails)
            {
                //Debug.WriteLine("Validity Date: " + item.ValidityDate);
                list1.Items.Add(new MufapMarketSummary {MufapMarketSummaryId = total++, Name = item.Name, Category = item.Category, Rating = item.Rating, ValidityDate = item.ValidityDate, Nav = item.Nav, Ytd = item.Ytd, Mtd = item.Mtd, _1Day = item._1Day, _15Days = item._15Days, _30Days = item._30Days, _90Days = item._90Days, _180Days = item._180Days, _270Days = item._270Days, _365Days = item._365Days, Ter = item.Ter, Mf = item.Mf, Sandm = item.Sandm });
            }

            FundImage.Visibility = Visibility.Hidden;
            loadingImage.Visibility = Visibility.Hidden;
            list1.Visibility = Visibility.Visible;

            btnBack.IsEnabled = true;
            comboCategory.IsEnabled = true;
            btnGet.IsEnabled = true;
            btnSave.IsEnabled = true;
            btnReset.IsEnabled = true;
            SearchTermTextBox.IsEnabled = true;
            btnSearch.IsEnabled = true;

            lblStatus.Text = "Status: Ready";
            var image2 = new BitmapImage();
            image2.BeginInit();
            image2.UriSource = ResourceAccessor.Get("Images/tick.gif");
            image2.EndInit();
            ImageBehavior.SetAnimatedSource(imgStatus, image2);

        }

        private List<String> GetHeaderData()
        {
            List<String> response = new List<string>();
            List<String> _data = getMUFAPMarketSummaryRates("");
            for (int i = 1; i < 17; i++){
                response.Add(_data[i]);
            }
            return response;  
        }
        private List<MufapMarketSummary> GetMUFAPFundSummaryData(string _selectcategory)
        {
            List<MufapMarketSummary> response = new List<MufapMarketSummary>();
            List<String> _data = getMUFAPMarketSummaryRates("");
            int _categoryFlag = 0;
            

            int y = 0;
            int counter = 0;
            bool _headerFlag = false;
            int _clearStatus = ClearMufapMarketSummary();
            if(_clearStatus == -1) {
                Debug.WriteLine("MUFAP Table Cleared..");
            }
            else
            {
                Debug.WriteLine("MUFAP Table Cleared.. Code: " + _clearStatus);
            }
            for (int i = 0; i < _data.Count; i++)
            {
                MufapMarketSummary obj1 = new MufapMarketSummary();
                for(int jk = 0; jk < _category.Count; jk++)
                {
                    if (_data[i].StartsWith(_category[jk]))
                    {
                        if (_categoryFlag == jk) {
                            _categoryFlag = jk;
                        }
                        else
                        {
                            _categoryFlag = jk;
                            _headerFlag = true;
                        }
                    }
                }
                if (_headerFlag) {
                    if (_data[i].Contains("*Fund Name"))
                    {
                        y = i;
                        obj1.Name = _data[y + 16];
                        obj1.Category = _category[_categoryFlag];
                        y++;
                        Debug.WriteLine("=>New Name: " + obj1.Name + ", Category: " + obj1.Category);
                    }
                    _headerFlag = false;
                }
                else
                {
                    if (_data[i].EndsWith("Unit Trust") || _data[i].EndsWith("of Pakistan") || _data[i].Contains("Yield") || _data[i].EndsWith("Optimizer") || _data[i].Contains("Plan") || _data[i].Contains("Fund") && !_data[i].Contains("*") && !_data[i].Contains("Fund of Funds - CPPI")) {
                        obj1.Name = _data[i];
                        obj1.Category = _category[_categoryFlag];
                        if (_data[i + 1].Contains("A") && _data[i + 1].Contains("(") && _data[i + 1].Contains(")"))
                        {
                            obj1.Rating = _data[i + 1];
                            obj1.ValidityDate = DateTime.Parse(_data[i + 2]);
                            obj1.Nav = _data[i + 3];
                            obj1.Ytd = _data[i + 4];
                            obj1.Mtd = _data[i + 5];
                            obj1._1Day = _data[i + 6];
                            obj1._15Days = _data[i + 7];
                            obj1._30Days = _data[i + 8];
                            obj1._90Days = _data[i + 9];
                            obj1._180Days = _data[i + 10];
                            obj1._270Days = _data[i + 11];
                            obj1._365Days = _data[i + 12];
                            obj1.Ter = _data[i + 13];
                            obj1.Mf = _data[i + 14];
                            obj1.Sandm = _data[i + 15];
                        }
                        else
                        {
                            obj1.ValidityDate = DateTime.Parse(_data[i + 1]);
                            obj1.Nav = _data[i + 2];
                            obj1.Ytd = _data[i + 3];
                            obj1.Mtd = _data[i + 4];
                            obj1._1Day = _data[i + 5];
                            obj1._15Days = _data[i + 6];
                            obj1._30Days = _data[i + 7];
                            obj1._90Days = _data[i + 8];
                            obj1._180Days = _data[i + 9];
                            obj1._270Days = _data[i + 10];
                            obj1._365Days = _data[i + 11];
                            obj1.Ter = _data[i + 12];
                            obj1.Mf = _data[i + 13];
                            obj1.Sandm = _data[i + 14];
                        }
                        if (string.IsNullOrEmpty(obj1.Rating))
                        {
                            obj1.Rating = " - ";
                        }
                        //Debug.WriteLine("=> "+obj1.FundId+": " + obj1.Name + ", Category: " + obj1.Category, ", Rating: "+ obj1.Rating + ", NAV: " + obj1.NAV + ", YTD: " + obj1.YTD + ", MTD: " + obj1.MTD + ", 1 Day: " + obj1._1Day + ", 15 Days: " + obj1._15Days + ", 30 Days: " + obj1._30Days + ", 90 Days: " + obj1._90Days + ", 180 Days: " + obj1._180Days + ", 270 Days: " + obj1._270Days + ", 365 Days: " + obj1._365Days + ", TER: " + obj1.TER + ", MF: " + obj1.MF + ", S&M" + obj1.SANDM + " .");
                        if (_selectcategory == "All Categories")
                        {
                            response.Add(new MufapMarketSummary { MufapMarketSummaryId = ++counter, Name = obj1.Name, Category = obj1.Category, Rating = string.IsNullOrEmpty(obj1.Rating) ? " - " : obj1.Rating, ValidityDate = obj1.ValidityDate, Nav = obj1.Nav, Ytd = obj1.Ytd, Mtd = obj1.Mtd, _1Day = obj1._1Day, _15Days = obj1._15Days, _30Days = obj1._30Days, _90Days = obj1._90Days, _180Days = obj1._180Days, _270Days = obj1._270Days, _365Days = obj1._365Days, Ter = obj1.Ter, Mf = obj1.Mf, Sandm = obj1.Sandm });
                            int _savingStatus = SavingSpecificMufapFundMarketSummary(obj1);
                            if(_savingStatus != 1) { Debug.WriteLine("Database Insertion Failed of Fund Name: " + obj1.Name); }
                        }
                        else if(obj1.Category == _selectcategory )
                        {
                            response.Add(new MufapMarketSummary { MufapMarketSummaryId = ++counter, Name = obj1.Name, Category = obj1.Category, Rating = string.IsNullOrEmpty(obj1.Rating) ? " - " : obj1.Rating, ValidityDate = obj1.ValidityDate, Nav = obj1.Nav, Ytd = obj1.Ytd, Mtd = obj1.Mtd, _1Day = obj1._1Day, _15Days = obj1._15Days, _30Days = obj1._30Days, _90Days = obj1._90Days, _180Days = obj1._180Days, _270Days = obj1._270Days, _365Days = obj1._365Days, Ter = obj1.Ter, Mf = obj1.Mf, Sandm = obj1.Sandm });
                            int _savingStatus = SavingSpecificMufapFundMarketSummary(obj1);
                            if (_savingStatus != 1) { Debug.WriteLine("Database Insertion Failed of Fund Name: " + obj1.Name); }
                        }
                    }
                    
                }
                
            }

            // return list of funds
            return response;
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
            HtmlNodeCollection result = null;
            result = doc.DocumentNode.SelectNodes(param);

            return result;
        }

        public List<String> getMUFAPMarketSummaryRates(string tag)
        {
            List<String> result = new List<String>();
            string _originalText = String.Empty;
            List<String> text = new List<string>();
            try
            {

                string URL = "http://www.mufap.com.pk/nav_returns_performance.php?tab=01";
                HtmlNodeCollection _nodes = null;
                HtmlNodeCollection _temp = null;
                _temp = FetchDataFromPSX(URL, "//td");
                _nodes = _temp != null ? _temp : null;
                if (_nodes != null)
                {
                    foreach (HtmlAgilityPack.HtmlNode node in _nodes)
                    {
                        if (node.InnerText != null)
                        {
                            _originalText += node.InnerText.ToString() + "\n";
                        }
                    }
                }

                string[] _madeText = _originalText.Split("\n");
                List<String> _finalText = new List<string>();

                for (int i = 0; i < _madeText.Length; i++)
                {
                    //if (_madeText[i].Trim() != "")
                    //{
                    text.Add(_madeText[i].Trim().Replace("  ", ""));
                    if(text[i] != "")
                    {
                        _finalText.Add(text[i]);
                    }
                    //}
                }
                return _finalText;

            }
            catch(WebException ex)
            {
                MessageBox.Show("Internet Exception: "+ ex.Message);
            }
            return text;

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

            string _word = SearchTermTextBox.Text.ToString().ToLower();
            if (_word != null)
            {
                list1.Items.Clear();
                string _searchKeyword = _word;
                List<MufapMarketSummary> _searchList = new List<MufapMarketSummary>();
                var _selectedItem = from item in __fundSummaryDetails
                                    where item.Name.ToLower().Contains(_searchKeyword) || item.Category.ToLower().Contains(_searchKeyword)
                                    orderby item.Name
                                    select item;
                _searchList = _selectedItem.ToList();
                int counter = 0;
                foreach (MufapMarketSummary item in _selectedItem)
                {
                    list1.Items.Add(new MufapMarketSummary { MufapMarketSummaryId = ++counter, Name = item.Name, Category = item.Category, Rating = item.Rating, ValidityDate = item.ValidityDate, Nav = item.Nav, Ytd = item.Ytd, Mtd = item.Mtd, _1Day = item._1Day, _15Days = item._15Days, _30Days = item._30Days, _90Days = item._90Days, _180Days = item._180Days, _270Days = item._270Days, _365Days = item._365Days, Ter = item.Ter, Mf = item.Mf, Sandm = item.Sandm });
                }
            }
        }

        private void SearchTermTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string _word = SearchTermTextBox.Text.ToString().ToLower();
            if (!string.IsNullOrEmpty(_word))
            {
                list1.Items.Clear();
                string _searchKeyword = _word;
                List<MufapMarketSummary> _searchList = new List<MufapMarketSummary>();
                var _selectedItem = from item in __fundSummaryDetails
                                    where item.Name.ToLower().Contains(_searchKeyword) || item.Category.ToLower().Contains(_searchKeyword)
                                    orderby item.Name
                                    select item;
                _searchList = _selectedItem.ToList();
                int counter = 0;
                foreach (MufapMarketSummary item in _selectedItem)
                {
                    list1.Items.Add(new MufapMarketSummary { MufapMarketSummaryId = ++counter, Name = item.Name, Category = item.Category, Rating = item.Rating, ValidityDate = item.ValidityDate, Nav = item.Nav, Ytd = item.Ytd, Mtd = item.Mtd, _1Day = item._1Day, _15Days = item._15Days, _30Days = item._30Days, _90Days = item._90Days, _180Days = item._180Days, _270Days = item._270Days, _365Days = item._365Days, Ter = item.Ter, Mf = item.Mf, Sandm = item.Sandm });
                }
            }
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            btnSave.IsEnabled = false;
            lblStatus.Text = "Status: Processing";
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = ResourceAccessor.Get("Images/processing.gif");
            image.EndInit();
            ImageBehavior.SetAnimatedSource(imgStatus, image);

            if (list1.Items.Count != 0)
            {
                await Task.Run(() => RunExcel());
            }
            else
            {
                MessageBox.Show("Please fetch the closing market summary first.", "Data Not Found", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            lblStatus.Text = "Status: Ready";
            var image2 = new BitmapImage();
            image2.BeginInit();
            image2.UriSource = ResourceAccessor.Get("Images/tick.gif");
            image2.EndInit();
            ImageBehavior.SetAnimatedSource(imgStatus, image2);
            btnSave.IsEnabled = true;
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
                    var worksheet = package.Workbook.Worksheets.Add("MUFAP Fund Market Summary");
                    //Add the headers
                    worksheet.Cells[1, 1].Value = "Id";
                    worksheet.Cells[1, 2].Value = "Name";
                    worksheet.Cells[1, 3].Value = "Category";
                    worksheet.Cells[1, 5].Value = "Validity Date";
                    worksheet.Cells[1, 4].Value = "Rating";
                    worksheet.Cells[1, 6].Value = "NAV";
                    worksheet.Cells[1, 7].Value = "YTD";
                    worksheet.Cells[1, 8].Value = "MTD";
                    worksheet.Cells[1, 9].Value = "1 Day";
                    worksheet.Cells[1, 10].Value = "15 Days";
                    worksheet.Cells[1, 11].Value = "30 Days";
                    worksheet.Cells[1, 12].Value = "90 Days";
                    worksheet.Cells[1, 13].Value = "180 Days";
                    worksheet.Cells[1, 14].Value = "270 Days";
                    worksheet.Cells[1, 15].Value = "365 Days";
                    worksheet.Cells[1, 16].Value = "TER %**";
                    worksheet.Cells[1, 17].Value = "MF %**";
                    worksheet.Cells[1, 18].Value = "S&M %**";

                    //Add some items...
                    List<MufapMarketSummary> items = getMufapMarketSummary();

                    int total = 1;
                    for (int index = 0; index < items.Count; index++)
                    {
                        total = index + 2;
                        worksheet.Cells["A" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["A" + total].Value = items[index].MufapMarketSummaryId;
                        worksheet.Cells["B" + total].Value = items[index].Name;
                        worksheet.Cells["C" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells["C" + total].Value = items[index].Category;
                        worksheet.Cells["D" + total].Value = items[index].Rating;
                        //worksheet.Cells["E" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["E" + total].Value = items[index].ValidityDate;
                        worksheet.Cells["F" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["F" + total].Value = items[index].Nav;
                        worksheet.Cells["G" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["G" + total].Value = items[index].Ytd;
                        worksheet.Cells["H" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["H" + total].Value = items[index].Mtd;
                        worksheet.Cells["I" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["I" + total].Value = items[index]._1Day;
                        worksheet.Cells["J" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["J" + total].Value = items[index]._15Days;
                        worksheet.Cells["K" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["K" + total].Value = items[index]._30Days;
                        worksheet.Cells["L" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["L" + total].Value = items[index]._90Days;
                        worksheet.Cells["M" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["M" + total].Value = items[index]._180Days;
                        worksheet.Cells["N" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["N" + total].Value = items[index]._270Days;
                        worksheet.Cells["O" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["O" + total].Value = items[index]._365Days;
                        worksheet.Cells["P" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["P" + total].Value = items[index].Ter;
                        worksheet.Cells["Q" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["Q" + total].Value = items[index].Mf;
                        worksheet.Cells["R" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["R" + total].Value = items[index].Sandm;
                    }

                    //EndTest

                    //Ok now format the values;
                    using (var range = worksheet.Cells[1, 1, 1, 18])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.DarkBlue);
                        range.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    }

                    //Create an autofilter for the range
                    worksheet.Cells["A1:K4"].AutoFilter = true;

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
                    package.Workbook.Properties.Title = "MUFAP Fund Market Summary Details";
                    package.Workbook.Properties.Author = "Saad Ahmed";
                    package.Workbook.Properties.Comments = "This is the mufap fund market summary detail report.";

                    // Set some extended property values
                    package.Workbook.Properties.Company = "EPPlus Software AB";

                    // Set some custom property values
                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", "Jan Källman");
                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", "EPPlus");

                    //var xlFile = FileOutputUtil.GetFileInfo("01-GettingStarted.xlsx");
                    string path = "MufapFundMarketSummary.xlsx";
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
            if (File.Exists("MufapFundMarketSummary.xlsx"))
            {
                try
                {
                    //using (Stream stream = new FileStream("FundMarketSummary.xlsx", FileMode.Open))
                    //{
                    // File/Stream manipulating code here
                    Process p = new Process();
                    p.StartInfo.UseShellExecute = true;
                    p.StartInfo.FileName = "MufapFundMarketSummary.xlsx";
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

        public List<MufapMarketSummary> getMufapMarketSummary()
        {
            List<MufapMarketSummary> items = new List<MufapMarketSummary>();

            if (ConfigurationManager.AppSettings["Client"].Equals("MSSQLSERVER"))
            {

                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                conn.Open();
                // 1.  create a command object identifying the stored procedure
                SqlCommand cmd = new SqlCommand("spGET_MUFAP_MARKET_SUMMARY", conn);

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
                        items.Add(new MufapMarketSummary { MufapMarketSummaryId = rdr.GetInt64(0), ValidityDate = rdr.GetDateTime(2), Category = rdr.GetString(3), Name = rdr.GetString(4), Rating = rdr.GetString(5), Nav = rdr.GetDecimal(6).ToString(), Ytd = rdr.GetDecimal(7).ToString(), Mtd = rdr.GetDecimal(8).ToString(), _1Day = rdr.GetDecimal(9).ToString(), _15Days = rdr.GetDecimal(10).ToString(), _30Days = rdr.GetDecimal(11).ToString(), _90Days = rdr.GetDecimal(12).ToString(), _180Days = rdr.GetDecimal(13).ToString(), _270Days = rdr.GetDecimal(14).ToString(), _365Days = rdr.GetDecimal(15).ToString(), Ter = rdr.GetDecimal(16).ToString(), Mf = rdr.GetDecimal(17).ToString(), Sandm = rdr.GetDecimal(18).ToString() });
                        //Debug.WriteLine("FUND_ID in Default DB: " + FUND_ID);
                    }
                }

                conn.Close();
            }

            else if (ConfigurationManager.AppSettings["Client"].Equals("SQLITE"))
            {
                items = _context.MufapMarketSummary.ToList();
            }

                return items;
        }

        private int ClearMufapMarketSummary()
        {
            int status = 0;

            if (ConfigurationManager.AppSettings["Client"].Equals("MSSQLSERVER"))
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                conn.Open();

                try
                {
                    SqlCommand cmd = new SqlCommand("spTRUNCATE_MUFAP_MARKET_SUMMARY", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    status = cmd.ExecuteNonQuery();

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
            else if (ConfigurationManager.AppSettings["Client"].Equals("SQLITE"))
            {
                _context.Database.ExecuteSqlRaw("TRUNCATE TABLE MufapMarketSummary");
            }
                return status;

        }

        public int SavingSpecificMufapFundMarketSummary(MufapMarketSummary _fundSummary)
        {
            int status = 0;

            if (ConfigurationManager.AppSettings["Client"].Equals("MSSQLSERVER"))
            {

                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                conn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("spINSERT_MUFAP_MARKET_SUMMARY", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@MMS_DATE", SqlDbType.DateTime);
                    cmd.Parameters["@MMS_DATE"].Value = DateTime.Now;
                    cmd.Parameters.Add("@MMS_VALIDITY_DATE", SqlDbType.DateTime);
                    cmd.Parameters["@MMS_VALIDITY_DATE"].Value = Convert.ToDateTime(_fundSummary.ValidityDate);
                    cmd.Parameters.Add("@MMS_CATEGORY", SqlDbType.VarChar, 500);
                    cmd.Parameters["@MMS_CATEGORY"].Value = _fundSummary.Category;
                    cmd.Parameters.Add("@MMS_NAME", SqlDbType.VarChar, 1000);
                    cmd.Parameters["@MMS_NAME"].Value = _fundSummary.Name;
                    cmd.Parameters.Add("@MMS_RATING", SqlDbType.VarChar, 500);
                    cmd.Parameters["@MMS_RATING"].Value = string.IsNullOrEmpty(_fundSummary.Rating) ? " - " : _fundSummary.Rating;
                    cmd.Parameters.Add("@MMS_NAV", SqlDbType.Decimal);
                    if (string.IsNullOrEmpty(_fundSummary.Nav) || _fundSummary.Nav.Contains("N/A"))
                    {
                        cmd.Parameters["@MMS_NAV"].Value = Convert.ToDecimal("0.00");
                    }
                    else
                    {
                        cmd.Parameters["@MMS_NAV"].Value = Convert.ToDecimal(_fundSummary.Nav);
                    }
                    cmd.Parameters.Add("@MMS_YTD", SqlDbType.Decimal);
                    if (string.IsNullOrEmpty(_fundSummary.Ytd) || _fundSummary.Ytd.Contains("N/A") || _fundSummary.Ytd.Contains("("))
                    {
                        cmd.Parameters["@MMS_YTD"].Value = Convert.ToDecimal("0.00");
                    }
                    else
                    {
                        cmd.Parameters["@MMS_YTD"].Value = Convert.ToDecimal(_fundSummary.Ytd);
                    }
                    //cmd.Parameters["@MMS_YTD"].Value = Convert.ToDecimal(_fundSummary.YTD);
                    cmd.Parameters.Add("@MMS_MTD", SqlDbType.Decimal);
                    if (string.IsNullOrEmpty(_fundSummary.Mtd) || _fundSummary.Mtd.Contains("N/A") || _fundSummary.Mtd.Contains("("))
                    {
                        cmd.Parameters["@MMS_MTD"].Value = Convert.ToDecimal("0.00");
                    }
                    else
                    {
                        cmd.Parameters["@MMS_MTD"].Value = Convert.ToDecimal(_fundSummary.Mtd);
                    }
                    //cmd.Parameters["@MMS_MTD"].Value = Convert.ToDecimal(_fundSummary.MTD);
                    cmd.Parameters.Add("@MMS_1Day", SqlDbType.Decimal);
                    if (string.IsNullOrEmpty(_fundSummary._1Day) || _fundSummary._1Day.Contains("N/A") || _fundSummary._1Day.Contains("("))
                    {
                        cmd.Parameters["@MMS_1Day"].Value = Convert.ToDecimal("0.00");
                    }
                    else
                    {
                        cmd.Parameters["@MMS_1Day"].Value = Convert.ToDecimal(_fundSummary._1Day);
                    }
                    //cmd.Parameters["@MMS_1Day"].Value = Convert.ToDecimal(_fundSummary._1Day);
                    cmd.Parameters.Add("@MMS_15Days", SqlDbType.Decimal);
                    if (string.IsNullOrEmpty(_fundSummary._15Days) || _fundSummary._15Days.Contains("N/A") || _fundSummary._15Days.Contains("("))
                    {
                        cmd.Parameters["@MMS_15Days"].Value = Convert.ToDecimal("0.00");
                    }
                    else
                    {
                        cmd.Parameters["@MMS_15Days"].Value = Convert.ToDecimal(_fundSummary._15Days);
                    }
                    //cmd.Parameters["@MMS_15Days"].Value = Convert.ToDecimal(_fundSummary._15Days);
                    cmd.Parameters.Add("@MMS_30Days", SqlDbType.Decimal);
                    if (string.IsNullOrEmpty(_fundSummary._30Days) || _fundSummary._30Days.Contains("N/A") || _fundSummary._30Days.Contains("("))
                    {
                        cmd.Parameters["@MMS_30Days"].Value = Convert.ToDecimal("0.00");
                    }
                    else
                    {
                        cmd.Parameters["@MMS_30Days"].Value = Convert.ToDecimal(_fundSummary._30Days);
                    }
                    //cmd.Parameters["@MMS_30Days"].Value = Convert.ToDecimal(_fundSummary._30Days);
                    cmd.Parameters.Add("@MMS_90Days", SqlDbType.Decimal);
                    if (string.IsNullOrEmpty(_fundSummary._90Days) || _fundSummary._90Days.Contains("N/A") || _fundSummary._90Days.Contains("("))
                    {
                        cmd.Parameters["@MMS_90Days"].Value = Convert.ToDecimal("0.00");
                    }
                    else
                    {
                        cmd.Parameters["@MMS_90Days"].Value = Convert.ToDecimal(_fundSummary._90Days);
                    }
                    //cmd.Parameters["@MMS_90Days"].Value = Convert.ToDecimal(_fundSummary._90Days);
                    cmd.Parameters.Add("@MMS_180Days", SqlDbType.Decimal);
                    if (string.IsNullOrEmpty(_fundSummary._180Days) || _fundSummary._180Days.Contains("N/A") || _fundSummary._180Days.Contains("("))
                    {
                        cmd.Parameters["@MMS_180Days"].Value = Convert.ToDecimal("0.00");
                    }
                    else
                    {
                        cmd.Parameters["@MMS_180Days"].Value = Convert.ToDecimal(_fundSummary._180Days);
                    }
                    //cmd.Parameters["@MMS_180Days"].Value = Convert.ToDecimal(_fundSummary._180Days);
                    cmd.Parameters.Add("@MMS_270Days", SqlDbType.Decimal);
                    if (string.IsNullOrEmpty(_fundSummary._270Days) || _fundSummary._270Days.Contains("N/A") || _fundSummary._270Days.Contains("("))
                    {
                        cmd.Parameters["@MMS_270Days"].Value = Convert.ToDecimal("0.00");
                    }
                    else
                    {
                        cmd.Parameters["@MMS_270Days"].Value = Convert.ToDecimal(_fundSummary._270Days);
                    }
                    //cmd.Parameters["@MMS_270Days"].Value = Convert.ToDecimal(_fundSummary._270Days);
                    cmd.Parameters.Add("@MMS_365Days", SqlDbType.Decimal);
                    if (string.IsNullOrEmpty(_fundSummary._365Days) || _fundSummary._365Days.Contains("N/A") || _fundSummary._365Days.Contains("("))
                    {
                        cmd.Parameters["@MMS_365Days"].Value = Convert.ToDecimal("0.00");
                    }
                    else
                    {
                        cmd.Parameters["@MMS_365Days"].Value = Convert.ToDecimal(_fundSummary._365Days);
                    }
                    //cmd.Parameters["@MMS_365Days"].Value = Convert.ToDecimal(_fundSummary._365Days);
                    cmd.Parameters.Add("@MMS_TER", SqlDbType.Decimal);
                    if (string.IsNullOrEmpty(_fundSummary.Ter) || _fundSummary.Ter.Contains("N/A") || _fundSummary.Ter.Contains("("))
                    {
                        cmd.Parameters["@MMS_TER"].Value = Convert.ToDecimal("0.00");
                    }
                    else
                    {
                        cmd.Parameters["@MMS_TER"].Value = Convert.ToDecimal(_fundSummary.Ter);
                    }
                    //cmd.Parameters["@MMS_TER"].Value = Convert.ToDecimal(_fundSummary.TER);
                    cmd.Parameters.Add("@MMS_MF", SqlDbType.Decimal);
                    if (string.IsNullOrEmpty(_fundSummary.Mf) || _fundSummary.Mf.Contains("N/A") || _fundSummary.Mf.Contains("("))
                    {
                        cmd.Parameters["@MMS_MF"].Value = Convert.ToDecimal("0.00");
                    }
                    else
                    {
                        cmd.Parameters["@MMS_MF"].Value = Convert.ToDecimal(_fundSummary.Mf);
                    }
                    //cmd.Parameters["@MMS_MF"].Value = Convert.ToDecimal(_fundSummary.MF);
                    cmd.Parameters.Add("@MMS_SANDM", SqlDbType.Decimal);
                    if (string.IsNullOrEmpty(_fundSummary.Sandm) || _fundSummary.Sandm.Contains("N/A") || _fundSummary.Sandm.Contains("("))
                    {
                        cmd.Parameters["@MMS_SANDM"].Value = Convert.ToDecimal("0.00");
                    }
                    else
                    {
                        cmd.Parameters["@MMS_SANDM"].Value = Convert.ToDecimal(_fundSummary.Sandm);
                    }
                    //cmd.Parameters["@MMS_SANDM"].Value = Convert.ToDecimal(_fundSummary.SANDM);
                    status = cmd.ExecuteNonQuery();



                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Database Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Debug.WriteLine("SQL Exception: " + ex.Message);
                    return 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Database Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Debug.WriteLine("General Exception: " + ex.Message);
                    return 0;
                }
                finally
                {
                    conn.Close();
                }
            }

            else if (ConfigurationManager.AppSettings["Client"].Equals("SQLITE"))
            {
                _context.MufapMarketSummary.Add(_fundSummary);
                _context.SaveChanges();
                status = 1;
            }

                return status;

        }

    }
}
