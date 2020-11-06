// Program 5
// CIS 200-01
// Fall 2019
// Due: 12/3/2019
// By: M1791

// File: TestProg.cs
// Testing class Tree with IComparable objects.

using System;
using Prog5;

// Class TreeTest Declaration
class TestProg
{
    // Test Class Tree
    static void Main()
    {
        int[] intArray = { 8, 2, 4, 3, 1, 7, 5, 6, 10, 9 }; // Array of ints
        double[] doubleArray = { 8.8, 2.2, 4.4, 3.3, 1.1, 7.7, 5.5, 6.6, 10.10, 9.9}; // Array of doubles
        string[] stringArray = {"eight", "two", "four", "three", "one", "seven", "five", "six", "ten", "nine"}; // Array of strings

        // Create int Tree
        Tree<double> intTree = new Tree<double>();
        PopulateTree(intArray, intTree, nameof(intTree));
        TraverseTree(intTree, nameof(intTree));

        // Create double Tree
        Tree<double> doubleTree = new Tree<double>();
        PopulateTree(doubleArray, doubleTree, nameof(doubleTree));
        TraverseTree(doubleTree, nameof(doubleTree));

        // Create string Tree
        Tree<double> stringTree = new Tree<double>();
        PopulateTree(stringArray, stringTree, nameof(stringTree));
        TraverseTree(stringTree, nameof(stringTree));
    }

    // Populate Tree with Array Elements
    private static void PopulateTree<T>(T[] array, Tree<double> tree, string name) where T : IComparable<T>
    {
        Console.WriteLine($"Inserting into {name}:");

        foreach (IComparable data in array)
        {
            Console.Write($"{data} ");
            tree.InsertNode(data);
        }
        Console.WriteLine();
        Pause();
    }

    // Perform Traversals
    private static void TraverseTree(Tree<double> tree, string treeType)
    {
        // Perform Inorder Traversal of Tree
        Console.WriteLine($"Inorder traversal of {treeType}");
        tree.InorderTraversal();
        Console.WriteLine();
        Pause();
    }

    // Precondition:  None
    // Postcondition: Pauses program execution until user presses Enter and
    //                then clears the screen
    public static void Pause()
    {
        Console.WriteLine("Press Enter to Continue...");
        Console.ReadLine();

        Console.Clear();
    }
}
