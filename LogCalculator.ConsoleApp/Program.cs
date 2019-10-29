using System;
using System.Collections.Generic;
using System.IO;

namespace LogCalculator.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = Calculate(new TimeSpan(0, 0, 10), new TimeSpan(0, 0, 1));
            if (File.Exists("result.csv"))
            {
                File.Delete("result.csv");
            }
            result.ForEach(result =>
            {
                File.AppendAllText("result.csv",$"{Math.Round(result.timePassed.TotalSeconds, MidpointRounding.ToZero)}, {result.currentValue}\n");
            });              
        }
        static List<LogCalculationResult> Calculate(TimeSpan period, TimeSpan logginPeriod)
        {
            var result = new List<LogCalculationResult>();
            var startTime = DateTime.UtcNow;
            var lastLogged = DateTime.UtcNow;
            long i = 1;
            while (DateTime.UtcNow.Subtract(startTime) < period)
            {
                unchecked
                {
                    Math.Log10(i);
                    Math.Pow(i, 2);
                    Math.Sqrt(i);
                    Math.Exp(i);
                }
                i++;
                Log();
            }
            void Log()
            {
                if (DateTime.UtcNow.Subtract(lastLogged) > logginPeriod)
                {
                    result.Add(new LogCalculationResult
                    {
                        currentValue = i,
                        timePassed = DateTime.UtcNow.Subtract(startTime)
                    });
                    lastLogged = DateTime.UtcNow;
                }

            }
            return result;
        }
    }
    class LogCalculationResult
    {
        public long currentValue { get; set; }
        public TimeSpan timePassed { get; set; }
    }
}
