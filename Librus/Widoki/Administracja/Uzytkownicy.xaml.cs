using Librus.DostepDoDanych.Pamiec;
using Librus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Librus.Widoki.Administracja
{
    /// <summary>
    /// Interaction logic for Uzytkownicy.xaml
    /// </summary>
    public partial class Uzytkownicy : Window
    {
        private readonly RepozytoriumUzytkownikowWPamieci repozytorium = new RepozytoriumUzytkownikowWPamieci();
        public Uzytkownicy()
        {
            InitializeComponent();
            this.repozytorium.Dodaj(new Uzytkownik("Aneta", "Dams", "damsA@gmail.com", TypRoli.Rodzic));
            this.repozytorium.Dodaj(new Uzytkownik("Łukasz", "Dams", "ldams@gmail.com", TypRoli.Administrator));
            this.repozytorium.Dodaj(new Uzytkownik("Anna", "Kowlska", "das@gmail.com", TypRoli.Rodzic));
            this.repozytorium.Dodaj(new Uzytkownik("Mateusz", "Brzeziński", "brzeziu@wp.pl", TypRoli.Nauczyciel));
         
           this.grid.ItemsSource = this.repozytorium.PobierzWszystkich();
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            var view = new DodawanieUzytkownika();
            view.ShowDialog();
            this.grid.ItemsSource = this.repozytorium.PobierzWszystkich();
            this.grid.Items.Refresh();
        }

        private void Szukaj_MouseEnter(object sender, MouseEventArgs e)
        {
            this.txtSzukaj.Text = null;
        }

    }
}
