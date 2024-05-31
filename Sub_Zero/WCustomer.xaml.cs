using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
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
    /// Логика взаимодействия для WCustomer.xaml
    /// </summary>
    public partial class WCustomer : Window
    {
        public string typeOpen;
        public Drivers driver;
        public Customer customer;
        public WCustomer(string a)
        {
            typeOpen = a;
            InitializeComponent();
            if (a== "Водители")
            {
                
                txtBlockName.Width = 150;
                txtBlockName.Text = "Водители";
              DataBaseSQL.LoadDriversToDataGrid(dgDriver);
      
            }
            else
            {
                tbAdressCompany.Visibility = Visibility.Hidden;
                tbCodeBank.Visibility = Visibility.Hidden;
                tbNameBank.Visibility = Visibility.Hidden;
                tbRastSchet.Visibility = Visibility.Hidden;
                tbYnp.Visibility = Visibility.Hidden;
                tbNameCompany.Visibility = Visibility.Hidden;
                DataBaseSQL.LoadCustomersToDataGrid(dgCustomer);

            }
            //var provercka = new BrushConverter();
            //ObservableCollection<Customer> peoples = new ObservableCollection<Customer>();

            ////Create DataGride Items Info
            //peoples.Add(new Customer(1, "Пожариццкая Полина Игоревна", "+3752985475", "PPP@mail.ru"));
            //dgCustomer.ItemsSource = peoples;
        }
        public WCustomer()
        {
          
            InitializeComponent();
            var provercka = new BrushConverter();
            ObservableCollection<Customer> peoples = new ObservableCollection<Customer>();

            //Create DataGride Items Info
            //peoples.Add(new Customer(1, "Пожариццкая Полина Игоревна", "+3752985475", "PPP@mail.ru"));
            //dgCustomer.ItemsSource = peoples;
        }
        bool pressAdd = false;

        private void bAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            textBoxFIODriver.Clear();
            textBoxPhoneNamber.Clear();
            textBoxDriveLesans.Clear();
            dpBirtheday.Text="";

            if (typeOpen == "Водители")
            {
                bAddCustomer.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#5a34cd"));
                bAddCustomer.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("White"));
                bDeleteCustomer.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Transparent"));
                bDeleteCustomer.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
                gbAddCDriver.Visibility = Visibility.Visible;
                dgDriver.Visibility = Visibility.Hidden;
            }
            else
            {
                bAddCustomer.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#5a34cd"));
                bAddCustomer.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("White"));
                bDeleteCustomer.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Transparent"));
                bDeleteCustomer.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
                gbAddCustomer.Visibility = Visibility.Visible;
                dgCustomer.Visibility = Visibility.Hidden;
            }
            pressAdd = true;
        }

        private void bDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {

            if (typeOpen == "Водители")
            {
                bDeleteCustomer.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#5a34cd"));
                bDeleteCustomer.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("White"));
                bAddCustomer.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Transparent"));
                bAddCustomer.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
                dgDriver.Visibility = Visibility.Visible;
                gbAddCDriver.Visibility = Visibility.Hidden;
            }
            else
            {
                bDeleteCustomer.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#5a34cd"));
                bDeleteCustomer.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("White"));
                bAddCustomer.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Transparent"));
                bAddCustomer.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
                dgCustomer.Visibility = Visibility.Visible;
                gbAddCustomer.Visibility = Visibility.Hidden;
            }
               
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_addPeople(object sender, RoutedEventArgs e)
        {

            if (pressAdd)
            {
                if (!cheng) { 
                
                    if (txtBlockName.Text == "Водители")
                    {
                        try
                        {
                            DateTime b = Convert.ToDateTime(dpBirtheday.Text);
                            Drivers a = new Drivers(textBoxFIODriver.Text, textBoxPhoneNamber.Text, textBoxDriveLesans.Text, b);
                            DataBaseSQL.AddDriver(a);
                            DataBaseSQL.LoadDriversToDataGrid(dgDriver);

                            
                        }
                        catch
                        {
                            MessageBox.Show("Ошибочка");
                        }
                    }
                    else
                    {
                        try
                        {
                            if (Fiz_face)
                            {
                                Customer customer = new Customer(tbFIO.Text, tbPhone.Text, tbPoch.Text, tbAdress.Text);
                                DataBaseSQL.AddCustomerFiz(customer);
                                DataBaseSQL.LoadCustomersToDataGrid(dgCustomer);
                            }
                            if (Uzface)
                            {
                                customer = new Customer(tbFIO.Text, tbPhone.Text, tbPoch.Text, 1, tbPasport.Text, tbAdress.Text, tbYnp.Text, tbNameCompany.Text, tbAdressCompany.Text, Convert.ToInt32(tbRastSchet.Text), tbNameBank.Text, tbCodeBank.Text);
                               bool access= DataBaseSQL.AddCustomerUr(customer);
                                 DataBaseSQL.LoadCustomersToDataGrid(dgCustomer);
                                if(access)
                                {
                                    MessageBox.Show("Ошибочка");

                                }
                            }
                        }
                        catch
                        {
                            MessageBox.Show("Ошибочка");
                        }
                    }
                }
                else
                {
                    if (txtBlockName.Text == "Водители")
                    {
                        try
                        {
                            driver.FullName = textBoxFIODriver.Text;
                            driver.PhoneNumber = textBoxPhoneNamber.Text;
                            driver.DateOfBirth = Convert.ToDateTime(dpBirtheday.Text);
                            driver.DrivingLicenseNumber = textBoxDriveLesans.Text;

                            DataBaseSQL.UpdateDriver(driver);
                            DataBaseSQL.LoadDriversToDataGrid(dgDriver);


                        }
                        catch
                        {
                            MessageBox.Show("Ошибочка");
                        }
                    }
                    else
                    {
                        if (Fiz_face)
                        {
                            customer.AdressCompany = tbAdressCompany.Text;
                            customer.CodeBank = tbCodeBank.Text;
                            customer.NameBank = tbNameBank.Text;
                            int rastSchet;
                            int.TryParse(tbRastSchet.Text, out rastSchet);
                            customer.RastSchet = rastSchet;
                            customer.Ynp = tbYnp.Text;
                            customer.AdressCompany = tbNameCompany.Text;
                            customer.Adress = tbAdress.Text;
                            customer.Fio = tbFIO.Text;
                            customer.Pochta = tbPoch.Text;
                            customer.NumberOfPhone = tbPhone.Text;
                            customer.Pasport = tbPasport.Text;

                            DataBaseSQL.UpdateCustomer(customer);
                            DataBaseSQL.LoadCustomersToDataGrid(dgCustomer);
                        }
                        else if(Uzface)
                        {
                            customer = new Customer(tbFIO.Text,tbNameBank.Text,tbPoch.Text,1,tbPasport.Text,tbAdress.Text,tbYnp.Text,tbNameCompany.Text,tbAdressCompany.Text,Convert.ToInt32(tbRastSchet.Text),tbNameBank.Text,tbCodeBank.Text);
                            DataBaseSQL.UpdateCustomer(customer);
                            DataBaseSQL.LoadCustomersToDataGrid(dgCustomer);
                        }
                    }
                    
                }


            }
            else
            {

                if (txtBlockName.Text == "Водители")
                {
                    try
                    {
                        // dgDriver
                    }
                    catch
                    {
                        MessageBox.Show("Ошибочка");
                    }
                }
                else
                {

                }

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try { 
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить этого водителя?", "Подтверждение удаления", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                Drivers selectedDriver = (Drivers)dgDriver.SelectedItem;
                    selectedDriver = DataBaseSQL.GetDriver(selectedDriver.FullName);
                DataBaseSQL.DeleteDriver(selectedDriver);
                DataBaseSQL.LoadDriversToDataGrid(dgDriver);
            }
            }catch {
             MessageBox.Show("Ошибка");

            }
        }
        bool cheng = false;
        private void Button_Click_updata(object sender, RoutedEventArgs e)
        {
            if (txtBlockName.Text == "Водители")
            {
                Drivers selectedDriver = (Drivers)dgDriver.SelectedItem;
                driver = selectedDriver = DataBaseSQL.GetDriver(selectedDriver.FullName);
                bAddCustomer_Click(sender, e);
                dpBirtheday.Text = selectedDriver.DateOfBirth.ToString();
                textBoxFIODriver.Text = selectedDriver.FullName;
                textBoxPhoneNamber.Text = selectedDriver.PhoneNumber;
                textBoxDriveLesans.Text = selectedDriver.DrivingLicenseNumber;
                cheng = true;
            }
            else
            {
               
                cheng = true;
            }
        }
        static bool Fiz_face=   false,Uzface=false;
        private void UrFace_Click(object sender, RoutedEventArgs e)
        {
            Fiz_face = false;
            Uzface =true;
           tbAdressCompany.Visibility = Visibility.Visible;
            tbCodeBank.Visibility = Visibility.Visible;
            tbNameBank.Visibility = Visibility.Visible;
            tbRastSchet.Visibility = Visibility.Visible;
           tbYnp.Visibility = Visibility.Visible;
            tbNameCompany.Visibility = Visibility.Visible;
            bUrFace.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#5a34cd"));
            bUrFace.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("White"));
            bFizFace.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Transparent"));
            bFizFace.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
        }

        private void Button_Add_Customer(object sender, RoutedEventArgs e)
        {
            Customer selectedCust = (Customer)dgCustomer.SelectedItem;
            customer= selectedCust = DataBaseSQL.GetCustomer(selectedCust.NumberOfPhone);
            bAddCustomer_Click(sender, e);
            if (customer.IsUrFace==1)
            {
                tbAdressCompany.Text = customer.AdressCompany;
                tbCodeBank.Text = customer.CodeBank;
                tbNameBank.Text = customer.NameBank;
                tbRastSchet.Text = customer.RastSchet.ToString();
                tbYnp.Text = customer.Ynp;
                tbNameCompany.Text = customer.AdressCompany;
                tbAdress.Text = customer.Adress;
                tbFIO.Text = customer.Fio;
                tbPoch.Text = customer.Pochta;
                tbPhone.Text = customer.NumberOfPhone;
                tbPasport.Text = customer.Pasport;
            }
            else
            {
                tbFIO.Text = customer.Fio;
                tbPoch.Text = customer.Pochta;
                tbPhone.Text = customer.NumberOfPhone;
                tbPasport.Text = customer.Pasport;
                tbAdress.Text = customer.Adress;
                bAddCustomer_Click(sender, e);
            }
             cheng = true;


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить этого клиента?", "Подтверждение удаления", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    Customer selectedCustomer = (Customer)dgCustomer.SelectedItem;
                    DataBaseSQL.DeleteCustomer(selectedCustomer);
                    DataBaseSQL.LoadCustomersToDataGrid(dgCustomer);
                }
            }
            catch
            {
                MessageBox.Show("Ошибка");

            }
        }

        private void FizFace_Click(object sender, RoutedEventArgs e)
        {
            Uzface = false;
            Fiz_face = true;
            tbAdressCompany.Visibility = Visibility.Hidden;
            tbCodeBank.Visibility = Visibility.Hidden;
            tbNameBank.Visibility = Visibility.Hidden;
            tbRastSchet.Visibility = Visibility.Hidden;
            tbYnp.Visibility = Visibility.Hidden;
            tbNameCompany.Visibility = Visibility.Hidden;
            bFizFace.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#5a34cd"));
            bFizFace.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("White"));
            bUrFace.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Transparent"));
            bUrFace.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
        }
    }
   
}

