using System.Data.Entity.Migrations;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Zabronim.Net.Migrations;
using Zabronim.Net.ZaEnviroment;

namespace Zabronim.Net {
    public class MvcApplication : HttpApplication {
        public static readonly ZLogger Logger = new ZLogger();

        protected void Application_Start() {
            InitMembersDb();

            ZLogger.Info("Application Start");

            new DbMigrator(new Configuration()).Update();

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void InitMembersDb() {
            string[] users = { "Sardar", "Admin" };
            string[] emails = { "prosardar@ya.ru", "admin@zabronim.ru" };
            string[] passwords = { "ecilA-123", "yeh327h2d-wd32" };
            string[] roles = { "Super Users", "Registered Users", "Users" };
            
            for (int i = 0; i < emails.Length; i++) {
                if (Membership.GetUserNameByEmail(emails[i]) != null) {
                    Membership.DeleteUser(users[i], true);
                }

                Membership.CreateUser(users[i], passwords[i], emails[i]);
            }

            for (int i = 0; i < roles.Length; i++) {
                if (Roles.RoleExists(roles[i])) {
                    foreach (string user in Roles.GetUsersInRole(roles[i])) {
                        Roles.RemoveUserFromRole(user, roles[i]);
                    }

                    Roles.DeleteRole(roles[i]);
                }

                Roles.CreateRole(roles[i]);

                var usernames = new string[users.Length];

                for (int j = 0; j < usernames.Length; j++) {
                    usernames[j] = users[j];
                }

                Roles.AddUsersToRole(usernames, roles[i]);
            }
        }
    }
}