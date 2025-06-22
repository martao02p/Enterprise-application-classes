using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PizzeriaApi.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        [JsonIgnore]
        public List<Pizza> Pizzas { get; set; } = new();
        public bool IsSystem { get; set; } = false;

        [NotMapped]
        public List<int>? PizzaIds { get; set; }
    }

}
