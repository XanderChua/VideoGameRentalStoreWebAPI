using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VideoGameRental.Common.DTO;
using VideoGameRentalStore.Interfaces;

namespace VideoGameRentalStore.ViewModel
{
    class VideoGameRentalViewModel : IVideoGameRentalViewModel
    {
        private HttpClient _httpClient;
        private string baselink = "https://localhost:44353/api";
        public VideoGameRentalViewModel() { }

        public void Initialize()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(baselink);
        }
        //Authentication
        public bool ValidateStaff(string inputID, string password)
        {
            Task<string> responseBody;
            var response = _httpClient.GetAsync($"{baselink}/Login/VerifyStaff?id="+inputID+"&password="+password);
            response.Wait();
            if (response.Result.IsSuccessStatusCode)
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                responseBody.Wait();
                return bool.Parse(responseBody.Result);
            }
            else
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                throw new Exception(responseBody.Result);
            }
        }
        public bool ValidateUser(string inputID, string password)
        {
            Task<string> responseBody;
            var response = _httpClient.GetAsync($"{baselink}/Login/VerifyUser?id=" + inputID + "&password=" + password);
            response.Wait();
            if (response.Result.IsSuccessStatusCode)
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                responseBody.Wait();
                return bool.Parse(responseBody.Result);
            }
            else
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                throw new Exception(responseBody.Result);
            }
        }

        //Store Manager
        public StoreStaffDTO AddStaff(string inputStaffID, string inputStaffPassword, string inputStaffName, string inputStaffPhone, string inputStaffAddress, string inputStaffEmail)
        {
            Task<string> responseBody;
            StoreStaffDTO dto = new StoreStaffDTO(inputStaffID, inputStaffPassword, inputStaffName, inputStaffPhone, inputStaffAddress, inputStaffEmail);
            StringContent queryString = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync($"{baselink}/StoreManager/AddStaff",queryString);
            response.Wait();
            if (response.Result.IsSuccessStatusCode)
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                responseBody.Wait();
                return JsonConvert.DeserializeObject<StoreStaffDTO>(responseBody.Result);
            }
            else
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                throw new Exception(responseBody.Result);
            }
        }
        public GamesDTO AddGames(string inputGamesID, string inputGamesName, string rentPrice)
        {
            Task<string> responseBody;
            GamesDTO dto = new GamesDTO(inputGamesID, inputGamesName, rentPrice,"Not Rented", string.Empty, string.Empty,String.Empty);
            StringContent queryString = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync($"{baselink}/StoreManager/AddGames", queryString);
            response.Wait();
            if (response.Result.IsSuccessStatusCode)
            {
                
                responseBody = response.Result.Content.ReadAsStringAsync();
                responseBody.Wait();
                return JsonConvert.DeserializeObject<GamesDTO>(responseBody.Result);
            }
            else
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                throw new Exception(responseBody.Result);
            }
        }
        public ICollection<StoreStaffDTO> ListStaff()
        {
            Task<string> responseBody;
            var response = _httpClient.GetAsync($"{baselink}/StoreManager/ListStaff");
            if (response.Result.IsSuccessStatusCode)
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                responseBody.Wait();
                return JsonConvert.DeserializeObject<ICollection<StoreStaffDTO>>(responseBody.Result);
            }
            else
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                throw new Exception(responseBody.Result);
            }
        }
        public ICollection<UserDTO> ListUser()
        {
            Task<string> responseBody;
            var response = _httpClient.GetAsync($"{baselink}/StoreManager/ListUser");
            if (response.Result.IsSuccessStatusCode)
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                responseBody.Wait();
                return JsonConvert.DeserializeObject<ICollection<UserDTO>>(responseBody.Result);
            }
            else
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                throw new Exception(responseBody.Result);
            }
        }
        public ICollection<GamesDTO> GamesAvailable()
        {
            Task<string> responseBody;
            var response = _httpClient.GetAsync($"{baselink}/StoreManager/AvailableGames");
            if (response.Result.IsSuccessStatusCode)
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                responseBody.Wait();
                return JsonConvert.DeserializeObject<ICollection<GamesDTO>>(responseBody.Result);
            }
            else
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                throw new Exception(responseBody.Result);
            }
        }
        public ICollection<GamesDTO> RentedGames()
        {
            Task<string> responseBody;
            var response = _httpClient.GetAsync($"{baselink}/StoreManager/GamesRented");
            if (response.Result.IsSuccessStatusCode)
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                responseBody.Wait();
                return JsonConvert.DeserializeObject<ICollection<GamesDTO>>(responseBody.Result);
            }
            else
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                throw new Exception(responseBody.Result);
            }
        }
        public ICollection<GamesDTO> RentedGames(string id)
        {
            Task<string> responseBody;
            var response = _httpClient.GetAsync($"{baselink}/StoreManager/GamesRented?id=" + id);
            if (response.Result.IsSuccessStatusCode)
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                responseBody.Wait();
                return JsonConvert.DeserializeObject<ICollection<GamesDTO>>(responseBody.Result);
            }
            else
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                throw new Exception(responseBody.Result);
            }
        }
        public ICollection<GamesDTO> OverduedGames()
        {
            Task<string> responseBody;
            
            var response = _httpClient.GetAsync($"{baselink}/StoreManager/OverduedGames");
            if (response.Result.IsSuccessStatusCode)
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                responseBody.Wait();
                return JsonConvert.DeserializeObject<ICollection<GamesDTO>>(responseBody.Result);
            }
            else
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                throw new Exception(responseBody.Result);
            }
        }
        //@todo Trainer: - implement it correctly
        public double TotalEarned()
        {
            Task<string> responseBody;
            var response = _httpClient.GetAsync($"{baselink}/StoreManager/TotalEarned");
            if (response.Result.IsSuccessStatusCode)
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                responseBody.Wait();
                return JsonConvert.DeserializeObject<double>(responseBody.Result);
            }
            else
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                throw new Exception(responseBody.Result);
            }
        }

        //Store Staff
        public UserDTO AddUser(string inputUserID, string inputUserPassword, string inputUserName, string inputUserPhone, string inputUserAddress, string inputUserEmail)
        {
            Task<string> responseBody;
            UserDTO dto = new UserDTO(inputUserID, inputUserPassword, inputUserName, inputUserPhone, inputUserAddress, inputUserEmail);
            StringContent queryString = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync($"{baselink}/StoreStaffManager/AddUser",queryString);
            response.Wait();
            if (response.Result.IsSuccessStatusCode)
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                responseBody.Wait();
                return JsonConvert.DeserializeObject<UserDTO>(responseBody.Result);
            }
            else
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                throw new Exception(responseBody.Result);
            }
        }
        //GamesAvailable()
        public ICollection<GamesDTO> SearchUser(string searchUserByGames)
        {
            Task<string> responseBody;
            var response = _httpClient.GetAsync($"{baselink}/StoreStaffManager/SearchUsers?gameid="+searchUserByGames);
            response.Wait();
            if (response.Result.IsSuccessStatusCode)
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                responseBody.Wait();
                return JsonConvert.DeserializeObject<ICollection<GamesDTO>>(responseBody.Result);
            }
            else
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                throw new Exception(responseBody.Result);
            }
        }
        public ICollection<GamesDTO> SearchGame(string searchGamesByUser)
        {
            Task<string> responseBody;
            var response = _httpClient.GetAsync($"{baselink}/StoreStaffManager/SearchGames?userid=" +searchGamesByUser);
            response.Wait();
            if (response.Result.IsSuccessStatusCode)
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                responseBody.Wait();
                return JsonConvert.DeserializeObject<ICollection<GamesDTO>>(responseBody.Result);
            }
            else
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                throw new Exception(responseBody.Result);
            }
        }

        //User
        public GamesDTO Rent(string id, string selectGameRent)
        {
            Task<string> responseBody;
            var method = new HttpMethod("PATCH");
            HttpRequestMessage request = new HttpRequestMessage(method, $"{baselink}/UserManager/Rent?gameID=" + selectGameRent + "&userid=" + id);
            var response = _httpClient.SendAsync(request);
            response.Wait();
            if (response.Result.IsSuccessStatusCode)
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                responseBody.Wait();
                return JsonConvert.DeserializeObject<GamesDTO>(responseBody.Result);
            }
            else
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                throw new Exception(responseBody.Result);
            }
        }
        public GamesDTO Return(string selectGameRent)
        {
            Task<string> responseBody;
            var method = new HttpMethod("PATCH");
            HttpRequestMessage request = new HttpRequestMessage(method, $"{baselink}/UserManager/Return?gameID=" + selectGameRent);
            var response = _httpClient.SendAsync(request);
            response.Wait();
            if (response.Result.IsSuccessStatusCode)
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                responseBody.Wait();
                return JsonConvert.DeserializeObject<GamesDTO>(responseBody.Result);
            }
            else
            {
                responseBody = response.Result.Content.ReadAsStringAsync();
                throw new Exception(responseBody.Result);
            }
        }
    }
}
