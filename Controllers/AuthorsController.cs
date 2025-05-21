using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DomainTables;

namespace SimpleApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public AuthorsController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> Get() =>
            await _db.Authors.Include(a => a.Books).ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> Get(int id)
        {
            var author = await _db.Authors.Include(a => a.Books)
                                          .FirstOrDefaultAsync(a => a.AuthorId == id);
            if (author == null) return NotFound();
            return author;
        }

        [HttpPost]
        public async Task<ActionResult<Author>> Post(Author author)
        {
            _db.Authors.Add(author);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = author.AuthorId }, author);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Author author)
        {
            if (id != author.AuthorId) return BadRequest();
            _db.Entry(author).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var author = await _db.Authors.FindAsync(id);
            if (author == null) return NotFound();
            _db.Authors.Remove(author);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
