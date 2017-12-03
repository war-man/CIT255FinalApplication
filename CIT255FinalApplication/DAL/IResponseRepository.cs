using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL
{
    public interface IResponseRepository
    {
        Response SelectAll();
        Forecastday SelectByPeriod(int period);
        void Update(Response obj);
        void Save();
    }
}
