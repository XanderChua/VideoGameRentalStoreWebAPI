using System.Web.Http;
using WebAPI.EntityFramework;
using WebAPI.Interface;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/Login")]
    public class AuthenticationController : ApiController
    {
        IContext videoGameRentalStoreContext;
        public AuthenticationController(IContext t)
        {
            videoGameRentalStoreContext = t;
        }
        public AuthenticationController()
        {
            videoGameRentalStoreContext = new VideoGameRentalStoreContext();
        }

        //VideoGameRentalStoreContext videoGameRentalStoreContext = new VideoGameRentalStoreContext();

        [HttpGet]
        [Route("VerifyStaff")]
        public IHttpActionResult ValidateStaff(string id, string password)
        {
            foreach (StoreStaff staff in videoGameRentalStoreContext.StoreStaffs)
            {
                if(staff.staffID==id && staff.staffPassword==password)
                {
                    return Ok(true);
                }
            }
            return Ok(false);
        }

        [HttpGet]
        [Route("VerifyUser")]
        public IHttpActionResult ValidateUser(string id, string password)
        {
            foreach (User user in videoGameRentalStoreContext.Users)
            {
                if (user.userID == id && user.userPassword == password)
                {
                    return Ok(true);
                }
            }
            return Ok(false);
        }
    }
}
