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
    // 굳이 이렇게 난수를 생성하는 애를 매니저로까지 쓰진 않아도 될듯.
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