using Isometric.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Isometric.Data
{
    public class InventoryManager
    {

        //dbid 가 키야
        public Dictionary<int, Item> Items = new Dictionary<int, Item>();


        public int NewItemDbID()
        {
            return Managers.Data.ItemDBDict.Keys.Max() + 1;
        }

        public void Clear()
        {
            Items.Clear();
        }

        public void Init()
        {
            LoadItem();
            Debug.Log(Items.Count);
            Debug.Log("InventoryManager :: " + Items[1]);
        }

        public int? FindEmptySlot()
        {
            List<int> slots = new List<int>();
            foreach (Item items in Items.Values)
            {
                slots.Add(items.Slot);
            }

            // 1 ~ 10 착용 11~20 아이템 사용 슬롯
            for (int i = 21; i <= 40; i++)
            {
                if (!slots.Contains(i))
                {
                    return i;
                }
            }
            return null;
        }
        public void LoadItem()
        {
            Debug.Log(Managers.Data.ItemDBDict.Count + "DataManager의 ItemDB딕셔너리 개수");
            foreach (ItemData itemDB in Managers.Data.ItemDBDict.Values)
            {
                if (!Items.ContainsKey(itemDB.itemDbID))
                {
                    Debug.Log("Inventory Manager에 Item이 포함되어있지 않은 경우에만 추가");
                    Add(Item.GetItemFromDB(itemDB));
                }
            }

            Debug.Log(Items.Count + "개의 아이템 인벤토리에 로드");
        }
        public void SaveItem()
        {
            foreach (Item item in Items.Values)
            {
                Debug.Log("DB에 현재 Item상태를 저장시킴");
                // Stackable 아이템의 경우 같이 소지하다가 다 사용한 경우 DB에는 Count = 0으로 표시되는 게 좋을 지 고민 중
                // 이렇게 코딩하면 Count가 0이되어 사라진 아이템을 지우진 않음
                Managers.Data.ItemDBDict[item.ItemDbId] = Item.ItemToDB(item);
            }
        }
        public void Add(Item item)
        {
            Items.Add(item.ItemDbId, item);
        }

        public Item GetItembySlotNum(int slot)
        {
            Item item = null;
            item = Items.Values.FirstOrDefault(x => x.Slot == slot);
            if(item == null)
            {
                Debug.Log("item is " + item);
                return null;
            }
            return item;
        }

        public void SwtichItemSlot(int toSlot, int fromSlot, bool switching)
        {
            if (switching)
            {
                Items[Find(x => x.Slot == toSlot).ItemDbId].Slot = fromSlot;
                Items[Find(x => x.Slot == fromSlot).ItemDbId].Slot = toSlot;
            }
            else
            {
                Items[Find(x => x.Slot == fromSlot).ItemDbId].Slot = toSlot;
            }
            UI_Inventory.Instance.RefreshSlot();
        }

        #region Item 사용관련
        public void  UseItembySlotNum(int slotnum)
        {
            Item item = GetItembySlotNum(slotnum);
            if(item == null)
            {
                return;
            }
            
            // 사용해서 없애거나 
            
            Managers.Item.Use(item);
        }
        public void ReduceItemCount(Item item, int useAmount)
        {
            
            if(Items[item.ItemDbId].Count == useAmount)
            {
                Items.Remove(item.ItemDbId);
            }
            Items[item.ItemDbId].Count -= useAmount;
            
        }

        public void Equip(Item item)
        {
            //item.Slot
        }
        #endregion
        public Item Get(int itemDbid)
        {
            Item item = null;
            Items.TryGetValue(itemDbid, out item);
            return item;
        }
        public Item Find(Func<Item, bool> condition)
        {
            foreach(Item item in Items.Values)
            {
                if (condition.Invoke(item))
                {
                    return item;
                }
            }

            //아이템을 못찾은경우
            Debug.Log("InvetoryManager :: FindFunction returns null");
            return null;
        }
    }
}
