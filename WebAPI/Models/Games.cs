using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [Table("GamesTable")]
    public class Games
    {
        [Key]
        public string gamesID { get; set; }
        public string gamesName { get; set; }
        public string gameRentPrice { get; set; }
        public string rentedStatus { get; set; }
        public string rentedBy { get; set; }
        public string rentedDate { get; set; }
        public string returnByDate { get; set; }
        public Games()
        {

        }
        public Games(string id, string name, string price, string status, string rent, string rd, string rbd)
        {
            gamesID = id;
            gamesName = name;
            gameRentPrice = price;
            rentedStatus = status;
            rentedBy = rent;
            rentedDate = rd;
            returnByDate = rbd;
        }
        public void UpdateRentStatus(string status)
        {
            rentedStatus = status;
        }
        public void UpdateRentedBy(string rentedby)
        {
            rentedBy = rentedby;
        }
        public void UpdateRentedDate(string rentdate)
        {
            rentedDate = rentdate;
        }
        public void UpdateReturnByDate(string returndate)
        {
            returnByDate = returndate;
        }
    }
}