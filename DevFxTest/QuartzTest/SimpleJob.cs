using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Logging;
using Quartz;



namespace QuartzTest
{
    public class SimpleJob : IJob
    {
       
        private static ILog _log = LogManager.GetLogger(typeof(SimpleJob));

        public SimpleJob() { }

        public void Info(ConsoleColor c,string str)
        {
              Console.ForegroundColor =c;
              _log.Info(str);
              Console.ForegroundColor = ConsoleColor.Gray;
        }


        public virtual void Execute(JobExecutionContext context)
        {
            Info(ConsoleColor.Yellow,string.Format("测试日志 {0}", System.DateTime.Now.ToString("r")));
        }

    }

}