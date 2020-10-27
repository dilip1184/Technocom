using System.Collections.Generic;

namespace TechnocomShared.Constants
{
    public class ErrorDescription
    {
        public const string L006 = "Invalid Username/Password";
        public const string NoRowSelectedinGridView = "Please select at least one {0}.";
        public const string NoResultFound = "No Result Found. Please search again.";

        public const string Empty = "Please enter #$#.";
        public const string InvalidSelect = "Please select #$#.";
        public const string InvalidAlphabet = "Please check your details, only alphabets are recognised in #$#.";
        public const string InvalidAlphaNumeric = "Please check your details, do not use any special characters in #$#.";
        public const string InvalidNumeric = "Please check your details, only numbers are recognised in #$#.";
        public const string InvalidAlphaNumericWithSpecialChars = "Please check your details, only [£,&] special characters are recognised in #$#.";
        public const string InvalidAlphaNumericWithSemiColon = "Please check your details, only semicolon [;] is recognised as special characters in #$#.";
        public const string InvalidDate = "Please enter a valid #$#.";
        public const string InvalidEmail = "Please enter a valid email address.";
        public const string InvalidPhone = "Please enter a valid phone number.";
        public const string InvalidNumericWithDecimal = "Please enter a valid #$#.";
        public const string InvalidPostcode = "Please enter a valid postcode.";
        public const string InvalidPassword = "Please enter a valid password min 8 to max 12 Alphanumeric with 1 special character."; 
    }

    public static class ErrorCodeDescription
    {
        private static readonly Dictionary<string, string> CodeDescription;

        static ErrorCodeDescription()
        {
            //Intialize the dictionary object
            CodeDescription = new Dictionary<string, string>();

            var obj = new ErrorDescription();
            var type = obj.GetType();
            var field = type.GetFields();
            foreach (var t in field)
            {
                CodeDescription.Add(t.Name, t.GetValue(obj).ToString());
            }
        }

        public static string GetErrorDescription(string errorCode)
        {
            return CodeDescription[errorCode];
        }
    }
}