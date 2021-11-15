using System.Web.Http;
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
            foreach (StoreStaff staff in videoGameRentalStoreContext.StoreStaffs)
            {
                if(staff.staffID==id && staff.staffPassword==password)
                {
                    return true;
                }
            }
            return false;
        }

        [HttpGet]
        [Route("VerifyUser")]
        public bool ValidateUser(string id, string password)
        {
            foreach (User user in videoGameRentalStoreContext.Users)
            {
                if (user.userID == id && user.userPassword == password)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
