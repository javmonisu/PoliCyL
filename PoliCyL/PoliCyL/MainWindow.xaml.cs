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
        Thread workerThread = null;
        ManualResetEvent threadInterrupt = new ManualResetEvent(false);
        String dataNotSplitted;

        public MainWindow()
        {
            InitializeComponent();
        }

        protected void Button_Click_1(object sender, RoutedEventArgs e)
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
                    MessageBox.Show(dataNotSplitted);
                    SplitCSV(dataNotSplitted);
                });
                this.workerThread.IsBackground = true;
                this.workerThread.Start();
            }
            else
            {
                
            }
        }
        public static void SplitCSV(String data)
        {
            List<string> splitted = new List<string>();
            string fileList = data;
            string[] tempStr;

            tempStr = data.Split(',');

            foreach (string item in tempStr)
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    splitted.Add(item);
                }
            }
        }
      
    }
}
