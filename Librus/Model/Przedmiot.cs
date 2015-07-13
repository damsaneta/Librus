using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librus.Model
{
    public class Przedmiot
    {
        public string Nazwa { get; private set; }
        public Przedmiot(string nazwa)
        {
            this.Nazwa = nazwa;
        }
        
    }
}
