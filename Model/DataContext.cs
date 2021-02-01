using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using PSXDataFetchingApp.Model.BindingTargets;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Diagnostics;
using System.IO.Compression;
using System.IO;
using System.Net;
using HtmlAgilityPack;

namespace PSXDataFetchingApp.Model
{
    public class DataContext: DbContext
    {
        public List<ScripInfo> _tempItems = new List<ScripInfo>();
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> opts) : base(opts)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("MSSQLSERVER"))
                //{
                //    optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                //}
                //else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("ORACLE"))
                //{
                //    optionsBuilder.UseOracle(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                //}
                //else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("SQLITE"))
                //{
                //    optionsBuilder.UseSqlite("data source= PSXDataWarehouse.db");
                //}
                //else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("POSTGRE"))
                //{
                //    optionsBuilder.UseNpgsql(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                //}
                //optionsBuilder.UseSqlite("data source= PSXDataWarehouse.db");
                //optionsBuilder.UseOracle("datasource=db32;username=IPAMS_TEST;password=IPAMS_TEST;");
                //optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ScripInfo>().HasData(GetScripData());
            base.OnModelCreating(modelBuilder);
        }

        private ScripInfo[] GetScripData()
        {
            return new ScripInfo[]
            {
                new ScripInfo { ScripInfoId = 1, Name = "IBL HealthCare Limited.", Symbol = "IBLHL"},
                new ScripInfo { ScripInfoId = 2, Name = "Macter International Limited.", Symbol = "MACTER"},
                new ScripInfo { ScripInfoId = 3, Name = "The Searle Company Ltd.", Symbol = "SEARL"},
                new ScripInfo { ScripInfoId = 4, Name = "Wyeth Pakistan Limited.", Symbol = "WYETH"},
                new ScripInfo { ScripInfoId = 5, Name = "Altern Energy Ltd.", Symbol = "ALTN"},
                new ScripInfo { ScripInfoId = 6, Name = "Engro Powergen Qadirpur Ltd.", Symbol = "EPQL"},
                new ScripInfo { ScripInfoId = 7, Name = "Hub Power Company Limited.", Symbol = "HUBC"},
                new ScripInfo { ScripInfoId = 8, Name = "K-Electric Limited.", Symbol = "KEL"},
                new ScripInfo { ScripInfoId = 9, Name = "Oil & Gas Development Company Ltd.", Symbol = "OGDC"},
                new ScripInfo { ScripInfoId = 10, Name = "Lucky Cement Limited.", Symbol = "LUCK"},
                new ScripInfo { ScripInfoId = 11, Name = "AGRITECH LIMITED", Symbol = "AGL"},
                new ScripInfo { ScripInfoId = 12, Name = "Allied Bank Ltd.", Symbol = "ABL"},
                new ScripInfo { ScripInfoId = 13, Name = "FAUJI FERTILIZER BIN QASIM", Symbol = "FFBL"},
                new ScripInfo { ScripInfoId = 14, Name = "Zephyr Textile Limited.", Symbol = "ZTL"}
            };
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
            catch (Exception)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            HtmlNodeCollection result = doc.DocumentNode.SelectNodes(param);
            return result;
        }

        public ScripInfo[] GetFileUploadPSXMarketSummary(int _year, int _month, int _day)
        {
            ScripInfo[] items = new ScripInfo[500];
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
                    catch (Exception)
                    {
                        //MessageBox.Show("File is being used by another process.\n" + ex.Message, "PSX File Is Being Used", MessageBoxButton.OK, MessageBoxImage.Information);
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

                            //ClearMarketSummaryClosing();

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
                                Int64 lCategoryId = 0;
                                //Int64 lCategoryId = Convert.ToInt64(data[3].ToString());
                                string lCategoryName = "";
                                //string lCategoryName = data[4].ToString();
                                Int64 lCode = 0;
                                //Int64 lCode = Convert.ToInt64(data[5]);
                                ScripInfo Item = new ScripInfo { Number = lNumber, Symbol = lSymbol, Name = lName, CategoryId = lCategoryId, CategoryName = lCategoryName, Code = lCode };

                                ///
                                /// Adding Record to ListView
                                ///

                                items[counter] = Item;

                                ///
                                /// Saving Record
                                ///

                                int _DbStatus = SavingMarketSummaryClosing(Item);

                                if (_DbStatus == 0)
                                {
                                    Debug.WriteLine("Database Insertion Status: " + _DbStatus + "\n At Name: " + Item.Name);
                                }

                            }
                        }

                    }
                    else
                    {
                        //MessageBox.Show("No Market Summary File Exist. Please check Pakistan Stock Exchange Site for more information.", "PSX File Not Found", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }


            }

            else
            {
                //MessageBox.Show("Files and Folders Are Not Deleted.\n Please Delete Market Summary.rar and PSXDownload Folder from Root Directory.", "PSX File Not Found", MessageBoxButton.OK, MessageBoxImage.Error);
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

        public int SavingMarketSummaryClosing(ScripInfo item)
        {
            //SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            //builder.DataSource = "PSXDataWarehouse.db";
            //builder.InitialCatalog = "pchr42563";
            //builder.IntegratedSecurity = true;
            //_context = new DataContext(builder.ConnectionString);
            ScripInfo.Add(item);
            SaveChanges();
            return 1;
        }

        public DbSet<Status> Status { get; set; }
        public DbSet<ScripInfo> ScripInfo { get; set; }
        public DbSet<FundInfo> FundInfo { get; set; }
        public DbSet<CurrentMarketSummary> CurrentMarketSummary { get; set; }
        public DbSet<FundwiseMarketSummary> FundwiseMarketSummary { get; set; }
        public DbSet<FundwiseBucketMarketSummary> FundwiseBucketMarketSummary { get; set; }
        public DbSet<ClosingMarketSummary> ClosingMarketSummary { get; set; }
        public DbSet<MufapMarketSummary> MufapMarketSummary { get; set; }
        public DbSet<Pkrv> Pkrv { get; set; }
        public DbSet<Pkfrv> Pkfrv { get; set; }

    }
}
