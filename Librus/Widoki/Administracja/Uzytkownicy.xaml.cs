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
        protected IList<Uzytkownik> uzytkownicy = new List<Uzytkownik>();
        public Uzytkownicy()
        {
            InitializeComponent();
           
            
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            var view = new DodawanieUzytkownika();
            view.ShowDialog();
            uzytkownicy= this.repozytorium.PobierzWszystkich();
        }

        private void Szukaj_MouseEnter(object sender, MouseEventArgs e)
        {
            this.txtSzukaj.Text = null;
        }

        private void Szukaj_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(this.txtSzukaj.Text==null)
            {
                uzytkownicy = this.repozytorium.PobierzWszystkich();
                this.grid.Items.Refresh();
            }
            else
            {
                uzytkownicy = this.repozytorium.WyszukajUzytkownikow(this.txtSzukaj.Text);
            }

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TypRoli typ = (TypRoli)this.comboBox.SelectedIndex;
            if (typ.ToString() != "Nieznany")
            {
                this.uzytkownicy = this.repozytorium.WyszukajPoRoli(typ.ToString());
                this.grid.Items.Refresh();
            }
            else
            {
                this.uzytkownicy= this.repozytorium.PobierzWszystkich();
                this.grid.Items.Refresh();
            }
            
        }

    }
}
