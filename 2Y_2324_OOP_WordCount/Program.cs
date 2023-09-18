using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2Y_2324_OOP_WordCount
{
    internal class Program
    {
        static Dictionary<string, int> _breakDown = new Dictionary<string, int>();

        static void Main(string[] args)
        {
            //string fileName = "Jupiter.txt";
            string fileName = "Odin.txt";

            string outputFile = fileName.Split('.')[0] + "_WordCount.txt";
            int totalWordCount = 0;

            lineManager(fileReader(fileName), out totalWordCount);

            fileWriter(outputFile, fileName, totalWordCount, wordsSort());

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static List<string> fileReader(string path)
        {
            List<string> lines = new List<string>();

            Console.Write("Begin Reading...");

            string line = "";
            using (StreamReader sr = new StreamReader(path))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Length > 0)
                        lines.Add(line);
                }
            }

            return lines;
        }

        static void lineManager(List<string> lines, out int totalWordCount)
        {
            int total = 0;
            foreach(string line in lines)
            {
                total += wordSegregate(wordSplitter(line));
            }

            Console.WriteLine("Done!");

            totalWordCount = total;
        }

        static string[] wordSplitter(string words)
        {
            string[] word = words.Split(' ');
            char[] letters = new char[] { };

            for (int x = 0; x < word.Length; x++)
            {
                if (word[x].Length > 0)
                {
                    // special character filter
                    if (word[x].Length > 1)
                    {
                        letters = word[x].ToCharArray();
                        if ((int)letters[letters.Length - 1] == 33 // !
                            || (int)letters[letters.Length - 1] == 44 // ,
                            || (int)letters[letters.Length - 1] == 46 // .
                            || (int)letters[letters.Length - 1] == 63 // ?
                            )
                        {
                            word[x] = "";
                            for (int y = 0; y < letters.Length - 1; y++)
                            {
                                word[x] += letters[y];
                            }
                        }
                    }
                }
            }

            return word;
        }

        static int wordSegregate(string[] words)
        {
            int counter = 0;

            foreach (string word in words)
            {
                if (_breakDown.ContainsKey(word.ToLower()))
                    _breakDown[word.ToLower()] += 1;
                else
                    _breakDown[word.ToLower()] = 1;

                counter++;
            }

            return counter;
        }

        static Dictionary<string, int> wordsSort()
        {
            string sortKey = "";
            int leastCount = 0;

            Dictionary<string, int> sortedBD = new Dictionary<string, int>();

            Console.Write("Begin Sorting...");

            while (_breakDown.Count > 0)
            {
                leastCount = 0;

                foreach (KeyValuePair<string, int> kvp in _breakDown)
                {
                    if (leastCount < kvp.Value)
                    {
                        leastCount = kvp.Value;
                        sortKey = kvp.Key;
                    }
                }

                sortedBD[sortKey] = leastCount;
                _breakDown.Remove(sortKey);
            }

            Console.WriteLine("Done!");

            return sortedBD;
        }

        static void fileWriter(string path, string initialFileName
            , int totalCount, Dictionary<string, int> sortedBD)
        {
            Console.Write("Begin Writing...");

            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine("Word count of {0}.", initialFileName);
                sw.WriteLine("Total Wordcount is {0}.", totalCount);
                foreach (KeyValuePair<string, int> kvp in sortedBD)
                {
                    sw.WriteLine("{0}-{1}", kvp.Key, kvp.Value);
                }
            }
            Console.WriteLine("Done!");
        }


    }
}
