using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP.Assets
{
    public static class Crossover
    {
        public static List<int> Breeding(Invidual parent1, Invidual parent2, int routeLengh)
        {
            var child = new List<int>();

            int newRouteLengh = routeLengh / 2;

            Random random = new Random();

            var takenChromosones = new List<int>();

            int randomPoint = random.Next(1, routeLengh - 2);

            for (int i = 0; i < routeLengh; i++)
            {
                child.Add(0);
            }

            for (int i = randomPoint; i < randomPoint + 2; i++)
            {
                child[i] = parent1.Route[i];
                takenChromosones.Add(i);
            }

            for (int i = 1; i < routeLengh - 1; i++)
            {
                if (takenChromosones.Contains(i))
                {
                    continue;
                }
                else
                {
                    int o = i;
                    while(child.Contains(parent2.Route[o]))
                    {
                        if (o == routeLengh - 1)
                        {
                            o = 1;
                        }
                        else
                        {
                            o++;
                        }
                    }
                    child[i] = parent2.Route[o];
                }
            }

            return child;
        }
    }
}
