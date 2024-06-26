﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroDiffusion
{
    class City
    {
        private const int Motif = 1000000;

        public City(int x, int y, Country country, int countOfMotifs)
        {
            X = x;
            Y = y;
            Country = country;

            Coins = new int[countOfMotifs];
            AccumulatedCoins = new int[countOfMotifs];
            for (int i = 0; i < Coins.Length; i++)
            {
                Coins[i] = 0;
                AccumulatedCoins[i] = 0;
            }
            Coins[country.Motif] = Motif;

            DiffusionCompleted = false;
        }

        public int[] Coins { get; private set; }
        public int[] AccumulatedCoins { get; private set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Country Country { get; private set; }
        public bool DiffusionCompleted { get; set; }

        private const int m_representativeValue = 1000;

        public bool HasAllMotifs()
        {
            return Coins.All(x => x > 0);
        }

        public void AccumulateCoins()
        {
            for (int i = 0; i < Coins.Length; i++)
            {
                Coins[i] += AccumulatedCoins[i];
                AccumulatedCoins[i] = 0;
            }
        }

        public int[] GetRepresentativeCoins()
        {
            int[] representativeCoins = new int[Coins.Length];
            for (int i = 0; i < Coins.Length; i++)
            {
                representativeCoins[i] = Coins[i] / m_representativeValue;
                AccumulatedCoins[i] -= representativeCoins[i];
            }

            return representativeCoins;
        }

        public void SetRepresentativeCoins(int[] representativeCoins)
        {
            if (representativeCoins.Length != Coins.Length)
            {
                return;
            }

            for (int i = 0; i < Coins.Length; i++)
            {
                AccumulatedCoins[i] += representativeCoins[i];
            }
        }
    }
}
