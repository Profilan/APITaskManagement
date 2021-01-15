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
        public void Log(Request request, Url url, User user, Task task)
        {
            if (!string.IsNullOrEmpty(task.SPLogger))
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
                    SqlCommand command = new SqlCommand(task.SPLogger, connection);
                    command.CommandTimeout = 300;
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
        }

        public void Log(Response response, Share share, User user, Task task)
        {
            if (!string.IsNullOrEmpty(task.SPLogger))
            {
                bool isOk = true;
                if (response.Code >= 300)
                {
                    isOk = false;
                    response.Ids = "";
                }

                var detail = response.Detail;
                var message = response.Code + " " + response.Description;

                string connectionstring = ConfigurationManager.ConnectionStrings["mvw"].ConnectionString;

                var requests = new List<Request>();

                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    DataSet dataSet = new DataSet();
                    SqlCommand command = new SqlCommand(task.SPLogger, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@ids", SqlDbType.VarChar).Value = response.Ids;
                    command.Parameters.Add("@IsOk", SqlDbType.Bit).Value = isOk;
                    command.Parameters.Add("@msg", SqlDbType.VarChar).Value = message;
                    command.Parameters.Add("@IsSuccess", SqlDbType.Bit).Value = isOk;

                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    adapter.Fill(dataSet);
                }
            }
        }

        public void Log(Response response, string recipient, User user, Task task)
        {
            bool isOk = true;
            if (response.Code >= 300)
            {
                isOk = false;
            }

            var detail = response.Detail;
            var message = response.Code + " " + response.Description;

            string connectionstring = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            var requests = new List<Request>();

            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                DataSet dataSet = new DataSet();
                SqlCommand command = new SqlCommand(task.SPLogger, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@id", SqlDbType.Int).Value = response.Id;
                command.Parameters.Add("@IsOk", SqlDbType.Bit).Value = isOk;
                command.Parameters.Add("@json", SqlDbType.VarChar).Value = detail;
                command.Parameters.Add("@msg", SqlDbType.VarChar).Value = message;
                // command.Parameters.Add("@target", SqlDbType.Int).Value = target.Id;
                command.Parameters.Add("@IsSuccess", SqlDbType.Bit).Value = isOk;

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(dataSet);
            }
        }
    }
}
