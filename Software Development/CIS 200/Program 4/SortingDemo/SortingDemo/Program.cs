// File: Program.cs
// By: Andrew L. Wright
// This program demonstrates the use of List's Sort method with
// Time2 objects modified to support a natural order
// and the use of List's Sort method with a Comparer

using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace SortingDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Time2 t1 = new Time2(),    // Midnight
            t2 = new Time2(12, 0, 0),  // 12:00 PM
            t3 = new Time2(7, 0, 0),   // 7:00 AM
            t4 = new Time2(23, 0, 0),  // 11:00 PM
            t5 = new Time2(12, 30, 0), // 12:30 PM
            t6 = new Time2(7, 0, 59);  // 7:00:59 AM

            // The list of time objects
            List<Time2> times = new List<Time2>();

            // Add the test data
            times.Add(t1);
            times.Add(t2);
            times.Add(t3);
            times.Add(t4);
            times.Add(t5);
            times.Add(t6);

            WriteLine("Orginal list:");
            foreach (Time2 t in times)
                WriteLine(t);
            Pause();

            times.Sort(); // Sort - uses natural order
            WriteLine("Sorted list (natural order):");
            foreach (Time2 t in times)
                WriteLine(t);
            Pause();

            times.Sort(new DescendingTimeOrder()); // Sort - uses specified Comparer class

            WriteLine("Sorted list (descending natural order) using Comparer:");
            foreach (Time2 t in times)
                WriteLine(t);
            Pause();

        }

        // Precondition:  None
        // Postcondition: Pauses program execution until user presses Enter and
        //                then clears the screen
        public static void Pause()
        {
            WriteLine("Press Enter to Continue...");
            ReadLine();

            Clear(); // Clear screen
        }

    }
}
