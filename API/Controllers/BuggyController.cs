using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly StoreContext _context;

        public BuggyController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            var product = _context.Products.Find(-1);

            if (product == null)
            {
                return NotFound(new ApiErrorResponse(404));  // return 404
            }

            return Ok(product);
            
        }

        [HttpGet("servererror")]
        public ActionResult GetServerErrorRequest()
        {
            var product = _context.Products.Find(-1);

            // return 500
            var something = product.ToString();

            return Ok(something);
        }

        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiErrorResponse(400));    // return 400
        }

        [HttpGet("badrequest/{id}")]
        public ActionResult GetBadRequest(int id)
        {
            // return 400
            return Ok();
        }
    }
}
