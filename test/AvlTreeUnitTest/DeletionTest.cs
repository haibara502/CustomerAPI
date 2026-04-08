// Copyright (c) Qinyun Song. All rights reserved.

using AvlTree;
using Contract;
using Xunit;

namespace AvlTreeUnitTest
{
    /// <summary>
    /// Test focusing on deletions.
    /// </summary>
    public class DeletionTest
    {
        /// <summary>
        /// Testing the deletion with a general case.
        /// </summary>
        /// <param name="value">Value to be deleted.</param>
        /// <param name="expectedInOrder">Expected in-order sequence.</param>
        /// <param name="expectedPreOrder">Expected pre-order sequence.</param>
        [Theory]
        [InlineData(9, new long[] { 18, 19, 27, 63, 81, 99, 108 }, new long[] { 63, 19, 18, 27, 99, 81, 108 })]
        [InlineData(18, new long[] { 9, 19, 27, 63, 81, 99, 108 }, new long[] { 63, 19, 9, 27, 99, 81, 108 })]
        [InlineData(19, new long[] { 9, 18, 27, 63, 81, 99, 108 }, new long[] { 27, 9, 18, 99, 63, 81, 108 })]
        [InlineData(27, new long[] { 9, 18, 19, 63, 81, 99, 108 }, new long[] { 19, 9, 18, 99, 63, 81, 108 })]
        [InlineData(63, new long[] { 9, 18, 19, 27, 81, 99, 108 }, new long[] { 19, 9, 18, 81, 27, 99, 108 })]
        [InlineData(81, new long[] { 9, 18, 19, 27, 63, 99, 108 }, new long[] { 19, 9, 18, 63, 27, 99, 108 })]
        [InlineData(99, new long[] { 9, 18, 19, 27, 63, 81, 108 }, new long[] { 19, 9, 18, 63, 27, 108, 81 })]
        [InlineData(108, new long[] { 9, 18, 19, 27, 63, 81, 99 }, new long[] { 19, 9, 18, 63, 27, 99, 81 })]
        public void GeneralDeletionTest(long value, long[] expectedInOrder, long[] expectedPreOrder)
        {
            Node? root = null;

            Customer[] customers = new[]
            {
                new Customer(63, 0),
                new Customer(9, 0),
                new Customer(19, 0),
                new Customer(27, 0),
                new Customer(18, 0),
                new Customer(108, 0),
                new Customer(99, 0),
                new Customer(81, 0),
            };

            foreach (var customer in customers)
            {
                root = Tree.Insert(root, customer, new Node(customer));
            }

            root = Tree.Delete(root, new Customer(value, 0));

            Assert.NotNull(root);
            Utils.VerifyTreeStructure(root, expectedInOrder, expectedPreOrder);
        }
    }
}