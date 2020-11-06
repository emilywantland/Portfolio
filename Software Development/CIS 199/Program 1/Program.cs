//Grading ID: N2466
//Course Section: CIS 199-75
//Program Number: 1
//Due Date: 2/12/2019
//Description: Program to estimate carpet installation prices

using System;
using static System.Console;
class Program1
{
    static void Main()
    {

        //Naming Constants
        const int squareYards = 9; //Square yards calculation constant
        const double wastePercentage = 0.1; //Waste percentage constant
        const double padPricing = 2.75; //Pad pricing cost constant
        const double laborPrice = 4.50; //Labor price constant
        const double firstRoomLabor = 100.00; //First room charge constant

        //Naming variables
        double maxWidth; //Max width of room variable
        double maxLength; //Max length of room variable
        double carpetPrice; //Carpet price variable
        int paddingLayers; //Padding layers variable
        int firstRoom; //First room charge variable

        //Title greeting writeline
        WriteLine("Welcome to the Handy-Dandy Carpet Estimator");

        //Line break
        WriteLine("");

        //Entering the max width of room
        Write("Enter the max width of room (in feet): ");
        maxWidth = double.Parse(ReadLine());

        //Entering the max length of room
        Write("Enter the max length of room (in feet): ");
        maxLength = double.Parse(ReadLine());

        //Entering the carpet price
        Write("Enter the carpet price (per sq. yard): ");
        carpetPrice = double.Parse(ReadLine());

        //Entering the layers of padding used
        Write("Enter layers of padding to use (1 or 2): ");
        paddingLayers = int.Parse(ReadLine());

        //Entering the room number
        Write("Is this the first room? (1 = YES, 0 = NO): ");
        firstRoom = int.Parse(ReadLine());

        //Declaring outputs
        double squareYardsNeeded;
        double carpetCost;
        double padCost;
        double laborCost;
        double totalCost;

        //Declaring calculations
        squareYardsNeeded = (maxWidth * maxLength) / squareYards;
        carpetCost = squareYardsNeeded * (1 + wastePercentage) * carpetPrice;
        padCost = paddingLayers * squareYardsNeeded * (1 + wastePercentage) * padPricing;
        laborCost = (firstRoom * firstRoomLabor) + squareYardsNeeded * laborPrice;
        totalCost = carpetCost + padCost + laborCost;

        //Line break
        WriteLine("");

        //Output Statements
        WriteLine($"Sq. Yards Needed: {squareYardsNeeded,9:F1}");
        WriteLine($"Carpet Cost: {carpetCost,15:C}");
        WriteLine($"Padding Cost: {padCost,14:C}");
        WriteLine($"Labor Cost: {laborCost,16:C}");
        WriteLine($"Total Cost: {totalCost,16:C}");
    }

}