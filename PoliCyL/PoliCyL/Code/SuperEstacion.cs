using System;
using System.Collections.Generic;

namespace PoliCyL
{
   public class SuperEstacion
    {
        private String nombre;
        private List<Tipo> medidores;

        public String getNombre()
        {
            return nombre;
        }        
        public List<Tipo> getMedidores()
        {
            return medidores;
        }
        public SuperEstacion(List<Tipo> medidores, String nombre)
        {
            this.medidores = medidores;
            this.nombre = nombre;
        }
    }
}