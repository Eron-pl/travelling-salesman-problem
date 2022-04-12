using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP.Assets
{
    public static class Selection
    {
        public static int Tournament(int numberOfPopulations, List<Invidual> inviduals)
        {
            int bestInvidualsIndex = 0;
            double bestInvidualsFitness = 0.0;

            Random random = new Random();
            int startingPoint = random.Next(0, numberOfPopulations / 2);
            int endingPoint = random.Next(startingPoint, numberOfPopulations - 1);

            for (int i = startingPoint; i < endingPoint; i++)
            {
                if (inviduals[i].FitnessScore > bestInvidualsFitness)
                {
                    bestInvidualsFitness = inviduals[i].FitnessScore;
                    bestInvidualsIndex = i;
                }
            }
            return bestInvidualsIndex;
        }
    }
}
