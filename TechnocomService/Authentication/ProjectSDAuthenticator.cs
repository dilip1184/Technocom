using TechnocomShared.Entities;
using TechnocomShared.EntityLoader;
using TechnocomShared.Interfaces;
using System;
using System.Linq;

namespace TechnocomService.Authentication
{
    internal sealed class TechnocomAuthenticator : IAuthenticator
    {
        public bool IsAuthenticated(int UserId, string password)
        {
            try
            {
                string userPassword = EntityBase.FillCollectionBySQLQuery<UserEntity>("SELECT UserPassword FROM [Users] WHERE UserId =" + UserId + " ").FirstOrDefault().UserPassword.ToString().Trim();

                if (String.Equals(password, userPassword, StringComparison.CurrentCulture))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}