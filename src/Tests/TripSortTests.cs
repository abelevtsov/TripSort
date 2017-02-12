using System.Collections;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;
using TripSort;

namespace Tests
{
    [TestFixture]
    public class TripSortTests
    {
        [Test]
        [TestCaseSource(typeof(TestCaseFactory), nameof(TestCaseFactory.TestCases))]
        public void TripsAreSorted(IEnumerable<Trip> trips)
        {
            var data = trips.ToArray();
            var sorted = TripSorter.Sort(data).ToArray();
            Assert.That(sorted.Length, Is.EqualTo(data.Length));
            for (var i = 0; i < sorted.Length - 1; i++)
            {
                var first = sorted[i];
                var next = sorted[i + 1];
                Assert.That(first.EndPoint, Is.EqualTo(next.StartPoint));
            }
        }

        private static class TestCaseFactory
        {
            private static readonly List<TripPoint> StarwarsPlanets =
                new List<TripPoint>
                {
                    new TripPoint { Name = "Alderaan", Details = "http://en.wikipedia.org/wiki/Alderaan" },
                    new TripPoint { Name = "Anoat", Details = "http://en.wikipedia.org/wiki/List_of_Star_Wars_planets_(A-B)#Anoat" },
                    new TripPoint { Name = "Ansion", Details = "http://en.wikipedia.org/wiki/Ansion#Ansion" },
                    new TripPoint { Name = "Bespin", Details = "http://en.wikipedia.org/wiki/Bespin" },
                    new TripPoint { Name = "Bogden", Details = "http://en.wikipedia.org/wiki/List_of_Star_Wars_planets_(A-B)#Bogden" },
                    new TripPoint { Name = "Boz Pity", Details = "http://en.wikipedia.org/wiki/Boz_Pity" },
                    new TripPoint { Name = "Cato Neimoidia", Details = "http://en.wikipedia.org/wiki/Cato_Neimoidia#Cato_Neimoidia" },
                    new TripPoint { Name = "Corellia", Details = "http://en.wikipedia.org/wiki/List_of_Star_Wars_planets_(C-D)#Corellia" },
                    new TripPoint { Name = "Coruscant", Details = "http://en.wikipedia.org/wiki/Coruscant" },
                    new TripPoint { Name = "Dagobah", Details = "http://en.wikipedia.org/wiki/Dagobah" },
                    new TripPoint { Name = "Dantooine", Details = "http://en.wikipedia.org/wiki/Dantooine#Dantooine" },
                    new TripPoint { Name = "Endor", Details = "http://en.wikipedia.org/wiki/Endor_(Star_Wars)" },
                    new TripPoint { Name = "Felucia", Details = "http://en.wikipedia.org/wiki/Felucia#Felucia" },
                    new TripPoint { Name = "Geonosis", Details = "http://en.wikipedia.org/wiki/Geonosis#Geonosis" },
                    new TripPoint { Name = "Hoth", Details = "http://en.wikipedia.org/wiki/Hoth" },
                    new TripPoint { Name = "Iego", Details = "http://en.wikipedia.org/wiki/Iego#Iego" },
                    new TripPoint { Name = "Kamino", Details = "http://en.wikipedia.org/wiki/Kamino" },
                    new TripPoint { Name = "Kashyyyk", Details = "http://en.wikipedia.org/wiki/Kashyyyk" },
                    new TripPoint { Name = "Kessel", Details = "http://en.wikipedia.org/wiki/List_of_Star_Wars_planets_(K-L)#Kessel" },
                    new TripPoint { Name = "Malastare", Details = "http://en.wikipedia.org/wiki/Malastare#Malastare" },
                    new TripPoint { Name = "Mustafar", Details = "http://en.wikipedia.org/wiki/Mustafar" },
                    new TripPoint { Name = "Mygeeto", Details = "http://en.wikipedia.org/wiki/Mygeeto#Mygeeto" },
                    new TripPoint { Name = "Naboo", Details = "http://en.wikipedia.org/wiki/Naboo" },
                    new TripPoint { Name = "Nar Shaddaa", Details = "http://en.wikipedia.org/wiki/Nar_Shaddaa#Nar_Shaddaa" },
                    new TripPoint { Name = "Ord Mantell", Details = "http://en.wikipedia.org/wiki/List_of_Star_Wars_planets_(O%E2%80%93Q)#Ord_Mantell" },
                    new TripPoint { Name = "Polis Massa", Details = "http://en.wikipedia.org/wiki/Polis_Massa#Polis_Massa" },
                    new TripPoint { Name = "Saleucami", Details = "http://en.wikipedia.org/wiki/List_of_Star_Wars_planets_(R-S)#Saleucami" },
                    new TripPoint { Name = "Subterrel", Details = "http://en.wikipedia.org/wiki/List_of_Star_Wars_planets_(R-S)#Subterrel" },
                    new TripPoint { Name = "Sullust", Details = "http://en.wikipedia.org/wiki/List_of_Star_Wars_planets_(R-S)#Sullust" },
                    new TripPoint { Name = "Taanab", Details = "http://en.wikipedia.org/wiki/List_of_Star_Wars_planets_(T-V)#Taanab" },
                    new TripPoint { Name = "Tatooine", Details = "http://en.wikipedia.org/wiki/Tatooine" },
                    new TripPoint { Name = "Tund", Details = "http://en.wikipedia.org/wiki/List_of_Star_Wars_planets_(T-V)#Tund" },
                    new TripPoint { Name = "Utapau", Details = "http://en.wikipedia.org/wiki/List_of_Star_Wars_planets_(T-V)#Utapau" },
                    new TripPoint { Name = "Yavin", Details = "http://en.wikipedia.org/wiki/Yavin" },
                    new TripPoint { Name = "Yavin IV", Details = "http://en.wikipedia.org/wiki/Yavin_IV#Yavin_IV" }
                };

            private static readonly List<Trip> Trips = new List<Trip>();

            public static IEnumerable TestCases
            {
                get
                {
                    if (!Trips.Any())
                    {
                        for (var i = 0; i < StarwarsPlanets.Count - 1; i++)
                        {
                            var first = StarwarsPlanets[i];
                            var next = StarwarsPlanets[i + 1];

                            Trips.Add(new Trip(first.Name, next.Name, first.Details, Vehicle.Spaceship));
                        }
                    }

                    yield return Trips;
                    yield return Trips.Shuffle();
                    yield return Trips.ToArray().Reverse();
                    yield return Enumerable.Range(0, 100).Select(i => new Trip(i.ToString(), (i + 1).ToString())).ToArray().Shuffle();
                    yield return Enumerable.Range(0, 1000).Select(i => new Trip(i.ToString(), (i + 1).ToString())).ToArray().Shuffle();
                    yield return Enumerable.Range(0, 10000).Select(i => new Trip(i.ToString(), (i + 1).ToString())).ToArray().Shuffle();
                }
            }

            private struct TripPoint
            {
                public string Name { get; set; }

                public string Details { get; set; }
            }
        }
    }
}
