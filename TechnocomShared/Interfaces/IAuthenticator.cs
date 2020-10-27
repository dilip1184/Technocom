namespace TechnocomShared.Interfaces
{
    public interface IAuthenticator
    {
        bool IsAuthenticated(int UserId, string Password);
    }
}