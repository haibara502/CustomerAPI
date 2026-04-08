using Backend;
using Xunit;

namespace BackendTest
{
    /// <summary>
    /// Testing updating customer score.
    /// </summary>
    public class CustomerScoreTest
    {
        /// <summary>
        /// Update the customer score.
        /// </summary>
        [Fact]
        public void UpdateCustomerScoreTest()
        {
            Leaderboard leaderboard = new Leaderboard();

            // Step 1: init a customer
            leaderboard.UpdateCustomerScore(1, 100);

            var customers = leaderboard.GetCustomerById(1, 0, 0);
            Assert.Single(customers);
            Assert.Equal(100, customers[0].Score);

            // Step 2: score plus 123
            leaderboard.UpdateCustomerScore(1, 123);

            customers = leaderboard.GetCustomerById(1, 0, 0);
            Assert.Single(customers);
            Assert.Equal(223, customers[0].Score);

            // Step 3: score minus 300
            // Note: the customer is now off the leaderboard because its score is less than zero.
            leaderboard.UpdateCustomerScore(1, -300);

            Assert.Throws<ArgumentException>(() => leaderboard.GetCustomerById(1, 0, 0));

            // Step 4: score plus 500
            // Note: the customer is back to the leaderboard again.
            leaderboard.UpdateCustomerScore(1, 500);

            customers = leaderboard.GetCustomerById(1, 0, 0);
            Assert.Single(customers);
            Assert.Equal(423, customers[0].Score);
        }
    }
}