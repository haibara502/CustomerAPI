// Copyright (c) Qinyun Song. All rights reserved.

using Backend;
using Microsoft.AspNetCore.Mvc;

namespace Service
{
    /// <summary>
    /// Customer controller to handle customer route.
    /// </summary>
    [ApiController]
    [Route("customer")]
    public class CustomerController : ControllerBase
    {
        private readonly Leaderboard leaderboard;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerController"/> class.
        /// </summary>
        /// <param name="leaderboard">Leaderboard singleton.</param>
        public CustomerController(Leaderboard leaderboard)
        {
            this.leaderboard = leaderboard;
        }

        /// <summary>
        /// Handle request to update customer score.
        /// </summary>
        /// <param name="customerid">Id of the customer.</param>
        /// <param name="score">Score to be updated.</param>
        /// <returns>Returns the status of the request.</returns>
        [HttpPost("{customerid}/score/{score}")]
        public IActionResult UpdateScore(
            [FromRoute] long customerid,
            [FromRoute] decimal score)
        {
            try
            {
                var customer = this.leaderboard.UpdateCustomerScore(customerid, score);
                return this.Ok($"Update succedded. The score of customer with id {customer.Id} is now {customer.Score}.");
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
    }
}
