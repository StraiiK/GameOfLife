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
    /// Логика взаимодействия для CellOfLifeControl.xaml
    /// </summary>
    public partial class CellOfLifeControl : UserControl
    {
        private SolidColorBrush _colorOfLife = new SolidColorBrush(Colors.Green);
        private SolidColorBrush _colorOfDeath = new SolidColorBrush(Colors.Black);
        private bool _isAlive = false;

        public bool IsAlive
        {
            get => _isAlive;
            set
            {
                if (_isAlive != value)
                {
                    _isAlive = value;
                    Rectangle.Fill = value ? _colorOfLife : _colorOfDeath;
                }
            }
        }

        public CellOfLifeControl()
        {
            InitializeComponent();
        }

        private void Rectangle_MouseUp(object sender, MouseButtonEventArgs e)
        {
            IsAlive = !IsAlive;
        }
    }
}
