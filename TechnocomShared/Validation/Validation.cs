using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;

namespace TechnocomShared.Validation
{
    /// <summary>
    /// Summary description for Validation.
    /// </summary>
    public class Validation
    {
        /*This function will return the error message of corresponding to the code from web.config file
          Input : Array of string - Each element of the array is in the form of '<ErrorCode>-<ErrorSourcefield>'
          Output : String */

        private string GetErrorMsgByCode(IEnumerable<string> strErrSrcCode)
        {
            char[] separator = {'-'}; //separator character
            var strErrMsg = string.Empty;
            var strFilePath = HttpContext.Current.Server.MapPath("~/App_Data/ValidationMessages.xml");
            XmlDocument xDoc;
            if (File.Exists(strFilePath))
            {
                xDoc = new XmlDocument();
                xDoc.Load(strFilePath);

                foreach (var t in strErrSrcCode)
                {
                    var strErr = t.Split(separator, 2);
                    var xErrorList = xDoc.SelectNodes("//error[@key = '" + strErr[0] + "']");
                    strErrMsg = strErrMsg + xErrorList[0].Attributes["value"].Value.Replace("#$#", strErr[1]) + "<br>";
                }
            }
            return (strErrMsg);
        }

        /*This function will test for Alphabets.*/

        public bool IsAlpha(string strToCheck)
        {
            var objAlphaPattern = new Regex(@"([^a-zA-Z\s])+");
            return !objAlphaPattern.IsMatch(strToCheck);
        }

        private bool IsValidDate(string strDate)
        {
            bool bReturn = true;
            string strRegex = @"^([0-9]{2,2})/([0-9]{2,2})/([0-9]{4,4})$";
            var re = new Regex(strRegex);
            if (re.IsMatch(strDate))
            {
                try
                {
                    DateTime.Parse(strDate);
                }
                catch (Exception)
                {
                    bReturn = false;
                }
            }
            else
                bReturn = false;
            return (bReturn);
        }

        private bool CheckedDecimal(string strVal)
        {
            bool bReturn = false;
            string strRegex = @"^\d{0,5}(\.\d{1,2})?$";
            var re = new Regex(strRegex);
            if (re.IsMatch(strVal))
                bReturn = true;
            return (bReturn);
        }

        private bool CheckedDecimalRange(string strVal, int numPart, int decimalPart)
        {
            bool bReturn = false;
            string strRegex = @"^\d{0," + numPart + @"}(\.\d{1," + decimalPart + "})?$";
            var re = new Regex(strRegex);
            if (re.IsMatch(strVal))
                bReturn = true;
            return (bReturn);
        }

        private bool CheckedRealDecimalRange(string strVal, int numPart, int decimalPart)
        {
            bool bReturn = false;
            //string strRegex = @"^[-+]?[0-9]\d{0," + numPart.ToString() + @"}(\.\d{1," + decimalPart.ToString() + "})?$";
            //string strRegex = @"^[-+]^\d{0," + numPart.ToString() + @"}(\.\d{1," + decimalPart.ToString() + "})?$";
            string strRegex = @"^[-+]?\d{0," + numPart + @"}(\.\d{1," + decimalPart + "})?$";
            var re = new Regex(strRegex);
            if (re.IsMatch(strVal))
                bReturn = true;
            return (bReturn);
        }

        /*This function will check whether the date is in dd/mm/yyyy format if not empty --*/

        public bool IsEmptyAndValidDate(string strFldVal, string strFldName, ref string strTemp)
        {
            bool bReturn = true;
            if (strFldVal != "")
            {
                if (!IsValidDate(strFldVal))
                {
                    strTemp = strTemp + "016" + "-" + strFldName + "~";
                    bReturn = false;
                }
                else
                {
                    if (DateTime.Parse(strFldVal).Date < DateTime.Parse("01/01/1900").Date)
                    {
                        strTemp = strTemp + "051" + "-" + strFldName + "~";
                        bReturn = false;
                    }
                }
            }
            else
            {
                strTemp = strTemp + "001" + "-" + strFldName + "~";
                bReturn = false;
            }
            return (bReturn);
        }

        public bool IsMoreDate(string strFldVal, string strFldNm, ref string strTemp)
        {
            bool bReturn = true;

            if (strFldVal != "")
            {
                DateTime dt = DateTime.Now;
                DateTime dtGiven = Convert.ToDateTime(strFldVal);
                if (dtGiven > dt)
                {
                    strTemp = strTemp + "013" + "-" + strFldNm + "~";
                    bReturn = false;
                }
            }
            else
            {
                strTemp = strTemp + "001" + "-" + strFldNm + "~";
                bReturn = false;
            }
            return (bReturn);
        }

        public bool CompareDate(string strFromDt, string strToDt, ref string strTemp)
        {
            bool bReturn = true;

            DateTime dtFrom = Convert.ToDateTime(strFromDt);
            DateTime dtTo = Convert.ToDateTime(strToDt);
            if (dtFrom > dtTo)
            {
                strTemp = strTemp + "052" + "-" + "~";
                bReturn = false;
            }
            return (bReturn);
        }

        public bool ChkPostCode(string strFldVal, string strFldNm, ref string strTemp)
        {
            bool bReturn = true;
            if (strFldVal.Trim() != "")
            {
                var objPattern = new Regex(@"^\s*([\w\s]{1,})\s*$");
                if (!objPattern.IsMatch(strFldVal))
                {
                    strTemp = strTemp + "003" + "-" + strFldNm + "~";
                    bReturn = false;
                }
            }
            else
            {
                strTemp = strTemp + "001" + "-" + strFldNm + "~";
                bReturn = false;
            }
            return (bReturn);
        }

        /// <summary>
        /// validate field as per the regex 
        /// </summary>
        /// <param name="strFldVal"></param>
        /// <param name="strFldNm"></param>
        /// <param name="strRegex"></param>
        /// <param name="strTemp"></param>
        /// <returns></returns>
        public bool ValidateField(string strFldVal, string strFldNm, string strRegex, ref string strTemp)
        {
            bool bReturn = true;
            if (strFldVal.Trim() != "")
            {
                var objPattern = new Regex(strRegex);
                if (!objPattern.IsMatch(strFldVal))
                {
                    strTemp = strTemp + "001" + "-" + strFldNm + "~";
                    bReturn = false;
                }
            }

            return (bReturn);
        }

        public bool ValidPostCode(string strFldVal, string strFldNm, ref string strTemp)
        {
            bool bReturn = true;
            var objPattern = new Regex(@"^\s*([\w\s]{1,})\s*$");
            if (!objPattern.IsMatch(strFldVal))
            {
                strTemp = strTemp + "003" + "-" + strFldNm + "~";
                bReturn = false;
            }
            return (bReturn);
        }

        public bool IsEmpty(string strFldVal, string strFldNm, ref string strTemp)
        {
            bool bReturn = true;
            if (strFldVal.Trim() == "")
            {
                strTemp = strTemp + "001" + "-" + strFldNm + "~";
                bReturn = false;
            }
            return (bReturn);
        }
    }
}