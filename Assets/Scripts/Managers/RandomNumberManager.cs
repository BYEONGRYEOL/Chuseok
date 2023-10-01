using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Isometric.Utility;
using Isometric.Data;
using System;
using System.Linq;
using Random = System.Random;

namespace Isometric
{

    public class RandomNumberManager
    {
        public Random random;
        public void Init()
        {
            random = new Random();
        }
        public float getRandomFloat(int max)
        {
            return (float)random.NextDouble() * max;
        }

        public int getRandomInt(int min = 0, int max = 100)
        {
            return random.Next(min, max);
        }
    }

}