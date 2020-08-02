using System;
using System.Collections.Generic;

namespace dictexample
{




    class Program
    {
        static void Main(string[] args)
        {
            var dict = new MyDictionary<int,string>();
            //int count = 0;

            //for (char c = 'a'; c <= 'z'; c++)
            //{
            //    dict.Insert(c, count++);
            //}

            //for (char c = 'a'; c <= 'z'; c++)
            //{
            //    Console.WriteLine(dict[c]);
            //}

            //Console.WriteLine(dict.Insert('a', 5));

            //Console.WriteLine(dict.Remove('a', 5));

            dict[5] = "hello";

            dict[6] = "world";

            //Console.WriteLine(dict.Search(5, "hello"));

            Console.WriteLine(dict.Search(5));

            dict.Resize();

        }
    }
}
