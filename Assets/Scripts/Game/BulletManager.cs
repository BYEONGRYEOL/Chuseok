using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Isometric
{
    public class BulletManager : MonoBehaviour
    {
        int minx = -40;
        int maxx = 40;
        int miny = -10;
        int maxy = 40;

        Coroutine instantiateCo;
        List<Bullet> bullets = new List<Bullet>();
        int maxCount = 50;
        int nowCount = 0;
        public void Start()
        {
            InvokeRepeating("checkBulletValid",0, 5f);
        }
        public void Update()
        {
            if(instantiateCo == null && nowCount < maxCount)
            {
                instantiateCo = StartCoroutine(InstantiateBullet(Managers.Random.getRandomFloat(3)));
            }
        }
        IEnumerator InstantiateBullet(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            GameObject go = Managers.Resource.Instantiate("InGame/Bullet");
            Bullet bullet = go.GetComponent<Bullet>();
            bullets.Add(bullet);
            nowCount++;
            instantiateCo = null;
        }

        public void InitBullet()
        {
            int direction = Managers.Random.getRandomInt(0, 4);
            

        }
        public Vector3 SetPosition(int minx, int maxx, int miny, int maxy, int direction)
        {
            return new Vector3(maxy, minx, miny);
        }
        public void checkBulletVaild()
        {
            for(int i = bullets.Count-1; i >=0; i--)
            {
                if (bullets[i].IsInMap())
                {
                    Managers.Resource.Destroy(bullets[i].gameObject);
                    bullets.RemoveAt(i);
                    nowCount--;
                }
            }
        }

        

    }
}
