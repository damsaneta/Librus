using Librus.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Librus.DostepDoDanych
{
    interface IRepozytoriumKlas
    {
        Task<IList<Klasa>> PobierzWszystkie();
        Klasa ZnajdzKlase(string id);
    }
}
