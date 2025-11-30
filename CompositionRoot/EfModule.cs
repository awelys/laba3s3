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
    public class EfModule : NinjectModule
    {
        public override void Load()
        {
            Bind<AppDbContext>().ToSelf().InSingletonScope();

            Bind<IRepository<Book>>()
                .To<EntityRepository<Book>>()
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
