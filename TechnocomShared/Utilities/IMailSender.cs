using System.Collections.Generic;
using System.Collections.Specialized;
using TechnocomShared.Enums;

namespace TechnocomShared.Utilities
{
    internal interface IMailSender
    {
        void Send(IList<string> lstTo, string strSubject, string strBody, bool isHtml = false);

        void Send(string strTo, string strSubject, string strBody, bool isHtml = false);

        void Send(string subject, string body, ListDictionary replacements, bool isHtml, params string[] recipients);
    }
}