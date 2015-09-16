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
    /// Interaction logic for OknoGlowneNauczyciel.xaml
    /// </summary>
    public partial class OknoGlowneNauczyciel : Window
    {
        public OknoGlowneNauczyciel()
        {
            InitializeComponent();
        }

        private void ObecnosciBtnClick(object sender, RoutedEventArgs e)
        {
            var obe = new SprawdzanieObecnosci();
            obe.Show();
        }

        private void OcenyBtnClick(object sender, RoutedEventArgs e)
        {
            var oceny = new DodawanieOcen();
            oceny.Show();
        }

        private void WylogujBtnClick(object sender, RoutedEventArgs e)
        {
            var panel = new PanelLogowania();
            panel.Show();
            this.Close();
        }
    }
}
