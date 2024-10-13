using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Model.Domain;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var regions = new List<Region>();
                regions.Add(new Region
                {
                    Id = new Guid(),
                    Name = "Rive du sud",
                    Code = "Richelieu",
                    RegionImageUrl = "https://maps.app.goo.gl/V8JfTLSd4QpVBtKb9"
                });

                regions.Add(new Region
                {
                    Id = new Guid(),
                    Name = "Montreal",
                    Code = "MTL",
                    RegionImageUrl = "https://maps.app.goo.gl/V8JfTLSd4QpVBtKb9"
                });


                return Ok(regions);
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
