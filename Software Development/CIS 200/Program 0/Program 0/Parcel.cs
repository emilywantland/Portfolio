// Grading ID: M1791
// Program #: 0
// Due Date: 9/9/2019
// Course Section: CIS 200-01
// Description: Parcel abstract base class, has origin addresses and destination addresses

using System;

namespace Program_0
{
      abstract class Parcel
    {
        // Backing Fields
        private Address _originAddress;      // Origin Address backing field
        private Address _destinationAddress; // Destination Address backing field

        public Parcel(Address originAddress, Address destinationAddress)
        {
            OriginAddress = originAddress;           // Validates origin address via property
            DestinationAddress = destinationAddress; // Validates destination address via property
        }

        // Property that gets and sets Origin Address
        protected Address OriginAddress
        {
            // Precondition:  None
            // Postcondition: The origin address has been returned
            get
            {
                return _originAddress;
            }

            // Precondition:  None
            // Postcondition: The origin address has been set to the specified value
            set
            {
                _originAddress = value;
            }
        }

        // Property that gets and sets Destination Address
        protected Address DestinationAddress
        {
            // Precondition:  None
            // Postcondition: The destination address has been returned
            get
            {
                return _destinationAddress;
            }

            // Precondition:  None
            // Postcondition: The destination address has been set to the specified value
            set
            {
                _destinationAddress = value;
            }
        }

        // Abstract method overridden by derived classes
        public abstract decimal CalcCost(); // No implementation here
    }
}