namespace TechnocomShared.Constants
{
    public class RegexConstants
    {
        public const string Alphabets =
            @"(^[a-zA-Z]+(([\'\,\.\-][a-zA-Z])?[a-zA-Z]*)*)|(^[a-zA-Z]+(([\'\,\.\-][a-zA-Z])?[a-zA-Z]*)*)[][a-zA-Z]+$";

        public const string AlphaNumeric = @"^[a-zA-Z0-9\s]+$";
        public const string Numeric = @"^([0-9]*)$";
        public const string AlphaNumericWithSpecialChars = @"([^a-zA-Z0-9-£&\s])+";
        public const string AlphaNumericWithSemiColon = @"^([a-zA-Z0-9;]*)$";

        public const string AlphaNumericWithDecimal = @"^([a-zA-Z0-9.]*)$";

        //public const string Date = @"^([0-9]{2,2})/([0-9]{2,2})/([0-9]{4,4})$";
        //public const string Date =
        //    @"^(((0?[1-9]|1[012])/(0?[1-9]|1\d|2[0-8])|(0?[13456789]|1[012])/(29|30)|(0?[13578]|1[02])/31)/(19|[2-9]\d)\d{2}|0?2/29/((19|[2-9]\d)(0[48]|[2468][048]|[13579][26])|(([2468][048]|[3579][26])00)))$";

        public const string Date = @"(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)";
       
        public const string Email =
            @"^(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*$";

        public const string Phone = @"^([0-9]{10})$";

        public const string NumericWithDecimal = @"^([0-9]*|\d*\.\d{1}?\d*)$";
        public const string Postcode = @"^\s*([\w\s]{1,})\s*$";

        public const string Password = @"^(?=.*[@*#$&!]+.*)(?=.*[0-9]+.*)(?=.*[a-zA-Z]+.*)[0-9a-zA-Z@*#$&!]{8,12}$";
    }
}