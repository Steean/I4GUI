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

namespace SimpleDraw
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Point startPoint;
        private Point endPoint;
        private SolidColorBrush myColorBrush;
        private bool mouseDown;
        private bool isLine;
        private Shape shape;
        private Line line;
        
        public MainWindow()
        {           
            InitializeComponent();            
        }

        private void Color_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.B)
            {
                myColorBrush = new SolidColorBrush();
                myColorBrush.Color = Color.FromRgb(0, 0, 0);
            }
            else if (e.Key == Key.R)
            {
                myColorBrush = new SolidColorBrush();
                myColorBrush.Color = Color.FromRgb(255, 0, 0);
            }
            else if (e.Key == Key.Y)
            {
                myColorBrush = new SolidColorBrush();
                myColorBrush.Color = Color.FromRgb(255, 255, 0);
            }
            else if (e.Key == Key.G)
            {
                myColorBrush = new SolidColorBrush();
                myColorBrush.Color = Color.FromRgb(0, 255, 0);
            }

            TbColorRect.Fill = myColorBrush;
        }

        private void Window_OnMouseMove(object sender, MouseEventArgs e)
        {
            Point positionOnCanvas = e.GetPosition(drawingCanvas);
            xCoord.Text = positionOnCanvas.X.ToString("F0");
            yCoord.Text = positionOnCanvas.Y.ToString("F0");

            endPoint = e.GetPosition(drawingCanvas);

            double width = Math.Abs(startPoint.X - endPoint.X);
            double height = Math.Abs(startPoint.Y - endPoint.Y);

            if (mouseDown)
            {
                if (isLine)
                {
                    line.X1 = startPoint.X;
                    line.Y1 = startPoint.Y;
                    line.X2 = endPoint.X;
                    line.Y2 = endPoint.Y;
                }

                else
                {
                    shape.Width = width;
                    shape.Height = height;
                    Canvas.SetLeft(shape, startPoint.X);
                    Canvas.SetTop(shape, startPoint.Y);
                }
            }
        }

        private void ShapeType_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(drawingCanvas);
            mouseDown = true;

            if (Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                shape = new Ellipse();
                shape.Fill = myColorBrush;
                drawingCanvas.Children.Add(shape);
            }

            else if (Keyboard.IsKeyDown(Key.LeftAlt))
            {
                shape = new Rectangle();
                shape.Fill = myColorBrush;
                drawingCanvas.Children.Add(shape);
            }

            else
            {
                isLine = true;
                line = new Line();                
                line.Stroke = myColorBrush;
                drawingCanvas.Children.Add(line);                
            }
        }

        private void ShapeType_OnMouseUp(object sender, MouseButtonEventArgs e)
        {            
            mouseDown = false;
            isLine = false;
        }

        private void BtnClear_OnClick(object sender, RoutedEventArgs e)
        {
            drawingCanvas.Children.Clear();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            myColorBrush = new SolidColorBrush();
            myColorBrush.Color = Color.FromRgb(0, 0, 0);
            TbColorRect.Fill = myColorBrush;
        }
    }
}
