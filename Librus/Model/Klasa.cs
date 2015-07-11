using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librus.Model
{
    public class Klasa
    {
        public string Nazwa { get; private set; }
        public int Id { get; set; }
        public Klasa(string klasa)
        {
            this.Nazwa = klasa;
        }
    }
}
