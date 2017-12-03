using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DAL
{
    /// <summary>
    /// the interface for the response repository
    /// </summary>
    public interface IResponseRepository
    {
        Response SelectAll();
        Forecastday SelectByPeriod(int period);
        void Save();
    }
}
