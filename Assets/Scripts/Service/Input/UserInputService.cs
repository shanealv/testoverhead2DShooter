using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RedWoods.Service
{
    public class UserInputService : IUserInputService
    {
        public float GetAxis(string axisName, int playerid = 0)
        {
            return Input.GetAxis(axisName + (playerid >= 0 ? $"_{playerid}" : ""));
        }
    }
}