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
using System.Data.SqlClient;

namespace Sub_Zero
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

        private void btClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void WindowToEnter_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        public MyUsers user;
        private void OpenMainWind(object sender, RoutedEventArgs e)
        {
            if ((string)btMainWind.Content == "Войти")
            {
                
                if (DataBaseSQL.ValidateUser(txtLogin.txtInput.Text, pwdInput.Password))
                {
                    user = DataBaseSQL.newuser;

                    if (DataBaseSQL.newuser.position == "Сотруд.Транспрт.Отдел")
                    {
                       
                        LogWindow a = new LogWindow(DataBaseSQL.newuser.position, user);
                        a.Show();
                        Close();
                    }
                    else
                    {
                        LogWindow a = new LogWindow(DataBaseSQL.newuser.position, user);
                        a.Show();
                        Close();
                    }  
                   
                }
            }
            else 
            {
                if (DataBaseSQL.AddUser(txtLogin.txtInput.Text, pwdInput.Password, cbSelectorWorks.Text))
                {
                    EnrOrLog();
                }
            }

        }

        private void btEnter_Click(object sender, RoutedEventArgs e)
        {
            EnrOrLog();
        }
        private void EnrOrLog()
        {
            if (btEnter.Content == "Нет аккаунта? Зарегистрируйтесь")
            {
                lPositionOfEnOrSig.Content = "Регистрация";
                btEnter.Content = "Уже есть аккаунт? Войти";
                cbSelectorWorks.Visibility = Visibility.Visible;
                btMainWind.Content = "Регистрация";
            }

            else
            {
                    lPositionOfEnOrSig.Content = "Вход";
                    btEnter.Content = "Нет аккаунта? Зарегистрируйтесь";
                    cbSelectorWorks.Visibility = Visibility.Hidden;
                    btMainWind.Content = "Войти";
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            pwdInput.Clear();
            pwdInput.Focus();
        }

        private void pwdInput_PasswordChanged_1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(pwdInput.Password))
            {

                tbPlaceHolder.Visibility = Visibility.Visible;
            }
            else
            {
                //string password = txtInput.Text;
                //txtInput.Text = new string('*', password.Length);
                tbPlaceHolder.Visibility = Visibility.Hidden;
            }
        }
    }
}

