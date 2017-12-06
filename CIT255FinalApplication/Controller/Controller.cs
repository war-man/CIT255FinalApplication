using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;
using BusinessLayer;

namespace Controller
{
    public class Controller
    {
        #region ENUMERABLES

        #endregion

        #region FIELDS

        bool active = true;

        //
        // response repository instantiated AGAINST the interface
        //
        // because we DECLARE the responseRepository variable up in
        // the fields section and make it static, you are ALLOWED
        // to instantiate it in different data formats based on
        // user choice in a switch statement and pass that 
        // repository into the controller
        static IResponseRepository responseRepository;

        #endregion

        #region PROPERTIES


        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// instantiate the repository for data type and start application
        /// </summary>
        public Controller()
        {
            responseRepository = new ResponseRepositoryXML();

            ApplicationControl();
        }

        #endregion

        #region METHODS

        private void ApplicationControl()
        {
            AppEnum.ManagerAction userActionChoice;

            //TODO: View: call welcome screen
            
            userActionChoice = AppEnum.ManagerAction.None;

            while (active)
            {
                Console.WriteLine("write your choice:");
                string answer = Console.ReadLine();

                switch (answer)
                {
                    case "welcome":
                        userActionChoice = AppEnum.ManagerAction.WelcomePage;
                        break;
                    case "get":
                        userActionChoice = AppEnum.ManagerAction.GetWeather;
                        break;
                    case "customize":
                        userActionChoice = AppEnum.ManagerAction.CustomizePlantingDay;
                        break;
                    case "auto":
                        userActionChoice = AppEnum.ManagerAction.AutoFillPlantingDays;
                        break;
                    case "toggle":
                        userActionChoice = AppEnum.ManagerAction.TogglePlantingDay;
                        break;
                    default:
                        break;
                }
                //TODO: View: get user action choice
                

                switch (userActionChoice)
                {
                    case AppEnum.ManagerAction.None:
                        break;
                    case AppEnum.ManagerAction.WelcomePage:
                        WelcomePage();
                        break;
                    case AppEnum.ManagerAction.GetWeather:
                        GetWeather();
                        break;
                    case AppEnum.ManagerAction.CustomizePlantingDay:
                        CustomizePlantingDay();
                        break;
                    case AppEnum.ManagerAction.AutoFillPlantingDays:
                        AutoFillPlantingDays();
                        break;
                    case AppEnum.ManagerAction.TogglePlantingDay:
                        TogglePlantingDay();
                        break;
                    case AppEnum.ManagerAction.Print:
                        Print();
                        break;
                    case AppEnum.ManagerAction.Exit:
                        Exit();
                        active = false;
                        break;
                    default:
                        break;
                }                
            }

            //TODO: View: exit application
        }

        private void Exit()
        {
            //TODO: View: display the closing screen
        }

        private void Print()
        {
            //TODO: View: pop up a PDF with a printout or at least screenshot?
        }

        private void TogglePlantingDay()
        {
            //
            // instantiate a responseBusiness class and pass the repository into it
            //
            ResponseBusiness responseBusiness = new ResponseBusiness(responseRepository);
            Response response;

            using (responseBusiness)
            {
                //TODO: View: update the planting day according to which day was clicked
                //responseBusiness.TogglePlantingDay(event handler?);
                responseBusiness.TogglePlantingDay(1);
                responseBusiness.Save();
            }

            //TODO: View: display the updated planting days dynamically 
        }

        private void AutoFillPlantingDays()
        {
            //
            // instantiate a responseBusiness class and pass the repository into it
            //
            ResponseBusiness responseBusiness = new ResponseBusiness(responseRepository);

            using (responseBusiness)
            {
                responseBusiness.AutoFillPlantingDays();
            }

            //TODO: View: display the updated planting days dynamically        
        }


        private void CustomizePlantingDay()
        {
            //TODO: View: display the customize planting day form
        }

        private void GetWeather()
        {
            //
            // instantiate a responseBusiness class and pass the repository into it
            //
            ResponseBusiness responseBusiness = new ResponseBusiness(responseRepository);
            Response response;

            using (responseBusiness)
            {
                responseBusiness.AssignIsRainyDay();
                response = responseBusiness.SelectAll();
                //TODO: View: display fresh API pull form
            }
        }

        private void WelcomePage()
        {
            //TODO: View: display welcome page
        }

        private void TestingAChange()
        {
            ResponseBusiness responseBusiness = new ResponseBusiness(responseRepository);

            Response response;

            using (responseBusiness)
            {
                response = responseBusiness.SelectAll();
            }

            long lengthOfForecast = response.Forecast.Simpleforecast.Forecastdays.Forecastday.LongCount();

            long oneLessThanFullList = lengthOfForecast - 1;

            for (long index = 0; index < oneLessThanFullList; index++)
            {
                if (response.Forecast.Simpleforecast.Forecastdays.Forecastday[(int)index + 1].Pop >= 50)
                {
                    response.Forecast.Simpleforecast.Forecastdays.Forecastday[(int)index].IsPlantingDay = true;
                }
            }

            foreach (var fd in response.Forecast.Simpleforecast.Forecastdays.Forecastday)
            {
                Console.WriteLine(fd.Date.Weekday);
                Console.WriteLine(fd.Pop);
                Console.WriteLine(fd.IsPlantingDay);
                Console.WriteLine();
            }
        }

        private void TestingMethods()
        {
            ResponseBusiness responseBusiness = new ResponseBusiness(responseRepository);

            Response response;

            using (responseBusiness)
            {
                response = responseBusiness.SelectAll();
            }

            foreach (var fd in response.Forecast.Simpleforecast.Forecastdays.Forecastday)
            {
                Console.WriteLine(fd.Period);
                Console.WriteLine(fd.Date.Weekday);
                Console.WriteLine(fd.Pop);
                Console.WriteLine(fd.IsPlantingDay);
            }

            ResponseBusiness responseBusiness2 = new ResponseBusiness(responseRepository);
            Forecastday forecastDay;
            using (responseBusiness2)
            {
                forecastDay = responseBusiness2.SelectByPeriod(1);

            }

            Console.WriteLine($"We found the first day! {forecastDay.Date.Weekday}, {forecastDay.Date.Monthname} {forecastDay.Date.Day}");

            Console.WriteLine("Press any key to exit");

            Console.ReadKey();
        }

        #endregion
    }
}
