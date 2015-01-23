using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ServiceStack.CacheAccess;
using ServiceStack.CacheAccess.Providers;
using ServiceStack.Configuration;
using ServiceStack.OrmLite;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;

namespace WebApplication4
{
    public class ServicesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ICacheClient>().ImplementedBy<MemoryCacheClient>());
            //container.Register(Component.For<IUserAuthRepository>().ImplementedBy<InMemoryAuthRepository>());
            var dbConnectionFactory =
                new OrmLiteConnectionFactory(HttpContext.Current.Server.MapPath("~/App_Data/data.txt"),
                    SqliteDialect.Provider);
            var iUserAuthRepository = new OrmLiteAuthRepository(dbConnectionFactory);
            container.Register(Component.For<IDbConnectionFactory>().Instance(dbConnectionFactory));
            container.Register(Component.For<IUserAuthRepository>().Instance(iUserAuthRepository));
            container.Register(Component.For<MeasuredDataRepository>());
            var authRepo = (OrmLiteAuthRepository)container.Resolve<IUserAuthRepository>();
            authRepo.CreateMissingTables();

            //string hash;
            //string salt;
            //new SaltedHash().GetHashAndSaltString("pass123", out hash, out salt);
            //authRepo.CreateUserAuth(new UserAuth
            //{
            //    Id = 1,
            //    DisplayName = "User",
            //    Email = "user@kobo.com",
            //    UserName = "user",
            //    FirstName = "User",
            //    LastName = "User",
            //    PasswordHash = hash,
            //    Salt = salt,
            //    //Roles = new List<string> { RoleNames.Admin }
            //    //Permissions = new List<string> {"ViewCurrentStatus"}
            //}, "pass123");
        }
    }
}