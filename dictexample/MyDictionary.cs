using System;
using System.Collections.Generic;

namespace dictexample
{
    class MyDictionary<TKey, TValue>
    {
        public int Count { get; private set; }

        LinkedList<KeyValuePair<TKey, TValue>>[] bucket;

        public double MaxLoadFactor => 0.3;

        public double CurrentLoadFactor => (double)Count / bucketSize;

        int bucketSize;
        public MyDictionary()
        {
            Count = 0;

            bucketSize = 10;

            bucket = new LinkedList<KeyValuePair<TKey, TValue>>[bucketSize];

        }

        public TValue this[TKey key]
        {
            get
            {
                return Get(key);
            }
            set
            {
                Insert(key, value);
            }

        }

        public TValue Search(TKey key)
        {
            int bucketIndex = GetBucketIndex(key);

            foreach (KeyValuePair<TKey, TValue> keyValuePair in bucket[bucketIndex])
            {
                if(keyValuePair.Key.Equals(key))
                {
                    return keyValuePair.Value;
                }
            }
            throw new Exception("Key does not exist!");
        }

        public void Resize()
        {
            var newbucket = new LinkedList<KeyValuePair<TKey, TValue>>[bucketSize * 2];
           
            foreach (var list in bucket)
            {
                if (list != null)
                {
                    foreach (var kvp in list)
                    {
                        int newbucketindex = Math.Abs(kvp.Key.GetHashCode()) % newbucket.Length;

                        
                        // insert into new array

                        if (newbucket[newbucketindex] == null)
                        {
                            newbucket[newbucketindex] = new LinkedList<KeyValuePair<TKey, TValue>>();
                        }


                        newbucket[newbucketindex].AddLast(kvp);

                        
                    }
                }
            }

            bucketSize *= 2;
            bucket = newbucket;
        }
        public bool Insert(TKey key, TValue value)
        {
            int bucketIndex = GetBucketIndex(key);

            // go into my array at that bucketindex

            if (CurrentLoadFactor > MaxLoadFactor)
            {
                Resize();
            }

            if (bucket[bucketIndex] == null)
            {
                bucket[bucketIndex] = new LinkedList<KeyValuePair<TKey, TValue>>();
            }

            // if it doesnt exist in the bucket already, dictionary dont allow for duplicates

            if (CheckKeyExists(key, bucketIndex))
            {
                return false;
            }


            bucket[bucketIndex].AddLast(new KeyValuePair<TKey, TValue>(key, value));

            Count++;

            return true;
        }

        public bool Remove(TKey key, int bucketIndex)
        {

            //LinkedList<KeyValuePair<TKey, TValue>> list = bucket[bucketIndex];

            bucketIndex = GetBucketIndex(key);

            if (bucket[bucketIndex] == null) //if array at that index is empty
            {
                return false;
            }

            foreach (KeyValuePair<TKey, TValue> keyValuePair in bucket[bucketIndex])
            {
                if (keyValuePair.Key.Equals(key))
                {
                    bucket[bucketIndex].Remove(keyValuePair);
                    return true;
                }
            }
            return false;
        }


        private int GetBucketIndex(TKey key)
        {
            return Math.Abs(key.GetHashCode()) % bucketSize;
        }

        private bool CheckKeyExists(TKey key, int bucketIndex)
        {
            LinkedList<KeyValuePair<TKey, TValue>> list = bucket[bucketIndex];

            foreach (KeyValuePair<TKey, TValue> keyValuePair in list)
            {
                if (keyValuePair.Key.Equals(key))
                {
                    return true;
                }
            }

            return false;
        }

        public TValue Get(TKey key)
        {
            int bucketIndex = GetBucketIndex(key);

            if (bucket[bucketIndex] != null)
            {
                foreach (var kvp in bucket[bucketIndex])
                {
                    if (kvp.Key.Equals(key))
                    {
                        return kvp.Value;
                    }

                }
            }

            return default;
        }
    }
}
