using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productsRepository;
        private readonly IGenericRepository<ProductBrand> _productBrandsRepository;
        private readonly IGenericRepository<ProductType> _productTypesRepository;
        private readonly ILogger<ProductsController> _logger;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productsRepository,
            IGenericRepository<ProductBrand> productBrandsRepository, IGenericRepository<ProductType> productTypesRepository, 
            ILogger<ProductsController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _productsRepository = productsRepository;
            _productTypesRepository = productTypesRepository;
            _productBrandsRepository = productBrandsRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            IActionResult result = null;

            try
            {
                ProductSpecification productSpecification = new ProductSpecification(id);
                Product product = await _productsRepository.GetEntityWithSpecAsync(productSpecification);
                ProductDto productDto = _mapper.Map<ProductDto>(product);
                result = Ok(productDto);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occured trying to fetch product item");
                result = BadRequest(ex.Message);
            }
            
            return result;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            ProductSpecification productSpecification = new ProductSpecification();
            IEnumerable<Product> products = await _productsRepository.GetEntitiesWithSpecAsync(productSpecification);
            IEnumerable<ProductDto> productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Ok(productsDto);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetProductBrands()
        {
            IEnumerable<ProductBrand> productBrands = await _productBrandsRepository.GetAllItemsAsync();
            return Ok(productBrands);
        }

        [HttpGet("brands/{id}")]
        public async Task<ActionResult<ProductBrand>> GetBrand(int id)
        {
            ProductBrand productBrand = await _productBrandsRepository.GetItemByIdAsync(id);
            return Ok(productBrand);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<ProductType>>> GetProductTypes()
        {
            IEnumerable<ProductType> productTypes = await _productTypesRepository.GetAllItemsAsync();
            return Ok(productTypes);
        }

        [HttpGet("types/{id}")]
        public async Task<ActionResult<ProductType>> GetType(int id)
        {
            ProductType productType = await _productTypesRepository.GetItemByIdAsync(id);
            return Ok(productType);
        }
    }
}
