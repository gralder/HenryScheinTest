using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HenryScheinTestForms
{
    class ParseString
    {

        /*
       * This method will take in a CSV input string and return a formated string.
       * 
       * input: string  (“Patient Name”,”SSN”,”Age”,”Phone Number”,”Status”
       *       “Prescott, Zeke”,”542-51-6641”,21,”801-555-2134”,”Opratory=2,PCP=1”)
       *
       * return: string   ([Patient Name] [SSN] [Age] [Phone Number] [Status]
       *      [Prescott, Zeke] [542-51-6641] [21] [801-555-2134] [Opratory=2,PCP=1]
       *      
       */
        public static string ParseInputString(string input)
        {
            string[] lines;
            string[] words;
            string word;
            List<string> stringList = new List<string>();
            string output = "";
            string[] newWords;
            string tempStr;

            // break up input string too handle \n or \r\n
            lines = input.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            // loop through all lines 
            foreach (string line in lines)
            {
                words = null;
                // split line into words "“Patient Name"
                words = line.Split('”');
                stringList = new List<string>();

                // loop through the words removing null and ',' 
                foreach (string wordT in words)
                {
                    word = wordT.Replace("“", "");
                    if (!string.IsNullOrEmpty(word) && word.Length > 1)
                    {
                        if (word[0].Equals(',')) // check for age ',99,'
                        {
                            tempStr = word.Replace(",", "");
                            stringList.Add(tempStr.Trim());
                        }
                        else
                        {
                            stringList.Add(word.Trim());
                        }
                    }
                }

                newWords = stringList.ToArray();

                // append line to return string
                foreach (var wordT in newWords)
                {
                    output = output + $"[{wordT}] ";
                }
                output = output + "\r\n";
            }
            return output;
        }
    }
}

