using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Code4YeouidoWebService.Helpers
{
    public class ScheduleRegistry : Registry
    {
        public ScheduleRegistry()
        {
            // Schedule an ITask to run at an interval
            //Schedule<MyTask>().ToRunNow().AndEvery(2).Seconds();

            //// Schedule an ITask to run once, delayed by a specific time interval. 
            //Schedule<MyTask>().ToRunOnceIn(5).Seconds();

            // Schedule a simple task to run at a specific time
            //Schedule(() => Console.WriteLine("Timed Task - Will run every day at 9:15pm: " + DateTime.Now)).ToRunEvery(1).Days().At(21, 15);

            Schedule(() => CrawlHelper.CrawlBillsWithDB()).ToRunEvery(1).Days().At(1, 0);
            Schedule(() => MailingHelper.메일발송()).ToRunEvery(1).Days().At(1, 0);
        }
    }
}