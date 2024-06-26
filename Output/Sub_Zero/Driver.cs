using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sub_Zero
{

    public class Drivers
    {
        public int id;
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string DrivingLicenseNumber { get; set; }
        private DateTime dateOfBirth { get; set; }

        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set
            {
                int age = DateTime.Now.Year - value.Year;
                if (value > DateTime.Now.AddYears(-age)) age--;

                if (age >= 18)
                    dateOfBirth = value;
                else
                    throw new ArgumentException("Age must be 18+");
            }
        }
        public Drivers( string fullName, string phoneNumber, string drivingLicenseNumber, DateTime dateOfBirth)
        {
            FullName = fullName;
            PhoneNumber = phoneNumber;
            DrivingLicenseNumber = drivingLicenseNumber;
            DateOfBirth = dateOfBirth;
        }
        public Drivers(int id,string fullName, string phoneNumber, string drivingLicenseNumber, DateTime dateOfBirth)
        {
            this.id = id;
            FullName = fullName;
            PhoneNumber = phoneNumber;
            DrivingLicenseNumber = drivingLicenseNumber;
            DateOfBirth = dateOfBirth;
        }
    }

}
