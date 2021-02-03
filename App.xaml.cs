using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PSXDataFetchingApp.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace PSXDataFetchingApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public ServiceProvider serviceProvider;
        public void ConfigureServices(IServiceCollection services)
        => services.AddDbContext<DataContext>();
        public App()
        {
            ShutdownMode = ShutdownMode.OnMainWindowClose;
            //CreateHostBuilder(args).Build().Run();
            //BuildWebHost(args).Run();
            ServiceCollection services = new ServiceCollection();
            services.AddDbContext<DataContext>( option => { 
                if(ConfigurationManager.AppSettings["DatabaseVendor"].Equals("MSSQLSERVER"))
                {
                    option.UseSqlServer(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                }
                else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("ORACLE"))
                {
                    //option.UseOracle(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                }
                else if(ConfigurationManager.AppSettings["DatabaseVendor"].Equals("SQLITE"))
                {
                    option.UseSqlite(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                }
                else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("POSTGRE"))
                {
                    option.UseNpgsql(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                }
            });
            DataContext ctx = new DataContext();
            SeedData.SeedDatabase(ctx);
            services.AddSingleton<MainWindow>();
            serviceProvider = services.BuildServiceProvider();
        }



        // EF Core uses this method at design time to access the DbContext
        //public static IHostBuilder CreateHostBuilder(string[] args)
        //    => Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(
        //            webBuilder => webBuilder.UseStartup<Startup>());

        private void OnStartup(object s, StartupEventArgs e)
        {
            var mainWindow = serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }

        

    }
}
