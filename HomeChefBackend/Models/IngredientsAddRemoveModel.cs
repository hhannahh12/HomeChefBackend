using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace HomeChefBackend.Models
{
    public class IngredientsAddRemoveModel
    {
        public string PantryId { get; set; }
        public IngredientModel[] Ingredients { get; set; }
    }
}
