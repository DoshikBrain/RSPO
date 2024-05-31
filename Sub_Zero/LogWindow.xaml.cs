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
using System.Collections.ObjectModel;

namespace Sub_Zero
{
    /// <summary>
    /// Логика взаимодействия для LogWindow.xaml
    /// </summary>
    public partial class LogWindow : Window
    {
       static public MyUsers newuser;
        public LogWindow()
        {
            InitializeComponent();
            var provercka= new BrushConverter();
            ObservableCollection<Box> boxes = new ObservableCollection<Box>();

            //Create DataGride Items Info
            //boxes.Add(new Box(1,"Полина",55.42,1.68,"Минск","Монако","Amg 63",new DateTime(2024,2,27)));
            //DgBox.ItemsSource = boxes;
        }
        public LogWindow(string a,MyUsers user)
        {
            newuser= user;
            if(a== "Сотруд.Транспрт.Отдел")
            {
                InitializeComponent();
                DgCar.Visibility = Visibility.Visible;
                DgBox.Visibility= Visibility.Hidden;
                txtBlockNameTable.Text ="Таблица транспорта";
                btCityDo.Visibility= Visibility.Hidden;
                btCityOt.Visibility= Visibility.Hidden;
                btName.Content = "Водитель";
                txtBlock2.Text = "Водители";
                txtBlock.Text = "Добавить транспорт";
                btMarks.Visibility= Visibility.Hidden;
                DataBaseSQL.LoadCarsToDataGrid(DgCar);
            }
            else
            {
                InitializeComponent();
                DgCar.Visibility = Visibility.Hidden;
                DgBox.Visibility = Visibility.Visible;
                txtBlockNameTable.Text = "Таблица посылок";
                btCityDo.Visibility = Visibility.Visible;
                btCityOt.Visibility = Visibility.Visible;
                btName.Content = "Заказчики";
                txtBlock2.Text = "Закзчики";
                txtBlock.Text = "Добавить посылку";
                DataBaseSQL.LoadBoxesToDataGrid(DgBox, newuser);

            }
            
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }
        private bool IsMaximize=false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximize)
                {
                    WindowState = WindowState.Normal;
                    Width = 1080;
                    Height = 720;
                    IsMaximize = false;
                }
                else
                {
                    WindowState = WindowState.Minimized;
                    IsMaximize= true;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ChangeIcon(object sender, MouseButtonEventArgs e)
        {
         WIconChange a = new WIconChange();
            a.Topmost = true;
            a.Show();
        }

        private void Button_Click_Document(object sender, RoutedEventArgs e)
        {
            WDocument a = new WDocument();
            a.Topmost = true;
            a.Show();
        }

        private void Button_Click_Customer(object sender, RoutedEventArgs e)
        {
            WCustomer a = new WCustomer(txtBlock2.Text);
            a.Show();
        }

        private void Button_Click_AddBox(object sender, RoutedEventArgs e)
        {
            if (txtBlockNameTable.Text == "Таблица транспорта")
            {
                WBox a = new WBox(txtBlock.Text, DgCar, newuser);
                a.Show();
            }
            else
            {
                WBox a = new WBox(txtBlock.Text, DgBox, newuser);
                a.Show();
            }
           
            
        }

        private void Button_Click_delet(object sender, RoutedEventArgs e)
        {
            //try
            if (txtBlockNameTable.Text == "Таблица транспорта")
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить этот трансспорт", "Подтверждение удаления", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    Car selectedDriver = (Car)DgCar.SelectedItem;
                    Drivers driv = DataBaseSQL.GetDriver(selectedDriver.name);
                    selectedDriver.Driver = driv.id;
                    selectedDriver = DataBaseSQL.GetCar(selectedDriver.NumberCar);
                    DataBaseSQL.DeleteCar(selectedDriver);
                    DataBaseSQL.LoadDriversToDataGrid(DgCar);
                }
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить эту посылку", "Подтверждение удаления", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    Box selectedBox = (Box)DgBox.SelectedItem;
                    selectedBox = DataBaseSQL.GetBox(selectedBox.NameBox);
                    DataBaseSQL.DeleteBox(selectedBox);
                    DataBaseSQL.LoadBoxesToDataGrid(DgBox, newuser);
                }
            }
            //catch
            //{
            //    MessageBox.Show("Ошибка");

            //}
        }

        private void Button_Click_Updata(object sender, RoutedEventArgs e)
        {
            if (txtBlockNameTable.Text == "Таблица транспорта")
            {

                Car selectedDriver = (Car)DgCar.SelectedItem;
                selectedDriver = DataBaseSQL.GetCar(selectedDriver.NumberCar);
                WBox a = new WBox(txtBlock.Text, DgCar, selectedDriver);
                a.Topmost = true;
                a.Show();
                DataBaseSQL.LoadDriversToDataGrid(DgCar);
            }
            else
            {
                Box selectedBox = (Box)DgBox.SelectedItem;
                selectedBox = DataBaseSQL.GetBox(selectedBox.NameBox);
                WBox a = new WBox(txtBlock.Text, DgCar, selectedBox, newuser);
                a.Topmost = true;
                a.Show();
                DataBaseSQL.LoadDriversToDataGrid(DgCar);
            }
        }

        private void Button_Delete_box(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить эту посылку", "Подтверждение удаления", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                Box selectedBox = (Box)DgBox.SelectedItem;
                selectedBox = DataBaseSQL.GetBox(selectedBox.NameBox);
                DataBaseSQL.DeleteBox(selectedBox);
                DataBaseSQL.LoadBoxesToDataGrid(DgBox, newuser);
            } 
        }
        private void Button_Change_box(object sender, RoutedEventArgs e)
        {

            Box selectedBox = (Box)DgBox.SelectedItem;
            selectedBox = DataBaseSQL.GetBox(selectedBox.NameBox);
            WBox a = new WBox(txtBlock.Text, DgBox, selectedBox, newuser);
            a.Topmost = true;
            a.Show();
            DataBaseSQL.LoadBoxesToDataGrid(DgBox, newuser);
        }

        private void btMarks_Click(object sender, RoutedEventArgs e)
        {
            WMarks a = new WMarks();
            a.Show();

       }
    }
}




