using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01
{
    public static class Parser
    {
        public static Dictionary<string, string> Parse(string info)
        {
            string[] Fields = new string[] { "Name", "MiddleName", "Surname", "PhoneNumber", "Country", "DateOfBirth", "Organisation", "Position", "Marks" };
            List<string> SplitedInfo = new List<string>();
            SplitedInfo.AddRange(info.Split(new string[] { ", " }, StringSplitOptions.None));
            Dictionary<string, string> response = new Dictionary<string, string>();
            foreach (string s in SplitedInfo)
            {
                string[] KeyValue = s.Split(new string[] { ": " }, StringSplitOptions.None);
                if (Fields.Contains(KeyValue[0]))
                {
                    response.Add(KeyValue[0], KeyValue[1]);
                }
            }
            foreach (string field in Fields)
            {
                if (!response.ContainsKey(field))
                {
                    response.Add(field, "NS");
                }
            }
            return response;
        }
    }
}
