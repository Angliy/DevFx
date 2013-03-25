using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HTB.DevFx.Utils;


namespace DevFxTest
{
    public class JobExcute
    {

        CollectionBase<Question> questions = QuestionModule.GetQuestions();

        public void ExcuteJob()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╭︿︿︿╮");
            Console.WriteLine("{/-★★-/}");
            Console.WriteLine(" ( (oo) )");
            Console.WriteLine("  ︶︶︶");
            Console.WriteLine("请按0开始答题,Exc退出");
            while (true)
            {
                Wait();
            }
        }

        public void Wait()
        {
            ConsoleKeyInfo k; k = Console.ReadKey(true);
            switch (k.Key)
            {
                case ConsoleKey.D0:
                    Qustion();
                    break;
                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
        }

        public void Qustion()
        {
            Random ran = new Random();
            int i = ran.Next(0, questions.Count);
            Console.WriteLine(questions[i].Text);
            string answer = Console.ReadLine();
            if (answer == questions[i].Answer)
            {
                Console.WriteLine("答对了;按0继续");
                //QuestionModule.WriteLog(0, "回答正确");
            }
            else
            {
                Console.WriteLine("答错了;按0继续");
                //QuestionModule.WriteLog(1, "回答错误");
            }
        }
    }

}
