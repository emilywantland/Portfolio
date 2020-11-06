// Program 1A
// CIS 200-01
// Fall 2019
// Due: 9/23/2019
// By: M1791

// File: Package.cs
// This classes stores the length, width, height, and weight
// of a package. Abstract and derived from the parcel class

using System;
using System.Collections.Generic;
using System.Text;

public abstract class Package : Parcel
{
    // Backing fields
    private double _length; // Length of package value
    private double _width;  // Width of package value
    private double _height; // Height of package value
    private double _weight; // Weight of package value

    // Precondition:  Length, width, height, and weight > 0
    // Postcondition: The package is created with the specified values for
    //                origin address, destination address, length, width, height, and weight
    public Package(Address originAddress, Address destAddress, double length, 
                   double width, double height, double weight)
        : base(originAddress, destAddress)
    {
        Length = length;
        Width = width;
        Height = height;
        Weight = weight;
    }

    protected double Length
    {
        // Precondition:  None
        // Postcondition: The package's length has been returned
        get
        {
            return _length;
        }

        // Precondition:  value > 0
        // Postcondition: The package's length has been set to the specified value
        set
        {
            if (value > 0)
            {
                _length = value;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Length", value, "Length in inches must be > 0");
            }
        }
    }

    protected double Width
    {
        // Precondition:  None
        // Postcondition: The package's width has been returned
        get
        {
            return _width;
        }

        // Precondition:  value > 0
        // Postcondition: The package's width has been set to the specified value
        set
        {
            if (value > 0)
            {
                _width = value;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Width", value, "Width in inches must be > 0");
            }
        }
    }

    protected double Height
    {
        // Precondition:  None
        // Postcondition: The package's height has been returned
        get
        {
            return _height;
        }

        // Precondition:  value > 0
        // Postcondition: The package's height has been set to the specified value
        set
        {
            if (value > 0)
            {
                _height = value;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Height", value, "Height in inches must be > 0");
            }
        }
    }

    protected double Weight
    {
        // Precondition:  None
        // Postcondition: The package's weight has been returned
        get
        {
            return _weight;
        }

        // Precondition:  value > 0
        // Postcondition: The package's weight has been set to the specified value
        set
        {
            if (value > 0)
            {
                _weight = value;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Weight", value, "Weight in pounds must be > 0");
            }
        }
    }

    // Precondition:  None
    // Postcondition: A String with the package's data has been returned
    public override string ToString()
    {
        return $"{base.ToString()}\n";
    }
}
