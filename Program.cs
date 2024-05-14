using Microsoft.VisualBasic;
using System.Diagnostics.Metrics;
using System.Drawing;
namespace EuroDiffusion;

class Program
{
    static void Main(string[] args)
    {
        Parser parser = new Parser();

        List<List<Country>> res = null;
        try
        {
            res = parser.ReadInputFile();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return;
        }

        int i = 1;
        foreach (List<Country> countryList in res)
        {
            Processor processor = new Processor(countryList);
            processor.Run(i);
            i++;
        }
    }
}

