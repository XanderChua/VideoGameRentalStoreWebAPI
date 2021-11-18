using System;
using System.Globalization;
using System.Linq;
using System.Web.Http;
using VideoGameRental.Common.DTO;
using WebAPI.EntityFramework;
using WebAPI.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using WebAPI.Interface;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/UserManager")]
    public class UserController : ApiController
    {
        IContext videoGameRentalStoreContext;
        public UserController(IContext t)
        {
            videoGameRentalStoreContext = t;
        }
        public UserController()
        {
            videoGameRentalStoreContext = new VideoGameRentalStoreContext();
        }

        //VideoGameRentalStoreContext videoGameRentalStoreContext = new VideoGameRentalStoreContext();

        public List<double> storeEarned = new List<double>();
        private void Update()//run VS as administrator
        {
            string storeStaffJson = JsonConvert.SerializeObject(storeEarned);
            File.WriteAllText("StoreEarned.json", storeStaffJson);
        }

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
                gamepatch.rentedBy = "";
                DateTime convertedReturnDate = DateTime.ParseExact(gamepatch.returnByDate, "dd/MM/yyyy",CultureInfo.InvariantCulture);
                double daysLate = ((DateTime.Now - convertedReturnDate).TotalDays);
                if (daysLate > 0)
                {
                    double gamePrice = Double.Parse(gamepatch.gameRentPrice);
                    double fine = daysLate * (gamePrice * 0.5);
                    storeEarned.Add(fine + gamePrice);
                    //Console.WriteLine("$" + (fine + gamePrice) + " paid.");
                    //Console.WriteLine("You paid an extra $" + fine + " fine for returning " + daysLate + " days late.");
                    Update();
                }
                else
                {
                    double gamePrice = Double.Parse(gamepatch.gameRentPrice);
                    storeEarned.Add(gamePrice);
                    //Console.WriteLine("$" + (gamePrice) + " paid.");
                    Update();
                }
                gamepatch.rentedDate = "";
                gamepatch.returnByDate = "";
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