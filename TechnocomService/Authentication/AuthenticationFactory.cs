using TechnocomShared.Configuration;
using TechnocomShared.Constants;
using TechnocomShared.Enums;
using TechnocomShared.Interfaces;

namespace TechnocomService.Authentication
{
    public static class AuthenticationFactory
    {
        public static IAuthenticator GetAuthenticator()
        {
            return new TechnocomAuthenticator();
        }
    }
}