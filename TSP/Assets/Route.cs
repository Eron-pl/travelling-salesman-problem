using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TSP.Assets
{
    public static class Route
    {
        public static List<int> GenerateRandomRoute(int numberOfPoints)
        {
            var RandomRoutes = new List<int>();
            Random random = new Random();
            int route;

            for (int i = 0; i < numberOfPoints + 1; i++)
            {
                if (i == 0)
                {
                    RandomRoutes.Add(0);
                }
                else if (i == numberOfPoints)
                {
                    RandomRoutes.Add(0);
                }
                else
                {
                    do
                    {
                       route = random.Next(0, numberOfPoints);
                    } while(RandomRoutes.Contains(route));
                    RandomRoutes.Add(route);
                }
            }
            return RandomRoutes;
        }

        public static void DrawRoute(List<int> route, Canvas cv,  List<Point> points)
        {
            // Usuwanie starych linii
            var lines = cv.Children.OfType<Line>().ToList();
            foreach (var line in lines)
            {
                cv.Children.Remove(line);
            }


            for (int i = 0; i < route.Count - 1; i++)
            {
                cv.Children.Add
                    (
                     new Line()
                     {

                         X1 = points[route[i]].x + 10,
                         X2 = points[route[i + 1]].x + 10,
                         Y1 = points[route[i]].y + 10,
                         Y2 = points[route[i + 1]].y + 10,
                         Stroke = new BrushConverter().ConvertFromString("Yellow") as Brush,
                         StrokeThickness = 2
                     }
                    );
            }
        }

        public static double CalculateDistance(List<int> route, List<Point> points)
        {
            double distance = 0.0;
            for (int i = 0;i < route.Count - 1;i++)
            {
                distance += points[route[i]].Distance(points[route[i + 1]]);
            }
            return distance;
        }

        public static double FindLowestDistance(List<Invidual> inviduals, List<Point> points)
        {
            double bestDistance = 999999.99;
            double tempDistance = 0.0;

            for (int i = 0; i < inviduals.Count; i++)
            {
                tempDistance = Route.CalculateDistance(inviduals[i].Route, points);
                if (tempDistance < bestDistance) bestDistance = tempDistance;
            }
            return bestDistance;
        }

        public static double FindLowestDistance(List<List<int>> routes, List<Point> points)
        {
            double bestDistance = 999999.99;
            double tempDistance = 0.0;

            for (int i = 0; i < routes.Count; i++)
            {
                tempDistance = Route.CalculateDistance(routes[i], points);
                if (tempDistance < bestDistance) bestDistance = tempDistance;
            }
            return bestDistance;
        }

        public static List<int> FindBestRoute (List<List<int>> routes, List<Point> points)
        {
            List<int> bestRoute = new List<int>();
            double bestDistance = 999999.99;
            double tempDistance = 0.0;

            for (int i = 0; i < routes.Count; i++)
            {
                tempDistance = Route.CalculateDistance(routes[i], points);
                if (tempDistance < bestDistance)
                {
                    bestDistance = tempDistance;
                    bestRoute.Clear();
                    foreach (var invidual in routes[i])
                    {
                        bestRoute.Add(invidual);
                    }
                }
            }
            return bestRoute;
        }
    }
}
