using System;
using System.Reflection;
using System.Text.RegularExpressions;
using TechnocomShared.Constants;

namespace TechnocomShared.Validation
{
    public class CustomValidation
    {
        public static string GenericValidation(string strControlText, string strControlCaption, bool bMandatory,
                                        string strValidationType, string strValidationErrorCode, string ControlType)
        {
            string strErrorMessage = string.Empty;

            if (bMandatory)
            {
                //code for mandatory check
                switch (ControlType)
                {
                    case "ctlTextBox":
                        if (strControlText.Trim() == string.Empty)
                        {
                            strErrorMessage = ErrorDescription.Empty + "<br/>";
                        }
                        break;
                    case "ctlTextArea":
                        if (strControlText.Trim() == string.Empty)
                        {
                            strErrorMessage = ErrorDescription.Empty + "<br/>";
                        }
                        break;
                    case "ctlDropDownList":
                        if (strControlText.Trim() == "0")
                        {
                            strErrorMessage = ErrorDescription.InvalidSelect + "<br/>";
                        }
                        break;

                    case "ctlCheckBoxList":
                        if (strControlText.Trim() == string.Empty)
                        {
                            strErrorMessage = ErrorDescription.InvalidSelect + "<br/>";
                        }
                        break;

                    case "ctlRadioButton":
                        if (strControlText.Trim().ToLower() == "false")
                        {
                            strErrorMessage = ErrorDescription.InvalidSelect + "<br/>";
                        }
                        break;
                }
            }

            if ((strValidationType != "0") && (strValidationErrorCode != "0"))
            {
                if ((strErrorMessage == string.Empty) && (strControlText.Trim() != string.Empty))
                {
                    //code for regex validations
                    var objRegexConstants = new RegexConstants();
                    Type objRegexType = objRegexConstants.GetType();
                    FieldInfo objFieldInfo = objRegexType.GetField(strValidationType);
                    string strRegex = objFieldInfo.GetValue(objRegexConstants).ToString();

                    //code for regex match and error generation
                    var objRegex = new Regex(strRegex);
                    if (!objRegex.IsMatch(strControlText.Trim()))
                    {
                        //code for generating error message for validation failure
                        var objErrorMessageConstants = new ErrorDescription();
                        Type objErrorType = objErrorMessageConstants.GetType();
                        FieldInfo objErrorFieldInfo = objErrorType.GetField(strValidationErrorCode);
                        strErrorMessage = objErrorFieldInfo.GetValue(objErrorMessageConstants) + "<br/>";
                    }
                }
            }
            return strErrorMessage.Replace("#$#", strControlCaption.Trim());
        }
    }
}