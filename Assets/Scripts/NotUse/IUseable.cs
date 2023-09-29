using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Isometric
{

    public interface IUseable
    {
        public Sprite MyIcon
        {
            get;
        }
        void Use();
    }

}