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

namespace PSXDataFetchingApp
{
    /// <summary>
    /// Interaction logic for Configuration.xaml
    /// </summary>
    public partial class Configuration : Window
    {
        public Configuration()
        {
            InitializeComponent();

            // Status
            var imageStatus = new BitmapImage();
            imageStatus.BeginInit();
            imageStatus.UriSource = ResourceAccessor.Get("Images/tick.gif");
            imageStatus.EndInit();
            ImageBehavior.SetAnimatedSource(imgStatus, imageStatus);

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
            MainWindow window = new MainWindow();
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
                Debug.WriteLine("Closing Percentage " + _status);
            }
            else
            {
                config.AppSettings.Settings["PopupAlert"].Value = "0";
                _status = false;
                Debug.WriteLine("Closing Percentage " + _status);
            }

            

            // Save the configuration file.
            config.Save(ConfigurationSaveMode.Modified);

            

            // Force a reload of the changed section.
            ConfigurationManager.RefreshSection("appSettings");

            bool _what = ConfigurationManager.AppSettings["PopupAlert"].ToString() == "1" ? true : false;

            

            if ( _what == _status)
            {
                var bc = new BrushConverter();
                btnAlert.Background = (Brush)bc.ConvertFrom("#32CD32");
            }
            else
            {
                var bc = new BrushConverter();
                btnAlert.Background = (Brush)bc.ConvertFrom("#FF6347");
            }

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
                Debug.WriteLine("Closing Percentage " + _statusEmail);
            }
            else
            {
                configEmail.AppSettings.Settings["EmailAlert"].Value = "0";
                _statusEmail = false;
                Debug.WriteLine("Closing Percentage " + _statusEmail);
            }

            //
            configEmail.Save(ConfigurationSaveMode.Modified);

            bool _whatEmail = ConfigurationManager.AppSettings["EmailAlert"].ToString() == "1" ? true : false;

            if (_whatEmail == _statusEmail)
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
    }
}
