using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleInjector;
using VashiteKinti.Data;
using VashiteKinti.Data.Models;
using VashiteKinti.Services;

namespace VashiteKinti.Forms
{
    static class Program
    {
        private static Container container;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Bootstrap();
            Application.Run(new Form1());
        }

        private static void Bootstrap()
        {
            // Create the container as usual.
            container = new Container();

            container.Register<VashiteKintiDbContext>(() =>
            {
                DbContextOptions<VashiteKintiDbContext> options = new DbContextOptions<VashiteKintiDbContext>(); // Configure your DbContextOptions here

                return new VashiteKintiDbContext(options);
            }, Lifestyle.Singleton);

            // Register your types, for instance:
            container.Register<IGenericDataService<Bank>, GenericDataService<Bank>>(Lifestyle.Transient);
//            container.Register<IGenericDataService<Deposit>, GenericDataService<Deposit>>(Lifestyle.Transient);
//
//            container.Register<IGenericDataService<Card>, GenericDataService<Card>>(Lifestyle.Transient);
//            container.Register<IGenericDataService<Credit>, GenericDataService<Credit>>(Lifestyle.Transient);
//
//            container.Register<IGenericDataService<Insurance>, GenericDataService<Insurance>>(Lifestyle.Transient);
//            container.Register<IGenericDataService<Investment>, GenericDataService<Investment>>(Lifestyle.Transient);

//            container.Register<Form1>();

            // Optionally verify the container.
            container.Verify();
        }
    }
}
