namespace Bufecik.Models
{
    public class Szczegoly
    {
        public int ID { get; set; }
        public int Ilosc { get; set; }
        public int KanapkaID { get; set; }
        public Kanapka? Kanapka { get; set; } = null!;
        public int ZamowienieID { get; set; }
        public Zamowienie? Zamowienie { get; set; } = null!;
    }
}
