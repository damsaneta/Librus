using Librus.DostepDoDanych;
using Librus.DostepDoDanych.BazaDanych;
using Librus.DostepDoDanych.Pamiec;
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
    /// <summary>
    /// Interaction logic for Uzytkownicy.xaml
    /// </summary>
    public partial class Uzytkownicy : Window
    {
        private readonly IRepozytoriumUzytkownikow repozytoriumUzytkownikow;

        public Uzytkownicy()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["database"].ConnectionString;
            this.repozytoriumUzytkownikow = new RepozytoriumUzytkownikow(connectionString);
            InitializeComponent();
            this.grid.ItemsSource = this.repozytoriumUzytkownikow.PobierzWszystkich();
        }

        private void DodajClick(object sender, RoutedEventArgs e)
        {
            var view = new DodawanieUzytkownika();
            bool? wynik = view.ShowDialog();
            if (wynik.HasValue && wynik.Value)
            {
                this.grid.ItemsSource = this.repozytoriumUzytkownikow.PobierzWszystkich();
            }
        }

        private void SzukajTextChanged(object sender, TextChangedEventArgs e)
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
                this.grid.ItemsSource = this.repozytoriumUzytkownikow.PobierzWszystkich();
                // this.grid.Items.Refresh();
            }
            else if ((!string.IsNullOrEmpty(this.txtSzukaj.Text) && this.txtSzukaj.Text != "Szukaj...") && (typ == "Szukaj po roli..." || typ == "Nieznany"))
            {
                this.grid.ItemsSource = this.repozytoriumUzytkownikow.WyszukajUzytkownikow(this.txtSzukaj.Text);
                //this.grid.ItemsSource = this.Lista;
            }
            else if ((!string.IsNullOrEmpty(this.txtSzukaj.Text) && this.txtSzukaj.Text != "Szukaj...") && (typ != "Szukaj po roli..." && typ != "Nieznany"))
            {
                this.grid.ItemsSource = this.repozytoriumUzytkownikow.WyszukajPoRoliIWzorcu(this.txtSzukaj.Text, typ);
            }

        }

        private void RolaSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TypRoli typ = (TypRoli)this.comboBox.SelectedIndex;
            if (typ.ToString() != "Nieznany" && (string.IsNullOrEmpty(this.txtSzukaj.Text) || this.txtSzukaj.Text == "Szukaj..."))
            {
                this.grid.ItemsSource = this.repozytoriumUzytkownikow.WyszukajPoRoli(typ.ToString());
                //this.grid.Items.Refresh();
            }
            else if (typ.ToString() != "Nieznany" && (!string.IsNullOrEmpty(this.txtSzukaj.Text) && this.txtSzukaj.Text != "Szukaj..."))
            {
                this.grid.ItemsSource = this.repozytoriumUzytkownikow.WyszukajPoRoliIWzorcu(this.txtSzukaj.Text, typ.ToString());
            }
            else if (typ.ToString() == "Nieznany" && (!string.IsNullOrEmpty(this.txtSzukaj.Text) && this.txtSzukaj.Text != "Szukaj..."))
            {
                this.grid.ItemsSource = this.repozytoriumUzytkownikow.WyszukajUzytkownikow(this.txtSzukaj.Text);
            }
            else
            {
                this.grid.ItemsSource = this.repozytoriumUzytkownikow.PobierzWszystkich();
                //this.grid.ItemsSource = this.Lista;
                //this.grid.Items.Refresh();
            }

        }

        private void TxtSzukajLostFocus(object sender, RoutedEventArgs e)
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
                this.grid.ItemsSource = this.repozytoriumUzytkownikow.PobierzWszystkich();
            }
            if (string.IsNullOrEmpty(txtSzukaj.Text) && typ == "a")
            {
                txtSzukaj.Text = "Szukaj...";
                this.grid.ItemsSource = this.repozytoriumUzytkownikow.PobierzWszystkich();
            }
        }

    }
}
