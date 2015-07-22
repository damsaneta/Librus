using Librus.Model;
using Librus.Widoki;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librus.DostepDoDanych
{
    public interface IRepozytoriumObecnosci
    {
        IList<ObecnoscUcznia> PobierzPoKlasieIDacie(string klasaId, DateTime data);
        IList<ObecnoscUcznia> PobierzObecnosciPoUczniu(Uczen uczen);
        void Zapisz(IList<ObecnoscUcznia> obecnosci);

    }
}
