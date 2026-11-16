using System.Globalization;
using Core.Models;
using Core.Services;
using ConsoleClient.Utils;

namespace ConsoleClient.Controllers
{
    public class KnjigaController
    {
        private readonly KnjigaService _service;

        public KnjigaController(KnjigaService service)
        {
            _service = service;
        }

        public void PrikaziSve()
        {
            List<Knjiga> knjige = _service.GetAll();

            if (knjige.Count == 0)
            {
                Console.WriteLine("Nema knjiga.");
                return;
            }

            foreach (Knjiga k in knjige)
            {
                Console.WriteLine($"ISBN: {k.ISBN}");
                Console.WriteLine($"  Naziv: {k.Naziv}");
                Console.WriteLine($"  Zanr: {k.Zanr}");
                Console.WriteLine($"  Godina izdanja: {k.GodinaIzdanja}");
                Console.WriteLine($"  Cena: {k.Cena:F2}");
                Console.WriteLine($"  Broj strana: {k.BrojStrana}");
                Console.WriteLine($"  Izdavac: {k.SifraIzdavaca}");
                Console.WriteLine($"  Autori (ID): {string.Join(", ", k.AutorIds)}");
                Console.WriteLine();
            }
        }

        private static decimal ProcitajCenu()
        {
            while (true)
            {
                Console.Write("Cena: ");
                string unos = (Console.ReadLine() ?? "")
                    .Trim().Replace(',', '.');

                if (decimal.TryParse(unos,
                    NumberStyles.AllowLeadingSign |
                    NumberStyles.AllowDecimalPoint,
                    CultureInfo.InvariantCulture,
                    out decimal cena))
                {
                    return cena;
                }

                Console.WriteLine("Neispravna cena.");
            }
        }

        private static string ProcitajSifruIzdavaca()
        {
            while (true)
            {
                Console.Write("Sifra izdavaca (Enter ako nema): ");
                string sifra = Console.ReadLine()?.Trim() ?? "";

                if (!sifra.Contains('|'))
                    return sifra;

                Console.WriteLine("Znak | nije dozvoljen.");
            }
        }

        private Knjiga UnesiPodatke(Knjiga? postojeca = null)
        {
            Knjiga knjiga = new Knjiga();

            knjiga.ISBN = postojeca?.ISBN
                ?? Unos.ProcitajTekst("ISBN: ");

            // Zadrzavamo postojece veze prilikom izmene.
            if (postojeca != null)
            {
                knjiga.AutorIds = new List<int>(postojeca.AutorIds);
                knjiga.KupiliPosetiociIds =
                    new List<int>(postojeca.KupiliPosetiociIds);
                knjiga.ListaZeljaPosetiociIds =
                    new List<int>(postojeca.ListaZeljaPosetiociIds);
            }

            knjiga.Naziv = Unos.ProcitajTekst("Naziv: ");
            knjiga.Zanr = Unos.ProcitajTekst("Zanr: ");
            knjiga.GodinaIzdanja =
                Unos.ProcitajCijeliBroj("Godina izdanja: ");
            knjiga.Cena = ProcitajCenu();
            knjiga.BrojStrana =
                Unos.ProcitajCijeliBroj("Broj strana: ");
            knjiga.SifraIzdavaca = ProcitajSifruIzdavaca();

            return knjiga;
        }

        public void Dodaj()
        {
            Console.WriteLine("--- Nova knjiga ---");

            Knjiga knjiga = UnesiPodatke();
            string? greska = _service.Add(knjiga);

            Console.WriteLine(greska == null
                ? "Knjiga je dodata."
                : "Greska: " + greska);
        }

        public void Izmijeni()
        {
            Console.WriteLine("--- Izmena knjige ---");

            string isbn = Unos.ProcitajTekst("ISBN knjige: ");
            Knjiga? knjiga = _service.GetByIsbn(isbn);

            if (knjiga == null)
            {
                Console.WriteLine("Knjiga nije pronadjena.");
                return;
            }

            Console.WriteLine($"Menjate: {knjiga.Naziv}");

            Knjiga izmenjena = UnesiPodatke(knjiga);
            string? greska = _service.Update(izmenjena);

            Console.WriteLine(greska == null
                ? "Knjiga je izmenjena."
                : "Greska: " + greska);
        }

        public void Obrisi()
        {
            Console.WriteLine("--- Brisanje knjige ---");

            string isbn = Unos.ProcitajTekst("ISBN knjige: ");
            Knjiga? knjiga = _service.GetByIsbn(isbn);

            if (knjiga == null)
            {
                Console.WriteLine("Knjiga nije pronadjena.");
                return;
            }

            bool potvrda = Unos.ProcitajDaNe(
                $"Da li zelite da obrisete knjigu {knjiga.Naziv}?");

            if (!potvrda)
            {
                Console.WriteLine("Brisanje otkazano.");
                return;
            }

            Console.WriteLine(_service.Delete(isbn)
                ? "Knjiga je obrisana."
                : "Brisanje nije uspelo.");
        }

        public void Meni()
        {
            while (true)
            {
                Console.WriteLine("\n--- KNJIGE ---");
                Console.WriteLine("1. Prikazi sve");
                Console.WriteLine("2. Dodaj");
                Console.WriteLine("3. Izmeni");
                Console.WriteLine("4. Obrisi");
                Console.WriteLine("0. Nazad");
                Console.Write("Izbor: ");

                switch (Console.ReadLine())
                {
                    case "1": PrikaziSve(); break;
                    case "2": Dodaj(); break;
                    case "3": Izmijeni(); break;
                    case "4": Obrisi(); break;
                    case "0": return;
                    default:
                        Console.WriteLine("Nepoznata opcija.");
                        break;
                }
            }
        }
    }
}
