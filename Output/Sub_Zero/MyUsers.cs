using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sub_Zero
{
    public class MyUsers
    {
        public int id;
        public string name;
        public string password;
        public string position;
        public MyUsers(string name, string password, string position, int id)
        {
            this.name = name;
            this.password = password;
            this.position = position;
            this.id = id;
        }
    }
}
