using System;

namespace PoliCyL
{
    public class Tipo
    {
        private String nombre { get; set; }
        private String precedente { get; set; }
        private String prevision { get; set; }
               
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