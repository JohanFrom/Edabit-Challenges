﻿using EdabitChallenges.VeryEasy;
using EdabitChallenges.Easy;
using EdabitChallenges.Medium;
using EdabitChallenges.Hard;
using EdabitChallenges.VeryHard;
using EdabitChallenges.Expert;
using System.Text;
using EdabitChallenges.Utils;

namespace EdabitChallenges
{
    internal class Program
    { 
        static void Main(string[] args)
        {
            Console.WriteLine("All challenges are ordered in difficulty level.");
            Console.WriteLine("To test the methods, just use the static class name.methodname e.g. TheCollatzConjecture.Collatz()");

            Console.WriteLine(VisualStudioProvider.SearchChallengeSolution("Absolute", null));

            VisualStudioProvider.CountCompletedChallenges();
        }

    }
}