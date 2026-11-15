using Core.Models;
using Core.Services;
using ConsoleClient.Utils;

namespace ConsoleClient.Controllers
{
    public class IzdavacController
    {
        private readonly IzdavacService _service;

        //konstruktor prima servis izvana i cuva ga u _service
        //controller zna sammo za servis, nikada za DAO
        public IzdavacController(IzdavacService service)
        {
            _service = service;
        }

        public void PrikaziSve()
            //trazi od posjetioca sve izdavace
        {
            List<Izdavac> izdavaci = _service.GetAll();

            if(izdavaci.Count == 0)
            {
                Console.WriteLine("Nema izdavaca.");
                //ako ih nema prekida se metoda
                return;
            }

            foreach(Izdavac i in izdavaci)
            {
                //ako ima onda foric prodje kroz svakog izdavaca i ipsie mu sifru i naziv
                Console.WriteLine($"{i.Sifra} | {i.Naziv}");
            }
        }

        public void Dodaj()
        {
            Console.WriteLine("--- Novi izdavac ---");

            Izdavac izdavac = new Izdavac();//pravi se prazan objekat
            izdavac.Sifra = Unos.ProcitajTekst("Sifra: ");//pita korisnika da unese nesto
            izdavac.Naziv = Unos.ProcitajTekst("Naziv: ");

            string? greska = _service.Add(izdavac);
            //salje izdavaca servisu, a on vraca tekst ako nesto nije u redu
            if (greska != null)
            {
                Console.WriteLine("Greska: " + greska);
                return;
            }

            Console.WriteLine("Izdavac je dodat.");
        }

        public void Izmijeni()
        {
            Console.WriteLine("--- Izmjena izdavaca ---");

            string sifra = Unos.ProcitajTekst("Sifra izdavaca: ");
            Izdavac? izdavac = _service.GetBySifra(sifra);
            //vraca isti objekat koji zivi u listi u DAO-u
            //Da ga mijenjamo direktno, a servis zatim odbije izmjenu,
             //   memorija bi ipak već imala promjenu.Zato pravimo izmijenjen, a
              //  stari ostaje netaknut dok servis ne odobri.



            if (izdavac == null)
            {
                Console.WriteLine("Izdavac nije pronadjen.");
                return;
            }

            Console.WriteLine($"Mijenjate: {izdavac.Naziv}");

            //sifra mora ostati ista jer po jjoj update u dao nalazi koga da zamijeni
            Izdavac izmijenjen = new Izdavac();
            izmijenjen.Sifra = izdavac.Sifra;
            izmijenjen.SefId = izdavac.SefId;
            izmijenjen.AutorIds = izdavac.AutorIds;
            izmijenjen.KnjigaIsbns = izdavac.KnjigaIsbns;

            izmijenjen.Naziv = Unos.ProcitajTekst("Naziv: ");
            //cita naziv

            string? greska = _service.Update(izmijenjen);
            //salje servisu a servis provjeri da nizv nije parazn i da izdavac s tom sifron postoji
            //pa tek onda zove dao
            if (greska != null)
            {
                Console.WriteLine("Greska: " + greska);
                return;
            }

            Console.WriteLine("Izdavac je izmijenjen.");
        }

        public void Obrisi()
        {
            Console.WriteLine("--- Brisanje izdavaca ---");

            string sifra = Unos.ProcitajTekst("Sifra izdavaca: ");
            //pronadje izdavac preko servisa
            Izdavac? izdavac = _service.GetBySifra(sifra);

            if (izdavac == null)
            {
                Console.WriteLine("Izdavac nije pronadjen.");
                return;
            }


            Console.Write($"Da li ste sigurni da zelite da obrisete {izdavac.Naziv}? (d/n): ");
            string? odgovor = Console.ReadLine();

            if (odgovor != "d")
            {
                Console.WriteLine("Brisanje otkazano.");
                return;
            }

            bool obrisan = _service.Delete(sifra);
        
            if (obrisan)
            {
                Console.WriteLine("Izdavac je obrisan.");
            }
            else
            {
                Console.WriteLine("Brisanje nije uspjelo.");
            }
        }

        public void Meni()
        {
            bool nazad = false;
            while (!nazad)
            {
                Console.WriteLine();
                Console.WriteLine("--- IZDAVACI ---");
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
                        nazad = true;
                        break;
                    default:
                        Console.WriteLine("Nepoznata opcija.");
                        break;
                }
            }
        }

    }
}