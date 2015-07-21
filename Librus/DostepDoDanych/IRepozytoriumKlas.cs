using System;
namespace Librus.DostepDoDanych
{
    interface IRepozytoriumKlas
    {
        void Dodaj(Librus.Model.Klasa klasa);
        System.Collections.Generic.IList<Librus.Model.Klasa> PobierzWszystkie();
        Librus.Model.Klasa ZnajdzKlase(string id);
    }
}
