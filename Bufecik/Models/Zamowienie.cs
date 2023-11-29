namespace Bufecik.Models
{
    public class Zamowienie
    {
        public int ID { get; set; }
        public DateTime DataZ { get; set; }
        public int KlientID { get; set; }
        public Klient? Klient { get; set; } = null!;
        public int StatusID { get; set; }
        public Status? Status { get; set; } = null!;

        public ICollection<Szczegoly> Szczegolys { get; } = new List<Szczegoly>();

    }
}
