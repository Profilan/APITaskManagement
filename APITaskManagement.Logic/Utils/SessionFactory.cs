using NHibernate;
using NHibernate.Cfg;
using NHibernate.Connection;
using NHibernate.Dialect;
using NHibernate.Driver;
using System.Reflection;

namespace APITaskManagement.Logic.Utils
{
    public class SessionFactory
    {
        private static ISessionFactory _factory;

        private static void Init()
        {
            Configuration config = new Configuration();
            config.Configure();
            // config.SetProperty("connection.connection_string_name", "default");

            _factory = config.BuildSessionFactory();
        }

        public static ISessionFactory GetSessionFactory()
        {
            if (_factory == null)
            {
                Init();
            }

            return _factory;
        }

        public static ISession GetNewSession()
        {
            return GetSessionFactory().OpenSession();
        }
    }
}
