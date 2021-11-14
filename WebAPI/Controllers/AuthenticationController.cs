using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Http;
using VideoGameRental.Common.DTO;
using WebAPI.EntityFramework;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/Login")]
    public class AuthenticationController : ApiController
    {
        VideoGameRentalStoreContext videoGameRentalStoreContext = new VideoGameRentalStoreContext();

        [HttpGet]
        [Route("VerifyStaff")]
        public bool ValidateStaff(string id, string password)
        {
            ICollection<StoreStaffDTO> dtoList = new Collection<StoreStaffDTO>();
            foreach (StoreStaff staff in videoGameRentalStoreContext.StoreStaffs)
            {
                dtoList.Add(MapToStaffDTO(staff));
                if(dtoList.Any(entry => entry.staffID != id && entry.staffPassword != password))
                {
                    return false;
                }
            }
            return true;
        }

        [HttpGet]
        [Route("VerifyUser")]
        public bool ValidateUser(string id, string password)
        {
            ICollection<UserDTO> dtoList = new Collection<UserDTO>();
            foreach (User user in videoGameRentalStoreContext.Users)
            {
                dtoList.Add(MapToUserDTO(user));
                if (dtoList.Any(entry => entry.userID != id && entry.userPassword != password))
                {
                    return false;
                }
            }
            return true;
        }
        private StoreStaffDTO MapToStaffDTO(StoreStaff storeStaff)
        {
            return new StoreStaffDTO(storeStaff.staffID, storeStaff.staffPassword, storeStaff.staffName, storeStaff.staffPhone, storeStaff.staffAddress, storeStaff.staffEmail);
        }
        private UserDTO MapToUserDTO(User storeUser)
        {
            return new UserDTO(storeUser.userID, storeUser.userPassword, storeUser.userName, storeUser.userPhone, storeUser.userAddress, storeUser.userEmail);
        }
    }
}
