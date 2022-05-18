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

namespace GameOfLife
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (universeControl == null)
            {
                return;
            }
            universeControl.IsClosedUnverse = radioButton_Closed.IsChecked ?? false;
        }

        private void ChangeSizeUniverse(object sender, TextChangedEventArgs e)
        {
            if (universeControl == null)
            {
                return;
            }
            universeControl.NumberOfCellsInWidth = Convert.ToInt32(textBoxWidth.Text);
            universeControl.NumberOfCellsInHeight = Convert.ToInt32(textBoxHeight.Text);
        }

        private void Button_Start(object sender, RoutedEventArgs e)
        {
            if (universeControl == null)
            {
                return;
            }
            universeControl.Start();
        }

        private void Button_Pause(object sender, RoutedEventArgs e)
        {
            if (universeControl == null)
            {
                return;
            }
            universeControl.Pause();
        }

        private void Button_RandomClick(object sender, RoutedEventArgs e)
        {
            universeControl.GenerateRandomLife();
        }
    }
}
