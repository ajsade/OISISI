using Core.Models;

namespace Core.Interfaces
{
    public interface IKnjigaDao
    {
        List<Knjiga> GetAll();
        Knjiga? GetByIsbn(string isbn);
        void Add(Knjiga knjiga);
        void Update(Knjiga knjiga);
        bool Delete(string isbn);
    }
}
