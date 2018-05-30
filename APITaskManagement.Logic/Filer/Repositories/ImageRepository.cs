using APITaskManagement.Logic.Common.Interfaces;
using APITaskManagement.Logic.Filer.Data;
using APITaskManagement.Logic.Utils;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Filer.Repositories
{
    public class ImageRepository : IRepository<Image, string>
    {
        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Image GetById(string id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Image entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Image> List(string sortOrder, string searchString, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Image> List()
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var query = from p in session.Query<Image>()
                            select p;

                return query.ToList();
            }
        }
        public IEnumerable<Image> ListByEANCode(string eanCode)
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var query = from i in session.Query<Image>()
                            select i;

                query = query.Where(i => i.EANCode == eanCode);

                return query.ToList();
            }
        }

        public void Update(Image entity)
        {
            throw new NotImplementedException();
        }
    }
}
