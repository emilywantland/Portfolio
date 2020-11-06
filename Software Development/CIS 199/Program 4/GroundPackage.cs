// Grading ID: N2466
// Section: CIS 199-75
// Due Date: 4-23-2019
// Program: 4
// Description: Displays original package information and changes data

using System;

namespace Program4
{
    public class GroundPackage // Doesn't produce output, does calculations
    {
        // Declaring variables
        public int _originZip;      // Package's origin zip
        public int _destinationZip; // Package's destination zip
        public double _length;      // Package's length
        public double _width;       // Package's width
        public double _height;      // Package's height
        public double _weight;      // Package's weight
        public int _zoneDistance;   // Zone distance

        const double SIZE_COST_FACTOR = 0.25;   // Size cost factor constant variable
        const double WEIGHT_COST_FACTOR = 0.45; // Weight cost factor constant variable

        public int OriginZip
        {
            // Precondition:  None
            // Postcondition: The packages's origin zip is returned
            get { return _originZip; }

            // Precondition:  The value must be greater or equal to 00000 and less than or equal to 99999
            // Postcondition: The packages's origin zip is set to the specified value
            set
            {
                if (value >= 00000 && value <= 99999)
                {
                    _originZip = value;
                }
                else
                {
                    _originZip = 40202;
                }
            }
        }

        public int DestinationZip
        {
            // Precondition:  None
            // Postcondition: The packages's destination zip is returned
            get { return _destinationZip; }

            // Precondition:  The value must be greater or equal to 00000 and less than or equal to 99999
            // Postcondition: The packages's destination zip is set to the specified value
            set
            {
                if (value >= 00000 && value <= 99999)
                {
                    _destinationZip = value;
                }
                else
                {
                    _destinationZip = 90210;
                }
            }
        }

        public double Length
        {
            // Precondition:  None
            // Postcondition: The packages's Length is returned
            get { return _length; }

            // Precondition:  The value must be greater than 0
            // Postcondition: The packages's length is set to the specified value
            set
            {
                if (value > 0)
                {
                    _length = value;
                }
                else
                {
                    _length = 1.0;
                }
            }
        }

        public double Width
        {
            // Precondition:  None
            // Postcondition: The packages's weight is returned
            get { return _width; }

            // Precondition:  The value must be greater than 0
            // Postcondition: The packages's weight is set to the specified value
            set
            {
                if (value > 0)
                {
                    _width = value;
                }
                else
                {
                    _width = 1.0;
                }
            }
        }

        public double Height
        {
            // Precondition:  None
            // Postcondition: The packages's width is returned
            get { return _height; }

            // Precondition:  The value must be greater than 0
            // Postcondition: The packages's width is set to the specified value
            set
            {
                if (value > 0)
                {
                    _height = value;
                }
                else
                {
                    _height = 1.0;
                }
            }
        }

        public double Weight
        {
            // Precondition:  None
            // Postcondition: The packages's weight is returned
            get { return _weight; }

            // Precondition:  The value must be greater than 0
            // Postcondition: The packages's weight is set to the specified value
            set
            {
                if (value > 0)
                {
                    _weight = value;
                }
                else
                {
                    _weight = 1.0;
                }
            }
        }

        // Precondition:  None
        // Postcondition: The package is constructed
        public GroundPackage(int originZip, int destinationZip, double length, 
                             double width, double height, double weight)
        {
            OriginZip = originZip;
            DestinationZip = destinationZip;
            Length = length;
            Width = width;
            Height = height;
            Weight = weight;
        }

        // Precondition: None
        // Postcondition: The zone distance is calculated
        public int ZoneDist
        {
            get
            {
                return Math.Abs((OriginZip / 10000) - (DestinationZip / 10000));
            }
        }

        // Virtual method to be overridden by derived classes
        // Precondition:  None
        // Postcondition: The package's cost is calculated and returned
        public virtual double CalcCost()
        {
            double cost = SIZE_COST_FACTOR * (Length + Width + Height)
                          + WEIGHT_COST_FACTOR * (ZoneDist + 1) * (Weight);
            return cost;
        }

        // Precondition:  None
        // Postcondition: The packages's information is returned as a formatted string
        public override string ToString() // Override is required!
        {
            return $"Origin Zip:      {OriginZip}{Environment.NewLine}" +
                   $"Destination Zip: {DestinationZip}{Environment.NewLine}" +
                   $"Package Length:  {Length}{Environment.NewLine}" +
                   $"Package Width:   {Width}{Environment.NewLine}" +
                   $"Package Height:  {Height}{Environment.NewLine}" +
                   $"Package Weight:  {Weight}{Environment.NewLine}" +
                   $"Zone Distance:   {ZoneDist}";
        }
    }
}