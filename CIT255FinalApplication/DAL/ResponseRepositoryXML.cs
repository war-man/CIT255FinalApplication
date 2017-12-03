using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using Model;
using Data;

namespace DAL
{
    /// <summary>
    /// method to write the forecast repository to the data file from the API
    /// </summary>
    public class ResponseRepositoryXML : IDisposable, IResponseRepository
    {
        #region FIELDS

        private Response _response;

        #endregion

        #region CONSTRUCTORS
        /// <summary>
        /// the constructor for the repository that pulls data from the API
        /// </summary>
        public ResponseRepositoryXML()
        {
            _response = ReadResponseData();
        }

        #endregion

        #region METHODS

        /// <summary>
        /// implements the Dispose() method to comply with the IDisposable interface
        /// </summary>
        public void Dispose()
        {
            _response = null;
        }

        /// <summary>
        /// returns the whole forecast as a response object
        /// </summary>
        /// <returns></returns>
        public Response ReadResponseData()
        {
            Response response;

            StreamReader sr = new StreamReader(DataSettings.dataFilePathLocal);

            XmlSerializer deserializer = new XmlSerializer(typeof(Response), new XmlRootAttribute("response"));

            using (sr)
            {
                object obj = deserializer.Deserialize(sr);

                response = obj as Response;
            }

            return response;
        }

        /// <summary>
        /// Writes to the repository file to serialize the data for data storage
        /// </summary>
        public void Save()
        {
            StreamWriter sw = new StreamWriter(DataSettings.dataFilePathLocal, false);

            XmlSerializer serializer = new XmlSerializer(typeof(Response), new XmlRootAttribute("Response"));

            using (sw)
            {
                serializer.Serialize(sw, _response);
            }
        }

        /// <summary>
        /// returns the whole forecast as a response object
        /// </summary>
        /// <returns></returns>
        public Response SelectAll()
        {
            return _response;
        }

        /// <summary>
        /// given the period number for the day, returns a forecastday
        /// </summary>
        /// <param name="period"></param>
        /// <returns>forecastday</returns>
        public Forecastday SelectByPeriod(int period)
        {
            Forecastday forecastDay = null;

            //
            // use LINQ to find the specific forecast day you wanted
            //

            forecastDay = _response.Forecast.Simpleforecast.Forecastdays.Forecastday.FirstOrDefault(fd => fd.Period == period);

            return forecastDay;
        }

        #endregion

    }
}
