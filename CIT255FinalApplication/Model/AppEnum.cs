using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class AppEnum
    {
        /// <summary>
        /// an enum to keep track of application flow
        /// </summary>
        public enum ManagerAction
        {
            None,
            WelcomePage,
            GetWeather,
            CustomizePlantingDay,
            AutoFillPlantingDays,
            TogglePlantingDay,
            Print,
            Exit
        }
    }
}
