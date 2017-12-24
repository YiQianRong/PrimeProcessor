using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
{
    public class Bucket <K, V>
    {
        public K Key { get; set; }
        public V Val { get; set; }
        public int Hash { get; set; }   // Store hash code; which is the index of List<Bucket<K, V>> Collection
    }

    public class HashTable : HashTable<System.Object, System.Object>
    {
    }

    public class HashTable<K, V>
    {
        protected List<Bucket<K, V>> Collection = new List<Bucket<K, V>>();

        public virtual void Add(K key, V val)
        {
            if(Contains(key))
            {
                throw new Exception("Duplicated key in Add");
            }

            var item = new Bucket<K, V>();
            item.Key = key;
            item.Val = val;
            item.Hash = Collection.Count();
            Collection.Add(item);
        }

        public virtual void Remove(K key)
        {
            int hashCode = GetHash(key);
            if (hashCode >= 0)
            {
                Collection.Remove(Collection[hashCode]);
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
        public virtual bool Contains(Object key)
        {
            return ContainsKey(key);
        }

        public virtual bool Contains(K key)
        {
            return ContainsKey(key);
        }

        protected virtual Bucket<K, V> GetBucket(K key)
        {
            var item = Collection.Where(i => KeyEquals(i.Key, key)).FirstOrDefault();
            return item;
        }

        protected virtual Bucket<K, V> GetBucket(Object key)
        {
            var item = Collection.Where(i => KeyEquals(i.Key, key)).FirstOrDefault();
            return item;
        }

        protected virtual Bucket<K, V> GetBucketByValue(V val)
        {
            var item = Collection.Where(i => KeyEquals(i.Val, val)).FirstOrDefault();
            return item;
        }

        protected virtual Bucket<K, V> GetBucketByValue(Object val)
        {
            var item = Collection.Where(i => KeyEquals(i.Val, val)).FirstOrDefault();
            return item;
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
        public virtual bool ContainsKey(Object key)
        {
            if (IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key", "ArgumentNull_Key");
            }

            var item = GetBucket(key);

            return item == null ? false : true;
        }

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
        public virtual bool ContainsValue(Object value)
        {
            var item = GetBucketByValue(value);

            return IsNullOrEmpty(item) ? false : true;
        }

        public virtual bool ContainsValue(V value)
        {
            var item = GetBucketByValue(value);

            return IsNullOrEmpty(item) ? false : true;
        }

        // Internal method to get the hash code for an Object.  This will call
        // GetHashCode() on each object if you haven't provided an IHashCodeProvider 
        // instance.  Otherwise, it calls hcp.GetHashCode(obj). 
        protected virtual int GetHash(Object key)
        {
            var item = GetBucket(key);
            if (!IsNullOrEmpty(item))
            {
                return item.Hash;
            }
            else
            {
                return -1;
            }
        }

        protected virtual int GetHash(K key)
        {
            var item = GetBucket(key);
            if (!IsNullOrEmpty(item))
            {
                return item.Hash;
            }
            else
            {
                return -1;
            }
        }

        // Internal method to compare two keys.  If you have provided an IComparer
        // instance in the constructor, this method will call comparer.Compare(item, key).
        // Otherwise, it will call item.Equals(key). 
        //
        protected virtual bool KeyEquals(Object item, Object key)
        {
            if (Object.ReferenceEquals(key, item))
            {
                return false;
            }

            return IsNullOrEmpty(item) ? false : item.Equals(key);
        }

        protected virtual bool KeyEquals(K item, K key)
        {
            return IsNullOrEmpty(item) ? false : item.Equals(key);
        }

        protected virtual bool KeyEquals(V item, V val)
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
