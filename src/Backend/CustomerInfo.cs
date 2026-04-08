// Copyright (c) Qinyun Song. All rights reserved.

namespace Backend
{
    /// <summary>
    /// Information of a customer to be returned in the leaderboard.
    /// </summary>
    public class CustomerInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerInfo"/> class.
        /// </summary>
        /// <param name="customerId">Id of the customer.</param>
        /// <param name="score">Score of the customer.</param>
        /// <param name="rank">Rank of the customer in the leaderboard.</param>
        public CustomerInfo(long customerId, decimal score, int rank)
        {
            this.CustomerId = customerId;
            this.Score = score;
            this.Rank = rank;
        }

        /// <summary>
        /// Gets the id of the customer.
        /// </summary>
        public long CustomerId { get; }

        /// <summary>
        /// Gets the score of the customer.
        /// </summary>
        public decimal Score { get; }

        /// <summary>
        /// Gets the rank of the customer.
        /// </summary>
        public int Rank { get; }
    }
}
