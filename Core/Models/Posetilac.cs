namespace Core.Models
{
    public class Posetilac
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
    }

}