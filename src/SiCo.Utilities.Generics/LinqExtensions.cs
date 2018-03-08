namespace SiCo.Utilities.Generics
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// LinQ Helpers
    /// </summary>
    public static class LinqExtensions
    {
        /// <summary>
        /// Convert string to double
        /// </summary>
        /// <param name="input">this/input IEnumerable</param>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>0 if false</returns>
        public static IEnumerable<T> Deduplicate<T>(this IEnumerable<T> input)
        {
            if (input == null || input.Count() <= 0)
            {
                yield break;
            }

            HashSet<T> passedValues = new HashSet<T>();
            foreach (T item in input)
            {
                if (passedValues.Contains(item))
                {
                    continue;
                }
                else
                {
                    passedValues.Add(item);
                    yield return item;
                }
            }
        }
    }
}