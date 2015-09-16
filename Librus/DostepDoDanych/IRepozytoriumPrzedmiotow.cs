using Librus.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Librus.DostepDoDanych
{
    interface IRepozytoriumPrzedmiotow
    {
        Task<IList<Przedmiot>> PobierzWszystkie();
        Przedmiot ZnajdzPrzedmiot(string id);
    }
}
