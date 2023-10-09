using Isometric.Utility;
using Isometric;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Isometric
{
    public class GrandmaManager : SingletonMonoBehaviour<GrandmaManager>
    {
        Sprite[] sprites;

        Dictionary<int, String> dirMapper = new Dictionary<int, string>();
        Coroutine instantiateCo;
        List<Grandma> grandmas = new List<Grandma>();
        int maxCount = 3;
        int nowCount = 0;
        public void Start()
        { 
            // Sprite파일의 이름이 U, UR .. 등등으로 되어있어서 0~7까지의 시계방향 8방향 대로 mapper 활용
            dirMapper.Add(0, "U");
            dirMapper.Add(1, "UR");
            dirMapper.Add(2, "R");
            dirMapper.Add(3, "DR");
            dirMapper.Add(4, "D");
            dirMapper.Add(5, "DL");
            dirMapper.Add(6, "L");
            dirMapper.Add(7, "UL");
            sprites = new Sprite[8];
            for(int i = 0; i <sprites.Length; i++)
            {
                sprites[i] = Managers.Resource.Load<Sprite>("Sprites/Grandma/Grandma_" + dirMapper[i]); // 스프라이트 블루오기
            }
        }
        public void Update()
        {
            if (instantiateCo == null && nowCount < maxCount)
            {
                // 할머니 랜덤 생성 2초~7초마다 한번
                instantiateCo = StartCoroutine(InstantiateGrandma(Managers.Random.getRandomFloat(5) + 2));
            }
        }
        IEnumerator InstantiateGrandma(float seconds)
        {
            // 난수들
            int dFlag = Managers.Random.getRandomInt(0, 8); // 8방향
            Vector3 position = GetGrandmaRandomPosition();
            yield return new WaitForSeconds(seconds); //대기 후
            instantiateCo = null; // 바로 재생성되는 것을 막기 위해
            GameObject go = Managers.Resource.Instantiate("InGame/Grandma"); // pool manager를 통해 생성 ( pop )
            Grandma grandma = go.GetComponent<Grandma>();
            grandma.InitGrandma(position, dFlag, sprites[dFlag]);
            // manager 딴 기록
            nowCount++;
            Debug.Log("Grandma 생성");
        }

        public Vector3 GetGrandmaRandomPosition()
        {
            // 할머니 위치할수 있는 지역
            // - 16.5 10
            // -3 17
            // 14 8.5
            // 0 1.5
            while (true)
            {
                // x -16 ~ 14
                float x = Managers.Random.getRandomFloat(28) - 15;
                float y = Managers.Random.getRandomFloat(15) + 2;
                // -16 10 좌 꼭짓점
                // -3 17 상 꼭짓점 
                // 14 8.5 우 꼭짓점
                // 0 2  하 꼭짓점

                // 좌상 line y = 7 / 13 x + 18.6
                // 상우 line y = - 1 / 2 x + 18.5
                // 우하 line y = - 13 / 28 x + 2
                // 좌하 line y =  - 1 / 2 x + 2

                // 우하 좌하 대입한 값보단 y가 커야하고,
                // 좌상 상우 대입한 값보단 y가 작아야한다.
                // 할머니 스폰지역 검사
                if (OverLine_DL(x, y) && OverLine_DR(x, y) && UnderLine_UL(x, y) && UnderLine_UR(x, y))
                {
                    // 생성가능 위치
                    return new Vector3(x, y, 0);
                }
            }
        }

        public void DeleteGrandma(Grandma grandma)
        {
            //destroy
            grandma.Enabled = false;
            grandmas.Remove(grandma);
            Managers.Resource.Destroy(grandma.gameObject);
            nowCount--;
            
        }

        public bool OverLine_DL(float x, float y)
        {
            if (x * -0.5 + 4 < y)
                return true;
            return false;
        }
        public bool OverLine_DR(float x, float y)
        {
            if (x * ((float)-13 / 28) + 4 < y)
                return true;
            return false;
        }
        public bool UnderLine_UR(float x, float y)
        {
            if (x * -0.5 + 18.5 > y)
                return true;
            return false;
        }
        public bool UnderLine_UL(float x, float y)
        {
            if (x * 7 / 13 + 18.6 > y)
                return true;
            return false;
        }
    }
}
