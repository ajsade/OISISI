using Core.Interfaces; //zbog interfejsa kog klasa implementira
using Core.Models;  //treba nam zbog posjetioca
using Core.Utils; //treba nam jer je tu smjesten JsonStorage

namespace Core.DAO //"adresa"klase
{
    public class PosetilacDao : IPosetilacDao //implementira ovaj interfejs
    {
        private const string FileName = "posetioci.json";
        //ime fajla u koji cemo da upisujemo posjetioce
        private readonly List<Posetilac> _posetioci;
        //lista sa objektima tipa Posjetilac, _posetioci je ime promjelnjive
        
        public PosetilacDao() //kontruktor, priprema listu
        {
            _posetioci = JsonStorage.Load<Posetilac>(FileName);
        //<Posetilac> kaze metodi koji tip objekta da pravi od jsona
        //deserijalizacijac
        }

        public List<Posetilac> GetAll() //public da je mogu zvati servisi, konzola..
        {
            return new List<Posetilac>(_posetioci); //pravi se kopija da neko izvana ne bi slucajno obrisao
                                                    //ili dodao nesto u nasu internu listu, mimo adda i deletea
        }
        public Posetilac? GetById (int id)//vrati jednog ili nijednog(u slucaju null)posjetioca
        {
            return _posetioci.FirstOrDefault(p => p.Id == id);
            //prodji kroz listu i daj prvog koji zadovoljava uslov, a ako ne
            //zadovoljava uslov nijedan, daj null
            //za svakog posjetioca p provjeri da li je p.Id jednak trazenom id
        }

        private void Save()
        {
            JsonStorage.Save(FileName, _posetioci);
            //da ime fajla i listu metodi a ona je pretvori u JSON, tj serijalizacija i upise u fajl
        }

        public void Add(Posetilac posetilac) //primi jednog posjetioca kog konza napravi iz unosa
        {
            int noviId = 1; //napravimo prom za novi id i setujemo je na 1, za prvog posjetioca
            if(_posetioci.Count > 0)//ako lista nije prazna
            {
                noviId = _posetioci.Max(p => p.Id) + 1;
                //max trazi najveci id u listi, ako je najveci 5 dodamo
                //i dobijemo sljedeci slobodan, 6
            }

            posetilac.Id = noviId; //upisemo taj broj u novog posjetioca
            _posetioci.Add(posetilac); //metoda liste koja dodaje posjetioca na kraj nase liste u memoriji
            Save(); //odmah upise cijeli listu u fajl, pa se nista ne gubi i da se program
                    //iznenada ugasi
        }

        public void Update(Posetilac posetilac)
        {
            int index = _posetioci.FindIndex(p => p.Id == posetilac.Id); //trazi na kom mjetsu u listi je element kojiz adovoljava uslov i smjesta u promejlljivu index
            
            if(index >= 0) //ako je posjetilac andjen
            {
                _posetioci[index] = posetilac; //zamjenimo starog novim na istom mejstu u listi
                Save();  //upisemo odmah izmjeny u fajl
            }
        }

        public bool Delete(int id)
        {
            Posetilac? posetilac = GetById(id); //ptonalazi psojetioca po id-u
            if (posetilac == null)
            {
                return false; //ako ga ne pronadje,metoda tu zavrsi i nema brisanja
            }

            _posetioci.Remove(posetilac); //izbacuje tog psojetyioca iz liste u memoriji. Remove pripada lsiti
            Save(); //upisuje listu u fajl, bez tog obrisanog posjetioca
            return true; //javlja da je brisanje uspjelo
        }
    }
}