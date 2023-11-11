using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB_153505_PIKHTOVNIKAVA.API.Data;
using WEB_153505_PIKHTOVNIKAVA.Domain.Entities;
using WEB_153505_PIKHTOVNIKAVA.API.Services.ProductService;
using WEB_153505_PIKHTOVNIKAVA.Domain.Models;
using Microsoft.AspNetCore.Authorization;

namespace WEB_153505_PIKHTOVNIKAVA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SneakersController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly IWebHostEnvironment env;
        private readonly IConfiguration conf;
        private readonly string appUrl;

        public SneakersController(IProductService service, IWebHostEnvironment env, IConfiguration conf)
        {
            _service = service;
            this.env = env;
            this.conf = conf;
            appUrl = conf.GetSection("AppUrl").Value!;
        }

        // GET: api/Sneakers
        [HttpGet]
        [Route("")]
        [Route("{category}/pageno{pageno:int}/pagesize{pagesize:int}")]
        [Route("{category}/pageno{pageno:int}")]
        [Route("{category}/pagesize{pagesize:int}")]
        [Route("pageno{pageno:int}/pagesize{pagesize:int}")]
        [Route("pageno{pageno:int}")]
        [Route("{category}")]
        [AllowAnonymousAttribute]
        public async Task<ActionResult<IEnumerable<Sneaker>>> Getsneakers(string? category = null, int pageNo = 1, int pageSize = 3)
        {
            
            var responde = await _service.GetProductListAsync(category, pageNo, pageSize);
            if (!responde.Success)
            {
                return NotFound();
            }

            return Ok(responde);
        }

        // GET: api/Sneakers/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Sneaker>> GetSneaker(int id)
        {
            var response = await _service.GetProductByIdAsync(id);
            if (!response.Success)
            {
                return NotFound();
            }
            var airplane = response.Data;

            if (airplane == null)
            {
                return NotFound();
            }

            //return airplane;
            return Ok(response);
        }

        // PUT: api/Sneakers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        //[Authorize]
        [AllowAnonymousAttribute]
        public async Task<IActionResult> PutSneaker(int id, Sneaker sneaker)
        {
            if (id != sneaker.Id)
            {
                return BadRequest();
            }

            try
            {
                await _service.UpdateProductAsync(id, sneaker);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SneakerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Sneakers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Sneaker>> PostSneaker(Sneaker sneaker)
        {
            await _service.CreateProductAsync(sneaker);
            return CreatedAtAction("GetSneaker", new { id = sneaker.Id }, sneaker);

        }

        // DELETE: api/Sneakers/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteSneaker(int id)
        {
            await _service.DeleteProductAsync(id);

            return NoContent();
        }

        private bool SneakerExists(int id)
        {
            var response = _service.GetProductByIdAsync(id).Result;
            if (!response.Success || response.Data == null)
            {
                return false;
            }

            return true;
        }

        [HttpPost("{id}")]
        [Authorize]
        public async Task<ActionResult<ResponseData<string>>> PostImage(
                                                                int id,
                                                                IFormFile formFile)
        {
            var response = await _service.SaveImageAsync(id, formFile);
            if (response.Success) 
            {
                return Ok(response);
            }
            return NotFound(response);
        }

}
}
