using DataAccessLayer;
using laba1s3core;
using Ninject.Modules;

namespace CompositionRoot
{
    public class SimpleConfigModule : NinjectModule
    {
        public override void Load()
        {
            /// <summary>
            /// EF по умолчанию
            /// </summary>
            Bind<IRepository<Book>>().To<EntityRepository<Book>>().InSingletonScope();

            /// <summary>
            /// Dapper
            /// </summary>
            Bind<string>().ToConstant("Server=(localdb)\\MSSQLLocalDB;Database=LibraryDb;Trusted_Connection=True;").WhenInjectedInto<BookDapperRepository>();
            Bind<IRepository<Book>>().To<BookDapperRepository>().InSingletonScope().Named("dapper");
        }
    }
}
