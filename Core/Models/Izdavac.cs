namespace Core.Models
{
    public class Izdavac
    {
        /*Списак књига*/
        public string Sifra { get; set; } = string.Empty;

        public string Naziv {  get; set; } = string.Empty;

        public int? SefId { get; set; } //moze biti prazan

        public List<int> AutorIds { get; set; } = new List<int>();

        public List<string> KnjigaIsbns { get; set; } = new List<string>();
    }
}