using System;
using System.Collections.Generic;
using VideoGameRental.Common.DTO;

namespace VideoGameRentalStore.Interfaces
{
    interface IVideoGameRentalViewModel
    {
        void Initialize();

        //Authetication
        bool ValidateStaff(string inputID, string password);
        bool ValidateUser(string inputID, string password);

        //Store Manager       
        StoreStaffDTO AddStaff(string inputStaffID, string inputStaffPassword, string inputStaffName, string inputStaffPhone, string inputStaffAddress, string inputStaffEmail);
        GamesDTO AddGames(string inputGamesID, string inputGamesName, string rentPrice);
        ICollection<StoreStaffDTO> ListStaff();
        ICollection<UserDTO> ListUser();
        ICollection<GamesDTO> GamesAvailable();
        ICollection<GamesDTO> RentedGames();
        ICollection<GamesDTO> RentedGames(string id);
        ICollection<GamesDTO> OverduedGames();
        double TotalEarned();

        //Store Staff
        UserDTO AddUser(string inputUserID, string inputUserPassword, string inputUserName, string inputUserPhone, string inputUserAddress, string inputUserEmail);
        ICollection<GamesDTO> SearchUser(string searchUserByGames);
        ICollection<GamesDTO> SearchGame(string searchGamesByUser);

        //User
        GamesDTO Rent(string id, string selectGameRent);
        GamesDTO Return(string selectGameRent);
    }
}
