using Core.Models;

namespace Core.Interfaces
{
    public interface IIzdavacDao
    {
        List<Izdavac> GetAll();
        Izdavac? GetBySifra(string sifra);
        void Add(Izdavac izdavac);
        void Update(Izdavac izdavac);
        bool Delete(string sifra);
    }
}