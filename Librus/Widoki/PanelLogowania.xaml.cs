﻿using Librus.DostepDoDanych.Pamiec;
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
        private readonly RepozytoriumUzytkownikowWPamieci repozytorium = new RepozytoriumUzytkownikowWPamieci();
        public PanelLogowania()
        {
            InitializeComponent();
        }

        private void Zaloguj_Click(object sender, RoutedEventArgs e)
        {
            bool result = true;
            result &= Walidator.WalidacjaWymaganegoPolaTekstowego(this.txtLogin, this.errLogin);
            result &=Walidator.WalidacjaHasla(this.txtHaslo, this.errHaslo);
            Uzytkownik u = repozytorium.PobierzPoEmailu(this.txtLogin.Text);
            if(result==true)
            {
                if(u!=null)
                {
                    if(u.Haslo==this.txtHaslo.Password)
                    {
                        var widok = new OknoGlowne();
                        widok.Show();
                        this.Close();

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
