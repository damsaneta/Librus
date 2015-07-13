using Librus.Model;
using Librus.Widoki.Administracja;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librus.DostepDoDanych.Pamiec
{
    public class RepozytoriumUzytkownikowWPamieci : Librus.DostepDoDanych.IRepozytoriumUzytkownikow
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
            emaileUzytkownika.Add(uzytkownik.Email, uzytkownik.Id);
        }
        public IList<Uzytkownik> WyszukajUzytkownikow(string wzorzec)
        {
            return uzytkownicy.Values.Where(x => (x.Imie.StartsWith(wzorzec, StringComparison.CurrentCultureIgnoreCase))
                || (x.Nazwisko.StartsWith(wzorzec, StringComparison.CurrentCultureIgnoreCase))
                || (x.Email.StartsWith(wzorzec, StringComparison.CurrentCultureIgnoreCase))
                ).ToList();
        }
        public IList<Uzytkownik> WyszukajPoRoli(string wzorzec)
        {
            return uzytkownicy.Values.Where(x => x.Rola.ToString().StartsWith(wzorzec, StringComparison.CurrentCultureIgnoreCase)).ToList();
        }

        public Uzytkownik WyszukajDziecka(string imie, string nazwisko)
        {
            IList<Uzytkownik> lista = uzytkownicy.Values.Where(x => (x.Rola.ToString().StartsWith(TypRoli.Uczen.ToString(), StringComparison.CurrentCultureIgnoreCase))).ToList();
            foreach (Uzytkownik u in lista)
            {
                if (u.Imie == imie && u.Nazwisko == nazwisko)
                {
                    return u;
                }

            }
            return null;

        }
        public IList<Uzytkownik> WyszukajPoRoliIWzorcu(string wzorzec, string rola)
        {
            return uzytkownicy.Values.Where(x => ((x.Imie.StartsWith(wzorzec, StringComparison.CurrentCultureIgnoreCase))
               || (x.Nazwisko.StartsWith(wzorzec, StringComparison.CurrentCultureIgnoreCase))
               || (x.Email.StartsWith(wzorzec, StringComparison.CurrentCultureIgnoreCase)))
               && (x.Rola.ToString().StartsWith(rola, StringComparison.CurrentCultureIgnoreCase))
               ).ToList();
        }

        public IList<Uczen> WyszukajPoKlasie(string wzorzec)
        {

            List<Uczen> uczniowie = new List<Uczen>();
            IList<Uzytkownik> lista = WyszukajPoRoli("Uczen");
            foreach (var x in lista)
            {
                uczniowie.Add((Uczen)x);
            }

            return uczniowie.FindAll(x => (x.Klasa.Nazwa.StartsWith(wzorzec, StringComparison.CurrentCultureIgnoreCase))).ToList();

        }
        public Uzytkownik PobierzPoEmailu(string email)
        {
            if (emaileUzytkownika.ContainsKey(email))
            {
                int id = emaileUzytkownika[email];
                return uzytkownicy[id];
            }
            return null;
        }
        public IList<Uzytkownik> WyszukiwanieDzieci(string tekst)
        {
            IList<Uzytkownik> dzieci = new List<Uzytkownik>();
            string slowo = tekst;
            string brakDzieci = string.Empty;
            string[] tab = slowo.Split(new char[] { ',' });
            for (int i = 0; i < tab.Length; i++)
            {
                tab[i] = tab[i].Trim();
            }
            foreach (string s in tab)
            {
                string[] wynik = s.Split(new char[] { ' ' });
                string imie = wynik[0];
                string nazwisko = wynik[1];
                Uzytkownik dziecko = WyszukajDziecka(imie, nazwisko);
                if (dziecko != null)
                {
                    dzieci.Add(dziecko);

                }

            }
            return dzieci;


        }

    }
}
