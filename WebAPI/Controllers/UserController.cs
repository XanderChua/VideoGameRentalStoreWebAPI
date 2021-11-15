using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
        private string readEarned;
        public List<double> storeEarned = new List<double>();

        private void Initialize()
        {
            readEarned = File.ReadAllText("StoreEarned.json");
            storeEarned = JsonConvert.DeserializeObject<List<double>>(readEarned);
        }
        private void Update()//run VS as administrator
        {
            string storeEarnedJson = JsonConvert.SerializeObject(storeEarned);
            File.WriteAllText("StoreEarned.json", storeEarnedJson);
        }

        VideoGameRentalStoreContext videoGameRentalStoreContext = new VideoGameRentalStoreContext();

        [HttpPatch]
        [Route("Rent")]
        public IHttpActionResult Rent(string gameid, string userid, DateTime dateTime)
        {
            ICollection<GamesDTO> dtoList = new Collection<GamesDTO>();
            foreach (Games games in videoGameRentalStoreContext.Games)
            {
                dtoList.Add(MapToGamesDTO(games));
            }
            var gamepatch = dtoList.Where(x => x.gamesID == gameid).FirstOrDefault(); 
            if(gamepatch != null)
            {
                gamepatch.rentedStatus = "Rented";
                gamepatch.rentedBy = userid;
                gamepatch.rentedDate = dateTime.ToString("dd/MM/yyyy");
                gamepatch.returnByDate = dateTime.AddDays(6).ToString("dd/MM/yyyy");
                videoGameRentalStoreContext.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return Ok("Rent success!!");
        }

        [HttpPatch]
        [Route("Return")]
        public IHttpActionResult ReturnGame(string gameid, DateTime dateTime)
        {
            Initialize();
            ICollection<GamesDTO> dtoList = new Collection<GamesDTO>();
            foreach (Games games in videoGameRentalStoreContext.Games)
            {
                dtoList.Add(MapToGamesDTO(games));
            }
            var gamepatch = dtoList.Where(x => x.gamesID == gameid).FirstOrDefault();
            if (gamepatch != null)
            {
                gamepatch.rentedStatus = "Not Rented";
                gamepatch.rentedBy = null;
                DateTime convertedReturnDate = Convert.ToDateTime(gamepatch.returnByDate);
                double daysLate = ((dateTime - convertedReturnDate).TotalDays);
                if (daysLate > 0)
                {
                    double gamePrice = Double.Parse(gamepatch.gameRentPrice);
                    double fine = daysLate * (gamePrice * 0.5);
                    storeEarned.Add(fine + gamePrice);
                    Console.WriteLine("$" + (fine + gamePrice) + " paid.");
                    Console.WriteLine("You paid an extra $" + fine + " fine for returning " + daysLate + " days late.");
                }
                else
                {
                    double gamePrice = Double.Parse(gamepatch.gameRentPrice);
                    storeEarned.Add(gamePrice);
                    Console.WriteLine("$" + (gamePrice) + " paid.");
                }
                gamepatch.rentedDate = null;
                gamepatch.returnByDate = null;
                Update();
                videoGameRentalStoreContext.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return Ok("Return success!!");
        }

        [HttpGet]
        [Route("RentedGames")]
        public IHttpActionResult GetRentedGames(string userid)
        {
            ICollection<GamesDTO> dtoList = new Collection<GamesDTO>();
            foreach (Games games in videoGameRentalStoreContext.Games)
            {
                dtoList.Add(MapToGamesDTO(games));
            }
            return Ok(dtoList.Where(x => x.rentedBy == userid));
        }
        private GamesDTO MapToGamesDTO(Games storeGames)
        {
            return new GamesDTO(storeGames.gamesID, storeGames.gamesName, storeGames.gameRentPrice, storeGames.rentedStatus, storeGames.rentedBy, storeGames.rentedDate, storeGames.returnByDate);
        }
    }
}