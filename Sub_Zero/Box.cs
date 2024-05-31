using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sub_Zero
{
    public class Box
    {
        public int Id { get; set; }
        public string NameBox { get; set; }
        public double Weight { get; set; }
        public double Value { get; set; }
        public int SityDostav { get; set; }
        public int SityOtprav { get; set; }
        public Car Car { get; set; }
        public DateTime dateOtprav { get; set; }
        public DateTime datePolych { get; set; }
        public double Price { get; set; }
        public double PriceBox { get; set; }
        public Customer Customer { get; set; }
        public Box() { }
       public string idCar { get; set; }
        public string idCustomer { get; set; }
        public string NameSityDostav { get; set; }
        public string NameSityOtprav { get; set; }
        public Box(int id, string nameBox, double weight, double value, int sityDostav, int sityOtprav, Car car, DateTime dateOtprav, DateTime datePolych, double price, double priceBox, Customer Customer)
        {
            Id = id;
            NameBox = nameBox;
            Weight = weight;
            Value = value;
            SityDostav = sityDostav;
            SityOtprav = sityOtprav;
           this.Car = car;
           this.dateOtprav=dateOtprav;
            this.datePolych = datePolych;
            Price = price;
            PriceBox = priceBox;
            this.Customer = Customer;
          idCar= this.Car.MarkOfCar;
            idCustomer = this.Customer.Fio;
        }
        public Box(string nameBox, double weight, double value, int sityDostav, int sityOtprav, Car Car, DateTime dateOtprav, DateTime datePolych, double price, Customer Customer)
        {
            NameBox = nameBox;
            Weight = weight;
            Value = value;
            SityDostav = sityDostav;
            SityOtprav = sityOtprav;
           this.Car = Car;
            this.dateOtprav = dateOtprav;
            this.datePolych = datePolych;
            Price = price;
            this.Customer = Customer;
            idCar = Car.MarkOfCar;
            idCustomer = Customer.Fio;
        }
        public static double CalculateDeliveryCost(Box box, double baseCost=29.12, double ratePerKg=14.24, double ratePerKm=17.5, double distance=7.5)
        {
            double deliveryCost = baseCost + box.Weight * ratePerKg + distance * ratePerKm+box.Price;
            return deliveryCost;
        }

    }

}
