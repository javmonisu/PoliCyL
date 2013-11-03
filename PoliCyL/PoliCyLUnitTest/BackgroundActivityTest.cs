using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoliCyL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace PoliCyLUnitTest
{
    [TestClass]
    public class BackgroundActivityTest
    {
        public static String data;
        public static String[] tokens,final;
        public static List<string> ciudades;

        [TestMethod]
        public void isHTTPWebServiceAvaliable()
        {
            HttpWebResponse resp = null;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://www.datosabiertos.jcyl.es/web/jcyl/risp/es/mediciones/niveles_de_polen/1284208096554.csv");
            try
            {
                resp = (HttpWebResponse)req.GetResponse();
            }
            catch (WebException)
            {
                Assert.Fail();
            }
            StreamReader sr = new StreamReader(resp.GetResponseStream());
            data = sr.ReadToEnd();
            sr.Close();
            Assert.AreNotEqual(null, data);
        }
        [TestMethod]
        public void isTheSameDataBeggining()
        {
            tokens = data.Split(new String[] { ";;;;;NIVELES DE POLEN;;;;;;;;\r\n;A�O;SEMANA;ESTACIONES;TIPOS POL�NICOS;PRECEDENTES           (�ltimos d�as);PREVISION pr�ximos d�as;;;;;;;\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            Assert.AreEqual(1, tokens.Length);
        }
        [TestMethod]
        public void hasNotDataChanged()
        {
            final = tokens[0].ToString().Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            Assert.AreEqual(764, final.Length);
        }
        [TestMethod]
        public void hasSameNumberOfStations()
        {
            ciudades = new List<string>();
            ciudades.Add(final[3]);
            for (int i = 3; i < final.Length;i++)
            {
                bool exists = false;
                if ((i + 8) < final.Length)
                {
                    i = i + 7;
                }
                else
                {
                    continue;
                }               
                for (int j = 0; j < ciudades.Count(); j++)
                    {
                        if (final[i].Equals(ciudades.ElementAt(j)))
                        {
                            exists = true;
                        }
                }
                if (!exists)
                {
                    ciudades.Add(final[i]);
                }
                i--;
            }
            Assert.AreEqual(13,ciudades.Count);
        }
        [TestMethod]
        public void hasTheSameOrder()
        {
            String[] ciudadesAnt = { "AVILA", "ARENAS DE SAN PEDRO","BURGOS","MIRANDA DE EBRO","LEON","PONFERRADA","PALENCIA","SALAMANCA","SEGOVIA","SORIA","VALLADOLID","ZAMORA","BEJAR" };
            List<string> oldCities = new List<string>(ciudadesAnt);
            for(int i = 0 ; i < ciudades.Count; i++){
                Assert.AreEqual(oldCities.ElementAt(i), ciudades.ElementAt(i));
            }       
        }
        [TestMethod]
        public void orderRawInfo()
        {        
            List<Tipo> medidores = new List<Tipo>();
            List<SuperEstacion> listaEstaciones = new List<SuperEstacion>();
            String localidad = final.ElementAt(3);

            for (int i = 5; i < final.Length; i++)
            {
                if ((final.ElementAt(i - 2).Equals(localidad) || final.ElementAt(i - 3).Equals(localidad)) && i < final.Length - 3)
                {
                    medidores.Add(new Tipo(final.ElementAt(i), final.ElementAt(i + 1), final.ElementAt(i - 1)));
                }               
                else
                {                   
                    if (i >= final.Length - 3)
                    {
                        medidores.Add(new Tipo(final.ElementAt(i), final.ElementAt(i + 1), final.ElementAt(i - 1)));
                        listaEstaciones.Add(new SuperEstacion(medidores, localidad));
                        break;
                    }
                    listaEstaciones.Add(new SuperEstacion(medidores, localidad));
                    medidores = new List<Tipo>();
                    medidores.Add(new Tipo(final.ElementAt(i), final.ElementAt(i + 1), final.ElementAt(i - 1)));
                    localidad = final.ElementAt(i - 2);
                }
                if ((i + 7) < final.Length)
                {
                    i = i + 6;
                }                                       
            }
            Assert.AreEqual(listaEstaciones.Count, ciudades.Count);
        }
    }
}