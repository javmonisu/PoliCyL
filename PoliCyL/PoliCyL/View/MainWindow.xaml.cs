using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Net;
using System.Threading;

namespace PoliCyL
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        public static List<SuperEstacion> dataList = new List<SuperEstacion>();
        Thread workerThread = null;
        ManualResetEvent threadInterrupt = new ManualResetEvent(false);
        public static String dataNotSplitted,filelist;
        public static String[] rowData,fullData;
        Boolean threadEnd = false;
        public MainWindow()
        {
            InitializeComponent();
        }
        public void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (workerThread == null)
            {
                this.threadInterrupt.Reset();
                this.workerThread = new Thread(()=>{
                    HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://www.datosabiertos.jcyl.es/web/jcyl/risp/es/mediciones/niveles_de_polen/1284208096554.csv");
                    HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                    StreamReader sr = new StreamReader(resp.GetResponseStream());
                    dataNotSplitted = sr.ReadToEnd();
                    sr.Close();
                    //MessageBox.Show(dataNotSplitted);
                    SplitCSV(dataNotSplitted);
                    setStations();
                    threadEnd = true;
                    
                });
                this.workerThread.IsBackground = true;
                this.workerThread.Start();
               
                while (!threadEnd)
                {
                    Thread.Sleep(250); 
                }
                Results newWindow = new Results(dataList);
                newWindow.Show();
                Hide();
            }
        }
        public static void SplitCSV(String data)
        {
            
            List<string> splitted = new List<string>();
            string fileList = data;
            fullData = data.Split(new String[] { ";;;;;;;;;\r\n" }, StringSplitOptions.RemoveEmptyEntries);           
        }       
        public static void Split(int i)
        {
            string afileList = fullData[i].ToString();
            rowData = afileList.Split(';');
        }
        public static void setStations()
        {
            List<Tipo> estacion = new List<Tipo>();
            //Caso excepcional : Avila
            Split(0);            
            estacion.Add(new Tipo(rowData[31], rowData[32], rowData[30]));
            int i;
            for (i = 1; i <= 12; i++)
            {
                Split(i);
                estacion.Add(new Tipo(rowData[5], rowData[6], rowData[4]));
            }
            dataList.Add(new SuperEstacion(estacion, rowData[3]));
            extractInfo(i);
        }
        /**
         *Extract the info from the CSV. 
         * 
         */
        public static void extractInfo(int i)
        {
            List<Tipo> estacion = new List<Tipo>();
            int k;
            int[] array2 = new int[] { 16, 12, 12, 9, 16, 12, 7, 14, 13, 14, 11, 13 };
            for (int j = 0; j < 12; j++)
            {
                for (k = i; k < i + array2[j]; k++)
                {
                    Split(k);
                    estacion.Add(new Tipo(rowData[5], rowData[6], rowData[4]));
                    
                }                
                dataList.Add(new SuperEstacion(estacion,rowData[3]));
                estacion = new List<Tipo>();
                i = k;                
            }
        }
    }    
}
