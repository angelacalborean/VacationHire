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
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AssetController : BaseController
    {
        private readonly ILogger<AssetController> _logger;
        private readonly IAssetRepository _assetRepository;

        public AssetController(IAssetRepository assetRepository, ILogger<AssetController> logger)
        {
            _assetRepository = assetRepository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<AssetResponse>), 200)]
        [ProducesResponseType(typeof(ApiNotFoundResult), 404)]
        [ProducesResponseType(typeof(ApiInternalErrorResult), 500)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var assets = await _assetRepository.GetAssets();
                if (assets == null)
                    return ApiNotFound("No assets found.", "AssetNotFoundErrorCode");

                return Ok(assets.Select(c => new AssetResponse { AssetName = c.AssetName, Description = c.Description }).ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled error while retrieving the assets.");
                return ApiInternalError(ex.Message, "GetAssetsErrorCode");
            }
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(AssetResponse), 200)]
        [ProducesResponseType(typeof(ApiNotFoundResult), 404)]
        [ProducesResponseType(typeof(ApiInternalErrorResult), 500)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var asset = await _assetRepository.GetById(id);
                if (asset == null)
                    return ApiNotFound($"No asset found for id = {id}.", "AssetNotFoundErrorCode");

                return Ok(new AssetResponse { AssetName = asset.AssetName, Description = asset.Description });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled error while retrieving the asset.");
                return ApiInternalError(ex.Message, "GetAssetByIdErrorCode");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")] // only a user with the Admin role can create a new asset
        [ProducesResponseType(typeof(List<AssetResponse>), 200)]
        [ProducesResponseType(typeof(ApiBadRequestResult), 400)]
        [ProducesResponseType(typeof(ApiInternalErrorResult), 500)]
        public async Task<IActionResult> Create([FromBody] CreateAssetRequest newAsset)
        {
            try
            {
                // More validations are need: duplicate check, that the category to which it belongs, exists
                var asset = new Asset { AssetName = newAsset.AssetName, Description = newAsset.Description, CategoryId = newAsset.CategoryId };
                var createdAsset = await _assetRepository.AddAsset(asset);
                if (createdAsset == null)
                    return ApiBadRequest("Asset could not be created.", "AssetCreationErrorCode");

                return Ok(new AssetResponse
                {
                    AssetName = createdAsset.AssetName,
                    Description = createdAsset.Description,
                    Id = createdAsset.Id,
                    CategoryName = createdAsset.Category.CategoryName
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled error while creating a new asset.");
                return ApiInternalError(ex.Message, "NewAssetErrorCode");
            }
        }
    }
}