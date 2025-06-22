using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzeriaApi.Models;

namespace PizzeriaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzasController : ControllerBase
    {
        private readonly PizzeriaContext _context;

        public PizzasController(PizzeriaContext context)
        {
            _context = context;
        }

        // GET: api/Pizzas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pizza>>> GetPizzas()
        {
            return await _context.Pizzas
                .Include(p => p.Ingredients)
                .ToListAsync();
        }


        // GET: api/Pizzas/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Pizza>> GetPizza(int id)
        {
            var pizza = await _context.Pizzas
                .Include(p => p.Ingredients)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pizza == null)
                return NotFound();

            return pizza;
        }


        // PUT: api/Pizzas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPizza(int id, Pizza updatedPizza)
        {
            if (id != updatedPizza.Id)
                return BadRequest("ID nie zgadza się z adresem URL.");

            var existingPizza = await _context.Pizzas
                .Include(p => p.Ingredients)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (existingPizza == null)
                return NotFound();

            updatedPizza.Ingredients ??= new List<Ingredient>();

            var systemIngredients = await _context.Ingredients
                .Where(i => i.IsSystem)
                .ToListAsync();

            foreach (var sysIng in systemIngredients)
            {
                if (!updatedPizza.Ingredients.Any(i => i.Id == sysIng.Id))
                    updatedPizza.Ingredients.Add(sysIng);
            }

            var ingredientIds = updatedPizza.Ingredients.Select(i => i.Id).ToList();
            var existingIngredients = await _context.Ingredients
                .Where(i => ingredientIds.Contains(i.Id))
                .ToListAsync();

            if (existingIngredients.Count != ingredientIds.Count)
                return BadRequest("Niektóre składniki nie istnieją.");

            existingPizza.Name = updatedPizza.Name;
            existingPizza.SmallPrice = updatedPizza.SmallPrice;
            existingPizza.MediumPrice = updatedPizza.MediumPrice;
            existingPizza.LargePrice = updatedPizza.LargePrice;
            existingPizza.DoughType = updatedPizza.DoughType;
            existingPizza.Ingredients = existingIngredients;

            await _context.SaveChangesAsync();

            return NoContent();
        }




        // POST: api/Pizzas
        [HttpPost]
        public async Task<ActionResult<Pizza>> PostPizza(Pizza pizza)
        {
            pizza.Ingredients ??= new List<Ingredient>();

            var systemIngredients = await _context.Ingredients
                .Where(i => i.IsSystem)
                .ToListAsync();

            foreach (var sysIng in systemIngredients)
            {
                if (!pizza.Ingredients.Any(i => i.Id == sysIng.Id))
                    pizza.Ingredients.Add(sysIng);
            }

            var ingredientIds = pizza.Ingredients.Select(i => i.Id).ToList();
            var existingIngredients = await _context.Ingredients
                .Where(i => ingredientIds.Contains(i.Id))
                .ToListAsync();

            if (existingIngredients.Count != ingredientIds.Count)
                return BadRequest("Niektóre składniki nie istnieją.");

            pizza.Ingredients = existingIngredients;

            _context.Pizzas.Add(pizza);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPizza), new { id = pizza.Id }, pizza);
        }




        // DELETE: api/Pizzas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePizza(int id)
        {
            var pizza = await _context.Pizzas
                .Include(p => p.Ingredients)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pizza == null)
            {
                return NotFound();
            }

            _context.Pizzas.Remove(pizza);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PizzaExists(int id)
        {
            return _context.Pizzas.Any(e => e.Id == id);
        }
    }
}
