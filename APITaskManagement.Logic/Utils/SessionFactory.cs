using NHibernate;
using NHibernate.Cfg;
using NHibernate.Connection;
using NHibernate.Dialect;
using NHibernate.Driver;
using System.Collections.Generic;
using System.Reflection;

namespace APITaskManagement.Logic.Utils
{
    public class SessionFactory
    {
        private static readonly ISessionFactory _globalSessionFactory = new Configuration().Configure().BuildSessionFactory();
        private static IDictionary<string, ISessionFactory> _allFactories = LoadAllFactories();

        private static IDictionary<string, ISessionFactory> LoadAllFactories()
        {
            var dictionary = new Dictionary<string, ISessionFactory>(2);

            // Database MAATWERK
            var factory = new Configuration()
                .Configure()
                .SetProperty("connection.connection_string_name", "default").BuildSessionFactory();
            dictionary.Add("default", factory);

            // Database 100 (Exact)
            factory = new Configuration()
                .Configure()
                .SetProperty("connection.connection_string_name", "db2").BuildSessionFactory();
            dictionary.Add("db2", factory);

            // Database MvW
            factory = new Configuration()
                .Configure()
                .SetProperty("connection.connection_string_name", "mvw").BuildSessionFactory();
            dictionary.Add("mvw", factory);

            return dictionary;
        }

        public static ISessionFactory GetSessionFactory(string identifier)
        {
            if (_allFactories == null)
            {
                _allFactories = LoadAllFactories();
            }

            return _allFactories[identifier];
        }

        public static ISession GetNewSession(string identifier = "default")
        {
            return GetSessionFactory(identifier).OpenSession();
        }
    }
}
