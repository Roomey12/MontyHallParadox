using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MontyHallParadox
{
    public class Door
    {
        public int Number { get; set; }
        public bool HasPrize { get; set; }
        public bool IsOpened { get; set; }

        private static readonly Random random = new Random();

        public static List<Door> FillData(int count)
        {
            var result = new List<Door>();
            var random = new Random();
            var correctNumber = random.Next(1, count + 1);

            for(int i = 0; i < count; i++)
            {
                result.Add(new Door()
                {
                    Number = i + 1,
                    HasPrize = i + 1 == correctNumber
                });
            }

            return result;
        }

        public static void OpenDoors(List<Door> doors, int firstAnswer)
        {
            var guessedCorrectDoor = doors.Any(x => x.HasPrize && x.Number == firstAnswer);

            int doorNotToOpen = 0;
            if (guessedCorrectDoor)
            {
                doorNotToOpen = random.Next(1, doors.Count() + 1);
                var recreate = doors.Any(x => x.HasPrize && x.Number == doorNotToOpen);
                while (recreate)
                {
                    doorNotToOpen = random.Next(1, doors.Count() + 1);
                    recreate = doors.Any(x => x.HasPrize && x.Number == doorNotToOpen);
                }
            }

            int doorToCompare = guessedCorrectDoor ? doorNotToOpen : firstAnswer;
            foreach (var door in doors)
            {
                if (!door.HasPrize && door.Number != doorToCompare)
                {
                    door.Open();
                }
            }
        }

        public static int ChangeAnswer(List<Door> doors, int firstAnswer)
        {
            int newAnswer = doors.FirstOrDefault(x => !x.IsOpened && x.Number != firstAnswer).Number;
            return newAnswer;
        }

        public void Open()
        {
            IsOpened = true;
        }

        public static void OpenFinalDoor(List<Door> doors, int answer)
        {
            foreach(var door in doors)
            {
                if(door.Number == answer)
                {
                    door.Open();
                    break;
                }
            }
        }

        public static bool GetResult(List<Door> doors)
        {
            var doesWon = doors.Any(x => x.HasPrize && x.IsOpened);
            string resultToOutput = doesWon ? "Win!\n" : "Lose!\n";
            Console.WriteLine(resultToOutput);
            return doesWon;
        }

        public static void MakeAnExperiment(int doorsCount, int repeatsCount)
        {
            double timesWon = 0;
            for(int i = 0; i < repeatsCount; i++)
            {
                var doors = FillData(doorsCount);
                var firstAnswer = random.Next(1, doorsCount + 1);

                Console.WriteLine($"Experiment №: {i+1}");
                OpenDoors(doors, firstAnswer);

                var secondAnswer = ChangeAnswer(doors, firstAnswer);
                OpenFinalDoor(doors, secondAnswer);

                var result = GetResult(doors);
                if (result)
                {
                    timesWon++;
                }
            }
            double winRate = timesWon / repeatsCount;
            Console.WriteLine("\n\n\n\n\n");

            Console.WriteLine($"Repeats: {repeatsCount}");
            Console.WriteLine($"Times Won: {timesWon}");
            Console.WriteLine($"Win Rate: {winRate * 100}%");
        }

        public static void MakeAnExperimentWithOutput(int doorsCount, int repeatsCount)
        {
            double timesWon = 0;
            for (int i = 0; i < repeatsCount; i++)
            {
                var doors = FillData(doorsCount);
                var firstAnswer = random.Next(1, doorsCount + 1);

                Console.WriteLine($"Experiment №: {i + 1}");
                Console.WriteLine($"First answer: {firstAnswer}\n");
                Console.WriteLine("Doors before opening\n");
                OutputDoors(doors);

                OpenDoors(doors, firstAnswer);

                Console.WriteLine("\nDoors after opening\n");
                OutputDoors(doors);

                var secondAnswer = ChangeAnswer(doors, firstAnswer);
                Console.WriteLine($"Second answer: {secondAnswer}\n");
                OpenFinalDoor(doors, secondAnswer);

                Console.WriteLine($"Doors after openning final door\n");
                OutputDoors(doors);

                var result = GetResult(doors);
                if (result)
                {
                    timesWon++;
                }
            }
            double winRate = timesWon / repeatsCount;
            Console.WriteLine("\n\n\n\n\n");

            Console.WriteLine($"Repeats: {repeatsCount}");
            Console.WriteLine($"Times Won: {timesWon}");
            Console.WriteLine($"Win Rate: {winRate * 100}%");
        }

        public static void OutputDoors(List<Door> doors)
        {
            foreach (var door in doors)
            {
                Console.WriteLine(door);
            }
        }

        public override string ToString()
        {
            return $"Number: {Number};\t IsOpened: {IsOpened};\tHasPrize: {HasPrize};";
        }
    }
}
