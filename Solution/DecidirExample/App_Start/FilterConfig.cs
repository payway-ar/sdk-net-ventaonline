using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace DecidirExample
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
