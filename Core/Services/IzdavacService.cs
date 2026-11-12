using Core.Interfaces;
using Core.Models;

namespace Core.Services
{
    public class IzdavacService
    {
        private readonly IIzdavacDao _dao;
        //serbis drzi DAO za izdavace,preko interfejsa,pa ga ne zanima
        //kako je DAO napravljen

        public IzdavacService(IIzdavacDao dao)
        {
            _dao = dao; //konstruktor prima dao izvana i upusje ga u polje _dao
                        // da ga ostale metode kogu koristiti
        }

        public List<Izdavac> GetAll()
            //pita DAO za izdavace i vraca ih dalje
        {
            return _dao.GetAll();
        }

        public Izdavac? GetBySifra(string sifra) //trazi jednog izdavaca po sifri
        {
            return _dao.GetBySifra(sifra);
        }

        public string? Add(Izdavac izdavac)
        {
            //prvo provjera da naziv i sifra nisu empty itd
            if (string.IsNullOrWhiteSpace(izdavac.Sifra))
            {
                return "Šifra izdavača je obavezna.";
            }

            if (string.IsNullOrWhiteSpace(izdavac.Naziv))
            {
                return "Naziv izdavača je obavezan.";
            }

            bool postoji = _dao.GetAll().Any(i => i.Sifra == izdavac.Sifra);
            //postoji li bar jedan izdavac sa istom sifrom
            if (postoji)
            {
                return "Izdavač sa tom šifrom već postoji.";
            }
            //tek kad pprodjemo sve provjere, zovemo dao.
            _dao.Add(izdavac);
            return null;
        }

        public string? Update(Izdavac izdavac)
        {
            if (string.IsNullOrWhiteSpace(izdavac.Naziv))
            {
                return "Naziv izdavača je obavezan.";
            }

            if (_dao.GetBySifra(izdavac.Sifra) == null)
            {
                return "Izdavač sa tom šifrom ne postoji.";
                //psoto je sifra jedinstvena, samo nam treba da psotoji
            }

            _dao.Update(izdavac);
            return null;
        }

        public bool Delete(string sifra)
        {
            return _dao.Delete(sifra);
        }
    }
}