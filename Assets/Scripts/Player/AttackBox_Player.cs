using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Isometric
{
    public class AttackBox_Player : MonoBehaviour
    {
        public float attackDamage = 3f;
        // Start is called before the first frame update
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Grandma"))
            {
                Grandma grandma = collision.GetComponentInParent<Grandma>();
                
            }

        }
    } 
}
