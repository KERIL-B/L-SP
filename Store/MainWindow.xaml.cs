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

namespace Store
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            mainSP.Visibility = System.Windows.Visibility.Hidden;
            startSP.Visibility = System.Windows.Visibility.Visible;
        }

        InterfaceC IClass = new InterfaceC();
        
        System.Timers.Timer timer;
        

        private void doorB_Click(object sender, RoutedEventArgs e)
        {
            if (IClass.CheckNumberOfCahBoxes(cashBoxesNumberTB, canvas1))
            {
                mainSP.Visibility = System.Windows.Visibility.Visible;
                startSP.Visibility = System.Windows.Visibility.Hidden;

                IClass.s = 0;
                timer = new System.Timers.Timer(100);
                timer.Elapsed+=timer_Elapsed;
                timer.Start();
            }
            cashBoxesNumberTB.Text = "";
        }

        private void timer_Elapsed (object sender, System.Timers.ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
                {
                    IClass.s++;
                    IClass.TimeCheck(canvas1);
                    timeL.Content = IClass.s + "/"+IClass.nextBuyerTime;
                });

        }
    }
}
