// Program that takes program names as command line arguments and 
// makes sure they are always executing. So when they die, this
// program restarts them.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections;

namespace Lab10_NET
{
    class Program
    {
        static void Main(string[] args)
        {

            if (args.Length < 1)
            {
                Console.WriteLine("Not enough arguments");
                return;
            }

            int count = 0;

            // launch each program from command line
            int [] processIDs = new int[100];
            foreach (string arg in args)
            {
                Process process = Process.Start(arg);
                processIDs[count] = process.Id;
                count++;
            }
            count = 0;
            int end = processIDs.Length;

            // infinite loop to continue opening programs
            while (true)
            {
                foreach (string arg in args)
                {
                    try
                    {
                        Process.GetProcessById(processIDs[count]);
                    }
                    catch (ArgumentException)
                    {
                        // if process is no longer running, restart and 
                        // store new ID number
                        Process p = Process.Start(arg);
                        processIDs[count] = p.Id;
                    }
                    count++;
                }
                // reset count to continue looping through processIDs array
                count = 0;
            }

        }
    }
}
