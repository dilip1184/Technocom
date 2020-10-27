using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using TechnocomShared.Entities;
using TechnocomControl;
using System;

namespace TechnocomWeb
{
    public class SessionClass
    {
        public static UserEntity LoginUserEntity
        {
            get
            {
                if (HttpContext.Current.Session["LoginUserEntity"] != null)
                    return (UserEntity)(HttpContext.Current.Session["LoginUserEntity"]);
                else
                    return null;
            }
            set
            {
                HttpContext.Current.Session["LoginUserEntity"] = value;
            }
        }
        public static IList<MenuEntity> UserMenuList
        {
            get
            {
                if (HttpContext.Current.Session["UserMenuList"] != null)
                    return ((IList<MenuEntity>)(HttpContext.Current.Session["UserMenuList"]));
                else
                    return null;
            }
            set
            {
                HttpContext.Current.Session["UserMenuList"] = value;
            }
        }
        public static int FinancialYearId
        {
            get
            {
                if (HttpContext.Current != null && HttpContext.Current.Session != null && HttpContext.Current.Session["FinancialYearId"] != null)
                    return Convert.ToInt32(HttpContext.Current.Session["FinancialYearId"]);
                else
                    return 0;
            }
            set
            {
                HttpContext.Current.Session["FinancialYearId"] = value;
            }
        }
        public static string CompanyLogoURL
        {
            get
            {
                if (HttpContext.Current != null && HttpContext.Current.Session != null && HttpContext.Current.Session["CompanyLogoURL"] != null)
                    return Convert.ToString(HttpContext.Current.Session["CompanyLogoURL"]);
                else
                    return "/Content/images/Securitaslogo.png";
            }
            set
            {
                HttpContext.Current.Session["CompanyLogoURL"] = value;
            }
        }
    }
}

