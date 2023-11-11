using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB_153505_PIKHTOVNIKAVA.API.Data;
using WEB_153505_PIKHTOVNIKAVA.Domain.Entities;
using WEB_153505_PIKHTOVNIKAVA.API.Services.SeasonCategoryService;
using WEB_153505_PIKHTOVNIKAVA.Domain.Models;

namespace WEB_153505_PIKHTOVNIKAVA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeasonCategoriesController : ControllerBase
    {
        private readonly ISeasonCategoryService _service;
        private readonly IWebHostEnvironment env;
        private readonly IConfiguration conf;
        private readonly string appUrl;


        public SeasonCategoriesController(ISeasonCategoryService service)
        {
            _service = service;
        }

        // GET: api/SeasonCategories
        [HttpGet]
        public async Task<ActionResult<ResponseData<List<SeasonCategory>>>> Getseason()
        {
            var categoryResponse = await _service.GetCategoryListAsync();

            if (!categoryResponse.Success)
                return NotFound(categoryResponse.ErrorMessage);

            return Ok(categoryResponse);
        }

        // GET: api/SeasonCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SeasonCategory>> GetSeasonCategory(int id)
        {
            var categories = (await _service.GetCategoryListAsync()).Data;
            if (categories == null)
            {
                return NotFound();
            }
            var engineTypeCategory = categories.Where(c => c.Id == id).FirstOrDefault();

            if (engineTypeCategory == null)
            {
                return NotFound();
            }

            return engineTypeCategory;
        }

        //// PUT: api/SeasonCategories/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutSeasonCategory(int id, SeasonCategory seasonCategory)
        //{
        //    if (id != seasonCategory.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(seasonCategory).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!SeasonCategoryExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/SeasonCategories
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<SeasonCategory>> PostSeasonCategory(SeasonCategory seasonCategory)
        //{
        //  if (_context.season == null)
        //  {
        //      return Problem("Entity set 'AppDbContext.season'  is null.");
        //  }
        //    _context.season.Add(seasonCategory);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetSeasonCategory", new { id = seasonCategory.Id }, seasonCategory);
        //}

        //// DELETE: api/SeasonCategories/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteSeasonCategory(int id)
        //{
        //    if (_context.season == null)
        //    {
        //        return NotFound();
        //    }
        //    var seasonCategory = await _context.season.FindAsync(id);
        //    if (seasonCategory == null)
        //    { 
        //        return NotFound();
        //    }

        //    _context.season.Remove(seasonCategory);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool SeasonCategoryExists(int id)
        //{
        //    return (_context.season?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
