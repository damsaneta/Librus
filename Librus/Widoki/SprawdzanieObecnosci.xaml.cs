using Librus.DostepDoDanych;
using Librus.DostepDoDanych.BazaDanych;
using Librus.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Librus.Widoki
{
    /// <summary>
    /// Interaction logic for SprawdzanieObecnosci.xaml
    /// </summary>
    public partial class SprawdzanieObecnosci : Window
    {
        private readonly IRepozytoriumKlas repozytoriumKlas;
        private readonly IRepozytoriumObecnosci repozytoriumObecnosci;
        private readonly IRepozytoriumUzytkownikow repozytoriumUzytkownikow;

        public SprawdzanieObecnosci()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["database"].ConnectionString;
            repozytoriumKlas = new RepozytoriumKlas(connectionString);
            repozytoriumObecnosci = new RepozytoriumObecnosci(connectionString);
            repozytoriumUzytkownikow = new RepozytoriumUzytkownikow(connectionString);
            InitializeComponent();
            this.Inicjalizuj();
        }

        private async void Inicjalizuj()
        {
            this.wyborDaty.SelectedDate = DateTime.Now.Date;
            this.klasaComboBox.ItemsSource = await this.repozytoriumKlas.PobierzWszystkie();
            this.klasaComboBox.DisplayMemberPath = "Nazwa";
            this.klasaComboBox.SelectedValuePath = "Id";
        }

        private async void KlasaComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool test = true;

            test &= Walidator.WalidacjaWymaganegoPolaDaty(this.wyborDaty, errData);
            test &= Walidator.WalidacjaWymaganegoComboBoxa(this.klasaComboBox, errKlasa);
            if ((this.klasaComboBox.SelectedValue != null) && test)
            {
                Klasa klasa = this.klasaComboBox.SelectedItem as Klasa;
                var obecnosci = await this.repozytoriumObecnosci.PobierzPoKlasieIDacie(klasa.Id, this.wyborDaty.SelectedDate.Value);
                if (obecnosci == null || obecnosci.Count == 0)
                {
                    var uczniowie = await repozytoriumUzytkownikow.WyszukajPoKlasie(klasa.Id);
                    obecnosci = uczniowie.Select(x => new ObecnoscUcznia(x, this.wyborDaty.SelectedDate.Value)).ToList();
                    this.nieobecnosciDataGrid.ItemsSource = obecnosci;
                }
                else
                {
                    this.nieobecnosciDataGrid.ItemsSource = obecnosci;
                }

            }

        }

        private async void ZapiszClick(object sender, RoutedEventArgs e)
        {
            bool test = true;
            test &= Walidator.WalidacjaWymaganegoPolaDaty(this.wyborDaty, this.errData);
            test &= Walidator.WalidacjaWymaganegoComboBoxa(this.klasaComboBox, errKlasa);
            if (this.klasaComboBox.SelectedValue != null && test)
            {
                var g = this.nieobecnosciDataGrid;
                var v = g.Items.SourceCollection as IList<ObecnoscUcznia>;
                await this.repozytoriumObecnosci.Zapisz(v);
                MessageBox.Show("Obecności zostały poprawnie zapisane.", "Sprawdzanie obecności", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private async void ZmianaDaty(object sender, SelectionChangedEventArgs e)
        {
            bool test = true;
            test &= Walidator.WalidacjaWymaganegoPolaDaty(this.wyborDaty, errData);//walidaxja daty
            test &= Walidator.WalidacjaWymaganegoComboBoxa(this.klasaComboBox, errKlasa);
            if (this.klasaComboBox.SelectedValue != null)
            {
                if (this.klasaComboBox.SelectedValue != null && test)
                {
                    Klasa klasa = this.klasaComboBox.SelectedItem as Klasa;
                    var obecnosci = await this.repozytoriumObecnosci.PobierzPoKlasieIDacie(klasa.Id, this.wyborDaty.SelectedDate.Value);
                    if (obecnosci == null || obecnosci.Count == 0)
                    {
                        var uczniowie = await repozytoriumUzytkownikow.WyszukajPoKlasie(klasa.Id);
                        obecnosci = uczniowie.Select(x => new ObecnoscUcznia(x, this.wyborDaty.SelectedDate.Value)).ToList();
                        this.nieobecnosciDataGrid.ItemsSource = obecnosci;
                    }
                    else
                    {
                        this.nieobecnosciDataGrid.ItemsSource = obecnosci;
                    }

                }
            }
        }
    }
}
