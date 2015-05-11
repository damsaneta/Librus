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
        public PanelLogowania()
        {
            InitializeComponent();
        }

        private void Zaloguj_Click(object sender, RoutedEventArgs e)
        {
            Walidator.WalidacjaWymaganegoPolaTekstowego(this.txtLogin, this.errLogin);
            Walidator.WalidacjaWymaganegoPolaTekstowego(this.txtHaslo, this.errHaslo);
        }

        private void TextBoxLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            Walidator.WalidacjaWymaganegoPolaTekstowego(this.txtLogin, this.errLogin);
        }

        private void TextBoxHaslo_TextChanged(object sender, TextChangedEventArgs e)
        {
            Walidator.WalidacjaWymaganegoPolaTekstowego(this.txtHaslo, this.errHaslo);
        }

        private void Anuluj_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
