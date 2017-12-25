using NUnit.Framework;

namespace HashTable.Test
{
    class HashTableTest
    {
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
            Assert.AreEqual(hashTable[2], "Test1");
            Assert.AreEqual(hashTable[200], "Test2");
            hashTable.Remove(200);
            Assert.AreEqual(hashTable.Count, 1);
            hashTable.Clear();
            Assert.AreEqual(hashTable.Count, 0);
        }
    }
}
