using System;
using System.Collections.Generic;
using System.Linq;

namespace MontyHallParadox
{
    class Program
    {
        static void Main(string[] args)
        {
            int doorsCount = 3;
            int repeatsCount = 100000;
            bool outputData = false;
            Door.MakeAnExperiment(doorsCount, repeatsCount, outputData);
            Console.ReadLine();
        }
    }
}
