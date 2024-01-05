// Detta gränssnitt definierar en generisk uppsättning CRUD-operationer (Create, Read, Update, Delete) för entiteter.
// T är en generisk typ som representerar en entitetsmodell.

namespace CV_Hemsida.Repositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(); // Hämtar alla entiteter av typen T.
        T GetById(int id); // Hämtar en entitet av typen T med ett specifikt ID.
        void Insert(T entity);  // Lägger till en ny entitet av typen T.
        void Update(T entity); // Uppdaterar en befintlig entitet av typen T.
        void Delete(T entity); // Tar bort en entitet av typen T.
        void Save(); // Sparar alla ändringar i databasen.
    }
}

