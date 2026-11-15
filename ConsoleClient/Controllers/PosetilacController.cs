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
                Console.WriteLine($"Id: {p.Id}");
                Console.WriteLine($"  Broj clanske karte: {p.BrojClanskeKarte}");
                Console.WriteLine($"  Ime i prezime: {p.Ime} {p.Prezime}");
                Console.WriteLine($"  Datum rodjenja: {p.DatumRodjenja:dd.MM.yyyy.}");
                Console.WriteLine($"  Adresa: {p.Adresa}, {p.Adresa.Drzava}");
                Console.WriteLine($"  Telefon: {p.KontaktTelefon}");
                Console.WriteLine($"  E-mail: {p.Email}");
                Console.WriteLine($"  Godina clanstva: {p.TrenutnaGodinaClanstva}");
                Console.WriteLine($"  Status: {p.Status}");
                Console.WriteLine($"  Prosjecna ocjena: {p.ProsecnaOcena}");
                Console.WriteLine($"  Kupovina: {p.Kupovine.Count}, na listi zelja: {p.ListaZelja.Count}");
                Console.WriteLine();
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

        public void Izmijeni()
        {
            Console.WriteLine("--- Izmjena posetioca ---");

            int id = Unos.ProcitajCijeliBroj("Id posetioca: ");
            //korisnik ukuca broj posjetioca i to vidi u ipsisu prikazi sve je je id prva kolona
            Posetilac? posetilac = _service.GetById(id);
            //pita servis za posjetioca sa tim brojem

            if (posetilac == null)
            {
                Console.WriteLine("Posetilac nije pronadjen.");
                return;
            }

            Console.WriteLine($"Mijenjate: {posetilac.Ime} {posetilac.Prezime}");
            //stavila sam {} da je ljepse tj da korsnik vidi koga mienja, ugl  stavr ispisa

            Posetilac izmijenjen = new Posetilac();
            //novi objekat mora imati isti id,jer po njemu dao zna koga miejnja u listi, da update ne bi mislio da je neki drugi

            izmijenjen.Id = posetilac.Id;
            izmijenjen.ProsecnaOcena = posetilac.ProsecnaOcena; //prepisujemo
            izmijenjen.Kupovine = posetilac.Kupovine;
            izmijenjen.ListaZelja = posetilac.ListaZelja;


            //polja koja unosi referent se citaju preko Unosa, isto kao pri dodavanju
            izmijenjen.BrojClanskeKarte = Unos.ProcitajTekst("Broj clanske karte: ");
            izmijenjen.Ime = Unos.ProcitajTekst("Ime: ");
            izmijenjen.Prezime = Unos.ProcitajTekst("Prezime: ");
            izmijenjen.DatumRodjenja = Unos.ProcitajDatum("Datum rodjenja");

            izmijenjen.KontaktTelefon = Unos.ProcitajTekst("Kontakt telefon: ");
            izmijenjen.Email = Unos.ProcitajTekst("E-mail: ");

            izmijenjen.Adresa.Ulica = Unos.ProcitajTekst("Ulica: ");
            izmijenjen.Adresa.Broj = Unos.ProcitajTekst("Broj: ");
            izmijenjen.Adresa.Grad = Unos.ProcitajTekst("Grad: ");
            izmijenjen.Adresa.Drzava = Unos.ProcitajTekst("Drzava: ");

            izmijenjen.TrenutnaGodinaClanstva = Unos.ProcitajCijeliBroj("Trenutna godina clanstva: ");
            izmijenjen.Status = Unos.ProcitajStatus();

            string? greska = _service.Update(izmijenjen);//saljemo servisu novi objekat
            //servis prvo provjeri poravila pa onda zove dao, koji zamijeni starog posjetioca novim i upise u fajl
            if (greska != null)
            {
                Console.WriteLine("Greska: " + greska);
                return;
            }

            Console.WriteLine("Posetilac je izmijenjen.");

        }

        public void Obrisi()
        {
            Console.WriteLine("--- Brisanje posetioca ---");

            int id = Unos.ProcitajCijeliBroj("Id posetioca: ");
            //Prvi dio je isti kao kod izmjene: korisnik ukuca Id,
            //servis pronađe posjetioca, a ako ga nema, ispiše se poruka i return prekida metodu.
            Posetilac? posetilac = _service.GetById(id);

            if (posetilac == null)
            {
                Console.WriteLine("Posetilac nije pronadjen.");
                return;
            }

            bool potvrda = Unos.ProcitajDaNe($"Da li ste sigurni da zelite da obrisete {posetilac.Ime} {posetilac.Prezime}?");

            if (!potvrda)
            {
                Console.WriteLine("Brisanje otkazano.");
                return;
            }

            bool obrisan = _service.Delete(id);
            if (obrisan)
            {
                Console.WriteLine("Posetilac je obrisan.");
            }
            else
            {
                Console.WriteLine("Brisanje nije uspjelo.");
            }
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
                Console.WriteLine("3. Izmijeni");
                Console.WriteLine("4. Obrisi");
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
                    case "3":
                        Izmijeni();
                        break;
                    case "4":
                        Obrisi();
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