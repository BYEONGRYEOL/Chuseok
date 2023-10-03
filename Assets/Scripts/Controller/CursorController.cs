using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Isometric.Utility;
using UnityEngine.UI;
namespace Isometric
{
    public class CursorController : SingletonMonoBehaviour<CursorController>
    {
        // 학생 때 만들던 토이 프로젝트에 쓰인 코드 그대로, 마우스 커서 컨트롤러로써
        // UI상 아이템 드래그, 클릭 등에 따라 기능과 마우스 커서 Icon Sprite 변경하는 기능
        // 코육대에선 사용하지않는다.
        int mask = (1 << (int)Enums.Layer.InterActive);
        // Start is called before the first frame update
        Image grabbingImage;
        Texture2D idleIcon;
        Camera main;

        private Vector3 iconOriginPosition;
        public int BeginDragSlot;

        public bool IsGrabbing = false;
        enum CursorType
        {
            Idle,
            Attack,
            Grab
        }
        CursorType cursorType = CursorType.Idle;
        private void Start()
        {
            Init();
        }
        void Init()
        {
            grabbingImage = null;
            IsGrabbing = false;
        }

        private void Update()
        {
            UpdateCursor();
        }
        
        void UpdateCursor()
        {

            if (Input.GetMouseButton(0))
                return;
            Ray ray = main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f, mask))
            {

            }
            else
            {
                Cursor.SetCursor(idleIcon, new Vector2(idleIcon.width / 4, idleIcon.height / 4), CursorMode.Auto);
            }
        }
    }
}