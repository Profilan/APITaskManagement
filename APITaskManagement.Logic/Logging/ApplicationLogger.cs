using System.Collections.Generic;
using APITaskManagement.Logic.Logging.Interfaces;
using APITaskManagement.Logic.Schedulers;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using APITaskManagement.Logic.Management;
using APITaskManagement.Logic.Filer;
using APITaskManagement.Logic.Filer.Data;

namespace APITaskManagement.Logic.Logging
{
    public class ApplicationLogger : ILogger
    {
        public void Log(Request request, Url url, string spLogger)
        {
            bool isOk = true;
            if (request.Response.Code >= 300)
            {
                isOk = false;
            }

            var detail = "{\"request\": " + request.Body + ",\"response\":" + request.Response.Detail + "}";
            var message = request.Response.Code + " " + request.Response.Description;

            string connectionstring = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            
            var requests = new List<Request>();

            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                DataSet dataSet = new DataSet();
                SqlCommand command = new SqlCommand(spLogger, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@id", SqlDbType.Int).Value = request.Id;
                command.Parameters.Add("@IsOk", SqlDbType.Bit).Value = isOk;
                command.Parameters.Add("@json", SqlDbType.VarChar).Value = detail;
                command.Parameters.Add("@msg", SqlDbType.VarChar).Value = message;
                // command.Parameters.Add("@target", SqlDbType.Int).Value = target.Id;
                command.Parameters.Add("@IsSuccess", SqlDbType.Bit).Value = isOk;

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(dataSet);
            }

        }

        public void Log(Response response, Share share)
        {
            throw new System.NotImplementedException();
        }

        public void Log(Response response, string recipient)
        {
            throw new System.NotImplementedException();
        }
    }
}
