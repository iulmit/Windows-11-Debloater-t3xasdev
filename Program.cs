using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;

namespace Windows_11_Debloater
{
    internal class Program
    {
        static void say(string message) { Console.Write(message); }
        static void sayl(string message) {Console.WriteLine(message);}
        static void pause() { Console.ReadKey(); }
        static void clear() { Console.Clear(); }
        static void Main(string[] args){ Menu(); }
        static void del(string args)
        {
            Process pi = new Process();
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.FileName = "Powershell.exe";
            psi.Arguments = args;
            psi.Verb = "runas";
            pi.StartInfo = psi;
            pi.Start();
        }

        static void Menu()
        {
            sayl("By using this software you agree that in no way is 4nglerdev responsible for damage to your device.\nEverything you do from this point on is on you if the software fails.\nPress any key to continue...");
            pause();
            clear();
            var reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");

            var currentBuildStr = (string)reg.GetValue("CurrentBuild");
            var currentBuild = int.Parse(currentBuildStr);
            if (currentBuild >= 22000)
            {
                sayl("1.)Quick Mode(Uninstalls instantly)\n2.)Safe Mode(Asks what to uninstall)\n\n");
                say("Please select an option [>] ");
                switch (Console.ReadLine())
                {
                    case "1":
                        say("Are you want to do quick mode? (y/n) [>] ");
                        if(Console.ReadLine() == "y")
                        {
                            QuickMode();
                        }
                        else
                        {
                            Environment.Exit(0);
                        }
                        break;
                    case "2":
                        SafeUninstall();
                        break;
                }

                pause();
            }
            else
            {
                sayl("This tool was not developed to work with your operating system/build of windows.\nPlease upgrade to use this tool.\n(If you are using Windows 11 it might be cause your using an insider build, but that is not proven)\n\nPress any key to exit..." + $"Your current build: {currentBuild}");
                pause();
                Environment.Exit(0);
            }

        }
        static void QuickMode()
        {
            string[] num = File.ReadAllLines(Environment.CurrentDirectory + "/debloat.txt");

            for (int i = 0; i < num.Length; i++)
            {
                string[] help = num[i].Split(':');
                string app = help[0];
                string command = help[1];
                try
                {

                    del(command);
                    sayl("[+] Uninstalled: " + app);


                }
                catch (Exception ex) { sayl("[-] An Error Occured - " + ex); }

            }
            sayl("Debloated! Press Any Key To Exit..."); pause(); Environment.Exit(0);
        }
        static void SafeUninstall()
        {
            string[] num = File.ReadAllLines(Environment.CurrentDirectory + "/debloat.txt");

            for (int i = 0; i < num.Length; i++)
            {
                clear();
                string[] help = num[i].Split(':');
                string app = help[0];
                string command = help[1];
                say("Uninstall: " + app + "? (y/n) [>] ");
                try
                {
                    if (Console.ReadLine() == "y")
                    {
                        del(command);
                        sayl("[+] Uninstalled: " + app);
                    }
                    else
                    {
                        sayl("[-] Didn't uninstall: " + app);
                    }
                }
                catch (Exception ex) { sayl("[-] An Error Occured - " + ex); }


            }
                sayl("Debloated! Press Any Key To Exit..."); pause(); Environment.Exit(0);
            }

    }
}
