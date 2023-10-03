using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Isometric
{
    // 플레이어 피격 범위
    public class HitBox_Player : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Bullet"))
            {
                Managers.Scene.LoadScene(Enums.Scene.SceneFailed);
            }
        }
    }

}