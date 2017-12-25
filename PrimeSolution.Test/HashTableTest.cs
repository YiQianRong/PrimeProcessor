using NUnit.Framework;

namespace HashTable.Test
{
    class HashTableTest
    {
        [Test]
        public void HashTableBinarySearchTest()
        {
            var hashTable = new HashTable<int, string>();
            hashTable[310] = "Test9";
            hashTable[988] = "Test10";
            hashTable[23] = "Test4";
            hashTable[37] = "Test5";
            hashTable[100] = "Test6";
            hashTable[101] = "Test7";
            hashTable[5] = "Test1";
            hashTable[13] = "Test3";
            hashTable[210] = "Test8";
            hashTable[11] = "Test2";

            int index = 101;
            Assert.AreEqual(hashTable.GetBucketByIndex(hashTable.BinarySearch(index)).Key, index);
            index = 988;
            Assert.AreEqual(hashTable.GetBucketByIndex(hashTable.BinarySearch(index)).Key, index);
            index = 23;
            Assert.AreEqual(hashTable.GetBucketByIndex(hashTable.BinarySearch(index)).Key, index);
            index = 37;
            Assert.AreEqual(hashTable.GetBucketByIndex(hashTable.BinarySearch(index)).Key, index);
            index = 100;
            Assert.AreEqual(hashTable.GetBucketByIndex(hashTable.BinarySearch(index)).Key, index);
            index = 101;
            Assert.AreEqual(hashTable.GetBucketByIndex(hashTable.BinarySearch(index)).Key, index);
            index = 5;
            Assert.AreEqual(hashTable.GetBucketByIndex(hashTable.BinarySearch(index)).Key, index);
            index = 13;
            Assert.AreEqual(hashTable.GetBucketByIndex(hashTable.BinarySearch(index)).Key, index);
            index = 210;
            Assert.AreEqual(hashTable.GetBucketByIndex(hashTable.BinarySearch(index)).Key, index);
            index = 11;
            Assert.AreEqual(hashTable.GetBucketByIndex(hashTable.BinarySearch(index)).Key, index);

        }

        [Test]
        public void HasTableTypesTest()
        {
            var hashTable = new HashTable<int, string>();
            hashTable[1] = "Test1";
            hashTable[100] = "Test2";
            Assert.AreEqual(hashTable[1], "Test1");
            Assert.AreEqual(hashTable[100], "Test2");
            Assert.AreEqual(hashTable.Count, 2);
            hashTable.Remove(100);
            Assert.AreEqual(hashTable.Count, 1);
            hashTable.Clear();
            Assert.AreEqual(hashTable.Count, 0);
        }

        [Test]
        public void HasTableOjectTypeTest()
        {
            var hashTable = new HashTable();
            hashTable[2] = "Test1";
            hashTable[200] = "Test2";
            var item = hashTable[2];
            Assert.AreEqual(hashTable[2], "Test1");
            Assert.AreEqual(hashTable[200], "Test2");
            hashTable.Remove(200);
            Assert.AreEqual(hashTable.Count, 1);
            hashTable.Clear();
            Assert.AreEqual(hashTable.Count, 0);
        }
    }
}
