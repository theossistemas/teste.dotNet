using JeffersonMello.Livraria.Data.Context;
using JeffersonMello.Livraria.DI;
using Microsoft.EntityFrameworkCore;
using Ninject;
using Serilog;
using System.Reflection;

namespace JeffersonMello.Livraria.Tests.Data.Abstract
{
    public abstract class DataTestBase
    {
        private string _connectionString = "Server=omegainc.top,11433;Database=omegainc_testetheos;User Id=jefferson;Password=c3df32ea11";

        #region Protected Fields

        protected StandardKernel kernel;

        protected DataContext dataContext;

        #endregion Protected Fields

        #region Public Constructors

        public DataTestBase()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs\\jeffersonmello.livraria.tests.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseSqlServer(_connectionString);

            dataContext = new DataContext(optionsBuilder.Options);
            kernel = new StandardKernel();
            new Bindings(kernel, dataContext).Load();
            kernel.Load(Assembly.GetExecutingAssembly());
        }

        #endregion Public Constructors
    }
}