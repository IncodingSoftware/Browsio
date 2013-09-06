namespace Browsio.UnitTest
{
    using System.Globalization;
    using System.Threading;
    using Browsio.Domain;
    using Incoding.MSpecContrib;
    using Machine.Specifications;
    using System.Configuration;
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;

    ////ncrunch: no coverage start

    public class MSpecAssemblyContext : IAssemblyContext
    {
        #region IAssemblyContext Members

        public void OnAssemblyStart()
        {
            var configure = Fluently
                    .Configure()
                    .Database(MsSqlConfiguration.MsSql2008
                                                .ConnectionString(ConfigurationManager.ConnectionStrings["Test"].ConnectionString)
                                                .ShowSql())
                    .Mappings(configuration => configuration.FluentMappings.AddFromAssembly(typeof(Bootstrapper).Assembly));

            NHibernatePleasure.StartSession(configure, true);

            var enCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = enCulture;
            Thread.CurrentThread.CurrentUICulture = enCulture;
        }

        public void OnAssemblyComplete()
        {
            NHibernatePleasure.StopAllSession();
        }

        #endregion
    }


    ////ncrunch: no coverage end
}