using LogParser.LogFileReader;
using LogParser.LogFileReader.SearchStrategy;
using System;

namespace LogParser
{
    class Program
    {
        static void Main(string[] args)
        {
            StrategyEnum[] searchStrategies = new StrategyEnum[] { StrategyEnum.DistinctClientIP, StrategyEnum.DistinctFqdnIP, StrategyEnum.CountClientCalls };
            FileReader fileReader = new FileReader("IISLog.log", searchStrategies);

            fileReader.ProcessLogFile();

            PrintResult(fileReader.StrategySearch.Result);
        }


        static void PrintResult(StrategyResult result)
        {
            Console.WriteLine("Distinct Client Ips:");
            foreach (var res in result.DistinctClientIPResult)
            {
                Console.WriteLine(res);
            }

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Distinct FQDN Ips of the client");
            foreach (var res in result.DistinctFqdnIPResult)
            {
                Console.WriteLine(res.Key);
                foreach (var val in res.Value)
                {
                    Console.WriteLine($"-- {val}");
                }
            }

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Count calls per client:");
            foreach (var res in result.CountClientCallsResult)
            {
                Console.WriteLine($"{res.Key} -- {res.Value}");
            }
        }
    }
}
