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
    /// Логика взаимодействия для WDocument.xaml
    /// </summary>
    public partial class WDocument : Window
    {
        public WDocument()
        {
            InitializeComponent();
            DataBaseSQL.LoadCustomersIntoComboBox(cbCustomer);
            DataBaseSQL.LoadCustomersIntoComboBox(cbCustomerWord);
            ExelBoard.Visibility = Visibility.Hidden;
            WordBoard.Visibility = Visibility.Hidden;
        }
        private string typeFile,typeTable;
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)

                if (e.ClickCount == 2)
                    DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void UserComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            List<Customer> customers = DataBaseSQL.GetAllCustomers();
            cbCustomer.ItemsSource = customers;
        }

        private void BChoiceFileW(object sender, RoutedEventArgs e)
        {
            xls = false;
            ExelBoard.Visibility = Visibility.Hidden;

            typeFile = ".docx";
            BWord.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#5a34cd"));
            BWord.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("White"));
            BExel.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Transparent"));
            BExel.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
            WordBoard.Visibility = Visibility.Visible;
        }
        bool xls=false;
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DateTime star, end;
            try
            {
                if (xls)
                {
                    star = Convert.ToDateTime(Start.Text);
                    end = Convert.ToDateTime(End.Text);
                    Customer customers = DataBaseSQL.GetCustomerForName(cbCustomer.Text);
                    DataBaseSQL.GenerateExcelReport(customers, star, end);
                }
                else
                {
                    Customer customer = DataBaseSQL.GetCustomerForName(cbCustomerWord.SelectedItem.ToString());
                            Box box = cbBox.SelectedItem as Box;
                    Drivers drive = DataBaseSQL.GetDriver(box.Car.Driver);
                    DataBaseSQL.GenerateWordReport(customer, box, drive);

                }
            }
            catch (Exception s)
            {
                if (xls)
                {
                    star = new DateTime(2020, 01, 01);
                        end = new DateTime(2027, 01, 01);
                    Customer customers = DataBaseSQL.GetCustomerForName(cbCustomer.Text);
                    DataBaseSQL.GenerateExcelReport(customers, star, end);

                }
                else
                {
                    MessageBox.Show("Какие то неполадки");
                }

                
            }
           
        }
        public List<Box> orders;
        private void cbCustomerWord_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Customer customer = DataBaseSQL.GetCustomerForName(cbCustomerWord.SelectedItem.ToString());
            if (customer == null) return;

            orders = DataBaseSQL.GetOrdersForCustomer(customer.Id);

            cbBox.Items.Clear();
            foreach (var order in orders)
            {
                cbBox.Items.Add(order);
            }
        }

        private void BChoiceFileE(object sender, RoutedEventArgs e)
        {
            typeFile = ".xlsx";
            xls = true;
            WordBoard.Visibility = Visibility.Hidden;
            ExelBoard.Visibility = Visibility.Visible;
            BWord.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Transparent"));
            BWord.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
            BExel.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#5a34cd"));
            BExel.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("White"));
        }
     
      

    }
}
