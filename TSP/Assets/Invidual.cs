using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP.Assets
{
    public class Invidual
    {
        private double _fitnessScore;
        private List<int> _route;

        public double FitnessScore
        {
            get { return _fitnessScore; }
            set { _fitnessScore = value; }
        }

        public List<int> Route 
        { 
            get { return _route; } 
            set { _route = value; }
        }

        public Invidual(double fitnessScore, List<int> route)
        {
            FitnessScore = fitnessScore;
            Route = route;
        }
    }
}
