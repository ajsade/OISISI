using Core.Interfaces;
using Core.Models;
using Core.Storage;

namespace Core.DAO
{
    public class KnjigaDao : IKnjigaDao
    {
        private readonly List<Knjiga> _knjige;
        private readonly Storage<Knjiga> _storage;

        public KnjigaDao()
        {
            _storage = new Storage<Knjiga>("knjige.txt");
            _knjige = _storage.Load();
        }

        public List<Knjiga> GetAll()
        {
            return new List<Knjiga>(_knjige);
        }

        public Knjiga? GetByIsbn(string isbn)
        {
            return _knjige.FirstOrDefault(k => k.ISBN == isbn);
        }

        public void Add(Knjiga knjiga)
        {
            _knjige.Add(knjiga);
            Save();
        }

        public void Update(Knjiga knjiga)
        {
            int index = _knjige.FindIndex(k => k.ISBN == knjiga.ISBN);

            if (index >= 0)
            {
                _knjige[index] = knjiga;
                Save();
            }
        }

        public bool Delete(string isbn)
        {
            Knjiga? knjiga = GetByIsbn(isbn);

            if (knjiga == null)
                return false;

            _knjige.Remove(knjiga);
            Save();
            return true;
        }

        private void Save()
        {
            _storage.Save(_knjige);
        }
    }
}
