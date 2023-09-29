using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Isometric.Data;

namespace Isometric.UI
{

    public class UI_Inventory_Slot : UI_Base
    {
        private Canvas canvas;
        private GraphicRaycaster gr;
        private int slotNum;
        public int SlotNum { get => slotNum; set => slotNum = value; }
        public Image icon;
        private bool hasItem = false;
        public bool HasItem { get { return hasItem; } set { hasItem = value; } }
        private void Start()
        {
            Init();
        }
        public override void Init()
        {

            gr = GameObject.FindGameObjectWithTag("Canvas_Inventory").GetComponent<Canvas>().GetComponent<GraphicRaycaster>();
            RectTransform rectTransform = GetComponent<RectTransform>();
            
            
            //BindEvent
            BindEvent(this.gameObject, PointerEventData => IconDrag(), type:Enums.UIEvent.Drag);
            BindEvent(this.gameObject, PointerEventDAta => IconBeginDrag(), type: Enums.UIEvent.BeginDrag);
            BindEvent(this.gameObject, PointerEventData => IconEndDrag(), type: Enums.UIEvent.EndDrag);
            
        }

        public void IconBeginDrag()
        {
            Debug.Log("드래그 시작한 Slot 넘버" + slotNum);

            CursorController.Instance.BeginDragSlot = slotNum;
            CursorController.Instance.OnItemBeginDrag(icon);
        }
        public void IconEndDrag()
        {
            var ped = new PointerEventData(null);
            ped.position = Input.mousePosition;
            Debug.Log("MousePosition 위치 " + ped.position.ToString());
            List<RaycastResult> results = new List<RaycastResult>();
            gr.Raycast(ped, results);
            for(int i = 0; i < results.Count; i++)
            {
                Debug.Log("Raycast 결과 위치" + "result List의 " + i  + "번째 요소" + results[i].gameObject.transform.position.ToString());
                Debug.Log("raycast 결과 이름" + i + "번째 요소 " + results[i].gameObject.name);
            }
            
            //아무 UI에도 닿지않은 마우스 포지션
            if(results.Count <= 0)
            {
                return;
            }
            //드롭 시 몇번 째 슬롯 칸에 멈췄는지 정보 받기

            Debug.Log("Raycast 충돌 오브젝트 이름 ::   " +results[0].gameObject.name);
            UI_Inventory_Slot newSlot = results[0].gameObject.transform.parent.GetComponent<UI_Inventory_Slot>();
            Debug.Log("Getcomponentinparant" + newSlot.gameObject.name);
            Debug.Log("Getcomponentinparant" + newSlot.gameObject.transform.position.ToString());

            Debug.Log("드래그 앤 드롭의 드롭 슬롯 ::: " + newSlot.SlotNum);
            Debug.Log("드래그 앤 드롭의 시작 슬롯 ::: " + CursorController.Instance.BeginDragSlot);

            if (newSlot != null)
            {
                Debug.Log("아이템 드래그앤 드롭 시 이 로그가 뜨지 않으면 null CHeck 당함");

                Managers.Inven.SwtichItemSlot(newSlot.SlotNum, CursorController.Instance.BeginDragSlot, newSlot.HasItem);

            }
            UI_Inventory.Instance.RefreshSlot();
        }

        

        public void IconDrag()
        {
            icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, 0.5f);
            CursorController.Instance.OnItemDrag(icon);
        }
        
        public void ResetItemIcon()
        {
            //투명화 시키기 보다는 기본 icon sprite가 있다면 기본값으로 하는게 좋을거같다.
            icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, 0f);
            
        }
        public void SetItemIcon(int templateid, int count)
        {
            Data.ItemInfo itemInfo = null;
            Managers.Data.ItemInfoDict.TryGetValue(templateid, out itemInfo);

            Sprite[] itemicons = Resources.LoadAll<Sprite>("Sprites/Fantastic UI Starter Pack");
            Debug.Log(itemicons +"itemicon multiple sprite 정상 로드" +itemicons.Length);
            Debug.Log(itemInfo.resourcePath);
            string resourcePath = itemInfo.resourcePath;
            resourcePath = resourcePath.Substring(resourcePath.LastIndexOf('/') + 1);
            
            Sprite itemicon = Array.Find(itemicons, x => x.name == resourcePath);
            
            //원래는 single sprite 라면 이런식으로 로드해야하긴해요~
            //Sprite itemicon = Managers.Resource.Load<Sprite>(itemInfo.resourcePath);
            
            Debug.Log("UI_Inventory_Slot" + itemicon);

            icon.sprite = itemicon;
        }
    }

}