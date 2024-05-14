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
            SetTowns();
        }

        private const int MaxSize = 10;
        List<Country> Countries = new List<Country>();

        Town[,] Towns = new Town[MaxSize + 1, MaxSize + 1];
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
                        if (Towns[x, y] == null)
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
            } while (!EachTownHasAllMotifs());

            Countries = Countries.OrderBy(c => c.NumberOfDaysToComplete).ToList();
            Console.WriteLine("Case Number " + numberOfCase);
            foreach (var country in Countries)
            {
                Console.WriteLine(country.Name + " " + country.NumberOfDaysToComplete);
            }

            return true;
        }

        private bool SetTowns()
        {
            foreach (var c in Countries)
            {
                for (int x = c.XL; x <= c.XH; x++)
                {
                    for (int y = c.YL; y <= c.YH; y++)
                    {
                        Towns[x, y] = new Town(x, y, c, NumberOfCountries);
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
                    if (Towns[x, y] == null)
                    {
                        continue;
                    }

                    Towns[x, y].AccumulateCoins();
                    CheckIfDiffusionIsCompleted(x, y, day);
                }
            }
        }

        private void CheckIfDiffusionIsCompleted(int x, int y, int day)
        {
            if (Towns[x, y] == null || Towns[x, y].DiffusionCompleted || !Towns[x, y].HasAllMotifs())
            {
                return;
            }

            if (Countries.Any(c => c.Name == Towns[x, y].Country.Name))
            {
                foreach (var country in Countries)
                {
                    if (country.Name == Towns[x, y].Country.Name)
                    {
                        country.SetNumberOfDaysToComplete(day);
                        break;
                    }
                }
            }
            else
            {
                Countries.Add(Towns[x, y].Country);
                Countries.Last().SetNumberOfDaysToComplete(day);
            }

            Towns[x, y].DiffusionCompleted = true;
        }

        private bool SendRepresentativeCoins(int xFrom, int yFrom, int xTo, int yTo)
        {
            if (Towns[xFrom, yFrom] != null && Towns[xTo, yTo] != null)
            {
                Towns[xTo, yTo].SetRepresentativeCoins(
                    Towns[xFrom, yFrom].GetRepresentativeCoins());
                return true;
            }

            return false;
        }

        private bool EachTownHasAllMotifs()
        {
            for (int x = 1; x < MaxSize + 1; x++)
            {
                for (int y = 1; y < MaxSize + 1; y++)
                {
                    if (Towns[x, y] != null && !Towns[x, y].HasAllMotifs())
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
