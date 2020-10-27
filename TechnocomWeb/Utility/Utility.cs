using TechnocomShared.Entities;
using TechnocomService;
using TechnocomControl;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace TechnocomWeb
{
    public class Utility
    {
        public static CloudBlobContainer GetStorageContainer()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference(ConfigurationManager.AppSettings["StorageFolder"].ToString());
            if (container.CreateIfNotExists())
            {
                // configure container for public access
                var permissions = container.GetPermissions();
                permissions.PublicAccess = BlobContainerPublicAccessType.Container;
                container.SetPermissions(permissions);
            }
            return container;
        }

        public static string FirstLetterToUpper(string s)
        {
            return new System.Globalization.CultureInfo("en-US", false).TextInfo.ToTitleCase(s.ToLower());
        }
        public static string UppercaseFirstEach_RestLowerCase(string s)
        {
            char[] a = s.ToLower().ToCharArray();

            for (int i = 0; i < a.Count(); i++)
            {
                a[i] = i == 0 || a[i - 1] == ' ' ? char.ToUpper(a[i]) : a[i];

            }

            return new string(a);
        }
        public static string GetModifiedString(string strValue)
        {
            return strValue.Replace("—", "-").Replace("&", "and").Replace("'", "").Replace("< >", " ").Replace("<>", " ").Replace("<", " ").Replace(">", " ").Replace("[", " ").Replace("]", " ").Trim();
        }
        public static string Base64Encode(string txtToEncode)
        {
            byte[] bt = Encoding.ASCII.GetBytes(txtToEncode);
            txtToEncode = Convert.ToBase64String(bt);
            return txtToEncode;
        }
        public static string Base64Decode(string txtToDecode)
        {
            txtToDecode = Regex.Replace(txtToDecode, @"\s+", "");
            if ((txtToDecode.Length == 4) || (txtToDecode.Length % 4) == 0)
            {
                byte[] bt = Convert.FromBase64String(txtToDecode);
                txtToDecode = Encoding.ASCII.GetString(bt);
            }
            return txtToDecode;
        }
        public static string GetString(object objValue)
        {
            try
            {
                return Convert.ToString(objValue).Trim();
            }
            catch
            {
                return string.Empty;
            }
        }
        public static bool GetBool(object objValue)
        {
            try
            {
                return bool.Parse(objValue.ToString());
            }
            catch
            {
                return false;
            }
        }
        public static int GetInt(object objValue)
        {
            try
            {
                return int.Parse(objValue.ToString());
            }
            catch
            {
                return 0;
            }
        }
        public static double GetDouble(object objValue)
        {
            try
            {
                return double.Parse(objValue.ToString());
            }
            catch
            {
                return 0;
            }
        }
        public static long GetLong(object objValue)
        {
            try
            {
                return long.Parse(objValue.ToString());
            }
            catch
            {
                return 0;
            }
        }
        public static float GetFloat(object objValue)
        {
            try
            {
                return float.Parse(objValue.ToString());
            }
            catch
            {
                return 0;
            }
        }
        public static decimal GetDecimal(object objValue)
        {
            try
            {
                return decimal.Parse(objValue.ToString());
            }
            catch
            {
                return 0;
            }
        }
        public static void SetLookupSelectedText(ctlDropDownList ctlDropDownList, string strSelectedText)
        {
            for (int index = 0; index <= ctlDropDownList.Items.Count - 1; index++)
            {
                if (ctlDropDownList.Items[index].Text.ToLower().Trim() == strSelectedText.ToLower())
                {
                    ctlDropDownList.SelectedIndex = index;

                    break;
                }
            }
        }
        public static void SetLookupSelectedValue(ctlDropDownList ctlDropDownList, string strSelectedValue)
        {
            bool flag = false;

            for (int index = 0; index <= ctlDropDownList.Items.Count - 1; index++)
            {
                if (ctlDropDownList.Items[index].Value.ToLower().Trim() == strSelectedValue.ToLower().Trim())
                {
                    ctlDropDownList.SelectedIndex = index;

                    flag = true;

                    break;
                }
            }

            if (flag == false)
            {
                if (ctlDropDownList.SelectedIndex != -1)
                {
                    ctlDropDownList.SelectedIndex = 0;
                }
            }
        }
        public static void SetDropdownLookupSelectedValue(DropDownList ctlDropDownList, string strSelectedValue)
        {
            bool flag = false;

            for (int index = 0; index <= ctlDropDownList.Items.Count - 1; index++)
            {
                if (ctlDropDownList.Items[index].Value.ToLower().Trim() == strSelectedValue.ToLower().Trim())
                {
                    ctlDropDownList.SelectedIndex = index;

                    flag = true;

                    break;
                }
            }

            if (flag == false)
            {
                if (ctlDropDownList.SelectedIndex != -1)
                {
                    ctlDropDownList.SelectedIndex = 0;
                }
            }
        }
        public static string GetTechnocomCheckBoxListSelectedValue(ctlCheckBoxList chkList)
        {
            string strIds = string.Empty;
            string itemList = string.Empty;

            foreach (ListItem listItem in chkList.Items)
            {
                if (listItem.Selected && listItem.Value != "-1")
                {
                    itemList += listItem.Value + ",";
                }
            }

            if (itemList != "")
            {
                if (itemList.Substring(itemList.Length - 1) == ",")
                {
                    itemList = itemList.Substring(0, itemList.LastIndexOf(","));
                }
            }

            strIds += itemList;

            if (itemList == string.Empty)
            {
                return strIds.Replace(',', ' ');
            }

            if (strIds == "-1")
            {
                strIds = string.Empty;
            }

            return strIds;
        }
        public static string GetTechnocomCheckBoxListSelectedValue(ctlCheckBoxListPostBack chkList)
        {
            string strIds = string.Empty;
            string itemList = string.Empty;

            foreach (ListItem listItem in chkList.Items)
            {
                if (listItem.Selected && listItem.Value != "-1")
                {
                    itemList += listItem.Value + ",";
                }
            }

            if (itemList != "")
            {
                if (itemList.Substring(itemList.Length - 1) == ",")
                {
                    itemList = itemList.Substring(0, itemList.LastIndexOf(","));
                }
            }

            strIds += itemList;

            if (itemList == string.Empty)
            {
                return strIds.Replace(',', ' ');
            }

            if (strIds == "-1")
            {
                strIds = string.Empty;
            }

            return strIds;
        }
        public static string GetListBoxSelectedValue(ListBox listList)
        {
            string strIds = string.Empty;
            string itemList = string.Empty;

            foreach (ListItem listItem in listList.Items)
            {
                itemList += listItem.Value + ",";
            }

            if (itemList != "")
            {
                if (itemList.Substring(itemList.Length - 1) == ",")
                {
                    itemList = itemList.Substring(0, itemList.LastIndexOf(","));
                }
            }

            strIds += itemList;

            if (itemList == string.Empty)
            {
                return strIds.Replace(',', ' ');
            }

            return strIds;
        }
        public static string GetCheckBoxListSelectedValue(CheckBoxList chkList)
        {
            string strIds = string.Empty;
            string itemList = string.Empty;

            foreach (ListItem listItem in chkList.Items)
            {
                if (listItem.Selected)
                {
                    itemList += listItem.Value + ",";
                }
            }

            if (itemList != "")
            {
                if (itemList.Substring(itemList.Length - 1) == ",")
                {
                    itemList = itemList.Substring(0, itemList.LastIndexOf(","));
                }
            }

            strIds += itemList;

            if (itemList == string.Empty)
            {
                return strIds.Replace(',', ' ');
            }

            return strIds;
        }
        public static string GetCheckBoxListSelectedText(CheckBoxList chkList)
        {
            string strText = string.Empty;
            string itemList = string.Empty;

            foreach (ListItem listItem in chkList.Items)
            {
                if (listItem.Selected)
                {
                    itemList += listItem.Text + ", ";
                }
            }

            if (itemList != "")
            {
                if (itemList.Substring(itemList.Length - 1) == ", ")
                {
                    itemList = itemList.Substring(0, itemList.LastIndexOf(", "));
                }
            }

            strText += itemList;

            if (itemList == string.Empty)
            {
                return strText.Replace(',', ' ').Replace(' ', ' ');
            }

            return strText;
        }
    }
}