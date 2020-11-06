//Grading ID: N2466
//Lab #: 5
//Due Date: 3/3/19
//Course Section: CIS 199-75
//Description: Application that prompts a user for valid temperatures within a range. 
//             Totals and averages the valid temperatures. Stops at sentinel value.

using System;
using static System.Console;
namespace Lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            //Naming variables
            int temp; //The numbers entered by the user
            int sum = 0; //The sum of all valid numbers entered
            int mean; //The average of all valid numbers entered
            int counter = 0; //The count of all valid numbers entered
            bool validInt = true; //Variable to test whether a valid number was entered

            //Naming constants
            const int LOW_TEMP = -20; //The low temperature in the range
            const int HIGH_TEMP = 130; //The high temperature in the range
            const int QUIT = 999; //The sentinel variable

            //Gathering the input
            WriteLine($"Enter temperature from {LOW_TEMP} to {HIGH_TEMP} (999 to stop)");
            Write("Enter temperature: ");
            validInt = int.TryParse(ReadLine(), out temp);

            //While loop and if statements
            while (temp != QUIT)
            {
                if (!validInt || (temp < LOW_TEMP || temp > HIGH_TEMP))
                {
                    WriteLine($"Valid temperatures range from {LOW_TEMP} to {HIGH_TEMP}. Please reenter temperature.");
                }
                else
                {
                    ++counter;
                    sum += temp;
                }
                    Write("Enter temperature: ");
                    validInt = int.TryParse(ReadLine(), out temp);
            }

            //Calculation
            mean = sum / counter;

            //Output statements
            WriteLine($"You entered {counter} valid temperatures.");
            WriteLine($"The mean temperature is {mean:F1} degrees.");
        }
    }
}
