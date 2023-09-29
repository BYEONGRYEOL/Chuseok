using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Isometric
{
    public class LayerSorter : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        private List<GameObject> obstacleList = new List<GameObject>();
        // Update is called once per frame
        void Start()
        {
            spriteRenderer = transform.GetComponent<SpriteRenderer>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            //Debug.Log("triggerenter");
            if (collision.CompareTag("Obstacle"))
            {
                //Debug.Log("obstacle ÀÎ½Ä");

                GameObject obstacle = collision.gameObject;
                SpriteRenderer obstacleSpriteRenderer = obstacle.GetComponent<SpriteRenderer>();


                if(obstacleList.Count ==0 || obstacleSpriteRenderer.sortingOrder -1 < spriteRenderer.sortingOrder)
                {
                    spriteRenderer.sortingOrder = obstacleSpriteRenderer.sortingOrder - 1;
                }

                obstacleList.Add(obstacle);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Obstacle"))
            {
                GameObject obstacle = collision.gameObject;
                
                obstacleList.Remove(obstacle);

                if(obstacleList.Count == 0)
                {
                    spriteRenderer.sortingOrder = 200;
                }

                else if( obstacleList.Count == 1)
                {
                    spriteRenderer.sortingOrder = obstacleList[0].GetComponent<SpriteRenderer>().sortingOrder - 1;

                }
                else
                {
                    
                    spriteRenderer.sortingOrder = obstacleList[0].GetComponent<SpriteRenderer>().sortingOrder - 1;
                }
            }
        }
    }

}