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
using System.Windows.Shapes;
using System.Drawing;

namespace PoliCyL.View
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private SuperEstacion parameter;
        private List<Tipo> medidores;
        public Boolean back = false;
        private List<SuperEstacion> dataList = new List<SuperEstacion>();
        public Window1(SuperEstacion parameter,List<SuperEstacion> list)
            : this()
        {            
            this.parameter = parameter;
            this.dataList = list;
            this.medidores = parameter.getMedidores();
            this.WindowStartupLocation = WindowStartupLocation.Manual;
            ListViewWindow1.ItemsSource = medidores;
            Title = parameter.getNombre();
        }
        public Window1()
        {
            InitializeComponent();
        }
        //Volver
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            back = true;
            Results newWindow = new Results(dataList);
            newWindow.Owner = this;
           
            newWindow.Show();
            Hide();
        }
        protected override void OnClosed(EventArgs e)
        {
            if (!back)
            {
                for (int intCounter = App.Current.Windows.Count - 1; intCounter >= 0; intCounter--)
                    App.Current.Windows[intCounter].Close();
            }
        }
    }
}
