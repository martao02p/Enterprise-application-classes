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
    public class IngredientsController : ControllerBase
    {
        private readonly PizzeriaContext _context;

        public IngredientsController(PizzeriaContext context)
        {
            _context = context;
        }

        // GET: api/Ingredients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ingredient>>> GetToppings()
        {
            return await _context.Ingredients.ToListAsync();
        }

        // GET: api/Ingredients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ingredient>> GetIngredient(int id)
        {
            var ingredient = await _context.Ingredients.FindAsync(id);

            if (ingredient == null)
            {
                return NotFound();
            }

            return ingredient;
        }

        // PUT: api/Ingredients/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIngredient(int id, Ingredient ingredient)
        {
            if (id != ingredient.Id)
                return BadRequest();

            var existingIngredient = await _context.Ingredients
                .Include(i => i.Pizzas) // załaduj relacje
                .FirstOrDefaultAsync(i => i.Id == id);

            if (existingIngredient == null)
                return NotFound();

            if (existingIngredient.IsSystem)
                return BadRequest("Nie można edytować składnika systemowego.");

            existingIngredient.Name = ingredient.Name;
            existingIngredient.Pizzas.Clear();

            if (ingredient.Pizzas != null)
            {
                foreach (var pizza in ingredient.Pizzas)
                {
                    var existingPizza = await _context.Pizzas.FindAsync(pizza.Id);
                    if (existingPizza != null)
                        existingIngredient.Pizzas.Add(existingPizza);
                }
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }



        // POST: api/Ingredients
        public async Task<ActionResult<Ingredient>> PostIngredient(Ingredient ingredient)
        {
            if (ingredient.Pizzas != null)
            {
                foreach (var pizza in ingredient.Pizzas)
                {
                    _context.Entry(pizza).State = EntityState.Unchanged;
                }
            }

            _context.Ingredients.Add(ingredient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIngredient", new { id = ingredient.Id }, ingredient);
        }



        // DELETE: api/Ingredients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredient(int id)
        {
            var ingredient = await _context.Ingredients.FindAsync(id);
            if (ingredient == null)
                return NotFound();

            if (ingredient.IsSystem)
                return BadRequest("Nie można usunąć składnika systemowego.");


            _context.Ingredients.Remove(ingredient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IngredientExists(int id)
        {
            return _context.Ingredients.Any(e => e.Id == id);
        }
    }
}
