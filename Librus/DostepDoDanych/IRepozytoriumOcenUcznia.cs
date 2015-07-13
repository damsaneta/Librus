using System;
namespace Librus.DostepDoDanych
{
    interface IRepozytoriumOcenUcznia
    {
        System.Collections.Generic.IList<Librus.Model.OcenyUcznia> PobierzOcenyPoUczniu(Librus.Model.Uczen uczen);
        System.Collections.Generic.IList<Librus.Model.OcenyUcznia> PobierzPoKlasieIPrzedmiocie(string klasa, string przedmiot);
        void Zapisz(System.Collections.Generic.IList<Librus.Model.OcenyUcznia> oceny);
    }
}
