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
            int repeatsCount = 1000000;
            Door.MakeAnExperiment(doorsCount, repeatsCount);
            Console.ReadLine();
        }
    }
}
