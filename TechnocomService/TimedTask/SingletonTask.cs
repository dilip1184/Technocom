using System;
using System.Timers;
using TechnocomService.Audit;
using TechnocomService.SessionManagement;
using TechnocomShared.Configuration;
using TechnocomShared.Constants;
using TechnocomShared.Enums;
using TechnocomShared.Exceptions;

namespace TechnocomService.TimedTask
{
    public sealed class SingletonTask
    {
        private static readonly SingletonTask instance = new SingletonTask();
        private static bool _timerInitialised;
        private readonly int _intervalInMinutes = Convert.ToInt32(AppConfigurationHelper.GetValue<string>(ConfigKeys.SessionClearingTime));
        //private readonly int intervalInMinutes = 1;

        private SingletonTask() 
        { 
        }

        public static SingletonTask Instance
        {
            get
            {
                return instance;
            }
        }

        public void InitTimer()
        {
            if (_timerInitialised)
                return;

            InitTimerForClearingAbandonedSessions();

            _timerInitialised = true;//Only do this once in the application:
        }

        private void InitTimerForClearingAbandonedSessions()
        {
            var clearingAbandonedSessionsTimer = new Timer {Interval = (_intervalInMinutes*60000)};
            //int intervalInMinutes = Convert.ToInt32(AppConfigurationHelper.GetValue<string>(ConfigKeys.SessionTimeout));
            clearingAbandonedSessionsTimer.Elapsed += ClearingAbandonedSessionsTimer_Elapsed;
            clearingAbandonedSessionsTimer.Start();
        }

        private void ClearingAbandonedSessionsTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var errorMsg = String.Empty;
            try
            {
                //int intervalInMinutes = Convert.ToInt32(AppConfigurationHelper.GetValue<string>(ConfigKeys.SessionTimeout));
                //Set the delay in emailing people to the same as this timer interval (so if this timer runs every 30 mins, people will be emailed if they've left a quote alone for 30 mins):
                //string methodNameAndParams = String.Format("/EmailAbandonedQuickQuotes?delayInMinutes={0}", intervalInMinutes);
                //WebRequest webRequest = HttpWebRequest.Create(Properties.Settings.Default.HobNobClubApps_SLHobNobInsurance_HobNobInsurance + methodNameAndParams);
                //WebResponse webResponse = webRequest.GetResponse();
                
                var objUserSession = SessionManager.DeleteSessionAndLockedData(_intervalInMinutes);
                if (objUserSession != null)
                {
                    foreach(var userSession in objUserSession)
                    {
                        SessionManager.LogOff(userSession.UserId, userSession.Sessionid);
                    }
                }
            }
            catch (BusinessException ex)
            {
                errorMsg = ex.DisplayMessage;
            }
            AuditLogger.LogActivity
                                (
                                    "Technocom-System",
                                    DateTime.Now,
                                    ScreenActivityType.Create,
                                    0,
                                    String.Format("The ClearingAbandonedSessionsTimer ran at {0} {1}", DateTime.Now, String.IsNullOrEmpty(errorMsg) ? "with no errors (from the timer)." : "and failed with: " + errorMsg),
                                    -1,
                                    -1
                                );
        }
    }
}
