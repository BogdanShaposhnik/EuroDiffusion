using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroDiffusion
{
    class Country
    {
        private const int MaxNameLength = 25;
        private const int MaxSize = 10;
        private const int MinSize = 1;

        public Country(string name, int xl, int yl, int xh, int yh, int countryNumber)
        {
            if (name.Length > MaxNameLength)
            {
                throw new ArgumentException("country name should be 25 chars at max");
            }

            if (xl > xh || yl > yh)
            {
                throw new ArgumentException("invalid country coordinates");
            }

            if (new[] { xl, yl, xh, yh }.ToList().Any(x => x < MinSize || x > MaxSize))
            {
                throw new ArgumentException("invalid range country coordinates");
            }

            XL = xl;
            XH = xh;
            YH = yh;
            YL = yl;
            Name = name;
            Motif = countryNumber;
            NumberOfDaysToComplete = 0;
            EachTownHasAllMotifs = false;
        }

        public int XL { get; set; }
        public int YL { get; set; }
        public int XH { get; set; }
        public int YH { get; set; }
        public string Name { get; set; }
        public int Motif { get; set; }
        public int NumberOfDaysToComplete { get; set; }
        public bool EachTownHasAllMotifs { get; set; }

        public bool IsEmpty()
        {
            return Name == "" || Motif < 0;
        }

        public void SetNumberOfDaysToComplete(int days)
        {
            if (NumberOfDaysToComplete < days)
            {
                NumberOfDaysToComplete = days;
            }
        }
    }
}
