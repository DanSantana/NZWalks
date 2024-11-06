using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Model.Domain;
using NZWalks.API.Model.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;

        public RegionsController( IRegionRepository regionRepository)
        {
            this.regionRepository = regionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var regionsDomain = await regionRepository.GetAllAsync();
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
            var region = await regionRepository.GetByIdAsync(id);
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
            regionDomain = await regionRepository.CreateAsync(regionDomain);

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
            var regionToUpdate = new Region()
            {
                Code = updateDtoReques.Code,
                Name = updateDtoReques.Name,
                RegionImageUrl = updateDtoReques.RegionImageUrl
            };
            var regionUpdated = await regionRepository.UpdateAsync(id, regionToUpdate);

            // return dto
            return Ok(SerializeToDto(regionUpdated));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomain = await regionRepository.DeleteAsync(id);
            if (regionDomain == null)
                return NotFound();

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
