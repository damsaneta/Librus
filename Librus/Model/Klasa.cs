using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librus.Model
{
    public class Klasa
    {   
        public Klasa( string id, string nazwa)
        {
            this.Nazwa = nazwa;
            this.Id = id;
        }

        public string Nazwa { get; private set; }

        public string Id { get; private set; }
    }
}
