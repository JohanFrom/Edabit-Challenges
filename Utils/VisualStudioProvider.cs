﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdabitChallenges.Utils
{
    public static class VisualStudioProvider
    {
        private static readonly Dictionary<string, int> _solutions = new()
        {
            {"VeryEasy", 0},
            {"Easy", 0},
            {"Medium", 0},
            {"Hard", 0},
            {"VeryHard", 0},
            {"Expert", 0}
        };

        private static readonly HashSet<string> _exludedDirs = new()
        {
            ".git",
            ".github",
            ".vs",
            "bin",
            "obj",
            "Utils"
        };

        /// <summary>
        /// Method for counting the completed challenges in this solution
        /// </summary>
        public static void CountCompletedChallenges()
        {
            string netFolder = new DirectoryInfo(Directory.GetCurrentDirectory()).ToString();
            string startDirectory = Directory.GetParent(netFolder)!.Parent!.Parent!.FullName;
            FileInfo[] fileInfo;
            DirectoryInfo directoryInfo;

            foreach (KeyValuePair<string, int> folder in _solutions)
            {
                directoryInfo = new(startDirectory + "//" + folder.Key);
                fileInfo = directoryInfo.GetFiles("*", SearchOption.AllDirectories);
                _solutions[folder.Key] = fileInfo.Length;
            }

            foreach (KeyValuePair<string, int> folder in _solutions)
            {
                if (folder.Key.Contains("Very"))
                {
                    Console.WriteLine("Difficulty: " + folder.Key.Insert(4, " ") + Environment.NewLine +
                    "Completed: " + folder.Value.ToString() + Environment.NewLine);
                }
                else
                {
                    Console.WriteLine("Difficulty: " + folder.Key + Environment.NewLine +
                    "Completed: " + folder.Value.ToString() + Environment.NewLine);
                }
            }
            Console.Write("Total completed challenges: ");
            Console.Write(_solutions.Values.Sum(x => x).ToString(), Console.ForegroundColor = ConsoleColor.Green);
            Console.WriteLine(Environment.NewLine, Console.ForegroundColor = ConsoleColor.White);
        }

        /// <summary>
        /// Method for finding a specific or multiple challenges based on a challenge name or a url
        /// </summary>
        /// <param name="challengeName"></param>
        /// <param name="challengeUrl"></param>
        /// <returns>String of file path/s</returns>
        public static string SearchChallengeSolution(string? challengeName, string? challengeUrl)
        {
            if (challengeName == null && challengeUrl == null)
                return "Must enter either a challenge name or a challenge url!";

            if (challengeName == string.Empty && challengeUrl == string.Empty)
                return "Must enter either a challenge name or a challenge url!";

            StringBuilder sb = new();
            sb.AppendLine("");
            string netFolder = new DirectoryInfo(Directory.GetCurrentDirectory()).ToString();
            DirectoryInfo startDirectory = Directory.GetParent(netFolder)!.Parent!.Parent!;
            var dirs = startDirectory.GetDirectories().Where(x => !_exludedDirs.Contains(x.ToString().Split("\\").Last()));

            foreach (var dir in dirs)
            {
                string[] files = Directory.GetFiles(dir.ToString(), "*.cs");
                foreach (var file in files)
                {
                    var fileContent = File.ReadLines(file.ToString()).Take(12);

                    foreach (var line in fileContent)
                    {
                        if (challengeName != null && challengeName != string.Empty)
                        {
                            if (line.Contains(challengeName))
                            {
                                if (!sb.ToString().Contains(file))
                                    sb.AppendLine(file);
                            }
                        }
                        if (challengeUrl != null && challengeUrl != string.Empty)
                        {
                            if (line.Contains(challengeUrl))
                            {
                                sb.AppendLine(file);
                                break;
                            }
                        }
                    }
                }
            }

            return sb.ToString() == string.Empty ? "Could not find a path to a challenge!" : sb.ToString();
        }
    }
}
