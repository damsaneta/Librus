using Librus.DostepDoDanych.Pamiec;
using Librus.Model;
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

        private readonly RepozytoriumKlas klasy = new RepozytoriumKlas();
        public App()
        {
            this.klasy.Dodaj(new Klasa("IA"));
            this.klasy.Dodaj(new Klasa("IB"));

            this.klasy.PobierzWszystkie();
            this.repozytorium.Dodaj(new Rodzic("Aneta", "Dams", "damsA@gmail.com", "AneDam",null));
            this.repozytorium.Dodaj(new Administrator("Łukasz", "Nowak", "ld@gmail.com", "ŁukNow"));
            this.repozytorium.Dodaj(new Uczen("Anna", "Kowalska", "das@gmail.com", "AnnKow",new Klasa("IA")));
            this.repozytorium.Dodaj(new Uczen("Michalina", "Nowak", "mich@gmail.com", "MicNow", new Klasa("IA")));
            this.repozytorium.Dodaj(new Uczen("Janusz", "Nowak", "jan@gmail.com", "JanNow", new Klasa("IB")));
            this.repozytorium.Dodaj(new Uczen("Alina", "Jawor", "jawor@gmail.com", "AliJaw", new Klasa("IB")));
            this.repozytorium.Dodaj(new Nauczyciel("Damian", "Brzeziński", "brzeziu@wp.pl", "DamBrz"));
            
        }
    }
}
