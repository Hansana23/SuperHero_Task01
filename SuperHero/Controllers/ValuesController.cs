using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHero.Models;

namespace SuperHero.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly SuperHeroDBContext _context;

        public ValuesController(SuperHeroDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hero>>> GetHeros()
        {
            return await _context.SuperHeros.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Hero>> GetSuperHero(int id)
        {
            var hero = await _context.SuperHeros.FindAsync(id);
            if(hero==null)
            {
                return BadRequest("Hero Not Found");
            }
            return Ok(hero);
        }

        [HttpPost]

        public async Task<ActionResult<Hero>> PostSuperHero(Hero superHero)
        {
            _context.SuperHeros.Add(superHero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeros.ToListAsync());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Hero>> PutSuperHero(int id, [FromBody] Hero superHero)
        {
            if (id != superHero.Id)
            {
                return BadRequest();
            }

            _context.Entry(superHero).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuperHeroExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(superHero);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSuperHero(int id)
        {
            var superHero = await _context.SuperHeros.FindAsync(id);
            if (superHero == null)
            {
                return NotFound();
            }

            _context.SuperHeros.Remove(superHero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeros.ToListAsync());
        }

        private bool SuperHeroExists(int id)
        {
            throw new NotImplementedException();
        }
    }
}
