namespace Bufecik.Models
{
    public class Kanapka
    {
        public int ID { get; set; }
        public string Nazwa { get; set; }
        public decimal Cena { get; set; }
        public string Skladniki { get; set; }
        public string Zdjecie { get; set; }

        public int KategoriaID { get; set; }
        public Kategoria? Kategoria { get; set; } = null!;

        public ICollection<Szczegoly> Szczegolys { get; } = new List<Szczegoly>();
        
    }
}
