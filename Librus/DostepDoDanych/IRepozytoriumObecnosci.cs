﻿using Librus.Model;
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
        Task<IList<ObecnoscUcznia>> PobierzPoKlasieIDacie(string klasaId, DateTime data);
        Task<IList<ObecnoscUcznia>> PobierzObecnosciPoUczniu(Uczen uczen);
        IList<ObecnoscUcznia> PobierzObecnoscPoUczniuIDacie(Uczen uczen, DateTime data);
        Task Zapisz(IList<ObecnoscUcznia> obecnosci);

    }
}
