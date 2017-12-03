using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
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

        public Controller()
        {
            responseRepository = new ResponseRepositoryXML();

            ApplicationControl();
        }

        #endregion

        #region METHODS

        private void ApplicationControl()
        {
            while (active)
            {
                TestingAChange();

                Console.ReadKey();
            }
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
