using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace TripSort
{
    public static class TripSorter
    {
        /// <summary>
        /// Сортирует список карточек путешествий так,
        /// что для каждой карточки в упорядоченном списке пункт назначения
        /// совпадает с пунктом отправления в следующей карточке.
        /// </summary>
        /// <param name="source">Список карточек для сортировки</param>
        /// <returns>Отсортированный список карточек</returns>
        /// <remarks>Сложность алгоритма O(N ^ 2)</remarks>
        public static IEnumerable<T> Sort<T>(IEnumerable<T> source) where T : Trip
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Contract.EndContractBlock();

            var data = source.ToArray();
            var n = data.Length;
            if (n < 2)
            {
                return data; // trivial situation
            }

            var sorted = new LinkedList<int>();
            var watchedIndexes = Enumerable.Range(0, n).ToArray();
            const int watchedFlag = -1;
            // количество проходов ~ N / 2
            while (sorted.Count < n)
            {
                // количество проходов равно N
                for (var i = 0; i < n; i++)
                {
                    // O(1)
                    if (watchedIndexes[i] == watchedFlag)
                    {
                        continue;
                    }

                    var trip = data[i]; // O(1)

                    // sorted.LastOrDefault() и sorted.FirstOrDefault() -> O(1)
                    // data[sorted.LastOrDefault()] и data[sorted.FirstOrDefault()] -> O(1)
                    if (data[sorted.LastOrDefault()].EndPoint == trip.StartPoint)
                    {
                        sorted.AddLast(i); // O(1)
                    }
                    else if (data[sorted.FirstOrDefault()].StartPoint == trip.EndPoint)
                    {
                        sorted.AddFirst(i); // O(1)
                    }
                    else
                    {
                        continue;
                    }

                    watchedIndexes[i] = watchedFlag; // O(1)
                }
            }

            return sorted.Select(i => data[i]); // O(N)
        }
    }
}
