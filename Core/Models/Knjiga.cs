namespace Core.Models
{
    public class Knjiga
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

        public override string ToString()
        {
            return $"{ISBN} - {Naziv} ({GodinaIzdanja})";
        }
    }
}