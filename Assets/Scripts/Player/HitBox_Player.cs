using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Isometric
{
    // �÷��̾� �ǰ� ����
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