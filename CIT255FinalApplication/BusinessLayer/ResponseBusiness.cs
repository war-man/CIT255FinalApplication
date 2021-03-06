﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;

namespace BusinessLayer
{
    /// <summary>
    /// The business logic class for the wunderground api response class, inherits from the IDisposable interface so it can be used in a using statement, and from the IResponseRepository interface so it can be applied to any data format for a repository
    /// </summary>
    public class ResponseBusiness : IDisposable, IResponseRepository
    {
        IResponseRepository _responseRepository;
        
        /// <summary>
        /// instantiate the ResponseBusiness class object with the repository as the input parameter set the internal copy of _responseRepository equal to the input parameter so that the business class is using repository that you pass it from the business class's calling method and uses it throughout its methods like Insert, Delete, etc 
        /// </summary>
        /// <param name="repository"></param>
        public ResponseBusiness(IResponseRepository repository)
        {
            _responseRepository = repository;
        }

        /// <summary>
        /// implements the Dispose() method to comply with the IDisposable interface
        /// </summary>
        public void Dispose()
        {
            _responseRepository = null;
        }

        /// <summary>
        /// Writes to the repository file to serialize the data for data storage
        /// </summary>
        public void Save()
        {
            _responseRepository.Save();
        }

        /// <summary>
        /// returns the whole forecast as a response object
        /// </summary>
        /// <returns>a response repository</returns>
        public Response SelectAll()
        {
            return _responseRepository.SelectAll();
        }

        /// <summary>
        /// given the period number for the day, returns a forecastday
        /// </summary>
        /// <param name="period"></param>
        /// <returns>forecastday</returns>
        public Forecastday SelectByPeriod(int period)
        {
            return _responseRepository.SelectByPeriod(period);
        }

        /// <summary>
        /// Automatically assigns the IsRainyDay bool for each day in the forecast, according to the business logic of greater than 50% chance of precipitation is a rainy day
        /// </summary>
        public void AssignIsRainyDay()
        {
            Response response = _responseRepository.SelectAll();

            long lengthOfForecast = response.Forecast.Simpleforecast.Forecastdays.Forecastday.LongCount();

            for (int index = 0; index < lengthOfForecast; index++)
            {
                Forecastday fd = response.Forecast.Simpleforecast.Forecastdays.Forecastday[(int)index];
                if (fd.Pop >= 50)
                {
                    fd.IsRainyDay = true;
                }
            }

            _responseRepository.Save();
        }

        /// <summary>
        /// Automatically assigns the IsPlantingDay bool for each day in the forecast, according to the business logic of checking the following day's percentage of precipitation, and if it is greater than 50%, then we say the day before is a planting day
        /// </summary>
        public void AutoFillPlantingDays()
        {
            Response response = _responseRepository.SelectAll();

            long lengthOfForecast = response.Forecast.Simpleforecast.Forecastdays.Forecastday.LongCount();
            
            //we only want to iterate through one less than the full forecast because our planting day logic is based on the weather forecast for the day after the current day, but we only know 10 days' worth of weather
            long oneLessThanFullList = lengthOfForecast - 1;

            for (long index = 0; index < oneLessThanFullList; index++)
            {
                Forecastday fdToday = response.Forecast.Simpleforecast.Forecastdays.Forecastday[(int)index];
                Forecastday fdTomorrow = response.Forecast.Simpleforecast.Forecastdays.Forecastday[(int)index + 1];
                //our business logic is that a planting day cannot be a rainy day, but the day AFTER that day should be a rainy day
                if (fdTomorrow.IsRainyDay == true
                    && fdToday.IsRainyDay == false)
                {
                    fdToday.IsPlantingDay = true;
                }
            }

            _responseRepository.Save();
        }

        /// <summary>
        /// take whatever the current value for IsPlantingDay bool is, and makes it the opposite. Allows the user to click a button to trigger a click event to toggle
        /// </summary>
        /// <param name="forecastDay"></param>
        public void TogglePlantingDay(int period)
        {
            Forecastday forecastDay = new Forecastday();

            forecastDay = SelectByPeriod(period);

            if (forecastDay.IsPlantingDay)
            {
                forecastDay.IsPlantingDay = false;
            }
            else
            {
                forecastDay.IsPlantingDay = true;
            }
        }
    }
}
