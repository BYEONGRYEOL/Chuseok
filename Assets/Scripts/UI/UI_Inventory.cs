using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Isometric.Data;
using System.Linq;

namespace Isometric.UI
{
    public class UI_Inventory : UI_Base
    {
        public List<UI_Inventory_Slot> Slots { get; set; } = new List<UI_Inventory_Slot>();

        private static UI_Inventory instance;
        public static UI_Inventory Instance { get { return instance; } }

        
        private void Awake()
        {
            Init();
            
        }
        public override void Init()
        {
            
            if(instance != null)
            {
                Destroy(this);
            }
            else
            {
                instance = this;
            }

            Managers.Inven.LoadItem();
            Slots.Clear();
            SlotInit();
            RefreshSlot();
        }

        public void SlotInit()
        {
            GameObject grid = transform.Find("Item_Grid").gameObject;
            foreach (Transform child in grid.transform)
                Destroy(child.gameObject);

            //�κ��丮 ������ �� ������ ���Ƿ� 20���� ����
            for (int i = 0; i < 20; i++)
            {
                GameObject go = Managers.Resource.Instantiate("UI/Items/Inventory_Slot", parent: grid.transform);
                go.gameObject.name += i;
                UI_Inventory_Slot slot = go.GetComponent<UI_Inventory_Slot>();
                slot.SlotNum = i;
                //�ʱ�ȭ 
                slot.HasItem = false;

                Slots.Add(slot);
            }
            foreach (Item item in Managers.Inven.Items.Values)
            {
                Slots[item.Slot].HasItem = true;
            }
        }

        public void RefreshSlot()
        {
            List<Item> items = Managers.Inven.Items.Values.ToList();


            items.Sort((left, right) => left.Slot.CompareTo(right.Slot));
            for(int i=0 ; i<Managers.Data.gameData.Inventory_capacity; i++)
            {
                // List.FindIndex �޼���� ��ã�� ��� -1�� ����

                int slot = items.FindIndex(x => x.Slot == i);
                if(slot < 0)
                {
                    Slots[i].ResetItemIcon();
                }
                else
                {
                    Slots[items[slot].Slot].SetItemIcon(items[slot].ItemTemplateId, items[slot].Count);
                    Debug.Log("�ش��ϴ� iconslot�� ã�� ���" + i + " ��° ������ ���Կ� icon ǥ��");
                }
                
            }
        }
    }
}