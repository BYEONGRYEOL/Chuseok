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
            // obstacle �±׸� �ް��ִ� �ݶ��̴��� �������
            if (collision.CompareTag("Obstacle"))
            {

                GameObject obstacle = collision.gameObject;
                SpriteRenderer obstacleSpriteRenderer = obstacle.GetComponent<SpriteRenderer>();
                // ��ֹ��� �����ų� ��ֹ� ���� �� sorting order�� ū ���
                if(obstacleList.Count ==0 || obstacleSpriteRenderer.sortingOrder -1 < spriteRenderer.sortingOrder)
                {
                    // ��ֹ��� �������� �ϹǷ� ���� sortingorder�� �����
                    spriteRenderer.sortingOrder = obstacleSpriteRenderer.sortingOrder - 1;
                }

                obstacleList.Add(obstacle);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Obstacle"))
            {
                // ��ֹ��� ����ִ� event ����
                GameObject obstacle = collision.gameObject;
                obstacleList.Remove(obstacle);

                if(obstacleList.Count == 0)
                {
                    //��ֹ��� ���� ���
                    spriteRenderer.sortingOrder = 200;
                }
                else
                {
                    // ���� ��ֹ��� ����������
                    spriteRenderer.sortingOrder = obstacleList[0].GetComponent<SpriteRenderer>().sortingOrder - 1;
                }
            }
        }
    }

}