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
using WpfAnimatedGif;
using System.Linq;
using HtmlAgilityPack;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading.Tasks;

namespace PSXDataFetchingApp
{
    /// <summary>
    /// Interaction logic for Configuration.xaml
    /// </summary>
    public partial class Configuration : Window
    {
        string _Date = String.Empty;
        public DataContext _context;
        //
        public List<ScripInfo> _tempItems = new List<ScripInfo>();
        public Configuration(DataContext ctx)
        {
            InitializeComponent();

            _context = ctx;

            // Status
            var imageStatus = new BitmapImage();
            imageStatus.BeginInit();
            imageStatus.UriSource = ResourceAccessor.Get("Images/tick.gif");
            imageStatus.EndInit();
            ImageBehavior.SetAnimatedSource(imgStatus, imageStatus);

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
                        MainWindow1.Background = (Brush)bc.ConvertFrom("#323280");

                        //lblDemo.Foreground = (Brush)bc.ConvertFrom("#f0a500");

                        //Setting Logo
                        var image = new BitmapImage();
                        image.BeginInit();
                        image.UriSource = ResourceAccessor.Get("Images/BOP.gif");
                        image.EndInit();
                        ImageBehavior.SetAnimatedSource(ClientLogo, image);

                        //Header Background
                        Header.Background = (Brush)bc.ConvertFrom("#f0a500");

                        //Body Background
                        Body.Background = (Brush)bc.ConvertFrom("#ffde80");

                        //Status Bar Background
                        statusBar.Background = (Brush)bc.ConvertFrom("#f0a500");
                    }
                    else if (ConfigurationManager.AppSettings["Client"].Equals("HBL"))
                    {
                        // Header Background Color
                        var bc = new BrushConverter();
                        MainWindow1.Background = (Brush)bc.ConvertFrom("#323280");

                        //lblDemo.Foreground = (Brush)bc.ConvertFrom("#008269");

                        //lblSubHeading.Background = (Brush)bc.ConvertFrom("#008269");

                        //Setting Logo
                        var image = new BitmapImage();
                        image.BeginInit();
                        image.UriSource = ResourceAccessor.Get("Images/HBL.gif");
                        image.EndInit();
                        ImageBehavior.SetAnimatedSource(ClientLogo, image);

                        //Header Background
                        Header.Background = (Brush)bc.ConvertFrom("#008269");

                        //Body Background
                        Body.Background = (Brush)bc.ConvertFrom("#cdcdcd");

                        //Status Bar Background
                        statusBar.Background = (Brush)bc.ConvertFrom("#008269");

                    }

                }
            }
            catch { }

            txtClient.Text = ConfigurationManager.AppSettings["Client"].ToString();
            txtPrimaryConnection.Text = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            txtSecondaryConnection.Text = ConfigurationManager.ConnectionStrings["IpamsConnection"].ToString();
            if(ConfigurationManager.AppSettings["PopupAlert"].ToString() == "1")
            {
                checkAlert.IsChecked = true;
            }
            else
            {
                checkAlert.IsChecked = false;
            }
            if (ConfigurationManager.AppSettings["EmailAlert"].ToString() == "1")
            {
                checkEmail.IsChecked = true;
            }
            else
            {
                checkEmail.IsChecked = false;
            }
            txtClosingPercentage.Text = ConfigurationManager.AppSettings["FundClosingPercentage"].ToString();

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow(_context);
            window.Show();
            this.Hide();
        }

        private void btnClient_Click(object sender, RoutedEventArgs e)
        {
            string ClientSymbol = txtClient.Text;
            ConfigurationManager.AppSettings["Client"] = ClientSymbol;

            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            // Update the setting.
            config.AppSettings.Settings["Client"].Value = ClientSymbol;

            // Save the configuration file.
            config.Save(ConfigurationSaveMode.Modified);

            // Force a reload of the changed section.
            ConfigurationManager.RefreshSection("appSettings");

            if (ConfigurationManager.AppSettings["Client"] == ClientSymbol)
            {
                var bc = new BrushConverter();
                btnClient.Background = (Brush)bc.ConvertFrom("#32CD32");
            }
            else
            {
                var bc = new BrushConverter();
                btnClient.Background = (Brush)bc.ConvertFrom("#FF6347");
            }

            
        }

        private void btnPrimaryConnection_Click(object sender, RoutedEventArgs e)
        {
            string PrimaryConnection = txtPrimaryConnection.Text;

            //System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //config.ConnectionStrings.ConnectionStrings.Remove(new ConnectionStringSettings("DefaultConnection", PrimaryConnection));
            //config.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings("DefaultConnection", PrimaryConnection));
            //config.Save(ConfigurationSaveMode.Modified, true);
            //ConfigurationManager.RefreshSection("DefaultConnection");

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
            connectionStringsSection.ConnectionStrings["DefaultConnection"].ConnectionString = PrimaryConnection;
            config.Save();
            ConfigurationManager.RefreshSection("connectionStrings");

            if (ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString() == PrimaryConnection)
            {
                var bc = new BrushConverter();
                btnPrimaryConnection.Background = (Brush)bc.ConvertFrom("#32CD32");
            }
            else
            {
                var bc = new BrushConverter();
                btnPrimaryConnection.Background = (Brush)bc.ConvertFrom("#FF6347");
            }

        }

        private void btnSecondaryConnection_Click(object sender, RoutedEventArgs e)
        {

            string SecondaryConnection = txtSecondaryConnection.Text;

            //System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //config.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings("IpamsConnection", SecondaryConnection));
            //config.Save(ConfigurationSaveMode.Modified, true);
            //ConfigurationManager.RefreshSection("IpamsConnection");

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
            connectionStringsSection.ConnectionStrings["IpamsConnection"].ConnectionString = SecondaryConnection;
            config.Save();
            ConfigurationManager.RefreshSection("connectionStrings");

            if (ConfigurationManager.ConnectionStrings["IpamsConnection"].ToString() == SecondaryConnection)
            {
                var bc = new BrushConverter();
                btnSecondaryConnection.Background = (Brush)bc.ConvertFrom("#32CD32");
            }
            else
            {
                var bc = new BrushConverter();
                btnSecondaryConnection.Background = (Brush)bc.ConvertFrom("#FF6347");
            }

        }

        private void btnCompanyCheck_Click(object sender, RoutedEventArgs e)
        {
            string _companyName = txtCompanyName.Text;
            string _symbol = String.Empty;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            try
            {

                conn.Open();
                // 1.  create a command object identifying the stored procedure
                SqlCommand cmd = new SqlCommand("spGetSymbolFromCompanyName", conn);

                // 2. set the command object so it knows to execute a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                // 3. add parameter to command, which will be passed to the stored procedure
                cmd.Parameters.Add(new SqlParameter("@CompanyName", _companyName));

                // execute the command
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    // iterate through results, printing each to console
                    while (rdr.Read())
                    {
                        _symbol = rdr["C_SYMBOL"].ToString();
                    }
                }

                lblCompanyStatus.Text = _symbol;

                if(_symbol != "Nil")
                {
                    var bc = new BrushConverter();
                    btnCompanyCheck.Background = (Brush)bc.ConvertFrom("#32CD32");
                }
                else
                {
                    var bc = new BrushConverter();
                    btnCompanyCheck.Background = (Brush)bc.ConvertFrom("#FF6347");
                }


            }

            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Database Connectivity Problem", MessageBoxButton.OK, MessageBoxImage.Error);
                Debug.WriteLine("Database Error: " + ex.Message);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "General Problem", MessageBoxButton.OK, MessageBoxImage.Error);
                Debug.WriteLine("Error: " + ex.Message);
            }

            finally
            {
                conn.Close();
            }
        }

        private void btnInsertCompany_Click(object sender, RoutedEventArgs e)
        {
            string _symbol = txtID.Text;
            string _companyName = txtName.Text;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            try
            {

                conn.Open();
                // 1.  create a command object identifying the stored procedure
                SqlCommand cmd = new SqlCommand("spInsertCompany", conn);

                // 2. set the command object so it knows to execute a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                // 3. add parameter to command, which will be passed to the stored procedure
                cmd.Parameters.Add(new SqlParameter("@SYMBOL", _symbol));
                cmd.Parameters.Add(new SqlParameter("@NAME", _companyName));

                // execute the command
                int queryStatus = cmd.ExecuteNonQuery();

                if (queryStatus == 1)
                {
                    var bc = new BrushConverter();
                    btnInsertCompany.Background = (Brush)bc.ConvertFrom("#32CD32");
                }
                else
                {
                    var bc = new BrushConverter();
                    btnInsertCompany.Background = (Brush)bc.ConvertFrom("#FF6347");
                }


            }

            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Database Connectivity Problem", MessageBoxButton.OK, MessageBoxImage.Error);
                Debug.WriteLine("Database Error: " + ex.Message);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "General Problem", MessageBoxButton.OK, MessageBoxImage.Error);
                Debug.WriteLine("Error: " + ex.Message);
            }

            finally
            {
                conn.Close();
            }
        }

        private void btnCompanySymbolCheckSecondary_Click(object sender, RoutedEventArgs e)
        {
            string _companyName = txtCompanyNameSecondary.Text;
            string _symbol = String.Empty;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["IpamsConnection"].ConnectionString;
            try
            {

                conn.Open();
                // 1.  create a command object identifying the stored procedure
                SqlCommand cmd = new SqlCommand("spGetShareSymbolByName", conn);

                // 2. set the command object so it knows to execute a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                // 3. add parameter to command, which will be passed to the stored procedure
                cmd.Parameters.Add(new SqlParameter("@NAME", _companyName));

                // execute the command
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    // iterate through results, printing each to console
                    while (rdr.Read())
                    {
                        _symbol = rdr["C_SYMBOL"].ToString();
                    }
                }

                lblCompanySymbolCheckSecondary.Text = _symbol;

                if (_symbol != "")
                {
                    var bc = new BrushConverter();
                    btnCompanySymbolCheckSecondary.Background = (Brush)bc.ConvertFrom("#32CD32");
                }
                else
                {
                    var bc = new BrushConverter();
                    btnCompanySymbolCheckSecondary.Background = (Brush)bc.ConvertFrom("#FF6347");
                }


            }

            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Database Connectivity Problem", MessageBoxButton.OK, MessageBoxImage.Error);
                Debug.WriteLine("Database Error: " + ex.Message);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "General Problem", MessageBoxButton.OK, MessageBoxImage.Error);
                Debug.WriteLine("Error: " + ex.Message);
            }

            finally
            {
                conn.Close();
            }
        }

        private void btnAlert_Click(object sender, RoutedEventArgs e)
        {
            bool _status = false;
            
            if (checkAlert.IsChecked == true) {
                ConfigurationManager.AppSettings["PopupAlert"] = "1";
                _status = true;
            }
            else { 
                ConfigurationManager.AppSettings["PopupAlert"] = "0";
                _status = false;
            }

            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            
            // Update the setting.
            if (checkAlert.IsChecked == true)
            {
                config.AppSettings.Settings["PopupAlert"].Value = "1";
                _status = true;
                Debug.WriteLine("Popup Alert " + _status);
            }
            else
            {
                config.AppSettings.Settings["PopupAlert"].Value = "0";
                _status = false;
                Debug.WriteLine("Popup Alert " + _status);
            }

            

            // Save the configuration file.
            config.Save(ConfigurationSaveMode.Modified);

            

            // Force a reload of the changed section.
            ConfigurationManager.RefreshSection("appSettings");

            bool _what = ConfigurationManager.AppSettings["PopupAlert"].ToString() == "1" ? true : false;

            

            //if ( _what == _status)
            //{
            //    var bc = new BrushConverter();
            //    btnAlert.Background = (Brush)bc.ConvertFrom("#32CD32");
            //}
            //else
            //{
            //    var bc = new BrushConverter();
            //    btnAlert.Background = (Brush)bc.ConvertFrom("#FF6347");
            //}

            //
            bool _statusEmail = false;

            System.Configuration.Configuration configEmail = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (checkEmail.IsChecked == true)
            {
                ConfigurationManager.AppSettings["EmailAlert"] = "1";
                _statusEmail = true;
            }
            else
            {
                ConfigurationManager.AppSettings["EmailAlert"] = "0";
                _statusEmail = false;
            }

            if (checkEmail.IsChecked == true)
            {
                configEmail.AppSettings.Settings["EmailAlert"].Value = "1";
                _statusEmail = true;
                Debug.WriteLine("Email Alert " + _statusEmail);
            }
            else
            {
                configEmail.AppSettings.Settings["EmailAlert"].Value = "0";
                _statusEmail = false;
                Debug.WriteLine("Email Alert " + _statusEmail);
            }

            //
            configEmail.Save(ConfigurationSaveMode.Modified);

            bool _whatEmail = ConfigurationManager.AppSettings["EmailAlert"].ToString() == "1" ? true : false;

            if (_whatEmail == _statusEmail && _what == _status)
            {
                var bc = new BrushConverter();
                btnAlert.Background = (Brush)bc.ConvertFrom("#32CD32");
            }
            else
            {
                var bc = new BrushConverter();
                btnAlert.Background = (Brush)bc.ConvertFrom("#FF6347");
            }

        }

        private void btnClosingPercentage_Click(object sender, RoutedEventArgs e)
        {
            string _percentage = txtClosingPercentage.Text;
            Debug.WriteLine("Closing Percentage " + _percentage);

            //ConfigurationManager.AppSettings["FundClosingPercentage"].Value = _percentage;


            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            // Update the setting.
            config.AppSettings.Settings["FundClosingPercentage"].Value = _percentage;

            // Save the configuration file.
            config.Save(ConfigurationSaveMode.Modified);

            // Force a reload of the changed section.
            ConfigurationManager.RefreshSection("appSettings");

            if (ConfigurationManager.AppSettings["FundClosingPercentage"] == _percentage)
            {
                var bc = new BrushConverter();
                btnClosingPercentage.Background = (Brush)bc.ConvertFrom("#32CD32");
            }
            else
            {
                var bc = new BrushConverter();
                btnClosingPercentage.Background = (Brush)bc.ConvertFrom("#FF6347");
            }
        }

        private async void btnUploadScripSymbols_Click(object sender, RoutedEventArgs e)
        {
            lblStatus.Text = "Status: Processing";
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = ResourceAccessor.Get("Images/processing.gif");
            image.EndInit();
            ImageBehavior.SetAnimatedSource(imgStatus, image);

            try
            {

                int _year = pkrvDatepicker.SelectedDate.Value.Year;
                int _month = pkrvDatepicker.SelectedDate.Value.Month;
                int _day = pkrvDatepicker.SelectedDate.Value.Day;
                if (_year != 0 && _month != 0 && _day != 0)
                {
                    //list1.Items.Clear();
                    _Date = String.Empty;
                    List<ScripInfo> _summaryData = new List<ScripInfo>();
                    try
                    {
                        await Task.Run(() => _summaryData = GetFileUploadPSXMarketSummary(_year, _month, _day));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("There is no file exist on PSX Site.\n" + ex.Message, "File Not Found", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                    }
                    if (_Date != String.Empty)
                    {
                        //txtDate.Text = "Date: " + Convert.ToDateTime(_Date).ToString("dddd, dd MMMM yyyy");
                        var _selectedItem = from item in _summaryData
                                            orderby item.Name
                                            select item;
                        _summaryData = _selectedItem.ToList();
                        //int counter = 0;
                        for (int i = 0; i < _summaryData.Count; i++)
                        {
                            //list1.Items.Add(new FundMarket { SERIAL = ++counter, NAME = _summaryData[i].Name, SYMBOL = _summaryData[i].Symbol, LDCP = _summaryData[i].LDCP, OPEN = _summaryData[i].OPEN, HIGH = _summaryData[i].HIGH, LOW = _summaryData[i].LOW, CHANGE = _summaryData[i].Change.ToString(), VOLUME = _summaryData[i].Volume });
                            Debug.WriteLine("Name: " + _summaryData[i].Name);
                        }
                        _tempItems = _summaryData;
                    }
                    else
                    {
                    }

                }
                else
                {
                    MessageBox.Show("Please select a valid date to fetch market summary closing data.", "Invalid Selection", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please select a valid date.\nDetails: " + ex.Message, "Invalid Selection", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            lblStatus.Text = "Status: Ready";
            var image2 = new BitmapImage();
            image2.BeginInit();
            image2.UriSource = ResourceAccessor.Get("Images/tick.gif");
            image2.EndInit();
            ImageBehavior.SetAnimatedSource(imgStatus, image2);
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

        public List<ScripInfo> GetFileUploadPSXMarketSummary(int _year, int _month, int _day)
        {
            List<ScripInfo> items = new List<ScripInfo>();
            string _fileName = "symbolname.rar";
            string _folderName = "PSXDownloadDirectory";
            string _unzipLisFile = _folderName + "\\symbolname.lis";
            if (File.Exists(_fileName))
            {
                File.Delete(_fileName);
            }
            if (Directory.Exists(_folderName))
            {
                if (File.Exists(_unzipLisFile))
                {
                    File.Delete(_unzipLisFile);
                }
                System.Threading.Thread.Sleep(1000);
                Directory.Delete(_folderName);
            }
            if (!File.Exists(_fileName) && !Directory.Exists(_folderName) && !File.Exists(_unzipLisFile))
            {
                using (WebClient wc = new WebClient())
                {
                    wc.DownloadFile(
                    new System.Uri("https://dps.psx.com.pk/download/symbol_name/" + _year + "-" + _month + "-" + _day + ".zip"),
                        _fileName
                    );
                }
                if (File.Exists(_fileName))
                {
                    bool boolUnzip = false;
                    try
                    {
                        boolUnzip = UnzipPSXFile(_fileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("File is being used by another process.\n" + ex.Message, "PSX File Is Being Used", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                    ///
                    /// If Condition to Check Whether File is successfull unzip and ready to be read.
                    ///

                    if (boolUnzip)
                    {
                        using (var reader = new StreamReader(_unzipLisFile))
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

                            ClearSymbolInfo();

                            ///
                            /// Iterating Each Line of Market Summary Closing File
                            ///

                            while ((line = reader.ReadLine()) != null)
                            {

                                ///
                                /// Incrementing Counter to Show On List View Serial Number
                                ///

                                counter++;

                                ///
                                /// Splitting Each Line Into Variables
                                ///

                                string[] data = line.Split("|");

                                ///
                                /// Saving Date Value to Global Variable
                                ///

                                //_Date = data[0];

                                ///
                                /// Getting Values from File
                                ///
                                Int64 lNumber = 0;
                                //Int64 lNumber = Convert.ToInt64(data[0]);
                                string lSymbol = data[0].ToString();
                                string lName = data[1].ToString();
                                string lDescription = data[2].ToString();
                                Int64 lCategoryId = 0;
                                //Int64 lCategoryId = Convert.ToInt64(data[3].ToString());
                                string lCategoryName = "";
                                //string lCategoryName = data[4].ToString();
                                Int64 lCode = 0;
                                //Int64 lCode = Convert.ToInt64(data[5]);
                                ScripInfo Item = new ScripInfo();
                                if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("MSSQLSERVER")) {
                                    Item = new ScripInfo { Symbol = lSymbol, Name = lName, Description = lDescription };
                                }
                                else {
                                    Item = new ScripInfo { Number = lNumber, Symbol = lSymbol, Name = lName, Description = lDescription, CategoryId = lCategoryId, CategoryName = lCategoryName, Code = lCode };
                                }
                                ///
                                /// Adding Record to ListView
                                ///

                                items.Add(Item);

                                ///
                                /// Saving Record
                                ///

                                int _DbStatus = SavingSymbolInformation(Item);

                                if (_DbStatus == 0)
                                {
                                    Debug.WriteLine("Database Insertion Status: " + _DbStatus + "\n At Name: " + Item.Name);
                                }

                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show("No Market Summary File Exist. Please check Pakistan Stock Exchange Site for more information.", "PSX File Not Found", MessageBoxButton.OK, MessageBoxImage.Information);
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
            if (Directory.Exists(_folderName))
            {
                if (File.Exists(_unzipLisFile))
                {
                    File.Delete(_unzipLisFile);
                }
                Directory.Delete(_folderName);
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
                    za.ExtractToDirectory("PSXDownloadDirectory/");
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

        private int ClearSymbolInfo()
        {
            int status = 0;
            if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("MSSQLSERVER")) {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                conn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("spTRUNCATE_SYMBOL_INFO", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    status = cmd.ExecuteNonQuery();
                    return status;
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
            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("SQLITE"))
            {
                //_context.ScripInfo.Remove();
                foreach (var entity in _context.ScripInfo)
                    _context.ScripInfo.Remove(entity);
                _context.SaveChanges();
            }

            return status;


        }

        public int SavingSymbolInformation(ScripInfo item)
        {
            int status = 0;
            if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("MSSQLSERVER"))
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                conn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("spINSERT_SYMBOL_INFO", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@SYMBOL_MARK", SqlDbType.VarChar, 30);
                    cmd.Parameters["@SYMBOL_MARK"].Value = item.Symbol;
                    cmd.Parameters.Add("@SYMBOL_NAME", SqlDbType.VarChar, 500);
                    cmd.Parameters["@SYMBOL_NAME"].Value = item.Name;
                    cmd.Parameters.Add("@SYMBOL_DESCRIPTION", SqlDbType.VarChar, -1);
                    cmd.Parameters["@SYMBOL_DESCRIPTION"].Value = item.Description;
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
                    MessageBox.Show(ex.Message, "Database Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Debug.WriteLine("General Exception: " + ex.Message);
                    return 0;
                }
                finally
                {
                    conn.Close();
                }
            }
            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("SQLITE")) {
                _context.ScripInfo.Add(item);
                _context.SaveChanges();
                status = 1;
            }
            return status;
        }


    }
}
