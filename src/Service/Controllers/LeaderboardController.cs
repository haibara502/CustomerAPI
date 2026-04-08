// Copyright (c) Qinyun Song. All rights reserved.

using Backend;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Service
{
    /// <summary>
    /// Leaderboard controller to handle leaderboard route.
    /// </summary>
    [ApiController]
    [Route("leaderboard")]
    public class LeaderboardController : ControllerBase
    {
        private readonly Leaderboard leaderboard;

        /// <summary>
        /// Initializes a new instance of the <see cref="LeaderboardController"/> class.
        /// </summary>
        /// <param name="leaderboard">Leaderboard singleton.</param>
        public LeaderboardController(Leaderboard leaderboard)
        {
            this.leaderboard = leaderboard;
        }

        /// <summary>
        /// Handle request to get customers by rank.
        /// </summary>
        /// <param name="start">Rank start.</param>
        /// <param name="end">Rank end.</param>
        /// <returns>Returns customers by rank.</returns>
        [HttpGet]
        public IActionResult GetCustomersByRank(
            [FromQuery] int start,
            [FromQuery] int end)
        {
            try
            {
                var items = this.leaderboard.GetCustomerByRank(start, end);
                return this.Ok(JsonConvert.SerializeObject(items));
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Handle request to get customers by id.
        /// </summary>
        /// <param name="customerid">Id of the customer.</param>
        /// <param name="high">Higher rank.</param>
        /// <param name="low">Lower rank.</param>
        /// <returns>Returns customers by id.</returns>
        [HttpGet("{customerid}")]
        public IActionResult GetCustomersById(
            [FromRoute] int customerid,
            [FromQuery] int? high,
            [FromQuery] int? low)
        {
            try
            {
                var items = this.leaderboard.GetCustomerById(customerid, high ?? 0, low ?? 0);
                return this.Ok(JsonConvert.SerializeObject(items));
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
    }
}
