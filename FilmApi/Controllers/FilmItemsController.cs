using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FilmApi.Models;

namespace FilmApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmItemsController : ControllerBase
    {
        private readonly FilmContext _context;

        public FilmItemsController(FilmContext context)
        {
            _context = context;
        }

        // GET: api/FilmItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FilmItem>>> GetFilmItems()
        {
            return await _context.FilmItems.ToListAsync();
        }

        // GET: api/FilmItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FilmItem>> GetFilmItem(int id)
        {
            var filmItem = await _context.FilmItems.FindAsync(id);

            if (filmItem == null)
            {
                return NotFound();
            }

            return filmItem;
        }

        // PUT: api/FilmItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilmItem(int id, FilmItem filmItem)
        {
            if (id != filmItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(filmItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmItemExists(id))
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

        // POST: api/FilmItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FilmItem>> PostFilmItem(FilmItem filmItem)
        {
            _context.FilmItems.Add(filmItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFilmItem), new { id = filmItem.Id }, filmItem);
        }

        // DELETE: api/FilmItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilmItem(int id)
        {
            var filmItem = await _context.FilmItems.FindAsync(id);
            if (filmItem == null)
            {
                return NotFound();
            }

            _context.FilmItems.Remove(filmItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FilmItemExists(int id)
        {
            return _context.FilmItems.Any(e => e.Id == id);
        }
    }
}
