using System;
using System.Collections.Generic;
using System.IO;
using VideoGameRental.Common.DTO;
using VideoGameRentalStore.Interfaces;
using VideoGameRentalStore.ViewModel;

namespace VideoGameRentalStore
{
    public class Program
    {
        private static IConsoleIO ConsoleIO;
        private static IVideoGameRentalViewModel vm;
        static void Main(string[] args)
        {
            ConsoleIO = new ConsoleIO();
            vm = new VideoGameRentalViewModel();
            vm.Initialize();
            DateTime dt = DateTime.Now;
            bool loop = true;
            while (loop)
            {               
                Console.WriteLine("Today's date: " + dt.ToString("dd/MM/yyyy"));
                Console.WriteLine("--Game Rental Store--");
                Console.WriteLine("Welcome! Please input an option.");
                Console.WriteLine("1. Log In");
                Console.WriteLine("2. Set Date");
                Console.WriteLine("3. Exit");
                try
                {
                    int input = Int32.Parse(Console.ReadLine());
                    if (input == 1)
                    {
                        (string inputID, string password) =LoginUser();
                        if (inputID == "000" && password == "super")
                        {
                            PerformOperationsvm(dt);                          
                        }
                        else if (vm.ValidateStaff(inputID, password))
                        {
                            Console.WriteLine("Staff login success!");
                            PerformOperationsStoreStaff(inputID);                           
                        }
                        else if (vm.ValidateUser(inputID, password))
                        {
                            Console.WriteLine("User login success!");
                            PerformOperationsUser(inputID, dt);
                        }
                        else
                        {
                            Console.WriteLine("User ID or password is incorrect");
                        }
                    }
                    else if (input == 2)
                    {
                        Console.WriteLine("Enter date in dd/mm/yyyy format:");
                        var date = Console.ReadLine();
                        var isValidDate = DateTime.TryParse(date, out dt);
                        if (isValidDate)
                        {
                            Console.WriteLine(dt.ToString("dd/MM/yyyy") + " has been set to today's date.");
                        }
                        else
                        {
                            Console.WriteLine(date + " is not a valid date string.");
                        }
                    }
                    else if (input == 3)
                    {
                        loop = false;
                    }
                    else
                    {
                        Console.WriteLine("Please enter the correct option!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception caught! " + ex.Message);
                }
            }
        }
        private static (string,string) LoginUser()
        {
            ConsoleIO.WriteLine("Enter ID to login:");
            string inputID = ConsoleIO.ReadLine();
            ConsoleIO.WriteLine("Enter Password to login:");
            string password = ConsoleIO.ReadLine();
            return (inputID,password);
        }

        private static void PerformOperationsvm(DateTime dateTime)
        {
            bool loop = true;
            while (loop)
            {
                Console.WriteLine("--Manage Store Page--");
                Console.WriteLine("1. Add Store Staff");
                Console.WriteLine("2. Add Games");
                Console.WriteLine("3. List Store Staffs");
                Console.WriteLine("4. List Users");
                Console.WriteLine("5. Games Available");
                Console.WriteLine("6. Games Rented");
                Console.WriteLine("7. Check Overdued Games");
                Console.WriteLine("8. Check Total Earned");
                Console.WriteLine("9. Exit");
                try
                {
                    int inputvmOption = Int32.Parse(Console.ReadLine());
                    if (inputvmOption == 1)
                    {
                        Console.WriteLine("Create Staff ID:");
                        string inputStaffID = Console.ReadLine();
                        Console.WriteLine("Create Staff Password:");
                        string inputStaffPassword = Console.ReadLine();
                        Console.WriteLine("Create Staff Name:");
                        string inputStaffName = Console.ReadLine();
                        Console.WriteLine("Create Staff Phone Number:");
                        string inputStaffPhone = Console.ReadLine();
                        Console.WriteLine("Create Staff Address:");
                        string inputStaffAddress = Console.ReadLine();
                        Console.WriteLine("Create Staff Email:");
                        string inputStaffEmail = Console.ReadLine();
                        vm.AddStaff(inputStaffID, inputStaffPassword, inputStaffName, inputStaffPhone, inputStaffAddress, inputStaffEmail);
                    }
                    else if (inputvmOption == 2)
                    {
                        Console.WriteLine("Create Games ID:");
                        string inputGamesID = Console.ReadLine();
                        Console.WriteLine("Create Games Name:");
                        string inputGamesName = Console.ReadLine();
                        Console.WriteLine("Enter Price:");
                        string rentPrice = Console.ReadLine();
                        vm.AddGames(inputGamesID, inputGamesName, rentPrice);
                    }
                    else if (inputvmOption == 3)
                    {
                        Console.WriteLine("Store Staffs:");
                        foreach (var staff in vm.ListStaff())
                        {
                            Console.WriteLine("Staff ID: " + staff.staffID + " Staff Name: " + staff.staffName +
                                "\nStaff Phone: " + staff.staffPhone + " Staff Address: " + staff.staffAddress +
                                "\nStaff Email: " + staff.staffEmail);
                        }
                        Console.WriteLine("\n");
                    }
                    else if (inputvmOption == 4)
                    {
                        Console.WriteLine("Users:");
                        foreach (var userList in vm.ListUser())
                        {
                            Console.WriteLine("User ID: " + userList.userID + " User Name: " + userList.userName +
                                "\nUser Phone: " + userList.userPhone + " User Address: " + userList.userAddress +
                                "\nUser Email: " + userList.userEmail);
                        }
                        Console.WriteLine("\n");
                    }
                    else if (inputvmOption == 5)
                    {
                        Console.WriteLine("Games Available:");
                        foreach (var gameNotRented in vm.GamesAvailable())
                        {
                            Console.WriteLine("Game ID: " + gameNotRented.gamesID + " Game Name: " + gameNotRented.gamesName + 
                                "\nGame Rent Price: " + gameNotRented.gameRentPrice);
                        }
                        Console.WriteLine("\n");
                    }
                    else if (inputvmOption == 6)
                    {
                        Console.WriteLine("Games Rented:");
                        foreach (var rentedgame in vm.RentedGames())
                        {
                            Console.WriteLine("Game ID: " + rentedgame.gamesID + "\nGame Name: " + rentedgame.gamesName + 
                                "\nRented By: " + rentedgame.rentedBy);
                        }

                        vm.RentedGames();
                        Console.WriteLine("\n");
                    }
                    else if (inputvmOption == 7)
                    {
                        Console.WriteLine("Overdued Games:");
                        foreach (var gamesDue in vm.OverduedGames(dateTime))
                        {
                            Console.WriteLine("Game ID: " + gamesDue.gamesID + " Game Name: " + gamesDue.gamesName +
                                "\nRented By: " + gamesDue.rentedBy + " Return By: " + gamesDue.returnByDate);                          
                        }
                        Console.WriteLine("\n");
                    }
                    else if (inputvmOption == 8)
                    {
                        Console.WriteLine(vm.TotalEarned());
                        Console.WriteLine("\n");
                    }
                    else if (inputvmOption == 9)
                    {
                        loop = false;
                    }
                    else
                    {
                        Console.WriteLine("Please enter the correct option!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception caught! " + ex.Message);
                }
            }
        }
        private static void PerformOperationsStoreStaff(string id)
        {
            bool loop = true;
            ICollection<StoreStaffDTO> staffCollection = vm.ListStaff();
            while (loop)
            {
                foreach (var name in staffCollection)
                {
                    if (name.staffID == id)
                    {
                        Console.WriteLine("Welcome " + name.staffID + ", " + name.staffName);
                    }
                }
                Console.WriteLine("--Store Staff Page--");
                Console.WriteLine("1. Add User");
                Console.WriteLine("2. Games Available");
                Console.WriteLine("3. Search User by Games Rented");
                Console.WriteLine("4. Search Games by Rented User");
                Console.WriteLine("5. Exit");
                try
                {
                    int inputStoreStaffOption = Int32.Parse(Console.ReadLine());
                    if (inputStoreStaffOption == 1)
                    {
                        Console.WriteLine("Create User ID:");
                        string inputUserID = Console.ReadLine();
                        Console.WriteLine("Create User Password:");
                        string inputUserPassword = Console.ReadLine();
                        Console.WriteLine("Create User Name:");
                        string inputUserName = Console.ReadLine();
                        Console.WriteLine("Create User Phone Number:");
                        string inputUserPhone = Console.ReadLine();
                        Console.WriteLine("Create User Address:");
                        string inputUserEmail = Console.ReadLine();
                        Console.WriteLine("Create User Email:");
                        string inputUserAddress = Console.ReadLine();
                        vm.AddUser(inputUserID, inputUserPassword, inputUserName, inputUserPhone, inputUserAddress, inputUserEmail);
                    }
                    else if (inputStoreStaffOption == 2)
                    {
                        vm.GamesAvailable();
                    }
                    else if (inputStoreStaffOption == 3)
                    {
                        Console.WriteLine("Enter Game ID:");
                        string searchUserByGames = Console.ReadLine();
                        foreach (var game in vm.SearchUser(searchUserByGames))
                        {
                            Console.WriteLine("Game ID: " + game.gamesID + "\nGame Name: " + game.gamesName);                           
                        }                     
                    }
                    else if (inputStoreStaffOption == 4)
                    {
                        Console.WriteLine("Enter User ID:");
                        string searchGamesByUser = Console.ReadLine();
                        foreach (var game in vm.SearchGame(searchGamesByUser))
                        {
                            if (game.rentedStatus == "Not Rented")
                            {
                                Console.WriteLine("Game ID: " + game.gamesID + "\nGame Name: " + game.gamesName);
                            }
                        }                    
                    }
                    else if (inputStoreStaffOption == 5)
                    {
                        loop = false;
                    }
                    else
                    {
                        Console.WriteLine("Please enter the correct option!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception caught! " + ex.Message);
                }
            }
        }
        private static void PerformOperationsUser(string id, DateTime dateTime)
        {
            bool loop = true;
            ICollection<UserDTO> userCollection = vm.ListUser();
            while (loop)
            {
                foreach(var name in userCollection)
                {
                    if(name.userID == id)
                    {
                        Console.WriteLine("Welcome " + name.userID + ", " + name.userName);
                    }
                }               
                Console.WriteLine("--User Page--");
                Console.WriteLine("1. Rent Game");
                Console.WriteLine("2. Return Game");
                Console.WriteLine("3. List Rented Games");
                Console.WriteLine("4. Exit");
                try
                {
                    int inputStoreUser = Int32.Parse(Console.ReadLine());
                    if (inputStoreUser == 1)
                    {
                        Console.WriteLine("Enter game ID to rent:");
                        ICollection<GamesDTO> gameCollection= vm.GamesAvailable();
                        foreach (var game in gameCollection)
                        {
                            if (game.rentedStatus == "Not Rented")
                            {
                                Console.WriteLine("Game ID: " + game.gamesID + "\nGame Name: " + game.gamesName + " Price: " + game.gameRentPrice);
                            }
                        }
                        string selectGameRent = Console.ReadLine();
                        vm.Rent(id, dateTime, selectGameRent);
                    }
                    else if (inputStoreUser == 2)
                    {
                        Console.WriteLine("Enter game ID to return:");
                        ICollection<GamesDTO> gameCollection = vm.RentedGames();
                        foreach (var rentedgame in gameCollection)
                        {
                            if (rentedgame.rentedStatus == "Rented" && rentedgame.rentedBy == id)
                            {
                                Console.WriteLine("Game ID: " + rentedgame.gamesID + "\nGame Name: " + rentedgame.gamesName);
                            }
                        }
                        string selectGameReturn = Console.ReadLine();
                        vm.Return(id, dateTime, selectGameReturn);
                    }
                    else if (inputStoreUser == 3)
                    {
                        foreach (var rentedgame in vm.ListRentedGames(id))
                        {
                            if (rentedgame.rentedBy == id)
                            {
                                Console.WriteLine("Game ID: " + rentedgame.gamesID + "\nGame Name: " + rentedgame.gamesName + " Return By: " + rentedgame.returnByDate);
                            }
                        }                      
                        Console.WriteLine("\n");
                    }
                    else if (inputStoreUser == 4)
                    {
                        loop = false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception caught! " + ex.Message);
                }
            }
        }

    }
}