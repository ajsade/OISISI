namespace Core.Models
{
    public class Adresa
    {
        public string Ulica { get; set; } = string.Empty;
        public string Broj { get; set; } = string.Empty;
        public string Grad { get; set; } = string.Empty;
        public string Drzava { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{Ulica} {Broj}, {Grad}"; 
        }
    }
  
}
