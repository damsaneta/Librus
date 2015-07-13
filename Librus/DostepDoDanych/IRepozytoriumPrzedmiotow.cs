using System;
namespace Librus.DostepDoDanych
{
    interface IRepozytoriumPrzedmiotow
    {
        void Dodaj(Librus.Model.Przedmiot przedmiot);
        System.Collections.Generic.IList<Librus.Model.Przedmiot> PobierzWszystkie();
        Librus.Model.Przedmiot ZnajdzPrzedmiot(string nazwa);
    }
}
