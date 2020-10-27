using TechnocomService.Audit;
using TechnocomService.Authentication;
using TechnocomService.SessionManagement;
using TechnocomShared.Entities;
using TechnocomShared.EntityLoader;
using TechnocomShared.Enums;
using TechnocomShared.Exceptions;
using TechnocomShared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TechnocomService
{
    public class LogonService
    {
        public IEnumerable<IBusinessEntity> LoginUser(string LoginId, string Password, string UserIP)
        {
            IList<IBusinessEntity> response = new List<IBusinessEntity>();

            UserEntity userEntity;
            try
            {
                userEntity = GetUserDetailbyLoginId(LoginId);
            }
            catch (FinderException)
            {
                throw new BusinessException("Invalid User Name");
            }

            var isAuthenticated = AuthenticationFactory.GetAuthenticator().IsAuthenticated(userEntity.UserId, Password);

            if (isAuthenticated)
            {
                response.Add(SessionManager.CreateSession(LoginId, UserIP));
                response.Add(userEntity);
            }
            
            if (!isAuthenticated)
                throw new BusinessException("Your password is invalid.");

            if (userEntity.IsActive == false)
            {
                throw new BusinessException("Your account is disabled, Please contact the administrator.");
            }

           // AuditLogger.LogActivity(userEntity.UserEntityId.ToString(), DateTime.Now, ScreenActivityType.Login,11,"User Logon",-1,-1);
            return response;
        }
        public UserEntity GetUserDetailbyLoginId(string LoginId)
        {
            try
            {
                return EntityBase.FillCollectionBySQLQuery<UserEntity>("SELECT * FROM [dbo].[UserViewList] WHERE LoginId ='" + LoginId + "'").FirstOrDefault();
            }
            catch (FinderException)
            {
                return new UserEntity();
            }
        }
        public UserEntity GetUserEntityId(int UserId)
        {
            try
            {
                return EntityBase.FillCollectionBySQLQuery<UserEntity>("SELECT * FROM [dbo].[UserViewList] WHERE UserId =" + UserId).FirstOrDefault();
            }
            catch (FinderException)
            {
                return new UserEntity();
            }
        }
        public void Logout(string LoginId, string SessionId)
        {
            AuditLogger.LogActivity(LoginId, DateTime.Now, ScreenActivityType.Logout, 12, "User Logout", -1, -1);
            SessionManager.LogOff(LoginId, SessionId);
        }
        public IList<MenuEntity> GetMenuItems(int RoleId)
        {
            var parameters = new object[] { RoleId };
            return EntityBase.FillCollection<MenuEntity>("GetMenuByRole", parameters);
        }
    }
}