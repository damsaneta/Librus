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
        private readonly RepozytoriumKlasWPamieci klasy = new RepozytoriumKlasWPamieci();
        private readonly IRepozytoriumPrzedmiotow repozytoriumPrzedmiotow = new RepozytoriumPrzedmiotowWPamieci();
        private readonly IRepozytoriumOcenUcznia repozytoriumOcen = new RepozytoriumOcenUczniaWPamieci();
        public App()
        {
            Przedmiot p1 = new Przedmiot("jang","Angielski");
            Przedmiot p2 = new Przedmiot("bio","Biologia");
            Przedmiot p3 = new Przedmiot("fiz","Fizyka");
            Przedmiot p4 = new Przedmiot("mat","Matematyka");
            repozytoriumPrzedmiotow.Dodaj(p1);
            repozytoriumPrzedmiotow.Dodaj(p2);
            repozytoriumPrzedmiotow.Dodaj(p3);
            repozytoriumPrzedmiotow.Dodaj(p4);
            

            IList<ObecnoscUcznia> lista = new List<ObecnoscUcznia>();
            var klasa1a = new Klasa("IA", "IA (mat - fiz)");
            this.klasy.Dodaj(klasa1a);
            var klasa1b = new Klasa("IB", "IB (hum - praw)");
            var klasa1c =  new Klasa("IC", "IC (bio - chem)");
            this.klasy.Dodaj(klasa1b);
            this.klasy.Dodaj(klasa1c);
            Uczen u3 = new Uczen("Alan", "Kowalski", "jamr@gmail.com", "AlaKow", klasa1a);
            Uczen u1 = new Uczen("Anna", "Kowalska", "das@gmail.com", "AnnKow", klasa1a);
            Uczen u2 = new Uczen("Michalina", "Nowak", "mich@gmail.com", "MicNow", klasa1a);
            Uczen u4 = new Uczen("Adam", "Boroch", "asdf@gmail.com", "AdaBor", klasa1b);
            Uczen u5 = new Uczen("Iza", "Kania", "fdsfsd@gmail.com", "IzaKan", klasa1b);
            this.klasy.PobierzWszystkie();
            IList<Uzytkownik> dzieci = new List<Uzytkownik>();
       
            this.repozytorium.Dodaj(new Administrator("Łukasz", "Nowak", "ld@gmail.com", "ŁukNow"));
            this.repozytorium.Dodaj(u1);
            this.repozytorium.Dodaj(u2);
            this.repozytorium.Dodaj(new Uczen("Janusz", "Nowak", "jan@gmail.com", "JanNow", klasa1c));
            this.repozytorium.Dodaj(new Uczen("Alina", "Jawor", "jawor@gmail.com", "AliJaw", klasa1c));
            this.repozytorium.Dodaj(new Uczen("Martyna", "Dudziec", "dudz@gmail.com", "MarDus", klasa1c));
            this.repozytorium.Dodaj(u3);
            this.repozytorium.Dodaj(new Nauczyciel("Damian", "Brzeziński", "brzeziu@wp.pl", "DamBrz"));
            this.repozytorium.Dodaj(u4);
            this.repozytorium.Dodaj(u5);
            dzieci.Add(u1);
            dzieci.Add(u3);
            this.repozytorium.Dodaj(new Rodzic("Aneta", "Dams", "damsA@gmail.com", "AneDam", dzieci));
            IList <OcenyUcznia> oceny = new List<OcenyUcznia>();
            OcenyUcznia b1 = new OcenyUcznia(u1,p3);
            b1.Oceny = "4, 5, 5, 2";
            oceny.Add(b1);
            OcenyUcznia b4 = new OcenyUcznia(u1, p4);
            b4.Oceny = "4, 5, 5, 5";
            oceny.Add(b4);
            OcenyUcznia b2 = new OcenyUcznia(u2,p3);
            b2.Oceny = "3, 2, 4, 2";
            oceny.Add(b2);
            OcenyUcznia b3 = new OcenyUcznia(u3,p3);
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
