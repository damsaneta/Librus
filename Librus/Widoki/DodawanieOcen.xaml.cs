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
    /// Interaction logic for DodawanieOcen.xaml
    /// </summary>
    public partial class DodawanieOcen : Window
    {
        private const string connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\aneta\Desktop\Librus\Librus\LibrusDatabase.mdf;Integrated Security=True";
        private readonly IRepozytoriumUzytkownikow repozytoriumUzytkownikow;
        private readonly IRepozytoriumPrzedmiotow repozytoriumPrzedmiotow;
        private readonly IRepozytoriumKlas repozytoriumKlas;
        private readonly IRepozytoriumOcenUcznia repozytoriumOcenUcznia;
        public DodawanieOcen()
        {
            this.repozytoriumUzytkownikow = new RepozytoriumUzytkownikow(connectionString);
            this.repozytoriumPrzedmiotow = new RepozytoriumPrzedmiotow(connectionString);
            this.repozytoriumKlas = new RepozytoriumKlas(connectionString);
            this.repozytoriumOcenUcznia = new RepozytoriumOcenUcznia(connectionString);
            InitializeComponent();
            this.klasaComboBox.ItemsSource = repozytoriumKlas.PobierzWszystkie();
            this.klasaComboBox.DisplayMemberPath = "Nazwa";
            this.klasaComboBox.SelectedValuePath = "Id";

            this.przedmiotComboBox.ItemsSource = repozytoriumPrzedmiotow.PobierzWszystkie();
            this.przedmiotComboBox.DisplayMemberPath = "Nazwa";
            this.przedmiotComboBox.SelectedValuePath = "Id";
        }
        private void KlasaComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool test = true;
            test &= Walidator.WalidacjaWymaganegoComboBoxa(this.klasaComboBox, this.errKlasa);
            test &= Walidator.WalidacjaWymaganegoComboBoxa(this.przedmiotComboBox, this.errPrzedmiot);
            if (test && !string.IsNullOrEmpty(this.klasaComboBox.SelectedValue.ToString()) && !string.IsNullOrEmpty(this.klasaComboBox.SelectedValue.ToString()))
            {
                Klasa klasa = this.klasaComboBox.SelectedItem as Klasa;
                Przedmiot przedmiot = this.przedmiotComboBox.SelectedItem as Przedmiot;
                var oceny = this.repozytoriumOcenUcznia.PobierzPoKlasieIPrzedmiocie(klasa.Id, przedmiot.Id);
                if (oceny == null || oceny.Count == 0)
                {
                    var uczniowie = repozytoriumUzytkownikow.WyszukajPoKlasie(klasa.Id);
                    oceny = uczniowie.Select(x => new OcenyUcznia(x, przedmiot)).ToList();
                    this.ocenyDataGrid.ItemsSource = oceny;
                }
                else
                {
                    this.ocenyDataGrid.ItemsSource = oceny;
                }

            }

        }

        private void PrzedmiotComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool test = true;
            test &= Walidator.WalidacjaWymaganegoComboBoxa(this.klasaComboBox, this.errKlasa);
            test &= Walidator.WalidacjaWymaganegoComboBoxa(this.przedmiotComboBox, this.errPrzedmiot);
            if (test && !string.IsNullOrEmpty(this.klasaComboBox.SelectedValue.ToString()) && !string.IsNullOrEmpty(this.klasaComboBox.SelectedValue.ToString()))
            {
                Klasa klasa = this.klasaComboBox.SelectedItem as Klasa;
                Przedmiot przedmiot = this.przedmiotComboBox.SelectedItem as Przedmiot;
                var oceny = this.repozytoriumOcenUcznia.PobierzPoKlasieIPrzedmiocie(klasa.Id, przedmiot.Id);
                if (oceny == null || oceny.Count == 0)
                {
                    var uczniowie = repozytoriumUzytkownikow.WyszukajPoKlasie(klasa.Id);
                    oceny = uczniowie.Select(x => new OcenyUcznia(x, przedmiot)).ToList();
                    this.ocenyDataGrid.ItemsSource = oceny;
                }
                else
                {
                    this.ocenyDataGrid.ItemsSource = oceny;
                }

            }

        }

        private void BtnZapiszClick(object sender, RoutedEventArgs e)
        {
            bool test = true;
            test &= Walidator.WalidacjaWymaganegoComboBoxa(this.przedmiotComboBox, this.errPrzedmiot);
            test &= Walidator.WalidacjaWymaganegoComboBoxa(this.klasaComboBox, errKlasa);
            if (test && !string.IsNullOrEmpty(this.klasaComboBox.SelectedValue.ToString()) && !string.IsNullOrEmpty(this.klasaComboBox.SelectedValue.ToString()))
            {
                var g = this.ocenyDataGrid;
                IList<OcenyUcznia> v = g.Items.SourceCollection as IList<OcenyUcznia>;
                foreach(OcenyUcznia w in v)
                {
                    if(w.Oceny == null)
                    {
                        w.Oceny = string.Empty;
                    }
                }
                this.repozytoriumOcenUcznia.Zapisz(v);
            }
        }
    }
}

