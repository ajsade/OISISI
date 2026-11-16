using Core.Models;

namespace Core.Interfaces
{
    public interface IAutorDao
    {
        List<Autor> GetAll();
        Autor? GetById(int id);
        void Add(Autor autor);
        void Update(Autor autor);
        bool Delete(int id);
    }
}