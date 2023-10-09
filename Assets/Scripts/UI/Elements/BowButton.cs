using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Isometric
{
    
    // 동적으로 붙지 않고 유니티 에디터상에서 연결해준 버튼
    public class BowButton : MonoBehaviour
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
            PlayerController.Instance.InterAction();
        }
        public void OnPinterUp()
        {
            PlayerController.Instance.InterActionCancel();

        }
    }
}
