using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace HenryScheinTestForms
{
    /*
     * Test program: C# app that takes a string of CSV data as input and 
     * returns the data reformatted as shown below.
     *
     * Example input string:
     *   “Patient Name”,”SSN”,”Age”,”Phone Number”,”Status”
     *   “Prescott, Zeke”,”542-51-6641”,21,”801-555-2134”,”Opratory=2,PCP=1”
     *   “Goldstein, Bucky”,”635-45-1254”,42,”435-555-1541”,”Opratory=1,PCP=1”
     *   “Vox, Bono”,”414-45-1475”,51,”801-555-2100”,”Opratory=3,PCP=2”
     *

     * Return string for the above sample input 
     *   [Patient Name] [SSN] [Age] [Phone Number] [Status]
     *   [Prescott, Zeke] [542-51-6641] [21] [801-555-2134] [Opratory=2,PCP=1]
     *   [Goldstein, Bucky] [635-45-1254] [42] [435-555-1541] [Opratory=1,PCP=1]
     *   [Vox, Bono] [414-45-1475] [51] [801-555-2100] [Opratory=3,PCP=2]
     *
     * The system should function with both \r\n and \n carriage return, new line
     * configurations.
     *
     * Programmer:  Glen R. Alder
     * Date:        4/6/2019
     * 
     */
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string input = textBoxInput.Text;
            
            textBoxOutput.Text = ParseString.ParseInputString(input);
        }


        //====================================================================
        //                 METHODS
        //====================================================================


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
        static string xxParseInputString(string input)
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
