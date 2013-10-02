using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliCyL
{
    public class Tipo
    {
        public String nombre { get; set; }
        public String precedente { get; set; }
        public String prevision { get; set; }
        
       
        public Tipo(String precedente, String prevision, String nombre)
        {
            this.precedente = precedente;
            this.prevision = prevision;
            this.nombre = nombre;           
        }
        public String getPrecedente()
        {
            return precedente;
        }
        public String getPrevision()
        {
            return prevision;
        }
        public String getNombre()
        {
            return nombre;
        }
     }
}
