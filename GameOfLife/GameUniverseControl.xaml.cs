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
using System.Windows.Threading;

namespace GameOfLife
{
    /// <summary>
    /// Логика взаимодействия для GameUniverseControl.xaml
    /// </summary>
    public partial class GameUniverseControl : UserControl
    {
        private CellOfLifeControl[,] _universeArray;
        private DispatcherTimer _timer;

        private int _cellSize = 25;
        private int _numberOfCellsInWidth;
        private int _numberOfCellsInHeight;
        private double _chanceLife = 0.25;

        public int NumberOfCellsInWidth
        {
            get => _numberOfCellsInWidth;
            set
            {
                if (_numberOfCellsInWidth != value)
                {
                    Initialize(value, _numberOfCellsInHeight);
                }
                _numberOfCellsInWidth = value;
            }
        }

        public int NumberOfCellsInHeight
        {
            get => _numberOfCellsInHeight;
            set
            {
                if (_numberOfCellsInHeight != value)
                {
                    Initialize(_numberOfCellsInWidth, value);
                }
                _numberOfCellsInHeight = value;
            }
        }

        public bool IsClosedUnverse { get; set; } = false;

        public GameUniverseControl()
        {
            InitializeComponent();
            _timer = new DispatcherTimer(DispatcherPriority.Render);
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Tick += Timer_Tick;
            Initialize(10, 10);
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Pause()
        {
            _timer.Stop();
        }

        public void Initialize(int width, int height)
        {
            CanvasControl.Children.Clear();
            _numberOfCellsInWidth = width;
            _numberOfCellsInHeight = height;
            _universeArray = new CellOfLifeControl[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    _universeArray[x, y] = new CellOfLifeControl();
                    _universeArray[x, y].Height = _cellSize;
                    _universeArray[x, y].Width = _cellSize;
                    CanvasControl.Children.Add(_universeArray[x, y]);
                    Canvas.SetLeft(_universeArray[x, y], x * _cellSize);
                    Canvas.SetTop(_universeArray[x, y], y * _cellSize);
                }
            }
        }

        public void Timer_Tick(object sender, EventArgs args)
        {
            StartNextCycleOfLife();
        }

        public void StartNextCycleOfLife()
        {
            List<Point> changedCells = new List<Point>();
            for (int x = 0; x < NumberOfCellsInWidth; x++)
            {
                for (int y = 0; y < NumberOfCellsInHeight; y++)
                {
                    bool newState;
                    switch (GetNumberOfLiveNeighbors(x, y))
                    {
                        case 2:
                            continue;
                        case 3:
                            newState = true;
                            break;
                        default:
                            newState = false;
                            break;
                    }
                    if (_universeArray[x, y].IsAlive != newState)
                    {
                        changedCells.Add(new Point(x, y));
                    }
                }
            }
            if (changedCells.Count > 0)
            {
                foreach (var point in changedCells)
                {
                    _universeArray[(int)point.X, (int)point.Y].IsAlive = !_universeArray[(int)point.X, (int)point.Y].IsAlive;
                }
            }
            else
            {
                MessageBox.Show("Игра окончена", "GemaOfLife", MessageBoxButton.OK, MessageBoxImage.Information);
                _timer.Stop();
            }
        }

        private int GetNumberOfLiveNeighbors(int inputX, int inputY)
        {
            int count = 0;
            for (int x = inputX - 1; x <= inputX + 1; x++)
            {
                int currentX = x;
                if (x < 0 || x >= NumberOfCellsInWidth)
                {
                    if (IsClosedUnverse)
                    {
                        currentX = x < 0 ? NumberOfCellsInWidth - 1 : 0;
                    }
                    else
                    {
                        continue;
                    }
                }
                for (int y = inputY - 1; y <= inputY + 1; y++)
                {
                    int currentY = y;
                    if (currentX == inputX && currentY == inputY)
                    {
                        continue;
                    }
                    if (y < 0 || y >= NumberOfCellsInHeight)
                    {
                        if (IsClosedUnverse)
                        {
                            currentY = y < 0 ? NumberOfCellsInHeight - 1 : 0;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    if (_universeArray[currentX, currentY].IsAlive)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public void GenerateRandomLife()
        {
            Random rnd = new Random();
            for (int x = 0; x < NumberOfCellsInWidth; x++)
            {
                for (int y = 0; y < NumberOfCellsInHeight; y++)
                {
                    _universeArray[x, y].IsAlive = rnd.NextDouble() < _chanceLife;
                }
            }
        }

    }

}
