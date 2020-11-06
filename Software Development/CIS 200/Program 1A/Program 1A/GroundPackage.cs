// Program 1A
// CIS 200-01
// Fall 2019
// Due: 9/23/2019
// By: M1791

// File: GroundPackage.cs
// Derived from the abstract package class. Ground packages have a zone 
// distance and cost calculation 

using System;
using System.Collections.Generic;
using System.Text;

namespace Program_1A
{
    public class GroundPackage : Package
    {
        // Constants
        const double SIZE_COST_FACTOR = 0.25;   // Size cost factor constant
        const double WEIGHT_COST_FACTOR = 0.45; // Weight cost factor constant
        const int ZIP_DIVISION = 10000;         // Zip code division constant 

        // Precondition:  None
        // Postcondition: The ground package is created with the specified values for
        //                origin address, destination address, length, width, height, and weight
        public GroundPackage(Address originAddress, Address destAddress, double length, 
                             double width, double height, double weight)
            : base(originAddress, destAddress, length, width, height, weight)
        {

        }

        protected int ZoneDistance // Read-only - No Set
        {
            // Precondition:  None
            // Postcondition: The zone distance has been returned
            get
            {
                int zoneDist; // Zone distance variable
                zoneDist = Math.Abs((OriginAddress.Zip / ZIP_DIVISION) - (DestinationAddress.Zip / ZIP_DIVISION));
                return zoneDist;
            }
        }

        // Precondition:  None
        // Postcondition: The ground package's cost has been returned
        public override decimal CalcCost()
        {
            double calculation = (SIZE_COST_FACTOR) * (Length + Width + Height) + (WEIGHT_COST_FACTOR) * (ZoneDistance + 1) * (Weight);
            return (decimal)calculation;
        }

        // Precondition:  None
        // Postcondition: A String with the ground package's data has been returned
        public override string ToString()
        {
            return $"Ground Package\n\n{base.ToString()}" +
                   $"Length: {Length:F1}\n" +
                   $"Width: {Width:F1}\n" +
                   $"Height: {Height:F1}\n" +
                   $"Weight: {Weight:F1}\n" +
                   $"Zone Distance: {ZoneDistance}\n";
        }
    }
}
