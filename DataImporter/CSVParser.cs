using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataImporter
{
    public class CSVData
    {
        public Dictionary<string, int> Columns { get; set; }

        public List<CSVRow> Rows { get; set; }

        public CSVData()
        {
            Columns = new Dictionary<string, int>();
            Rows = new List<CSVRow>();
        }
    }

    public class CSVRow
    {
        public string[] Value { get; set; }
    }

    public static class CSVParser
    {
        private static Regex csvSplit = new Regex("(?:^|,)(\"(?:[^\"]+|\"\")*\"|[^,]*)", RegexOptions.Compiled);

        public static CSVData Parse(string csvString)
        {
            try
            {
                CSVData returnValue = new CSVData();
                var lines = (csvString.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)).ToList();
                returnValue.Columns = new Dictionary<string, int>();
                foreach (string s in SplitLine(lines[0]))
                {
                    returnValue.Columns.Add(s, returnValue.Columns.Count);
                }
                lines.RemoveAt(0);
                foreach (string line in lines)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                        returnValue.Rows.Add(new CSVRow() { Value = SplitLine(line) });
                }
                return returnValue;
            }
            catch (Exception e)
            {
                Console.WriteLine("Threw an exception in CSVParser.Parse" + e.Message);
            }
            return null;
        }

        private static string[] SplitLine(string lineToSplit)
        {
            try
            {
                lineToSplit = lineToSplit.Replace("&apos;", "'").Replace("&amp;", "&");
                var matches = csvSplit.Matches(lineToSplit);
                string[] returnValue = new string[matches.Count];
                for (int i = 0; i < matches.Count; i++)
                    returnValue[i] = matches[i].Value.TrimStart(',').Trim('"').Replace(@"""""",@"""");
                return returnValue;
            }
            catch (Exception e)
            {
                Console.WriteLine("Threw an exception in SplitLine: " + lineToSplit + e.Message);
            }
            return new string[0];
        }
    }
}
