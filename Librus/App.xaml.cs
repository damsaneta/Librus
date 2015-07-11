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
        public App()
        {
            IList<ObecnoscUcznia> lista = new List<ObecnoscUcznia>();
            this.klasy.Dodaj(new Klasa("IA"));
            this.klasy.Dodaj(new Klasa("IB"));
            Uczen u3 = new Uczen("Alan", "Jamróz", "jamr@gmail.com", "AlaJam", new Klasa("IA"));
            Uczen u1 =new Uczen("Anna", "Kowalska", "das@gmail.com", "AnnKow",new Klasa("IA"));
            Uczen u2 =new Uczen("Michalina", "Nowak", "mich@gmail.com", "MicNow", new Klasa("IA"));
            this.klasy.PobierzWszystkie();
            this.repozytorium.Dodaj(new Rodzic("Aneta", "Dams", "damsA@gmail.com", "AneDam",null));
            this.repozytorium.Dodaj(new Administrator("Łukasz", "Nowak", "ld@gmail.com", "ŁukNow"));
            this.repozytorium.Dodaj(u1);
            this.repozytorium.Dodaj(u2);
            this.repozytorium.Dodaj(new Uczen("Janusz", "Nowak", "jan@gmail.com", "JanNow", new Klasa("IB")));
            this.repozytorium.Dodaj(new Uczen("Alina", "Jawor", "jawor@gmail.com", "AliJaw", new Klasa("IB")));
            this.repozytorium.Dodaj(new Uczen("Martyna", "Dudziec", "dudz@gmail.com", "MarDus", new Klasa("IB")));
            this.repozytorium.Dodaj(u3);
            this.repozytorium.Dodaj(new Nauczyciel("Damian", "Brzeziński", "brzeziu@wp.pl", "DamBrz"));
            ObecnoscUcznia o1 = new ObecnoscUcznia(u1,DateTime.Now.Date);
            o1.Godzina8 = true;
            o1.Godzina9=true;
            o1.Godzina10=false;
            o1.Godzina11 = true;
            o1.Godzina12 = false;
            o1.Godzina13 = false;
            o1.Godzina14 = false;
            o1.Godzina15 =false;
            lista.Add(o1);
            repozytoriumObecnosci.Zapisz(lista);
        }
    }
}
