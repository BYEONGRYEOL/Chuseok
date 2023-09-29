using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Isometric.Utility;
using UnityEngine.UI;
namespace Isometric
{

    public class CursorController : SingletonMonoBehaviour<CursorController>
    {
        int mask = (1 << (int)Enums.Layer.InterActive);
        // Start is called before the first frame update
        Image grabbingImage;
        Texture2D idleIcon;
        Texture2D grabIcon;
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
            idleIcon = Managers.Resource.Load<Texture2D>("Texture/Cursor/Cursor_Idle");
            grabIcon = Managers.Resource.Load<Texture2D>("Texture/Cursor/Cursor_Grab");
            main = Camera.main;
            grabbingImage = null;
            IsGrabbing = false;
        }

        void Update()
        {
            UpdateCursor();
        }

        public void GraphicRaycaseTest()
        {
            if (Input.GetMouseButtonDown(0))
            {

            }
        }
        public void OnItemBeginDrag(Image image)
        {
            grabbingImage = Instantiate(image, image.transform.parent);
            Cursor.SetCursor(grabIcon, new Vector2(grabIcon.width / 4, grabIcon.height / 4), CursorMode.Auto);
            //icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, 0.5f;
            //GraphicRaycast 에서 뒤에 있는 UI 컴포넌트를 인식하지 못하는 오류가 나기 때문에 잠시 grabbingImage를 GR이 인식하지 못하도록 raycast target false
            grabbingImage.GetComponent<Image>().raycastTarget = false;
            iconOriginPosition = grabbingImage.transform.position;


        }
        public void OnItemDrag(Image image)
        {
            //잡고있던 마우스를 떼면 리턴
            
            grabbingImage.transform.position = Input.mousePosition;
            if (Input.GetMouseButtonUp(0))
                OnItemEndDrag(image);
        }
        public void OnItemEndDrag(Image image)
        {
            //Drgging 종료시 원위치, 드래그하는동안 껐던 racasttarget 다시 true

            grabbingImage.GetComponent<Image>().raycastTarget = true;
            grabbingImage.transform.position = iconOriginPosition;
            grabbingImage = null;
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