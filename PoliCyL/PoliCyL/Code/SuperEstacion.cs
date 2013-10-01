using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliCyL
{
    public class SuperEstacion
    {
        public String nombre;
        public List<Tipo> medidores;

        public String getNombre()
        {
            return nombre;
        }
        public void setNombre(String nombre)
        {
            this.nombre = nombre;
        }
        public void setMedidores(List<Tipo> medidores)
        {
            this.medidores = medidores;
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
