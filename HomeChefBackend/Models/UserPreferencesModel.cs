using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace HomeChefBackend.Models
{
    public class UserPreferencesModel
    {
        public string UserId { get; set; }
        public string PreferencesId { get; set; }
        public string Portion { get; set; }
        public string DietryRequirements { get; set; }
        public string Intollerances { get; set; }
        public string MeasuringUnit { get; set; }
        
    }
}
