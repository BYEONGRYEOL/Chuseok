using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
namespace Isometric
{

    public class Bullet : MonoBehaviour
    {
        public float xlowerBound = -40;
        public float xUpperBound = 40;
        public float ylowerBound = -10;
        public float yUpperBound = 40;

        
        public float speed = 1f;
        public Vector3 direction = new Vector3(1, 0, 0);
        public static bool isAble = true;
        public static bool IsAble
        {
            get => isAble;
            set => isAble = value;
        }

        public void OnDisable()
        {
            isAble = false;
        }
        public void OnEnable()
        {
            isAble = true;
            
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
        public void CheckInMap()
        {
            IsAble = isInMap();
        }
        private void Move()
        {

            this.gameObject.transform.position += direction * speed;
        }
        private bool isInMap()
        {
            float x = this.gameObject.transform.position.x;
            float y = this.gameObject.transform.position.y;
            // map boundary 안에 있는지 검사
            if (x > xlowerBound && x < xUpperBound && y > ylowerBound && y < yUpperBound)
                return true;
            return false;
        }
    }

}