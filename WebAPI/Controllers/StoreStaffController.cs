using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;
using Newtonsoft.Json;
using System.IO;
using WebAPI.EntityFramework;
using VideoGameRental.Common.DTO;
using System.Collections.ObjectModel;

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
        [Route("AvailableGames")]
        public IHttpActionResult GetAvailableGames()
        {
            ICollection<GamesDTO> dtoList = new Collection<GamesDTO>();
            foreach (Games games in videoGameRentalStoreContext.Games)
            {
                dtoList.Add(MapToGamesDTO(games));
            }
            return Ok(dtoList.Where(x => x.rentedStatus == "Not Rented"));
        }

        [HttpGet]
        [Route("SearchUser")]
        public IHttpActionResult SearchUserByGamesRented(string gameid)
        {
            ICollection<GamesDTO> dtoList = new Collection<GamesDTO>();
            foreach (Games games in videoGameRentalStoreContext.Games)
            {
                dtoList.Add(MapToGamesDTO(games));
            }
            return Ok(dtoList.Where(x => x.gamesID == gameid));
        }

        [HttpGet]
        [Route("SearchGames")]
        public IHttpActionResult SearchGamesRentedByUser(string userid)
        {
            ICollection<GamesDTO> dtoList = new Collection<GamesDTO>();
            foreach (Games games in videoGameRentalStoreContext.Games)
            {
                dtoList.Add(MapToGamesDTO(games));
            }
            return Ok(dtoList.Where(x => x.rentedBy == userid));
        }
        //[HttpPatch]
        //[Route("UpdatePassword")]
        //public Dictionary<string, StoreStaff> UpdatePassword(string id, string password)
        //{
        //    Initialize();
        //    StoreStaff existingStaff = storeStaffs[id];
        //    if (existingStaff == null)
        //    {
        //        return null;
        //    }
        //    else
        //    {
        //        storeStaffs.Remove(id);
        //        existingStaff.UpdateName(password);
        //        storeStaffs.Add(id, existingStaff);
        //        Update();
        //    }
        //    return storeStaffs;
        //}

        //[HttpPatch]
        //[Route("UpdateName")]
        //public Dictionary<string, StoreStaff> UpdateName(string id, string name)
        //{
        //    Initialize();
        //    StoreStaff existingStaff= storeStaffs[id];
        //    if (existingStaff == null)
        //    {
        //        return null;
        //    }               
        //    else
        //    {
        //        storeStaffs.Remove(id);
        //        existingStaff.UpdateName(name);
        //        storeStaffs.Add(id, existingStaff);
        //        Update();
        //    }
        //    return storeStaffs;
        //}

        //[HttpPatch]
        //[Route("UpdatePhone")]
        //public Dictionary<string, StoreStaff> UpdatePhone(string id, string phone)
        //{
        //    Initialize();
        //    StoreStaff existingStaff = storeStaffs[id];
        //    if (existingStaff == null)
        //    {
        //        return null;
        //    }
        //    else
        //    {
        //        storeStaffs.Remove(id);
        //        existingStaff.UpdatePhone(phone);
        //        storeStaffs.Add(id, existingStaff);
        //        Update();
        //    }
        //    return storeStaffs;
        //}

        //[HttpPatch]
        //[Route("UpdateAddress")]
        //public Dictionary<string, StoreStaff> UpdateAddress(string id, string address)
        //{
        //    Initialize();
        //    StoreStaff existingStaff = storeStaffs[id];
        //    if (existingStaff == null)
        //    {
        //        return null;
        //    }
        //    else
        //    {
        //        storeStaffs.Remove(id);
        //        existingStaff.UpdateAddress(address);
        //        storeStaffs.Add(id, existingStaff);
        //        Update();
        //    }
        //    return storeStaffs;
        //}

        //[HttpPatch]
        //[Route("UpdateEmail")]
        //public Dictionary<string, StoreStaff> UpdateEmail(string id, string email)
        //{
        //    Initialize();
        //    StoreStaff existingStaff = storeStaffs[id];
        //    if (existingStaff == null)
        //    {
        //        return null;
        //    }
        //    else
        //    {
        //        storeStaffs.Remove(id);
        //        existingStaff.UpdateEmail(email);
        //        storeStaffs.Add(id, existingStaff);
        //        Update();
        //    }
        //    return storeStaffs;
        //}

        //[HttpDelete]
        //[Route("DeleteStaff/{id}")]
        //public Dictionary<string, StoreStaff> Delete(string id)
        //{
        //    Initialize();
        //    StoreStaff existingStaff = storeStaffs[id];
        //    if (existingStaff != null)
        //    {
        //        storeStaffs.Remove(id);
        //        Update();
        //    }          
        //    return storeStaffs;
        //}
        private GamesDTO MapToGamesDTO(Games storeGames)
        {
            return new GamesDTO(storeGames.gamesID, storeGames.gamesName, storeGames.gameRentPrice, storeGames.rentedStatus, storeGames.rentedBy, storeGames.rentedDate, storeGames.returnByDate);
        }
        private User MapToUserModel(UserDTO storeUserDto)
        {
            return new User(storeUserDto.userID, storeUserDto.userPassword, storeUserDto.userName, storeUserDto.userPhone, storeUserDto.userAddress, storeUserDto.userEmail);
        }
    }
}
