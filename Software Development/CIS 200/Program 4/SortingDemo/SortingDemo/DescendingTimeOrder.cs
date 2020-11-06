// File: DescendingTimeOrder.cs
// By: Andrew L. Wright
// This class provides an IComparer for the Time2 class
// that orders the objects in descending time order.

using System;
using System.Collections.Generic;
using System.Text;

namespace SortingDemo
{
    public class DescendingTimeOrder : Comparer<Time2>
    {
        // Precondition:  None
        // Postcondition: Reverses natural time order, so descending
        //                When t1 < t2, method returns positive #
        //                When t1 == t2, method returns zero
        //                When t1 > t2, method returns negative #
        public override int Compare(Time2 t1, Time2 t2)
        {
            // Ensure correct handling of null values (in .NET, null less than anything)
            if (t1 == null && t2 == null) // Both null?
                return 0;                 // Equal

            if (t1 == null) // only t1 is null?
                return -1;  // null is less than any actual time

            if (t2 == null) // only t2 is null?
                return 1;   // Any actual time is greater than null

            return (-1)*t1.CompareTo(t2); // Reverses natural order, so descending
        }
    }
}
