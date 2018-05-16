using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using Quartz.Impl;
using System.Threading.Tasks;

namespace bashgah.Models
{
    public class jobScheduler
    {
        public static async Task Start()

        {

            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler =await factory.GetScheduler();

            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<jobclass>().Build();

            ITrigger trigger = TriggerBuilder.Create()

                    .WithIdentity("jobclass", "IDG")

                    .StartNow()

                    .WithSimpleSchedule(s => s

                    .WithIntervalInSeconds(10)

                    .RepeatForever())

                        .Build();

            await scheduler.ScheduleJob(job, trigger);

            // some sleep to show what's happening
            //await Task.Delay(TimeSpan.FromSeconds(5));

            // and last shut down the scheduler when you are ready to close your program
            //await scheduler.Shutdown();
        }

    }
}