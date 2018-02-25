using System;
using System.Collections.Generic;

namespace LogParser.LogFileReader.SearchStrategy
{
    public class StrategySearch
    {
        public StrategyEnum[] SearchStrategies { get; set; }

        public StrategyResult Result { get; set; }

        public StrategySearch(StrategyEnum[] searchStrategies)
        {
            this.SearchStrategies = searchStrategies;
            this.Result = new StrategyResult();
        }

        public void ProcessSearchResult(LineValues lineValues)
        {
            foreach (var strategy in this.SearchStrategies)
            {
                switch (strategy)
                {
                    case StrategyEnum.DistinctClientIP:
                        {
                            this.CalculateDistinctClientIPStrategy(lineValues.ClientIp);
                            break;
                        }
                    case StrategyEnum.DistinctFqdnIP:
                        {
                            this.CalculateDistinctFqdnIPStrategy(lineValues.ClientIp, lineValues.FqdnIp);
                            break;
                        }
                    case StrategyEnum.CountClientCalls:
                        {
                            this.CalculateCountClientCallsStrategy(lineValues.ClientIp);
                            break;
                        }
                    default:
                        throw new Exception("Insupported search strategy");
                }
            }
        }

        public void CalculateDistinctClientIPStrategy(string clientIp)
        {
            if (!this.Result.DistinctClientIPResult.Contains(clientIp))
            {
                this.Result.DistinctClientIPResult.Add(clientIp);
            }
        }

        public void CalculateDistinctFqdnIPStrategy(string clientIp, string fqdnIp)
        {
            if (!this.Result.DistinctFqdnIPResult.ContainsKey(clientIp))
            {
                this.Result.DistinctFqdnIPResult.Add(clientIp, new List<string>() { fqdnIp });
            }
            else
            {
                if (!this.Result.DistinctFqdnIPResult[clientIp].Contains(fqdnIp))
                {
                    this.Result.DistinctFqdnIPResult[clientIp].Add(fqdnIp);
                }
            }
        }

        public void CalculateCountClientCallsStrategy(string clientIp)
        {
            if (!this.Result.CountClientCallsResult.ContainsKey(clientIp))
            {
                this.Result.CountClientCallsResult.Add(clientIp, 1);
            }
            else
            {
                this.Result.CountClientCallsResult[clientIp] += 1;
            }
        }
    }
}
