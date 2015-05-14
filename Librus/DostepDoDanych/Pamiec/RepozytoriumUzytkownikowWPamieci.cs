using Librus.Model;
using Librus.Widoki.Administracja;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librus.DostepDoDanych.Pamiec
{
    public class RepozytoriumUzytkownikowWPamieci
    {
        private static readonly Dictionary<int, Uzytkownik> uzytkownicy = new Dictionary<int, Uzytkownik>();
        private static readonly Dictionary<string, int> emaileUzytkownika = new Dictionary<string, int>();

        private static int ostatnieID = 0;
        public IList<Uzytkownik> PobierzWszystkich()
        {
            return uzytkownicy.Values.ToList();
        }
        
        public void Dodaj(Uzytkownik uzytkownik)
        {
            ostatnieID++;
            uzytkownik.Id = ostatnieID;
            uzytkownicy.Add(ostatnieID, uzytkownik);
            emaileUzytkownika.Add(uzytkownik.Email,uzytkownik.Id);
        }
        public IList<Uzytkownik> WyszukajUzytkownikow(string wzorzec)
        {
           return uzytkownicy.Values.Where(x => (x.Imie.StartsWith(wzorzec, StringComparison.CurrentCultureIgnoreCase))
               || (x.Nazwisko.StartsWith(wzorzec, StringComparison.CurrentCultureIgnoreCase))
               || (x.Email.StartsWith(wzorzec,StringComparison.CurrentCultureIgnoreCase))
               ).ToList();
        }
        public Uzytkownik PobierzPoEmailu(string email)
        {
           if(emaileUzytkownika.ContainsKey(email))
           {
               int id = emaileUzytkownika[email];
               return uzytkownicy[id];
           }
            return null;
        }

        public IList<Uzytkownik> WyszukajPoRoli(string wzorzec)
        {
            return uzytkownicy.Values.Where(x => x.Rola.ToString().StartsWith(wzorzec, StringComparison.CurrentCultureIgnoreCase)).ToList();
        }
    }
}
