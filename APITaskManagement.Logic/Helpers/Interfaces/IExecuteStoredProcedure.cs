using System.Collections.Generic;
using System.Data.SqlClient;

namespace APITaskManagement.Logic.Helpers.Interfaces
{
    public interface IExecuteStoredProcedure
    {
        int ExecuteScalarStoredProcedure<TOut>(string procedureName, IList<SqlParameter> sqlParameters);
        IEnumerable<TOut> ExecuteStoredProcedure<TOut>(string procedureName, IList<SqlParameter> sqlParameters);
    }
}
