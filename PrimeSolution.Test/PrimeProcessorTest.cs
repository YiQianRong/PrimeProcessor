﻿using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrimeSolution.Test
{
    [TestFixture]
    public class PrimeProcessorTest
    {
        //[Test]
        public void ChineseLeftoversIITest()
        {
            //Problem 552 Chinese leftovers II
            //Let An be the smallest positive integer satisfying An mod pi = i for all 1 ≤ i ≤ n, where pi is the i-th prime. 
            //For example A2 = 5, since this is the smallest positive solution of the system of equations 
            // A2 mod 2 = 1 
            // A2 mod 3 = 2
            //
            //The system of equations for A3 adds another constraint. That is, A3 is the smallest positive solution of
            // A3 mod 2 = 1 
            // A3 mod 3 = 2
            // A3 mod 5 = 3
            //
            //and hence A3 = 23. Similarly, one gets A4 = 53 and A5 = 1523. 
            //Let S(n) be the sum of all primes up to n that divide at least one element in the sequence A. 
            //For example, S(50) = 69 = 5 + 23 + 41, since 5 divides A2, 23 divides A3 and 41 divides A10 = 5765999453. No other prime number up to 
            //50 divides an element in A. 
            //
            //Find S(300000).
        }

        [Test]
        public void PrimeTripletsTest()
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

            //It is np problem

            var primeList = PrimeProcessor.PrimeTriplets(1);
            Assert.AreEqual(primeList[1], 1);

            primeList = PrimeProcessor.PrimeTriplets(2);
            Assert.AreEqual(primeList[1], 2);
            Assert.AreEqual(primeList[2], 3);

            primeList = PrimeProcessor.PrimeTriplets(3);
            Assert.AreEqual(primeList[1], 5);

            primeList = PrimeProcessor.PrimeTriplets(4);
            Assert.AreEqual(primeList[1], 7);

            primeList = PrimeProcessor.PrimeTriplets(5);
            Assert.AreEqual(primeList[1], 11);
            Assert.AreEqual(primeList[2], 13);

            primeList = PrimeProcessor.PrimeTriplets(6);
            Assert.AreEqual(primeList[1], 17);
            Assert.AreEqual(primeList[2], 19);

            primeList = PrimeProcessor.PrimeTriplets(7);
            Assert.AreEqual(primeList[1], 23);

            primeList = PrimeProcessor.PrimeTriplets(8);
            Assert.AreEqual(primeList[1], 29);
            Assert.AreEqual(primeList[2], 31);

            primeList = PrimeProcessor.PrimeTriplets(9);
            Assert.AreEqual(primeList[1], 37);
            Assert.AreEqual(primeList[2], 41);
            Assert.AreEqual(primeList[3], 43);

            primeList = PrimeProcessor.PrimeTriplets(10);
            Assert.AreEqual(primeList[1], 47);
            Assert.AreEqual(primeList[2], 53);

            primeList = PrimeProcessor.PrimeTriplets(11);
            Assert.AreEqual(primeList[1], 59);
            Assert.AreEqual(primeList[2], 61);

            //To be completed
        }

        [Test]
        public void CircularPrimesTest()
        {
            //Problem 35 Circular primes
            //The number, 197, is called a circular prime because all rotations of the digits: 197, 971, and 719, are themselves prime.
            //There are thirteen such primes below 100: 2, 3, 5, 7, 11, 13, 17, 31, 37, 71, 73, 79, and 97.
            //How many circular primes are there below one million?

            //O(n * Log(n))

            long primeProduct = 100000; //
            var primeList = PrimeProcessor.CircularPrimes(primeProduct);
            long result = primeList.OrderByDescending(p => p).ToList().FirstOrDefault();

            Assert.AreEqual(result, 9311);
        }

        [Test]
        public void TheSumOfPrimesTest()
        {
            //Problem 10 Summation of primes
            //The sum of the primes below 10 is 2 + 3 + 5 + 7 = 17.
            //Find the sum of all the primes below two million.

            //O(n * Log(n))

            long primeProduct = 2000; //277050
            var primeList = PrimeProcessor.PrimeList(primeProduct);
            long result = 0;

            foreach(long prime in primeList)
            {
                result += prime;
            }

            Assert.AreEqual(result, 277050);
        }

        [Test]
        public void The_nth_PrimeTest()
        {
            //Problem 7 10001st prime projecteuler.net/problem=7
            //By listing the first six prime numbers: 2, 3, 5, 7, 11, and 13, we can see that the 6th prime is 13.
            //What is the 10 001st prime number ?

            //It is a NP problem

            long nth_prime = 10001; //104743
            var largestPrimeFactors = PrimeProcessor.The_nth_Prime(nth_prime);
            var result = largestPrimeFactors[largestPrimeFactors.Count-1];

            Assert.AreEqual(result, 104743);
        }

        [Test]
        public void LargestPrimeFactorsTest()
        {
            //Problem 3 Largest prime factor projecteuler.net/problem=3
            //The prime factors of 13195 are 5, 7, 13 and 29.
            //What is the largest prime factor of the number 600851475143 ?

            //O(n * Log(n))

            //long printProduct = 13195; //5, 7, 13, 29
            long primeProduct = 600851475143; //71, 839, 1471, 6857
            var largestPrimeFactors = PrimeProcessor.LargestPrimeFactor(primeProduct);

            long result = 1;
            foreach (long prime in largestPrimeFactors)
            {
                result = result * prime;
            }

            Assert.AreEqual(primeProduct, result);
        }


    }
}
