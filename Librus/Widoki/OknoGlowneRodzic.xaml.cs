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
    public partial class OknoGlowneRodzic : Window
    {
        public OknoGlowneRodzic()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Tworzy nowe okno WyswietlanieObecnosci po wciśnięciu przycisku.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ObecnosciBtnClick(object sender, RoutedEventArgs e)
        {
            var obecnosci = new WyswietlanieNieobecnosci(this.nazwaLbl.Content.ToString());
            obecnosci.Show();
        }

        /// <summary>
        /// Tworzy nowe okno WyswietlanieOcen po wciśnięciu przycisku.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void OcenyBtnClick(object sender, RoutedEventArgs e)
        {
            var oceny = new WyswietlanieOcen(this.nazwaLbl.Content.ToString());
            oceny.Show();
        }

        /// <summary>
        /// Wylogowywanie.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void WylogujBtnClick(object sender, RoutedEventArgs e)
        {
            var panelLogowania = new PanelLogowania();
            panelLogowania.Show();
            this.Close();
        }
    }
}
