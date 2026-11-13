using System.Runtime.InteropServices;
using Core.DAO;
using Core.Services;
using ConsoleClient.Controllers;

var posetilacService = new PosetilacService(new PosetilacDao());
//pisemo var da komapjler zakljuci tip
//pravi DAO koji pri nastanku ucita  posetioci.json (desriajlizacijs)
var izdavacService = new IzdavacService(new IzdavacDao());
//predajemo servisu DAO kroz kontruktor
//mjesto gdje sastavkjm koznolu servis dao fajl

var posetilacController = new PosetilacController(posetilacService);

bool kraj = false;
while (!kraj)
{
    Console.WriteLine();//preglednosti radi
    Console.WriteLine("=== SAJAM KNJIGA ===");
    Console.WriteLine("1. Posetioci");
    Console.WriteLine("2. Izdavaci");
    Console.WriteLine("0. Izlaz");
    Console.WriteLine("Izbor: ");
    string? izbor = Console.ReadLine();
    //ceka da korisnil unese red i pritisne enter, pa vraca ono sto je on ukucao

    switch (izbor)
    {
        case "1":
            posetilacController.PrikaziSve();
            break;
        case "2":
            Console.WriteLine("(izdavaci - uskoro)");
            break;
        case "0":
            kraj = true;
            break;
        default:
            Console.WriteLine("Nepoznata opcija. ");
            break;
    }


}