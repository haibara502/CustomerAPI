// Copyright (c) Qinyun Song. All rights reserved.

namespace Contract
{
    /// <summary>
    /// Customer information.
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Customer"/> class.
        /// </summary>
        /// <param name="id">Id of the customer. Id should be unique.</param>
        /// <param name="score">Score of the customer.</param>
        public Customer(long id, decimal score)
        {
            this.Id = id;
            this.Score = score;
        }

        /// <summary>
        /// Gets the id of the customer.
        /// </summary>
        public long Id { get; }

        /// <summary>
        /// Gets or sets the score of the customer.
        /// </summary>
        public decimal Score { get; set; } = 0;

        public static bool operator <(Customer c1, Customer c2)
        {
            return c1.Score == c2.Score ? c1.Id < c2.Id : c1.Score > c2.Score;
        }

        public static bool operator >(Customer c1, Customer c2)
        {
            return c1.Score == c2.Score ? c1.Id > c2.Id : c1.Score < c2.Score;
        }

        /// <summary>
        /// Update the score of the customer.
        /// </summary>
        /// <param name="delta">Value to be changed.</param>
        public void UpdateScore(decimal delta)
        {
            this.Score += delta;
        }
    }
}
