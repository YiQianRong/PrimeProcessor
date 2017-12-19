using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrimeSolution
{
    public class PrimeProcessor
    {
        //Problem 35196 Prime Triplets
        //NP problem
        public static List<long> PrimeTriplets(long n)
        {
            var primeTriplets = new Dictionary<long, List<long>>();
            List<long> primes = new List<long>();
            primes.Add(2);
            primes.Add(3);
            primes.Add(5);

            long lastNumber = 1;
            var rowList = new List<long>();
            long len = primeTriplets.Count + 1;

            for (long i = 1; primeTriplets.Count < n; i++)
            {
                if (rowList.Count == 0)
                {
                    rowList.Add(lastNumber);
                }

                bool isPrime = true;
                foreach (long prime in primes)
                {
                    if (i <= prime) continue;

                    isPrime = isPrime && (i % prime) != 0;
                }

                if (isPrime)
                {
                    if (!primes.Contains(i) && i > 1)
                    {
                        primes.Add(i);
                    }

                    if (i <= lastNumber)
                    {
                        rowList.Add(i);
                    }else
                    {
                        primeTriplets.Add(len, rowList);
                        len++;
                        lastNumber = rowList[0] + len;
                        rowList = new List<long>();
                        rowList.Add(lastNumber);
                        rowList.Add(i);
                    }
                }

            }

            var nList = primeTriplets[n];
            return nList;
        }

        //Problem 35 Circular primes
        //O(n * Log(n))
        public static List<long> CircularPrimes(long n)
        {
            var circularPrimeList = new List<long>();
            var primes = PrimeList(n);

            circularPrimeList.Add(2);
            circularPrimeList.Add(3);
            circularPrimeList.Add(5);
            circularPrimeList.Add(7);
            circularPrimeList.Add(11);
            circularPrimeList.Add(13);
            circularPrimeList.Add(17);
            circularPrimeList.Add(31);
            circularPrimeList.Add(37);
            circularPrimeList.Add(71);
            circularPrimeList.Add(73);
            circularPrimeList.Add(79);
            circularPrimeList.Add(97);

            foreach(var prime in primes)
            {
                if (prime < 100)
                    continue;

                string strNumber = prime.ToString();
                List<long> numberList = All_Rotations(strNumber);

                bool isContain = true;
                foreach(long rotation in numberList)
                {
                    isContain = isContain && primes.Contains(rotation);
                }

                if(isContain)
                {
                    foreach (var number in numberList)
                    {
                        if (!circularPrimeList.Contains(number))
                        {
                            circularPrimeList.Add(number);
                        }
                    }
                }
            }

            return circularPrimeList.OrderBy(p => p).ToList();
        }

        public static List<long> All_Rotations(string strNumber)
        {
            List<long> numbers = new List<long>();
            numbers.Add(long.Parse(strNumber));
            int firstLoop = strNumber.Length;
            for(int i = 0; i< firstLoop; i++)
            {
                for (int j = i+1; j< firstLoop; j++)
                {
                    StringBuilder rotation = new StringBuilder(strNumber);
                    char firstByte = rotation[i];
                    rotation[i] = rotation[j];
                    rotation[j] = firstByte;
                    numbers.Add(long.Parse(rotation.ToString()));
                }

            }
            return numbers;
        }

        //Problem 3 Largest prime factor projecteuler.net/problem=3
        //O(n * Log(n))
        public static List<long> LargestPrimeFactor(long n)
        {
            var largestPrimeFactor = new List<long>();
            long printProduct = n;
            bool isPrime = true;

            List<long> primes = new List<long>();

            for (long i = 2; i <= n; i++)
            {
                isPrime = true;
                foreach (long prime in primes)
                {
                    isPrime = isPrime && (i % prime) != 0;
                }

                if (isPrime)
                {
                    long prime = i;
                    primes.Add(prime);
                    bool isPrimeFactor = (printProduct % prime) == 0;

                    if (isPrimeFactor && printProduct > 1)
                    {
                        printProduct = printProduct / prime;

                        largestPrimeFactor.Add(prime);
                        n = printProduct;

                        if (printProduct < i)
                        {
                            break;
                        }
                    }
                }
            }

            return largestPrimeFactor;
        }

        //O(n * Log(n))
        public static List<long> PrimeList(long n)
        {
            List<long> primes = new List<long>();
            primes.Add(2);
            primes.Add(3);
            primes.Add(5);
            primes.Add(7);
            primes.Add(11);
            primes.Add(13);
            primes.Add(17);
            primes.Add(19);
            primes.Add(23);
            primes.Add(29);
            primes.Add(31);
            primes.Add(37);
            primes.Add(41);
            primes.Add(43);
            primes.Add(47);
            primes.Add(53);
            primes.Add(59);
            primes.Add(61);

            for (long i = 62; i < n; i++)
            {
                bool isPrime = true;
                foreach (long prime in primes)
                {
                    isPrime = isPrime && (i % prime) != 0;
                }

                if (isPrime)
                {
                    primes.Add(i);
                }
            }

            return primes;
        }

        //It is NP problem
        public static List<long> The_nth_Prime(long n)
        {
            List<long> primes = new List<long>();
            primes.Add(2);
            primes.Add(3);
            primes.Add(5);
            primes.Add(7);
            primes.Add(11);
            primes.Add(13);
            primes.Add(17);
            primes.Add(19);
            primes.Add(23);
            primes.Add(29);
            primes.Add(31);
            primes.Add(37);
            primes.Add(41);
            primes.Add(43);
            primes.Add(47);
            primes.Add(53);
            primes.Add(59);
            primes.Add(61);

            for (long i = 62; primes.LongCount() < n; i++)
            {
                bool isPrime = true;
                foreach (long prime in primes)
                {
                    isPrime = isPrime && (i % prime) != 0;
                }

                if (isPrime)
                {
                    primes.Add(i);
                }
            }

            return primes;
        }
    }
}
