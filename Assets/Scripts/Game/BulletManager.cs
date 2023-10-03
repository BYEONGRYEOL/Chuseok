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
    public class BulletManager : SingletonMonoBehaviour<BulletManager>
    {
        float[] bulletTypeProbability = { 0.1f, 0.3f, 0.6f, 1.0f }; // 송편 타입 확률표
        // 송편 생성 위치
        int minx = -24;
        int maxx = 45;
        int miny = -5;
        int maxy = 40;

        // 송편에 줄 스프라이트 미리 들고잇기
        Sprite[] sprites;
        Coroutine instantiateCo; // 송편생성코루틴
        List<Bullet> bullets = new List<Bullet>(); // 현재 씬에 나와있는 송편들
        int maxCount = 500; // 송편 최대 갯수
        int nowCount = 0;

        public int frequency = 3; // 송편 출현 빈도
        public int difficulty = 1; // 난이도에 따라 더 빠르게 생성
        public void Start()
        {
            InvokeRepeating("checkBulletVaild", 5f, 3f); // 맵에 잇는 송편이 boundary를 넘어갔는지 3초마다 검사하기
            sprites = new Sprite[4];
            for(int i = 0; i < 4; i++)
            {
                sprites[i] = Managers.Resource.Load<Sprite>("Sprites/Bullets/bulletType_" + (i + 1)); // Sprite 불러오기
            }
        }

        
        public void Update()
        {
            // 현재 송편생성 코루틴이 실행중이지 않으며 최대 송편 개수를 넘지 않은 경우
            if(instantiateCo == null && nowCount < maxCount)
            {
                // 생성빈도 + 난이도 만큼으로 0.0부터 1.0 난수를 나누어 그만큼만 대기하고 송편을 생성시킨다.
                instantiateCo = StartCoroutine(InstantiateBullet(Managers.Random.getRandomFloat(1) / (frequency + difficulty) ));
            }
        }
        IEnumerator InstantiateBullet(float seconds)
        {
            // 난수들
            int bulletType = getRandomBulletType(); // 송편의 색 별로 속도와 생성확률이 다르다. 송편의 타입 결정하기
            int dFlag = Managers.Random.getRandomInt(0, 4); // 랜덤 방향ㄴ
            float speed = Managers.Random.getRandomFloat(3) + (4 - bulletType) * 1.5f; // 송편의 속도 로직
            
            Vector3 direction = GetBulletRandomDirection(dFlag);
            Vector3 position = GetBulletRandomPosition(dFlag);

            yield return new WaitForSeconds(seconds); //대기 후
            instantiateCo = null; // 바로 재생성되는 것을 막기 위해

            GameObject go = Managers.Resource.Instantiate("InGame/Bullet"); // pool manager를 통해 생성 ( pop )
            Bullet bullet = go.GetComponent<Bullet>(); // 생성된 게임오브젝트는 prefab에 Bullet 스크립트가 붙어잇다.

            // bullet manager 딴에 생성된 송편 추가 및 현재 송편 개수 +1
            bullets.Add(bullet);
            nowCount++;

            // 송편 초기화 및 움직이도록 IsAble true
            bullet.InitBullet(speed, direction, position, sprites[bulletType]);
            bullet.IsAble = true;
            //Debug.Log("bullet 생성");
        }

        public int getRandomBulletType()
        {
            int bulletType = 0;
            float probability = Managers.Random.getRandomFloat(1);
            for(int type =0; type <4; type++)
            {
                // 확률표보다 작은 확률일때 통과
                //현재 0.1, 0.3, 0.6, 1.0 4개의 요소를 가진 배열인데, 따라서
                // 배열의 index 0에서 멈출 확률 0.1
                // 배열의 index 1에서 멈출 확률 0.2
                // 배열의 index 2에서 멈출 확률 0.3
                // 배열의 index 3에서 멈출 확률 0.4
                if (bulletTypeProbability[type] > probability)
                {
                    bulletType = type;
                    break;
                }
            }
            return bulletType;
        }
        
            
        public Vector3 GetBulletRandomPosition(int direction)
        {
            int x=0, y=0;
            switch (direction)
            {
                case 0: // 송편의 이동방향이 상 이라면 random x, with miny ** 아래에서 생성되어 위를 향해 가야한다.
                    x = Managers.Random.getRandomInt(minx, maxx);
                    y = miny;
                    break;
                case 1: // 하, random x, with maxy
                    x = Managers.Random.getRandomInt(minx, maxx);
                    y = maxy;
                    break;
                case 2: // 좌  방향으로 이동해야 하므로 x = maxx
                    x = maxx;
                    y = Managers.Random.getRandomInt(miny, maxy);
                    break;
                case 3:
                    x = minx;
                    y = Managers.Random.getRandomInt(miny, maxy);
                    break;

            }
            return new Vector3((float)x,(float)y);
        }

        public Vector3 GetBulletRandomDirection(int direction)
        {
            int x = Managers.Random.getRandomInt(-10, 10);
            int y = Managers.Random.getRandomInt(5, 20);
            int temp = 0;

            // direction이 0인 경우 상 방향, 생략
            switch (direction)
            {
                case 1:
                    y *= -1;
                    break;
                case 2: // 좌
                    temp = x;
                    x = -1 * y;
                    y = temp;
                    break;
                case 3:
                    temp = x;
                    x =  y;
                    y = temp;
                    break;
            }
            // 정규화된 방향벡터
            float r = (float)Math.Sqrt(x * x + y * y);
            return new Vector3(x / r, y / r);
        }
        public void checkBulletVaild()
        {
            // 역순으로 진행해야 index 오류 안생김
            for(int i = bullets.Count-1; i >=0; i--)
            {
                // 현재 송편 매니저가 들고있는 송편리스트의 각 요소(송편)들이 맵 안에 잇는지 체크
                if (bullets[i].IsInMap() == false) // 맵 밖으로 나간 경우 pool stack에 push
                {
                    Managers.Resource.Destroy(bullets[i].gameObject);
                    bullets.RemoveAt(i);
                    nowCount--;
                }
            }
        }
    }
}
