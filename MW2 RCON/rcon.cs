﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;

namespace MW2_RCON
{
    class rcon
    {
        public static volatile ErrorLog Logger = new ErrorLog();
        public static volatile DisplayBox Full = new DisplayBox();

        private static void Main()
        {
            // File locations
            string path = @"RCON.log";
            string Settings = @"Settings.ini";

            // Version and built date theads
            DateTime buildDate = new FileInfo(Assembly.GetExecutingAssembly().Location).LastWriteTime;
            string BUILDNUMBER_STR = "1";
            string VERSIONSTRING = ("Zero 1.0-" + BUILDNUMBER_STR);
            string LogFile = DateTime.Now.ToString("yyyy-MM-dd_HH.mm.ss") + ".log";

            string oG7Hq1TSkWWqm9mSAV0FtA = "";
            string xdMsvHqjZ2GKlUd2JXxg = "";
            int MTHmCTouzjZ9BU3wvGPA = 0;
            string MrinAdsKhq0Ol6jlTxIseg = "";

            // Console header
            Console.WriteLine("+-------------------------------------+");
            Console.WriteLine("| Freedom's RCON Tool!                |");
            Console.WriteLine("+-------------------------------------+");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("{0} built on {1}", VERSIONSTRING, buildDate);
            Console.WriteLine("http://FreedomTS.tk");
            Console.ResetColor();
            Console.WriteLine("+-------------------------------------+");
            Console.WriteLine("Enter a command you want to RCON!");
            Console.WriteLine("For help and some simple commands type '!help'");
            Console.WriteLine("+----------------------------------------------+");

            // Console defult title
            Console.Title = "Freedom's RCON Tool";

            // Settings.ini creator
            if (!File.Exists(Settings))
            {
                // Create a file to write to. 
                StreamWriter sw = File.CreateText(Settings);
                sw.WriteLine("//Auto generated by {0} on {1}, do not modify unless you know what you are doing", Environment.MachineName, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                sw.WriteLine("//+-------------------------------------+");
                sw.WriteLine("//| Freedom's RCON Tool! |");
                sw.WriteLine("//| All settings goes here.   |");
                sw.WriteLine("//+-------------------------------------+");
                sw.WriteLine();
                Console.WriteLine();
                Console.Write(">>Enter the IP to the server: ");
                string HDwe6HyoylcUr6OvQk2uA = Console.ReadLine();
                sw.WriteLine("IP={0}", HDwe6HyoylcUr6OvQk2uA);
                Console.Write(">>Enter the server Port: ");
                HDwe6HyoylcUr6OvQk2uA = Console.ReadLine();
                sw.WriteLine("PORT={0}", HDwe6HyoylcUr6OvQk2uA);
                Console.Write(">>Enter the Rcon Password for the server: ");
                HDwe6HyoylcUr6OvQk2uA = Console.ReadLine();
                sw.WriteLine("PASSWORD={0}", HDwe6HyoylcUr6OvQk2uA);
                sw.WriteLine("CONSOLE_NAME=^0[1ADMIN^0]");
                sw.Close();
                Console.WriteLine("Saved Settings.ini");
            }

            // Settings.ini reader
            using (StreamReader reader = new StreamReader(Settings))
            {
                try
                {
                    while (!reader.EndOfStream)
                    {
                        string str = reader.ReadLine();
                        if (!str.StartsWith("//") && !str.Equals(string.Empty))
                        {
                            string[] strArray = str.Split(new char[] { '=' });
                            if (strArray.Length >= 1)
                            {
                                string str2 = strArray[0];
                                switch (str2)
                                {
                                    case "IP":
                                        {
                                            xdMsvHqjZ2GKlUd2JXxg = strArray[1];
                                            break;
                                        }
                                    case "PORT":
                                        {
                                            MTHmCTouzjZ9BU3wvGPA = Convert.ToInt32(strArray[1]);
                                            break;
                                        }
                                    case "PASSWORD":
                                        {
                                            oG7Hq1TSkWWqm9mSAV0FtA = strArray[1];
                                            break;
                                        }
                                    case "CONSOLE_NAME":
                                        {
                                            break;
                                        }
                                    default:
                                        {
                                            Console.WriteLine("Slight Problem with this section of code");
                                            break;
                                        }
                                }
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    reader.Close();
                    reader.Dispose();
                    File.Delete(Settings);
                    MessageBox.Show("Fatal error has occurred. \r\nA mimidump has been written to " + LogFile + ".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    Logger.LogError(exception, VERSIONSTRING);
                    Environment.Exit(0);
                    Application.Exit();
                }
                // Says to user settings are loaded
                Console.WriteLine("Loaded Settings.ini");
                Console.WriteLine();
            }

            try
            {
                if (string.IsNullOrEmpty(xdMsvHqjZ2GKlUd2JXxg) == false && MTHmCTouzjZ9BU3wvGPA > 0 && string.IsNullOrEmpty(oG7Hq1TSkWWqm9mSAV0FtA) == false)
                {
                    string hello = rcon.sendCommand("sv_hostname", xdMsvHqjZ2GKlUd2JXxg, oG7Hq1TSkWWqm9mSAV0FtA, MTHmCTouzjZ9BU3wvGPA);
                    hello = hello.Replace("����print", "");
                    hello = hello.Replace("\"sv_hostname\" is: \"", "");
                    hello = hello.Replace("\" default: \"CoD4Host^7\"", "");
                    hello = hello.Replace(" Domain is any text", "[Connected]");
                    hello = hello.Replace("^1", "");
                    hello = hello.Replace("^2", "");
                    hello = hello.Replace("^3", "");
                    hello = hello.Replace("^4", "");
                    hello = hello.Replace("^5", "");
                    hello = hello.Replace("^6", "");
                    hello = hello.Replace("^7", "");
                    hello = hello.Replace("^8", "");
                    hello = hello.Replace("^9", "");
                    hello = hello.Replace("^0", "");
                    Console.Title = hello;
                    if (hello == "Error: Failed to connect to server. [Either server is offline or server is changing map]" || MrinAdsKhq0Ol6jlTxIseg == null)
                    {
                        Console.Title = "Server failed to respond to handshake";
                        Full.Showme("Server failed to respond to handshake. \r\nServer is either offline or server is changing map");
                        //Console.WriteLine("ERROR: Server failed to respond to handshake.");
                        //Console.WriteLine("Press any key to exit");
                        //Console.ReadKey();
                    }
                    else
                    {
                        MrinAdsKhq0Ol6jlTxIseg = "true";
                        Console.Title = hello;
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Fatal error has occurred. \r\nA mimidump has been written to " + LogFile + ".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                Logger.LogError(exception, VERSIONSTRING);
                Environment.Exit(0);
                Application.Exit();
            }

            if (MrinAdsKhq0Ol6jlTxIseg == "true")
            {
                for (; ; )
                {
                    Console.Write(">>RCON: ");
                    string commandz = Console.ReadLine();

                    // Creates the Log file.
                    if (!File.Exists(path))
                    {
                        // Create a file to write to. 
                        StreamWriter sw = File.CreateText(path);
                        sw.WriteLine("//+-------------------------------------+");
                        sw.WriteLine("//| Freedom's RCON Tool v1.0! |");
                        sw.WriteLine("//| Log for RCON              |");
                        sw.WriteLine("//+-------------------------------------+");
                        sw.Close();
                    }

                    using (StreamWriter sw = File.AppendText(path))
                    {
                        DateTime NOW = DateTime.Now;
                        sw.WriteLine(NOW + " - Command: " + commandz);
                    }

                    if (commandz == "")
                    {
                        Console.WriteLine("Error: You have not entered a command.");
                    }
                    else if (commandz.StartsWith("!"))
                    {
                        if (commandz == "!help")
                        {
                            Console.WriteLine("You have found the help guide.");
                        }
                        else
                        {
                            Console.WriteLine("Error: '!' Cannot be used as a rcon command.");
                        }
                    }
                    else
                    {
                        string response = rcon.sendCommand(commandz, xdMsvHqjZ2GKlUd2JXxg, oG7Hq1TSkWWqm9mSAV0FtA, MTHmCTouzjZ9BU3wvGPA);
                        if (commandz.StartsWith("map") && response == "Error: Getting server response!!!")
                        {
                            response = "Log: Server successfully changed map to " + commandz + "";
                        }
                        //response = response.Replace("\n", "");
                        response = response.Replace("\n", "\r\n");
                        response = response.Replace("����print", "");
                        // Removes all colours from text
                        response = response.Replace("^1", "");
                        response = response.Replace("^2", "");
                        response = response.Replace("^3", "");
                        response = response.Replace("^4", "");
                        response = response.Replace("^5", "");
                        response = response.Replace("^6", "");
                        response = response.Replace("^7", "");
                        response = response.Replace("^8", "");
                        response = response.Replace("^9", "");
                        response = response.Replace("^0", "");
                        response = Regex.Replace(response, @"^\s+$[\r\n]*", "", RegexOptions.Multiline);
                        if (response.Length >= 3)
                        {
                            response = response.Remove(response.Length - 2);
                        }
                        Console.WriteLine(response);

                        using (StreamWriter sw = File.AppendText(path))
                        {
                            DateTime NOW = DateTime.Now;
                            sw.WriteLine(NOW + " - Response: \r\n" + response);
                        }
                    }
                }
            }
        }

        public static string sendCommand(string rconCommand, string gameServerIP, string password, int gameServerPort)
        {
            try
            {
                //connecting to server
                Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                client.Connect(IPAddress.Parse(gameServerIP), gameServerPort);

                string command;
                Byte[] bufferRec;
                command = "rcon " + password + " " + rconCommand;
                byte[] bufferTemp = Encoding.ASCII.GetBytes(command);
                byte[] bufferSend = new byte[bufferTemp.Length + 5];

                //intial 5 characters as per standard
                bufferSend[0] = byte.Parse("255");
                bufferSend[1] = byte.Parse("255");
                bufferSend[2] = byte.Parse("255");
                bufferSend[3] = byte.Parse("255");
                bufferSend[4] = byte.Parse("02");
                int j = 5;

                for (int i = 0; i < bufferTemp.Length; i++)
                {
                    bufferSend[j++] = bufferTemp[i];
                }

                //send rcon command and get response
                IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
                client.Send(bufferSend, bufferSend.Length, SocketFlags.None);
                Thread.Sleep(200); // Give server time to respond
                int avail = client.Available;
                if (avail > 0)
                {
                    //big enough to receive response
                    bufferRec = new byte[avail];
                    client.Receive(bufferRec, SocketFlags.None);
                    return System.Text.Encoding.UTF8.GetString(bufferRec);
                }
                return "Error: Getting server response!!!";
            }
            catch (Exception exception)
            {
                return "Error: Failed to connect to server. [Either server is offline or server is changing map]";
            }
        }
    }
}
