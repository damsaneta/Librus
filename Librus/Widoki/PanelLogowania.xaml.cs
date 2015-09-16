using Librus.DostepDoDanych;
using Librus.DostepDoDanych.BazaDanych;
using Librus.Model;
using Librus.Widoki.Administracja;
using System;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;

namespace Librus.Widoki
{
    /// <summary>
    /// Interaction logic for PanelLogowania.xaml
    /// </summary>
    public partial class PanelLogowania : Window
    {
        private readonly IRepozytoriumUzytkownikow repozytoriumUzytkownikow;

        public PanelLogowania()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["database"].ConnectionString;
            this.repozytoriumUzytkownikow = new RepozytoriumUzytkownikow(connectionString);
            InitializeComponent();
        }

        private async void ZalogujClick(object sender, RoutedEventArgs e)
        {
            bool result = true;
            result &= Walidator.WalidacjaWymaganegoPolaTekstowego(this.txtLogin, this.errLogin);
            result &=Walidator.WalidacjaHasla(this.txtHaslo, this.errHaslo);
            Uzytkownik uzytkownik = await repozytoriumUzytkownikow.PobierzPoEmailu(this.txtLogin.Text);
            if(result==true)
            {
                if(uzytkownik!=null)
                {
                    if(uzytkownik.Haslo==this.txtHaslo.Password)
                    {

                        if (uzytkownik.Rola == Rola.Rodzic || uzytkownik.Rola==Rola.Uczen)
                        {
                            var widok = new OknoGlowneRodzic();
                            widok.nazwaLbl.Content = uzytkownik.Email;
                            widok.Show();
                            this.Close();
                        }
                        else if(uzytkownik.Rola == Rola.Nauczyciel)
                        {
                            var widok = new OknoGlowneNauczyciel();
                            widok.nazwaLbl.Content = uzytkownik.Email;
                            widok.Show();
                            this.Close();
                        }
                        else if(uzytkownik.Rola == Rola.Administrator)
                        {
                            var widok = new Uzytkownicy();
                            widok.Show();
                            this.Close();
                        }
                    }
                    else
                    {
                        // TODO
                        Walidator.WyswietlBlad(this.txtHaslo, this.errHaslo, "Niepoprawne hasło!");
                    }
                }
                else
                {
                    // TODO
                    Walidator.WyswietlBlad(this.txtLogin, this.errLogin, "Uzytkownik o takim e-mailu nie istnieje!");
                }
            }
        }

        private void TextBoxLoginTextChanged(object sender, TextChangedEventArgs e)
        {
           if( Walidator.WalidacjaWymaganegoPolaTekstowego(this.txtLogin, this.errLogin))
           {
               Walidator.WalidacjaPolaEmail(this.txtLogin, this.errLogin);
           }
        }

        private void AnulujClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        } 
    }
}
