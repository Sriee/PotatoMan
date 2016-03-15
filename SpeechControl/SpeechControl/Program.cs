using System;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Speech.Recognition;
using System.Reflection;
using System.Windows.Forms;

namespace SpeechControl
{
    static class Program
    {

        public static string endWord = "Exit";  //Default
        public static bool flag = true; //Default - Speech Recognition Enabled 

        static void Main(string[] args)
        {

            Console.WriteLine("## Starting Speech Recognition...");

            //Initializing the speech recognition engine
            SpeechRecognitionEngine speechRecognizer = new SpeechRecognitionEngine();

            //Adds the grammar text 
            Choices gameControls = new Choices();
            try
            {
                try
                {
                    string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\grammar.txt";

                    string[] txt = File.ReadAllLines(path);
                    foreach (string line in txt)
                    {
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
                                Console.WriteLine("## Speech Recognition: Disabled, Bye!...");
                                System.Environment.Exit(0);
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
                            gameControls.Add(line);
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("File Reader class");
                    Console.WriteLine(e.Message);
                }

                //Loading grammar into the speech recognition engine
                GrammarBuilder gB = new GrammarBuilder();
                gB.Append(gameControls);
                Grammar myGrammar = new Grammar(gB);
                speechRecognizer.LoadGrammar(myGrammar);

                //Registering for speech recognition event notification
                speechRecognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(speechRecognized);

                speechRecognizer.SetInputToDefaultAudioDevice();
                speechRecognizer.RecognizeAsync(RecognizeMode.Multiple);


                Console.WriteLine("## Ready....");
                while (true)
                {
                    Thread.Sleep(10);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                speechRecognizer.RecognizeAsyncStop();
                //Disposing the speech recogniton object
                speechRecognizer.Dispose();
                System.Environment.Exit(0);
            }
            finally
            {
                if (speechRecognizer != null) speechRecognizer.Dispose();
            }

        }//End of Main

        /// <summary>
        /// Handler for the speech recognition event
        /// </summary>
        static void speechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string detectedWord = "";
            if (flag == true)
            {
                detectedWord = e.Result.Text;
                Console.WriteLine(detectedWord);
                if (e.Result.Text == endWord)
                {
                    Console.WriteLine("## Stoping Recognition, Bye:)");
                    System.Environment.Exit(0);
                }
                else
                {
                    switch (detectedWord)
                    {
                        case "Jump":
                            SendKeys.SendWait(" ");
                            break;
                        case "Left":
                        case "left":
                            SendKeys.SendWait("A");
                            break;
                        case "Right":
                        case "right":
                            SendKeys.SendWait("D");
                            break;
                        case "Fire":
                        case "fire":
                            SendKeys.SendWait("X");
                            break;
                        case "Bomb":
                        case "bomb":
                            SendKeys.SendWait("Z");
                            break;
                        case "Move Left":
                            for (int i = 0; i < 20; i++) SendKeys.SendWait("A");
                            break;
                        case "Move Right":
                            for (int i = 0; i < 20; i++) SendKeys.SendWait("D");
                            break;
                    }//End of switch case
                }//End of else
            }
        }//End of speech Recognized  

    }//End of program
}//End of namespace
