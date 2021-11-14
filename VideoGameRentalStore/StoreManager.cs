using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace VideoGameRentalStore
{
    public class StoreManager
    {
        //staff
        private string readStaff;
        private Dictionary<string, StoreStaff> _staffDict;
        public Dictionary<string, StoreStaff> StaffDictObj
        {
            get
            {
                if (_staffDict == null)
                {
                    _staffDict = new Dictionary<string, StoreStaff>();
                }
                return _staffDict;
            }
            set
            {
                _staffDict = value;
            }
        }

        //user
        private string readUser;
        private Dictionary<string, User> _userDict;
        public Dictionary<string, User> UserDictObj
        {
            get
            {
                if (_userDict == null)
                {
                    _userDict = new Dictionary<string, User>();
                }
                return _userDict;
            }
            set
            {
                _userDict = value;
            }
        }

        //games
        private string readGames;
        private Dictionary<string, Games> _gamesDict;
        public Dictionary<string, Games> GamesDictObj
        {
            get
            {
                if (_gamesDict == null)
                {
                    _gamesDict = new Dictionary<string, Games>();                   
                }
                return _gamesDict;
            }
            set
            {
                _gamesDict = value;

            }
        }

        //earned
        private string readEarned;
        private List<double> _earnedList;
        public List<double> EarnedListObj
        {
            get
            {
                if (_earnedList == null)
                {
                    _earnedList = new List<double>();
                }
                return _earnedList;
            }
            set
            {
                _earnedList = value;
            }
        }

        //main functions
        public void Initialize()
        {
            readStaff = File.ReadAllText("StoreStaff.json");
            readUser = File.ReadAllText("StoreUser.json");
            readGames = File.ReadAllText("StoreGames.json");
            readEarned = File.ReadAllText("StoreEarned.json");
            StaffDictObj = JsonConvert.DeserializeObject<Dictionary<string, StoreStaff>>(readStaff);
            UserDictObj = JsonConvert.DeserializeObject<Dictionary<string, User>>(readUser);
            GamesDictObj = JsonConvert.DeserializeObject<Dictionary<string, Games>>(readGames);
            EarnedListObj = JsonConvert.DeserializeObject<List<double>>(readEarned);
        }
        public bool ValidateStaff(string userId, string password)
        {
            return StaffDictObj.Any(entry => entry.Key == userId && entry.Value.staffPassword == password);
        }
        public bool ValidateUser(string userId, string password)
        {
            return UserDictObj.Any(entry => entry.Key == userId && entry.Value.userPassword == password);
        }

        //manager functions
        public void AddStoreStaff(string inputStaffID, string inputStaffPassword, string inputStaffName, string inputStaffPhone, string inputStaffAddress, string inputStaffEmail)
        {
            try
            {
                if (inputStaffPhone.Any(char.IsDigit))
                {
                    StaffDictObj.Add(inputStaffID, new StoreStaff(inputStaffID, inputStaffPassword, inputStaffName, inputStaffPhone, inputStaffAddress, inputStaffEmail));
                    UpdateStaffs();
                    Console.WriteLine(inputStaffID + ", " + inputStaffName + " added!");
                }
                else
                {
                    Console.WriteLine("Please enter a valid phone number.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception handled. " + ex.Message);
            }
        }
        public void AddGames(string inputGamesID, string inputGamesName, string rentPrice)
        {
            try
            {
                if (rentPrice.Any(char.IsDigit))
                {
                    GamesDictObj.Add(inputGamesID, new Games(inputGamesID, inputGamesName, rentPrice, "Not Rented", null, null, null));
                    UpdateGames();
                    Console.WriteLine(inputGamesID + ", " + inputGamesName + " added!");
                }
                else
                {
                    Console.WriteLine("Please enter a valid price.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception handled. " + ex.Message);
            }
        }
        public void ListStaff()
        {
            Console.WriteLine("Store Staffs:");
            foreach (var staff in StaffDictObj)
            {
                Console.WriteLine("Staff ID: " + staff.Key + " Staff Name: " + staff.Value.staffName +
                    "\nStaff Phone: " + staff.Value.staffPhone + " Staff Address: " + staff.Value.staffAddress +
                    "\nStaff Email: " + staff.Value.staffEmail);
            }
        }
        public void ListUser()
        {
            Console.WriteLine("Users:");
            foreach (var userList in UserDictObj)
            {
                Console.WriteLine("User ID: " + userList.Key + " User Name: " + userList.Value.userName +
                    "\nUser Phone: " + userList.Value.userPhone + " User Address: " + userList.Value.userAddress +
                    "\nUser Email: " + userList.Value.userEmail);
            }
        }
        public void GamesAvailable()
        {
            Console.WriteLine("Games Available:");
            foreach (var gameNotRented in GamesDictObj)
            {
                if (gameNotRented.Value.rentedStatus == "Not Rented")
                {
                    Console.WriteLine("Game ID: " + gameNotRented.Key + "\nGame Name: " + gameNotRented.Value.gamesName);
                }
            }
        }
        public void GamesRented()
        {
            Console.WriteLine("Games Rented:");
            foreach (var rentedgame in GamesDictObj)
            {
                if (rentedgame.Value.rentedStatus == "Rented")
                {
                    Console.WriteLine("Game ID: " + rentedgame.Key + "\nGame Name: " + rentedgame.Value.gamesName + "\nRented By: " + rentedgame.Value.rentedBy);
                }
            }
        }
        public void OverduedGames(DateTime dateTime, Games games)
        {
            GamesDictObj = JsonConvert.DeserializeObject<Dictionary<string, Games>>(readGames);
            Console.WriteLine("Overdued Games:");
            foreach (var gamesDue in GamesDictObj)
            {
                DateTime convertedReturnDate = DateTime.Parse(gamesDue.Value.returnByDate + " 12:00:00 AM");
                double daysLate = ((dateTime - convertedReturnDate).TotalDays);
                if (gamesDue.Value.returnByDate != "" && daysLate > 0)
                {
                    Console.WriteLine("Game ID: " + gamesDue.Key + " Game Name: " + gamesDue.Value.gamesName +
                        "\nRented By: " + gamesDue.Value.rentedBy + " Return By: " + gamesDue.Value.returnByDate);
                }
            }
        }
        public void TotalEarned()
        {
            double earnedProfit = EarnedListObj.Sum();
            Console.WriteLine("Current amount earned in total: $" + earnedProfit);
        }

        //staff functions
        public void AddUser(User user, string inputUserID, string inputUserPassword, string inputUserName, string inputUserPhone, string inputUserAddress, string inputUserEmail)
        {
            try
            {
                if (inputUserPhone.Any(char.IsDigit))
                {
                    UserDictObj.Add(inputUserID, new User(inputUserID, inputUserPassword, inputUserName, inputUserPhone, inputUserAddress, inputUserEmail));
                    UpdateUser();
                    Console.WriteLine(inputUserID + ", " + inputUserName + " added!");
                }
                else
                {
                    Console.WriteLine("Please enter a valid phone number.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception handled. " + ex.Message);
            }

        }
        public void StaffGamesAvailable(Games games)
        {
            foreach (var game in GamesDictObj)
            {
                if (game.Value.rentedStatus == "Not Rented")
                {
                    Console.WriteLine("Game ID: " + game.Key + "\nGame Name: " + game.Value.gamesName);
                }
            }
        }
        public void SearchUserByGames(Games games, string searchUserByGames)
        {
            Console.WriteLine("User who rented " + searchUserByGames + ": ");
            foreach (var user in GamesDictObj)
            {
                if (user.Key == searchUserByGames)
                {
                    Console.WriteLine(user.Value.rentedBy);
                }
            }
        }
        public void SearchGamesByUser(Games games, string searchGamesByUser)
        {
            Console.WriteLine("Games rented by " + searchGamesByUser + ": ");
            foreach (var game in GamesDictObj)
            {
                if (game.Value.rentedBy == searchGamesByUser)
                {
                    Console.WriteLine(game.Key);
                }
            }
        }
        public void UpdateStaffs()
        {
            //FileStream fsStaff = new FileStream("StaffDetails.txt", FileMode.OpenOrCreate, FileAccess.Write);
            //StreamWriter swStaff = new StreamWriter(fsStaff);
            //foreach (var staff in StaffDictObj)
            //{
            //    swStaff.WriteLine(staff.Key + "," + staff.Value.staffPassword +
            //        "," + staff.Value.staffName + "," + staff.Value.staffPhone +
            //        "," + staff.Value.staffAddress + "," + staff.Value.staffEmail);
            //}
            //swStaff.Close();
            //fsStaff.Close();

            string storeStaffJson = JsonConvert.SerializeObject(StaffDictObj);
            File.WriteAllText("StoreStaff.json", storeStaffJson);
        }

        //user functions
        public void RentGames(string id, DateTime dateTime, string selectGameRent)
        {
            GamesDictObj[selectGameRent].rentedStatus = "Rented";
            GamesDictObj[selectGameRent].rentedBy = id;
            GamesDictObj[selectGameRent].rentedDate = dateTime.ToString("dd/MM/yyyy");
            GamesDictObj[selectGameRent].returnByDate = dateTime.AddDays(6).ToString("dd/MM/yyyy");
            UpdateGames();
            Console.WriteLine("Game ID: " + selectGameRent + " successfully rented!");
        }
        public void ReturnGames(string id, DateTime dateTime, string selectGameReturn)
        {
            GamesDictObj[selectGameReturn].rentedStatus = "Not Rented";
            GamesDictObj[selectGameReturn].rentedBy = "";
            DateTime convertedReturnDate = Convert.ToDateTime(GamesDictObj[selectGameReturn].returnByDate);
            double daysLate = ((dateTime - convertedReturnDate).TotalDays);
            if (daysLate > 0)
            {
                double gamePrice = Double.Parse(GamesDictObj[selectGameReturn].gameRentPrice);
                double fine = daysLate * (gamePrice * 0.5);
                EarnedListObj.Add(fine + gamePrice);
                Console.WriteLine("$" + (fine + gamePrice) + " paid.");
                Console.WriteLine("You paid an extra $" + fine + " fine for returning " + daysLate + " days late.");
            }
            else
            {
                double gamePrice = Double.Parse(GamesDictObj[selectGameReturn].gameRentPrice);
                EarnedListObj.Add(gamePrice);
                Console.WriteLine("$" + (gamePrice) + " paid.");
            }
            GamesDictObj[selectGameReturn].rentedDate = "";
            GamesDictObj[selectGameReturn].returnByDate = "";
            UpdateEarned();
            UpdateGames();
            Console.WriteLine("Game ID: " + selectGameReturn + " successfully returned!");
        }
        public void ListRentedGames(string id)
        {
            foreach (var rentedgame in GamesDictObj)
            {
                if (rentedgame.Value.rentedBy == id)
                {
                    Console.WriteLine("Game ID: " + rentedgame.Key + "\nGame Name: " + rentedgame.Value.gamesName + " Return By: " + rentedgame.Value.returnByDate);
                }
            }
        }
        public void UpdateUser()
        {
            //FileStream fsUser = new FileStream("UserDetails.txt", FileMode.OpenOrCreate, FileAccess.Write);
            //StreamWriter swUser = new StreamWriter(fsUser);
            //foreach (var user in UserDictObj)
            //{
            //    swUser.WriteLine(user.Key + "," + user.Value.userPassword +
            //        "," + user.Value.userName + "," + user.Value.userPhone +
            //        "," + user.Value.userAddress + "," + user.Value.userEmail);
            //}
            //swUser.Close();
            //fsUser.Close();

            string storeUserJson = JsonConvert.SerializeObject(UserDictObj);
            File.WriteAllText("StoreUser.json", storeUserJson);
        }

        //games function
        public void UpdateGames()
        {
            //FileStream fsGames = new FileStream("GamesDetails.txt", FileMode.Truncate, FileAccess.Write);
            //StreamWriter swGames = new StreamWriter(fsGames);
            //foreach (var games in GamesDictObj)
            //{
            //    swGames.WriteLine(games.Key + "," + games.Value.gamesName +
            //        "," + games.Value.gameRentPrice + "," + games.Value.rentedStatus +
            //        "," + games.Value.rentedBy + "," + games.Value.rentedDate +
            //        "," + games.Value.returnByDate);
            //}
            //swGames.Flush();
            //swGames.Close();
            //fsGames.Close();
            string storeGamesJson = JsonConvert.SerializeObject(GamesDictObj);
            File.WriteAllText("StoreGames.json", storeGamesJson);
        }

        //earned function
        public void UpdateEarned()
        {
            //FileStream fsEarned = new FileStream("EarnedTotal.txt", FileMode.OpenOrCreate, FileAccess.Write);
            //StreamWriter swEarned = new StreamWriter(fsEarned);
            //foreach (var earned in EarnedListObj)
            //{
            //    swEarned.WriteLine(earned);
            //}
            //swEarned.Close();
            //fsEarned.Close();
            string storeEarnedJson = JsonConvert.SerializeObject(EarnedListObj);
            File.WriteAllText("StoreEarned.json", storeEarnedJson);
        }
    }
}