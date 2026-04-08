// Copyright (c) Qinyun Song. All rights reserved.

using AvlTree;
using Contract;

namespace Backend
{
    /// <summary>
    /// The leaderboard of the customers.
    /// </summary>
    public class Leaderboard
    {
        private readonly ReaderWriterLockSlim rwlock = new ReaderWriterLockSlim();
        private readonly Dictionary<long, Node?> idToNode = new Dictionary<long, Node?>();
        private readonly Dictionary<long, Customer> idToCustomer = new Dictionary<long, Customer>();
        private Node? root = null;

        /// <summary>
        /// Update the score of a specific customer.
        /// </summary>
        /// <param name="id">Id of the customer.</param>
        /// <param name="delta">Delta score to be updated.</param>
        /// <returns>Returns the updated customer info.</returns>
        public Customer UpdateCustomerScore(long id, decimal delta)
        {
            if (delta < -1000 || delta > 1000)
            {
                throw new ArgumentOutOfRangeException($"Update score should be within [-1000, 1000]. Input score: {delta}.");
            }

            this.rwlock.EnterWriteLock();

            try
            {
                if (this.idToCustomer.ContainsKey(id))
                {
                    if (delta != 0 && this.idToNode[id] != null)
                    {
                        this.root = Tree.Delete(this.root, this.idToCustomer[id]);
                        this.idToNode[id] = null;
                    }

                    this.idToCustomer[id].UpdateScore(delta);
                }
                else
                {
                    this.idToCustomer[id] = new Customer(id, delta);
                }

                if (this.idToCustomer[id].Score >= 0)
                {
                    this.idToNode[id] = new Node(this.idToCustomer[id]);
                    this.root = Tree.Insert(this.root, this.idToCustomer[id], this.idToNode[id] !);
                }
            }
            finally
            {
                this.rwlock.ExitWriteLock();
            }

            return this.idToCustomer[id];
        }

        /// <summary>
        /// Gets the customer list by rank.
        /// </summary>
        /// <param name="start">Start rank.</param>
        /// <param name="end">End rank.</param>
        /// <returns>Returns the list of customers inside the rank range.</returns>
        public List<CustomerInfo> GetCustomerByRank(int start, int end)
        {
            if (start < 0 || end < 0 || start > end)
            {
                throw new ArgumentOutOfRangeException($"Start {start} or end {end} is invalid. Expect: 0 < start <= end. Input start: {start}. Input end: {end}.");
            }

            if (end > (this.root?.Size ?? 0))
            {
                throw new ArgumentOutOfRangeException($"Start and end should not exceed the total number of customers in the leaderboard. Current leaderboard size: {this.root?.Size ?? 0}. Input start: {start}. Input end: {end}.");
            }

            var customers = new List<Customer>();

            this.rwlock.EnterReadLock();
            try
            {
                Tree.GetRange(this.root, start, end, customers);
            }
            finally
            {
                this.rwlock.ExitReadLock();
            }

            return customers
                .Select((c, i) => new CustomerInfo(c.Id, c.Score, start + i))
                .ToList();
        }

        /// <summary>
        /// Gets the customer list by customer id and rank range.
        /// </summary>
        /// <param name="id">Id of the customer.</param>
        /// <param name="high">Higher rank.</param>
        /// <param name="low">Lower rank.</param>
        /// <returns>Returns the list of customers by id and rank range.</returns>
        public List<CustomerInfo> GetCustomerById(long id, int high, int low)
        {
            if (low < 0 || high < 0)
            {
                throw new ArgumentOutOfRangeException($"Low and high should be no less than zero. Input low: {low}. Input high: {high}.");
            }

            var customer = this.idToCustomer[id];
            if (customer.Score < 0)
            {
                throw new ArgumentException($"Customer with id {id} has score less than zero. It is not on the leaderboard.");
            }

            int rank;
            this.rwlock.EnterReadLock();
            try
            {
                rank = Tree.GetRank(this.idToNode[id] !);
            }
            finally
            {
                this.rwlock.ExitReadLock();
            }

            if (high >= rank)
            {
                throw new ArgumentOutOfRangeException($"High should not exceed the rank of the customer. The rank of the customer is {rank}. Input high: {high}.");
            }

            if (low > this.root!.Size - rank)
            {
                throw new ArgumentOutOfRangeException($"Low should not exceed the total number of customers on the leaderboard minus the rank. Total number of customers on the leaderboard is {this.root!.Size}. The rank of the customer is {rank}. Input low: {low}.");
            }

            return this.GetCustomerByRank(rank - high, rank + low);
        }
    }
}
