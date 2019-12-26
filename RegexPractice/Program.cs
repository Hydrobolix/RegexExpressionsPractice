using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;

namespace RegexPractice {
    class Program {
        static void Main(string[] args) {
            string[] inputs = {
                "--help",
                "--name SOME_NAME   --count 10",
                "--name --help",
                "",
                "--name1 world",
                "--nAme SOME_NAME_TO_LONG",
                "--count    73 --help",
                "--count 9",
                "--help name",
                "--count 44",
                "--count g",
                "--name abc --help",
                "--name 123 --Name good",
                "--count a",
                "--count 101",
                "--count",
                "--count -1",
                "--count --name",
                "sample",
                "--count 10 --unknown",
                "--count ten",
                "--help --unknown",
                "--help count",
                "--help --name",
                "--name 16",
            };

            Program p = new Program();

            
            int count = 0;
            foreach(string s in inputs)
                Console.WriteLine($"{++count}:  " + p.Solution(s));
            
            /*
            string countParam = @"\s*--\b(count)\s+[0-9]+\s*";

            Match m = Regex.Match("--count 99", countParam, RegexOptions.IgnoreCase);
            int s = Convert.ToInt32("--count 99".Remove(m.Index, 8));
            Console.WriteLine(s);
            */
        }

        public int Solution(string S) {
            string countParam = @"\s*--\b(count)\s+[0-9]+\s*";
            string nameParam = @"\s*--\b(name)\s+\w+\s*";
            string helpParam = @"\s*--\b(help)\s*";

            string subS = S;
            int valid = 0;
            int helpValid = -1;

            Match mCount = null;
            Match mName = null;
            Match mHelp = null;

            int steps = 0;

            if (subS.Length == 0) valid = -1;

            while (subS.Length > 0) {
                mCount = Regex.Match(subS, countParam, RegexOptions.IgnoreCase);
                if (mCount.Success) {
                    string num_SubS = subS.Substring(mCount.Index, mCount.Length);
                    num_SubS = num_SubS.Trim();
                    subS = subS.Remove(mCount.Index, mCount.Length);
                    
                    int num = Convert.ToInt32(num_SubS.Remove(0, 8));
                    if (num < 10 || num > 100) valid = -1;
                    //int num = Convert.ToInt32(num_subS.Remove(0, 8));
                    //valid = 0;
                }

                mName = Regex.Match(subS, nameParam, RegexOptions.IgnoreCase);
                if (mName.Success) {
                    subS = subS.Remove(mName.Index, mName.Length);
                    //valid = 0;
                }

                mHelp = Regex.Match(subS, helpParam, RegexOptions.IgnoreCase);
                if (mHelp.Success) {
                    subS = subS.Remove(mHelp.Index, mHelp.Length);
                    //valid = 0;
                    helpValid = 1;
                }

                if ((!mCount.Success && !mName.Success && !mHelp.Success) && steps == 0) {
                    valid = -1;
                }

                if (!mCount.Success && !mName.Success && !mHelp.Success) {
                    subS = "";
                }
            }


            if (helpValid == 1 && valid == 0) valid = 1;

            return valid;
        }
    }
}
