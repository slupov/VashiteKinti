using System;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using SimpleInjector;
using SimpleInjector.Diagnostics;
using SimpleInjector.Lifestyles;
using VashiteKinti.Data;
using VashiteKinti.Data.Models;
using VashiteKinti.Services;

namespace VashiteKinti.Forms
{
    static class Program
    {
        private static Container _container;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Bootstrap();
            Application.Run(_container.GetInstance<Form1>());
        }

        private static void Bootstrap()
        {
            // Create the container as usual.
            _container = new Container();
            _container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            _container.Register<VashiteKintiDbContext>(() =>
            {
                DbContextOptions<VashiteKintiDbContext> options = new DbContextOptions<VashiteKintiDbContext>(); // Configure your DbContextOptions here

                return new VashiteKintiDbContext(options);
            }, Lifestyle.Singleton);

            // Register your types, for instance:
            _container.Register<IGenericDataService<Bank>, GenericDataService<Bank>>(Lifestyle.Singleton);
            _container.Register<IGenericDataService<Deposit>, GenericDataService<Deposit>>(Lifestyle.Singleton);

            _container.Register<IGenericDataService<Card>, GenericDataService<Card>>(Lifestyle.Transient);
            _container.Register<IGenericDataService<Credit>, GenericDataService<Credit>>(Lifestyle.Transient);

            _container.Register<IGenericDataService<Insurance>, GenericDataService<Insurance>>(Lifestyle.Transient);
            _container.Register<IGenericDataService<Investment>, GenericDataService<Investment>>(Lifestyle.Transient);

            _container.Register<Form1>(Lifestyle.Singleton);

            // Optionally verify the container.
            _container.Verify();
        }
    }
}
