using HyperSlackers.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace HyperSlackers.Localization.Demo
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // HS:
            // use the convention-based model metadata provider
            var metadataProvider = new ConventionModelMetadataProvider(false, // Attribute not required on models
                typeof(Localized.Models), // Models resource file has highest priority for searching
                typeof(Localized.Enums), 
                typeof(Localized.Validation)); // lowest priority

            // default is false, so searching for resources searches all resource files for most specific name,
            //   then moves to next most specific name
            // setting to true will search first resource file for all generated names before moving to next resource file
            //metadataProvider.ResourceTypeHasPriority = false; 
            ModelMetadataProviders.Current = metadataProvider;
        }
    }
}
