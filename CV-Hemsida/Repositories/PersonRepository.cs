// Denna klass implementerar IRepository för entitetstypen Person.
// Den tillhandahåller metoder för att hantera persondata i databasen.

using CV_Hemsida.Repositories;
using CVDataLayer;
using CVModels;

namespace CV_SITE.Repositories
{
    public class PersonRepository : IRepository<Person>
    {
        private readonly CVContext _dbContext; // En instans av databaskontexten.


        // Konstruktören initierar databaskontexten.
        public PersonRepository(CVContext dataBaseContext)
        {
            _dbContext = dataBaseContext;
        }

        // Implementering av IRepository-metoder:

        public void Delete(Person entity)
        {
            _dbContext.Remove(entity);
        }

        public IEnumerable<Person> GetAll()
        {
            return _dbContext.Personer;
        }
        public Person GetById(int id)
        {
            throw new NotImplementedException();
        }
        public Person GetById(string id)
        {
            return _dbContext.Personer.Where(p => p.Personnummer.Equals(id)).FirstOrDefault();
        }

        public void Insert(Person entity)
        {
            _dbContext.Add(entity);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Update(Person entity)
        {
            _dbContext.Update(entity);
        }
    }
}
