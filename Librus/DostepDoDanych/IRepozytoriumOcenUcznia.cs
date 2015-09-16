using Librus.Model;
using System;
using System.Collections.Generic;
namespace Librus.DostepDoDanych
{
    interface IRepozytoriumOcenUcznia
    {
        System.Collections.Generic.IList<Librus.Model.OcenyUcznia> PobierzOcenyPoUczniu(Librus.Model.Uczen uczen);
        System.Collections.Generic.IList<Librus.Model.OcenyUcznia> PobierzPoKlasieIPrzedmiocie(string klasaId, string przedmiotId);
        void Zapisz(System.Collections.Generic.IList<Librus.Model.OcenyUcznia> oceny);
        IList<OcenyUcznia> PobierzOcenyPoUczniuIPrzedmiocie(Uczen uczen, string przedmiotId);

    }
}
