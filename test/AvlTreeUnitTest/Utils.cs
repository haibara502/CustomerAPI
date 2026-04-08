// Copyright (c) Qinyun Song. All rights reserved.

using AvlTree;
using Contract;
using Xunit;

namespace AvlTreeUnitTest
{
    /// <summary>
    /// Utility functions to help with the test.
    /// </summary>
    internal class Utils
    {
        /// <summary>
        /// Verify the tree in several perspectives.
        /// </summary>
        /// <param name="node">Root of the tree.</param>
        /// <param name="expectedInOrder">Expected in-order sequence.</param>
        /// <param name="expectedPreOrder">Expected pre-order sequence.</param>
        internal static void VerifyTreeStructure(Node node, long[] expectedInOrder, long[] expectedPreOrder)
        {
            VerifyParentChildRelation(node);
            Assert.Null(node.Parent);

            VerifyHeightAndBalance(node);
            VerifySize(node);

            var actualInOrder = GetInOrderTraversal(node).Select(x => x.Id).ToArray();
            Assert.Equal(expectedInOrder, actualInOrder);

            var actualPreOrder = GetPreOrderTraversal(node).Select(x => x.Id).ToArray();
            Assert.Equal(expectedPreOrder, actualPreOrder);
        }

        private static void VerifyParentChildRelation(Node? node)
        {
            if (node == null)
            {
                return;
            }

            if (node.Left != null)
            {
                Assert.Equal(node.Left.Parent, node);
            }

            if (node.Right != null)
            {
                Assert.Equal(node.Right.Parent, node);
            }

            VerifyParentChildRelation(node.Left);
            VerifyParentChildRelation(node.Right);
        }

        private static List<Customer> GetInOrderTraversal(Node? node)
        {
            var customers = new List<Customer>();

            if (node == null)
            {
                return customers;
            }

            customers.AddRange(GetInOrderTraversal(node.Left));
            customers.Add(node.Customer);
            customers.AddRange(GetInOrderTraversal(node.Right));
            return customers;
        }

        private static void VerifyHeightAndBalance(Node? node)
        {
            if (node == null)
            {
                return;
            }

            VerifyHeightAndBalance(node.Left);
            VerifyHeightAndBalance(node.Right);

            int leftHeight = node.Left?.Height ?? 0;
            int rightHeight = node.Right?.Height ?? 0;

            int expectedHeight = 1 + Math.Max(leftHeight, rightHeight);
            int expectedBalance = leftHeight - rightHeight;

            Assert.Equal(expectedHeight, node.Height);
            Assert.Equal(expectedBalance, node.Balance);
        }

        private static void VerifySize(Node? node)
        {
            if (node == null)
            {
                return;
            }

            VerifySize(node.Left);
            VerifySize(node.Right);

            int leftSize = node.Left?.Size ?? 0;
            int rightSize = node.Right?.Size ?? 0;

            int expectedSize = 1 + leftSize + rightSize;
            Assert.Equal(expectedSize, node.Size);
        }

        /// <summary>
        /// Gets the pre-order traversal of the tree.
        /// </summary>
        /// <param name="node">Current node.</param>
        /// <returns>Returns the pre-order traversal of the tree.</returns>
        private static List<Customer> GetPreOrderTraversal(Node? node)
        {
            var customers = new List<Customer>();

            if (node == null)
            {
                return customers;
            }

            customers.Add(node.Customer);
            customers.AddRange(GetPreOrderTraversal(node.Left));
            customers.AddRange(GetPreOrderTraversal(node.Right));
            return customers;
        }
    }
}
