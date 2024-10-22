using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Model.Domain;
using NZWalks.API.Model.DTO;

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
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var regionsDomain = await dbContext.Regions.ToListAsync();
                var regionDtos = new List<RegionDto>();
                foreach (var region in regionsDomain)
                {
                    regionDtos.Add(SerializeToDto(region));
                }
                return Ok(regionDtos);
            }
            catch (Exception e)
            {
                return NotFound(new { e.Message });
            }
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var region = await dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);
            if (region == null)
                return NotFound();

            return Ok(SerializeToDto(region));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto regionDto)
        {
            var regionDomain = new Region
            {
                Code = regionDto.Code,
                Name = regionDto.Name,
                RegionImageUrl = regionDto.RegionImageUrl
            };
            await dbContext.Regions.AddAsync(regionDomain);
            await dbContext.SaveChangesAsync();

            var newRegionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetById), new { id = newRegionDto.Id }, regionDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateDtoReques)
        {
            // validar id
            var regionDomain = await dbContext.Regions.FirstOrDefaultAsync(r=>r.Id == id);
            if (regionDomain == null)
            {
                return NotFound();
            }
            //Map dto to domain
            regionDomain.Name = updateDtoReques.Name;
            regionDomain.Code = updateDtoReques.Code;
            regionDomain.RegionImageUrl = updateDtoReques.RegionImageUrl;           

            // save
            dbContext.SaveChanges();
            //dto re map
            var newRegionDto = SerializeToDto(regionDomain);

            // return dto
            return Ok(newRegionDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomain = await dbContext.Regions.FirstOrDefaultAsync(x=>x.Id == id);
            if (regionDomain == null)
                return NotFound();

            dbContext.Regions.Remove(regionDomain);
            await dbContext.SaveChangesAsync();

            var RegionDto = SerializeToDto(regionDomain);

            return Ok($"Region with was deleted with success. \n {System.Text.Json.JsonSerializer.Serialize(RegionDto)}");
        }


        private RegionDto SerializeToDto(Region region)
        {
            return new RegionDto
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };
        }


    }
}
