using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopApi.Entities;
using ShopApi.Models;
using ShopApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApi.Controllers
{
    [Route("api/sklep")]
    [ApiController]
    [Authorize]
    public class SklepController : ControllerBase
    {
        private readonly ISklepService _sklepService;

        public SklepController(ISklepService sklepService)
        {
            _sklepService = sklepService;
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public ActionResult CreateArtykul([FromBody] CreateArtykulDto dto)
        {

            var id = _sklepService.Create(dto);

            return Created($"/api/sklep/{id}", null);

        }

        
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult Update([FromBody] UpdateArtykulDto dto, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var isUpdated = _sklepService.Update(id, dto);

            if (!isUpdated)
                return NotFound();

            return Ok();
        }



        [HttpGet]
        public ActionResult<IEnumerable<Artykul>> GetAll([FromQuery] StronnicowanieDto stronnicowanie)
        {
            var sklepDtos = _sklepService.GetAll(stronnicowanie);

            return Ok(sklepDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<Artykul> Get([FromRoute] int id)
        {
            var sklep = _sklepService.GetById(id);

            if (sklep is null)
            {
                return NotFound();
            }

            return Ok(sklep);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete([FromRoute] int id)
        {
            var isDeleted = _sklepService.Delete(id);

            if (isDeleted)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
