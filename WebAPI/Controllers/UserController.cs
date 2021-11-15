using System;
using System.Globalization;
using System.Linq;
using System.Web.Http;
using VideoGameRental.Common.DTO;
using WebAPI.EntityFramework;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/UserManager")]
    public class UserController : ApiController
    {
        VideoGameRentalStoreContext videoGameRentalStoreContext = new VideoGameRentalStoreContext();

        [HttpPatch]
        [Route("Rent")]
        public IHttpActionResult Rent(string gameid, string userid)
        {
            var gamepatch = videoGameRentalStoreContext.Games.Where(x => x.gamesID == gameid).FirstOrDefault(); 
            if(gamepatch != null)
            {
                gamepatch.rentedStatus = "Rented";
                gamepatch.rentedBy = userid;
                gamepatch.rentedDate = DateTime.Now.ToString("dd/MM/yyyy");
                gamepatch.returnByDate = DateTime.Now.AddDays(6).ToString("dd/MM/yyyy");
                videoGameRentalStoreContext.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return Ok(MapToGamesDTO(gamepatch));
        }

        [HttpPatch]
        [Route("Return")]
        public IHttpActionResult ReturnGame(string gameid)
        {
            var gamepatch = videoGameRentalStoreContext.Games.Where(x => x.gamesID == gameid).FirstOrDefault();
            if (gamepatch != null)
            {
                gamepatch.rentedStatus = "Not Rented";
                gamepatch.rentedBy = null;
                DateTime convertedReturnDate = DateTime.ParseExact(gamepatch.returnByDate, "dd/MM/yyyy",CultureInfo.InvariantCulture);
                double daysLate = ((DateTime.Now - convertedReturnDate).TotalDays);
                if (daysLate > 0)
                {
                    double gamePrice = Double.Parse(gamepatch.gameRentPrice);
                    double fine = daysLate * (gamePrice * 0.5);
                }
                else
                {
                    double gamePrice = Double.Parse(gamepatch.gameRentPrice);
                }
                gamepatch.rentedDate = null;
                gamepatch.returnByDate = null;
                videoGameRentalStoreContext.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return Ok(MapToGamesDTO(gamepatch));
        }
       
        private GamesDTO MapToGamesDTO(Games storeGames)
        {
            return new GamesDTO(storeGames.gamesID, storeGames.gamesName, storeGames.gameRentPrice, storeGames.rentedStatus, storeGames.rentedBy, storeGames.rentedDate, storeGames.returnByDate);
        }
    }
}