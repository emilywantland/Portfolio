// Grading ID: M1791
// Program #: 0
// Due Date: 9/9/2019
// Course Section: CIS 200-01
// Descritpion: Simple test application class that creates a list of Parcel objects and tests them

using System;
using System.Collections.Generic;

namespace Program_0
{
    class MailSystemTest
    {
        static void Main()
        {
            // Create derived-class address objects
            var address1 = new Address("Microsoft Corporation", "One Microsoft Way", "Redmond", "WA", 98052);
            var address2 = new Address("Googleplex", "1600 Amphitheatre Parkway", "Moutain View", "CA", 94043);
            var address3 = new Address("IBM North America", "590 Madison Avenue", "New York", "NY", 10022);
            var address4 = new Address("HP Inc.", "1501 Page Mill Road", "Office #65", "Palo Alto", "CA", 94304);

            // Create derived-class letter objects
            var letter1 = new Letter(address1, address2, 2.00M);
            var letter2 = new Letter(address4, address2, 1.00M);
            var letter3 = new Letter(address3, address4, 1.50M);

            // Create List<Parcel> and initialize with letter objects
            var letters = new List<Parcel>() { letter1, letter2, letter3 };

            Console.WriteLine("-=- Letters Processed Polymorphically -=-\n");

            // Generically process each element in letters
            foreach (var currentParcel in letters)
            {
                Console.WriteLine(currentParcel); // Invokes ToString
                Console.WriteLine("----------\n");
            }
        }
    }
}