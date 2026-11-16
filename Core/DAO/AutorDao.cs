using Core.Interfaces;
using Core.Models;
using Core.Storage;

namespace Core.DAO
{
    public class AutorDao : IAutorDao
    {
        private readonly List<Autor> _autori;
        private readonly Storage<Autor> _storage;

        public AutorDao()
        {
            _storage = new Storage<Autor>("autori.txt");
            _autori = _storage.Load();
        }

        public List<Autor> GetAll()
        {
            return new List<Autor>(_autori);
        }

        public Autor? GetById(int id)
        {
            return _autori.FirstOrDefault(a => a.Id == id);
        }

        public void Add(Autor autor)
        {
            int noviId = _autori.Count == 0
                ? 1
                : _autori.Max(a => a.Id) + 1;

            autor.Id = noviId;
            _autori.Add(autor);
            Save();
        }

        public void Update(Autor autor)
        {
            int index = _autori.FindIndex(a => a.Id == autor.Id);

            if (index >= 0)
            {
                _autori[index] = autor;
                Save();
            }
        }

        public bool Delete(int id)
        {
            Autor? autor = GetById(id);

            if (autor == null)
                return false;

            _autori.Remove(autor);
            Save();
            return true;
        }

        private void Save()
        {
            _storage.Save(_autori);
        }
    }
}
