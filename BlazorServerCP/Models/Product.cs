using Newtonsoft.Json;

namespace BlazorServerCP.Models
  
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public Category Category { get; set; } = new Category();
        public int? CategoryId { get; set; }
        public string ImageURL { get; set; } = "";
        public double Price { get; set; }
        public bool IsEditing { get; set; }

        [JsonIgnore]
        public int SelectedCategoryId { get; set; }
    }
}
