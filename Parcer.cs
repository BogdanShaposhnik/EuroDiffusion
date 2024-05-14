using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroDiffusion
{
    class Parser
    {
        private const string DefaultPath = @"..\..\..\input.txt";

        public List<List<Country>> ReadInputFile(string path = DefaultPath)
        {
            var result = new List<List<Country>>();
            List<string> input = ReadFile(path);
            var endOfInputFlag = false;
            var caseFlag = false;
            int numberOfCountries = 0;
            List<Country> curList = null;
            int i = 0;

            foreach (var line in input)
            {
                var trimmedLine = line.Trim();

                if (trimmedLine == "")
                    continue;

                if (trimmedLine == "0")
                {
                    endOfInputFlag = true;
                    continue;
                }

                if (caseFlag)
                {
                    var args = line.Split(" ");
                    if (args.Length != 5)
                    {
                        throw new ArgumentException("wrong number of arguments in country line");
                    }

                    var name = args[0];
                    var xl = int.Parse(args[1]);
                    var yl = int.Parse(args[2]);
                    var xh = int.Parse(args[3]);
                    var yh = int.Parse(args[4]);
                    curList.Add(new Country(name, xl, yl, xh, yh, i));
                    i++;
                    if (--numberOfCountries == 0)
                    {
                        result.Add(curList);
                        caseFlag = false;
                    }
                }
                else
                {
                    if (trimmedLine.All(char.IsDigit))
                    {
                        numberOfCountries = int.Parse(line);
                        i = 0;
                    }
                    else
                    {
                        throw new ArgumentException("Wrong number of countries");
                    }
                        
                    curList = new List<Country>();
                    caseFlag = true;
                }


            }

            if (!endOfInputFlag)
            {
                throw new ArgumentException("No 0 at the end of file");
            }
                
            return result;
        }

        private List<string> ReadFile(string path)
        {
            using StreamReader file = new StreamReader(path);
            string line;
            var result = new List<string>();
            while ((line = file.ReadLine()) != null)
            {
                result.Add(line);
            }

            return result;
        }
    }  
}
