namespace Token.Models;

public class UserInfo
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string City { get; set; }

    public UserInfo(
        int userId, 
        string userName, 
        string firstName, 
        string lastName, 
        string city)
    {
        UserId = userId;
        UserName = userName;
        FirstName = firstName;
        LastName = lastName;
        City = city;
    }
}
