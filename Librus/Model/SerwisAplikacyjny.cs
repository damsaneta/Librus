using Librus.DostepDoDanych;
using Librus.DostepDoDanych.BazaDanych;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librus.Model
{
    public class SerwisAplikacyjny
    {
        private readonly IRepozytoriumUzytkownikow repozytoriumUzytkownikow;
        private readonly IRepozytoriumPrzedmiotow repozytoriumPrzedmiotow;
        private readonly IRepozytoriumKlas repozytoriumKlas;
        private readonly IRepozytoriumOcenUcznia repozytoriumOcenUcznia; 
        public SerwisAplikacyjny(string connectionString)
        {
            this.repozytoriumUzytkownikow = new RepozytoriumUzytkownikow(connectionString);
            this.repozytoriumPrzedmiotow = new RepozytoriumPrzedmiotow(connectionString);
            this.repozytoriumKlas = new RepozytoriumKlas(connectionString);
            this.repozytoriumOcenUcznia = new RepozytoriumOcenUcznia(connectionString);

        }
    }
}
