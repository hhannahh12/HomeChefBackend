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
        public int Portion { get; set; }
        public string[] DietryRequirements { get; set; }

        public string[] Intollerances { get; set; }
        public bool MeasuringUnit { get; set; }

        public UserPreferencesModel(
            string userid,
            string preferencesId,
            int portion,
            string[] dietryRequirements,
            string[] intollerances,
            bool measuringUnit)
        {
            UserId = userid;
            PreferencesId = preferencesId;
            Portion = portion;
            DietryRequirements = dietryRequirements;
            Intollerances = intollerances;
            MeasuringUnit = measuringUnit;
        }
       

    }
}
