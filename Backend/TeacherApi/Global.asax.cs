using AppDbContext.Entities;
using TeacherApi.DataSeed;
using MultipartDataMediaFormatter;
using MultipartDataMediaFormatter.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace TeacherApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            OnlineCourseDb db = new OnlineCourseDb();
            DataSeeder seed = new DataSeeder(db);
            GlobalConfiguration.Configuration.Formatters.Add(new
                FormMultipartEncodedMediaTypeFormatter(new MultipartFormatterSettings()));
            seed.SeedDefaultCountry();
            seed.SeedLevels();
            IdentitySeed.createRolesandUsers();
        }
    }
}
