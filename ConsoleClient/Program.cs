using Core.DAO;
using Core.Services;
using ConsoleClient.Controllers;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var posetilacService = new PosetilacService(new PosetilacDao());
//pisemo var da komapjler zakljuci tip
//pravi DAO koji pri nastanku ucita  posetioci.json (desriajlizacijs)
var izdavacService = new IzdavacService(new IzdavacDao());
//predajemo servisu DAO kroz kontruktor
//mjesto gdje sastavkjm koznolu servis dao fajl

//A
var autorService = new AutorService(new AutorDao());
var knjigaService = new KnjigaService(new KnjigaDao());

var posetilacController = new PosetilacController(posetilacService);
var izdavacController = new IzdavacController(izdavacService);

//A
var autorController = new AutorController(autorService);
var knjigaController = new KnjigaController(knjigaService);


bool kraj = false;
while (!kraj)
{
    Console.WriteLine();//preglednosti radi
    Console.WriteLine("=== SAJAM KNJIGA ===");
    Console.WriteLine("1. Posetioci");
    Console.WriteLine("2. Izdavaci");
    Console.WriteLine("3. Autori");
    Console.WriteLine("4. Knjige");
    Console.WriteLine("0. Izlaz");
    Console.Write("Izbor: ");
    string? izbor = Console.ReadLine();
    //ceka da korisnil unese red i pritisne enter, pa vraca ono sto je on ukucao

    switch (izbor)
    {
        case "1":
            posetilacController.Meni();
            break;
        case "2":
            izdavacController.Meni();
            break;
        case "3":
            autorController.Meni();
            break;

        case "4":
            knjigaController.Meni();
            break;
        case "0":
            kraj = true;
            break;
        default:
            Console.WriteLine("Nepoznata opcija. ");
            break;
    }


}