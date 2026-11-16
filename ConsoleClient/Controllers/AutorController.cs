using Core.Models;
using Core.Services;
using ConsoleClient.Utils;

namespace ConsoleClient.Controllers
{
    public class AutorController
    {
        private readonly AutorService _service;

        public AutorController(AutorService service)
        {
            _service = service;
        }

        public void PrikaziSve()
        {
            List<Autor> autori = _service.GetAll();

            if (autori.Count == 0)
            {
                Console.WriteLine("Nema autora.");
                return;
            }

            foreach (Autor a in autori)
            {
                Console.WriteLine($"Id: {a.Id}");
                Console.WriteLine($"  Ime i prezime: {a.Ime} {a.Prezime}");
                Console.WriteLine($"  Datum rodjenja: {a.DatumRodjenja:dd.MM.yyyy.}");
                Console.WriteLine($"  Adresa: {a.Adresa}, {a.Adresa.Drzava}");
                Console.WriteLine($"  Telefon: {a.Telefon}");
                Console.WriteLine($"  E-mail: {a.Email}");
                Console.WriteLine($"  Broj licne karte: {a.BrojLicneKarte}");
                Console.WriteLine($"  Godine iskustva: {a.GodineIskustva}");
                Console.WriteLine($"  ISBN knjiga: {string.Join(", ", a.ISBNKnjiga)}");
                Console.WriteLine();
            }
        }

        private Autor UnesiPodatke(int id = 0, List<string>? knjige = null)
        {
            Autor autor = new Autor();
            autor.Id = id;
            autor.ISBNKnjiga = knjige == null
                ? new List<string>()
                : new List<string>(knjige);

            autor.Ime = Unos.ProcitajTekst("Ime: ");
            autor.Prezime = Unos.ProcitajTekst("Prezime: ");
            autor.DatumRodjenja = Unos.ProcitajDatum("Datum rodjenja");

            autor.Adresa.Ulica = Unos.ProcitajTekst("Ulica: ");
            autor.Adresa.Broj = Unos.ProcitajTekst("Broj: ");
            autor.Adresa.Grad = Unos.ProcitajTekst("Grad: ");
            autor.Adresa.Drzava = Unos.ProcitajTekst("Drzava: ");

            autor.Telefon = Unos.ProcitajTekst("Telefon: ");
            autor.Email = Unos.ProcitajTekst("E-mail: ");
            autor.BrojLicneKarte = Unos.ProcitajTekst("Broj licne karte: ");
            autor.GodineIskustva = Unos.ProcitajCijeliBroj("Godine iskustva: ");

            return autor;
        }

        public void Dodaj()
        {
            Console.WriteLine("--- Novi autor ---");

            Autor autor = UnesiPodatke();
            string? greska = _service.Add(autor);

            Console.WriteLine(greska == null
                ? "Autor je dodat."
                : "Greska: " + greska);
        }

        public void Izmijeni()
        {
            Console.WriteLine("--- Izmena autora ---");

            int id = Unos.ProcitajCijeliBroj("Id autora: ");
            Autor? autor = _service.GetById(id);

            if (autor == null)
            {
                Console.WriteLine("Autor nije pronadjen.");
                return;
            }

            Console.WriteLine($"Menjate: {autor.Ime} {autor.Prezime}");

            Autor izmenjen = UnesiPodatke(autor.Id, autor.ISBNKnjiga);
            string? greska = _service.Update(izmenjen);

            Console.WriteLine(greska == null
                ? "Autor je izmenjen."
                : "Greska: " + greska);
        }

        public void Obrisi()
        {
            Console.WriteLine("--- Brisanje autora ---");

            int id = Unos.ProcitajCijeliBroj("Id autora: ");
            Autor? autor = _service.GetById(id);

            if (autor == null)
            {
                Console.WriteLine("Autor nije pronadjen.");
                return;
            }

            bool potvrda = Unos.ProcitajDaNe(
                $"Da li zelite da obrisete autora {autor.Ime} {autor.Prezime}?");

            if (!potvrda)
            {
                Console.WriteLine("Brisanje otkazano.");
                return;
            }

            Console.WriteLine(_service.Delete(id)
                ? "Autor je obrisan."
                : "Brisanje nije uspelo.");
        }

        public void Meni()
        {
            while (true)
            {
                Console.WriteLine("\n--- AUTORI ---");
                Console.WriteLine("1. Prikazi sve");
                Console.WriteLine("2. Dodaj");
                Console.WriteLine("3. Izmeni");
                Console.WriteLine("4. Obrisi");
                Console.WriteLine("0. Nazad");
                Console.Write("Izbor: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        PrikaziSve();
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
                        return;
                    default:
                        Console.WriteLine("Nepoznata opcija.");
                        break;
                }
            }
        }
    }
}
