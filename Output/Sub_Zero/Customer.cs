using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sub_Zero
{
    public class Customer
    {
            public int Id { get; set; }
            public string Fio { get; set; }
            public string NumberOfPhone { get; set; }
            public string Pochta { get; set; }
            public int IsUrFace { get; set; }
            public string Pasport { get; set; }
            public string Adress { get; set; }
            public string Ynp { get; set; }
            public string NameCompany { get; set; }
            public string AdressCompany { get; set; }
            public string RastSchet { get; set; }
            public string NameBank { get; set; }
            public string CodeBank { get; set; }
        public Customer(int id, string fio)
        {
            Id = id;
            Fio = fio;
        }
            public Customer(int id, string fio, string numberOfPhone, string pochta, int isUrFace, string pasport, string adress, string ynp, string nameCompany, string adressCompany, string rastSchet, string nameBank, string codeBank)
            {
                Id = id;
                Fio = fio;
                NumberOfPhone = numberOfPhone;
                Pochta = pochta;
                IsUrFace = isUrFace;
                Pasport = pasport;
                Adress = adress;
                Ynp = ynp;
                NameCompany = nameCompany;
                AdressCompany = adressCompany;
                RastSchet = rastSchet;
                NameBank = nameBank;
                CodeBank = codeBank;
            }

            public Customer(string fio, string numberOfPhone, string pochta, string adress, string pasport)
            {
                Fio = fio;
                NumberOfPhone = numberOfPhone;
                Pochta = pochta;
                 Adress=adress;
            Pasport = pasport;
            }
        public Customer( string fio, string numberOfPhone, string pochta, int isUrFace, string pasport, string adress, string ynp, string nameCompany, string adressCompany, string rastSchet, string nameBank, string codeBank)
        {
            Fio = fio;
            NumberOfPhone = numberOfPhone;
            Pochta = pochta;
            IsUrFace = isUrFace;
            Pasport = pasport;
            Adress = adress;
            Ynp = ynp;
            NameCompany = nameCompany;
            AdressCompany = adressCompany;
            RastSchet = rastSchet;
            NameBank = nameBank;
            CodeBank = codeBank;
        }
    }


    }

