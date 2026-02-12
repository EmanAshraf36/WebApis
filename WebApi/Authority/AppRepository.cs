namespace WebApi.Authority;

public class AppRepository
{
    private static List<Application> _applications = new List<Application>()
    {
        new Application
        {
            ApplicationId = 1,
            ApplicationName = "MVCWebApp",
 
            Scopes = "read,write"
        }
    };

    public static Application? GetApplicationByClientId(string clientId)
    {
        return _applications.FirstOrDefault(x => x.ClientId == clientId);
    }
}