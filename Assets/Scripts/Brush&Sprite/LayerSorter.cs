using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Isometric
{
    public class LayerSorter : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        private List<GameObject> obstacleList = new List<GameObject>();
        void Start()
        {
            spriteRenderer = transform.GetComponent<SpriteRenderer>();
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            // obstacle 태그를 달고있는 콜라이더와 만난경우
            if (collision.CompareTag("Obstacle"))
            {

                GameObject obstacle = collision.gameObject;
                SpriteRenderer obstacleSpriteRenderer = obstacle.GetComponent<SpriteRenderer>();
                // 장애물이 없었거나 장애물 보다 내 sorting order가 큰 경우
                if(obstacleList.Count ==0 || obstacleSpriteRenderer.sortingOrder -1 < spriteRenderer.sortingOrder)
                {
                    // 장애물에 가려져야 하므로 나의 sortingorder를 낮춘다
                    spriteRenderer.sortingOrder = obstacleSpriteRenderer.sortingOrder - 1;
                }

                obstacleList.Add(obstacle);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Obstacle"))
            {
                // 장애물과 닿아있던 event 종료
                GameObject obstacle = collision.gameObject;
                obstacleList.Remove(obstacle);

                if(obstacleList.Count == 0)
                {
                    //장애물이 없는 경우
                    spriteRenderer.sortingOrder = 200;
                }
                else
                {
                    // 현재 장애물에 가려지도록
                    spriteRenderer.sortingOrder = obstacleList[0].GetComponent<SpriteRenderer>().sortingOrder - 1;
                }
            }
        }
    }

}