using APITaskManagement.Logic.Api.Data;
using APITaskManagement.Logic.Common.Interfaces;
using APITaskManagement.Logic.Helpers;
using APITaskManagement.Logic.Utils;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api.Repositories
{
    public class CashOnDeliveryOrderlineRepository : IRepository<CashOnDeliveryOrderline, int>
    {
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public CashOnDeliveryOrderline GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(CashOnDeliveryOrderline entity)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                DataSet dataSet = new DataSet();
                SqlCommand command = new SqlCommand("EEK_sp_DISTRIBUTION_COD_ORDERLINES" , connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@OrderlineId", SqlDbType.Int).Value = entity.OrderlineId;
                command.Parameters.Add("@cod", SqlDbType.Int).Value = entity.CashOnDelivery;
                
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(dataSet);
            }
        }

        public IEnumerable<CashOnDeliveryOrderline> List(string sortOrder, string searchString, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CashOnDeliveryOrderline> List()
        {
            throw new NotImplementedException();
        }

        public void Update(CashOnDeliveryOrderline entity)
        {
            throw new NotImplementedException();
        }
    }
}
