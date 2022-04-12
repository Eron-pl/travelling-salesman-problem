using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP.Assets
{
    public static class Population
    {
        public static List<List<int>> GeneratePopulation(int numberOfPopulations, int numberOfPoints)
        {
            var Population = new List<List<int>>();

            for (int i = 0; i < numberOfPopulations; i++)
            {
                var route = Route.GenerateRandomRoute(numberOfPoints);
                Population.Add(route);
            }
            return Population;
        }
    }
}
