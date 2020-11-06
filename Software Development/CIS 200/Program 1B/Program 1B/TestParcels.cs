// Program 1B
// CIS 200-01
// Fall 2019
// Due: 10/2/2019
// By: M1791

// File: TestParcels.cs
// This is a simple, console application designed to exercise the Parcel hierarchy.
// It creates several different Parcels, uses LINQ query filtering, and prints results.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Console;

namespace Prog1
{
    class TestParcels
    {
        // Precondition:  None
        // Postcondition: Parcels have been created and displayed
        static void Main(string[] args)
        {
            // Test Data - Magic Numbers OK
            Address a1 = new Address("John Smith", "123 Any St.", "Apt. 45", "Louisville", "KY", 40202); // Test Address 1
            Address a2 = new Address("Jane Doe", "987 Main St.", "Beverly Hills", "CA", 90210); // Test Address 2
            Address a3 = new Address("James Kirk", "654 Roddenberry Way", "Suite 321", "El Paso", "TX", 79901); // Test Address 3
            Address a4 = new Address("John Crichton", "678 Pau Place", "Apt. 7", "Portland", "ME", 04101); // Test Address 4
            Address a5 = new Address("Microsoft Corporation", "One Microsoft Way", "Redmond", "WA", 98052); // Test Address 5
            Address a6 = new Address("Googleplex", "1600 Amphitheatre Parkway", "Moutain View", "CA", 94043); // Test Address 6
            Address a7 = new Address("IBM North America", "590 Madison Avenue", "New York", "NY", 10022); // Test Address 7
            Address a8 = new Address("HP Inc.", "1501 Page Mill Road", "Office #65", "Palo Alto", "CA", 94304); // Test Address 8

            Letter l1 = new Letter(a1, a2, 3.95M); // Letter test object 1
            Letter l2 = new Letter(a3, a6, 2.35M); // Letter test object 2
            GroundPackage gp1 = new GroundPackage(a4, a8, 14, 10, 5, 12.5); // Ground test object 1
            GroundPackage gp2 = new GroundPackage(a3, a4, 10, 6, 3, 7.5); // Ground test object 2
            NextDayAirPackage ndap1 = new NextDayAirPackage(a5, a3, 25, 15, 15, 5, 7.50M); // Next Day test object 1
            NextDayAirPackage ndap2 = new NextDayAirPackage(a6, a7, 15, 12, 6.5, 10.5, 2.50M); // Next Day test object 2
            NextDayAirPackage ndap3 = new NextDayAirPackage(a8, a5, 17, 11, 8.5, 50.5, 3.50M); // Next Day test object 3
            TwoDayAirPackage tdap1 = new TwoDayAirPackage(a7, a2, 45.5, 40.5, 28.0, 80.5, TwoDayAirPackage.Delivery.Saver);// Two Day test object 1
            TwoDayAirPackage tdap2 = new TwoDayAirPackage(a6, a8, 30.5, 20.5, 25.0, 12, TwoDayAirPackage.Delivery.Early);// Two Day test object 1
            TwoDayAirPackage tdap3 = new TwoDayAirPackage(a7, a1, 10, 15.5, 5.0, 60, TwoDayAirPackage.Delivery.Early);// Two Day test object 3

            // List of test parcels
            List<Parcel> parcels = new List<Parcel>();

            // Populate list
            parcels.Add(l1);
            parcels.Add(l2);
            parcels.Add(gp1);
            parcels.Add(gp2);
            parcels.Add(ndap1);
            parcels.Add(ndap2);
            parcels.Add(ndap3);
            parcels.Add(tdap1);
            parcels.Add(tdap2);
            parcels.Add(tdap3);

            // Order parcels by zip code in a descending order
            var destZipDescending =
               from p in parcels
               orderby p.DestinationAddress.Zip descending
               select p;

            // Display destZipDescending query results
            Console.WriteLine("Results of query destZipDescending:");
            Console.WriteLine("-----------------------------------\n");
            foreach (var p in destZipDescending)
            {
                Console.WriteLine($"{p}\n");
                Console.WriteLine("---\n");
            }

            // Order parcels by cost in an ascending order
            var costAscending =
               from p in parcels
               orderby p.CalcCost() ascending
               select p;

            // Display costAscending query results
            Console.WriteLine("Results of query costAscending:");
            Console.WriteLine("-------------------------------\n");
            foreach (var p in costAscending)
            {
                Console.WriteLine($"{p}\n");
                Console.WriteLine("---\n");
            }

            var typeAscendingCostDescending =
                from p in parcels
                orderby p.GetType().Name.ToString() ascending, p.CalcCost() descending
                select p;

            // Display typeAscendingCostDescending query results
            Console.WriteLine("Results of query typeAscendingCostDescending:");
            Console.WriteLine("-------------------------------------------------\n");
            foreach (var p in typeAscendingCostDescending)
            {
                Console.WriteLine($"{p}\n");
                Console.WriteLine("---\n");
            }

            // Order Airpackages by weight if IsHeavy
            var heavyAirPackageWeightDescending =
                from p in parcels.OfType<AirPackage>()
                where p.IsHeavy()
                orderby p.Weight descending
                select p;

            // Display heavyAirPackageWeightDescending query results
            Console.WriteLine("Results of query heavyAirPackageWeightDescending:");
            Console.WriteLine("-------------------------------------------------\n");
            foreach (var p in heavyAirPackageWeightDescending)
            {
                Console.WriteLine($"{p}\n");
                Console.WriteLine("---\n");
            }
        }
    }
}