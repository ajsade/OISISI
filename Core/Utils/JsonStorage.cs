using System.Text.Json;
//oba biblioteka dolazi uz .NET i zna da pretvara objekte u JSON tekst i nazad.
using System.Text.Json.Serialization;
//isto, samo sadzi i dodatne alate.(koristicu da bih status upisala kao rijec "Vip"
//a ne kao broj)


namespace Core.Utils
{
    public static class JsonStorage //static jer nema objekte
    {
        private static readonly JsonSerializerOptions Options = new JsonSerializerOptions
        //vidiljiva samo u JsonStorage-u
        //vrijednost se postavi jednom i poslije se ne moze mijenjati
        //JsonSerializerOptions je tip iz biblioteke System.Text.Json

        {
            WriteIndented = true, //pravi lijepo uvucen JSON
            Converters = { new JsonStringEnumConverter() } //bez njega bi nam enum bio status:1,2...
        };
        private static string DataFolder =>
            //string jer je kao putanja do foldera,DataFolder je ime koje smo odabrali
            // ,,=>"  dalje ili vraca
            AppContext.BaseDirectory + "Data" + Path.DirectorySeparatorChar;
        //AppContext.BaseDirectory je folder u kom se nalazi pokrenuti program.
        //"Data" je ime foldera koji cemo napraviti
        //Path.DirectorySeparatorChar znak koji razdvaja foldere u putanji

        public static List<T> Load<T>(string fileName) //publci da mogu zvati DAO-i
        {//Load je ime metode
            //Tip popunjavamo pri pozivu metode tek
            string path = DataFolder + fileName;
            //pravi punu putanju, tj spaja folder i ime fajla, path je ime nove promjelnjive
            if (!File.Exists(path))
            {
                return new List<T>();//ako fajl ne postoji vracamo praznu listu i tu se metoda zavrsav
            }
            string json = File.ReadAllText(path);
            //smjesta cijeli sadrzaj fajla u promjenljivu json, samo tekst imamo, ne objekte
            return JsonSerializer.Deserialize<List<T>>(json, Options) ?? new List<T>();  //?? se cita=ako je prethodno prazno(null) uzmi ovo
            //Deserialize pretvara tekst u objekte, u zagradama mu damo tekst i nasa podesavanja(json,optons)
        }

        public static void Save<T>(string fileName, List<T> items)
        {                          //prima ime fajla u koji se upisuje, prima listu objketa koje treba upisati,
                                   //a mi smo mu dale ime items unutar metode
                                   //za razliku od Loada, Save nam je void
            Directory.CreateDirectory(DataFolder); //pravimo folder ako ga nema,u slucaju 1.upisa, ako ga ima nece puci svakako
            string json = JsonSerializer.Serialize(items, Options);
            //serijalizacija : uzme listu objekata (items) i onda je pretvor u JSON tekst
            //pa se za rezultat dobije ibican tekst u promjneljivoj json
            File.WriteAllText(DataFolder + fileName, json);//i sad upisemo taj tekst u fajl, prvi dio nam je outanja, a drugi je tekst koji
            //se upisuje
            //ako fajl vec postoji, prepise ga novim sadrzajem 
        }
    }
}