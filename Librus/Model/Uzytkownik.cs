using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librus.Model
{
    public class Uzytkownik
    {
        public Uzytkownik()
        {
                
        }
        public Uzytkownik(string imie, string nazwisko,string email, TypRoli rola)
        {
            this.Imie = imie;
            this.Nazwisko = nazwisko;
            this.Email = email;
            this.Rola =  new Rola(rola);
        }
        public string PelnaNazwa { get { return this.Imie + " " + this.Nazwisko; } }
        public string Email { get; set; }
        public Rola Rola { get;set; }
        public string Imie { get;  set; }
        public string Nazwisko { get; set; }
        public int Id { get; set; }
      

    }
}
