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
            Debug.Log("�巡�� ������ Slot �ѹ�" + slotNum);

            CursorController.Instance.BeginDragSlot = slotNum;
            CursorController.Instance.OnItemBeginDrag(icon);
        }
        public void IconEndDrag()
        {
            var ped = new PointerEventData(null);
            ped.position = Input.mousePosition;
            Debug.Log("MousePosition ��ġ " + ped.position.ToString());
            List<RaycastResult> results = new List<RaycastResult>();
            gr.Raycast(ped, results);
            for(int i = 0; i < results.Count; i++)
            {
                Debug.Log("Raycast ��� ��ġ" + "result List�� " + i  + "��° ���" + results[i].gameObject.transform.position.ToString());
                Debug.Log("raycast ��� �̸�" + i + "��° ��� " + results[i].gameObject.name);
            }
            
            //�ƹ� UI���� �������� ���콺 ������
            if(results.Count <= 0)
            {
                return;
            }
            //��� �� ��� ° ���� ĭ�� ������� ���� �ޱ�

            Debug.Log("Raycast �浹 ������Ʈ �̸� ::   " +results[0].gameObject.name);
            UI_Inventory_Slot newSlot = results[0].gameObject.transform.parent.GetComponent<UI_Inventory_Slot>();
            Debug.Log("Getcomponentinparant" + newSlot.gameObject.name);
            Debug.Log("Getcomponentinparant" + newSlot.gameObject.transform.position.ToString());

            Debug.Log("�巡�� �� ����� ��� ���� ::: " + newSlot.SlotNum);
            Debug.Log("�巡�� �� ����� ���� ���� ::: " + CursorController.Instance.BeginDragSlot);

            if (newSlot != null)
            {
                Debug.Log("������ �巡�׾� ��� �� �� �αװ� ���� ������ null CHeck ����");

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
            //����ȭ ��Ű�� ���ٴ� �⺻ icon sprite�� �ִٸ� �⺻������ �ϴ°� �����Ű���.
            icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, 0f);
            
        }
        public void SetItemIcon(int templateid, int count)
        {
            Data.ItemInfo itemInfo = null;
            Managers.Data.ItemInfoDict.TryGetValue(templateid, out itemInfo);

            Sprite[] itemicons = Resources.LoadAll<Sprite>("Sprites/Fantastic UI Starter Pack");
            Debug.Log(itemicons +"itemicon multiple sprite ���� �ε�" +itemicons.Length);
            Debug.Log(itemInfo.resourcePath);
            string resourcePath = itemInfo.resourcePath;
            resourcePath = resourcePath.Substring(resourcePath.LastIndexOf('/') + 1);
            
            Sprite itemicon = Array.Find(itemicons, x => x.name == resourcePath);
            
            //������ single sprite ��� �̷������� �ε��ؾ��ϱ��ؿ�~
            //Sprite itemicon = Managers.Resource.Load<Sprite>(itemInfo.resourcePath);
            
            Debug.Log("UI_Inventory_Slot" + itemicon);

            icon.sprite = itemicon;
        }
    }

}