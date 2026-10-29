namespace Core.Models
{
    public class Kupovina
    {
        public string KnjigaIsbn { get; set; } = string.Empty;
        public DateTime DatumKupovine {  get; set; }

        public int Ocena { get; set; }

        public string Komentar { get; set; } = string.Empty;

    }
}