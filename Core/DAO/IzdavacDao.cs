using Core.Interfaces;
using Core.Models;
using Core.Utils;

namespace Core.DAO
{
    public class IzdavacDao : IIzdavacDao
    {
        private const string FileName = "izdavaci.json"; //izdavaci idu u svoj fajl, odvojeni od posjetilaca, da se ne bi mejdusobno prepisivbali
        private readonly List<Izdavac> _izdavaci;

        public IzdavacDao()
        {
            _izdavaci = JsonStorage.Load<Izdavac>(FileName);
        }

        public List<Izdavac> GetAll() //vraca kopiju liste, da niko izvana ne mijenja nasu internu listu mimo Add i Delete
        {
            return new List<Izdavac>(_izdavaci);
        }

        public Izdavac? GetBySifra(string sifra) //vraca jednog ili nijednog izdavaca
        {
            return _izdavaci.FirstOrDefault(i => i.Sifra == sifra); //uslov: za svakog izdavaca i provjeri da li je njegova sifra jednaka trazenoj
        }

        private void Save() //upise listu izdavaca u izdavaci,json (serijalizacija)
        {
            JsonStorage.Save(FileName, _izdavaci);
        }


        public void Add(Izdavac izdavac)//primi jednog izdavaca kog ce konzola napraviti iz unosa
        {
            _izdavaci.Add(izdavac); //doda ga na kraj liste u memoriji. add je metod list-a
            Save(); //odmah upise cijelu listu u izdavaci.json
        }

        public void Update(Izdavac izdavac)
        {
            int index = _izdavaci.FindIndex(i => i.Sifra == izdavac.Sifra); //trazi na kom mjestu u listi je izdavac sa sitoms firom
            if (index >= 0)
            {
                _izdavaci[index] = izdavac; //mijenja starog novim
                Save(); //upise u fajl izmjene
            }
        }

        public bool Delete(string sifra)
        {
            Izdavac? izdavac = GetBySifra(sifra); //pronalazi izdavaca
            if(izdavac == null)
            {
                return false;
            }

            _izdavaci.Remove(izdavac); //izbaci ga
            Save() ; //upise izmjene u fajl
            return true;
        }




    }
}