// Program 5
// CIS 200-01
// Fall 2019
// Due: 12/3/2019
// By: M1791

// File: Prog5.cs
// Declaration of class TreeNode and class Tree.

using System;

namespace Prog5
{
   // class TreeNode Declaration
   class TreeNode<T> where T : IComparable
   {
      // Automatic Property LeftNode
      public TreeNode<T> LeftNode { get; set; }

      // Automatic Property Data
      public IComparable Data { get; private set; }

      // Automatic Property RightNode
      public TreeNode<T> RightNode { get; set; }

      // Initialize Data and make this a Leaf Node
      public TreeNode(IComparable nodeData)
      {
         Data = nodeData;
      }

      // Insert TreeNode into Tree that contains Nodes;
      // Ignore Duplicate Values
      public void Insert(IComparable insertValue)
      {
         if (insertValue.CompareTo(Data) < 0) // Insert in Left Subtree
         {
            // Insert New TreeNode
            if (LeftNode == null)
            {
               LeftNode = new TreeNode<T>(insertValue);
            }
            else // Continue Traversing Left Subtree
            {
               LeftNode.Insert(insertValue);
            }
         }
         else if (insertValue.CompareTo(Data) > 0) // Insert in Right
         {
            // Insert New TreeNode
            if (RightNode == null)
            {
               RightNode = new TreeNode<T>(insertValue);
            }
            else // Continue Traversing Right Subtree
            {
               RightNode.Insert(insertValue);
            }
         }
      }
   }

   // Class Tree Declaration
   public class Tree<T> where T : IComparable
   {
      private TreeNode<T> root;

      // Insert a new node in the binary search tree.
      // If the root node is null, create the root node here.
      // Otherwise, call the insert method of class TreeNode.
      public void InsertNode(IComparable insertValue)
      {
         if (root == null)
         {
            root = new TreeNode<T>(insertValue);
         }
         else
         {
            root.Insert(insertValue);
         }
      }

      // Begin Inorder Traversal
      public void InorderTraversal()
      {
         InorderHelper(root);
      }

      // Recursive Method to Perform Inorder Traversal
      private void InorderHelper(TreeNode<T> node)
      {
         if (node != null)
         {
            // Traverse Left Subtree
            InorderHelper(node.LeftNode);

            // Output Node Data
            Console.Write($"{node.Data} ");

            // Traverse Right Subtree
            InorderHelper(node.RightNode);
         }
      }
   }
}
