using NUnit.Framework;

namespace HashTable.Test
{
    class HashTableTest
    {
        private void CheckBucketByKey(HashTable<int, string> hashTable, int key)
        {
            var index = hashTable.BinarySearch(key);
            var item = hashTable.GetBucketByIndex(index);
            Assert.AreEqual(item.Key, key);
        }

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

            CheckBucketByKey(hashTable, 310);
            CheckBucketByKey(hashTable, 988);
            CheckBucketByKey(hashTable, 23);
            CheckBucketByKey(hashTable, 37);
            CheckBucketByKey(hashTable, 100);
            CheckBucketByKey(hashTable, 101);
            CheckBucketByKey(hashTable, 5);
            CheckBucketByKey(hashTable, 13);
            CheckBucketByKey(hashTable, 210);
            CheckBucketByKey(hashTable, 11);
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
