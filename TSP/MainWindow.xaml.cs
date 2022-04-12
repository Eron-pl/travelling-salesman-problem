using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TSP.Assets;
using Point = TSP.Assets.Point;
using LiveCharts;
using LiveCharts.Wpf;

namespace TSP
{
    
    public partial class MainWindow : Window
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        public List<double> bestDistances { get; set; }
        public ChartValues<double> bestRouteDistance { get; set; }

        public Canvas cv = new Canvas()
        {
            Background = new BrushConverter().ConvertFromString("Black") as Brush,
            Width = 400,
            Height = 400,
        };

        public List<Point> points = new List<Point>();

        public readonly string filepath = "D:\\data.csv";

        public MainWindow()
        {
            InitializeComponent();

            mainGrid.Children.Add(cv);
            Grid.SetColumn(cv, 1);
            Grid.SetRowSpan(cv, 3);
            cv.Height = 500;
            cv.Width = 500;

           bestRouteDistance = new ChartValues<double>();
        }

        private void BTNrandomGeneration_Click(object sender, RoutedEventArgs e)
        {
            if (bestRouteDistance.Count > 0) bestRouteDistance.Clear();
            cv.Children.Clear();
            points.Clear();

            int numberOfPoints = Int32.Parse(TBnumberOfPoints.Text);
            Random r = new Random();

            // Generowanie losowych punktów
            for (int i = 0; i < numberOfPoints; i++)
            {
                points.Add
                    (
                        new (r.Next((int)cv.Width-10), r.Next((int)cv.Height-10), cv, i)
                    );
            }
        }

        private void BTNcalculateBestRoute_Click(object sender, RoutedEventArgs e)
        {
            if (bestRouteDistance.Count > 0) bestRouteDistance.Clear();

            int numberOfPoints = Int32.Parse(TBnumberOfPoints.Text);
            int numberOfPopulations = Int32.Parse(TBnumberOfPopulations.Text);
            int numberOfGenerations = Int32.Parse(TBnumberOfGenerations.Text);

            int generationIterator = 1;

            Random random = new Random();

            // Generowanie populacji
            var population = Population.GeneratePopulation(numberOfPopulations, numberOfPoints);

            bestRouteDistance.Add(Route.FindLowestDistance(population, points));

            Data.ClearFile(filepath);

            while (generationIterator <= numberOfGenerations)
            {
                Data.AddText($"======GENERATION : {generationIterator} ======", filepath);
                foreach (var invidual in population)
                {
                    Data.AddInvidual(Fitness.GetFitness(invidual, points), invidual, Route.CalculateDistance(invidual, points), generationIterator, filepath);
                }
                Data.AddText($"\n", filepath);

                // Ewaluacja fitnessu każdego osobnika
                var populationsInviduals = new List<Invidual>();
                for (int i = 0; i < population.Count; i++)
                {
                    populationsInviduals.Add
                        (
                            new(Fitness.GetFitness(population[i], points), population[i])
                        );
                }

                // Selekcja
                var bestInviduals = new List<Invidual>();
                for (int i = 0; i < (numberOfPopulations / 2); i++)
                {
                    bestInviduals.Add
                        (
                            populationsInviduals[Selection.Tournament(numberOfPopulations, populationsInviduals)]
                        );
                }

                List<List<int>> tempRoute = new List<List<int>>();
                List<double> tempFitness = new List<double>();

                for (int i = 0; i < numberOfPopulations; i++)
                {
                    tempRoute.Add
                        (
                          Crossover.Breeding(bestInviduals[random.Next(0, bestInviduals.Count)], bestInviduals[random.Next(0, bestInviduals.Count)], numberOfPoints + 1)
                        );

                    tempFitness.Add
                        (
                            Fitness.GetFitness(tempRoute[i], points)
                        ); 
                }

                var children = new List<Invidual>();

                for (int i = 0; i < numberOfPopulations; i++)
                {
                    children.Add
                        (
                            new Invidual(tempFitness[i], tempRoute[i])
                        );
                }

                bestRouteDistance.Add(Route.FindLowestDistance(children, points));

                population.Clear();
                foreach (var invidual in children)
                {
                    population.Add(invidual.Route);
                }

                generationIterator++;
            }


            var bestRoute = Route.FindBestRoute(population, points);

            Route.DrawRoute(bestRoute, cv, points);

            StringBuilder routeString = new StringBuilder();
            routeString.Append("Najlepsza ścieżka: ");
            for (int i = 0; i < bestRoute.Count; i++)
            {
                if (i == bestRoute.Count - 1)
                    routeString.Append(bestRoute[i] + 1);
                else
                    routeString.Append($"{(bestRoute[i] + 1)} => ");
            }
            TBbestRoute.Text = routeString.ToString();

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Najkrótszy dystans",
                    Values = bestRouteDistance
                }
            };

            var labels = new string[numberOfGenerations];
            for (int i = 0; i < numberOfGenerations; i++)
            {
                labels[i] = (i + 1).ToString();
            }

            Labels = labels;

            DataContext = this;
        }
    }
}
