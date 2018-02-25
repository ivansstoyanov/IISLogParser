using System.Collections.Generic;

namespace LogParser.LogFileReader.SearchStrategy
{
    public class StrategyResult
    {
        public List<string> DistinctClientIPResult { get; set; }

        public Dictionary<string, List<string>> DistinctFqdnIPResult { get; set; }

        public Dictionary<string, int> CountClientCallsResult { get; set; }

        public StrategyResult()
        {
            this.DistinctClientIPResult = new List<string>();
            this.DistinctFqdnIPResult = new Dictionary<string, List<string>>();
            this.CountClientCallsResult = new Dictionary<string, int>();
        }
    }
}
