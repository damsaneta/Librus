using Librus.DostepDoDanych;
using Librus.DostepDoDanych.BazaDanych;
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

namespace Librus.Widoki
{
    /// <summary>
    /// Interaction logic for PanelLogowania.xaml
    /// </summary>
    public partial class PanelLogowania : Window
    {
        private const string connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\aneta\Desktop\Librus\Librus\LibrusDatabase.mdf;Integrated Security=True";
        private readonly IRepozytoriumUzytkownikow repozytoriumUzytkownikow;
        public PanelLogowania()
        {
            this.repozytoriumUzytkownikow = new RepozytoriumUzytkownikow(connectionString);
            InitializeComponent();
        }

        private void Zaloguj_Click(object sender, RoutedEventArgs e)
        {
            bool result = true;
            result &= Walidator.WalidacjaWymaganegoPolaTekstowego(this.txtLogin, this.errLogin);
            result &=Walidator.WalidacjaHasla(this.txtHaslo, this.errHaslo);
            Uzytkownik uzytkownik = repozytoriumUzytkownikow.PobierzPoEmailu(this.txtLogin.Text);
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
                            var widok = new Administracja.Uzytkownicy();
                            widok.Show();
                            this.Close();
                        }
                    }
                    else
                    {
                        Walidator.WyswietlBlad(this.txtHaslo, this.errHaslo, "Niepoprawne hasło!");
                    }
                }
                else
                {
                    Walidator.WyswietlBlad(this.txtLogin, this.errLogin, "Uzytkownik o takim e-mailu nie istnieje!");
                }
            }
        }

        private void TextBoxLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
           if( Walidator.WalidacjaWymaganegoPolaTekstowego(this.txtLogin, this.errLogin))
           {
               Walidator.WalidacjaPolaEmail(this.txtLogin, this.errLogin);
           }
        }

       

        private void Anuluj_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

      
    }
}
