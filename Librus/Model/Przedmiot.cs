using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librus.Model
{
    public class Przedmiot
    {
        public Przedmiot(string id, string nazwa)
        {
            this.Id = id;
            this.Nazwa = nazwa;
        }

        public string Nazwa { get; private set; }

        public string Id { get; private set; }
    }
}
