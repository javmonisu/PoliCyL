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

namespace PoliCyL
{
    public partial class Results : Window
    {
        private List<SuperEstacion> parameter;

        public Results(List<SuperEstacion> parameter)
            : this()
        {
            this.parameter = parameter;
            this.WindowStartupLocation = WindowStartupLocation.Manual;
        }

        public Results()
        {
            InitializeComponent();
        }
        //Avila
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            prepareValues(0);
        }
        //Arenas de San Pedro
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            prepareValues(1);
        }
        //Burgos
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            prepareValues(2);
        }
        //Miranda de Ebro
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            prepareValues(3);
        }
        //Leon
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            prepareValues(4);
        }
        //Ponferrada
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            prepareValues(5);
        }
        //Palencia
        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            prepareValues(6);
        }
        //Salamanca
        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            prepareValues(7);
        }
        //Segovia
        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            prepareValues(8);
        }
        //Soria
        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            prepareValues(9);
        }
        //Valladolid
        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            prepareValues(10);
        }
        //Zamora
        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            prepareValues(11);
        }
        //Bejar
        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            prepareValues(12);
        }
        public void prepareValues(int i)
        {
            SuperEstacion s = parameter[i];
            if (i == 10)
            {
                s.medidores.Reverse();
            }

            PoliCyL.View.Window1 newWindow = new PoliCyL.View.Window1(s, parameter);
            newWindow.Show();
            Hide();
            
        }   
    }
}
