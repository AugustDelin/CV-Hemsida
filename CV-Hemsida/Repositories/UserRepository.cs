using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CVDataLayer;
using CVModels;

namespace CV_Hemsida.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly CVContext _dbContext;

        public UserRepository(CVContext cvcontext)
        {
            _dbContext = cvcontext ?? throw new ArgumentNullException(nameof(cvcontext));
        }

        public void Delete(User entity)
        {
            _dbContext.Remove(entity);
        }

        public IEnumerable<User> GetAll()
        {
            return _dbContext.Users;
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public User GetById(string id)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Id.Equals(id));
        }

        public void Insert(User entity)
        {
            _dbContext.Add(entity);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Update(User entity)
        {
            _dbContext.Update(entity);
            _dbContext.SaveChanges();
        }
    }
}
