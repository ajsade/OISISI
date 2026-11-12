using Core.Interfaces; //zbog iposetilacdao
using Core.Models; 

namespace Core.Services
{
    public class PosetilacService 
    {
        private readonly IPosetilacDao _dao;
        //servis drzi jedno polje a to je DAO preko kog cita i upisuje
        //koristi IPosetilacDao, jer servisa ne zanima kako, nego koje sve metode ima
        public PosetilacService(IPosetilacDao dao)
        {
            _dao = dao;
        }

        public List<Posetilac> GetAll()
            //"pita" DAO za sve posjetioce 
        {
            return _dao.GetAll();
        }

        public Posetilac? GetById(int id)
            //za jednog
        {
            return _dao.GetById(id);
        }
        //postoje da konzola ne ragovara sa dao, nego sevrisom samo
         
        public string? Add(Posetilac posetilac)  //vraca poruku o gresci, ako vrati null sve je u redu
        {
            if (string.IsNullOrWhiteSpace(posetilac.BrojClanskeKarte)) //da li je prazno, ako da baca gresku
            {  
                return "Broj članske karte je obavezan.";
            }

            bool postoji = _dao.GetAll().Any(p => p.BrojClanskeKarte == posetilac.BrojClanskeKarte);
            //da li psotoji bar jedan da zavolji uslov za svakog posjetioca da li jenjegov broj broj karte jednak broju koji je stigap
            if (postoji)
            {
                return "Posetilac sa tim brojem članske karte već postoji.";
            }

            _dao.Add(posetilac); //ako su sve provjere prosle, tek tad pzoivao DAO
            return null; //Javi da je uspjelo sve

            //Dakle, servis odlucuje sta smije uci, a DAO samo cita i upisuje
        }

        public string? Update(Posetilac posetilac)
        {
            if (string.IsNullOrWhiteSpace(posetilac.BrojClanskeKarte))
            {
                return "Broj članske karte je obavezan.";
            }

            bool zauzet = _dao.GetAll().Any(p => p.BrojClanskeKarte == posetilac.BrojClanskeKarte && p.Id != posetilac.Id);
            //postoji posjetilac koji ima isti broj karte, a nije taj koji upravo mijenjamo
            if (zauzet)
            {
                return "Drugi posetilac već ima taj broj članske karte.";
            }

            _dao.Update(posetilac);
            return null;
        }

        public bool Delete(int id)
        {
            return _dao.Delete(id);
        }

    }
}