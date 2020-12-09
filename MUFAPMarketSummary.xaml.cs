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

namespace PSXDataFetchingApp
{
    /// <summary>
    /// Interaction logic for MUFAPMarketSummary.xaml
    /// </summary>
    public partial class MUFAPMarketSummary : Window
    {
        List<String> _HeaderText = new List<string>();
        public static List<SpecificMUFAPFundSummary> __fundSummaryDetails = null;
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
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {

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

            txtStatus.Text = "Status: Processing";
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
            Debug.WriteLine(_selectedCategory);

            await Task.Run(() => __fundSummaryDetails = GetMUFAPFundSummaryData(_selectedCategory));

            foreach (SpecificMUFAPFundSummary item in __fundSummaryDetails)
            {
                list1.Items.Add(new SpecificMUFAPFundSummary { FundId = item.FundId, Name = item.Name, Category = item.Category, Rating = item.Rating, ValidityDate = item.ValidityDate, NAV = item.NAV, YTD = item.YTD, MTD = item.MTD, _1Day = item._1Day, _15Days = item._15Days, _30Days = item._30Days, _90Days = item._90Days, _180Days = item._180Days, _270Days = item._270Days, _365Days = item._365Days, TER = item.TER, MF = item.MF, SANDM = item.SANDM });
            }

            FundImage.Visibility = Visibility.Hidden;
            loadingImage.Visibility = Visibility.Hidden;
            list1.Visibility = Visibility.Visible;

            btnBack.IsEnabled = true;
            comboCategory.IsEnabled = true;
            btnGet.IsEnabled = true;
            btnSave.IsEnabled = true;
            btnReset.IsEnabled = true;
            txtSearch.IsEnabled = true;
            btnSearch.IsEnabled = true;

            txtStatus.Text = "Status: Ready";
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
        private List<SpecificMUFAPFundSummary> GetMUFAPFundSummaryData(string _selectcategory)
        {
            List<SpecificMUFAPFundSummary> response = new List<SpecificMUFAPFundSummary>();
            List<String> _data = getMUFAPMarketSummaryRates("");
            int _categoryFlag = 0;
            

            int y = 0;
            int counter = 0;
            bool _headerFlag = false;
            for (int i = 0; i < _data.Count; i++)
            {
                SpecificMUFAPFundSummary obj1 = new SpecificMUFAPFundSummary();
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
                            obj1.ValidityDate = _data[i + 2];
                            obj1.NAV = _data[i + 3];
                            obj1.YTD = _data[i + 4];
                            obj1.MTD = _data[i + 5];
                            obj1._1Day = _data[i + 6];
                            obj1._15Days = _data[i + 7];
                            obj1._30Days = _data[i + 8];
                            obj1._90Days = _data[i + 9];
                            obj1._180Days = _data[i + 10];
                            obj1._270Days = _data[i + 11];
                            obj1._365Days = _data[i + 12];
                            obj1.TER = _data[i + 13];
                            obj1.MF = _data[i + 14];
                            obj1.SANDM = _data[i + 15];
                        }
                        else
                        {
                            obj1.ValidityDate = _data[i + 1];
                            obj1.NAV = _data[i + 2];
                            obj1.YTD = _data[i + 3];
                            obj1.MTD = _data[i + 4];
                            obj1._1Day = _data[i + 5];
                            obj1._15Days = _data[i + 6];
                            obj1._30Days = _data[i + 7];
                            obj1._90Days = _data[i + 8];
                            obj1._180Days = _data[i + 9];
                            obj1._270Days = _data[i + 10];
                            obj1._365Days = _data[i + 11];
                            obj1.TER = _data[i + 12];
                            obj1.MF = _data[i + 13];
                            obj1.SANDM = _data[i + 14];
                        }
                        //Debug.WriteLine("=> "+obj1.FundId+": " + obj1.Name + ", Category: " + obj1.Category, ", Rating: "+ obj1.Rating + ", NAV: " + obj1.NAV + ", YTD: " + obj1.YTD + ", MTD: " + obj1.MTD + ", 1 Day: " + obj1._1Day + ", 15 Days: " + obj1._15Days + ", 30 Days: " + obj1._30Days + ", 90 Days: " + obj1._90Days + ", 180 Days: " + obj1._180Days + ", 270 Days: " + obj1._270Days + ", 365 Days: " + obj1._365Days + ", TER: " + obj1.TER + ", MF: " + obj1.MF + ", S&M" + obj1.SANDM + " .");
                        if (_selectcategory == "All Categories")
                        {
                            response.Add(new SpecificMUFAPFundSummary { FundId = ++counter, Name = obj1.Name, Category = obj1.Category, Rating = obj1.Rating == "" ? "-" : obj1.Rating, ValidityDate = obj1.ValidityDate, NAV = obj1.NAV, YTD = obj1.YTD, MTD = obj1.MTD, _1Day = obj1._1Day, _15Days = obj1._15Days, _30Days = obj1._30Days, _90Days = obj1._90Days, _180Days = obj1._180Days, _270Days = obj1._270Days, _365Days = obj1._365Days, TER = obj1.TER, MF = obj1.MF, SANDM = obj1.SANDM });
                        }
                        else if(obj1.Category == _selectcategory )
                        {
                            response.Add(new SpecificMUFAPFundSummary { FundId = ++counter, Name = obj1.Name, Category = obj1.Category, Rating = obj1.Rating == "" ? "-" : obj1.Rating, ValidityDate = obj1.ValidityDate, NAV = obj1.NAV, YTD = obj1.YTD, MTD = obj1.MTD, _1Day = obj1._1Day, _15Days = obj1._15Days, _30Days = obj1._30Days, _90Days = obj1._90Days, _180Days = obj1._180Days, _270Days = obj1._270Days, _365Days = obj1._365Days, TER = obj1.TER, MF = obj1.MF, SANDM = obj1.SANDM });
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
            HtmlNodeCollection result = doc.DocumentNode.SelectNodes(param);

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
                HtmlNodeCollection _nodes = FetchDataFromPSX(URL, "//td");
                
                int startCapture = 0;

                foreach (HtmlAgilityPack.HtmlNode node in _nodes)
                {
                    if (node.InnerText != null)
                    {
                        _originalText += node.InnerText.ToString()+"\n";
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
            list1.Items.Clear();
            string _searchKeyword = SearchTermTextBox.Text.ToString().ToLower();
            List<SpecificMUFAPFundSummary> _searchList = new List<SpecificMUFAPFundSummary>();
            var _selectedItem = from item in __fundSummaryDetails
                                where item.Name.ToLower().Contains(_searchKeyword) || item.Category.ToLower().Contains(_searchKeyword)
                                orderby item.Name
                                select item;
            _searchList = _selectedItem.ToList();
            int counter = 0;
            foreach (SpecificMUFAPFundSummary item in _selectedItem)
            {
                list1.Items.Add(new SpecificMUFAPFundSummary { FundId = ++counter, Name = item.Name, Category = item.Category, Rating = item.Rating, ValidityDate = item.ValidityDate, NAV = item.NAV, YTD = item.YTD, MTD = item.MTD, _1Day = item._1Day, _15Days = item._15Days, _30Days = item._30Days, _90Days = item._90Days, _180Days = item._180Days, _270Days = item._270Days, _365Days = item._365Days, TER = item.TER, MF = item.MF, SANDM = item.SANDM });
            }
        }

        private void SearchTermTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchTermTextBox.Text != null)
            {
                list1.Items.Clear();
                string _searchKeyword = SearchTermTextBox.Text.ToString().ToLower();
                List<SpecificMUFAPFundSummary> _searchList = new List<SpecificMUFAPFundSummary>();
                var _selectedItem = from item in __fundSummaryDetails
                                    where item.Name.ToLower().Contains(_searchKeyword) || item.Category.ToLower().Contains(_searchKeyword)
                                    orderby item.Name
                                    select item;
                _searchList = _selectedItem.ToList();
                int counter = 0;
                foreach (SpecificMUFAPFundSummary item in _selectedItem)
                {
                    list1.Items.Add(new SpecificMUFAPFundSummary { FundId = ++counter, Name = item.Name, Category = item.Category, Rating = item.Rating, ValidityDate = item.ValidityDate, NAV = item.NAV, YTD = item.YTD, MTD = item.MTD, _1Day = item._1Day, _15Days = item._15Days, _30Days = item._30Days, _90Days = item._90Days, _180Days = item._180Days, _270Days = item._270Days, _365Days = item._365Days, TER = item.TER, MF = item.MF, SANDM = item.SANDM });
                }
            }
        }
    }
}
