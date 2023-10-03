using Isometric.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Isometric
{
    public class Grandma : MonoBehaviour
    {
        public bool Enabled
        {
            get => enabled;
            set => enabled = value;
        }
        int grandMaDirection;
        // 초기화 함수
        public void InitGrandma(Vector3 position, int direction, Sprite sprite)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprite;
            this.grandMaDirection = direction;
            this.transform.position = position;
        }

        public void BowComplete()
        {
            // 플레이어가 맞절에 성공한 경우 실행되는 함수
            // 돈을 주자
            Managers.Sound.Play("Money");
            int money = 10000 * Managers.Random.getRandomInt(1,10);
            GameManagerEX.Instance.Money += money;
            GrandmaManager.Instance.DeleteGrandma(this);
        }

        public void TryBow(int direction)
        {
            // 맞절만 인정
            // direction, grandMaDirection
            // 0            4
            // 1            5
            // 2            6
            // 3            7
            // 4            0
            // 5            1
            // 6            2
            // 7            3
            // 두 값의 차이가 4라면 8방향 맞절 인정
            if(Math.Abs(direction - grandMaDirection) == 4)
            {
                // 맞절인 경우
                BowComplete();
            }
        }
    }
    
}
