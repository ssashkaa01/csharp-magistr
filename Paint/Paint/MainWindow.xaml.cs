using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using System.IO;

namespace Paint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isDrawing = false;
        private Brush brushColor = Brushes.Black;
        private double brushSize = 5;
        private Point startPoint;
        private string tool = "Brush"; // За замовчуванням - кисть

        public MainWindow()
        {
            InitializeComponent();
        }

        // Зміна товщини лінії
        private void brushSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // Оновлюємо розмір кисті згідно значення слайдера
            brushSize = e.NewValue;
        }

        // Вибір кольору
        private void SelectColor_Click(object sender, RoutedEventArgs e)
        {
            var colorDialog = new System.Windows.Forms.ColorDialog();
            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var color = colorDialog.Color;
                brushColor = new SolidColorBrush(Color.FromArgb(color.A, color.R, color.G, color.B));
            }
        }

        // Малювання на канві
        private void drawingCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isDrawing = true;
            startPoint = e.GetPosition(drawingCanvas);

            if (tool == "Line" || tool == "Rectangle" || tool == "Ellipse")
            {
                drawingCanvas.CaptureMouse(); // Захоплюємо мишу для малювання фігур
            }
        }

        private void drawingCanvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!isDrawing) return;

            var position = e.GetPosition(drawingCanvas);
            if (tool == "Brush")
            {
                Ellipse ellipse = new Ellipse
                {
                    Fill = brushColor,
                    Width = brushSize,
                    Height = brushSize
                };
                Canvas.SetLeft(ellipse, position.X - brushSize / 2);
                Canvas.SetTop(ellipse, position.Y - brushSize / 2);
                drawingCanvas.Children.Add(ellipse);
            }
            // Виправлення для інших інструментів додати тут, якщо потрібно.
        }

        private void drawingCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isDrawing = false;
            var endPoint = e.GetPosition(drawingCanvas);

            if (tool == "Line")
            {
                Line line = new Line
                {
                    X1 = startPoint.X,
                    Y1 = startPoint.Y,
                    X2 = endPoint.X,
                    Y2 = endPoint.Y,
                    Stroke = brushColor,
                    StrokeThickness = brushSize
                };
                drawingCanvas.Children.Add(line);
            }
            else if (tool == "Rectangle")
            {
                Rectangle rect = new Rectangle
                {
                    Stroke = brushColor,
                    StrokeThickness = brushSize,
                    Width = Math.Abs(endPoint.X - startPoint.X),
                    Height = Math.Abs(endPoint.Y - startPoint.Y)
                };
                Canvas.SetLeft(rect, Math.Min(startPoint.X, endPoint.X));
                Canvas.SetTop(rect, Math.Min(startPoint.Y, endPoint.Y));
                drawingCanvas.Children.Add(rect);
            }
            else if (tool == "Ellipse")
            {
                Ellipse ellipse = new Ellipse
                {
                    Stroke = brushColor,
                    StrokeThickness = brushSize,
                    Width = Math.Abs(endPoint.X - startPoint.X),
                    Height = Math.Abs(endPoint.Y - startPoint.Y)
                };
                Canvas.SetLeft(ellipse, Math.Min(startPoint.X, endPoint.X));
                Canvas.SetTop(ellipse, Math.Min(startPoint.Y, endPoint.Y));
                drawingCanvas.Children.Add(ellipse);
            }

            drawingCanvas.ReleaseMouseCapture(); // Звільняємо мишу
        }

        // Інструменти
        private void Brush_Click(object sender, RoutedEventArgs e) => tool = "Brush";
        private void Eraser_Click(object sender, RoutedEventArgs e)
        {
            brushColor = Brushes.White;
            tool = "Brush";
        }

        private void Line_Click(object sender, RoutedEventArgs e) => tool = "Line";
        private void Rectangle_Click(object sender, RoutedEventArgs e) => tool = "Rectangle";
        private void Ellipse_Click(object sender, RoutedEventArgs e) => tool = "Ellipse";

        // Збереження зображення
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp"
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                SaveCanvasAsImage(saveFileDialog.FileName);
            }
        }

        private void SaveCanvasAsImage(string filename)
        {
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)drawingCanvas.ActualWidth, (int)drawingCanvas.ActualHeight, 96d, 96d, PixelFormats.Default);
            rtb.Render(drawingCanvas);

            BitmapEncoder encoder;
            if (filename.EndsWith(".png")) encoder = new PngBitmapEncoder();
            else if (filename.EndsWith(".jpg")) encoder = new JpegBitmapEncoder();
            else encoder = new BmpBitmapEncoder();

            encoder.Frames.Add(BitmapFrame.Create(rtb));
            using (FileStream fs = File.OpenWrite(filename))
            {
                encoder.Save(fs);
            }
        }

        // Відкриття зображення
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.png;*.jpg;*.bmp"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                BitmapImage bitmap = new BitmapImage(new Uri(openFileDialog.FileName));
                Image image = new Image
                {
                    Source = bitmap,
                    Width = bitmap.Width,
                    Height = bitmap.Height
                };
                drawingCanvas.Children.Clear();
                drawingCanvas.Children.Add(image);
            }
        }
    }
}
