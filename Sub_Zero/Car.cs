using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sub_Zero
{
    public class Car
    {
        public int id;
        public string MarkOfCar { get; set; }
        public int Driver { get; set; }
        public string name { get; set; }
        public double MaxDostupVelue { get; set; }
        public double MaxDostupWeight { get; set; }
        public string NumberCar { get; set; }
        public Car() { }
        public Car(string markOfCar, string driver, double maxDostupVelue, double maxDostupWeight, string numberCar)
        {
            MarkOfCar = markOfCar;
            name = driver;
            MaxDostupVelue = maxDostupVelue;
            MaxDostupWeight = maxDostupWeight;
            NumberCar = numberCar;
        }
        public Car(int id,string markOfCar, int driver, double maxDostupVelue, double maxDostupWeight, string numberCar)
        {
            MarkOfCar = markOfCar;
            Driver = driver;
            MaxDostupVelue = maxDostupVelue;
            MaxDostupWeight = maxDostupWeight;
            NumberCar = numberCar;
            this.id = id;
        }
    }
}
