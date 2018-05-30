using APITaskManagement.Logic.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Management.Repositories
{
    public class RoleRepository : IRepository<Role, int>
    {
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Role GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Role entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> List(string sortOrder, string searchString, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> List()
        {
            throw new NotImplementedException();
        }

        public void Update(Role entity)
        {
            throw new NotImplementedException();
        }
    }
}
