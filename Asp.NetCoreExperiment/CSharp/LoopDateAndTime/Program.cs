using System;

namespace LoopDateAndTime
{
    class Program
    {
        static void Main(string[] args)
        {
            // 按天循环
            var beginDate = DateTime.Parse("2020-02-25");
            var endDate = DateTime.Parse("2020-03-10");
            LoopDate(beginDate, endDate);
            // 按小时循环
            var beginTime = DateTime.Parse("2020-02-25 19:00:00");
            var endTime = DateTime.Parse("2020-02-27 01:30:00");
            LoopTime(beginDate, endDate);

            Console.ReadLine();
        }
        /// <summary>
        /// 天循环
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        static void LoopDate(DateTime beginDate, DateTime endDate)
        {
            //循环日期，以一天为增量筏值
            for (var date = beginDate; date < endDate; date = date.AddDays(1))
            {
                Console.WriteLine(date);
            }
        }
        /// <summary>
        /// 小时循环
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        static void LoopTime(DateTime beginTime, DateTime endTime)
        {
            //循环日期，以一小时为增量筏值
            for (var date = beginTime; date < endTime; date = date.AddHours(1))
            {
                Console.WriteLine(date);
            }
        }
    }
}
