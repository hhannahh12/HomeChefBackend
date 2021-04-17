
using System.Collections.Generic;
namespace HomeChefBackend.Models
{

    public class FavoriteRecipeModel
    {
        public string favoritesId { get; set; }
        public string recipeId { get; set; }
        public string title { get; set; }
        public string imageUrl { get; set; }
        public string servings { get; set; }
        public string readyIn { get; set; }
        public List<string> instructions { get; set; }
        public List<string> ingredients { get; set; }
    
    }
}