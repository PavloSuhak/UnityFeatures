using System;
using System.Collections.Generic;

namespace Game.Scripts.Extensions
{
    public static class CollectionExtensions
    {
        private static Random rng = new Random(); 

        public static void Shuffle<T>(this IList<T> list)  
        {  
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1); 
                (list[k], list[n]) = (list[n], list[k]);
            }
        } 
        
        public static void Shuffle<T>(this Queue<T> queue)
        {
            T[] array = queue.ToArray();
            int n = array.Length;

            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                (array[k], array[n]) = (array[n], array[k]);
            }

            queue.Clear();
            foreach (T item in array)
            {
                queue.Enqueue(item);
            }
        }
    }
}