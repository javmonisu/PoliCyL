using System;
using System.Collections.Generic;
using System.Windows;
using System.Threading;

namespace PoliCyL
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        public static List<SuperEstacion> dataList = null;
        Thread workerThread = null;
        Boolean error=false;

        public MainWindow()
        {
            InitializeComponent();
        }
        public void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (workerThread == null)
            {
                this.workerThread = new Thread(()=>{
                    Code.BackgroundActivity backTivity = new Code.BackgroundActivity();
                    error = backTivity.connection();
                    dataList = backTivity.getInformation();                                                                       
                });
                this.workerThread.IsBackground = true;
                this.workerThread.Start();               
                while (dataList==null && !error)
                {
                    Thread.Sleep(0); 
                }
                if (error)
                {                  
                    Application.Current.Shutdown();
                }                
                Results newWindow = new Results(dataList);
                newWindow.Owner = this;
                newWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                newWindow.Show();
                Hide();                
            }
        }      
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Application.Current.Shutdown();
        }
    }    
}