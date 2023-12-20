using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CVDataLayer;
using CVModels;
using Microsoft.EntityFrameworkCore;

namespace CV_Hemsida.Repositories
{
    public class UserRepository : IRepository<Användare>
    {
        private readonly CVContext _dbContext;

        public UserRepository(CVContext cvcontext)
        {
            _dbContext = cvcontext ?? throw new ArgumentNullException(nameof(cvcontext));
        }

        public void Delete(Användare entity)
        {
            _dbContext.Remove(entity);
        }

        public IEnumerable<Användare> GetAll()
        {
            return _dbContext.Users;
        }

        public Användare GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Användare GetById(string id)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Id.Equals(id));
        }

        public void Insert(Användare entity)
        {
            _dbContext.Add(entity);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Update(Användare entity)
        {
            _dbContext.Update(entity);
            _dbContext.SaveChanges();
        }
    }
}
