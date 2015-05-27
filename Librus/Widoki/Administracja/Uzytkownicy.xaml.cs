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

            this.grid.ItemsSource = this.repozytorium.PobierzWszystkich();
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            var view = new DodawanieUzytkownika();
            bool? wynik =view.ShowDialog();
            if(wynik.HasValue && wynik.Value)
            {
                this.grid.ItemsSource = this.repozytorium.PobierzWszystkich();
            }
            
        }

        private void Szukaj_MouseEnter(object sender, MouseEventArgs e)
        {
            this.txtSzukaj.Text = null;
        }

        private void Szukaj_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(this.txtSzukaj.Text==null)
            {
                this.grid.ItemsSource = this.repozytorium.PobierzWszystkich();
               // this.grid.Items.Refresh();
            }
            else
            {
                this.grid.ItemsSource  = this.repozytorium.WyszukajUzytkownikow(this.txtSzukaj.Text);
                //this.grid.ItemsSource = this.Lista;
            }

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TypRoli typ = (TypRoli)this.comboBox.SelectedIndex;
            if (typ.ToString() != "Nieznany")
            {
                this.grid.ItemsSource = this.repozytorium.WyszukajPoRoli(typ.ToString());
                //this.grid.Items.Refresh();
            }
            else
            {
                this.grid.ItemsSource = this.repozytorium.PobierzWszystkich();
                //this.grid.ItemsSource = this.Lista;
                //this.grid.Items.Refresh();
            }
            
        }

    }
}
