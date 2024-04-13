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
    public class CategoryController : BaseController
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository, ILogger<CategoryController> logger)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<CategoryResponse>), 200)]
        [ProducesResponseType(typeof(ApiBadRequestResult), 400)]
        [ProducesResponseType(typeof(ApiInternalErrorResult), 500)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var categories = await _categoryRepository.GetCategories();
                if (categories == null)
                    return ApiNotFound("No categories found.", "CategoryNotFoundErrorCode");

                return Ok(categories.Select(c => new CategoryResponse { CategoryName = c.CategoryName, Description = c.Description }).ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled error while retrieving the categories.");
                return ApiInternalError(ex.Message, "GetCategoriesErrorCode");
            }
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(CategoryResponse), 200)]
        [ProducesResponseType(typeof(ApiBadRequestResult), 400)]
        [ProducesResponseType(typeof(ApiInternalErrorResult), 500)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var category = await _categoryRepository.GetById(id);
                if (category == null)
                    return ApiNotFound($"No categories found for id = {id}.", "CategoryNotFoundErrorCode");

                return Ok(new CategoryResponse { CategoryName = category.CategoryName, Description = category.Description });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled error while retrieving the category.");
                return ApiInternalError(ex.Message, "GetCategoryByIdErrorCode");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")] // only a user with the Admin role can create a category
        [ProducesResponseType(typeof(List<CategoryResponse>), 200)]
        [ProducesResponseType(typeof(ApiBadRequestResult), 400)]
        [ProducesResponseType(typeof(ApiInternalErrorResult), 500)]
        public async Task<IActionResult> Create([FromBody] CreateCategoryRequest newCategory)
        {
            try
            {
                // More validations are need: duplicate check
                var category = new Category { CategoryName = newCategory.CategoryName, Description = newCategory.Description };
                var createdCategory = await _categoryRepository.AddCategory(category);
                if (createdCategory == null)
                    return ApiBadRequest("Category could not be created.", "CategoryCreationErrorCode");

                return Ok(new CategoryResponse
                {
                    CategoryName = createdCategory.CategoryName,
                    Description = createdCategory.Description,
                    Id = createdCategory.Id

                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled error while creating a new asset.");
                return ApiInternalError(ex.Message, "NewCategoryErrorCode");
            }
        }
    }
}