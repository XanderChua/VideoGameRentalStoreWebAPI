using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPILibrary.Models
{
    class StoreStaff
    {
        public string staffID { get; set; }
        public string staffPassword { get; set; }
        public string staffName { get; set; }
        public string staffPhone { get; set; }
        public string staffAddress { get; set; }
        public string staffEmail { get; set; }

        internal StoreStaff(string id, string password, string name, string phone, string address, string email)
        {
            staffID = id;
            staffPassword = password;
            staffName = name;
            staffPhone = phone;
            staffAddress = address;
            staffEmail = email;
        }
    }
}
