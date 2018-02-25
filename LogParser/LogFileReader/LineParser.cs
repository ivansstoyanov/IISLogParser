using System;

namespace LogParser.LogFileReader
{
    public class LineParser
    {
        public readonly char VALUES_SEPARATOR = ' ';

        public readonly string CLIENT_IP_VALUE = "c-ip";

        public readonly string FQDN_VALUE = "s-ip";

        public string[] LogParameters { get; set; }

        public LineParser(string line)
        {
            var logParameters = this.SplitLineToArray(line);
            this.LogParameters = new string[logParameters.Length];

            for (int i = 1; i < logParameters.Length; i++)
            {
                this.LogParameters[i - 1] = logParameters[i];
            }
        }

        public LineValues GetParsedValues(string line)
        {
            var lineValues = this.SplitLineToArray(line);

            return new LineValues()
            {
                ClientIp = this.GetClientIpValue(lineValues),
                FqdnIp = this.GetFqdnIpValue(lineValues)
            };
        }

        private string GetClientIpValue(string[] lineValues)
        {
            int logParametherIndex = this.FindLogParameterIndex(this.CLIENT_IP_VALUE);

            return lineValues[logParametherIndex];
        }

        private string GetFqdnIpValue(string[] lineValues)
        {
            int logParametherIndex = this.FindLogParameterIndex(this.FQDN_VALUE);

            return lineValues[logParametherIndex];
        }

        private string[] SplitLineToArray(string line)
        {
            return line.Split(this.VALUES_SEPARATOR);
        }

        private int FindLogParameterIndex(string value)
        {
            return Array.FindIndex(this.LogParameters, s => s == value);
        }
    }
}
