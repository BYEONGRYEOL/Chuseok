using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
namespace Isometric
{

    public class LayerManager : MonoBehaviour
    {

        private void Start()
        {
            FindAllChildren(this.gameObject);
        }

        public void FindAllChildren(GameObject g)
        {
            SpriteRenderer[] allChildren = g.GetComponentsInChildren<SpriteRenderer>();
            Debug.Log(allChildren.Length);
            foreach (SpriteRenderer child in allChildren)
            {
                child.gameObject.GetComponent<SpriteRenderer>().sortingOrder = this.GetComponent<TilemapRenderer>().sortingOrder;
            }
        }
    }

}