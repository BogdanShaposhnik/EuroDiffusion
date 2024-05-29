using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroDiffusion
{
    class Processor
    {
        public Processor(List<Country> countries)
        {
            Countries = countries;
            NumberOfCountries = countries.Count;
            SetCities();
        }

        private const int MaxSize = 10;
        List<Country> Countries = new List<Country>();

        City[,] Cities = new City[MaxSize + 1, MaxSize + 1];
        int NumberOfCountries;

        public bool Run(int numberOfCase)
        {
            

            int day = 1;
            do
            {
                if (NumberOfCountries == 1)
                {
                    day = 0;
                    break;
                };

                for (int x = 1; x < MaxSize + 1; x++)
                {
                    for (int y = 1; y < MaxSize + 1; y++)
                    {
                        if (Cities[x, y] == null)
                        {
                            continue;
                        }

                        SendRepresentativeCoins(x, y, x + 1, y);
                        SendRepresentativeCoins(x, y, x, y + 1);
                        SendRepresentativeCoins(x, y, x - 1, y);
                        SendRepresentativeCoins(x, y, x, y - 1);
                    }
                }

                BureaucraticIssuesInTheEvening(day);
                day++;
            } while (!EachCityHasAllMotifs());

            Countries = Countries.OrderBy(c => c.NumberOfDaysToComplete).ToList();
            Console.WriteLine("Case Number " + numberOfCase);
            foreach (var country in Countries)
            {
                Console.WriteLine(country.Name + " " + country.NumberOfDaysToComplete);
            }

            return true;
        }

        private bool SetCities()
        {
            foreach (var c in Countries)
            {
                for (int x = c.XL; x <= c.XH; x++)
                {
                    for (int y = c.YL; y <= c.YH; y++)
                    {
                        Cities[x, y] = new City(x, y, c, NumberOfCountries);
                    }
                }
            }

            return true;
        }

        private void BureaucraticIssuesInTheEvening(int day)
        {
            for (int x = 1; x < MaxSize + 1; x++)
            {
                for (int y = 1; y < MaxSize + 1; y++)
                {
                    if (Cities[x, y] == null)
                    {
                        continue;
                    }

                    Cities[x, y].AccumulateCoins();
                    CheckIfDiffusionIsCompleted(x, y, day);
                }
            }
        }

        private void CheckIfDiffusionIsCompleted(int x, int y, int day)
        {
            if (Cities[x, y] == null || Cities[x, y].DiffusionCompleted || !Cities[x, y].HasAllMotifs())
            {
                return;
            }

            if (Countries.Any(c => c.Name == Cities[x, y].Country.Name))
            {
                foreach (var country in Countries)
                {
                    if (country.Name == Cities[x, y].Country.Name)
                    {
                        country.SetNumberOfDaysToComplete(day);
                        break;
                    }
                }
            }
            else
            {
                Countries.Add(Cities[x, y].Country);
                Countries.Last().SetNumberOfDaysToComplete(day);
            }

            Cities[x, y].DiffusionCompleted = true;
        }

        private bool SendRepresentativeCoins(int xFrom, int yFrom, int xTo, int yTo)
        {
            if (Cities[xFrom, yFrom] != null && Cities[xTo, yTo] != null)
            {
                Cities[xTo, yTo].SetRepresentativeCoins(
                    Cities[xFrom, yFrom].GetRepresentativeCoins());
                return true;
            }

            return false;
        }

        private bool EachCityHasAllMotifs()
        {
            foreach (City c in Cities)
            {
                if(c != null && !c.HasAllMotifs())
                {
                    return false;
                }
            }

            return true;
        }
    }
}
