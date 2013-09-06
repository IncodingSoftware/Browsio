namespace Browsio.Domain
{
    #region << Using >>

    using System;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Web;
    using Browsio.Amazon;
    using DotNetOpenAuth.OAuth2;
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;
    using FluentValidation;
    using Incoding.Block.IoC;
    using Incoding.Block.Logging;
    using Incoding.CQRS;
    using Incoding.Data;
    using Incoding.EventBroker;
    using Incoding.Extensions;
    using Incoding.MetaLanguageContrib;
    using Incoding.MvcContrib;
    using Incoding.Utilities;
    using NHibernate.Context;


    #endregion

    public static class Bootstrapper
    {
        ////ncrunch: no coverage start

        #region Factory constructors

        public static void Start()
        {
            LoggingFactory.Instance.Initialize(logging =>
                                                   {
                                                       string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log");
                                                       logging.WithPolicy(policy => policy.For(LogType.Debug).Use(FileLogger.WithAtOnceReplace(path, () => "Debug_{0}.txt".F(DateTime.Now.ToString("yyyyMMdd")))));
                                                   });

            IoCFactory.Instance.Initialize(init => init.WithProvider(new StructureMapIoCProvider(registry =>
                                                                                                     {
                                                                                                         registry.For<IDispatcher>().Use<DefaultDispatcher>();
                                                                                                         registry.For<IEventBroker>().Use<DefaultEventBroker>();
                                                                                                         registry.For<IAmazonService>().Use<AmazonService>();
                                                                                                         registry.For<HttpContextBase>().Use(() => new HttpContextWrapper(HttpContext.Current));

                                                                                                         var configure = Fluently
                                                                                                                 .Configure()
                                                                                                                 .Database(MsSqlConfiguration.MsSql2008.ConnectionString(ConfigurationManager.ConnectionStrings["Main"].ConnectionString))
                                                                                                                 .Mappings(configuration => configuration.FluentMappings.AddFromAssembly(typeof(Bootstrapper).Assembly))
                                                                                                                 .CurrentSessionContext<ThreadStaticSessionContext>();

                                                                                                         registry.For<IManagerDataBase>().Use(new NhibernateManagerDataBase(configure));
                                                                                                         registry.For<INhibernateSessionFactory>().Singleton().Use(() => new NhibernateSessionFactory(configure));

                                                                                                         registry.For<IUnitOfWorkFactory>().Use<NhibernateUnitOfWorkFactory>();
                                                                                                         registry.For<IUnitOfWork>().Use<NhibernateUnitOfWork>();
                                                                                                         registry.For<IRepository>().Use<NhibernateRepository>();

                                                                                                         registry.Scan(r =>
                                                                                                                           {
                                                                                                                               r.WithDefaultConventions();
                                                                                                                               r.TheCallingAssembly();

                                                                                                                               r.ConnectImplementationsToTypesClosing(typeof(AbstractValidator<>));
                                                                                                                               r.ConnectImplementationsToTypesClosing(typeof(IEventSubscriber<>));
                                                                                                                               r.AddAllTypesOf<ISetUp>();
                                                                                                                           });
                                                                                                         registry.For<IFormAuthentication>().Use<FormAuthentication>();
                                                                                                         registry.For<ISessionContext>().Use<HttpSessionContext>();
                                                                                                         registry.For<IAmazonConfig>().Use(() => new AmazonConfig(ConfigurationManager.AppSettings["AmazonStoreId"]));
                                                                                                         registry.For<IEmailSender>().AlwaysUnique().Use(() => new NetEmailSender(ConfigurationManager.AppSettings["SmtpHost"], int.Parse(ConfigurationManager.AppSettings["SmtpPort"]), true, new NetworkCredential(ConfigurationManager.AppSettings["MailFrom"], ConfigurationManager.AppSettings["MailPassword"])));
                                                                                                         var facebookProvider = new OAuthProvider(new OAuthProviderParams
                                                                                                                                                                          {
                                                                                                                                                                              Authorization = new AuthorizationServerDescription
                                                                                                                                                                                                  {
                                                                                                                                                                                                      TokenEndpoint = new Uri("https://graph.facebook.com/oauth/request_token"),
                                                                                                                                                                                                      AuthorizationEndpoint = new Uri("https://graph.facebook.com/oauth/authorize")
                                                                                                                                                                                                  },
                                                                                                                                                                              AppId = BrowsioConfiguration.Connectors.Facebook.Key,
                                                                                                                                                                              AppSecret = BrowsioConfiguration.Connectors.Facebook.Value,
                                                                                                                                                                              AccessTokenUrl = "https://graph.facebook.com/oauth/access_token",
                                                                                                                                                                              Scope = new[] { "publish_actions", "publish_stream", "offline_access", "read_stream", "read_mailbox", "read_friendlists","email" }
                                                                                                                                                                          }, (int)ConnectorOfType.Facebook);
                                                                                                         registry.For<IOAuthProvider>().Use(facebookProvider).Named(ConnectorOfType.Facebook.ToString());
                                                                                                         registry.For<IConnectProviderClient>().Use<ConnectFacebookClient>().Named(ConnectorOfType.Facebook.ToString());
                                                                                                     })));

            foreach (var setUp in IoCFactory.Instance.ResolveAll<ISetUp>().OrderBy(r => r.GetOrder()))
                setUp.Execute();


            var defNoty = PinesNotyOptions.Default;
            defNoty.Stack = PinesNotyStackType.BarTop;
            defNoty.Delay = 1500;
            defNoty.Hide = true;

            var defBlock = JqueryBlockUiOptions.Default;
            defBlock.Message = "<h1>Please wait....</h1>";

            var defAjax = JqueryAjaxOptions.Default;
            defAjax.Cache = false;

        }

        #endregion

        ////ncrunch: no coverage end   
    }
}