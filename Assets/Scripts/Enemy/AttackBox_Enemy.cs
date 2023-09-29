using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Isometric
{
    public class AttackBox_Enemy : MonoBehaviour
    {
        private Stat stat;

        
        
        public void Awake()
        {
            
        }
        //즉발 공격인 경우
        private void OnTriggerEnter2D(Collider2D collision)
        {
            stat = GetComponentInParent<Stat>();

            if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerHitBox"))
            {
                GetComponentInParent<EnemyController>().OnHit();
            }
        }

        
    }

}