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
    /// Interaction logic for OknoGlowneRodzic.xaml
    /// </summary>
    public partial class OknoGlowneRodzic : Window
    {
        public OknoGlowneRodzic()
        {
            InitializeComponent();
        }

        private void ObecnosciBtnClick(object sender, RoutedEventArgs e)
        {
            var obecnosci = new WyswietlanieNieobecnosci(this.nazwaLbl.Content.ToString());
            obecnosci.Show();
        }

        private void OcenyBtnClick(object sender, RoutedEventArgs e)
        {
            var oceny = new WyswietlanieOcen(this.nazwaLbl.Content.ToString());
            oceny.Show();
        }

        private void WylogujBtnClick(object sender, RoutedEventArgs e)
        {
            var panelLogowania = new PanelLogowania();
            panelLogowania.Show();
            this.Close();
        }
    }
}
