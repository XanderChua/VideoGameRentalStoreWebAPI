using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [Table("StoreStaffTable")]
    public class StoreStaff
    {
        [Key]
        public string staffID { get; set; }
        public string staffPassword { get; set; }
        public string staffName { get; set; }
        public string staffPhone { get; set; }
        public string staffAddress { get; set; }
        public string staffEmail { get; set; }
        public StoreStaff()
        {

        }
        public StoreStaff(string id, string password, string name, string phone, string address, string email)
        {
            staffID = id;
            staffPassword = password;
            staffName = name;
            staffPhone = phone;
            staffAddress = address;
            staffEmail = email;
        }
        public void UpdatePassword(string password)
        {
            staffPassword = password;
        }
        public void UpdateName(string name)
        {
            staffName = name;
        }
        public void UpdatePhone(string phone)
        {
            staffPhone = phone;
        }
        public void UpdateAddress(string address)
        {
            staffAddress = address;
        }
        public void UpdateEmail(string email)
        {
            staffEmail = email;
        }
    }
}