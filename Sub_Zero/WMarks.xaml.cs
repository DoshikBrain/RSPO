using DocumentFormat.OpenXml.Drawing.Charts;
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

namespace Sub_Zero
{
    /// <summary>
    /// Логика взаимодействия для WMarks.xaml
    /// </summary>
    public partial class WMarks : Window
    {
        List<Box> orders;
        public WMarks()
        {
            InitializeComponent();
          orders = DataBaseSQL.GetAllBoxes();

            cbBox.Items.Clear();
            foreach (var order in orders)
            {
                cbBox.Items.Add(order);
            }
        }
        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();

        }

        private void BChoiceFileW(object sender, RoutedEventArgs e)
        {
            stGive.Visibility=Visibility.Visible;
            stNotGive.Visibility = Visibility.Hidden;
            bGive.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#5a34cd"));
            bGive.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("White"));
            bOjed.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Transparent"));
            bOjed.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
            dtGive.Text = DateTime.Now.ToString();
        }

        private void BChoiceFileE(object sender, RoutedEventArgs e)
        {
            stGive.Visibility = Visibility.Hidden;
            stNotGive.Visibility = Visibility.Visible;
            bOjed.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#5a34cd"));
            bOjed.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("White"));
            bGive.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Transparent"));
            bGive.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
            dtNotGive.Text = DateTime.Now.ToString();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime dateGive,dateNotGive;
                

                if (stGive.Visibility == Visibility.Visible)
                {
 
                    dateGive=Convert.ToDateTime(dtGive.Text);
                    DataBaseSQL.UpdateDatePolych(cbBox.SelectedItem as Box, dateGive);
                }
                else if (stNotGive.Visibility == Visibility.Visible)
                {
                    dateNotGive = Convert.ToDateTime(dtNotGive.Text);
                    DataBaseSQL.UpdateDateProsrochAndDopDengi(cbBox.SelectedItem as Box, dateNotGive, 24.346) ;
                }
            }
            catch {
                MessageBox.Show("Вы не выбрали дату или посылку");
            }
        }
    }
}
