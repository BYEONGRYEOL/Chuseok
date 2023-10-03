using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Isometric
{
    public class CameraManager : MonoBehaviour
    {
        public GameObject character;
        Transform character_transform;
        void Start()
        {
            
            character_transform = PlayerController.Instance.transform;
        }
        void LateUpdate()
        {
            //플레이어를 적당한 속도로 따라가게끔 Lerp 함수 활용
            Vector2 position_xy = Vector2.Lerp(transform.position, character_transform.position, 1f * Time.deltaTime);
            transform.position = new Vector3(position_xy.x, position_xy.y, -10);
            
        }
    }

}
