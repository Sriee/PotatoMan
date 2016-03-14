using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace SpeechControl
{
    class Program
    {
        public static int port = 26000; //Default
        public static string ipServer = "192.168.0.255"; //Default
        public static string endWord = "Exit";  //Default

        public static void Main(string[] args)
        {
            Console.WriteLine("## Starting Speech Recognition##");

            bool flag = true;

            try
            {

                string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\grammar.txt";

                string[] txt = File.ReadAllLines(path);
                foreach (string line in txt)
                {
                    //Extracting port number from user
                    if (line.StartsWith("--P"))
                    {
                        var text = line.Split(new char[] { ' ' });
                        port = Convert.ToInt32(text[1]);
                        Console.WriteLine("## Specified Port:" + port);
                        continue;
                    }
                    //Extracting IP address from user
                    if (line.StartsWith("--I"))
                    {
                        var text = line.Split(new char[] { ' ' });
                        ipServer = text[1];
                        Console.WriteLine("## IP Address:" + ipServer);
                        continue;
                    }
                    //Enable Speech Recognition 
                    if (line.StartsWith("--E"))
                    {
                        var text = line.Split(new char[] { ' ' });
                        if (text[1].Equals("Enable", StringComparison.OrdinalIgnoreCase))
                        {
                            flag = true;
                            Console.WriteLine("## Speech Recognition: Enabled");
                            continue;
                        }
                        else {
                            flag = false;
                            Console.WriteLine("## Speech Recognition: Disabled");
                            continue;
                        }
                    }
                    //End Word to stop recognition 
                    if (line.StartsWith("--T"))
                    {
                        var text = line.Split(new char[] { ' ' });
                        Console.WriteLine("!!Default End word changed from \"" + endWord + "\" to \"" + text[1] + "\"");
                        continue;
                    }
                    //Removing empty lines in the text 
                    if (line == string.Empty) continue;
                    //Ignoring the comments in the grammar file
                    if (line.StartsWith("!-")) continue;
                    else
                        Console.WriteLine(line);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("File Reader class");
                Console.WriteLine(e.Message);
            }
        }//End of main
    }//End of class 
}//End of Test
