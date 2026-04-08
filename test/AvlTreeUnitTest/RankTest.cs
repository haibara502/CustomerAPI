// Copyright (c) Qinyun Song. All rights reserved.

using AvlTree;
using Contract;
using Xunit;

namespace AvlTreeUnitTest
{
    /// <summary>
    /// Test focusing on ranking.
    /// </summary>
    public class RankTest
    {
        /// <summary>
        /// Testing the deletion with a general case.
        /// </summary>
        /// <param name="start">Range start.</param>
        /// <param name="end">Range end.</param>
        /// <param name="expectedIds">Expected ids.</param>
        [Theory]
        [InlineData(1, 8, new long[] { 9, 18, 19, 27, 63, 81, 99, 108 })]
        [InlineData(1, 2, new long[] { 9, 18 })]
        [InlineData(3, 6, new long[] { 19, 27, 63, 81 })]
        [InlineData(4, 7, new long[] { 27, 63, 81, 99 })]
        [InlineData(8, 8, new long[] { 108 })]
        [InlineData(4, 6, new long[] { 27, 63, 81 })]
        [InlineData(5, 6, new long[] { 63, 81 })]
        [InlineData(6, 8, new long[] { 81, 99, 108 })]
        [InlineData(4, 4, new long[] { 27 })]
        [InlineData(3, 4, new long[] { 19, 27 })]
        public void GeneralRankRangeTest(int start, int end, long[] expectedIds)
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

            var selectedCustomers = new List<Customer>();
            Tree.GetRange(root, start, end, selectedCustomers);
            var actualIds = selectedCustomers.Select(c => c.Id).ToArray();

            Assert.Equal(expectedIds, actualIds);
        }

        /// <summary>
        /// Test getting the rank of each node.
        /// </summary>
        [Fact]
        public void GetRankTest()
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

            Dictionary<long, Node> idToNode = new Dictionary<long, Node>();

            foreach (var customer in customers)
            {
                idToNode[customer.Id] = new Node(customer);
                root = Tree.Insert(root, customer, idToNode[customer.Id]);
            }

            var orderedId = customers.OrderBy(c => c.Id).Select(c => c.Id).ToList();
            for (int i = 0; i < orderedId.Count; ++i)
            {
                Assert.Equal(i + 1, Tree.GetRank(idToNode[orderedId[i]]));
            }
        }
    }
}