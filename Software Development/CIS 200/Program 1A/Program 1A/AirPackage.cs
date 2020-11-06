// Program 1A
// CIS 200-01
// Fall 2019
// Due: 9/23/2019
// By: M1791

// File: AirPackage.cs
// Derived from the abstract package class. Air packages include a 
// measurement for heaviness and size

using System;
using System.Collections.Generic;
using System.Text;

namespace Program_1A
{
    public abstract class AirPackage : Package
    {
        // Precondition:  None
        // Postcondition: The air package is created with the specified values for
        //                origin address, destination address, length, width, height, and weight
        public AirPackage(Address originAddress, Address destAddress, double length,
                             double width, double height, double weight)
            : base(originAddress, destAddress, length, width, height, weight)
        {

        }

        // Precondition:  Package weight must be greater or equal to 50 pounds to be true
        // Postcondition: Returns boolean
        public bool IsHeavy()
        {
            if (Weight >= 50)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Precondition:  Package size must be greater or equal to 75 inches to be true
        // Postcondition: Returns boolean
        public bool IsLarge()
        {
            if ((Length + Width + Height) >= 75)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Precondition:  None
        // Postcondition: A String with the air package's data has been returned
        public override string ToString()
        {
            return $"{base.ToString()}" +
                   $"Length: {Length:F1}\n" +
                   $"Width: {Width:F1}\n" +
                   $"Height: {Height:F1}\n" +
                   $"Weight: {Weight:F1}\n" +
                   $"Heavy: {IsHeavy()}\n" +
                   $"Large: {IsLarge()}\n";
        }
    }
}
