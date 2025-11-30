using CompositionRoot;
using laba1s3core;
using Ninject;
using Ninject.Modules;
using Ninject.Syntax;
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
            bool useDapper = true; // EF - false

            IKernel kernel = useDapper
                ? new StandardKernel(new DapperModule())
                : new StandardKernel(new EfModule());
            var logic = kernel.Get<ILibraryLogic>();
            Application.Run(new Form1(logic));

        }
    }
}
