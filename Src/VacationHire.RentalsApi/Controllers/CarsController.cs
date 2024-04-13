using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VacationHire.Common;
using VacationHire.Common.GenericResponses;
using VacationHire.Data.Enum;
using VacationHire.QueueManagement.Interfaces;
using VacationHire.RentalsApi.Responses;
using VacationHire.RentalService.BusinessObjects;
using VacationHire.RentalService.Interfaces;

namespace VacationHire.RentalsApi.Controllers
{
    /// <summary>
    ///     Controller that deals with car rentals
    ///         - In case there will be more items for rent, another common base controller can be created assuming that the rental and return workflows are the same
    /// 
    ///     I went on the assumption that different items will have different workflows, so I think a separate controller is more appropriate for each asset type
    /// ( the process of renting a car is different from renting a cabin, for example)
    ///
    ///     If you want to have a common controller for all items, an approach will be to have a path parameter to specify the asset type. That also changes the way
    /// the rental services are created since you need to have them for the asset types
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : BaseController
    {
        private readonly ILogger<CarsController> _logger;

        private readonly IRentableService _rentableService;

        private readonly IQueueClientManager _queueManager;


        public CarsController(ILogger<CarsController> logger,
            IRentableServiceFactory serviceFactory,
            IQueueClientManager queueManager)
        {
            _logger = logger;

            _rentableService = serviceFactory.CreateRentableService(AssetType.Car);

            _queueManager = queueManager;

        }

        /// <summary>
        ///       Returns all available cars from the system
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous] // assume all users can see the available cars, and only after they log in they can rent one
        [ProducesResponseType(typeof(List<CarAssetResponse>), 200)]
        [ProducesResponseType(typeof(ApiNotFoundResult), 404)]
        [ProducesResponseType(typeof(ApiInternalErrorResult), 500)]
        public async Task<IActionResult> GetAllAvailableItems()
        {
            try
            {
                var items = await _rentableService.GetRentableItems();
                if (items == null)
                    return ApiNotFound("No assets found.", "AssetNotFoundErrorCode");

                return Ok(items.Select(car => new CarAssetResponse(car)).ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled error while retrieving the rentable assets.");
                return ApiInternalError(ex.Message);
            }
        }


        /// <summary>
        ///     Allows a user to rent a car
        /// - Authentication is required
        /// - Authorization: checks needed to ensure the user has the needed permissions to rent a car (demo purposes: ask for User role)
        ///
        /// - No validation is added here, but it should be added in a real-world scenario
        ///     - have an [FromBody] RentRequest request with more details about the rental
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        [HttpPost("{itemId}/rent")]
        [Authorize(Roles = "User")] // User role is required to rent a car
        [ProducesResponseType(typeof(RentAcknowledgeResponse), 200)]
        //[ProducesResponseType(typeof(ApiNotFoundResult), 400)] - needed in case validation was added and failed
        [ProducesResponseType(typeof(ApiNotFoundResult), 404)]
        [ProducesResponseType(typeof(ApiInternalErrorResult), 500)]
        public async Task<IActionResult> RentACar(int itemId)
        {
            // Improvements: ask an [FromBody] RentRequest request with more details and validate it
            //if (!ModelState.IsValid)
            //      return ApiBadRequest(ModelState);

            try
            {
                var item = await _rentableService.GetItemById(itemId);
                if (item == null)
                    return ApiNotFound("No assets found.");

                //var x = new CarRentable(item.Id, item.Description, item.AssetId, item.State) { Model = item.Model }

                var result = await _rentableService.RentAnItem(item as CarRentable);
                var response = new RentAcknowledgeResponse(result.RentRequestId, result.RentableAssetId)
                {
                    // set the rest of the properties
                    RequestDate = DateTime.UtcNow,
                    Message = "Your rental request is pending approval."
                };

                // Add response to a queue / service bus in order to trigger the approval workflow
                // Message added to queue will be different that the one returned to the user
                await _queueManager.QueueMessage(response.ToString());

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled error while renting a car asset.");
                return ApiInternalError(ex.Message);
            }
        }


        /// <summary>
        ///     Allows a user to return a car (improvement points from rent endpoint are still valid here)
        /// <param name="itemId"></param>
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        [HttpPost("{itemId}/return")]
        [Authorize(Roles = "User")] // User role is required to rent a car
        [ProducesResponseType(typeof(RentAcknowledgeResponse), 200)]
        [ProducesResponseType(typeof(ApiNotFoundResult), 404)]
        [ProducesResponseType(typeof(ApiInternalErrorResult), 500)]
        public async Task<IActionResult> ReturnACar(int itemId)
        {
            try
            {
                var item = await _rentableService.GetItemById(itemId);
                if (item == null)
                    return ApiNotFound("No assets found.");

                var result = await _rentableService.ReturnAnItem(item as CarRentable);
                var response = new ReturnAcknowledgeResponse(result);

                // Add response to a queue / service bus in order to trigger the return check workflow

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled error while returning a car asset.");
                return ApiInternalError(ex.Message);
            }
        }
    }
}