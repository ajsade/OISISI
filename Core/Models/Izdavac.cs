using Core.Storage.Serialization;
namespace Core.Models
{
    public class Izdavac : ISerializable
    {
      
        public string Sifra { get; set; } = string.Empty;

        public string Naziv {  get; set; } = string.Empty;

        public int? SefId { get; set; } //moze biti prazan

        public List<int> AutorIds { get; set; } = new List<int>();

        public List<string> KnjigaIsbns { get; set; } = new List<string>();

        public string[] ToCSV()
        {
            return new string[]
            {
                Sifra,
                Naziv,
                SefId.HasValue ? SefId.Value.ToString() : "",
                string.Join(";", AutorIds),
                string.Join(";", KnjigaIsbns)
            };
        }

        public void FromCSV(string[] values)
        {
            Sifra = values[0];
            Naziv = values[1];
            SefId = values[2].Length == 0 ? null : int.Parse(values[2]);
            AutorIds = values[3].Length == 0
                ? new List<int>()
                : values[3].Split(';').Select(x => int.Parse(x)).ToList();
            KnjigaIsbns = values[4].Length == 0
                ? new List<string>()
                : values[4].Split(';').ToList();
        }
    }
}