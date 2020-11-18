using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public ProductsController(IGenericRepository<Product> productsRepository,
            IGenericRepository<ProductBrand> productBrandsRepository, IGenericRepository<ProductType> productTypesRepository)
        {
            _productsRepository = productsRepository;
            _productTypesRepository = productTypesRepository;
            _productBrandsRepository = productBrandsRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            Product product = await _productsRepository.GetItemByIdAsync(id);
            return Ok(product);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            IEnumerable<Product> products = await _productsRepository.GetAllItemsAsync();
            return Ok(products);
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
