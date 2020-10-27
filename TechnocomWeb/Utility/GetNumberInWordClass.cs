using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechnocomWeb
{
    public class GetNumberInWordClass
    {
        public static string ConvertNumberToWord(long nNumber)
        {
            long CurrentNumber = nNumber;
            string sReturn = string.Empty;

            //if (CurrentNumber >= 1000000000)
            //{
            //    sReturn += (" " + GetWord(CurrentNumber / 1000000000, "Billion"));
            //    CurrentNumber = CurrentNumber % 1000000000;
            //}
            if (CurrentNumber >= 100000)
            {
                sReturn += (" " + GetWord(CurrentNumber / 100000, "Lacs"));
                CurrentNumber = CurrentNumber % 100000;
            }
            if (CurrentNumber >= 1000)
            {
                sReturn += (" " + GetWord(CurrentNumber / 1000, "Thousand"));
                CurrentNumber = CurrentNumber % 1000;
            }
            if (CurrentNumber >= 100)
            {
                sReturn += (" " + GetWord(CurrentNumber / 100, "Hundred"));
                CurrentNumber = CurrentNumber % 100;
            }
            if (CurrentNumber >= 20)
            {
                sReturn += (" " + GetWord(CurrentNumber, ""));
                CurrentNumber = CurrentNumber % 10;
            }
            else if (CurrentNumber > 0)
            {
                sReturn += (" " + GetWord(CurrentNumber, ""));
                CurrentNumber = 0;
            }

            return sReturn.Trim();
        }

        private static string GetWord(long nNumber, string sPrefix)
        {
            long nCurrentNumber = nNumber;
            string sReturn = string.Empty;
            while (nCurrentNumber > 0)
            {
                if (nCurrentNumber > 100)
                {
                    sReturn = sReturn + " " + GetWord(nCurrentNumber / 100, "Hundred");
                    nCurrentNumber = nCurrentNumber % 100;
                }
                else if (nCurrentNumber >= 20)
                {
                    sReturn = sReturn + " " + GetTwentyWord(nCurrentNumber / 10);
                    nCurrentNumber = nCurrentNumber % 10;
                }
                else
                {
                    sReturn = sReturn + " " + GetLessThanTwentyWord(nCurrentNumber);
                    nCurrentNumber = 0;
                }
            }
            sReturn = sReturn.Trim() + " " + sPrefix;
            return sReturn;
        }

        private static string GetTwentyWord(long nNumber)
        {
            string sReturn = string.Empty;
            switch (nNumber)
            {
                case 2:
                    sReturn = "Twenty";
                    break;
                case 3:
                    sReturn = "Thirty";
                    break;
                case 4:
                    sReturn = "Forty";
                    break;
                case 5:
                    sReturn = "Fifty";
                    break;
                case 6:
                    sReturn = "Sixty";
                    break;
                case 7:
                    sReturn = "Seventy";
                    break;
                case 8:
                    sReturn = "Eighty";
                    break;
                case 9:
                    sReturn = "Ninety";
                    break;
            }
            return sReturn;
        }

        private static string GetLessThanTwentyWord(long nNumber)
        {
            string sReturn = string.Empty;
            switch (nNumber)
            {
                case 1:
                    sReturn = "One";
                    break;
                case 2:
                    sReturn = "Two";
                    break;
                case 3:
                    sReturn = "Three";
                    break;
                case 4:
                    sReturn = "Four";
                    break;
                case 5:
                    sReturn = "Five";
                    break;
                case 6:
                    sReturn = "Six";
                    break;
                case 7:
                    sReturn = "Seven";
                    break;
                case 8:
                    sReturn = "Eight";
                    break;
                case 9:
                    sReturn = "Nine";
                    break;
                case 10:
                    sReturn = "Ten";
                    break;
                case 11:
                    sReturn = "Eleven";
                    break;
                case 12:
                    sReturn = "Twelve";
                    break;
                case 13:
                    sReturn = "Thirteen";
                    break;
                case 14:
                    sReturn = "Forteen";
                    break;
                case 15:
                    sReturn = "Fifteen";
                    break;
                case 16:
                    sReturn = "Sixteen";
                    break;
                case 17:
                    sReturn = "Seventeen";
                    break;
                case 18:
                    sReturn = "Eighteen";
                    break;
                case 19:
                    sReturn = "Nineteen";
                    break;
            }
            return sReturn;
        }
    }
}