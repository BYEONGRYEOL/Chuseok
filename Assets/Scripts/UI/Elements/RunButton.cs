using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

namespace Isometric
{
    // 달리기 버튼
    public class RunButton : MonoBehaviour
    {

        void Start()
        {
            if (Managers.Data.gameData.IsMobile == false)
            {
                gameObject.SetActive(false);
            }
        }
        

        public void OnPointerDown(BaseEventData _Data)
        {
            PlayerController.Instance.Running();
        }
        public void OnPinterUp()
        {
            PlayerController.Instance.RunningCancel();

        }
    }
}
