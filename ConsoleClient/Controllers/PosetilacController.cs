using Core.Models;
using Core.Services;


namespace ConsoleClient.Controllers
{
    public class PosetilacController
    {
        private readonly PosetilacService _service;

        public PosetilacController(PosetilacService service)
            //konstrukor orima servis izvana i cuva ga u _service, jer kontroler zna za servis, ne dao
        {
            _service = service;
        }

        public void PrikaziSve()//ispise sve
        {
            List<Posetilac> posetioci = _service.GetAll();

            if (posetioci.Count == 0)
            {
                Console.WriteLine("Nema posetilaca.");
                return;
            }

            foreach (Posetilac p in posetioci)
            {
                Console.WriteLine($"{p.Id} | {p.BrojClanskeKarte} | {p.Ime} {p.Prezime} | {p.Status} | {p.Adresa}");

            }
        }
    }
}