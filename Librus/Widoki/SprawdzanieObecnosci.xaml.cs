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
        int licznik = 0;
        private readonly RepozytoriumUzytkownikowWPamieci repozytorium = new RepozytoriumUzytkownikowWPamieci();
        public SprawdzanieObecnosci()
        {

            InitializeComponent();
            this.wyborDaty.SelectedDate = DateTime.Now;

        }

        private void KlasaComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {



            if (licznik != 0)
            {
                this.nieobecnosciDataGrid.ItemsSource = this.repozytorium.WyszukajPoKlasie("IA")
                    .Select(uczen => new ObecnoscUcznia(uczen)).ToList();

            }
            licznik++;

        }

        private void GodzinaComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int ind = godzinaComboBox.SelectedIndex;
            if (ind != 0)
            {
                ind += 7;
                string wynik = ind + ":00";

                this.nieobecnosciDataGrid.Columns[1].Header = wynik;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var g = this.nieobecnosciDataGrid;
            var v = g.Items;
            var b = (g.Columns[1] as DataGridCheckBoxColumn);
            
            
        }

        private void nieobecnosciDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

        }
    }
}
