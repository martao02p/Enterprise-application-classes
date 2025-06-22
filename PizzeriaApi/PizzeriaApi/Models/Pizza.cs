namespace PizzeriaApi.Models
{
    public class Pizza
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal SmallPrice { get; set; }
        public decimal MediumPrice { get; set; }
        public decimal LargePrice { get; set; }
        public string DoughType { get; set; } = "thin"
        public List<Ingredient> Ingredients { get; set; } = new();
    }
}
