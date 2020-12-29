using API.Dtos;
using API.Helper;
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
    
    public class ProductsController : BaseApiController
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ProductsController> logger)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            IActionResult result;

            try
            {
                ProductSpecification productSpecification = new ProductSpecification(id);
                Product product = await _unitOfWork.Repository<Product>().GetEntityWithSpecAsync(productSpecification);
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
        public async Task<ActionResult<QueryResult<ProductDto>>> GetProducts([FromQuery] ProductSpecParams spec)
        {
            ProductSpecification productSpecification = new ProductSpecification(spec);
            IEnumerable<Product> products = await _unitOfWork.Repository<Product>().GetEntitiesWithSpecAsync(productSpecification);
            IEnumerable<ProductDto> productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);

            ProductPagingSpecification pagingSpec = new ProductPagingSpecification(spec);
            int totalCount = await _unitOfWork.Repository<Product>().GetTotalCountAsync(pagingSpec);

            QueryResult<ProductDto> queryResult = 
                new QueryResult<ProductDto> { Count = totalCount, Data = productsDto, PageIndex = spec.PageIndex, PageSize = spec.PageSize };

            return Ok(queryResult);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetProductBrands()
        {
            IEnumerable<ProductBrand> productBrands = await _unitOfWork.Repository<ProductBrand>().GetAllItemsAsync();
            return Ok(productBrands);
        }

        [HttpGet("brands/{id}")]
        public async Task<ActionResult<ProductBrand>> GetBrand(int id)
        {
            ProductBrand productBrand = await _unitOfWork.Repository<ProductBrand>().GetItemByIdAsync(id);
            return Ok(productBrand);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<ProductType>>> GetProductTypes()
        {
            IEnumerable<ProductType> productTypes = await _unitOfWork.Repository<ProductType>().GetAllItemsAsync();
            return Ok(productTypes);
        }

        [HttpGet("types/{id}")]
        public async Task<ActionResult<ProductType>> GetType(int id)
        {
            ProductType productType = await _unitOfWork.Repository<ProductType>().GetItemByIdAsync(id);
            return Ok(productType);
        }
    }
}
