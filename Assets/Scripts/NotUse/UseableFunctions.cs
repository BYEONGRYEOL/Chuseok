using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Isometric.Utility;
namespace Isometric
{

    public class UseableFunctions : SingletonMonoBehaviour<UseableFunctions>
    {
        public void Function(string functionName)
        {
            Invoke(functionName, 0);
        }
        public void Teleport()
        {
            Vector2 nowPosition = PlayerController.Instance.transform.position;
            PlayerController.Instance.transform.position = nowPosition + new Vector2(5, 5);
        }

        
    }

}