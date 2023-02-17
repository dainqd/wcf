namespace baseNetApi.models.Auth;

public class AuthenticateResponse
{
    public int id { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string username { get; set; }
    public string email { get; set; }
    public string phoneNumber { get; set; }
    public string birthday { get; set; }
    public string gender { get; set; } 
    public string address { get; set; } 
    public UserStatus status { get; set; }
    public Role role { get; set; }
    public string token { get; set; }
}