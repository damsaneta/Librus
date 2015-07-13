using Librus.DostepDoDanych;
using Librus.DostepDoDanych.Pamiec;
using Librus.Model;
using Librus.Widoki;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Librus
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly RepozytoriumUzytkownikowWPamieci repozytorium = new RepozytoriumUzytkownikowWPamieci();
        private readonly RepozytoriumObecnosciWPamieci repozytoriumObecnosci = new RepozytoriumObecnosciWPamieci();
        private readonly RepozytoriumKlas klasy = new RepozytoriumKlas();
        private readonly IRepozytoriumPrzedmiotow repozytoriumPrzedmiotow = new RepozytoriumPrzedmiotow();
        private readonly IRepozytoriumOcenUcznia repozytoriumOcen = new RepozytoriumOcenUcznia();
        public App()
        {
            repozytoriumPrzedmiotow.Dodaj(new Przedmiot("Angielski"));
            repozytoriumPrzedmiotow.Dodaj(new Przedmiot("Biologia"));
            repozytoriumPrzedmiotow.Dodaj(new Przedmiot("Fizyka"));
            repozytoriumPrzedmiotow.Dodaj(new Przedmiot("Matematyka"));
            

            IList<ObecnoscUcznia> lista = new List<ObecnoscUcznia>();
            this.klasy.Dodaj(new Klasa("IA"));
            this.klasy.Dodaj(new Klasa("IB"));
            this.klasy.Dodaj(new Klasa("IC"));
            Uczen u3 = new Uczen("Alan", "Jamróz", "jamr@gmail.com", "AlaJam", new Klasa("IA"));
            Uczen u1 = new Uczen("Anna", "Kowalska", "das@gmail.com", "AnnKow", new Klasa("IA"));
            Uczen u2 = new Uczen("Michalina", "Nowak", "mich@gmail.com", "MicNow", new Klasa("IA"));
            Uczen u4 = new Uczen("Adam", "Boroch", "asdf@gmail.com", "AdaBor", new Klasa("IC"));
            Uczen u5 = new Uczen("Iza", "Kania", "fdsfsd@gmail.com", "IzaKan", new Klasa("IC"));
            this.klasy.PobierzWszystkie();
            this.repozytorium.Dodaj(new Rodzic("Aneta", "Dams", "damsA@gmail.com", "AneDam", null));
            this.repozytorium.Dodaj(new Administrator("Łukasz", "Nowak", "ld@gmail.com", "ŁukNow"));
            this.repozytorium.Dodaj(u1);
            this.repozytorium.Dodaj(u2);
            this.repozytorium.Dodaj(new Uczen("Janusz", "Nowak", "jan@gmail.com", "JanNow", new Klasa("IB")));
            this.repozytorium.Dodaj(new Uczen("Alina", "Jawor", "jawor@gmail.com", "AliJaw", new Klasa("IB")));
            this.repozytorium.Dodaj(new Uczen("Martyna", "Dudziec", "dudz@gmail.com", "MarDus", new Klasa("IB")));
            this.repozytorium.Dodaj(u3);
            this.repozytorium.Dodaj(new Nauczyciel("Damian", "Brzeziński", "brzeziu@wp.pl", "DamBrz"));
            this.repozytorium.Dodaj(u4);
            this.repozytorium.Dodaj(u5);
            IList <OcenyUcznia> oceny = new List<OcenyUcznia>();
            OcenyUcznia b1 = new OcenyUcznia(u1,new Przedmiot("Fizyka"));
            b1.Oceny = "4, 5, 5, 2";
            oceny.Add(b1);
            OcenyUcznia b2 = new OcenyUcznia(u2,new Przedmiot("Fizyka"));
            b2.Oceny = "3, 2, 4, 2";
            oceny.Add(b2);
            OcenyUcznia b3 = new OcenyUcznia(u3,new Przedmiot("Fizyka"));
            b3.Oceny = "1, 2, 3, 2";
            oceny.Add(b3);
            this.repozytoriumOcen.Zapisz(oceny);
            ObecnoscUcznia o1 = new ObecnoscUcznia(u1, DateTime.Now.Date);
            o1.Godzina8 = true;
            o1.Godzina9 = true;
            o1.Godzina10 = false;
            o1.Godzina11 = true;
            o1.Godzina12 = false;
            o1.Godzina13 = false;
            o1.Godzina14 = false;
            o1.Godzina15 = false;
            lista.Add(o1);
            ObecnoscUcznia o2 = new ObecnoscUcznia(u2, DateTime.Now.Date);
            o2.Godzina8 = true;
            o2.Godzina9 = true;
            o2.Godzina10 = false;
            o2.Godzina11 = true;
            o2.Godzina12 = true;
            o2.Godzina13 = false;
            o2.Godzina14 = false;
            o2.Godzina15 = true;
            lista.Add(o2);
            ObecnoscUcznia o3 = new ObecnoscUcznia(u3, DateTime.Now.Date);
            o3.Godzina8 = true;
            o3.Godzina9 = true;
            o3.Godzina10 = true;
            o3.Godzina11 = true;
            o3.Godzina12 = true;
            o3.Godzina13 = true;
            o3.Godzina14 = true;
            o3.Godzina15 = true;
            lista.Add(o3);
            ObecnoscUcznia o4 = new ObecnoscUcznia(u4, new DateTime(2015, 7, 11, 11, 11, 11, 11, DateTimeKind.Local));
            o4.Godzina8 = true;
            o4.Godzina9 = true;
            o4.Godzina10 = true;
            o4.Godzina11 = true;
            o4.Godzina12 = true;
            o4.Godzina13 = true;
            o4.Godzina14 = true;
            o4.Godzina15 = true;
            lista.Add(o4);
            ObecnoscUcznia o5 = new ObecnoscUcznia(u5, new DateTime(2015, 7, 11, 11, 11, 11, 11, DateTimeKind.Local));
            o5.Godzina8 = true;
            o5.Godzina9 = true;
            o5.Godzina10 = true;
            o5.Godzina11 = true;
            o5.Godzina12 = true;
            o5.Godzina13 = true;
            o5.Godzina14 = true;
            o5.Godzina15 = false;
            lista.Add(o5);
            repozytoriumObecnosci.Zapisz(lista);
        }
    }
}
