using Core.Models;
using Core.Services;
using ConsoleClient.Utils;


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

        public void Dodaj()
            //ispise naslov, pita redom za polja, posalje servisu i javi uspjeh ili gresku
            //vodi unos, zvacemo je iz menija kasnijee
        {
            Console.WriteLine("--- Novi posetilac ---");

            Posetilac posetilac = new Posetilac();//pravi prazan objekat posjetioca
            posetilac.BrojClanskeKarte = Unos.ProcitajTekst("Broj clanske karte: ");
            posetilac.Ime = Unos.ProcitajTekst("Ime: ");//upisuje u polje objekta vrijednost, zpve pomocnu metodu 
            //ta metoda ispise pitanje. ceka odgovor i ne pusta dalje dok polje ne bude popunjeno
            posetilac.Prezime = Unos.ProcitajTekst("Prezime: ");
            posetilac.DatumRodjenja = Unos.ProcitajDatum("Datum rodjenja");
            //id necemo unositi jer njega racuna dao u add

            posetilac.KontaktTelefon = Unos.ProcitajTekst("Kontakt telefon: ");//isto kao ime i prezime
            posetilac.Email = Unos.ProcitajTekst("E-mail: ");

            posetilac.Adresa.Ulica = Unos.ProcitajTekst("Ulica: ");
            //posto je adresa poseban objekat unutar psojetioca, pa se prvo iz posjetioca uzme
            //adresa u u nju upise ulica
            //a objekat sam napravila u modelu
            posetilac.Adresa.Broj = Unos.ProcitajTekst("Broj: ");
            posetilac.Adresa.Grad = Unos.ProcitajTekst("Grad: ");
            posetilac.Adresa.Drzava = Unos.ProcitajTekst("Drzava: ");

            posetilac.TrenutnaGodinaClanstva = Unos.ProcitajCijeliBroj("Trenutna godina clanstva: ");
            posetilac.Status = Unos.ProcitajStatus(); //korisnik bira 1ili2

            string? greska = _service.Add(posetilac); 
            //predam popunjenog posjetioca servisu.kontroler ne zove dao nego samo servis. servis provjeri pravila i tek onda upise u fajl(serijalizacija)
            if(greska != null)
            {
                Console.WriteLine("Greska: " + greska);//spojimo dva teksta u jedan red
                return;
            }

            Console.WriteLine("Posetilac je dodat."); //ako nije bilo greske , return nije prekinuo metodu pa je posjetilac uspjesno dodat
        }

        public void Meni()
        {
            bool nazad = false; //dok korisnik ne odabere nulu
            while (!nazad)
            {
                Console.WriteLine();
                Console.WriteLine("--- POSETIOCI ---");
                Console.WriteLine("1. Prikazi sve");
                Console.WriteLine("2. Dodaj");
                Console.WriteLine("0. Nazad");
                Console.Write("Izbor: ");
                string? izbor = Console.ReadLine();

                switch (izbor)
                {
                    case "1":
                        PrikaziSve();//pozivam metodu
                        break;
                    case "2":
                        Dodaj();
                        break;
                    case "0":
                        nazad = true;
                        break;
                    default:
                        Console.WriteLine("Nepoznata opcija.");
                        break;//prekida ovu petlju, pa se vracam u meni
                }
            }
        }
    }
}