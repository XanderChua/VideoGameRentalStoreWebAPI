//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;
//using System.IO;
//using VideoGameRentalStore;
//using Moq;

//namespace UnitTest
//{
//    [TestClass]
//    public class UnitTest
//    {
//        StoreManager storeManager;
//        StoreStaff storeStaff;
//        Games games;
//        User user;
//        Earned earned;

//        [TestInitialize]
//        public void TestInitialize()
//        {
//            storeManager = new StoreManager();
//            games = new Games();
//            user = new User();
//            earned = new Earned();

//            FileStream fsStaff = new FileStream("StaffDetails.txt", FileMode.OpenOrCreate, FileAccess.Read);
//            fsStaff.Seek(0, SeekOrigin.Begin);
//            StreamReader srStaff = new StreamReader(fsStaff);
//            string strStaff = srStaff.ReadLine();
//            while (!string.IsNullOrWhiteSpace(strStaff))
//            {
//                var strArr = strStaff.Split(',');
//                var staff = new StoreStaff(strArr[0], strArr[1], strArr[2], strArr[3], strArr[4], strArr[5]);
//                if (!storeManager.StaffDictObj.ContainsKey(strArr[0]))
//                {
//                    storeManager.StaffDictObj.Add(strArr[0], staff);
//                }
//                strStaff = srStaff.ReadLine();
//            }
//            srStaff.Close();
//            fsStaff.Close();

//            FileStream fsUser = new FileStream("UserDetails.txt", FileMode.OpenOrCreate, FileAccess.Read);
//            fsUser.Seek(0, SeekOrigin.Begin);
//            StreamReader srUser = new StreamReader(fsUser);
//            string strUser = srUser.ReadLine();
//            while (!string.IsNullOrWhiteSpace(strUser))
//            {
//                var strArr = strUser.Split(',');
//                var user = new User(strArr[0], strArr[1], strArr[2], strArr[3], strArr[4], strArr[5]);
//                if (!user.UserDictObj.ContainsKey(strArr[0]))
//                {
//                    user.UserDictObj.Add(strArr[0], user);
//                }
//                strUser = srUser.ReadLine();
//            }
//            srUser.Close();
//            fsUser.Close();

//            FileStream fsGamesTest = new FileStream("GamesDetails.txt", FileMode.OpenOrCreate, FileAccess.Read);
//            fsGamesTest.Seek(0, SeekOrigin.Begin);
//            StreamReader srGamesTest = new StreamReader(fsGamesTest);
//            string strGamesTest = srGamesTest.ReadLine();
//            while (!string.IsNullOrWhiteSpace(strGamesTest))
//            {
//                var strArr = strGamesTest.Split(',');
//                var games = new Games(strArr[0], strArr[1], strArr[2], strArr[3], strArr[4], strArr[5], strArr[6]);
//                if (!games.GamesDictObj.ContainsKey(strArr[0]))
//                {
//                    games.GamesDictObj.Add(strArr[0], games);
//                }
//                strGamesTest = srGamesTest.ReadLine();
//            }
//            srGamesTest.Close();
//            fsGamesTest.Close();
//        }

//        [TestMethod]
//        public void TestLogin()
//        {
//            var id = "USER999";
//            var password = "user999";
//            var mockConsoleIO = new Mock<IConsoleIO>();           
//            var greeter = new Program(mockConsoleIO.Object);
//            greeter.LoginUser();

//            mockConsoleIO.Setup(t => t.ReadLine()).Returns(id);
//            mockConsoleIO.Verify(t => t.WriteLine("Enter ID to login:"), Times.Once());

//            mockConsoleIO.Setup(t => t.ReadLine()).Returns(password);
//            mockConsoleIO.Verify(t => t.WriteLine("Enter Password to login:"), Times.Once());
//        }
        
//        [TestMethod]
//        public void TestAddStoreStaff()
//        {
//            string inputStaffID = "STAFF999";
//            string inputStaffPassword = "pw999";
//            string inputStaffName = "Linda";
//            string inputStaffPhone = "90807060";
//            string inputStaffAddress = "neverland street 9";
//            string inputStaffEmail = "linda@mail.com";
//            storeManager.AddStoreStaff(storeStaff, inputStaffID, inputStaffPassword, inputStaffName, inputStaffPhone, inputStaffAddress, inputStaffEmail);
//        }

//        [TestMethod]
//        public void TestAddGames()
//        {
//            string inputGamesID = "GAME997";
//            string inputGamesName = "GameForUnitTest997";
//            string rentPrice = "97";
//            storeManager.AddGames(games, inputGamesID, inputGamesName, rentPrice);
//            games.UpdateGames();
//        }

//        [TestMethod]
//        public void TestAddGames1()
//        {
//            string inputGamesID = "GAME999";
//            string inputGamesName = "GameForUnitTest999";
//            string rentPrice = "abc";
//            storeManager.AddGames(games, inputGamesID, inputGamesName, rentPrice);
//            games.UpdateGames();
//        }

//        [TestMethod]
//        public void TestListStaff()
//        {
//            storeManager.ListStaff(storeStaff);
//        }

//        [TestMethod]
//        public void TestListUser()
//        {
//            storeManager.ListUser(user);
//        }

//        [TestMethod]
//        public void TestGamesAvailableManager()
//        {
//            storeManager.GamesAvailable(games);
//        }

//        [TestMethod]
//        public void TestOverduedGames()
//        {
//            DateTime dateTime = DateTime.Now;
//            storeManager.OverduedGames(dateTime, games);
//        }

//        [TestMethod]
//        public void TestTotalEarned()
//        {
//            storeManager.TotalEarned(earned);
//        }

//        [TestMethod]
//        public void TestGamesRented()
//        {
//            storeManager.GamesRented(games);
//        }

//        [TestMethod]
//        public void TestAddUser()
//        {
//            string inputUserID = "USER999";
//            string inputUserPassword = "user999";
//            string inputUserName = "Melinda";
//            string inputUserPhone = "88899900";
//            string inputUserAddress = "dempsy hill";
//            string inputUserEmail = "melinda@mail.com";
//            storeStaff.AddUser(user, inputUserID, inputUserPassword, inputUserName, inputUserPhone, inputUserAddress, inputUserEmail);
//        }

//        [TestMethod]
//        public void TestAddUser1()
//        {
//            string inputUserID = "USER999";
//            string inputUserPassword = "user999";
//            string inputUserName = "Melinda";
//            string inputUserPhone = "abcd";
//            string inputUserAddress = "dempsy hill";
//            string inputUserEmail = "melinda@mail.com";
//            storeStaff.AddUser(user, inputUserID, inputUserPassword, inputUserName, inputUserPhone, inputUserAddress, inputUserEmail);
//        }

//        [TestMethod]
//        public void TestGamesAvailableStaff()
//        {
//            storeStaff.GamesAvailable(games);
//        }

//        [TestMethod]
//        public void TestSearchUserByGames()
//        {
//            string inputGameID = "GAME001";
//            storeStaff.SearchUserByGames(games, inputGameID);
//            Assert.AreEqual(games.GamesDictObj[inputGameID].rentedBy, "USER003");
//        }

//        [TestMethod]
//        public void TestSearchGamesByUser()
//        {
//            string inputGameID = "GAME001";
//            string inputUser = "USER003";
//            storeStaff.SearchGamesByUser(games, inputUser);
//            Assert.AreEqual(games.GamesDictObj[inputGameID].gamesID, "GAME001");
//        }

//        [TestMethod]
//        public void TestGameRent()
//        {
//            string inputUserID = "USER001";
//            DateTime dateTime = DateTime.Now;
//            string selectGameRent = "GAME998";
//            user.RentGames(inputUserID, dateTime, selectGameRent);
//            bool res;
//            if (games.GamesDictObj[selectGameRent].rentedBy != null)
//            {
//                res = true;
//                Assert.IsTrue(res);
//            }
//        }

//        [TestMethod]
//        public void TestGameReturn()
//        {
//            string inputUserID = "USER001";
//            DateTime dateTime = DateTime.Now;
//            string selectGameReturn = "GAME998";
//            user.ReturnGames(inputUserID, dateTime, selectGameReturn);
//        }//Test this if return on time

//        [TestMethod]
//        public void TestGameReturn1()
//        {
//            string inputUserID = "USER001";
//            DateTime dateTime = DateTime.Now;
//            string selectGameReturn = "GAME998";
//            user.ReturnGames(inputUserID, dateTime.AddDays(7), selectGameReturn);
//        }//Test this if return late

//        [TestMethod]
//        public void TestListRentedGames()
//        {
//            string inputUserID = "USER002";
//            user.ListRentedGames(inputUserID);
//        }

//        [TestMethod]
//        public void TestStoreStaffUpdate()
//        {
//            storeStaff.UpdateStaffs();
//        }

//        [TestMethod]
//        public void TestGamesUpdate()
//        {
//            games.UpdateGames();
//        }
       
//        [TestMethod]
//        public void TestUserUpdate()
//        {
//            user.UpdateUser();
//        }

//        [TestMethod]
//        public void TestEarnedUpdate()
//        {
//            earned.UpdateEarned();
//        }

//        [TestCleanup]
//        public void Cleanup()
//        {
//            storeStaff = null;
//            user = null;
//            games = null;
//            earned = null;
//        }
//    }
//}
