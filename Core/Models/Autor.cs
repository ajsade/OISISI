using Core.Storage.Serialization;
using System.Globalization;


namespace Core.Models
{
	public class Autor : ISerializable
	{
		public string Ime { get; set; } = "";
		public string Prezime { get; set; } = "";
		public DateTime DatumRodjenja { get; set; }
		public Adresa Adresa { get; set; } = new Adresa();
		public string Telefon { get; set; } = "";
		public string Email { get; set; } = "";
		public string BrojLicneKarte { get; set; } = "";
		public int GodineIskustva { get; set; }
		public int Id { get; set; }
		public List<string> ISBNKnjiga { get; set; } = new();

        public string[] ToCSV()
        {
            return new string[]
            {
        Id.ToString(),
        Ime,
        Prezime,
        DatumRodjenja.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
        Adresa.Ulica,
        Adresa.Broj,
        Adresa.Grad,
        Adresa.Drzava,
        Telefon,
        Email,
        BrojLicneKarte,
        GodineIskustva.ToString(),
        string.Join(";", ISBNKnjiga)
            };
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Ime = values[1];
            Prezime = values[2];
            DatumRodjenja = DateTime.ParseExact(
                values[3], "yyyy-MM-dd", CultureInfo.InvariantCulture);

            Adresa = new Adresa
            {
                Ulica = values[4],
                Broj = values[5],
                Grad = values[6],
                Drzava = values[7]
            };

            Telefon = values[8];
            Email = values[9];
            BrojLicneKarte = values[10];
            GodineIskustva = int.Parse(values[11]);

            ISBNKnjiga = string.IsNullOrEmpty(values[12])
                ? new List<string>()
                : values[12].Split(';').ToList();
        }
    }
}