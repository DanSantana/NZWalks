using AutoMapper;
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
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var regionsDomain = await regionRepository.GetAllAsync();
                var regionDtos = new List<RegionDto>();
                regionDtos = mapper.Map<List<RegionDto>>(regionsDomain);

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

            return Ok(mapper.Map<RegionDto>(region));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto regionDto)
        {
            var regionDomain = mapper.Map<Region>(regionDto);
            regionDomain = await regionRepository.CreateAsync(regionDomain);
            var newRegionDto = mapper.Map<RegionDto>(regionDomain);

            return CreatedAtAction(nameof(GetById), new { id = newRegionDto.Id }, regionDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateDtoReques)
        {
            var regionToUpdate = mapper.Map<Region>(updateDtoReques);
            var regionUpdated = await regionRepository.UpdateAsync(id, regionToUpdate);
            return Ok(mapper.Map<RegionDto>(regionUpdated));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomain = await regionRepository.DeleteAsync(id);
            if (regionDomain == null)
                return NotFound();

            var RegionDto = mapper.Map<RegionDto>(regionDomain);
            return Ok($"Region with was deleted with success. \n {System.Text.Json.JsonSerializer.Serialize(RegionDto)}");
        }
    }
}
