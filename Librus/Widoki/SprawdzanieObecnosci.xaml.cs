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
    /// Interaction logic for SprawdzanieObecnosci.xaml
    /// </summary>
    public partial class SprawdzanieObecnosci : Window
    {
        private const string connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\aneta\Desktop\Librus\Librus\LibrusDatabase.mdf;Integrated Security=True";
        private readonly IRepozytoriumKlas repozytoriumKlas;
        private readonly IRepozytoriumObecnosci repozytoriumObecnosci;
        private readonly IRepozytoriumUzytkownikow repozytoriumUzytkownikow;

        public SprawdzanieObecnosci()
        {
            repozytoriumKlas = new RepozytoriumKlas(connectionString);
            repozytoriumObecnosci = new RepozytoriumObecnosci(connectionString);
            repozytoriumUzytkownikow = new RepozytoriumUzytkownikow(connectionString);
            InitializeComponent();
            this.wyborDaty.SelectedDate = DateTime.Now.Date;
            this.klasaComboBox.ItemsSource = this.repozytoriumKlas.PobierzWszystkie();
            this.klasaComboBox.DisplayMemberPath = "Nazwa";
            this.klasaComboBox.SelectedValuePath = "Id";
        }

        private void KlasaComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool test = true;

            test &= Walidator.WalidacjaWymaganegoPolaDaty(this.wyborDaty, errData);
            test &= Walidator.WalidacjaWymaganegoComboBoxa(this.klasaComboBox, errKlasa);
            if ((this.klasaComboBox.SelectedValue != null) && test)
            {
                Klasa klasa = this.klasaComboBox.SelectedItem as Klasa;
                var obecnosci = this.repozytoriumObecnosci.PobierzPoKlasieIDacie(klasa.Id, this.wyborDaty.SelectedDate.Value);
                if (obecnosci == null || obecnosci.Count == 0)
                {
                    var uczniowie = repozytoriumUzytkownikow.WyszukajPoKlasie(klasa.Id);
                    obecnosci = uczniowie.Select(x => new ObecnoscUcznia(x, this.wyborDaty.SelectedDate.Value)).ToList();
                    this.nieobecnosciDataGrid.ItemsSource = obecnosci;
                }
                else
                {
                    this.nieobecnosciDataGrid.ItemsSource = obecnosci;
                }

            }

        }

        private void ZapiszClick(object sender, RoutedEventArgs e)
        {
            bool test = true;
            test &= Walidator.WalidacjaWymaganegoPolaDaty(this.wyborDaty, this.errData);
            test &= Walidator.WalidacjaWymaganegoComboBoxa(this.klasaComboBox, errKlasa);
            if (this.klasaComboBox.SelectedValue != null && test)
            {
                var g = this.nieobecnosciDataGrid;
                var v = g.Items.SourceCollection as IList<ObecnoscUcznia>;
                this.repozytoriumObecnosci.Zapisz(v);
                MessageBox.Show("Obecności zostały poprawnie zapisane.", "Sprawdzanie obecności", MessageBoxButton.OK, MessageBoxImage.Information);

            }

        }

        private void ZmianaDaty(object sender, SelectionChangedEventArgs e)
        {
            bool test = true;
            test &= Walidator.WalidacjaWymaganegoPolaDaty(this.wyborDaty, errData);//walidaxja daty
            test &= Walidator.WalidacjaWymaganegoComboBoxa(this.klasaComboBox, errKlasa);
            if (this.klasaComboBox.SelectedValue != null)
            {
                if (this.klasaComboBox.SelectedValue != null && test)
                {
                    Klasa klasa = this.klasaComboBox.SelectedItem as Klasa;
                    var obecnosci = this.repozytoriumObecnosci.PobierzPoKlasieIDacie(klasa.Id, this.wyborDaty.SelectedDate.Value);
                    if (obecnosci == null || obecnosci.Count == 0)
                    {
                        var uczniowie = repozytoriumUzytkownikow.WyszukajPoKlasie(klasa.Id);
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
