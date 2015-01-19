using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using ServiceStack.CacheAccess;
using ServiceStack.CacheAccess.Providers;
using ServiceStack.Configuration;
using ServiceStack.OrmLite;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.WebHost.Endpoints;

namespace WebApplication4
{
    public class Global : System.Web.HttpApplication
    {
        public class HelloAppHost : AppHostBase
        {
            public HelloAppHost() : base("Hello Web Services", typeof (HelloService).Assembly)
            {
            }

            public override void Configure(Funq.Container container)
            {
                //throw new NotImplementedException();
                //Plugins.Add(new AuthFeature( () => new AuthUserSession(), 
                //    new IAuthProvider[] {new BasicAuthProvider()}));
                Plugins.Add(new AuthFeature(() => new AuthUserSession(),
                    new IAuthProvider[] { new BasicAuthProvider(), new TwitterAuthProvider(new AppSettings())}));
                Plugins.Add(new RegistrationFeature());

                container.Register<ICacheClient>(new MemoryCacheClient());
                var userStore = new InMemoryAuthRepository();
                container.Register<IUserAuthRepository>(userStore);
                var dbConnectionFactory =
                    new OrmLiteConnectionFactory(HttpContext.Current.Server.MapPath("~/App_Data/data.txt"),
                        SqliteDialect.Provider);
                container.Register<IDbConnectionFactory>(dbConnectionFactory);
                string hash;
                string salt;
                new SaltedHash().GetHashAndSaltString("pass123", out hash, out salt);
                userStore.CreateUserAuth(new UserAuth()
                {
                    Id = 1,
                    DisplayName = "Mary",
                    Email = "mary@blow.com",
                    UserName = "muser",
                    FirstName = "Mary",
                    LastName = "Blow",
                    PasswordHash = hash,
                    Salt = salt,
                    Roles = new List<string> { RoleNames.Admin }
                    //Permissions = new List<string> {"ViewCurrentStatus"}
                }, "pass123");
                container.RegisterAutoWired<MeasuredDataRepository>();
            }
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            new HelloAppHost().Init();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}