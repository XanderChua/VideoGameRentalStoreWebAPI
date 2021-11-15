using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.Http;
using VideoGameRental.Common.DTO;
using WebAPI.EntityFramework;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/StoreStaffManager")]
    public class StoreStaffController : ApiController
    {
        VideoGameRentalStoreContext videoGameRentalStoreContext = new VideoGameRentalStoreContext();

        [HttpPost]
        [Route("AddUser")]
        public UserDTO AddUser(UserDTO storeUser)
        {
            videoGameRentalStoreContext.Users.Add(MapToUserModel(storeUser));
            videoGameRentalStoreContext.SaveChanges();
            return storeUser;
        }

        [HttpGet]
        [Route("SearchUsers")]
        public IHttpActionResult SearchUserByGamesRented(string gameid)
        {
            ICollection<UserDTO> dtoList = new Collection<UserDTO>();
            foreach (Games games in videoGameRentalStoreContext.Games)
            {
                if (games.gamesID == gameid)
                {
                    foreach (User user in videoGameRentalStoreContext.Users)
                    {
                        if (user.userID == games.rentedBy)
                            dtoList.Add(MapToUserDTO(user));
                    }
                    break;
                }
            }
            return Ok(dtoList);
        }

        [HttpGet]
        [Route("SearchGames")]
        public IHttpActionResult SearchGamesRentedByUser(string userid)
        {
            ICollection<GamesDTO> dtoList = new Collection<GamesDTO>();
            foreach (Games games in videoGameRentalStoreContext.Games)
            {
                if (games.rentedBy==userid)
                    dtoList.Add(MapToGamesDTO(games));
            }
            return Ok(dtoList);
        }
       
        private User MapToUserModel(UserDTO storeUser)
        {
            return new User(storeUser.userID, storeUser.userPassword, storeUser.userName, storeUser.userPhone, storeUser.userAddress, storeUser.userEmail);
        }
        private UserDTO MapToUserDTO(User storeUser)
        {
            return new UserDTO(storeUser.userID, storeUser.userPassword, storeUser.userName, storeUser.userPhone, storeUser.userAddress, storeUser.userEmail);
        }
        private GamesDTO MapToGamesDTO(Games storeGames)
        {
            return new GamesDTO(storeGames.gamesID, storeGames.gamesName, storeGames.gameRentPrice, storeGames.rentedStatus, storeGames.rentedBy, storeGames.rentedDate, storeGames.returnByDate);
        }
    }
}
