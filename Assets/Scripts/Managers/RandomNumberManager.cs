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
        public float getRandomFloat()
        {
            return (float)random.NextDouble();
        }
    }

}