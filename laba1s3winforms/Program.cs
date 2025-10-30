using CompositionRoot;
using laba1s3core;
using Ninject;
using System;
using System.Windows.Forms;
namespace laba1s3winforms
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", AppDomain.CurrentDomain.BaseDirectory);
            ApplicationConfiguration.Initialize();
            //var logic = RepositoryFactory.CreateLibraryLogic(useEf: true);


            /// <summary>
            /// EF
            /// </summary>
            //var kernel = new StandardKernel(new SimpleConfigModule());

            /// <summary>
            /// Dapper
            /// </summary>
            var kernel = new StandardKernel(new SimpleConfigModule()).Get<IRepository<Book>>("dapper");
            var logic = new LibraryLogic(kernel);
            Application.Run(new Form1(logic));
        }
    }
}
