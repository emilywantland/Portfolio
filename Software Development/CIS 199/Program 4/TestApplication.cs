// Grading ID: N2466
// Section: CIS 199-75
// Due Date: 4-23-2019
// Program: 4
// Description: Displays original package information and changes data

using static System.Console;

namespace Program4
{
    public class TestApplication
    {
        public static void Main(string[] args)
        {
            // Create derived class objects
            GroundPackage package1 = new GroundPackage(40220, 95993, 4.5, 3.0, 8.3, 20.7);
            GroundPackage package2 = new GroundPackage(33594, 19061, 29.1, 18.4, 9.6, 47.3);
            GroundPackage package3 = new GroundPackage(58078, 07740, 3.8, 4.7, 9.3, 10.1);
            GroundPackage package4 = new GroundPackage(38637, 97603, 2.6, 1.9, 3.5, 2.1);
            GroundPackage package5 = new GroundPackage(22030, 77478, 6.7, 11.4, 7.3, 3.6);

            // Create five-element GroundPackage array
            GroundPackage[] packages = new GroundPackage[5];

            // Initialize array with Packages of derived types
            packages[0] = package1;
            packages[1] = package2;
            packages[2] = package3;
            packages[3] = package4;
            packages[4] = package5;

            // Calling method
            WriteLine("---------------------");
            WriteLine("-   Original Data   -");
            WriteLine("---------------------");
            WriteLine();
            DisplayPackages(packages);

            // Changing data
            package1.Length = 0.1;
            package2.Height = 21.2;
            package3.OriginZip = 40208;
            package4.DestinationZip = 12345;
            package5.Weight = 10.2;

            // Calling method
            WriteLine("---------------------");
            WriteLine("-   After Changes   -");
            WriteLine("---------------------");
            WriteLine();
            DisplayPackages(packages);
        }

        // Precondition: None
        // Postcondition: The packages's information and cost is returned as a formatted string
        public static void DisplayPackages(GroundPackage[] packages)
        {
            foreach (GroundPackage currentPackage in packages)
            { 
                WriteLine($"{currentPackage}"); // invokes ToString implicitly
                WriteLine($"Cost:            {currentPackage.CalcCost():C}"); // Wrestled with spacing
                WriteLine();                                                  // and gave up :P
            }
        }
    }
}