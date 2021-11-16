using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace HomeWork5WpfAppTask2Version2
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

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image (.jpg)|*.jpg";

            if (dlg.ShowDialog() == true)
            {
                string filename = dlg.FileName;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(filename);
                bitmap.EndInit();
                img.Source = bitmap;
            }
            //OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.Filter = "Графический Файл (*.jpg)|*.jpg|Все файлы(*.*)|*.*";

            //if (openFileDialog.ShowDialog() == true)
            //{
            //    // Не работает.
            //    //Image simpleImage = new Image();
            //    //simpleImage.Width = 200;
            //    //simpleImage.Margin = new Thickness(5);


            //    //BitmapImage bi = new BitmapImage();

            //    //bi.BeginInit();
            //    //bi.UriSource = new Uri(openFileDialog.FileName, UriKind.RelativeOrAbsolute);
            //    //bi.EndInit();

            //    //simpleImage.Source = bi;
            //}
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Графический Файл (*.gif)|*.gif|Все файлы(*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                string imgPath = saveFileDialog.FileName;
                MemoryStream ms = new MemoryStream();
                FileStream fs = new FileStream(imgPath, FileMode.Create);

                double width = SystemParameters.FullPrimaryScreenWidth;
                double hight = SystemParameters.FullPrimaryScreenHeight;
                //rtb - объект класса RenderTargetBitmap
                RenderTargetBitmap rtb = new RenderTargetBitmap((int) width , (int) hight, 120, 96, PixelFormats.Pbgra32);
                rtb.Render(inkCanvas);

                GifBitmapEncoder gifEnc = new GifBitmapEncoder(); //сохраняеv в формате GIF
                gifEnc.Frames.Add(BitmapFrame.Create(rtb));
                gifEnc.Save(fs);
                fs.Close();
                MessageBox.Show("Файл сохранен, " + imgPath); //Для информации
            }

        }


        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            inkCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string fontName = ((sender as ComboBox).SelectedItem as TextBlock).Text;
            if (inkCanvas != null)
            {
                if (fontName == "Черный")
                {
                    inkCanvas.DefaultDrawingAttributes.Color = Colors.Black;
                }
                else if (fontName == "Красный")
                {
                    inkCanvas.DefaultDrawingAttributes.Color = Colors.Red;
                }
                else if (fontName == "Синий")
                {
                    inkCanvas.DefaultDrawingAttributes.Color = Colors.Blue;
                }
                else if (fontName == "Желтый")
                {
                    inkCanvas.DefaultDrawingAttributes.Color = Colors.Yellow;
                }
                else if (fontName == "Зеленый")
                {
                    inkCanvas.DefaultDrawingAttributes.Color = Colors.Green;
                }
            }
        }


    }
}
