using Librus.DostepDoDanych.Pamiec;
using Librus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librus.Widoki.Administracja
{
     public class UzytkownicyViewModel
    {
        private readonly RepozytoriumUzytkownikowWPamieci repozytorium = new RepozytoriumUzytkownikowWPamieci();
        public IList<Uzytkownik> uzytkownicy { get;  set; }

        public UzytkownicyViewModel()
        {
            this.repozytorium.Dodaj(new Uzytkownik("Aneta", "Brzezińska", "damsA@gmail.com", TypRoli.Rodzic));
            this.repozytorium.Dodaj(new Uzytkownik("Łukasz", "Dams", "ldams@gmail.com", TypRoli.Administrator));
            this.repozytorium.Dodaj(new Uzytkownik("Anna", "Kowlska", "das@gmail.com", TypRoli.Rodzic));
            this.repozytorium.Dodaj(new Uzytkownik("Damian", "Brzeziński", "brzeziu@wp.pl", TypRoli.Nauczyciel));
            uzytkownicy = repozytorium.PobierzWszystkich();
        }
    }
}
