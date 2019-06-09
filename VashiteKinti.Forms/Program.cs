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
           
            //
            //container.Register<IGenericDataService<Card>, GenericDataService<Card>>(Lifestyle.Transient);
            //container.Register<IGenericDataService<Credit>, GenericDataService<Credit>>(Lifestyle.Transient);
            //
            //container.Register<IGenericDataService<Insurance>, GenericDataService<Insurance>>(Lifestyle.Transient);
            //container.Register<IGenericDataService<Investment>, GenericDataService<Investment>>(Lifestyle.Transient);

//            _container.RegisterDisposableTransient<Form1>();
            _container.Register<Form1>(Lifestyle.Singleton);

            // Optionally verify the container.
            _container.Verify();
        }

        public static void RegisterDisposableTransient<TConcrete>(
            this Container c)
            where TConcrete : class, IDisposable
        {
            var scoped = Lifestyle.Scoped;
            var r = Lifestyle.Transient.CreateRegistration<TConcrete>(c);

            r.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "ignore");
            c.AddRegistration(typeof(TConcrete), r);
            c.RegisterInitializer<TConcrete>(o => scoped.RegisterForDisposal(c, o));
        }
    }
}
