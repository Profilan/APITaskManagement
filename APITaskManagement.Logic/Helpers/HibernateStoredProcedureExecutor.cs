using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using System.Data.SqlClient;
using NHibernate.Transform;
using APITaskManagement.Logic.Helpers.Interfaces;

namespace APITaskManagement.Logic.Helpers
{
    public class HibernateStoredProcedureExecutor : IExecuteStoredProcedure
    {
        private readonly ISession _session;

        public HibernateStoredProcedureExecutor(ISession session)
        {
            _session = session;
        }

        public static IQuery AddStoredProcedureParameters(IQuery query, IEnumerable<SqlParameter> parameters)
        {
            foreach (var parameter in parameters)
            {
                query.SetParameter(parameter.ParameterName, parameter.Value);
            }

            return query;
        }

        public int ExecuteScalarStoredProcedure<TOut>(string procedureName, IList<SqlParameter> sqlParameters)
        {
            int result;

            var query = _session.GetNamedQuery(procedureName);
            AddStoredProcedureParameters(query, sqlParameters);
           
            result = query.ExecuteUpdate();

            return result;
        }

        public IEnumerable<TOut> ExecuteStoredProcedure<TOut>(string procedureName, IList<SqlParameter> sqlParameters)
        {
            IEnumerable<TOut> result;

            var query = _session.GetNamedQuery(procedureName);
            AddStoredProcedureParameters(query, sqlParameters);
            result = query.List<TOut>();

            return result;
        }

        public IEnumerable<TOut> ExecuteStoredProcedure<TOut>(string procedureName)
        {
            IEnumerable<TOut> result;

            var query = _session.GetNamedQuery(procedureName);
            
            result = query.List<TOut>();

            return result;
        }
    }
}
