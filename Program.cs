using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using static System.Console;

namespace FileSearcher
{
    class Program
    {
        
        static void Main(string[] args)
        {
            
            Stopwatch stw = new Stopwatch();
            stw.Start();

            if(args.Length <= 0)
            {
                WriteLine("\n     Usage\n     FileSearcher PATH SEARCHPATTERN/NAME\n     FileSearhcer NAMEFILE - searching in current directory");
            }
            else if (args.Length == 1)
            {
                Find(args[0]);
            }


            stw.Stop();
            if(stw.ElapsedMilliseconds > 1000)
            {
                WriteLine($"Time: {Convert.ToDouble(stw.ElapsedMilliseconds / 1000)} sec");
            }
            else
            {
                WriteLine($"Time: {stw.ElapsedMilliseconds} ms");
            } 
        }
        private static void Find(string pattern)
        {
            int j = 0;
            Stack<string> dirs = new Stack<string>();
            dirs.Push(Directory.GetCurrentDirectory());
            while (dirs.Count > 0)
            {
                try
                {
                    foreach (string d in Directory.GetDirectories(dirs.Pop()))
                    {

                        dirs.Push(d);
                        j++;
                        //WriteLine(d);
                    }
                    try
                    {
                        foreach (string f in Directory.GetFiles(dirs.Pop(), pattern))
                        {
                            FileInfo fi = new FileInfo(f);
                            WriteLine($"File: {fi.FullName}");
                            if (fi.FullName.Length < 1)
                            {
                                WriteLine("File not found");
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        WriteLine(e.Message);
                        continue;
                    }

                }


                catch(Exception e)
                {
                    WriteLine(e.Message);
                    continue;
                }

                
            }
            WriteLine($"Searched total : {j} folders");
        }
    }
}
