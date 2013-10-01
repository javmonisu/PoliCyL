using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliCyL
{
    public class Tipo
    {
        public String precedente;
        public String prevision;
        public String nombre;
       
        public Tipo(String precedente, String prevision, String nombre)
        {
            this.precedente = precedente;
            this.prevision = prevision;
            this.nombre = nombre;           
        }
     }
}
