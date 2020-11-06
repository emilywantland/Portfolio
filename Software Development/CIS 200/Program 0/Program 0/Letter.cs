// Grading ID: M1791
// Program #: 0
// Due Date: 9/9/2019
// Course Section: CIS 200-01
// Description: Models letters, has a fixed cost, origin address, and destination address

using System;

namespace Program_0
{
    class Letter : Parcel
    {
        private decimal _fixedCost; // Fixed cost backing field
        private const decimal MIN_FIXED_COST = 0; // Minimum fixed cost value

        // Three-Parameter Constructor 
        public Letter(Address originAddress, Address destinationAddress, decimal fixedCost)
            : base(originAddress, destinationAddress)
        {
            FixedCost = fixedCost; // Validates fixed cost via property
        }

        // Property that gets and sets the fixed cost
        protected decimal FixedCost
        {
            // Precondition:  None
            // Postcondition: The fixed cost has been returned
            get
            {
                return _fixedCost;
            }

            // Precondition:  Must be a positive number - Exception thrown if negative
            // Postcondition: The fixed cost has been set to the specified value
            set
            {
                if (value > MIN_FIXED_COST)
                {
                    _fixedCost = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, $"Fixed cost must be greater than 0");
                }
            }
        }

        // Calculate fixed cost
        public override decimal CalcCost() => FixedCost;

        // Return string representation of Letter object, using properties
        public override string ToString() => $"Fixed Cost: {FixedCost:C}\n\n" +
                                             $"Origin Address:\n{OriginAddress}\n" +
                                             $"Destination Address:\n{DestinationAddress}";
    }
}