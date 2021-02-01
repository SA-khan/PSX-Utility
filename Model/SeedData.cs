using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace PSXDataFetchingApp.Model
{
    public class SeedData
    {
        public static void SeedDatabase(DataContext context)
        {
            if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("MSSQLSERVER")) { }
            else if (ConfigurationManager.AppSettings["DatabaseVendor"].Equals("ORACLE")) { }
            else
            {
                context.Database.Migrate();
                if (context.ScripInfo.Count() == 0)
                {
                    context.ScripInfo.AddRange(
                        new ScripInfo
                        {
                            ScripInfoId = -1,
                            Number = 021490,
                            Symbol = "DOMF",
                            Name = "Dominion Stock Fund Limited",
                            CategoryId = 0806,
                            CategoryName = "CLOSE - END MUTUAL FUND",
                            Code = 5000000
                        });
                    context.SaveChanges();
                }
            }
        }
    }
}
