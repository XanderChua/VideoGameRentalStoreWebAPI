using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.Http;
using VideoGameRental.Common.DTO;
using WebAPI.EntityFramework;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/StoreManager")]
    public class StoreManagerController : ApiController
    {
        VideoGameRentalStoreContext videoGameRentalStoreContext = new VideoGameRentalStoreContext();

        [HttpPost]
        [Route("AddStaff")]
        public StoreStaffDTO AddStaff(StoreStaffDTO storeStaff)
        {
            videoGameRentalStoreContext.StoreStaffs.Add(MapToStaffModel(storeStaff));
            videoGameRentalStoreContext.SaveChanges();
            return storeStaff;
        }

        [HttpPost]
        [Route("AddGames")]
        public GamesDTO AddGames(GamesDTO storeGames)
        {
            videoGameRentalStoreContext.Games.Add(MapToGamesModel(storeGames));
            videoGameRentalStoreContext.SaveChanges();
            return storeGames;
        }

        [HttpGet]
        [Route("ListStaff")]
        public IHttpActionResult GetAllStaff()
        {
            ICollection<StoreStaffDTO> dtoList = new Collection<StoreStaffDTO>();
            foreach (StoreStaff staff in videoGameRentalStoreContext.StoreStaffs)
            {
                dtoList.Add(MapToStaffDTO(staff));
            }              
            return Ok(dtoList);
        }

        [HttpGet]
        [Route("ListUser")]
        public IHttpActionResult GetAllUser()
        {
            ICollection<UserDTO> dtoList = new Collection<UserDTO>();
            foreach (User user in videoGameRentalStoreContext.Users)
            {
                dtoList.Add(MapToUserDTO(user));
            }
            return Ok(dtoList);
        }

        [HttpGet]
        [Route("AvailableGames")]
        public IHttpActionResult GetAvailableGames()
        {
            ICollection<GamesDTO> dtoList = new Collection<GamesDTO>();
            foreach (Games games in videoGameRentalStoreContext.Games)
            {
                if(games.rentedStatus == "Not Rented")
                    dtoList.Add(MapToGamesDTO(games));
            }
            return Ok(dtoList);
        }

        [HttpGet]
        [Route("GamesRented")]
        public IHttpActionResult GetRentedGames()
        {
            ICollection<GamesDTO> dtoList = new Collection<GamesDTO>();
            foreach (Games games in videoGameRentalStoreContext.Games)
            {
                if (games.rentedStatus == "Rented")
                    dtoList.Add(MapToGamesDTO(games));
            }
            return Ok(dtoList);
        }

        [HttpGet]
        [Route("OverduedGames")]
        public IHttpActionResult GetOverduedGames(Games storeGames, DateTime dateTime)
        {
            ICollection<GamesDTO> dtoList = new Collection<GamesDTO>();
            DateTime convertedReturnDate = DateTime.Parse(storeGames.returnByDate + " 12:00:00 AM");
            double daysLate = ((dateTime - convertedReturnDate).TotalDays);
            foreach (Games games in videoGameRentalStoreContext.Games)
            {
                if (games.returnByDate!="" && daysLate>0 )
                    dtoList.Add(MapToGamesDTO(games));
            }
            return Ok(dtoList);
        }

        [HttpGet]
        [Route("TotalEarned")]
        public IHttpActionResult GetTotalEarned()
        {
            double earnedProfit = 0;
            return Ok(earnedProfit);
        }

        private StoreStaffDTO MapToStaffDTO(StoreStaff storeStaff)
        {
            return new StoreStaffDTO(storeStaff.staffID, storeStaff.staffPassword, storeStaff.staffName, storeStaff.staffPhone, storeStaff.staffAddress, storeStaff.staffEmail);
        }
        private StoreStaff MapToStaffModel(StoreStaffDTO storeStaffDto)
        {
            return new StoreStaff(storeStaffDto.staffID, storeStaffDto.staffPassword, storeStaffDto.staffName, storeStaffDto.staffPhone, storeStaffDto.staffAddress, storeStaffDto.staffEmail);
        }
        private GamesDTO MapToGamesDTO(Games storeGames)
        {
            return new GamesDTO(storeGames.gamesID, storeGames.gamesName, storeGames.gameRentPrice, storeGames.rentedStatus, storeGames.rentedBy, storeGames.rentedDate, storeGames.returnByDate);
        }
        private Games MapToGamesModel(GamesDTO storeGamesDto)
        {
            return new Games(storeGamesDto.gamesID, storeGamesDto.gamesName, storeGamesDto.gameRentPrice, storeGamesDto.rentedStatus, storeGamesDto.rentedBy, storeGamesDto.rentedDate, storeGamesDto.returnByDate);
        }
        private UserDTO MapToUserDTO(User storeUser)
        {
            return new UserDTO(storeUser.userID, storeUser.userPassword, storeUser.userName, storeUser.userPhone, storeUser.userAddress, storeUser.userEmail);
        }
        
    }
}
