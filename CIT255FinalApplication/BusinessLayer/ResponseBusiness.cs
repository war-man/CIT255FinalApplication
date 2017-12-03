using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Models;

namespace BusinessLayer
{
    public class ResponseBusiness : IDisposable, IResponseRepository
    {
        IResponseRepository _responseRepository;

        //
        // instantiate the ResponseBusiness class object with the repository
        // as the input parameter
        // set the internal copy of _responseRepository equal to the input
        // parameter so that the business class is using repository
        // that you pass it from the business class's calling method 
        // and uses it throughout its methods like Insert, Delete, etc

        public ResponseBusiness(IResponseRepository repository)
        {
            _responseRepository = repository;
        }

        public void Dispose()
        {
            _responseRepository = null;
        }

        public Response SelectAll()
        {
            return _responseRepository.SelectAll();
        }

        public Forecastday SelectByPeriod(int period)
        {
            return _responseRepository.SelectByPeriod(period);
        }
    }
}
