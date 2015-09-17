using Librus.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Librus.DostepDoDanych
{
    /// <summary>
    /// Interface IRepozytoriumUzytkownikow
    /// </summary>
    public interface IRepozytoriumUzytkownikow
    {
        Task Dodaj(Uzytkownik uzytkownik);
        Task<Uzytkownik> PobierzPoEmailu(string email);
        Task<IList<Uzytkownik>> PobierzWszystkich();
        Task<Uzytkownik> WyszukajDziecka(string imie, string nazwisko);
        Task<IList<Uczen>> WyszukajPoKlasie(string wzorzec);
        Task<IList<Uzytkownik>> WyszukajPoRoli(string wzorzec);
        Task<IList<Uzytkownik>> WyszukajPoRoliIWzorcu(string wzorzec, string rola);
        Task<IList<Uzytkownik>> WyszukajUzytkownikow(string wzorzec);
        Task<IList<Uzytkownik>> WyszukiwanieDzieci(string tekst);
        Task<Uzytkownik> PobierzUzytkownikaPoId(int id);
    }
}
