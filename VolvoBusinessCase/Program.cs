using System;
using System.Collections.Generic;
using System.IO;

namespace VolvoBusinessCase
{
    class Program
    {
        static void Main(string[] args)
        {
            string lastStringsPath = Path.Combine(System.IO.Directory.GetCurrentDirectory(),"LastInput.txt");
            bool assignedCorrectly = false;
            bool loadedLastSequence = false;
            int q = 0;

            List<string> strings = new List<string>();
            List<StringData> outputList = new List<StringData>();
            string inputStringsAmount = "";
            string tempString;
            Console.WriteLine("Specify how many lines would you like to assess, between 1 and 5.");
            Console.WriteLine("You can also use keyword \"last\" to use the lines from the last query");
            Console.Write("Number of lines = ");
            while (!assignedCorrectly)
            {
                inputStringsAmount = Console.ReadLine();
                inputStringsAmount = inputStringsAmount.ToLower();
                if (inputStringsAmount == "last")
                {
                    try
                    {
                        strings = new List<string>(File.ReadAllLines(lastStringsPath));
                        loadedLastSequence = true;
                        assignedCorrectly = true;
                        //break;
                    }
                    catch(FileNotFoundException e )
                    {
                        strings = new List<string>();
                        Console.WriteLine("No file found");
                    }
                    
                }
                try
                {
                    q = Int32.Parse(inputStringsAmount);
                    if (q >= 0 && q <= 5)
                    {
                        assignedCorrectly = true;
                    }
                    else
                    {
                        Console.WriteLine("Number of lines should be in range 1-5, including those values");
                    }
                }
                catch (FormatException e)
                {
                    q = 0;
                    Console.WriteLine("Not an integer");
                }
                
                
            }
            Console.WriteLine(" ");
            if (!loadedLastSequence)
            {
                Console.WriteLine("Enter your lines, length should be in range 1-100, including those values");
                for (int i = 0; i < q; i++)
                {
                    tempString = Console.ReadLine();
                    if (isCorrectFormating(tempString))
                    {

                        if (tempString.Length >= 1 && tempString.Length <= 100)
                        {
                            strings.Add(tempString.ToUpper());
                        }
                        else
                        {
                            Console.WriteLine("Invalid string length, enter a new string:");
                            q++;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Incorrect formatting, strings should only contain characters \'H\' and \'E\'");
                        q++;
                    }
                    
                    
                    

                }
                File.WriteAllLines(lastStringsPath, strings);
            }
            
                foreach (string line in strings)
                {
                    outputList.Add(AlternateCharacters(line));
                }
                for(int i = 0;i<strings.Count;i++)
                {
                    Console.WriteLine("Input string = " + strings[i]);
                    Console.WriteLine("Output string = " + outputList[i].s);
                    Console.WriteLine("Number of deletions : " + outputList[i].numberOfDeletions);
                    Console.WriteLine(" ");
                }
            
        }

        public static StringData AlternateCharacters(string s)
        {
            int pointerCurrent;
            int pointerOneAfterCurrent;
            int counter = 0;
            string newString = "";
            for(int i=0;i<s.Length;i++)
            {
                pointerCurrent = s[i];
                if(i == s.Length-1)
                {
                    newString = newString + s[i].ToString();
                    break;
                }
                pointerOneAfterCurrent = s[i+1];
                if(pointerCurrent != pointerOneAfterCurrent)
                {
                    newString = newString + s[i].ToString();
                    //counter++;
                }
                else
                {
                    counter++;
                }
                
            }
            StringData strd = new StringData(counter, newString);
            return strd;
        }

        public static bool isCorrectFormating(string s)
        {
            char[] acceptedCharacters = { 'H', 'E' };
            bool isCorrect = true;
            s = s.ToUpper();
            foreach (char c in s)
            {
                if (!Array.Exists(acceptedCharacters, character => character == c))
                {
                    isCorrect = false;
                    break;
                }
            }


            return isCorrect;
        }


        public class StringData
        {
            public int numberOfDeletions;
            public string s;
            public StringData(int n, string str)
            {
                numberOfDeletions = n;
                s = str;
            }
        }
    }
}
