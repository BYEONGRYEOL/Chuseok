using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
namespace Isometric
{
    public class Bullet : MonoBehaviour
    {
        // 총알이 맵 밖으로 나갔는지 검사
        public float xlowerBound = -40;
        public float xUpperBound = 40;
        public float ylowerBound = -10;
        public float yUpperBound = 40;

        private float speed;
        private Vector3 direction;

        public bool isAble = true;
        public bool IsAble
        {
            get => isAble;
            set => isAble = value;
        }

        public void Disable()
        {
            IsAble = false;
        }
        void Update()
        {
            if (IsAble)
            {
                Move();
            }
        }
        
        private void Move()
        {
            // 송편 이동시키기
            this.gameObject.transform.position += direction * speed * Time.deltaTime;
        }
        public bool IsInMap()
        {
            // pool manager딴에서 현재 씬에 있는 송편이 맵 밖에 나갔는지 검사하기
            float x = this.gameObject.transform.position.x;
            float y = this.gameObject.transform.position.y;
            // map boundary 안에 있는지 검사
            if (x > xlowerBound && x < xUpperBound && y > ylowerBound && y < yUpperBound)
                return true;
            return false;
        }

        public void InitBullet(float speed, Vector3 direction, Vector3 position, Sprite sprite)
        {
            // 송편 스프라이트와 방향, 위치 속도 초기화
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprite;
            this.direction = direction;
            this.transform.position = position;
            this.speed = speed;
        }
    }

}