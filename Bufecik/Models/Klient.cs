using Microsoft.AspNetCore.Identity;

namespace Bufecik.Models
{
    public class Klient
    {
        public int ID { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Telefon { get; set; }
        public string UserID { get; set; }
        public IdentityUser? User { get; set; }

        public ICollection<Zamowienie> Zamowienies { get;} = new List<Zamowienie>();
    }
}
