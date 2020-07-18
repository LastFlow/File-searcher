using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Windows.Forms;
using static System.Console;

namespace Geg
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                WriteLine("First argument - path \nSecond argument - name of file");
                
            }
            else
            {
                Search(args[0], args[1]);
            }
        }
        public static void Search(string path, string file)
        {
            bool isFound = false;
            // What to search.
            string FileParameter = file;
            // Where to search. 
            string Path = path;
            Stack<string> StackDirs = new Stack<string>();

            StackDirs.Push(Path);

            while (StackDirs.Count > 0)
            {
                if (!isFound)
                {
                    string curr = StackDirs.Pop();

                    try
                    {
                        string[] subDirs = Directory.GetDirectories(curr);

                        foreach (string d in subDirs)
                        {
                            StackDirs.Push(d);
                        }
                    }
                    catch (UnauthorizedAccessException e)
                    {
                        WriteLine(e.Message);
                        continue;
                    }
                    catch (DirectoryNotFoundException e)
                    {
                        WriteLine(e.Message);
                        continue;
                    }


                    string[] files;
                    try
                    {
                        files = Directory.GetFiles(curr, FileParameter);

                        foreach (string f in files)
                        {
                            FileInfo fi = new FileInfo(f);
                            if (fi.Name == FileParameter)
                            {
                                WriteLine(fi.FullName);
                                isFound = true;
                                break;
                            }

                        }
                    }
                    catch (FileNotFoundException e)
                    {
                        WriteLine(e.Message);
                        break;
                    }
                    catch (UnauthorizedAccessException e)
                    {
                        WriteLine(e.Message);
                        continue;
                    }

                }
                else
                {
                    WriteLine("\n========FINISH=========");
                    break;
                    
                
                }
            }
        }
    }
}