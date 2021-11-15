namespace VideoGameRental.Common.DTO
{
    public class GamesDTO
    {
        public string gamesID { get; set; }
        public string gamesName { get; set; }
        public string gameRentPrice { get; set; }
        public string rentedStatus { get; set; }
        public string rentedBy { get; set; }
        public string rentedDate { get; set; }
        public string returnByDate { get; set; }
        public GamesDTO(string id, string name, string price, string status, string rent, string rd, string rbd)
        {
            gamesID = id;
            gamesName = name;
            gameRentPrice = price;
            rentedStatus = "Not Rented";
            rentedBy = rent;
            rentedDate = rd;
            returnByDate = rbd;
        }
    }
}
