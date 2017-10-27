using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedWoods.Service
{
    public interface ITimeService
    {
        /// <summary>
        /// Gets the current time in seconds since the game start.
        /// </summary>
        /// <returns></returns>
        float GetTime();
    }
}