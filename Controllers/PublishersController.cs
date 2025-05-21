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
    public class PublishersController : ControllerBase
    {
        private readonly AppDbContext _db;
        public PublishersController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Publisher>>> Get() =>
            await _db.Publishers.Include(p => p.Books).ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Publisher>> Get(int id)
        {
            var pub = await _db.Publishers.Include(p => p.Books)
                                          .FirstOrDefaultAsync(p => p.PublisherId == id);
            if (pub == null) return NotFound();
            return pub;
        }

        [HttpPost]
        public async Task<ActionResult<Publisher>> Post(Publisher publisher)
        {
            _db.Publishers.Add(publisher);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = publisher.PublisherId }, publisher);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Publisher publisher)
        {
            if (id != publisher.PublisherId) return BadRequest();
            _db.Entry(publisher).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var pub = await _db.Publishers.FindAsync(id);
            if (pub == null) return NotFound();
            _db.Publishers.Remove(pub);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
