using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrimeSolution
{
    public class TripletPrime
    {
        public long Prime { set; get; }
        public int TripletID { set; get; }
        public int VertexID { set; get; }

        public TripletPrime(long prime, int tripletID, int vertexID)
        {
            Prime = prime;
            TripletID = tripletID;
            VertexID = vertexID;
        }
    }

    public class PrimeProcessor
    {
        //Problem 196 Prime triplets
        //Build a triangle from all positive integers in the following way:
        // 1
        // 2-  3-
        // 4   5-  6
        // 7-  8   9  10
        //11- 12  13- 14  15
        //16  17- 18  19- 20  21
        //22  23- 24  25  26  27  28
        //29- 30  31- 32  33  34  35  36
        //37- 38  39  40  41- 42  43- 44  45
        //46  47- 48  49  50  51  52  53- 54 55
        //56  57  58  59- 60  61- 62  63  64 65 66

        //Each positive integer has up to eight neighbours in the triangle.
        //A set of three primes is called a prime triplet if one of the three primes has the other two as neighbours in the triangle.
        //For example, in the second row, the prime numbers 2 and 3 are elements of some prime triplet.
        //If row 8 is considered, it contains two primes which are elements of some prime triplet, i.e. 29 and 31.
        //If row 9 is considered, it contains only one prime which is an element of some prime triplet: 37.
        //Define S(n) as the sum of the primes in row n which are elements of any prime triplet.
        //Then S(8)=60 and S(9)=37.
        //You are given that S(10000)=950007619.
        //Find  S(5678027) + S(7208785).

        // O( n * n * log(n) )
        public static List<TripletPrime> PrimeTriplets(long n)
        {
            var primeTriplets = new Dictionary<long, List<TripletPrime>>();
            List<long> primes = new List<long>();
            primes.Add(2);
            primes.Add(3);
            primes.Add(5);

            long lastNumber = 1;
            var rowList = new List<TripletPrime>();
            long len = primeTriplets.Count + 1;

            int tripletID = 1;
            int vertexID = 0;

            for (long i = 1; primeTriplets.Count < n; i++)
            {
                if (rowList.Count == 0)
                {
                    TripletPrime tP = new TripletPrime(lastNumber, 0, 0);
                    rowList.Add(tP);
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
                        TripletPrime tP = new TripletPrime(i, tripletID, vertexID);
                        rowList.Add(tP);
                    }else
                    {
                        primeTriplets.Add(len, rowList);
                        len++;
                        lastNumber = rowList[0].Prime + len;
                        rowList = new List<TripletPrime>();
                        TripletPrime tP = new TripletPrime(lastNumber, 0, 0);
                        rowList.Add(tP);
                        TripletPrime tP2 = new TripletPrime(i, tripletID, vertexID);
                        rowList.Add(tP2);
                    }

                    vertexID++;
                    if(vertexID >= 4)
                    {
                        vertexID = 1;
                        tripletID++;
                    }
                }

            }

            var nList = primeTriplets[n];
            return nList;
        }

        public static long SumOfPrimeTriplets(long n)
        {
            var list = PrimeTriplets(n);

            if(list.Count < 2)
            {
                return 0;
            }
            else if(list.Count == 2)
            {
                return list[1].Prime;
            }
            else
            {
                bool isFirst = true;
                long sumPrimes = 0;
                int tripletID = list[1].TripletID;

                foreach(var prime in list)
                {
                    if(isFirst)
                    {
                        isFirst = false;
                        continue;
                    }

                    if(prime.TripletID == tripletID)
                    {
                        sumPrimes += prime.Prime;
                    }
                }

                return sumPrimes;
            }
        }

        //Problem 35 Circular primes
        //The number, 197, is called a circular prime because all rotations of the digits: 197, 971, and 719, are themselves prime.
        //There are thirteen such primes below 100: 2, 3, 5, 7, 11, 13, 17, 31, 37, 71, 73, 79, and 97.
        //How many circular primes are there below one million?
        // O(n * Log(n))
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
                //Less than Log(n) here
                foreach (long rotation in numberList)
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

        protected static List<long> All_Rotations(string strNumber)
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
                //Less than Log(n) here
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

        //Problem 10 Summation of primes
        //The sum of the primes below 10 is 2 + 3 + 5 + 7 = 17.
        //Find the sum of all the primes below two million.
        // O(n * Log(n))
        public static long TheSumOfPrimesTest(long n)
        {
            long primeProduct = 2000; //277050
            var primeList = PrimeList(primeProduct);
            long result = 0;

            foreach (long prime in primeList)
            {
                result += prime;
            }

            return result;
        }

        // O(n * Log(n))
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
                //Less than Log(n) here
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

        //Problem 7 10001st prime projecteuler.net/problem=7
        //By listing the first six prime numbers: 2, 3, 5, 7, 11, and 13, we can see that the 6th prime is 13.
        //What is the 10001st prime number ?
        // O(n * Log(n))
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
                //Less than Log(n) here
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
