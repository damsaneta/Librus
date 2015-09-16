using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librus.Model
{
    public abstract class Uzytkownik
    {
        protected Uzytkownik()
        {

        }

        protected Uzytkownik(string imie, string nazwisko, string email, Rola rola, string haslo)
        {
            this.Imie = imie;
            this.Nazwisko = nazwisko;
            this.Email = email;
            this.Rola = rola;
            this.Haslo = haslo;
        }

        public string PelnaNazwa { get { return this.Imie + " " + this.Nazwisko; } }

        public string Email { get; set; }

        public Rola Rola { get; set; }

        public string Imie { get; set; }

        public string Nazwisko { get; set; }

        public int Id { get; set; }

        public string Haslo { get; set; }

    }
}
