using Librus.Model;
using System;
namespace Librus.DostepDoDanych
{
    interface IRepozytoriumUzytkownikow
    {
        void Dodaj(Librus.Model.Uzytkownik uzytkownik);
        Librus.Model.Uzytkownik PobierzPoEmailu(string email);
        System.Collections.Generic.IList<Librus.Model.Uzytkownik> PobierzWszystkich();
        Librus.Model.Uzytkownik WyszukajDziecka(string imie, string nazwisko);
        System.Collections.Generic.IList<Librus.Model.Uczen> WyszukajPoKlasie(string wzorzec);
        System.Collections.Generic.IList<Librus.Model.Uzytkownik> WyszukajPoRoli(string wzorzec);
        System.Collections.Generic.IList<Librus.Model.Uzytkownik> WyszukajPoRoliIWzorcu(string wzorzec, string rola);
        System.Collections.Generic.IList<Librus.Model.Uzytkownik> WyszukajUzytkownikow(string wzorzec);
        System.Collections.Generic.IList<Librus.Model.Uzytkownik> WyszukiwanieDzieci(string tekst);
        Uzytkownik PobierzUzytkownikaPoId(int id);
    }
}
