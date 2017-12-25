using System;
using System.Collections.Generic;
using System.Linq;
using MicrosoftResearch.Infer.Collections;

namespace HashTable
{
    public class Bucket<K, V> : IComparable<Bucket<K, V>>
    {
        public K Key { get; set; }
        public V Val { get; set; }
        public int Hash { get; set; }   // Store hash code; which is the index of List<Bucket<K, V>> Collection

        public int CompareTo(Bucket<K, V> other)
        {
            int compareResult = 0;

            if (Key is IComparable)
            {
                compareResult = (Key as IComparable).CompareTo(other.Key);
            }
            return compareResult;
        }
    }

    // Defines a comparer to create a sorted set
    // that is sorted by the Bucket.Key
    public class SortedByBucketKey<K, V> : IComparer<Bucket<K, V>>
    {
        public int Compare(Bucket<K, V> x, Bucket<K, V> y)
        {
            return (x.Key as IComparable).CompareTo(y.Key);
        }
    }

    public class HashTable : HashTable<System.Object, System.Object>
    {
    }

    //This HashTable algorithm is using SortedSet (balanced tree) as container
    //Reduce the usage of memory to minimum.
    //Avoid memory restucture, bucket collision like orignal Hashtable
    //Add, Remove, Search are O(log(n))
    public class HashTable<K, V>
    {
        protected MicrosoftResearch.Infer.Collections.SortedSet<Bucket<K, V>> Collection = 
                    new MicrosoftResearch.Infer.Collections.SortedSet<Bucket<K, V>>(new SortedByBucketKey<K, V>());

        public virtual void Add(K key, V val)
        {
            if(Contains(key))
            {
                throw new Exception("Duplicated key in Add");
            }

            var item = new Bucket<K, V>();
            item.Key = key;
            item.Val = val;
            Collection.Add(item);
        }

        public virtual void Remove(K key)
        {
            int hashCode = GetHash(key);
            if (hashCode >= 0)
            {
                var item = Collection[hashCode];
                Collection.Remove(item);
            }
        }

        public virtual void Clear()
        {
            Collection.Clear();
        }

        public virtual V this[K key]
        {
            get {
                int hashCode = GetHash(key);
                if (hashCode >= 0 )
                {
                    return Collection[hashCode].Val;
                }
                else
                {
                    return default(V);
                }
            }

            set {
                int hashCode = GetHash(key);
                if (hashCode >= 0)
                {
                    Collection[hashCode].Val = value;
                }
                else
                {
                    Add(key, value);
                }
            }
        }

        // Checks if this hashtable contains the given key. 
        public virtual bool Contains(K key)
        {
            return ContainsKey(key);
        }

        protected virtual Bucket<K, V> GetBucket(K key)
        {
            int index = GetHash(key);

            if (index >= 0)
            {
                var item = Collection[index];
                item.Hash = index;
                return item;
            }

            return default(Bucket<K, V>);
        }

        protected virtual Bucket<K, V> GetBucketByValue(V val)
        {
            int index = Collection.FindIndex(
                    delegate (Bucket<K, V> cell)
                    {
                        return ValueEquals(cell.Val, val);
                    }
                );

            if (index >= 0)
            {
                var item = Collection[index];
                item.Hash = index;
                return item;
            }

            return default(Bucket<K, V>);
        }

        protected virtual bool IsNullOrEmpty(Object value)
        {
            if (Object.ReferenceEquals(value, null))
                return true;

            var type = value.GetType();
            return type.IsValueType
                && Object.Equals(value, Activator.CreateInstance(type));
        }

        // Checks if this hashtable contains an entry with the given key.  
        public virtual bool ContainsKey(K key)
        {
            if (IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key", "ArgumentNull_Key");
            }

            var item = GetBucket(key);
            return IsNullOrEmpty(item) ? false : true;
        }

        // Checks if this hashtable contains an entry with the given value. The
        // values of the entries of the hashtable are compared to the given value 
        // using the Object.Equals method. This method performs a linear 
        // search and is thus be substantially slower than the ContainsKey
        // method. 
        public virtual bool ContainsValue(V value)
        {
            var item = GetBucketByValue(value);

            return IsNullOrEmpty(item) ? false : true;
        }

        // Internal method to get the hash code for an Object.  
        protected virtual int GetHash(K key)
        {
            //This is O(n)
            //int hash = Collection.FindIndex(
            //        delegate (Bucket<K, V> cell)
            //        {
            //            return KeyEquals(cell.Key, key);
            //        }
            //    );

            //binary search O(Log(n))
            int hash = BinarySearch(key);
            return hash;
        }

        public virtual Bucket<K, V> GetBucketByIndex(int index)
        {
            var item = Collection[index];
            item.Hash = index;
            return item;
        }

        public virtual int BinarySearch(K key)
        {
            //Binary search O(Log(n))
            int lowIndex = 0;
            int highIndex = Collection.Count - 1;
            int midIndex = (highIndex - lowIndex) / 2;

            do
            {
                if(midIndex >= Collection.Count)
                {
                    return -1;
                }

                var item = Collection[midIndex];
                int compareResult = 0;
                if (item.Key is IComparable)
                {
                    compareResult = (item.Key as IComparable).CompareTo(key);
                }

                if(compareResult == 0)
                {
                    lowIndex = midIndex;
                    highIndex = midIndex;
                }else if (compareResult > 0)
                {
                    highIndex = midIndex;
                    if (midIndex == lowIndex + 1)
                    {
                        midIndex = lowIndex;
                    }
                    else
                    {
                        midIndex = lowIndex + (highIndex - lowIndex) / 2;
                    }
                }
                else if (compareResult < 0)
                {
                    lowIndex = midIndex;
                    if (highIndex == midIndex + 1)
                    {
                        midIndex = highIndex;
                    }
                    else
                    {
                        midIndex = lowIndex + (highIndex - lowIndex) / 2;
                    }
                }

            } while (lowIndex != midIndex && midIndex != highIndex);

            var itemLast = Collection[midIndex];
            int compareResultLast = 0;
            if (itemLast.Key is IComparable)
            {
                compareResultLast = (itemLast.Key as IComparable).CompareTo(key);
            }

            if(compareResultLast != 0)
            {
                return -1;
            }

            return midIndex;
        }

        // Internal method to compare two keys.  If you have provided an IComparer
        // instance in the constructor, this method will call comparer.Compare(item, key).
        // Otherwise, it will call item.Equals(key). 
        protected virtual bool KeyEquals(K item, K key)
        {
            return IsNullOrEmpty(item) ? false : item.Equals(key);
        }

        protected virtual bool ValueEquals(V item, V val)
        {
            return IsNullOrEmpty(item) ? false : item.Equals(val);
        }

        // Returns a collection representing the keys of this hashtable. The order 
        // in which the returned collection represents the keys is unspecified, but
        // it is guaranteed to be          buckets = newBuckets; the same order in which a collection returned by
        // GetValues represents the values of the hashtable.
        // 
        // The returned collection is live in the sense that any changes
        // to the hash table are reflected in this collection.  It is not 
        // a static copy of all the keys in the hash table. 
        //
        public virtual ICollection<K> Keys
        {
            get
            {
                var item = Collection.Select( i => i.Key ).ToList<K>();
                return item;
            }
        }

        // Returns a collection representing the values of this hashtable. The 
        // order in which the returned collection represents the values is
        // unspecified, but it is guaranteed to be the same order in which a 
        // collection returned by GetKeys represents the keys of the
        // hashtable.
        //
        // The returned collection is live in the sense that any changes 
        // to the hash table are reflected in this collection.  It is not
        // a static copy of all the keys in the hash table. 
        // 
        public virtual ICollection<V> Values
        {
            get
            {
                var item = Collection.Select(i => i.Val).ToList<V>();
                return item;
            }
        }

        // Returns the number of associations in this hashtable. 
        // 
        public virtual int Count
        {
            get { return Collection.Count; }
        }

    }
}
