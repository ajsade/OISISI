namespace Core.Models
{
    public class Adresa
    {
        string Ulica { get; set; } = string.Empty;
        string Broj { get; set; } = string.Empty;
        string Grad { get; set; } = string.Empty;
        string Drzava { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{Ulica} {Broj}, {Grad}"; 
        }
    }
  
}
