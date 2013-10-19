using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.IO;
using System.Net;
using System.Threading;

namespace PoliCyLUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        public static String data;
        public static String[] tokens;
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
            String dataNotSplitted = sr.ReadToEnd();
            data = dataNotSplitted;
            sr.Close();
            Assert.AreNotEqual(null, dataNotSplitted);
        }
        [TestMethod]
        public void isTheSameDataBeggining()
        {
            tokens = data.Split(new String[] { ";;;;;NIVELES DE POLEN;;;;;;;;\r\n;A�O;SEMANA;ESTACIONES;TIPOS POL�NICOS;PRECEDENTES           (�ltimos d�as);PREVISION pr�ximos d�as;;;;;;;\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            Assert.AreEqual(1, tokens.Length);
        }
        [TestMethod]
        public void hasDataChanged()
        {
            string[] afileList = tokens[0].ToString().Split(';');
            Assert.AreEqual(1876, afileList.Length);
        }
    }
}