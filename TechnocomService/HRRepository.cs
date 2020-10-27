using TechnocomShared.Entities;
using TechnocomShared.Constants;
using TechnocomShared.DataAccess;
using TechnocomShared.EntityLoader;
using TechnocomShared.Enums;
using TechnocomShared.Exceptions;
using TechnocomShared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using TechnocomShared.Helpers;

namespace TechnocomService
{
    public class HRRepository : BaseService, IBusinessService
    {
        public HRRepository()
            : base()
        {
        }
        public HRRepository(ContextInfo context)
            : base(context)
        {

        }

        #region Role
        public IList<RoleManagementEntity> GetRoleList()
        {
            try
            {
                return EntityBase.FillCollectionBySQLQuery<RoleManagementEntity>("SELECT * FROM [Role] WHERE [RoleId] NOT IN (1,2,3)");
            }
            catch (FinderException)
            {
                return new List<RoleManagementEntity>();
            }
        }
        public RoleManagementEntity GetRoleById(int RoleId)
        {
            try
            {
                return EntityBase.FillCollectionBySQLQuery<RoleManagementEntity>("SELECT TOP(1) * FROM [Role] WHERE [RoleId]=" + RoleId + "").FirstOrDefault();
            }
            catch (FinderException)
            {
                return new RoleManagementEntity();
            }
        }
        public IList<MenuItemEntity> GetMenuListByRoleId(int RoleId)
        {
            try
            {
                return EntityBase.FillCollectionBySQLQuery<MenuItemEntity>("SELECT a.[NavigationId], a.[ParentId], a.[MenuName], a.[URLPath], a.[IconClass], a.[SortOrder], CASE WHEN a.[NavigationId] NOT IN (SELECT [NavigationId] FROM [MenuRoleDetail] WHERE RoleId = " + RoleId + ") THEN CAST('false' as bit) ELSE CAST('true' as bit) END AS [IsChecked] FROM [dbo].[Menu] a");
            }
            catch (FinderException)
            {
                return new List<MenuItemEntity>();
            }
        }
        public OperationStatusEntity ManageRole(RoleManagementEntity param)
        {
            var parameters = new object[] 
                                            { 
                                                param.OperationId, param.RoleName, param.RoleId
                                            };

            return EntityBase.FillObject<OperationStatusEntity>("RoleManage", parameters);
        }
        public void UpdateMenuRoleDetail(int RoleId, int ParentId, string NavigationIds, int CreatedBy)
        {
            var parameters = new object[] { RoleId, ParentId, NavigationIds, CreatedBy };
            DataConnection.EExecuteNonQuery("MenuRoleDetailManage", parameters);
        }

        //public string UpdateUserPassword(string NewPassword, int UserId)
        //{
        //    try
        //    {
        //        DataConnection.ExecuteSQLQuery("UPDATE [Users] SET [UserPassword] ='" + NewPassword + "', [UserLastPasswordChange] = GETDATE() WHERE [UserId] =" + UserId + "");
        //        return "success";
        //    }
        //    catch (BaseException be)
        //    {
        //        return be.DisplayMessage;
        //    }
        //}

        #endregion

        #region EmployeeInformation
        public IList<UserEntity> GetEmployeeInformationList(string UserName, string MobileNumber, string NICNo, int RoleId)
        {
            try
            {
                var parameters = new object[] { UserName, MobileNumber, NICNo, RoleId };
                return EntityBase.FillCollection<UserEntity>("EmployeeInformationList", parameters);
            }
            catch (FinderException)
            {
                return new List<UserEntity>();
            }
        }
        public UserEntity GetEmployeeInformationById(long UserId)
        {
            try
            {
                return EntityBase.FillCollectionBySQLQuery<UserEntity>("SELECT TOP(1) * FROM [Users] WHERE [UserId]=" + UserId + "").FirstOrDefault();
            }
            catch (FinderException)
            {
                return new UserEntity();
            }
        }

        public OperationStatusEntity UpdateEmployeeInformation(UserEntity param)
        {
            var parameters = new object[] 
                                            { 
                                                //param.UserId, param.CompanyId, param.UserName, param.EmployeeCode, param.NICNo, param.UserEmailId, param.MobileNumber, 
                                                //param.RoleId, param.LocationId,param.DesignationTypeId, param.ParmanentAddress, param.PresentAddress, param.PhotoPath, param.IsActive,
                                                //param.POCName, param.CNICNo, param.POCPhoneNo, param.POCMobileNo, param.POCParmanentAddress, param.POCPresentAddress, 
                                                //param.POCEmailId, param.POCRelation, param.POCRemark, param.CreatedBy
                                             };

            return EntityBase.FillObject<OperationStatusEntity>("EmployeeInformationUpdate", parameters);
        }
        public OperationStatusEntity UpdateUserLoginUpdate(UserEntity param)
        {
            var parameters = new object[] 
                                            { 
                                               // param.UserId, param.LoginId, param.UserPassword, param.IsActivateLogin, param.CreatedBy
                                            };

            return EntityBase.FillObject<OperationStatusEntity>("UserLoginUpdate", parameters);
        }
        public string UpdateUserPassword(string NewPassword, int UserId)
        {
            try
            {
                DataConnection.ExecuteSQLQuery("UPDATE [Users] SET [UserPassword] ='" + NewPassword + "', [UserLastPasswordChange] = GETDATE() WHERE [UserId] =" + UserId + "");
                return "success";
            }
            catch (BaseException be)
            {
                return be.DisplayMessage;
            }
        }

        #endregion
    }
}