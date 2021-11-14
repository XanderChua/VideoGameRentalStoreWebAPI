namespace VideoGameRental.Common.DTO
{
    public class StoreStaffDTO
    {
        public string staffID { get; set; }
        public string staffPassword { get; set; }
        public string staffName { get; set; }
        public string staffPhone { get; set; }
        public string staffAddress { get; set; }
        public string staffEmail { get; set; }
        public StoreStaffDTO(string id, string password, string name, string phone, string address, string email)
        {
            staffID = id;
            staffPassword = password;
            staffName = name;
            staffPhone = phone;
            staffAddress = address;
            staffEmail = email;
        }
    }
}
