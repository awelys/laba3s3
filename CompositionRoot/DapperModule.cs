using DataAccessLayer;
using laba1s3core;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompositionRoot
{
    public class DapperModule : NinjectModule
    {
        public override void Load()
        {
            Bind<string>()
                .ToConstant("Server=(localdb)\\MSSQLLocalDB;Database=LibraryDb;Trusted_Connection=True;")
                .WhenInjectedInto<BookDapperRepository>();

            Bind<IRepository<Book>>()
                .To<BookDapperRepository>()
                .InSingletonScope();

            Bind<IBookBusinessLogic>()
                .To<BookBusinessLogic>()
                .InSingletonScope();

            //Bind<IBookService>()
            //    .To<BookService>()
            //    .InSingletonScope();

            Bind<ILibraryLogic>()
                .To<LibraryLogic>()
                .InSingletonScope();
        }
    }
}
