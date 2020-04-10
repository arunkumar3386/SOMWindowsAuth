using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Web.Security;


namespace StarMonthAuth
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void WindowsAuthentication_OnAuthenticate(object sender, WindowsAuthenticationEventArgs e)
        {
            // ensure we have a name and made it through authentication
            if (e.Identity != null && e.Identity.IsAuthenticated)
            {
                try
                {
                    var username = e.Identity.Name;
                    username = username.Replace("\\", "|");
                    if (username.Contains("|"))
                    {
                        username = username.Split('|')[1];
                    }
                    username = username.ToLower();
                    var rolesLst = new List<string> { "0" };
                    GenericPrincipal principal = new GenericPrincipal(e.Identity, rolesLst.ToArray());
                    Thread.CurrentPrincipal = HttpContext.Current.User = principal;
                }
                catch (Exception ex)
                {
                    //throw ex;
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
