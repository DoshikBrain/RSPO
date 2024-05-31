using Microsoft.Win32;
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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace Sub_Zero
{
    /// <summary>
    /// Логика взаимодействия для WIconChange.xaml
    /// </summary>
    public partial class WIconChange : Window
    {
        public WIconChange()
        {
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            
                if (e.ClickCount == 2)
                    DragMove();
                //if (e.ChangedButton == MouseButton.Left)
                //    DragMove();
            }
        private bool IsMaximize = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //if (e.ClickCount == 2)
            //{
            //    if (IsMaximize)
            //    {
            //        WindowState = WindowState.Normal;
            //        Width = 1080;
            //        Height = 720;
            //        IsMaximize = false;
            //    }
            //    else
            //    {
            //        WindowState = WindowState.Minimized;
            //        IsMaximize = true;
            //    }
            //}
        }

        private void OpenFile(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {

                // Здесь вы можете использовать выбранный файл
            }
        }

        private void CloseWind(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }

}
   
