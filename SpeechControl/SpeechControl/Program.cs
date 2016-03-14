﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Speech.Recognition;
using System.IO;
using System.Speech.Synthesis;
using System.Reflection;
using System.Net;
using System.Net.Sockets;

namespace SpeechControl
{
    class Program
    {
        //Declaring member variables to be used for the program
        public static int port = 26000;
        public static string endWord = "Exit";  //Default
        public static string detectedWord = "#";
        public static string ipServer = "192.168.0.255"; //Default
        public static byte[] data = new byte[512];

        public static void Main(string[] args)
        {
            Console.WriteLine("## Starting Speech Recognition##");

            bool flag = true;

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

                /*************************************************************************************************
                /UDP Communication 
                **************************************************************************************************/
                Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                IPEndPoint iep = new IPEndPoint(IPAddress.Parse(ipServer), port);
                server.Connect(iep);
                if (flag == true)
                {
                    while (true)
                    {
                        if (detectedWord != "#")
                        {
                            data = Encoding.ASCII.GetBytes(detectedWord);
                            Console.WriteLine("Sent word" + detectedWord);
                            server.SendTo(data, iep);
                            detectedWord = "#";
                        }
                    }
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

        }//End of main

        /// <summary>
        /// Handler for the speech recognition event
        /// </summary>
        static void speechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            detectedWord = e.Result.Text;
            Console.WriteLine(detectedWord);
            if (e.Result.Text == endWord)
            {
                Console.WriteLine("## Stoping Recognition, Bye:)");
                System.Environment.Exit(0);
            }
            else
                detectedWord = "#";
        }//End of speechRecognized
    }//End of class 
}//End of SpeechControl
