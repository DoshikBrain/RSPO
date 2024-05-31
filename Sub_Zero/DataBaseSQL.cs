using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media.Animation;
using OfficeOpenXml;
using System.IO;
using System.Windows.Documents;
using System.Xml.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Paragraph = DocumentFormat.OpenXml.Wordprocessing.Paragraph;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Sub_Zero
{

    public class DataBaseSQL
    {
        static private string connectionString = "Data Source=USER;Initial Catalog=Sub_zero;Integrated Security=True;Encrypt=False";
        static public MyUsers newuser;
        static public Customer newCustomer;
        static public bool AddUser(string username, string password, string position)
        {


            try
            {
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(position))
                {
                    throw new ArgumentException("Имя, пароль, или тип работника не могут быть пустыми");
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string checkQuery = "SELECT COUNT(*) FROM users WHERE nickname = @Username";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@Username", username);

                        int userCount = (int)checkCommand.ExecuteScalar();

                        if (userCount > 0)
                        {
                            return false;
                        }
                    }

                    string insertQuery = "INSERT INTO users (nickname, password, kindOfWork) VALUES (@Username, @Password, @Position)";
                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@Username", username);
                        insertCommand.Parameters.AddWithValue("@Password", password);
                        insertCommand.Parameters.AddWithValue("@Position", position);

                        insertCommand.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
                throw;
            }
        }
        static public bool ValidateUser(string username, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    throw new ArgumentException("Имя пользователя и пароль не могут быть пустыми или равными null.");
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM users WHERE nickname = @Username AND password = @Password";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);

                        int count = (int)command.ExecuteScalar();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                newuser = new MyUsers(username, password, reader[3].ToString(), Convert.ToInt32(reader[0]));
                            }
                        }

                        return count > 0;
                    }
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
                throw;
            }

        }
        static public bool AddDriver(Drivers driver)
        {
            try
            {
                if (driver == null)
                {
                    throw new ArgumentException("Объект Driver не может быть null.");
                }

                if (string.IsNullOrEmpty(driver.FullName) || string.IsNullOrEmpty(driver.PhoneNumber) || string.IsNullOrEmpty(driver.DrivingLicenseNumber) || driver.DateOfBirth == null)
                {
                    throw new ArgumentException("Поля FullName, PhoneNumber, DrivingLicenseNumber и DateOfBirth не могут быть пустыми или равными null.");
                }

                if (driver.PhoneNumber.Length != 13)
                {
                    throw new ArgumentException("Номер телефона должен содержать  цифр.");
                }


                if (driver.DrivingLicenseNumber.Length < 6 || !driver.DrivingLicenseNumber.All(char.IsLetterOrDigit))
                {
                    throw new ArgumentException("Водительское удостоверение должно содержать не менее 6 цифр или букв.");
                }
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO driever (fio, phoneNumber, driverLessons, dateOfBirth) VALUES (@fio, @phoneNumber, @driverLessons, @dateOfBirth)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@fio", driver.FullName);
                        command.Parameters.AddWithValue("@phoneNumber", driver.PhoneNumber);
                        command.Parameters.AddWithValue("@driverLessons", driver.DrivingLicenseNumber);
                        command.Parameters.AddWithValue("@dateOfBirth", driver.DateOfBirth);

                        command.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
                throw;
            }
        }
        public static Drivers GetDriver(string name)
        {
            Drivers driver = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM driever WHERE fio = @fio";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@fio", name);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            driver = new Drivers(
                                id: reader.GetInt32(reader.GetOrdinal("id")),
                                fullName: reader.GetString(reader.GetOrdinal("fio")),
                                phoneNumber: reader.GetString(reader.GetOrdinal("phoneNumber")),
                                drivingLicenseNumber: reader.GetString(reader.GetOrdinal("driverLessons")),
                                dateOfBirth: reader.GetDateTime(reader.GetOrdinal("dateOfBirth"))
                            );
                        }
                    }
                }
            }

            return driver;
        }
        public static Drivers GetDriver(int id)
        {
            Drivers driver = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM driever WHERE id = @id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            driver = new Drivers(
                                id: reader.GetInt32(reader.GetOrdinal("id")),
                                fullName: reader.GetString(reader.GetOrdinal("fio")),
                                phoneNumber: reader.GetString(reader.GetOrdinal("phoneNumber")),
                                drivingLicenseNumber: reader.GetString(reader.GetOrdinal("driverLessons")),
                                dateOfBirth: reader.GetDateTime(reader.GetOrdinal("dateOfBirth"))
                            );
                        }
                    }
                }
            }

            return driver;
        }
        static public bool DeleteDriver(Drivers driver)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM driever WHERE id = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", driver.id);
                    command.ExecuteNonQuery();
                }
            }
            return true;
        }
        static public bool UpdateDriver(Drivers driver)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE driever SET fio = @fio, phoneNumber = @phoneNumber, driverLessons = @driverLessons, dateOfBirth = @dateOfBirth WHERE id = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", driver.id);
                    command.Parameters.AddWithValue("@fio", driver.FullName);
                    command.Parameters.AddWithValue("@phoneNumber", driver.PhoneNumber);
                    command.Parameters.AddWithValue("@driverLessons", driver.DrivingLicenseNumber);
                    command.Parameters.AddWithValue("@dateOfBirth", driver.DateOfBirth);

                    command.ExecuteNonQuery();
                }
            }
            return true;
        }
        public static void LoadDriversToDataGrid(System.Windows.Controls.DataGrid dgDriver)
        {
            List<Drivers> drivers = new List<Drivers>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM driever";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string fullName = reader["fio"].ToString();
                            string phoneNumber = reader["phoneNumber"].ToString();
                            string drivingLicenseNumber = reader["driverLessons"].ToString();
                            DateTime dateOfBirth = Convert.ToDateTime(reader["dateOfBirth"].ToString());

                            Drivers driver = new Drivers(fullName, phoneNumber, drivingLicenseNumber, dateOfBirth);
                            drivers.Add(driver);
                        }
                    }
                }
            }
            dgDriver.ItemsSource = drivers;
        }
        public static bool AddCar(Car car)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string checkQuery = "SELECT COUNT(*) FROM Car WHERE idDriver = @Driver";
                using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@Driver", car.Driver);
                    int count = (int)checkCommand.ExecuteScalar();

                    if (count > 0)
                    {
                        return false;
                    }
                }
          
                string query = "INSERT INTO Car ( MarkOfCar, idDriver, MaxDostupVelue, MaxDostupWeight, NumberCar) VALUES ( @MarkOfCar, @Driver, @MaxDostupVelue, @MaxDostupWeight, @NumberCar)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@MarkOfCar", car.MarkOfCar);
                    command.Parameters.AddWithValue("@Driver", car.Driver);
                    command.Parameters.AddWithValue("@MaxDostupVelue", car.MaxDostupVelue);
                    command.Parameters.AddWithValue("@MaxDostupWeight", car.MaxDostupWeight);
                    command.Parameters.AddWithValue("@NumberCar", car.NumberCar);

                    command.ExecuteNonQuery();
                }
            }
            return true;
        }
        static public bool DeleteCar(Car car)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Car WHERE id = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", car.id);
                    command.ExecuteNonQuery();
                }
            }
            return true;
        }
        static public Car GetCar(string numberCar)
        {
            Car car = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Car WHERE NumberCar = @NumberCar";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NumberCar", numberCar);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            car = new Car(
                                id: (int)reader["id"],
                                markOfCar: (string)reader["MarkOfCar"],
                                driver: (int)reader["IdDriver"],
                                maxDostupVelue: (double)reader["MaxDostupVelue"],
                                maxDostupWeight: (double)reader["MaxDostupWeight"],
                                numberCar: (string)reader["NumberCar"]
                            );
                        }
                    }
                }
            }

            return car;
        }
        static public Car GetCar(int id)
        {
            Car car = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Car WHERE id = @id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            car = new Car(
                                id: (int)reader["id"],
                                markOfCar: (string)reader["MarkOfCar"],
                                driver: (int)reader["IdDriver"],
                                maxDostupVelue: (double)reader["MaxDostupVelue"],
                                maxDostupWeight: (double)reader["MaxDostupWeight"],
                                numberCar: (string)reader["NumberCar"]
                            );
                        }
                    }
                }
            }

            return car;
        }
        static public Car GetCarForName(string nameCar)
        {
            Car car = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Car WHERE MarkOfCar = @NameCar";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NameCar", nameCar);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            car = new Car(
                                id: (int)reader["id"],
                                markOfCar: (string)reader["MarkOfCar"],
                                driver: (int)reader["IdDriver"],
                                maxDostupVelue: (double)reader["MaxDostupVelue"],
                                maxDostupWeight: (double)reader["MaxDostupWeight"],
                                numberCar: (string)reader["NumberCar"]
                            );
                        }
                    }
                }
            }

            return car;
        }
        static public Car GetCarForName(string nameCar, Drivers driver)
        {
            Car car = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Car WHERE MarkOfCar = @NameCar And idDriver=@idDriver";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NameCar", nameCar);
                    command.Parameters.AddWithValue("@idDriver", driver.id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            car = new Car(
                                id: (int)reader["id"],
                                markOfCar: (string)reader["MarkOfCar"],
                                driver: (int)reader["IdDriver"],
                                maxDostupVelue: (double)reader["MaxDostupVelue"],
                                maxDostupWeight: (double)reader["MaxDostupWeight"],
                                numberCar: (string)reader["NumberCar"]
                            );
                        }
                    }
                }
            }

            return car;
        }
        static public bool UpdateCar(Car car)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Car SET MarkOfCar = @MarkOfCar, IdDriver = @Driver, MaxDostupVelue = @MaxDostupVelue, MaxDostupWeight = @MaxDostupWeight WHERE id = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", car.id);
                    command.Parameters.AddWithValue("@MarkOfCar", car.MarkOfCar);
                    command.Parameters.AddWithValue("@Driver", car.Driver);
                    command.Parameters.AddWithValue("@MaxDostupVelue", car.MaxDostupVelue);
                    command.Parameters.AddWithValue("@MaxDostupWeight", car.MaxDostupWeight);


                    command.ExecuteNonQuery();
                }
            }
            return true;
        }
        static public void LoadDriversIntoComboBox(ComboBox comboBox)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT fio FROM driever";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comboBox.Items.Add(reader["fio"].ToString());
                        }
                    }
                }
            }
        }
        public static void LoadCarsToDataGrid(System.Windows.Controls.DataGrid dgCar)
        {
            List<Car> cars = new List<Car>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM car";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string mark = reader["markOfCar"].ToString();
                            int drivers_ = Convert.ToInt32(reader["idDriver"]);
                            string number = reader["numberCar"].ToString();
                            double Velue = Convert.ToDouble(reader["maxDostupVelue"]);
                            double Weight = Convert.ToDouble(reader["maxDostupWeight"]);

                            Drivers driver = DataBaseSQL.GetDriver(drivers_);
                            Car car = new Car(mark, driver.FullName, Velue, Weight, number);
                            cars.Add(car);
                        }
                    }
                }
            }
            dgCar.ItemsSource = cars;
        }
        static public bool AddCustomerFiz(string fio, string numberOfPhone, string pochta, bool isUrFace, string pasport, string adress)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string insertQuery = "INSERT INTO Customer (fio, numberOfPhone, pochta, isUrFace, pasport, adress) VALUES (@Fio, @NumberOfPhone, @Pochta, @IsUrFace, @Pasport, @Adress)";
                using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                {
                    insertCommand.Parameters.AddWithValue("@Fio", fio);
                    insertCommand.Parameters.AddWithValue("@NumberOfPhone", numberOfPhone);
                    insertCommand.Parameters.AddWithValue("@Pochta", pochta);
                    insertCommand.Parameters.AddWithValue("@IsUrFace", isUrFace);
                    insertCommand.Parameters.AddWithValue("@Pasport", pasport);
                    insertCommand.Parameters.AddWithValue("@Adress", adress);

                    int rowsAffected = insertCommand.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }
        static public bool AddCustomerFiz(Customer cust)
        {
            try
            {
                if (cust == null)
                {
                    throw new ArgumentException("Объект Customer не может быть null.");
                }

                if (string.IsNullOrEmpty(cust.Fio) || string.IsNullOrEmpty(cust.NumberOfPhone) || string.IsNullOrEmpty(cust.Pochta) || string.IsNullOrEmpty(cust.Adress))
                {
                    throw new ArgumentException("Поля Fio, NumberOfPhone, Pochta и Adress не могут быть пустыми или равными null.");
                }


                if (cust.NumberOfPhone.Length != 13)
                {
                    throw new ArgumentException("Номер телефона должен содержать 11 цифр.");
                }

                if (!cust.Pochta.EndsWith("@mail.ru") && !cust.Pochta.EndsWith("@gumail.ru"))
                {
                    throw new ArgumentException("Почта должна быть @mail.ru или @gumail.ru.");
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string insertQuery = "INSERT INTO Customer (fio, numberOfPhone, pochta, isUrFace, pasport, adress) VALUES (@Fio, @NumberOfPhone, @Pochta, @IsUrFace, @Pasport, @Adress)";
                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@Fio", cust.Fio);
                        insertCommand.Parameters.AddWithValue("@NumberOfPhone", cust.NumberOfPhone);
                        insertCommand.Parameters.AddWithValue("@Pochta", cust.Pochta);
                        insertCommand.Parameters.AddWithValue("@IsUrFace", 0);
                        insertCommand.Parameters.AddWithValue("@Pasport", cust.Pasport ?? (object)DBNull.Value);
                        insertCommand.Parameters.AddWithValue("@Adress", cust.Adress);

                        int rowsAffected = insertCommand.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
                throw;
            }

        }
        public static void LoadCustomersToDataGrid(System.Windows.Controls.DataGrid dgCust)
        {
            List<Customer> customers = new List<Customer>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Customer";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["id"]);
                            string fio = reader["fio"].ToString();
                            string numberOfPhone = reader["numberOfPhone"].ToString();
                            string pochta = reader["pochta"].ToString();
                            int isUrFace = Convert.ToInt32(reader["isUrFace"]);
                            string pasport = reader["pasport"].ToString();
                            string adress = reader["adress"].ToString();
                            string ynp = reader["ynp"].ToString();
                            string nameCompany = reader["nameCompany"].ToString();
                            string adressCompany = reader["adressCompany"].ToString();
                            int rastSchet;
                            int.TryParse(reader["rastSchet"].ToString(), out rastSchet);
                            string nameBank = reader["nameBank"].ToString();
                            string codeBank = reader["codeBank"].ToString();
                            Customer customer = new Customer(id, fio, numberOfPhone, pochta, isUrFace, pasport, adress, ynp, nameCompany, adressCompany, rastSchet, nameBank, codeBank);
                            customers.Add(customer);
                        }
                    }
                }
            }
            dgCust.ItemsSource = customers;
        }
        public static Customer GetCustomer(string phone)
        {
            Customer customer = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Customer WHERE numberOfPhone = @phone";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@phone", phone);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["id"]);
                            string fio = reader["fio"].ToString();
                            string numberOfPhone = reader["numberOfPhone"].ToString();
                            string pochta = reader["pochta"].ToString();
                            int isUrFace = Convert.ToInt32(reader["isUrFace"]);
                            string pasport = reader["pasport"].ToString();
                            string adress = reader["adress"].ToString();
                            string ynp = reader["ynp"].ToString();
                            string nameCompany = reader["nameCompany"].ToString();
                            string adressCompany = reader["adressCompany"].ToString();
                            int rastSchet;
                            int.TryParse(reader["rastSchet"].ToString(), out rastSchet);
                            string nameBank = reader["nameBank"].ToString();
                            string codeBank = reader["codeBank"].ToString();

                            customer = new Customer(id, fio, numberOfPhone, pochta, isUrFace, pasport, adress, ynp, nameCompany, adressCompany, rastSchet, nameBank, codeBank);
                        }
                    }
                }
            }

            return customer;
        }
        public static Customer GetCustomer(int idC)
        {
            Customer customer = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Customer WHERE id = @id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", idC);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["id"]);
                            string fio = reader["fio"].ToString();
                            string numberOfPhone = reader["numberOfPhone"].ToString();
                            string pochta = reader["pochta"].ToString();
                            int isUrFace = Convert.ToInt32(reader["isUrFace"]);
                            string pasport = reader["pasport"].ToString();
                            string adress = reader["adress"].ToString();
                            string ynp = reader["ynp"].ToString();
                            string nameCompany = reader["nameCompany"].ToString();
                            string adressCompany = reader["adressCompany"].ToString();
                            int rastSchet;
                            int.TryParse(reader["rastSchet"].ToString(), out rastSchet);
                            string nameBank = reader["nameBank"].ToString();
                            string codeBank = reader["codeBank"].ToString();

                            customer = new Customer(id, fio, numberOfPhone, pochta, isUrFace, pasport, adress, ynp, nameCompany, adressCompany, rastSchet, nameBank, codeBank);
                        }
                    }
                }
            }

            return customer;
        }
        public static Customer GetCustomerForName(string name)
        {
            Customer customer = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Customer WHERE fio = @fio";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@fio", name);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["id"]);
                            string fio = reader["fio"].ToString();
                            string numberOfPhone = reader["numberOfPhone"].ToString();
                            string pochta = reader["pochta"].ToString();
                            int isUrFace = Convert.ToInt32(reader["isUrFace"]);
                            string pasport = reader["pasport"].ToString();
                            string adress = reader["adress"].ToString();
                            string ynp = reader["ynp"].ToString();
                            string nameCompany = reader["nameCompany"].ToString();
                            string adressCompany = reader["adressCompany"].ToString();
                            int rastSchet;
                            int.TryParse(reader["rastSchet"].ToString(), out rastSchet);
                            string nameBank = reader["nameBank"].ToString();
                            string codeBank = reader["codeBank"].ToString();

                            customer = new Customer(id, fio, numberOfPhone, pochta, isUrFace, pasport, adress, ynp, nameCompany, adressCompany, rastSchet, nameBank, codeBank);
                        }
                    }
                }
            }

            return customer;
        }
        public static bool UpdateCustomer(Customer customer)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Customer SET fio = @Fio, numberOfPhone = @NumberOfPhone, pochta = @Pochta, isUrFace = @IsUrFace, pasport = @Pasport, adress = @Adress, ynp = @Ynp, nameCompany = @NameCompany, adressCompany = @AdressCompany, rastSchet = @RastSchet, nameBank = @NameBank, codeBank = @CodeBank WHERE id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", customer.Id);
                    command.Parameters.AddWithValue("@Fio", customer.Fio);
                    command.Parameters.AddWithValue("@NumberOfPhone", customer.NumberOfPhone);
                    command.Parameters.AddWithValue("@Pochta", customer.Pochta);
                    command.Parameters.AddWithValue("@IsUrFace", customer.IsUrFace);
                    command.Parameters.AddWithValue("@Pasport", customer.Pasport);
                    command.Parameters.AddWithValue("@Adress", customer.Adress);
                    command.Parameters.AddWithValue("@Ynp", customer.Ynp);
                    command.Parameters.AddWithValue("@NameCompany", customer.NameCompany);
                    command.Parameters.AddWithValue("@AdressCompany", customer.AdressCompany);
                    command.Parameters.AddWithValue("@RastSchet", customer.RastSchet);
                    command.Parameters.AddWithValue("@NameBank", customer.NameBank);
                    command.Parameters.AddWithValue("@CodeBank", customer.CodeBank);

                    command.ExecuteNonQuery();
                }
            }
            return true;
        }
        static public bool DeleteCustomer(Customer customer)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Customer WHERE id = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", customer.Id);
                    command.ExecuteNonQuery();
                }
            }
            return true;
        }

        public static bool DeleteBox(Box box)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string boxQuery = "SELECT * FROM Box WHERE id = @id";
                using (SqlCommand boxCommand = new SqlCommand(boxQuery, connection))
                {
                    boxCommand.Parameters.AddWithValue("@id", box.Id);
                    SqlDataReader boxReader = boxCommand.ExecuteReader();
                    double boxWeight = 0;
                    double boxValue = 0;
                    int carId = 0;
                    if (boxReader.Read())
                    {
                        boxWeight = Convert.ToDouble(boxReader["Weight"].ToString());
                        boxValue = Convert.ToDouble(boxReader["Value"].ToString());
                        carId = Convert.ToInt32(boxReader["IdCar"].ToString());
                    }
                    boxReader.Close();

                    string updateCarQuery = "UPDATE Car SET Weight = Weight - @Weight, Velue = Velue - @Value WHERE id = @IdCar";
                    using (SqlCommand updateCarCommand = new SqlCommand(updateCarQuery, connection))
                    {
                        updateCarCommand.Parameters.AddWithValue("@Weight", boxWeight);
                        updateCarCommand.Parameters.AddWithValue("@Value", boxValue);
                        updateCarCommand.Parameters.AddWithValue("@IdCar", carId);
                        updateCarCommand.ExecuteNonQuery();
                    }
                }
                // Удаление связанных записей в таблице Dostavka
                string deleteDostavkaQuery = "DELETE FROM Dostavka WHERE idBox = @id";
                using (SqlCommand deleteDostavkaCommand = new SqlCommand(deleteDostavkaQuery, connection))
                {
                    deleteDostavkaCommand.Parameters.AddWithValue("@id", box.Id);
                    deleteDostavkaCommand.ExecuteNonQuery();
                }
                // Удаление записи из таблицы Box
                string query = "DELETE FROM Box WHERE id = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", box.Id);
                    command.ExecuteNonQuery();
                }
            }
            return true;
        }


        public static Box GetBox(string nameBox)
        {
            Box box = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Box WHERE NameBox = @NameBox";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NameBox", nameBox);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int idCar = (int)reader["IdCar"];
                            Car car = DataBaseSQL.GetCar(idCar);
                            Customer customer = DataBaseSQL.GetCustomer((int)reader["IdCustomer"]);
                            box = new Box((int)reader["Id"], (string)reader["NameBox"], (double)reader["Weight"], (double)reader["Value"], (int)reader["SityDostav"], (int)reader["SityOtprav"], car,
                                dateOtprav: (DateTime)reader["dateOtprav"],
                                datePolych: (DateTime)reader["datePolych"],
                                price: (double)reader["Price"],
                                priceBox: (double)reader["PriceBox"],
                                Customer: customer
                            );
                        }

                    }
                }
            }

            return box;
        }
        public static void LoadBoxesToDataGrid(System.Windows.Controls.DataGrid dgBox, MyUsers user)
        {
            List<Box> boxes = new List<Box>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT * FROM Box WHERE idUser={user.id}";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Car car = DataBaseSQL.GetCar(Convert.ToInt32(reader["IdCar"].ToString()));
                            Customer customer = DataBaseSQL.GetCustomer(Convert.ToInt32(reader["IdCustomer"].ToString()));
                            int id = Convert.ToInt32(reader["Id"].ToString());
                            string nameBox = reader["NameBox"].ToString();
                            double weight = Convert.ToDouble(reader["Weight"].ToString());
                            double value = Convert.ToDouble(reader["Value"].ToString());
                            int sityDostav = Convert.ToInt32(reader["SityDostav"].ToString());
                            int sityOtprav = Convert.ToInt32(reader["SityOtprav"].ToString());
                            DateTime dateOtprav = Convert.ToDateTime(reader["dateOtprav"].ToString());
                            DateTime datePolych = Convert.ToDateTime(reader["datePolych"].ToString());
                            double price = Convert.ToDouble(reader["Price"].ToString());
                            double priceBox = Convert.ToDouble(reader["PriceBox"].ToString());
                            string nameSityDostav = DataBaseSQL.GetCityIdByName(Convert.ToInt32(reader["SityDostav"].ToString()));
                            string namesityOtprav = DataBaseSQL.GetCityIdByName(Convert.ToInt32(reader["SityOtprav"].ToString()));


                            Box box = new Box(id, nameBox, weight, value, sityDostav, sityOtprav, car, dateOtprav, datePolych, price, priceBox, customer);
                            box.NameSityDostav = nameSityDostav;
                            box.NameSityOtprav = namesityOtprav;
                            boxes.Add(box);
                        }
                    }
                }
            }
            dgBox.ItemsSource = boxes;
        }
        public static bool AddBox(Box box, MyUsers s)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string carQuery = "SELECT * FROM Car WHERE id = @IdCar ";
                using (SqlCommand carCommand = new SqlCommand(carQuery, connection))
                {
                    carCommand.Parameters.AddWithValue("@IdCar", box.Car.id);
                    using (SqlDataReader carReader = carCommand.ExecuteReader())
                    {
                        if (carReader.Read())
                        {
                            double maxDostupWeight = Convert.ToDouble(carReader["MaxDostupWeight"].ToString());
                            double maxDostupVelue = Convert.ToDouble(carReader["MaxDostupVelue"].ToString());
                            double Weight = string.IsNullOrEmpty(carReader["Weight"].ToString()) ? 0 : Convert.ToDouble(carReader["Weight"].ToString());
                            double Velue = string.IsNullOrEmpty(carReader["Velue"].ToString()) ? 0 : Convert.ToDouble(carReader["Velue"].ToString());

                            if (box.Weight > maxDostupWeight || box.Value > maxDostupVelue || maxDostupWeight < Weight + box.Weight || maxDostupVelue < Velue + box.Value)
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                int idBox;
                string query = "INSERT INTO box (nameBox, weight, value, sityDostav, sityOtprav, idCar, dateOtprav, datePolych, price, priceBox, idCustomer,idUser) OUTPUT INSERTED.id VALUES (@NameBox, @Weight, @Value, @SityDostav, @SityOtprav, @IdCar, @dateOtprav, @datePolych, @Price, @PriceBox, @IdCustomer,@IdUser)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NameBox", box.NameBox);
                    command.Parameters.AddWithValue("@Weight", box.Weight);
                    command.Parameters.AddWithValue("@Value", box.Value);
                    command.Parameters.AddWithValue("@SityDostav", box.SityDostav);
                    command.Parameters.AddWithValue("@SityOtprav", box.SityOtprav);
                    command.Parameters.AddWithValue("@IdCar", box.Car.id);
                    command.Parameters.AddWithValue("@dateOtprav", box.dateOtprav);
                    command.Parameters.AddWithValue("@datePolych", box.datePolych);
                    command.Parameters.AddWithValue("@Price", box.Price);
                    command.Parameters.AddWithValue("@PriceBox", box.PriceBox);
                    command.Parameters.AddWithValue("@IdCustomer", box.Customer.Id);
                    command.Parameters.AddWithValue("@IdUser", s.id);
                    idBox = (int)command.ExecuteScalar();
                }

                string insertDostavkaQuery = "INSERT INTO Dostavka ( DateDostav,idBox) VALUES (@DateDostav, @IdBox)";
                using (SqlCommand insertDostavkaCommand = new SqlCommand(insertDostavkaQuery, connection))
                {
                    insertDostavkaCommand.Parameters.AddWithValue("@IdBox", idBox);
                    insertDostavkaCommand.Parameters.AddWithValue("@DateDostav", box.datePolych);
                    insertDostavkaCommand.ExecuteNonQuery();
                }


                string updateCarQuery = "UPDATE Car SET Weight = Weight + @Weight, Velue = Velue + @Value WHERE id = @IdCar";
                using (SqlCommand updateCarCommand = new SqlCommand(updateCarQuery, connection))
                {
                    updateCarCommand.Parameters.AddWithValue("@Weight", box.Weight);
                    updateCarCommand.Parameters.AddWithValue("@Value", box.Value);
                    updateCarCommand.Parameters.AddWithValue("@IdCar", box.Car.id);
                    updateCarCommand.ExecuteNonQuery();
                }
            }

       
            return true;
        }
        static public void LoadCitiesIntoComboBox(ComboBox comboBox)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT city FROM City";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comboBox.Items.Add(reader["city"].ToString());
                        }
                    }
                }
            }
        }
        public static void LoadCustomersIntoComboBox(ComboBox comboBox)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT fio FROM Customer";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comboBox.Items.Add(reader["fio"].ToString());
                        }
                    }
                }
            }
        }
        public static void LoadCarsIntoComboBox(ComboBox comboBox)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT MarkOfCar FROM car";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comboBox.Items.Add(reader["MarkOfCar"].ToString());
                        }
                    }
                }
            }
        }
        static public bool AddCustomerUr(Customer cust)
        {
            try
            {
                if (cust == null)
                {
                    throw new ArgumentException("Объект Customer не может быть null.");
                }

                if (string.IsNullOrEmpty(cust.Fio) || string.IsNullOrEmpty(cust.NumberOfPhone) || string.IsNullOrEmpty(cust.Pochta) || string.IsNullOrEmpty(cust.Adress))
                {
                    throw new ArgumentException("Поля Fio, NumberOfPhone, Pochta и Adress не могут быть пустыми или равными null.");
                }

                if (cust.NumberOfPhone.Length != 13)
                {
                    throw new ArgumentException("Номер телефона должен содержать 12 цифр.");
                }

                if (!cust.Pochta.EndsWith("@mail.ru") && !cust.Pochta.EndsWith("@gumail.ru"))
                {
                    throw new ArgumentException("Почта должна быть @mail.ru или @gumail.ru.");
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string insertQuery = "INSERT INTO Customer ( fio, numberOfPhone, pochta, isUrFace, pasport, adress, ynp, nameCompany, adressCompany, rastSchet,nameBank ,codeBank ) VALUES (@Fio,@NumberOfPhone,@Pochta,@IsUrFace,@Pasport,@Adress,@Ynp,@NameCompany,@AdressCompany,@RastSchet ,@NameBank ,@CodeBank)";
                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@Fio", cust.Fio);
                        insertCommand.Parameters.AddWithValue("@NumberOfPhone", cust.NumberOfPhone);
                        insertCommand.Parameters.AddWithValue("@Pochta", cust.Pochta);
                        insertCommand.Parameters.AddWithValue("@IsUrFace", cust.IsUrFace);
                        insertCommand.Parameters.AddWithValue("@Pasport", cust.Pasport ?? (object)DBNull.Value);
                        insertCommand.Parameters.AddWithValue("@Adress", cust.Adress);
                        insertCommand.Parameters.AddWithValue("@Ynp", cust.Ynp ?? (object)DBNull.Value);
                        insertCommand.Parameters.AddWithValue("@NameCompany", cust.NameCompany ?? (object)DBNull.Value);
                        insertCommand.Parameters.AddWithValue("@AdressCompany", cust.AdressCompany ?? (object)DBNull.Value);
                        insertCommand.Parameters.AddWithValue("@RastSchet", cust.RastSchet);
                        insertCommand.Parameters.AddWithValue("@NameBank", cust.NameBank ?? (object)DBNull.Value);
                        insertCommand.Parameters.AddWithValue("@CodeBank", cust.CodeBank ?? (object)DBNull.Value);

                        int rowsAffected = insertCommand.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
                throw;
            }
        }
        static public int GetCityIdByName(string cityName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT id FROM City WHERE city = @cityName";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@cityName", cityName);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return int.Parse(reader["id"].ToString());
                        }
                    }
                }
            }
            return 0;
        }
        static public string GetCityIdByName(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM City WHERE id = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader["city"].ToString();
                        }
                    }
                }
            }
            return null;
        }
        public static bool UpdateBox(Box oldBox, Box newBox)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string updateCarQuery = "UPDATE Car SET Weight = Weight - @OldWeight, Velue = Velue - @OldValue WHERE id = @IdCar";
                using (SqlCommand updateCarCommand = new SqlCommand(updateCarQuery, connection))
                {
                    updateCarCommand.Parameters.AddWithValue("@OldWeight", oldBox.Weight);
                    updateCarCommand.Parameters.AddWithValue("@OldValue", oldBox.Value);
                    updateCarCommand.Parameters.AddWithValue("@IdCar", oldBox.Car.id);
                    updateCarCommand.ExecuteNonQuery();
                }
                string query = "UPDATE Box SET nameBox = @NameBox, weight = @Weight, value = @Value, sityDostav = @SityDostav, sityOtprav = @SityOtprav, idCar = @IdCar, dateOtprav = @dateOtprav, datePolych = @datePolych, price = @Price, priceBox = @PriceBox, idCustomer = @IdCustomer WHERE id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", oldBox.Id);
                    command.Parameters.AddWithValue("@NameBox", newBox.NameBox);
                    command.Parameters.AddWithValue("@Weight", newBox.Weight);
                    command.Parameters.AddWithValue("@Value", newBox.Value);
                    command.Parameters.AddWithValue("@SityDostav", newBox.SityDostav);
                    command.Parameters.AddWithValue("@SityOtprav", newBox.SityOtprav);
                    command.Parameters.AddWithValue("@IdCar", newBox.Car.id);
                    command.Parameters.AddWithValue("@dateOtprav", newBox.dateOtprav);
                    command.Parameters.AddWithValue("@datePolych", newBox.datePolych);
                    command.Parameters.AddWithValue("@Price", newBox.Price);
                    command.Parameters.AddWithValue("@PriceBox", newBox.PriceBox);
                    command.Parameters.AddWithValue("@IdCustomer", newBox.Customer.Id);
                    command.ExecuteNonQuery();
                }
                updateCarQuery = "UPDATE Car SET Weight = Weight + @NewWeight, Velue = Velue + @NewValue WHERE id = @IdCar";
                using (SqlCommand updateCarCommand = new SqlCommand(updateCarQuery, connection))
                {
                    updateCarCommand.Parameters.AddWithValue("@NewWeight", newBox.Weight);
                    updateCarCommand.Parameters.AddWithValue("@NewValue", newBox.Value);
                    updateCarCommand.Parameters.AddWithValue("@IdCar", newBox.Car.id);
                    updateCarCommand.ExecuteNonQuery();
                }
                string checkDostavkaQuery = "SELECT COUNT(*) FROM Dostavka WHERE idBox = @IdBox";
                using (SqlCommand checkDostavkaCommand = new SqlCommand(checkDostavkaQuery, connection))
                {
                    checkDostavkaCommand.Parameters.AddWithValue("@IdBox", oldBox.Id);
                    int count = (int)checkDostavkaCommand.ExecuteScalar();
                    if (count > 0)
                    {
                        string updateDostavkaQuery = "UPDATE Dostavka SET DateDostav = @DateDostav WHERE idBox = @IdBox";
                        using (SqlCommand updateDostavkaCommand = new SqlCommand(updateDostavkaQuery, connection))
                        {
                            updateDostavkaCommand.Parameters.AddWithValue("@IdBox", oldBox.Id);
                            updateDostavkaCommand.Parameters.AddWithValue("@DateDostav", newBox.datePolych);
                            updateDostavkaCommand.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        string insertDostavkaQuery = "INSERT INTO Dostavka (DateDostav, idBox) VALUES (@DateDostav, @IdBox)";
                        using (SqlCommand insertDostavkaCommand = new SqlCommand(insertDostavkaQuery, connection))
                        {
                            insertDostavkaCommand.Parameters.AddWithValue("@IdBox", oldBox.Id);
                            insertDostavkaCommand.Parameters.AddWithValue("@DateDostav", newBox.datePolych);
                            insertDostavkaCommand.ExecuteNonQuery();
                        }
                    }
                }
            }

            return true;
        }

        public static List<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Customer";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["id"]);
                            string fio = reader["fio"].ToString();

                            Customer customer = new Customer(id, fio);
                            customers.Add(customer);
                        }
                    }
                }
            }

            return customers;
        }

        public static void GenerateExcelReport(Customer selectedCustomer, DateTime start, DateTime end)
        {
            if (selectedCustomer == null) return;


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @" SELECT Box.*, City1.city as CityDostav, City2.city as CityOtprav FROM Box INNER JOIN City as City1 ON Box.SityDostav = City1.id INNER JOIN City as City2 ON Box.SityOtprav = City2.id WHERE Box.IdCustomer = @IdCustomer AND Box.dateOtprav >= @StartDate AND Box.datePolych <= @EndDate";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdCustomer", selectedCustomer.Id);
                    command.Parameters.AddWithValue("@StartDate", start);
                    command.Parameters.AddWithValue("@EndDate", end);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        using (ExcelPackage excel = new ExcelPackage())
                        {
                            var ws = excel.Workbook.Worksheets.Add("Packages");

                            ws.Cells[1, 1].Value = "ID";
                            ws.Cells[1, 2].Value = "Название посылки";
                            ws.Cells[1, 3].Value = "Вес посылки";
                            ws.Cells[1, 4].Value = "Объём посылки";
                            ws.Cells[1, 5].Value = "Город Получения";
                            ws.Cells[1, 6].Value = "Город доставки";
                            ws.Cells[1, 7].Value = "Дата отправки";
                            ws.Cells[1, 8].Value = "Дата получения";
                            ws.Cells[1, 9].Value = "Цена товара";
                            ws.Cells[1, 10].Value = "Цена посылки";

                            int rowStart = 2;
                            while (reader.Read())
                            {
                                ws.Cells[rowStart, 1].Value = reader["Id"];
                                ws.Cells[rowStart, 2].Value = reader["NameBox"];
                                ws.Cells[rowStart, 3].Value = reader["Weight"];
                                ws.Cells[rowStart, 4].Value = reader["Value"];
                                ws.Cells[rowStart, 5].Value = reader["CityDostav"];
                                ws.Cells[rowStart, 6].Value = reader["CityOtprav"];
                                ws.Cells[rowStart, 7].Value =Convert.ToString( reader["dateOtprav"]);
                                ws.Cells[rowStart, 8].Value = Convert.ToString(reader["datePolych"]);
                                ws.Cells[rowStart, 9].Value = reader["Price"];
                                ws.Cells[rowStart, 10].Value = reader["PriceBox"];
                                rowStart++;
                            }

                            string excelName = $"C:\\Users\\kupit\\OneDrive\\Desktop\\Документик.xlsx";

                            using (var stream = File.Create(excelName))
                            {
                                excel.SaveAs(stream);
                            }
                        }
                    }
                }
            }
        }
        public static void GenerateWordReport(Customer selectedCustomer, Box box,  Drivers driver)
        {
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create("C:\\Users\\kupit\\OneDrive\\Desktop\\Отчет.docx", WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body body = mainPart.Document.AppendChild(new Body());

                AppendParagraph(body, "ДОГОВОР ПЕРЕВОЗКИ ГРУЗА", true, JustificationValues.Center);
                AppendParagraph(body, $"{DateTime.Now:dd.MM.yyyy}.", false, JustificationValues.Right);
                AppendParagraph(body, "Наименование компании: Sub_Zero", false, JustificationValues.Right);
                AppendParagraph(body, "1. СТОРОНЫ ДОГОВОРА", true);
                AppendParagraph(body, $"1.1. Перевозчик: {driver.FullName}");
                AppendParagraph(body, $"1.2. Грузополучатель: {selectedCustomer.Fio}");
                AppendParagraph(body, $"1.3. Грузоотправитель: {selectedCustomer.Fio}");
                AppendParagraph(body, "2. ХАРАКТЕРИСТИКИ ГРУЗА", true);
                AppendParagraph(body, $"2.1. Вес: {box.Weight}");
                AppendParagraph(body, $"2.2. Объем: {box.Value}");
                AppendParagraph(body, "3. ПУНКТ ОТПРАВЛЕНИЯ И НАЗНАЧЕНИЯ", true);
                AppendParagraph(body, $"3.1. Пункт отправления: {GetCityIdByName(box.SityOtprav)}");
                AppendParagraph(body, $"3.2. Пункт назначения: {GetCityIdByName(box.SityDostav)}");

                AppendParagraph(body, "4. СТОИМОСТЬ ДОСТАВКИ И ПОРЯДОК РАСЧЕТОВ", true);
                AppendParagraph(body, $"4.1. Стоимость доставки: {box.PriceBox - box.Price}");

                AppendParagraph(body, "5. СРОКИ ПЕРЕВОЗКИ", true);
                AppendParagraph(body, $"5.1. Дата отправки: {box.dateOtprav}");
                AppendParagraph(body, $"5.2. Дата получения: {box.datePolych}");

                AppendParagraph(body, "6. ПРАВА И ОБЯЗАННОСТИ СТОРОН", true);
                AppendParagraph(body, "6.1. Перевозчик обязуется доставить груз в указанное место в указанные сроки. Грузоотправитель обязуется оплатить услуги перевозки в соответствии с договором. Перевозчик не несет ответственности за потерю или повреждение груза.");

                AppendParagraph(body, "7. ОТВЕТСТВЕННОСТЬ ЗА НАРУШЕНИЕ УСЛОВИЙ СДЕЛКИ", true);
                AppendParagraph(body, "7.1. В случае нарушения условий договора стороны несут ответственность в соответствии с действующим законодательством.");

                AppendParagraph(body, "8. ФОРС-МАЖОР", true);
                AppendParagraph(body, "8.1. В случае наступления обстоятельств непреодолимой силы, стороны освобождаются от ответственности за неисполнение обязательств по договору.");

                if (selectedCustomer.IsUrFace == 1)
                {
                    AppendParagraph(body, "9. ЮРИДИЧЕСКИЕ АДРЕСА И БАНКОВСКИЕ РЕКВИЗИТЫ СТОРОН", true);
                    AppendParagraph(body, $"9.1. Юридический адрес: {selectedCustomer.AdressCompany}");
                    AppendParagraph(body, $"9.2. Банковские реквизиты: Банк - {selectedCustomer.NameBank}, Код банка - {selectedCustomer.CodeBank}");
                }

                AppendParagraph(body, "10. ПОДПИСИ СТОРОН", true);
                AppendParagraph(body, "Грузоотправитель (Подпись)_____________");
                AppendParagraph(body, "Директор (Подпись)_____________");
                mainPart.Document.Save();
            }
        }
        private static void AppendParagraph(Body body, string text, bool bold = false, JustificationValues? align = null)
        {
            Paragraph para = new Paragraph();
             ParagraphProperties paraProps = new ParagraphProperties();
            Justification justification = new Justification() { Val = align };
            paraProps.Append(justification);
            para.Append(paraProps);

            DocumentFormat.OpenXml.Wordprocessing.Run run = new DocumentFormat.OpenXml.Wordprocessing.Run();
            if (bold)
            {
                DocumentFormat.OpenXml.Wordprocessing.RunProperties runProps = new  DocumentFormat.OpenXml.Wordprocessing.RunProperties();
                DocumentFormat.OpenXml.Wordprocessing.Bold boldElement = new DocumentFormat.OpenXml.Wordprocessing.Bold();
                runProps.Append(boldElement);
                run.Append(runProps);
            }

            run.Append(new DocumentFormat.OpenXml.Wordprocessing.Text(text));
            para.Append(run);
            body.Append(para);
        }

        private static void AppendParagraph(Body body, string text, bool isHeading = false)
        {
            Paragraph para = new Paragraph();
            ParagraphProperties paraProps = new ParagraphProperties();
            para.Append(paraProps);

            DocumentFormat.OpenXml.Wordprocessing.Run run = new DocumentFormat.OpenXml.Wordprocessing.Run();
            if (isHeading)
            {
                DocumentFormat.OpenXml.Wordprocessing.RunProperties runProps = new DocumentFormat.OpenXml.Wordprocessing.RunProperties();
                DocumentFormat.OpenXml.Wordprocessing.Bold boldElement = new DocumentFormat.OpenXml.Wordprocessing.Bold();
                runProps.Append(boldElement);
                run.Append(runProps);
            }

            run.Append(new DocumentFormat.OpenXml.Wordprocessing.Text(text));
            para.Append(run);
            body.Append(para);
        }
        private static void AppendParagraph(Body body, string text)
        {
            Paragraph para = new Paragraph();
            DocumentFormat.OpenXml.Wordprocessing.Run run = new DocumentFormat.OpenXml.Wordprocessing.Run();
            run.Append(new DocumentFormat.OpenXml.Wordprocessing.Text(text));
            para.Append(run);
            body.Append(para);
        }

        public static List<Box> GetOrdersForCustomer(int customerId)
        {
            List<Box> orders = new List<Box>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Box WHERE IdCustomer = @IdCustomer";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdCustomer", customerId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idCar = (int)reader["IdCar"];
                            Car car = DataBaseSQL.GetCar(idCar);
                            Customer customers = DataBaseSQL.GetCustomer((int)reader["IdCustomer"]);
                            Box box = new Box(
                                id: (int)reader["Id"],
                                nameBox: (string)reader["NameBox"],
                                weight: (double)reader["Weight"],
                                value: (double)reader["Value"],
                                sityDostav: (int)reader["SityDostav"],
                                sityOtprav: (int)reader["SityOtprav"],
                                car: car,
                                dateOtprav: (DateTime)reader["dateOtprav"],
                                datePolych: (DateTime)reader["datePolych"],
                                price: (double)reader["Price"],
                                priceBox: (double)reader["PriceBox"],
                                Customer: DataBaseSQL.GetCustomer((int)reader["IdCustomer"]
                                ));
                            
                            orders.Add(box);
                        }
                    }
                }
            }

            return orders;
        }
        public static List<Box> GetAllBoxes()
        {
            List<Box> boxes = new List<Box>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Box";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idCar = (int)reader["IdCar"];
                            Car car = DataBaseSQL.GetCar(idCar);
                            Customer customer = DataBaseSQL.GetCustomer((int)reader["IdCustomer"]);
                            Box box = new Box(
                                id: (int)reader["Id"],
                                nameBox: (string)reader["NameBox"],
                                weight: (double)reader["Weight"],
                                value: (double)reader["Value"],
                                sityDostav: (int)reader["SityDostav"],
                                sityOtprav: (int)reader["SityOtprav"],
                                car: car,
                                dateOtprav: (DateTime)reader["dateOtprav"],
                                datePolych: (DateTime)reader["datePolych"],
                                price: (double)reader["Price"],
                                priceBox: (double)reader["PriceBox"],
                                Customer: customer
                            );

                            boxes.Add(box);
                        }
                    }
                }
            }

            return boxes;
        }

        public static void UpdateDatePolych(Box boxId, DateTime datePolych)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Dostavka SET DatePolych = @DatePolych WHERE idBox = @idBox";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DatePolych", datePolych);
                    command.Parameters.AddWithValue("@idBox", boxId.Id);

                    command.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateDateProsrochAndDopDengi(Box boxId, DateTime dateProsroch, double tariff)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Dostavka SET DatePolych = DateDostav, DateProsroch = @DateProsroch, DopDengi = DATEDIFF(day, DateDostav, @DateProsroch) * @tariff WHERE idBox = @idBox";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DateProsroch", dateProsroch);
                    command.Parameters.AddWithValue("@tariff", tariff);
                    command.Parameters.AddWithValue("@idBox", boxId.Id);

                    command.ExecuteNonQuery();
                }
                if (boxId.PriceBox != null)
                {
                    query = "INSERT INTO box (priceBox) OUTPUT INSERTED.id VALUES ( @PriceBox + (SELECT DopDengi FROM Dostavka WHERE idBox = @idBox))";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PriceBox", boxId.PriceBox);
                        command.Parameters.AddWithValue("@idBox", boxId.Id);

                        boxId.Id = (int)command.ExecuteScalar();
                    }
                }
                else
                {
                    
                }
            }
        }




        //public static List<Box> GetOrdersForCustomer()
        //{
        //    List<Box> orders = new List<Box>();

        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        string query = "SELECT * FROM Box WHERE IdCustomer = @IdCustomer";

        //        using (SqlCommand command = new SqlCommand(query, connection))
        //        {
        //            command.Parameters.AddWithValue("@IdCustomer", customerId);

        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    int idCar = (int)reader["IdCar"];
        //                    Car car = DataBaseSQL.GetCar(idCar);
        //                    Customer customers = DataBaseSQL.GetCustomer((int)reader["IdCustomer"]);
        //                    Box box = new Box(
        //                        id: (int)reader["Id"],
        //                        nameBox: (string)reader["NameBox"],
        //                        weight: (double)reader["Weight"],
        //                        value: (double)reader["Value"],
        //                        sityDostav: (int)reader["SityDostav"],
        //                        sityOtprav: (int)reader["SityOtprav"],
        //                        car: car,
        //                        dateOtprav: (DateTime)reader["dateOtprav"],
        //                        datePolych: (DateTime)reader["datePolych"],
        //                        price: (double)reader["Price"],
        //                        priceBox: (double)reader["PriceBox"],
        //                        Customer: DataBaseSQL.GetCustomer((int)reader["IdCustomer"]
        //                        ));

        //                    orders.Add(box);
        //                }
        //            }
        //        }
        //    }

        //    return orders;
        //}

    }

}

