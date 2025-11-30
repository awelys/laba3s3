using DataAccessLayer;
using laba1s3core;
using Ninject;
using Ninject.Modules;

namespace CompositionRoot
{
    public class SimpleConfigModule : NinjectModule
    {
        public override void Load()
        {
            // DbContext для EF
            Bind<AppDbContext>().ToSelf().InSingletonScope();

            // EF репозиторий - именованный
            Bind<IRepository<Book>>()
                .To<EntityRepository<Book>>()
                .InSingletonScope()
                .Named("ef");

            // Строка подключения для Dapper
            Bind<string>()
                .ToConstant("Server=(localdb)\\MSSQLLocalDB;Database=LibraryDb;Trusted_Connection=True;")
                .WhenInjectedInto<BookDapperRepository>();

            // Dapper репозиторий - именованный
            Bind<IRepository<Book>>()
                .To<BookDapperRepository>()
                .InSingletonScope()
                .Named("dapper");

            // Бизнес-логика - используем EF репозиторий по умолчанию
            Bind<IBookBusinessLogic>()
                .To<BookBusinessLogic>()
                .InSingletonScope()
                .WithConstructorArgument("repository", ctx => ctx.Kernel.Get<IRepository<Book>>("ef"));

            // Сервис - используем EF репозиторий
            Bind<IBookService>()
                .To<BookService>()
                .InSingletonScope()
                .WithConstructorArgument("repo", ctx => ctx.Kernel.Get<IRepository<Book>>("ef"));

            // Логика приложения
            Bind<ILibraryLogic>()
                .To<LibraryLogic>()
                .InSingletonScope();
        }
    }
}
