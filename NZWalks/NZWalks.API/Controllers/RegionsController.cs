using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Model.Domain;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;

        public RegionsController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var regions = dbContext.Regions.ToList();
                return Ok(regions);
            }
            catch (Exception e)
            {                
                return NotFound(new { e.Message });
            }
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var region = dbContext.Regions.FirstOrDefault(r => r.Id == id);

            if (region == null)
                return NotFound();            

         return Ok(region);
        }


    }
}
