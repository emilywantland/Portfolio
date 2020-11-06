// Grading ID: M1791
// Program #: 0
// Due Date: 9/9/2019
// Course Section: CIS 200-01
// Description: Models addresses, each has a recipient name, address line 1, address line 2, city, state, and zip code.

using System;

namespace Program_0
{
    public class Address
    {
        // Backing Fields
        private string _name;     // Name of recipient backing field
        private string _address1; // Line 1 of address backing field
        private string _address2; // Line 2 of address backing field
        private string _city;     // City in address backing field
        private string _state;    // State in address backing field
        private int _zip;         // Zip code of address backing field

        // Validation values
        private const int MIN_ZIP = 00000; // Minimum zip code value
        private const int MAX_ZIP = 99999; // Maximum zip code value

        // Six-Parameter Constructor
        public Address(string name, string address1, string address2, string city, string state, int zip)
        {
            Name = name;         // Validates name via property
            Address1 = address1; // Validates address line 1 via property
            Address2 = address2; // Validates address line 2 via property
            City = city;         // Validates city via property
            State = state;       // Validates state via property
            Zip = zip;           // Validates zip via property
        }

        // Five-Parameter Overloaded Constructor
        public Address(string name, string address1, string city, string state, int zip)
        {
            Name = name;         // Validates name via property
            Address1 = address1; // Validates address line 1 via property
            City = city;         // Validates city via property
            State = state;       // Validates state via property
            Zip = zip;           // Validates zip via property
        }

        // Property that gets and sets recipient's name
        protected string Name
        {
            // Precondition:  None
            // Postcondition: The recipient's name has been returned
            get
            {
                return _name;
            }

            // Precondition:  Must have a value or no white space - Exception thrown if so
            // Postcondition: The recipient's name has been set to the specified value
            set
            {
                if (string.IsNullOrWhiteSpace(value.Trim())) // Validation
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, $"Name cannot be left blank");
                }
                else
                {
                    _name = value.Trim();
                }
            }
        }

        // Property that gets and sets address' line 1
        protected string Address1
        {
            // Precondition:  None
            // Postcondition: The address' line 1 has been returned
            get
            {
                return _address1;
            }

            // Precondition:  Must have a value or no white space - Exception thrown if so
            // Postcondition: The address' line 1 has been set to the specified value
            set
            {
                if (string.IsNullOrWhiteSpace(value.Trim())) // Validation
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, $"Address Line 1 cannot be left blank");
                }

                else
                {
                    _address1 = value.Trim();
                }
            }
        }

        // Property that gets and sets address' line 2
        protected string Address2
        {
            // Precondition:  None
            // Postcondition: The address' line 2 has been returned
            get
            {
                return _address2;
            }

            // Precondition:  Must have a value or no white space - Exception thrown if so
            // Postcondition: The address' line 2 has been set to the specified value
            set
            {
                if (string.IsNullOrWhiteSpace(value.Trim())) // Validation
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, $"Address Line 2 cannot be left blank");
                }

                else
                {
                    _address2 = value.Trim();
                }
            }
        }

        // Property that gets and sets address' city
        protected string City
        {
            // Precondition:  None
            // Postcondition: The package's city has been returned
            get
            {
                return _city;
            }

            // Precondition:  Must have a value or no white space - Exception thrown if so
            // Postcondition: The address' city has been set to the specified value
            set
            {
                if (string.IsNullOrWhiteSpace(value.Trim())) // Validation
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, $"City cannot be left blank");
                }

                else
                {
                    _city = value.Trim();
                }
            }
        }

        // Property that gets and sets address' state
        protected string State
        {
            // Precondition:  None
            // Postcondition: The package's state has been returned
            get
            {
                return _state;
            }

            // Precondition:  Must have a value or no white space - Exception thrown if so
            // Postcondition: The address' state has been set to the specified value
            set
            {
                if (string.IsNullOrWhiteSpace(value.Trim())) // Validation
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, $"State cannot be left blank");
                }

                else
                {
                    _state = value.Trim();
                }
            }
        }

        // Property that gets and sets address' zip
        protected int Zip
        {
            // Precondition:  None
            // Postcondition: The package's zip code has been returned
            get
            {
                return _zip;
            }

            // Precondition:  MIN_ZIP <= value <= MAX_ZIP - Exception thrown if so
            // Postcondition: The package's zip code has been set to the specified value
            set
            {
                if ((value >= MIN_ZIP) && (value <= MAX_ZIP)) // Validation
                {
                    _zip = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, $"Zip Code cannot be less than 0 or greater than 99,999");
                }
            }
        }

        public override string ToString()
        {
            if (Address2 != null)
            {
                return $"{Name}\n" +
                       $"{Address1}\n" +
                       $"{Address2}\n" +
                       $"{City}, {State} {Zip:D5}\n";
            }
            else
            {
                return $"{Name}\n" +
                       $"{Address1}\n" +
                       $"{City}, {State} {Zip:D5}\n";
            }
        }
    }
}