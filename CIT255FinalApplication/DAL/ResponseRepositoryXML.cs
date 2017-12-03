using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using Models;
using Data;

namespace DAL
{
    /// <summary>
    /// method to write the forecast repository to the data file from the API
    /// </summary>
    public class ResponseRepositoryXML : IDisposable, IResponseRepository
    {
        private Response _response;

        public ResponseRepositoryXML()
        {
            _response = ReadResponseData();
        }

        public void Dispose()
        {
            _response = null;
        }

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

        public Response SelectAll()
        {
            return _response;
        }

        public Forecastday SelectByPeriod(int period)
        {
            Forecastday forecastDay = null;

            //
            // use LINQ to find the specific forecast day you wanted
            //

            forecastDay = _response.Forecast.Simpleforecast.Forecastdays.Forecastday.FirstOrDefault(fd => fd.Period == period);

            return forecastDay;
        }
    }
}
