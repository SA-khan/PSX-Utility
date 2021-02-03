using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace PSXDataFetchingApp.Model
{
    internal static class ResourceAccessor
    {

        public static Uri Get(string resourcePath)
        {
            var uri = string.Format(
                "pack://application:,,,/{0};component/{1}"
                , Assembly.GetExecutingAssembly().GetName().Name
                , resourcePath
            );

            return new Uri(uri);
        }
    }
}
