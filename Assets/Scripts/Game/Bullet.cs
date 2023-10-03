using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
namespace Isometric
{
    public class Bullet : MonoBehaviour
    {
        // �Ѿ��� �� ������ �������� �˻�
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
            // ���� �̵���Ű��
            this.gameObject.transform.position += direction * speed * Time.deltaTime;
        }
        public bool IsInMap()
        {
            // pool manager������ ���� ���� �ִ� ������ �� �ۿ� �������� �˻��ϱ�
            float x = this.gameObject.transform.position.x;
            float y = this.gameObject.transform.position.y;
            // map boundary �ȿ� �ִ��� �˻�
            if (x > xlowerBound && x < xUpperBound && y > ylowerBound && y < yUpperBound)
                return true;
            return false;
        }

        public void InitBullet(float speed, Vector3 direction, Vector3 position, Sprite sprite)
        {
            // ���� ��������Ʈ�� ����, ��ġ �ӵ� �ʱ�ȭ
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprite;
            this.direction = direction;
            this.transform.position = position;
            this.speed = speed;
        }
    }

}