using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using CRUD.Models;

namespace CRUD
{
    public class Global : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            DataAccess.StrConnection = "Host=localhost;" +
            	                       "Port=5432;" +
            	                       "Username=postgres;" +
            	                       "Password=qwertypy;" +
            	                       "Database=Prueba;";
        }
    }
}
