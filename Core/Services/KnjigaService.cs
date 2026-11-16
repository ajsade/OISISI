using Core.Interfaces;
using Core.Models;

namespace Core.Services
{
    public class KnjigaService
    {
        private readonly IKnjigaDao _dao;

        public KnjigaService(IKnjigaDao dao)
        {
            _dao = dao;
        }

        public List<Knjiga> GetAll()
        {
            return _dao.GetAll();
        }

        public Knjiga? GetByIsbn(string isbn)
        {
            return _dao.GetByIsbn(isbn);
        }

        public string? Add(Knjiga knjiga)
        {
            string? greska = Validiraj(knjiga);
            if (greska != null)
                return greska;

            if (_dao.GetByIsbn(knjiga.ISBN) != null)
                return "Knjiga sa tim ISBN brojem vec postoji.";

            _dao.Add(knjiga);
            return null;
        }

        public string? Update(Knjiga knjiga)
        {
            if (_dao.GetByIsbn(knjiga.ISBN) == null)
                return "Knjiga nije pronadjena.";

            string? greska = Validiraj(knjiga);
            if (greska != null)
                return greska;

            _dao.Update(knjiga);
            return null;
        }

        public bool Delete(string isbn)
        {
            return _dao.Delete(isbn);
        }

        private string? Validiraj(Knjiga knjiga)
        {
            if (string.IsNullOrWhiteSpace(knjiga.ISBN))
                return "ISBN je obavezan.";

            if (string.IsNullOrWhiteSpace(knjiga.Naziv))
                return "Naziv knjige je obavezan.";

            if (string.IsNullOrWhiteSpace(knjiga.Zanr))
                return "Zanr je obavezan.";

            if (knjiga.GodinaIzdanja <= 0)
                return "Godina izdanja mora biti pozitivna.";

            if (knjiga.Cena < 0)
                return "Cena ne moze biti negativna.";

            if (knjiga.BrojStrana <= 0)
                return "Broj strana mora biti veci od nule.";

            return null;
        }
    }
}
