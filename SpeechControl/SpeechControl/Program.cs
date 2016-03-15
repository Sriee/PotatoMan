using System;
using System.Diagnostics;
using System.Speech.Recognition;
using System.IO;
using System.Reflection;

namespace SpeechControl
{
    class Program
    {
        //Declaring member variables to be used for the program
        
        public static string endWord = "Exit";  //Default
        

        public static void Main(string[] args)
        {
            Console.WriteLine("## Starting Speech Recognition##");

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

        }//End of main

        /// <summary>
        /// Handler for the speech recognition event
        /// </summary>
        static void speechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            
            Console.WriteLine(detectedWord);
            
        }//End of speechRecognized
    }//End of class 
}//End of SpeechControl
