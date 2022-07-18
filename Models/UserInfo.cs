namespace DotNetCoreWebAPI.Models
{
    public class UserInfo
    {
        public int Identifier { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CityName { get; set; }

        public UserInfo(
            int identifier,
            string firstName,
            string lastName,
            string cityName)
        {
            this.Identifier = identifier;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.CityName = cityName;
        }
    }
}
