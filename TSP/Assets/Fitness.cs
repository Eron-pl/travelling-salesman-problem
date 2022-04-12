using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TSP.Assets
{
    public static class Fitness
    {
        public static double GetFitness(List<int> route, List<Point> points)
        {
            double totalDistance = 0.0;
            for (int i = 0; i < route.Count - 1; i++)
            {
                totalDistance += points[route[i]].Distance(points[route[i + 1]]);
            }
            return 1 / totalDistance;
        }
    }
}
