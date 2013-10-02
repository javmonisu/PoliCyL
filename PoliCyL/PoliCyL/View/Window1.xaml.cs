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

namespace PoliCyL.View
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private SuperEstacion parameter;
        private List<SuperEstacion> dataList = new List<SuperEstacion>();
        public Window1(SuperEstacion parameter,List<SuperEstacion> list)
            : this()
        {
            this.parameter = parameter;
            this.dataList = list;
            this.WindowStartupLocation = WindowStartupLocation.Manual;
            List<Tipo> items = parameter.getMedidores();
            List.ItemsSource = items;
            char nombre = parameter.getNombre().First();
            parameter.setNombre(parameter.getNombre().ToLower());
            Estacion.Text = parameter.getNombre();
        }
        public Window1()
        {
            InitializeComponent();
               
        }
        //Volver
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Results newWindow = new Results(dataList);
            newWindow.Show();
            Hide();
        }

    }
}
