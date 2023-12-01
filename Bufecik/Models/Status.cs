namespace Bufecik.Models
{
    public class Status
    {
        public int ID { get; set; }
        public string Nazwa { get; set; }
        public ICollection<Zamowienie> Zamowienies { get; } = new List<Zamowienie>();
    }
}
