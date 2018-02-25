using LogParser.LogFileReader.SearchStrategy;
using System.IO;

namespace LogParser.LogFileReader
{
    public class FileReader
    {
        private LineParser LineParser { get; set; }

        public string FileName { get; set; }

        public StrategySearch StrategySearch { get; set; }

        public FileReader(string fileName, StrategyEnum[] searchStrategies)
        {
            this.FileName = fileName;
            this.StrategySearch = new StrategySearch(searchStrategies);
        }

        public void ProcessLogFile()
        {
            var lines = File.ReadLines(this.FileName);

            foreach (var line in lines)
            {
                if (line.StartsWith('#'))
                {
                    this.ReadSettings(line);
                    continue;
                }
                else
                {
                    this.ProcessLine(line);
                }
            }
        }

        public void ReadSettings(string line)
        {
            if (line.StartsWith("#Fields"))
            {
                this.LineParser = new LineParser(line);
            }
        }

        public void ProcessLine(string line)
        {
            this.StrategySearch.ProcessSearchResult(this.LineParser.GetParsedValues(line));
        }
    }
}
