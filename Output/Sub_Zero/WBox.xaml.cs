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
    /// Логика взаимодействия для WBox.xaml
    /// </summary>
    public partial class WBox : Window
    {
        MyUsers user;
        public DataGrid dt;
        public WBox(string a, DataGrid v, MyUsers c)
        {
            user = c;
            dt = v;
            InitializeComponent();
            if (a == "Добавить транспорт")
            {
                txtBlockNameWindow.Width = 150;
                txtBlockNameWindow.Text = "Транспорт";
                SpBox.Visibility = Visibility.Hidden;
                SpCar.Visibility = Visibility.Visible;
                DataBaseSQL.LoadDriversIntoComboBox(cbDriverforCar);


            }
            else
            {
                DataBaseSQL.LoadCitiesIntoComboBox(cbSityOpr);
                DataBaseSQL.LoadCitiesIntoComboBox(cbSityDevelr);
                DataBaseSQL.LoadCarsIntoComboBox(cbCar);
                DataBaseSQL.LoadCustomersIntoComboBox(cbCustomer);
                DataBaseSQL.LoadDriversIntoComboBox(cbDriver);
                SpBox.Visibility = Visibility.Visible;
                SpCar.Visibility = Visibility.Hidden;
            }

        }
        bool cheng = false;
        Car car;
        Box box;
        public WBox(string a, DataGrid v, Car c)
        {
            dt = v;
            InitializeComponent();
            if (a == "Добавить транспорт")
            {
                txtBlockNameWindow.Width = 150;
                txtBlockNameWindow.Text = "Транспорт";
                SpBox.Visibility = Visibility.Hidden;
                SpCar.Visibility = Visibility.Visible;
                DataBaseSQL.LoadDriversIntoComboBox(cbDriverforCar);

                tbMarkCar.Text = c.MarkOfCar;
                cbDriverforCar.Text = c.name;
                tbMaxVelue.Text = c.MaxDostupVelue.ToString();
                tbMaxWeight.Text = c.MaxDostupWeight.ToString();
                tbNumberCar.Text = c.NumberCar;
                cheng = true;
                car = c;
            }
            else
            {
                SpBox.Visibility = Visibility.Visible;
                SpCar.Visibility = Visibility.Hidden;
            }

        }
        public WBox(string a, DataGrid v, Box c, MyUsers us)
        {
            box = c;
            user = us;
            dt = v;
            InitializeComponent();
            if (a == "Добавить транспорт")
            {
                txtBlockNameWindow.Width = 150;
                txtBlockNameWindow.Text = "Транспорт";
                SpBox.Visibility = Visibility.Hidden;
                SpCar.Visibility = Visibility.Visible;
                DataBaseSQL.LoadDriversIntoComboBox(cbDriverforCar);
            }
            else
            {
                DataBaseSQL.LoadCitiesIntoComboBox(cbSityOpr);
                DataBaseSQL.LoadCitiesIntoComboBox(cbSityDevelr);
                DataBaseSQL.LoadCarsIntoComboBox(cbCar);
                DataBaseSQL.LoadCustomersIntoComboBox(cbCustomer);
                DataBaseSQL.LoadDriversIntoComboBox(cbDriver);
                txtBlockNameWindow.Text = "Обнов.";
                SpBox.Visibility = Visibility.Visible;
                SpCar.Visibility = Visibility.Hidden;
                tbNameOfBox.Text = c.NameBox;
                cbCustomer.Text = c.Customer.Fio;
                dtOtpr.Text = c.dateOtprav.ToString();
                dtDevel.Text = c.datePolych.ToString();
                tbWeight.Text = c.Weight.ToString();
                tbHight.Text = c.Value.ToString();
                cbCar.Text = c.Car.MarkOfCar;
                Drivers driv = DataBaseSQL.GetDriver(c.Car.Driver);
                cbDriver.Text = driv.FullName;
                cbSityOpr.Text = DataBaseSQL.GetCityIdByName(c.SityOtprav);
                cbSityDevelr.Text = DataBaseSQL.GetCityIdByName(c.SityDostav);
                tbProceproj.Text = c.Price.ToString();
                cheng = true;
            }

        }
        public WBox()
        {
            InitializeComponent();
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
        bool seccess = false;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try { 
            if (!cheng)
            {
                if (txtBlockNameWindow.Text != "Посылка")
                {
                    Car a = new Car(tbMarkCar.Text, cbDriverforCar.Text, Convert.ToDouble(tbMaxVelue.Text), Convert.ToDouble(tbMaxWeight.Text), tbNumberCar.Text);
                    Drivers drier = DataBaseSQL.GetDriver(cbDriverforCar.Text);
                    a.Driver = drier.id;
                    seccess = DataBaseSQL.AddCar(a);
                    if (!seccess)
                    {
                        MessageBox.Show("Произошла ошибка добавления автомобиля.Измените данные");
                    }
                    DataBaseSQL.LoadCarsToDataGrid(dt);

                }
                else
                {
                    double weight = Convert.ToDouble(tbWeight.Text), value = Convert.ToDouble(tbHight.Text), priceBox = Convert.ToDouble(tbWeight.Text);

                    DateTime dateOtpr = Convert.ToDateTime(dtOtpr.Text);
                    DateTime dateTo = Convert.ToDateTime(dtDevel.Text);
                    int SityOtpr = DataBaseSQL.GetCityIdByName(cbSityOpr.SelectedItem.ToString());
                    int SityTo = DataBaseSQL.GetCityIdByName(cbSityDevelr.SelectedItem.ToString());
                    Drivers driver = DataBaseSQL.GetDriver(cbDriver.SelectedItem.ToString());
                    Car car = DataBaseSQL.GetCarForName(cbCar.Text.ToString(), driver);
                    if (car != null)
                    {


                        Customer customer = DataBaseSQL.GetCustomerForName(cbCustomer.SelectedItem.ToString());

                        Box box = new Box(tbNameOfBox.Text, weight, value, SityOtpr, SityTo, car, dateOtpr, dateTo, priceBox, customer);
                        box.PriceBox = Box.CalculateDeliveryCost(box);
                        DataBaseSQL.AddBox(box, user);
                        DataBaseSQL.LoadBoxesToDataGrid(dt, user);
                    }
                    else
                    {
                        MessageBox.Show("Транспорт или водитель не соответствуют друг другу");
                    }
                }
            }
            else if (cheng && txtBlockNameWindow.Text != "Обнов.")
            {
                car.NumberCar = tbNumberCar.Text;
                car.MarkOfCar = tbMarkCar.Text;
                car.name = cbDriverforCar.Text;
                car.MaxDostupVelue = Convert.ToDouble(tbMaxVelue.Text);
                car.MaxDostupWeight = Convert.ToDouble(tbMaxWeight.Text);

                DataBaseSQL.UpdateCar(car);
                DataBaseSQL.LoadCarsToDataGrid(dt);
            }
            else
            {
                double weight = Convert.ToDouble(tbWeight.Text),
                value = Convert.ToDouble(tbHight.Text),
                priceBox = Convert.ToDouble(tbProceproj.Text);
                DateTime dateOtpr = Convert.ToDateTime(dtOtpr.Text);
                DateTime dateTo = Convert.ToDateTime(dtDevel.Text);
                int SityOtpr = DataBaseSQL.GetCityIdByName(cbSityOpr.SelectedItem.ToString());
                int SityTo = DataBaseSQL.GetCityIdByName(cbSityDevelr.SelectedItem.ToString());
                Drivers driver = DataBaseSQL.GetDriver(cbDriver.SelectedItem.ToString());
                Car car = DataBaseSQL.GetCarForName(cbCar.Text.ToString(), driver);
                if (car != null)
                {


                    Customer customer = DataBaseSQL.GetCustomerForName(cbCustomer.SelectedItem.ToString());

                    Box newbox = new Box(tbNameOfBox.Text, weight, value, SityOtpr, SityTo, car, dateOtpr, dateTo, priceBox, customer);
                    newbox.PriceBox = Box.CalculateDeliveryCost(newbox);
                    DataBaseSQL.UpdateBox(box,newbox);
                    DataBaseSQL.LoadBoxesToDataGrid(dt, user);
                }
                else
                {
                    MessageBox.Show("Транспорт или водитель не соответствуют друг другу");
                }

            }
            }
            catch { }
        }
    }
}

