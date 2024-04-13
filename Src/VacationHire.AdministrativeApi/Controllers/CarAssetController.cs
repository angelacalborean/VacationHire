using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VacationHire.AdministrativeApi.Requests;
using VacationHire.AdministrativeApi.Responses;
using VacationHire.Common;
using VacationHire.Common.GenericResponses;
using VacationHire.Data.Models;
using VacationHire.Repository.Interfaces;

namespace VacationHire.AdministrativeApi.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CarAssetController : BaseController
    {
        private readonly ILogger<CarAssetController> _logger;

        private readonly IMapper _mapper;
        private readonly ICarAssetRepository _carAssetRepository;

        public CarAssetController(ICarAssetRepository carAssetRepository, IMapper mapper, ILogger<CarAssetController> logger)
        {
            _carAssetRepository = carAssetRepository;

            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<CarAssetResponse>), 200)]
        [ProducesResponseType(typeof(ApiInternalErrorResult), 500)]
        public async Task<IActionResult> ListAllCarAssets()
        {
            try
            {
                var carAssets = await _carAssetRepository.GetAll();
                if (carAssets == null)
                    return ApiNotFound("No car assets found.", "CarAssetsNotFoundErrorCode");

                return Ok(carAssets.Select(c => _mapper.Map<CarAssetResponse>(c)).ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled error while retrieving the list of car assets.");
                return ApiInternalError(ex.Message, "GetCarAssetsErrorCode");
            }
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(CarAssetResponse), 200)]
        [ProducesResponseType(typeof(ApiNotFoundResult), 404)]
        [ProducesResponseType(typeof(ApiInternalErrorResult), 500)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var carAsset = await _carAssetRepository.GetById(id);
                if (carAsset == null)
                    return ApiNotFound($"No car assets found for id = {id}.", "CarAssetNotFoundErrorCode");

                return Ok(_mapper.Map<CarAssetResponse>(carAsset));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled error while retrieving the car asset.");
                return ApiInternalError(ex.Message, "CarAssetNotFoundErrorCode");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(List<CarAssetResponse>), 200)]
        [ProducesResponseType(typeof(ApiBadRequestResult), 400)]
        [ProducesResponseType(typeof(ApiInternalErrorResult), 500)]
        public async Task<IActionResult> Create([FromBody] CreateCarAssetRequest newCarAsset)
        {
            try
            {
                // More validations are need
                var carAssetToBeAdded = _mapper.Map<CarAsset>(newCarAsset);
                var createdCarAsset = await _carAssetRepository.Add(carAssetToBeAdded);
                if (createdCarAsset == null)
                    return ApiBadRequest("Car asset could not be created.", "CarAssetCreationErrorCode");

                return Ok(_mapper.Map<CarAssetResponse>(createdCarAsset));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled error while creating a new car asset.");
                return ApiInternalError(ex.Message, "CarAssetCreationErrorCode");
            }
        }
    }
}