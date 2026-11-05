namespace Core.Models
{
	public class Autor
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
	}
}