using System.Globalization;
using Core.Storage.Serialization;
namespace Core.Models
{
    public class Posetilac : ISerializable
    {
        public int Id { get; set; } //dodala sam ga kao neki primarni kljuc,
                                    //jedinstveno se prepoznaje posjetilac
        public string Ime { get; set; } = string.Empty;
        public string Prezime { get; set; } = string.Empty;

        public DateTime DatumRodjenja { get; set; }
        
        public Adresa Adresa { get; set; } = new Adresa();

        public string KontaktTelefon { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string BrojClanskeKarte { get; set; } = string.Empty;

        public int TrenutnaGodinaClanstva { get; set; }

        public StatusPosetioca Status { get; set; }

        public double ProsecnaOcena { get; set; }

        public List<string> ListaZelja { get; set; } = new List<string>();

        public List<Kupovina> Kupovine { get; set; } = new List<Kupovina>();

        public string[] ToCSV()
        {
            return new string[]
            {
                Id.ToString(),
                BrojClanskeKarte,
                Ime,
                Prezime,
                DatumRodjenja.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                Adresa.Ulica,
                Adresa.Broj,
                Adresa.Grad,
                Adresa.Drzava,
                KontaktTelefon,
                Email,
                TrenutnaGodinaClanstva.ToString(),
                Status.ToString(),
                ProsecnaOcena.ToString(CultureInfo.InvariantCulture),
                string.Join(";", ListaZelja)
            };
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            BrojClanskeKarte = values[1];
            Ime = values[2];
            Prezime = values[3];
            DatumRodjenja = DateTime.ParseExact(values[4], "yyyy-MM-dd", CultureInfo.InvariantCulture);

            Adresa = new Adresa
            {
                Ulica = values[5],
                Broj = values[6],
                Grad = values[7],
                Drzava = values[8]
            };

            KontaktTelefon = values[9];
            Email = values[10];
            TrenutnaGodinaClanstva = int.Parse(values[11]);
            Status = Enum.Parse<StatusPosetioca>(values[12]);
            ProsecnaOcena = double.Parse(values[13], CultureInfo.InvariantCulture);
            ListaZelja = values[14].Length == 0 ? new List<string>() : values[14].Split(';').ToList();
        }
    }

}