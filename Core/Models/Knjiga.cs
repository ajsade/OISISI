using System.Globalization;
using Core.Storage.Serialization;

namespace Core.Models
{
    public class Knjiga : ISerializable
    {
        public string ISBN { get; set; } = string.Empty;
        public string Naziv { get; set; } = string.Empty;
        public string Zanr { get; set; } = string.Empty;
        public int GodinaIzdanja { get; set; }
        public decimal Cena { get; set; }
        public int BrojStrana { get; set; }

        public List<int> AutorIds { get; set; } = new();
        public string SifraIzdavaca { get; set; } = string.Empty;

        public List<int> KupiliPosetiociIds { get; set; } = new();
        public List<int> ListaZeljaPosetiociIds { get; set; } = new();

        public string[] ToCSV()
        {
            return new string[]
            {
        ISBN,
        Naziv,
        Zanr,
        GodinaIzdanja.ToString(),
        Cena.ToString(CultureInfo.InvariantCulture),
        BrojStrana.ToString(),
        string.Join(";", AutorIds),
        SifraIzdavaca,
        string.Join(";", KupiliPosetiociIds),
        string.Join(";", ListaZeljaPosetiociIds)
            };
        }

        public void FromCSV(string[] values)
        {
            ISBN = values[0];
            Naziv = values[1];
            Zanr = values[2];
            GodinaIzdanja = int.Parse(values[3]);
            Cena = decimal.Parse(values[4], CultureInfo.InvariantCulture);
            BrojStrana = int.Parse(values[5]);

            AutorIds = string.IsNullOrEmpty(values[6])
                ? new List<int>()
                : values[6].Split(';').Select(int.Parse).ToList();

            SifraIzdavaca = values[7];

            KupiliPosetiociIds = string.IsNullOrEmpty(values[8])
                ? new List<int>()
                : values[8].Split(';').Select(int.Parse).ToList();

            ListaZeljaPosetiociIds = string.IsNullOrEmpty(values[9])
                ? new List<int>()
                : values[9].Split(';').Select(int.Parse).ToList();
        }

        public override string ToString()
        {
            return $"{ISBN} - {Naziv} ({GodinaIzdanja})";
        }
    }
}