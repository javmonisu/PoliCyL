using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Documents;

namespace PoliCyL.Code
{
    class BackgroundActivity 
    {
        private static List<SuperEstacion> dataList = new List<SuperEstacion>();
        private static String dataNotSplitted;
        private static String[] rowData, fullData;
        HttpWebResponse resp = null;

        public BackgroundActivity(){}

        public List<SuperEstacion> getInformation()
        {
                StreamReader sr = new StreamReader(resp.GetResponseStream());
                dataNotSplitted = sr.ReadToEnd();
                sr.Close();
                SplitCSV(dataNotSplitted);
                setStations();
                return dataList;            
        }
        public Boolean connection()
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://www.datosabiertos.jcyl.es/web/jcyl/risp/es/mediciones/niveles_de_polen/1284208096554.csv");
            try
            {
                resp = (HttpWebResponse)req.GetResponse();
            }
            catch (WebException)
            {
                MessageBox.Show("Esta aplicación necesita conexión a Internet para funcionar correctamente.\nPor favor, compruebe su conexión a Internet.");
                return true;
            }           
            return false;
        }
        /**
         * Divide la información en tokens.
         * */
        private static void SplitCSV(String data)
        {
            List<string> splitted = new List<string>();
            string fileList = data;
            fullData = data.Split(new String[] { ";;;;;NIVELES DE POLEN;;;;;;;;\r\n;A�O;SEMANA;ESTACIONES;TIPOS POL�NICOS;PRECEDENTES           (�ltimos d�as);PREVISION pr�ximos d�as;;;;;;;\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        }
        /**
         * Divide un token en mini-tokens.
         * */
        public static void Split(int i)
        {
            string afileList = fullData[i].ToString();
            rowData = afileList.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries); 
        }
        /**
         * Se encarga de agregar a un array las diferentes estaciones de medición.
         * */
        public static void setStations()
        {
            Split(0);       
            extractInfo();
        }
        /**
         *Extrae información del CSV.
         */
        public static void extractInfo()
        {
            List<Tipo> medidores = new List<Tipo>();
            String localidad = null;
            localidad = rowData.ElementAt(3);
            for (int i = 5; i < rowData.Length; i++)
            {
                if ((rowData.ElementAt(i - 2).Equals(localidad) || rowData.ElementAt(i - 3).Equals(localidad)) && i < rowData.Length - 3)
                {
                    medidores.Add(new Tipo(rowData.ElementAt(i), rowData.ElementAt(i + 1), rowData.ElementAt(i - 1)));
                }
                else
                {
                    if (i >= rowData.Length - 3)
                    {
                        medidores.Add(new Tipo(rowData.ElementAt(i), rowData.ElementAt(i + 1), rowData.ElementAt(i - 1)));
                        dataList.Add(new SuperEstacion(orderArray(medidores), localidad));
                        break;
                    }
                    dataList.Add(new SuperEstacion(orderArray(medidores), localidad));
                    medidores = new List<Tipo>();
                    medidores.Add(new Tipo(rowData.ElementAt(i), rowData.ElementAt(i + 1), rowData.ElementAt(i - 1)));
                    localidad = rowData.ElementAt(i - 2);
                }
                if ((i + 7) < rowData.Length)
                {
                    i = i + 6;
                }
            }
        }
        public static List<Tipo> orderArray(List<Tipo> medidores)
        {
            List<Tipo> orderedList = medidores.OrderBy(o => o.getNombre()).ToList();
            return orderedList;
        }
    }
}