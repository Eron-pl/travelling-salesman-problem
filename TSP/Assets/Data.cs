using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TSP.Assets
{

    public static class Data
    {
        public static bool failToCreateLog = false;
        //public static string filepath = Directory.GetCurrentDirectory() + @"\data.csv";
        public static string filepath = "D:\\" + @"\data.csv";

        public static void AddInvidual(double fitness, List<int> route, double distance, int generetionNumber)
        {
            if (!failToCreateLog)
            {
                try
                {
                    StringBuilder routeString = new StringBuilder();
                    route.ForEach(x => routeString.Append(x.ToString() + " "));
                    System.IO.File.AppendAllText(filepath, $"{generetionNumber}, {fitness}, {routeString}, {distance}\n");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString(), "Nie udało się zapisać logów aplikacji!");
                    failToCreateLog = true;
                }
            }
        }

        public static void AddText(string text)
        {
            if (!failToCreateLog)
            {
                try
                {
                    using (System.IO.StreamWriter file = new StreamWriter(filepath, true))
                    { 
                        file.WriteLine(text);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString(), "Nie udało się zapisać logów aplikacji!");
                    failToCreateLog = true;
                }
            }
        }

        public static void ClearFile()
        {
            if (!failToCreateLog)
            {
                try
                {
                    if (File.Exists(filepath)) System.IO.File.WriteAllText(filepath, string.Empty);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString(), "Nie udało się zapisać logów aplikacji!");
                    failToCreateLog = true;
                }
            }
            if (File.Exists(filepath)) System.IO.File.WriteAllText(filepath, string.Empty);
        }
    }
}
