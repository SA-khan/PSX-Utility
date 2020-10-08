using OfficeOpenXml;
using OfficeOpenXml.Style;
using PSXDataFetchingApp.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfAnimatedGif;

namespace PSXDataFetchingApp
{
    /// <summary>
    /// Interaction logic for PreviewWindow.xaml
    /// </summary>
    public partial class PreviewWindow : Window
    {

        public static SpecificScripDetail[] _scrip = null;
        public class MarketSummary
        {
            public string Name { get; set; }
            public string Symbol { get; set; }
            public double CURRENT { get; set; }

            public double LDCP { get; set; }
            public double OPEN { get; set; }
            public double HIGH { get; set; }
            public double LOW { get; set; }
            public double Change { get; set; }
            public double Volume { get; set; }
        }
        public PreviewWindow(DateTime date, string status, Double Volume, Double Value, Double Trades, List<string> companyName, List<string> companySymbol, List<double> LDCP, List<double> OPEN, List<double> HIGH, List<double> LOW, List<double> CURRENT, List<double> CHANGE, List<double> VOLUME)
        {
            InitializeComponent();

            _scrip = new SpecificScripDetail[companyName.Count];

            for(int i = 0; i < companyName.Count; i++)
            {
                _scrip[i] = new SpecificScripDetail();
                _scrip[i].SECTOR = "";
                _scrip[i].DATE = date;
                _scrip[i].STATUS = status;
                _scrip[i].SCRIP = companyName[i];
                _scrip[i].SYMBOL = companySymbol[i];
                _scrip[i].LDCP = Convert.ToDecimal(LDCP[i]);
                _scrip[i].OPEN = Convert.ToDecimal(OPEN[i]);
                _scrip[i].HIGH = Convert.ToDecimal(HIGH[i]);
                _scrip[i].LOW = Convert.ToDecimal(LOW[i]);
                _scrip[i].CURRENT = Convert.ToDecimal(CURRENT[i]);
                _scrip[i].CHANGE = Convert.ToDouble(CHANGE[i]);
                _scrip[i].VOLUME = Convert.ToDecimal(VOLUME[i]);
            }

            //Client Specific Properties
            try
            {
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["Client"]))
                {
                    if (ConfigurationManager.AppSettings["Client"].Equals("BOP"))
                    {
                        // Header Background Color 
                        var bc = new BrushConverter();
                        header.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#f0a500");

                        //Setting Logo
                        var image = new BitmapImage();
                        image.BeginInit();
                        image.UriSource = ResourceAccessor.Get("Images/BOP.gif");
                        image.EndInit();
                        ImageBehavior.SetAnimatedSource(imgClient, image);
                    }
                    else if (ConfigurationManager.AppSettings["Client"].Equals("HBL"))
                    {
                        // Header Background Color
                        var bc = new BrushConverter();
                        header.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#008269");

                        //Setting Logo
                        var image = new BitmapImage();
                        image.BeginInit();
                        image.UriSource = ResourceAccessor.Get("Images/HBL.gif");
                        image.EndInit();
                        ImageBehavior.SetAnimatedSource(imgClient, image);
                    }
                }
            }
            catch { }

            lblDate.Text +=  date.ToString();
            lblStatus.Text += status.ToString();
            lblVolume.Text += Volume.ToString("#,##0");
            lblValue.Text += Value.ToString("#,##0");
            lblTrades.Text += Trades.ToString("#,##0");

            MarketSummary[] data = new MarketSummary[companyName.Count];

            //GridViewColumn col1 = new GridViewColumn();
            //GridViewColumn col2 = new GridViewColumn();
            //GridViewColumn col3 = new GridViewColumn();
            //GridViewColumn col4 = new GridViewColumn();
            //GridViewColumn col5 = new GridViewColumn();
            //GridViewColumn col6 = new GridViewColumn();
            //GridViewColumn col7 = new GridViewColumn();
            //GridViewColumn col8 = new GridViewColumn();
            //GridViewColumn col9 = new GridViewColumn();


            //gridView.Columns.Add(col1);
            //gridView.Columns.Add(col2);
            //gridView.Columns.Add(col3);
            //gridView.Columns.Add(col4);
            //gridView.Columns.Add(col5);
            //gridView.Columns.Add(col6);
            //gridView.Columns.Add(col7);
            //gridView.Columns.Add(col8);
            //gridView.Columns.Add(col9);

            

            col1.DisplayMemberBinding = new Binding("Name");
            col2.DisplayMemberBinding = new Binding("Symbol");
            col3.DisplayMemberBinding = new Binding("CURRENT");
            col4.DisplayMemberBinding = new Binding("LDCP");
            col5.DisplayMemberBinding = new Binding("OPEN");
            col6.DisplayMemberBinding = new Binding("HIGH");
            col7.DisplayMemberBinding = new Binding("LOW");
            col8.DisplayMemberBinding = new Binding("Change");
            col9.DisplayMemberBinding = new Binding("Volume");
            col1.Header = "NAME";
            col2.Header = "SYMBOL";
            col3.Header = "CURRENT";
            col4.Header = "LDCP";
            col5.Header = "OPEN";
            col6.Header = "HIGH";
            col7.Header = "LOW";
            col8.Header = "CHANGE";
            col9.Header = "VOLUME";

            //lblMessage.Content = "COMPANY NAME - SYMBOL - CURRENT - LDCP - OPEN - HIGH - LOW - CURRENT - CHANGE - VOLUME \n" ;
            for (int i = 0; i < companyName.Count; i++)
            {
                if (companyName[i] == null) { }
                else
                {

                    list1.Items.Add(new MarketSummary { Name = companyName[i], Symbol = companySymbol[i], CURRENT = CURRENT[i],LDCP = LDCP[i], OPEN = OPEN[i], HIGH = HIGH[i], LOW = LOW[i], Change = CHANGE[i], Volume = VOLUME[i] });
                }
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            mainWindow.Button_Click(sender, e);
            this.Close();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Hide();
        }

        private async void btnExport_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() => RunExcel());
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
                    var worksheet = package.Workbook.Worksheets.Add("Pakistan Stock Exchange Market Summary");
                    //Add the headers
                    worksheet.Cells[1, 1].Value = "Sr. No.";
                    worksheet.Cells[1, 2].Value = "SECTOR";
                    worksheet.Cells[1, 3].Value = "DATE";
                    worksheet.Cells[1, 4].Value = "STATUS";
                    worksheet.Cells[1, 5].Value = "SCRIP";
                    worksheet.Cells[1, 6].Value = "SYMBOL";
                    worksheet.Cells[1, 7].Value = "LDCP";
                    worksheet.Cells[1, 8].Value = "OPEN";
                    worksheet.Cells[1, 9].Value = "HIGH";
                    worksheet.Cells[1, 10].Value = "LOW";
                    worksheet.Cells[1, 11].Value = "CURRENT";
                    worksheet.Cells[1, 12].Value = "CHANGE";
                    worksheet.Cells[1, 13].Value = "VOLUME";

                    //Add some items...
                    int countFlag = 1;
                    int total = 1;
                    for (int index = 0; index < _scrip.Length; index++)
                    {
                        total = index + 2;

                        worksheet.Cells["A" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["A" + total].Value =  countFlag++;
                        worksheet.Cells["B" + total].Value = _scrip[index].SECTOR;
                        worksheet.Cells["C" + total].Value = _scrip[index].DATE;
                        worksheet.Cells["D" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["D" + total].Value = _scrip[index].STATUS;
                        worksheet.Cells["E" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        worksheet.Cells["E" + total].Value = _scrip[index].SCRIP;
                        worksheet.Cells["F" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        worksheet.Cells["F" + total].Value = _scrip[index].SYMBOL;
                        worksheet.Cells["G" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["G" + total].Value = _scrip[index].LDCP;
                        worksheet.Cells["H" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["H" + total].Value = _scrip[index].OPEN;
                        worksheet.Cells["I" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["I" + total].Value = _scrip[index].HIGH;
                        worksheet.Cells["J" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["J" + total].Value = _scrip[index].LOW;
                        worksheet.Cells["K" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["K" + total].Value = _scrip[index].CURRENT;
                        worksheet.Cells["L" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["L" + total].Value = _scrip[index].CHANGE;
                        worksheet.Cells["M" + total].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells["M" + total].Value = _scrip[index].VOLUME;
                    }

                    //EndTest

                    //Ok now format the values;
                    using (var range = worksheet.Cells[1, 1, 1, 13])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.DarkBlue);
                        range.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    }

                    //Create an autofilter for the range
                    worksheet.Cells["A1:M4"].AutoFilter = true;

                    worksheet.Cells["A2:A2"].Style.Numberformat.Format = "@";   //Format as text
                    worksheet.Cells["C2:C3000"].Style.Numberformat.Format = "HH:mm am/pm MMMM dd, yyyy";

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
                    package.Workbook.Properties.Title = "Pakistan Stock Exchange Market Summary";
                    package.Workbook.Properties.Author = "Saad Ahmed";
                    package.Workbook.Properties.Comments = "This is the Pakistan Stock Exchange Market Summary Export File.";

                    // Set some extended property values
                    package.Workbook.Properties.Company = "EPPlus Software AB";

                    // Set some custom property values
                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", "Saad Ahmed");
                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", "EPPlus");

                    //var xlFile = FileOutputUtil.GetFileInfo("01-GettingStarted.xlsx");
                    string path = "PSX_MarketSummary_"+DateTime.Now.Year+DateTime.Now.Month+DateTime.Now.Day.ToString("00")+".xlsx";
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
                xlFile.Dispose();
                package.Dispose();
            }
            Debug.WriteLine("Excel Sheet Created.");
            Thread.Sleep(2000);
            if (File.Exists("PSX_MarketSummary_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day.ToString("00") + ".xlsx"))
            {
                try
                {
                    //using (Stream stream = new FileStream("FundMarketSummary.xlsx", FileMode.Open))
                    //{
                    // File/Stream manipulating code here
                    Process p = new Process();
                    p.StartInfo.UseShellExecute = true;
                    p.StartInfo.FileName = "PSX_MarketSummary_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day.ToString("00") + ".xlsx";
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

    }
}
