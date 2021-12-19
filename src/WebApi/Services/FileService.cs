using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace WebApi.Services
{
    public class FileService : IFileService
    {
        public string FileValidation(Stream stream)
        {
            List<string> invalidLines = new List<string>();
            using (StreamReader reader = new StreamReader(stream))
            {
                int linenumber = 1;
                while (!reader.EndOfStream)
                {
                    string Line = reader.ReadLine();
                    if (!string.IsNullOrEmpty(Line))
                    {
                        string Op = validation(Line, linenumber);
                        if (!string.IsNullOrEmpty(Op))
                            invalidLines.Add(Op);
                    }
                    linenumber++;
                }
            }

            dynamic json = new JObject();
            if (!invalidLines.Any())
                json.fileValid = true;
            else
            {
                json.fileValid = false;
                json.invalidLines = new JArray(invalidLines);
            }
            return JsonConvert.SerializeObject(json);
        }

        private string validation(string line, int linenumber)
        {
            string errorOP = string.Empty;
            string[] arr = line.Split(' ');
            if (arr.Length > 1)
            {
                bool AccountName = Regex.IsMatch(arr[0], @"^(\b[A-Z]\w*\s*)+$");
                bool AccountNumber = Regex.IsMatch(arr[1], @"^((3)([0-9]){6})$") ? true : Regex.IsMatch(arr[1], @"^(3)([0-9]){6}[pP]$");
                //if (!AccountNumber)
                //    bool AccountNumber = Regex.IsMatch(arr[1], @"^(3)([0-9]){6}[pP]$");
                if (!AccountName && !AccountNumber)
                    errorOP = "Account name,Account number-not valid for " + linenumber + " line '" + line + "'";
                else if (AccountName && !AccountNumber)
                    errorOP = "Account number-not valid for " + linenumber + " line '" + line + "'";
                else if (!AccountName && AccountNumber)
                    errorOP = "Account name-not valid for " + linenumber + " line '" + line + "'";
            }
            return errorOP;
        }
    }
}
