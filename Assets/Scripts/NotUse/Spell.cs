using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace Isometric
{
    [Serializable]
    public class Spell : IUseable, IMoveable
    {
        [SerializeField] private string spellName;
        [SerializeField] Sprite icon;
        

        public string SpellName
        {
            get
            {
                return spellName;
            }
        }
            
        public Sprite MyIcon
        {
            get
            {
                return icon;
            }
        }
        public void Use()
        {
            UseableFunctions.Instance.Function(SpellName);
        }

        
        
    }

}