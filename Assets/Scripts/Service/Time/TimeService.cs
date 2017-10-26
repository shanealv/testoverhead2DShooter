using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RedWoods.Service
{
    public class TimeService : ITimeService
    {
        public float GetTime() => Time.time;
    }
}