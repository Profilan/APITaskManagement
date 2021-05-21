using APITaskManagement.Logic.Logging.Interfaces;
using APITaskManagement.Logic.Management;
using APITaskManagement.Logic.Management.Repositories;
using APITaskManagement.Logic.Schedulers;
using FluentAssertions.Execution;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

namespace APITaskManagement.Logic.ReceiveSend.Interfaces
{
    public abstract class ReceiveSendAction
    {
        protected Task _task;
        protected IList<ILogger> Loggers { get; set; }
        private readonly UserRepository userRepository = new UserRepository();
        protected User user;

        public ReceiveSendAction(Task task)
        {
            _task = task;
            Loggers = new List<ILogger>();

            user = userRepository.GetById(40); // Administrator
        }

        public abstract void Execute();

        public void AddLogger(ILogger logger)
        {
            Loggers.Add(logger);
        }

        #region Private Methods

        protected void LogResponse(Request request)
        {
            foreach (ILogger logger in Loggers)
            {
                logger.Log(request, _task.Url, user, _task);
            }
        }

        protected string GetToken()
        {
            RestClient client = new RestClient(_task.Authentication.OAuthUrl);
            client.Timeout = -1;
            RestRequest request = new RestRequest(Method.POST);
            request.AlwaysMultipartFormData = true;
            request.AddParameter("grant_type", _task.Authentication.GrantType);
            request.AddParameter("client_id", _task.Authentication.Username);
            request.AddParameter("client_secret", _task.Authentication.Password);
            // request.AddParameter("scope", _task.Authentication.Scope);
            request.AddParameter("audience", _task.Authentication.OAuthAudience);
            IRestResponse tResponse = client.Execute(request);

            string responseJson = tResponse.Content;
            string token = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseJson)["access_token"].ToString();

            return token;
        }

        #endregion
    }
}
