using Moq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using VideoGameRental.Common.DTO;
using WebAPI.Controllers;
using WebAPI.EntityFramework;
using WebAPI.Models;
using Xunit;

namespace UnitTestWebAPI
{
    public class UnitTest1
    {
        [Fact]
        public void LoginStaffSuccess()
        {
            StoreStaff storeStaff = new StoreStaff() { staffID = "STAFF001", staffPassword = "pw1", staffName = "John", staffPhone = "99990000", staffAddress = "12 fwfewf 12", staffEmail = "qwe@mail.com" };
            var data = new List<StoreStaff>
            {
                storeStaff
            }.AsQueryable();

            var mockSet = new Mock<DbSet<StoreStaff>>();
            mockSet.As<IQueryable<StoreStaff>>().Setup(x => x.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<StoreStaff>>().Setup(x => x.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<StoreStaff>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<StoreStaff>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<VideoGameRentalStoreContext>();
            mockContext.Setup(x => x.StoreStaffs).Returns(mockSet.Object);

            AuthenticationController ac = new AuthenticationController(mockContext.Object);
            IHttpActionResult res = ac.ValidateStaff("STAFF001", "pw1");

            var contentResult = res as OkNegotiatedContentResult<bool>;

            Assert.True(contentResult.Content);
        }

        [Fact]
        public void LoginUserSuccess()
        {
            User storeUser = new User() { userID = "USER001", userPassword = "user1" };
            var data = new List<User>
            {
                storeUser
            }.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(x => x.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<User>>().Setup(x => x.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<User>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<User>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<VideoGameRentalStoreContext>();
            mockContext.Setup(x => x.Users).Returns(mockSet.Object);

            AuthenticationController ac = new AuthenticationController(mockContext.Object);
            IHttpActionResult res = ac.ValidateUser("USER001", "user1");

            var contentResult = res as OkNegotiatedContentResult<bool>;

            Assert.True(contentResult.Content);
        }

        [Fact]
        public void AddStaff()
        {
            StoreStaffDTO storeStaff = new StoreStaffDTO("STAFF001", "pw1", "John", "99990000", "12 fwfewf 12", "qwe@mail.com"); //{ staffID = "STAFF001", staffPassword = "pw1", staffName = "John", staffPhone = "99990000", staffAddress = "12 fwfewf 12", staffEmail = "qwe@mail.com" };
            var data = new List<StoreStaff>
            {
            }.AsQueryable();

            var mockSet = new Mock<DbSet<StoreStaff>>();
            mockSet.As<IQueryable<StoreStaff>>().Setup(x => x.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<StoreStaff>>().Setup(x => x.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<StoreStaff>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<StoreStaff>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<VideoGameRentalStoreContext>();
            mockContext.Setup(x => x.StoreStaffs).Returns(mockSet.Object);

            StoreManagerController smc = new StoreManagerController(mockContext.Object);
            IHttpActionResult res = smc.AddStaff(storeStaff);

            var contentResult = res as OkNegotiatedContentResult<StoreStaffDTO>;

            Assert.Equal(storeStaff, contentResult.Content);
        }

        [Fact]
        public void AddGames()
        {
            GamesDTO storeGames = new GamesDTO("GAME001", "Hamezzz", "25", "Not Rented", "", "", ""); //{ staffID = "STAFF001", staffPassword = "pw1", staffName = "John", staffPhone = "99990000", staffAddress = "12 fwfewf 12", staffEmail = "qwe@mail.com" };
            var data = new List<Games>
            {
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Games>>();
            mockSet.As<IQueryable<Games>>().Setup(x => x.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Games>>().Setup(x => x.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Games>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Games>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<VideoGameRentalStoreContext>();
            mockContext.Setup(x => x.Games).Returns(mockSet.Object);

            StoreManagerController smc = new StoreManagerController(mockContext.Object);
            IHttpActionResult res = smc.AddGames(storeGames);

            var contentResult = res as OkNegotiatedContentResult<GamesDTO>;

            Assert.Equal(storeGames, contentResult.Content);
        }

        [Fact]
        public void GetAllStaff()
        {
            StoreStaff storeStaff = new StoreStaff() { staffID = "STAFF001", staffPassword = "pw1", staffName = "Tammy", staffPhone = "98989898", staffAddress = "12 fwdfwed 12", staffEmail = "tammy@mail.com"};
            StoreStaffDTO storeStaffDTO = MapToStaffDTO(storeStaff); //= new StoreStaffDTO("STAFF001", "pw1", "Tammy", "98989898", "12 fwdfwed 12", "tammy@mail.com");
            var data = new List<StoreStaff>
            {
                storeStaff
            }.AsQueryable();

            var mockSet = new Mock<DbSet<StoreStaff>>();
            mockSet.As<IQueryable<StoreStaff>>().Setup(x => x.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<StoreStaff>>().Setup(x => x.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<StoreStaff>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<StoreStaff>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<VideoGameRentalStoreContext>();
            mockContext.Setup(x => x.StoreStaffs).Returns(mockSet.Object);

            StoreManagerController smc = new StoreManagerController(mockContext.Object);
            IHttpActionResult res = smc.GetAllStaff();

            var contentResult = res as OkNegotiatedContentResult<ICollection<StoreStaffDTO>>;

            ICollection<StoreStaffDTO> assertList = new Collection<StoreStaffDTO>();
            assertList.Add(storeStaffDTO);
            //assertList.Add(storeStaffDTO1);
            Assert.True(assertList.Equals(contentResult.Content));
            //Assert.Equal(assertList, contentResult.Content);
        }

        [Fact]
        public void GetAllUser()
        {
            User storeUser = new User() { userID = "USER001", userPassword = "pw1", userName = "Tammy", userPhone = "98989898", userAddress = "12 fwdfwed 12", userEmail = "tammy@mail.com" };
            UserDTO storeUserDTO = new UserDTO("USER001", "pw1", "Tammy", "98989898", "12 fwdfwed 12", "tammy@mail.com");
            var data = new List<User>
            {
                storeUser
            }.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(x => x.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<User>>().Setup(x => x.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<User>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<User>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<VideoGameRentalStoreContext>();
            mockContext.Setup(x => x.Users).Returns(mockSet.Object);

            StoreManagerController smc = new StoreManagerController(mockContext.Object);
            IHttpActionResult res = smc.GetAllUser();

            var contentResult = res as OkNegotiatedContentResult<ICollection<UserDTO>>;

            ICollection<UserDTO> assertList = new Collection<UserDTO>();
            assertList.Add(storeUserDTO);
            //assertList.Add(storeStaffDTO1);
            Assert.Equal(assertList, contentResult.Content);
        }

        [Fact]
        public void GetAvailableGames()
        {
            Games storeGames = new Games() { gamesID = "GAME001", gamesName = "Gamezxc", gameRentPrice = "20", rentedStatus = "Not Rented", rentedBy = "", rentedDate = "", returnByDate = "" };
            Games storeGames1 = new Games() { gamesID = "GAME002", gamesName = "GamezxcABC", gameRentPrice = "20", rentedStatus = "Rented", rentedBy = "USER001", rentedDate = "01/11/2021", returnByDate = "07/11/2021" };
            GamesDTO storeGamesDTO = new GamesDTO("GAME001", "Gamezxc", "20", "Not Rented", "", "", "");
            GamesDTO storeGamesDTO1 = new GamesDTO("GAME002", "GamezxcABC", "20", "Rented", "USER001", "01/11/2021", "07/11/2021");
            var data = new List<Games>
            {
                storeGames
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Games>>();
            mockSet.As<IQueryable<Games>>().Setup(x => x.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Games>>().Setup(x => x.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Games>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Games>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<VideoGameRentalStoreContext>();
            mockContext.Setup(x => x.Games).Returns(mockSet.Object);

            StoreManagerController smc = new StoreManagerController(mockContext.Object);
            IHttpActionResult res = smc.GetAvailableGames();

            var contentResult = res as OkNegotiatedContentResult<ICollection<GamesDTO>>;

            ICollection<GamesDTO> assertList = new Collection<GamesDTO>();
            assertList.Add(storeGamesDTO);
            assertList.Add(storeGamesDTO1);
            //assertList.Add(storeStaffDTO1);
            Assert.Equal(assertList, contentResult.Content);
        }
        private StoreStaffDTO MapToStaffDTO(StoreStaff storeStaff)
        {
            return new StoreStaffDTO(storeStaff.staffID, storeStaff.staffPassword, storeStaff.staffName, storeStaff.staffPhone, storeStaff.staffAddress, storeStaff.staffEmail);
        }
    }   
}

