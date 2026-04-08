// Copyright (c) Qinyun Song. All rights reserved.

using Backend;
using Contract;
using Xunit;

namespace BackendTest
{
    /// <summary>
    /// Test relating to rank.
    /// </summary>
    public class RankTest
    {
        /// <summary>
        /// Test to get the customers by rank range.
        /// </summary>
        /// <param name="start">Range start.</param>
        /// <param name="end">Range end.</param>
        [Theory]
        [InlineData(1, 8)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(8, 8)]
        [InlineData(4, 7)]
        [InlineData(2, 5)]
        [InlineData(4, 5)]
        [InlineData(3, 6)]
        public void RankRangeTest(int start, int end)
        {
            Leaderboard leaderboard = new Leaderboard();

            Customer[] customers = new[]
            {
                new Customer(63, 10),
                new Customer(9, 234),
                new Customer(19, 65),
                new Customer(27, 89),
                new Customer(18, 300),
                new Customer(108, 126),
                new Customer(99, 1),
                new Customer(81, 800),
                new Customer(62, 800),
            };

            for (int i = 0; i < customers.Length; ++i)
            {
                leaderboard.UpdateCustomerScore(customers[i].Id, customers[i].Score);
            }

            var sortedCustomer = customers
                .OrderByDescending(c => c.Score)
                .ThenBy(c => c.Id)
                .ToList();

            var customersInRange = leaderboard.GetCustomerByRank(start, end);
            Assert.Equal(end - start + 1, customersInRange.Count);
            for (int i = 0; i <= end - start; ++i)
            {
                Assert.Equal(start + i, customersInRange[i].Rank);
                Assert.Equal(sortedCustomer[i + start - 1].Score, customersInRange[i].Score);
                Assert.Equal(sortedCustomer[i + start - 1].Id, customersInRange[i].CustomerId);
            }
        }

        /// <summary>
        /// Test get customer by id.
        /// </summary>
        /// <param name="high">higher rank.</param>
        /// <param name="low">lower rank.</param>
        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 2)]
        [InlineData(2, 1)]
        [InlineData(3, 0)]
        [InlineData(6, 1)]
        [InlineData(2, 4)]
        public void IdRangeTest(int high, int low)
        {
            Leaderboard leaderboard = new Leaderboard();

            Customer[] customers = new[]
            {
                new Customer(63, 10),
                new Customer(9, 234),
                new Customer(19, 65),
                new Customer(27, 89),
                new Customer(18, 300),
                new Customer(108, 126),
                new Customer(99, 1),
                new Customer(81, 800),
                new Customer(62, 800),
            };

            for (int i = 0; i < customers.Length; ++i)
            {
                leaderboard.UpdateCustomerScore(customers[i].Id, customers[i].Score);
            }

            var sortedCustomer = customers
                .OrderByDescending(c => c.Score)
                .ThenBy(c => c.Id)
                .ToList();

            for (int i = high; i < sortedCustomer.Count - low; ++i)
            {
                var customersById = leaderboard.GetCustomerById(sortedCustomer[i].Id, high, low);
                for (int j = i - high; j < i + low; ++j)
                {
                    Assert.Equal(sortedCustomer[j].Id, customersById[j - i + high].CustomerId);
                    Assert.Equal(sortedCustomer[j].Score, customersById[j - i + high].Score);
                    Assert.Equal(j - i + high + 1, customersById[j - i + high].Rank);
                }
            }
        }
    }
}
