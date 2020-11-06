// Program 1A
// CIS 200-01
// Fall 2019
// Due: 9/23/2019
// By: M1791

// File: Program.cs
// Simple test program for Parcel classes

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Program_1A.TwoDayAirPackage;
using static System.Console;

namespace Program_1A
{
    class Program
    {
        // Precondition:  None
        // Postcondition: Small list of Parcels is created and displayed
        static void Main(string[] args)
        {
            Address a1 = new Address("  John Smith  ", "   123 Any St.   ", "  Apt. 45 ",
                "  Louisville   ", "  KY   ", 40202); // Test Address 1
            Address a2 = new Address("Jane Doe", "987 Main St.",
                "Beverly Hills", "CA", 90210); // Test Address 2
            Address a3 = new Address("James Kirk", "654 Roddenberry Way", "Suite 321",
                "El Paso", "TX", 79901); // Test Address 3
            Address a4 = new Address("John Crichton", "678 Pau Place", "Apt. 7",
                "Portland", "ME", 04101); // Test Address 4

            //Letter l1 = new Letter(a1, a3, 0.50M); // Test Letter 1
            //Letter l2 = new Letter(a2, a4, 1.20M); // Test Letter 2
            //Letter l3 = new Letter(a4, a1, 1.70M); // Test Letter 3

            GroundPackage gp1 = new GroundPackage(a3, a4, 14.0, 10.0, 5.0, 12.5); // Test Ground Package

            NextDayAirPackage ndap1 = new NextDayAirPackage(a1, a3, 25.0, 15.0, 15.0, 85.0, 7.50M); // Test Next Day Air Package

            TwoDayAirPackage tdap1 = new TwoDayAirPackage(a4, a1, 46.5, 39.5, 28.0, 80.5, Delivery.Saver); // Test Two Day Air Package

            List<Parcel> parcels = new List<Parcel>(); // Test list of parcels

            //parcels.Add(l1);
            //parcels.Add(l2);
            //parcels.Add(l3);

            // Add test data to list
            parcels.Add(gp1);
            parcels.Add(ndap1);
            parcels.Add(tdap1);
 
            // Display data
            WriteLine("Program 1A - List of Packages");
            WriteLine("-----------------------------\n");

            foreach (Parcel p in parcels)
            {
                WriteLine(p);
                WriteLine("-----------------------------\n");
            }
        }
    }
}
