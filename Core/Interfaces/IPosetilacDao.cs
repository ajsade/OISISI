using Core.Models;
    //koristi se posetilac 

namespace Core.Interfaces
{
    //interface ce da nam kaze koje metode klasa mora imati, ali ne kako rade
   //servis ce se oslanjati na to, a ne na konkretnu klasu
    public interface IPosetilacDao
    {
        List<Posetilac> GetAll();//daje listu svih posjetilaca
        Posetilac? GetById(int id); //daje jednog posjetioca po broju(? znaci moze i nista da vrati tj null)
        void Add(Posetilac posetilac); //dodaje novog posjetioca
        void Update(Posetilac posetilac); //mijenja postojecegg
        bool Delete(int id); //brise i vraca true ako je uspjelo
    }
}