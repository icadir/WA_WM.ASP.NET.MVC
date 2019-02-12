using Admin.BLL.Identity;
using Admin.Models;
using Admin.Models.IdentityModels;
using Admin.Web.UI.App_Start;
using Microsoft.AspNet.Identity;
using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Admin.Web.UI.App_Code;

namespace Admin.Web.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            FiltersConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            var roller = Enum.GetNames(typeof(IdentityRoles));
            var roleManager = MembershipTools.NewRoleManager();
            foreach (var rol in roller)
            {
                if (!roleManager.RoleExists(rol))
                    roleManager.Create(new Role()
                    {
                        Name = rol
                    });
            }
        }
    }
}
