using Core.Interfaces;
using Core.Models;

namespace Core.Services
{
    public class AutorService
    {
        private readonly IAutorDao _dao;

        public AutorService(IAutorDao dao)
        {
            _dao = dao;
        }

        public List<Autor> GetAll()
        {
            return _dao.GetAll();
        }

        public Autor? GetById(int id)
        {
            return _dao.GetById(id);
        }

        public string? Add(Autor autor)
        {
            string? greska = Validiraj(autor);
            if (greska != null)
                return greska;

            bool postoji = _dao.GetAll().Any(a =>
                a.BrojLicneKarte == autor.BrojLicneKarte);

            if (postoji)
                return "Autor sa tim brojem licne karte vec postoji.";

            _dao.Add(autor);
            return null;
        }

        public string? Update(Autor autor)
        {
            if (_dao.GetById(autor.Id) == null)
                return "Autor nije pronadjen.";

            string? greska = Validiraj(autor);
            if (greska != null)
                return greska;

            bool zauzet = _dao.GetAll().Any(a =>
                a.BrojLicneKarte == autor.BrojLicneKarte
                && a.Id != autor.Id);

            if (zauzet)
                return "Drugi autor vec ima taj broj licne karte.";

            _dao.Update(autor);
            return null;
        }

        public bool Delete(int id)
        {
            return _dao.Delete(id);
        }

        private string? Validiraj(Autor autor)
        {
            if (string.IsNullOrWhiteSpace(autor.Ime) ||
                string.IsNullOrWhiteSpace(autor.Prezime))
                return "Ime i prezime su obavezni.";

            if (string.IsNullOrWhiteSpace(autor.BrojLicneKarte))
                return "Broj licne karte je obavezan.";

            int at = autor.Email.IndexOf('@');
            int tacka = at < 0 ? -1 : autor.Email.IndexOf('.', at);

            if (at <= 0 || tacka < at + 2 ||
                tacka == autor.Email.Length - 1)
            {
                return "E-mail adresa nije ispravna.";
            }

            if (autor.GodineIskustva < 0)
                return "Godine iskustva ne mogu biti negativne.";

            if (autor.DatumRodjenja > DateTime.Today)
                return "Datum rodjenja ne moze biti u buducnosti.";

            return null;
        }
    }
}
