using Librus.DostepDoDanych;
using Librus.DostepDoDanych.BazaDanych;
using Librus.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
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
    public partial class Uzytkownicy : Window
    {
        private readonly IRepozytoriumUzytkownikow repozytoriumUzytkownikow;

        public Uzytkownicy()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["database"].ConnectionString;
            this.repozytoriumUzytkownikow = new RepozytoriumUzytkownikow(connectionString);
            this.InitializeComponent();
            this.Inicjalizuj();
        }

        private async void Inicjalizuj()
        {
            this.grid.ItemsSource = await this.repozytoriumUzytkownikow.PobierzWszystkich();
        }

        /// <summary>
        /// Tworzony jest nowy widok DodawanieUzywkownika.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private async void DodajClick(object sender, RoutedEventArgs e)
        {
            var view = new DodawanieUzytkownika();
            bool? wynik = view.ShowDialog();
            if (wynik.HasValue && wynik.Value)
            {
                this.grid.ItemsSource = await this.repozytoriumUzytkownikow.PobierzWszystkich();
            }
        }

        /// <summary>
        /// Wyszukiwanie użytkowników na podstawie fragmentu imienia, nazwiska lub maila.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="TextChangedEventArgs"/> instance containing the event data.</param>
        private async void SzukajTextChanged(object sender, TextChangedEventArgs e)
        {
            string typ = "";
            TypRoli typ1 = TypRoli.Nieznany;
            try
            {
                typ1 = (TypRoli)this.comboBox.SelectedIndex;
            }
            catch
            {
                typ = "Szukaj po roli...";

            }
            if (typ != "Szukaj po roli...") typ = typ1.ToString();
            if ((string.IsNullOrEmpty(this.txtSzukaj.Text) || this.txtSzukaj.Text == "Szukaj...") && (typ == "Szukaj po roli..." || typ == "Nieznany"))
            {
                this.grid.ItemsSource = await this.repozytoriumUzytkownikow.PobierzWszystkich();
            }
            else if ((!string.IsNullOrEmpty(this.txtSzukaj.Text) && this.txtSzukaj.Text != "Szukaj...") && (typ == "Szukaj po roli..." || typ == "Nieznany"))
            {
                this.grid.ItemsSource = await this.repozytoriumUzytkownikow.WyszukajUzytkownikow(this.txtSzukaj.Text);
            }
            else if ((!string.IsNullOrEmpty(this.txtSzukaj.Text) && this.txtSzukaj.Text != "Szukaj...") && (typ != "Szukaj po roli..." && typ != "Nieznany"))
            {
                this.grid.ItemsSource = await this.repozytoriumUzytkownikow.WyszukajPoRoliIWzorcu(this.txtSzukaj.Text, typ);
            }

        }

        /// <summary>
        /// Wybieranie użytkowników na podstawie wybranej roli przez administratora.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private async void RolaSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TypRoli typ = (TypRoli)this.comboBox.SelectedIndex;
            if (typ.ToString() != "Nieznany" && (string.IsNullOrEmpty(this.txtSzukaj.Text) || this.txtSzukaj.Text == "Szukaj..."))
            {
                this.grid.ItemsSource = await this.repozytoriumUzytkownikow.WyszukajPoRoli(typ.ToString());
            }
            else if (typ.ToString() != "Nieznany" && (!string.IsNullOrEmpty(this.txtSzukaj.Text) && this.txtSzukaj.Text != "Szukaj..."))
            {
                this.grid.ItemsSource = await this.repozytoriumUzytkownikow.WyszukajPoRoliIWzorcu(this.txtSzukaj.Text, typ.ToString());
            }
            else if (typ.ToString() == "Nieznany" && (!string.IsNullOrEmpty(this.txtSzukaj.Text) && this.txtSzukaj.Text != "Szukaj..."))
            {
                this.grid.ItemsSource = await this.repozytoriumUzytkownikow.WyszukajUzytkownikow(this.txtSzukaj.Text);
            }
            else
            {
                this.grid.ItemsSource = await this.repozytoriumUzytkownikow.PobierzWszystkich();
            }

        }

        /// <summary>
        /// Przywracanie tekstu "Szukaj..." w textBoxie.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private async void TxtSzukajLostFocus(object sender, RoutedEventArgs e)
        {
            string typ = "";
            TypRoli typ1 = TypRoli.Nieznany;
            try
            {
                typ1 = (TypRoli)this.comboBox.SelectedIndex;
            }
            catch
            {
                typ = "Szukaj po roli...";

            }
            if (typ1.ToString() == "Nieznany") typ = "a";
            if (string.IsNullOrEmpty(txtSzukaj.Text) && typ == "Szukaj po roli...")
            {
                txtSzukaj.Text = "Szukaj...";
                this.grid.ItemsSource = await this.repozytoriumUzytkownikow.PobierzWszystkich();
            }
            if (string.IsNullOrEmpty(txtSzukaj.Text) && typ == "a")
            {
                txtSzukaj.Text = "Szukaj...";
                this.grid.ItemsSource = await this.repozytoriumUzytkownikow.PobierzWszystkich();
            }
        }

    }
}
