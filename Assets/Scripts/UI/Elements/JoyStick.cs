using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.EventSystems;
using UnityEngine;

namespace Isometric
{
    public class JoyStick : MonoBehaviour
    {
        public Transform Stick;         // 조이스틱.

        private Vector3 StickFirstPos;  // 조이스틱의 처음 위치.
        private Vector3 JoyVec;         // 조이스틱의 벡터(방향)
        private float Radius;           // 조이스틱 배경의 반지금ㄴ

        void Start()
        {
            if (Managers.Data.gameData.IsMobile == false)
                return;
            Radius = GetComponent<RectTransform>().sizeDelta.y * 0.5f;
            StickFirstPos = Stick.transform.position;

            // 캔버스 크기에대한 반지름 조절.
            float Can = transform.parent.GetComponent<RectTransform>().localScale.x;
            Radius *= Can;
        }
        private void Update()
        {
            // 플레이어 컨트롤러의 direction을 변경시킴
            PlayerController.Instance.mobileDirection = JoyVec;

        }

        public void Drag(BaseEventData _Data)
        {
            PointerEventData Data = _Data as PointerEventData;
            Vector3 Pos = Data.position;

            // 조이스틱을 이동시킬 방향을 구함.(오른쪽,왼쪽,위,아래)
            JoyVec = (Pos - StickFirstPos).normalized;
            //Debug.Log(JoyVec);
            // 조이스틱의 처음 위치와 현재 내가 터치하고있는 위치의 거리를 구한다
            float Dis = Vector3.Distance(Pos, StickFirstPos);

            // 거리가 반지름보다 작으면 조이스틱을 현재 터치하고 있는곳으로 이동.
            if (Dis < Radius)
                Stick.position = StickFirstPos + JoyVec * Dis;
            // 거리가 반지름보다 커지면 조이스틱을 반지름의 크기만큼만 이동
            else
                Stick.position = StickFirstPos + JoyVec * Radius;


        }
        // 드래그 끝.
        public void DragEnd()
        {
            Stick.position = StickFirstPos; // 스틱을 원래의 위치로
            JoyVec = Vector3.zero;          // 방향을 0으로
        }
    }
}
