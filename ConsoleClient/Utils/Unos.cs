using System.Globalization;
using Core.Models;
namespace ConsoleClient.Utils
{
    public static class Unos
        //ne pravimo dakle objekat nego zovemo Unos.ProcitajTekst
        //poput kutije sa pomocnim alatima za citanje sa tastature
    {
        public static string ProcitajTekst(string poruka)
            //prima pitanjekoje treba ispisati i vraca ono sto krosnik ukuca
        {
            while (true)
            {
                Console.Write(poruka);//ispise pitanje
                string? unos = Console.ReadLine();//ceka odgovor

                if (!string.IsNullOrWhiteSpace(unos))
                {
                    return unos.Trim(); //skidamo suvisne razmake s pocetka i kraja
                }

                Console.WriteLine("Polje ne smije biti prazno.");
            }
        }

        public static DateTime ProcitajDatum (string poruka)
        {
            while (true)
            {
                Console.Write(poruka + " (dd.MM.yyyy): ");
                string ? unos = Console.ReadLine();
                                                      // CultureInfo.InvariantCulture kaze da se format ne mijenja u zavisnosti jezika, pa radi isto na svakom racunara, zato je dodat System.Globalization
                if (DateTime.TryParseExact(unos,"dd.MM.yyyy",CultureInfo.InvariantCulture,DateTimeStyles.None,out DateTime datum)) //DateTimeStyles.none jer ne zelimo dodatna podesavanja,
                                                                                                                                   //OUT popuni promenjjivu koju smo napravuli
               //korisnik vidi u kom formatu d aunese ispravno
              //TryParseExact pokusava da pretvori ukucani tekst u datum tacno po formatu
              //program u slucaju neuspjeha nece pasti na pogresan unos, nego se samo javi false
                {
                    return datum;
                }

                Console.WriteLine("Neispravan datum, pokusajte ponovo. ");
            }
        }

        public static int ProcitajCijeliBroj(string poruka)
        {
            while (true)
            {
                Console.Write(poruka);
                string? unos = Console.ReadLine();

                if (int.TryParse(unos, out int broj))//pokusava da pretvori ukucani tekst u cijeli broj, ako supije smjeti u rpomeljviu broj svoj rezultat pomocu OUT-a
                {
                    return broj;
                }

                Console.WriteLine("Unesite cijeli broj.");
            }
        }

        public static StatusPosetioca ProcitajStatus() //varca StatusPosetioca, enum, zato smiju samo dvije vriejdnosti dase upisu
        {
            while (true)
            {
                Console.Write("Status (1 - Redovan, 2 - VIP): ");
                string? unos = Console.ReadLine();

                if (unos == "1")
                {
                    return StatusPosetioca.Redovan;
                }

                if (unos == "2")
                {
                    return StatusPosetioca.Vip;
                }

                Console.WriteLine("Unesite 1 ili 2.");
            }
        }
    }
}