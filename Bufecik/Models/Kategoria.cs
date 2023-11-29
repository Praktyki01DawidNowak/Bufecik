namespace Bufecik.Models
{
    public class Kategoria
    {
        public int ID { get; set; }
        public string Nazwa { get; set; }

        public ICollection<Kanapka> Kanapkas { get; } = new List<Kanapka>();
    }
}
