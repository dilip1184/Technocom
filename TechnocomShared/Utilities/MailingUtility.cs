using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Web.UI.WebControls;
using TechnocomShared.Configuration;
using TechnocomShared.Constants;
using TechnocomShared.Logging;
using System.Web;

namespace TechnocomShared.Utilities
{
    public class MailingUtility : IMailSender
    {
        private static readonly string FromAddress = AppConfigurationHelper.GetValue<string>(ConfigKeys.SmtpServerFromAddress);
        private static readonly string SmtpServer = AppConfigurationHelper.GetValue<string>(ConfigKeys.SmtpServerAddress);
        private static readonly int Port = AppConfigurationHelper.GetValue<int>(ConfigKeys.SmtpServerPort);
        private static readonly string Username = AppConfigurationHelper.GetValue<string>(ConfigKeys.SmtpServerUserName);
        private static readonly string Password = AppConfigurationHelper.GetValue<string>(ConfigKeys.SmtpServerPassword);
        private static readonly string Domain = AppConfigurationHelper.GetValue<string>(ConfigKeys.SmtpServerDomain);

        private static readonly bool UseCredentials =
            AppConfigurationHelper.GetValue<bool>(ConfigKeys.SmtpServerUseAuthentication);

        /// <summary>
        /// Sends the Email
        /// </summary>
        /// <param name="lstTo">List of To email addresses</param>
        /// <param name="strSubject">Subject of Email</param>
        /// <param name="strBody">Body of Email</param>
        /// <param name="isHtml">if set to <c>true</c> [is HTML].</param>
        public void Send(IList<string> lstTo, string strSubject, string strBody, bool isHtml = false)
        {
            try
            {
                if (!AppConfigurationHelper.GetValue<bool>(ConfigKeys.EnableAlerts)) return;

                LogWriter.GetLogWriter().Debug("Sending email to " + string.Join(",", lstTo.ToArray()) + " Subject:" + strSubject + " Body:" + strBody);
                var mailMessage = new MailMessage { };
                mailMessage.From = new MailAddress(FromAddress);
                foreach (string strTo in lstTo)
                    mailMessage.To.Add(new MailAddress(strTo));
                mailMessage.Subject = strSubject;
                mailMessage.Body = strBody;
                mailMessage.IsBodyHtml = isHtml;

                var smtp = new SmtpClient(SmtpServer, Port) { };

                if (UseCredentials) smtp.Credentials = new NetworkCredential(Username, Password, Domain);

                smtp.Send(mailMessage);
            }
            catch (Exception ex)
            {
                LogWriter.GetLogWriter().Exception(ex);
            }
        }

        /// <summary>
        /// Sends the Email
        /// </summary>
        /// <param name="strTo">To email address</param>
        /// <param name="strSubject">Subject of Email</param>
        /// <param name="strBody">Body of Email</param>
        /// <param name="isHtml">if set to <c>true</c> [is HTML].</param>
        public void Send(string strTo, string strSubject, string strBody, bool isHtml = false)
        {
            Send(new List<string> { strTo }, strSubject, strBody, isHtml);
        }

        public void Send(string subject, string body, ListDictionary replacements, bool isHtml, params string[] recipients)
        {
            try
            {
                if (!AppConfigurationHelper.GetValue<bool>(ConfigKeys.EnableAlerts))
                    return;

                LogWriter.GetLogWriter().Debug(string.Format("Sending email to {0} Subject:{1} Body:{2}", string.Join(";", recipients), subject, body));

                var md = new MailDefinition
                {
                    From = FromAddress,
                    Subject = subject,
                    IsBodyHtml = isHtml
                };

                var smtp = new SmtpClient(SmtpServer, Port);
                if (UseCredentials)
                    smtp.Credentials = new NetworkCredential(Username, Password, Domain);

                if (File.Exists(body))
                {
                    md.BodyFileName = body;
                    smtp.Send(md.CreateMailMessage(string.Join(";", recipients), replacements == null ? new ListDictionary() : replacements, new System.Web.UI.Control()));
                }
                else
                    smtp.Send(md.CreateMailMessage(string.Join(";", recipients), replacements == null ? new ListDictionary() : replacements, body, new System.Web.UI.Control()));
            }
            catch (Exception ex) { LogWriter.GetLogWriter().Exception(ex); }
        }
    }
}