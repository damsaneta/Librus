using Librus.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Librus.DostepDoDanych
{
    interface IRepozytoriumOcenUcznia
    {
        Task<IList<OcenyUcznia>> PobierzOcenyPoUczniu(Uczen uczen);
        Task<IList<OcenyUcznia>> PobierzPoKlasieIPrzedmiocie(string klasaId, string przedmiotId);
        Task Zapisz(System.Collections.Generic.IList<Librus.Model.OcenyUcznia> oceny);
        IList<OcenyUcznia> PobierzOcenyPoUczniuIPrzedmiocie(Uczen uczen, string przedmiotId);

    }
}
