using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programing
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please enter arguments.");
                Console.WriteLine("Usage: Programing <file> <ascending> <sort>");
                return;
            }

            //var file = "Number.txt";
            var file = args[0];
            if (string.IsNullOrEmpty(file))
            {
                Console.WriteLine("Missing <file> argument");
                return;
            }
            //var ascending = true;
            var ascending = bool.TryParse(args[1], out bool succes);
            if (succes == false)
            {
                Console.WriteLine("Missing <ascending> argument");
                return;
            }

            //var sort = "n"; // n - number sort; s - string sort; h - hybrid sort;
            var sort = args[2];
            if (string.IsNullOrEmpty(sort))
            {
                Console.WriteLine("Missing <sort> argument");
                return;
            }

            var numbers = ReadNumbers(file);

            switch (sort)
            {
                case "n":
                    {
                        var numberSorted = NumberSort(numbers, ascending);
                        Console.Write("Number Sort: ");
                        PrintNumbers(numberSorted);
                    }
                    break;
                case "s":
                    {
                        var numbersStringOrdered = StringSort(numbers, ascending);
                        Console.Write("String Sort: ");
                        PrintNumbers(numbersStringOrdered);
                    }
                    break;
                case "h":
                    {
                        var numbersHybridSorted = HybridSort(numbers, ascending);
                        Console.Write("Hybrid Sort: ");
                        PrintNumbers(numbersHybridSorted);
                    }
                    break;
                default:
                    Console.WriteLine("Unknown sorting");
                    break;
            }
            Console.ReadKey();
        }

        public static List<int> ReadNumbers(string file)
        {
            var lines = File.ReadAllLines(file);
            var firstLine = lines[0];
            var firstLineWords = firstLine.Split(',');
            var numbers = new List<int>();
            foreach (var word in firstLineWords)
            {
                var number = word.Replace(" ", "");
                numbers.Add(Convert.ToInt32(number));
            }
            return numbers;
        }

        public static void PrintNumbers(List<int> numbers)
        {
            //Console.Write("Number Sort: ");
            for (int i = 0; i < numbers.Count(); i++)
            {
                Console.Write(numbers[i] + " ");
            }
        }

        public static List<int> NumberSort(List<int> numbers, bool ascending)
        {
            for (int i = 0; i < numbers.Count(); i++)
            {
                for (int j = 0; j < numbers.Count() - 1; j++)
                {
                    if ((ascending == true && numbers[j] > numbers[j + 1]) || (ascending == false && numbers[j] < numbers[j + 1]))
                    {
                        int temp = numbers[j];
                        numbers[j] = numbers[j + 1];
                        numbers[j + 1] = temp;
                    }
                }
            }
            return numbers;
        }

        public static List<int> StringSort(List<int> numbers, bool ascending)
        {
            for (int i = 0; i < numbers.Count(); i++)
            {
                for (int j = 0; j < numbers.Count() - 1; j++)
                {
                    if ((ascending == true && numbers[j].ToString().CompareTo(numbers[j + 1].ToString()) > 0) ||
                        (ascending == false && numbers[j].ToString().CompareTo(numbers[j + 1].ToString()) < 0))
                    {
                        int temp = numbers[j];
                        numbers[j] = numbers[j + 1];
                        numbers[j + 1] = temp;
                    }
                }
            }
            return numbers;

        }

        public static List<int> HybridSort(List<int> numbers, bool ascending)
        {
            var oddNumbers = new List<int>();
            var evenNumbers = new List<int>();
            for (int i = 0; i < numbers.Count(); i++)
            {
                if (numbers[i] % 2 == 0)
                {
                    evenNumbers.Add(numbers[i]);
                }
                else
                {
                    oddNumbers.Add(numbers[i]);
                }
            }

            var evenNumberSorted = NumberSort(evenNumbers, !ascending);
            var oddNumbersSorted = NumberSort(oddNumbers, ascending);
            var numbersSorted = new List<int>();
            numbersSorted.AddRange(evenNumberSorted);
            numbersSorted.AddRange(oddNumbersSorted);
            return numbersSorted;
        }
    }
}
