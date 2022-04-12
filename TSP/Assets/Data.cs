using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP.Assets
{

    public static class Data
    {
        public static void AddInvidual(double fitness, List<int> route, double distance, int generetionNumber, string filepath)
        {
            try
            {
                StringBuilder routeString = new StringBuilder();
                route.ForEach(x => routeString.Append(x.ToString() + " "));

                using (System.IO.StreamWriter file = new StreamWriter(filepath, true))
                {
                    file.WriteLine($"{generetionNumber}, {fitness}, {routeString}, {distance}");
                }
            }  
            catch (Exception e)
            {
                throw new ApplicationException("x",e);
            }
        }

        public static void AddText(string title, string filepath)
        {
            try
            {
                using (System.IO.StreamWriter file = new StreamWriter(filepath, true))
                {
                    file.WriteLine(title);
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException("x", e);
            }
        }

        public static void ClearFile(string filepath)
        {
            System.IO.File.WriteAllText(filepath, string.Empty);
        }
    }
}
