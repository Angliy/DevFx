using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using Quartz.Impl;
using Common.Logging;
using System.Threading;

namespace QuartzTest
{
    class Program
    {
        static void Main(string[] args)
        {

             
            ISchedulerFactory sf = new StdSchedulerFactory();
            IScheduler sched = sf.GetScheduler();

            IScheduler sched1 = sf.GetScheduler();

            JobDetail job = new JobDetail("job1","group1",typeof(SimpleJob));

            //DateTime runtime = TriggerUtils.GetEvenMinuteDate(DateTime.UtcNow);//下一分钟

            //DateTime runtime = TriggerUtils.GetNextGivenSecondDate(null, 15);//15秒后

            //每一秒运行一次 重复十次 开始时间为now 结束时间为null
            //SimpleTrigger trigger = new SimpleTrigger("trigger1", "gropp1", "job1", "group1", DateTime.UtcNow, null, 10, TimeSpan.FromSeconds(10));

              
            SimpleTrigger trigger = new SimpleTrigger("trigger1",
                                "gropp1",
                                DateTime.UtcNow,
                                null,
                                SimpleTrigger.RepeatIndefinitely,
                                TimeSpan.FromSeconds(1));
                               

            sched.ScheduleJob(job,trigger);

            LogManager.GetLogger(typeof(Program)).Info("开始循环,每10秒运行一次,重复10次");
          
            sched.Start();

            //Thread.Sleep(90 * 1000);


        }
    }
}
