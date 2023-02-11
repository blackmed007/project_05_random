using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ProjectFive
{
    internal class Program
    {
        public static int Seed = 0;
        public static Random random = new Random();

        static void Main(string[] args)
        {
            List<int> HistoryLCG = new List<int>();
            List<int> HistoryXOR = new List<int>();
            List<int> HistoryRand = new List<int>();

            for (int i = 0; i < 10; i++)
            {
                HistoryLCG.Add(0);
                HistoryXOR.Add(0);
                HistoryRand.Add(0);
            }
            for (int i = 0; i < 100000; i++)
            {
                long randomValue = NextLCG(0, 10);
                long randomValue2 = NextXorshift32(0, 10);
                long randomValue3 = NextRandom(0, 10);

                HistoryLCG[(int)randomValue]++;
                HistoryXOR[(int)randomValue2]++;
                HistoryRand[(int)randomValue3]++;
            }

            Console.WriteLine("\nLCG");
            for (int i = 0; i < 10; i++)
            {
                Console.Write(i + ": ");
                for (int j = 0; j < HistoryLCG[i] / 1000; j++)
                {
                    Console.Write("#");
                }
                Console.WriteLine(" " + (double)HistoryLCG[i] / 1000 + "%");
            }

            Console.WriteLine("\nXorshift32");
            for (int i = 0; i < 10; i++)
            {
                Console.Write(i + ": ");
                for (int j = 0; j < HistoryXOR[i] / 1000; j++)
                {
                    Console.Write("#");
                }
                Console.WriteLine(" " + (double)HistoryXOR[i] / 1000 + "%");
            }

            Console.WriteLine("\nRandom");
            for (int i = 0; i < 10; i++)
            {
                Console.Write(i + ": ");
                for (int j = 0; j < HistoryRand[i] / 1000; j++)
                {
                    Console.Write("#");
                }
                Console.WriteLine(" " + (double)HistoryRand[i] / 1000 + "%");
            }
        }

        public static long NextLCG(long minValue, long maxValue)
        {
            // Initialize the multiplier
            long multiplier = 1103515245;

            // Initialize the increment
            long increment = 12345;

            // Initialize the modulus
            long modulus = 2147483648;

            // Calculate the next random number
            long nextRandomNumber = (multiplier * Seed + increment) % modulus;

            // increment seed
            Seed++;

            // Calculate the next random number between the min and max values
            long nextRandomNumberBetweenMinAndMax = minValue + (nextRandomNumber % (maxValue - minValue));

            // Return the absolute next random number between the min and max values
            // calculate the absolute manually without using Math.Abs
            return nextRandomNumberBetweenMinAndMax < 0 ? -nextRandomNumberBetweenMinAndMax : nextRandomNumberBetweenMinAndMax;
        }

        public static long NextRandom(long minValue, long maxValue)
        {
            // use built in random number generator
            return random.Next((int)minValue, (int)maxValue);
        }

        public static long NextXorshift32(long minValue, long maxValue)
        {
            // Calculate the next random number
            long nextRandomNumber = Seed ^ (Seed << 13);
            nextRandomNumber = nextRandomNumber ^ (nextRandomNumber >> 17);
            nextRandomNumber = nextRandomNumber ^ (nextRandomNumber << 5);

            // Calculate the next random number between the min and max values
            long nextRandomNumberBetweenMinAndMax = minValue + (nextRandomNumber % (maxValue - minValue));

            // calculate the absolute manually without using Math.Abs
            return nextRandomNumberBetweenMinAndMax < 0 ? -nextRandomNumberBetweenMinAndMax : nextRandomNumberBetweenMinAndMax;
        }
    }
}
