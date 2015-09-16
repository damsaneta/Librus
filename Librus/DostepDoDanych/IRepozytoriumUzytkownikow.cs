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
        /// <summary>
        /// Dodajs the specified uzytkownik.
        /// </summary>
        /// <param name="uzytkownik">The uzytkownik.</param>
        Task Dodaj(Uzytkownik uzytkownik);
        /// <summary>
        /// Pobierzs the po emailu.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>Librus.Model.Uzytkownik.</returns>
        Task<Uzytkownik> PobierzPoEmailu(string email);
        /// <summary>
        /// Pobierzs the wszystkich.
        /// </summary>
        /// <returns>System.Collections.Generic.IList&lt;Librus.Model.Uzytkownik&gt;.</returns>
        Task<IList<Librus.Model.Uzytkownik>> PobierzWszystkich();
        /// <summary>
        /// Wyszukajs the dziecka.
        /// </summary>
        /// <param name="imie">The imie.</param>
        /// <param name="nazwisko">The nazwisko.</param>
        /// <returns>Librus.Model.Uzytkownik.</returns>
        Task<Uzytkownik> WyszukajDziecka(string imie, string nazwisko);
        /// <summary>
        /// Wyszukajs the po klasie.
        /// </summary>
        /// <param name="wzorzec">The wzorzec.</param>
        /// <returns>System.Collections.Generic.IList&lt;Librus.Model.Uczen&gt;.</returns>
        Task<IList<Uczen>> WyszukajPoKlasie(string wzorzec);
        /// <summary>
        /// Wyszukajs the po roli.
        /// </summary>
        /// <param name="wzorzec">The wzorzec.</param>
        /// <returns>System.Collections.Generic.IList&lt;Librus.Model.Uzytkownik&gt;.</returns>
        Task<IList<Uzytkownik>> WyszukajPoRoli(string wzorzec);
        /// <summary>
        /// Wyszukajs the po roli i wzorcu.
        /// </summary>
        /// <param name="wzorzec">The wzorzec.</param>
        /// <param name="rola">The rola.</param>
        /// <returns>System.Collections.Generic.IList&lt;Librus.Model.Uzytkownik&gt;.</returns>
        Task<IList<Uzytkownik>> WyszukajPoRoliIWzorcu(string wzorzec, string rola);
        /// <summary>
        /// Wyszukajs the uzytkownikow.
        /// </summary>
        /// <param name="wzorzec">The wzorzec.</param>
        /// <returns>System.Collections.Generic.IList&lt;Librus.Model.Uzytkownik&gt;.</returns>
        Task<IList<Uzytkownik>> WyszukajUzytkownikow(string wzorzec);
        /// <summary>
        /// Wyszukiwanies the dzieci.
        /// </summary>
        /// <param name="tekst">The tekst.</param>
        /// <returns>System.Collections.Generic.IList&lt;Librus.Model.Uzytkownik&gt;.</returns>
        Task<IList<Uzytkownik>> WyszukiwanieDzieci(string tekst);
        /// <summary>
        /// Pobierzs the uzytkownika po identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Uzytkownik.</returns>
        Task<Uzytkownik> PobierzUzytkownikaPoId(int id);
    }
}
