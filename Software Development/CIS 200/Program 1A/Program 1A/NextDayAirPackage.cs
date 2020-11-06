// Program 1A
// CIS 200-01
// Fall 2019
// Due: 9/23/2019
// By: M1791

// File: NextDayAirPackage.cs
// Derived from the air package class. Next day air packages include an 
// express fee and different cost calculations

using System;
using System.Collections.Generic;
using System.Text;

namespace Program_1A
{
    public class NextDayAirPackage : AirPackage
    {
        // Backing fields
        private decimal _expressFee; // Express shipping fee value

        // Constants
        const double SIZE_COST_FACTOR = 0.30;       // Size cost factor constant
        const double WEIGHT_COST_FACTOR = 0.25;     // Weight cost factor constant
        const double WEIGHT_AND_SIZE_CHARGE = 0.20; // Weight and size charge constant

        // Precondition:  Length, width, height, and weight > 0
        // Postcondition: The package is created with the specified values for origin address, 
        //                destination address, length, width, height, and weight, express fee
        public NextDayAirPackage(Address originAddress, Address destAddress, double length,
                       double width, double height, double weight, decimal expressFee)
            : base(originAddress, destAddress, length, width, height, weight)
        {
            ExpressFee = expressFee;
        }

        public decimal ExpressFee // Read-only - Private set
        {
            // Precondition:  None
            // Postcondition: The express fee has been returned
            get
            {
                return _expressFee;
            }
            // Precondition:  Value must be >= 0
            // Postcondition: The express fee has been privately set
            private set
            {
                if (value >= 0)
                {
                    _expressFee = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("ExpressFee", value, "Express fee must be >= 0");
                }
            }
        }

        // Precondition:  None
        // Postcondition: The next day air package's cost has been returned
        public override decimal CalcCost()
        {
            double baseCost = SIZE_COST_FACTOR * (Length + Width + Height) + WEIGHT_COST_FACTOR * (Weight) + (double)ExpressFee;

            double weightCharge = WEIGHT_AND_SIZE_CHARGE * (Weight);

            double sizeCharge = WEIGHT_AND_SIZE_CHARGE * (Length + Width + Height);

            if (IsHeavy())
            {
                return (decimal)(baseCost += weightCharge);
            }
            if (IsLarge())
            {
                return (decimal)(baseCost += sizeCharge);
            }
            else
            {
                return (decimal)baseCost;
            }
        }

        // Precondition:  None
        // Postcondition: A String with the next day air package's data has been returned
        public override string ToString()
        {
            return $"Next Day Air Package\n\n{base.ToString()}";
        }
    }
}
