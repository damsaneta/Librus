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
        public App()
        {
            this.repozytorium.Dodaj(new Uzytkownik("Aneta", "Brzezińska", "damsA@gmail.com", TypRoli.Rodzic));
            this.repozytorium.Dodaj(new Uzytkownik("Łukasz", "Dams", "ldams@gmail.com", TypRoli.Administrator));
            this.repozytorium.Dodaj(new Uzytkownik("Anna", "Kowlska", "das@gmail.com", TypRoli.Rodzic));
            this.repozytorium.Dodaj(new Uzytkownik("Damian", "Brzeziński", "brzeziu@wp.pl", TypRoli.Nauczyciel));
            
        }
    }
}
