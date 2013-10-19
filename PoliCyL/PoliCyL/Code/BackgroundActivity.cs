using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.IO;
using System.Net;
using System.Threading;

namespace PoliCyL.Code
{
    class BackgroundActivity 
    {
        public static List<SuperEstacion> dataList = new List<SuperEstacion>();
        public static String dataNotSplitted;
        public static String[] rowData, fullData;
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
        public static void SplitCSV(String data)
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
            rowData = afileList.Split(';');
        }
        /**
         * Se encarga de agregar a un array las diferentes estaciones de medición.
         * */
        public static void setStations()
        {
            List<Tipo> estacion = new List<Tipo>();
            //Caso excepcional : Avila
            Split(0);
            estacion.Add(new Tipo(rowData[31], rowData[32], rowData[30]));
            int i;
            Split(1);
            for (i = 2; i <= 13 && rowData[3]=="AVILA"; i++)
            {
                Split(i);
                estacion.Add(new Tipo(rowData[5], rowData[6], rowData[4]));
            }
            dataList.Add(new SuperEstacion(estacion, rowData[3]));
            extractInfo(i);
        }
        /**
         *Extrae información del CSV.
         */
        public static void extractInfo(int i)
        {
            List<Tipo> estacion = new List<Tipo>();
            int k;
            //Estaciones.
            int[] array2 = new int[] { 13, 15, 15, 12, 17, 11, 10, 12, 14, 6, 17, 13 };           
            for (int j = 0; j < 12; j++)
            {
                for (k = i; k < i + array2[j]; k++)
                {
                    Split(k);
                    estacion.Add(new Tipo(rowData[5], rowData[6], rowData[4]));
                }
                dataList.Add(new SuperEstacion(estacion, rowData[3]));
                estacion = new List<Tipo>();
                i = k;
            }          
        }       
    }
}
