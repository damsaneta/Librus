using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librus.Model
{
    public class Rola
    {
        public Rola(TypRoli typ)
        {
            this.Typ = typ;
            this.Nazwa = typ.ToString();
        }
        public TypRoli Typ { get; private set; }

        public string Nazwa { get; private set; }

        public override string ToString()
        {
            return this.Nazwa;
        }
    
    }
}
