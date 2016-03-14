using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace Test
{
    class Program
    {
        public static int port = 0;
        public static string endWord = "end";
        public static string fileName = "grammar.txt";

        public static void Main(string[] args)
        {
            try
            {
                string[] txt = File.ReadAllLines(fileName);
                foreach (string line in txt)
                {
                    //Extracting port number from user
                    if (line.StartsWith("--P"))
                    {
                        var text = line.Split(new char[] { ' ' });
                        port = Convert.ToInt32(text[1]);
                        Console.WriteLine("!!Specified Port:" + port);
                        continue;
                    }
                    //End Word to stop recognition 
                    if (line.StartsWith("--E"))
                    {
                        var text = line.Split(new char[] { ' ' });
                        Console.WriteLine("!!Default End word changed from \"" + endWord + "\" to \"" + text[1] + "\"");
                        continue;
                    }
                    //Removing empty lines in the text 
                    if (line == string.Empty) continue;
                    //Ignoring the comments in the grammar file
                    if (line.StartsWith("--")) continue;
                    else
                        Console.WriteLine(line);
                }
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Debug.WriteLine("File Reader class");
                Console.WriteLine(e.Message);
            }
        }//End of main
    }//End of class 
}//End of Test
