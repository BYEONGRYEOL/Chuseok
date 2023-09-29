using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Isometric.Data
{

    public struct ItemInfos
    {
        public ItemInfo itemInfo;
        public ItemData itemDB;
    }
    
    public class Item
    {
        public ItemData itemDB { get; } = new ItemData();
        public ItemInfo itemInfo { get; } = new ItemInfo();



        public int ItemDbId { get => itemDB.itemDbID; set => itemDB.itemDbID = value; }
        public int ItemTemplateId { get => itemDB.itemTemplateID; set => itemDB.itemTemplateID = value; }
        public int Count { get => itemDB.count; set => itemDB.count = value; }
        public string Name { get => itemInfo.name; set => itemInfo.name = value; }
        public string Description { get => itemInfo.description; set => itemInfo.description = value; } 

        public int Slot { get => itemDB.slot; set => itemDB.slot = value; }

        bool reduceWhenUse = false;
        public bool ReduceWhenUse { get => reduceWhenUse; set => reduceWhenUse = value; }
        public Enums.ItemType ItemType { get; private set; }
        public bool Stackable { get; protected set; }

        public Item(Enums.ItemType itemType)
        {
            ItemType = itemType;
        }
        
        public static ItemInfos CheckItem(int templateID)
        {
            ItemInfos infos = new ItemInfos();
            Managers.Data.ItemInfoDict.TryGetValue(templateID, out infos.itemInfo);
            if(infos.itemInfo == null)
            {
                Debug.Log("templateID에 해당하는 Item Info를 찾을 수 없음");
            }

            infos.itemDB = Managers.Data.ItemDBDict.FirstOrDefault(x => x.Value.itemTemplateID == templateID && x.Value.count < infos.itemInfo.maxCount).Value;
            
            //first or default의 Nullcheck같은 느낌
            if (infos.itemDB.Equals(default(KeyValuePair)))
            {
                infos.itemDB = null;
                Debug.Log("해당하는 DB 찾을 수 없음");
            }
            return infos;
        }

        
        public static Item CollectItem(int templateID, int count = 1)
        {
            ItemInfos infos = CheckItem(templateID);
            if(infos.itemDB == null)
            {
                CollectNewItem(infos.itemInfo, count);
            }

            //TODO
            //itemDB에서 검색이 된 경우로, 가지고있는 아이템을 획득한경우이다.
            //장비 등과 같이 non-Stackable한 아이템일수도, Stackable 하지만 최대개수만큼 가지고있을 수도 있다.

            foreach(KeyValuePair<int, ItemData> kv  in Managers.Data.ItemDBDict)
            {
                // templateid 일치하고
                if(kv.Value.itemTemplateID == templateID)
                {
                    // non-Stackable한 아이템일경우
                    if(infos.itemInfo.maxCount == 1)
                    {
                        CollectNewItem(infos.itemInfo, count);
                    }

                    if(kv.Value.count + count <= infos.itemInfo.maxCount)
                    {

                        //주운 아이템을 더해도 최대개수를 넘기지 않는 경우
                        kv.Value.count += count;
                    }

                    else if(kv.Value.count + count > infos.itemInfo.maxCount)
                    {
                        //주운 아이템을 더하면 최대개수를 넘기는 경우
                        count = count - infos.itemInfo.maxCount + kv.Value.count;
                        kv.Value.count = infos.itemInfo.maxCount;
                        CollectNewItem(infos.itemInfo, count);
                    }
                    
                    //다른경우가 없잖아
                }
            }

            return null;
        }
        public static Item InfotoItem(ItemInfo itemInfo, int count = 1)
        {
            Item item = new Item(itemInfo.itemType);
            item.Count = count;
            item.Stackable = itemInfo.maxCount > 1;
            item.Description = itemInfo.description;
            item.Name = itemInfo.name;
            return item;
        }
        public static Item CollectNewItem(ItemInfo iteminfo, int count = 1)
        {
            //TODO
            // DB에 없으므로 새로운 아이템으로 인식하여 획득과 동시에 DB에 저장 후 InventoryManager에 알림
            if(iteminfo == null)
            {
                Debug.Log(iteminfo);
                return null;
            }
            int? slot =  Managers.Inven.FindEmptySlot();
            if(slot == null)
            {
                Debug.Log("인벤토리에 빈공간이 없음");
                return null;
            }

            //TODO
            Item item = null;
            switch (iteminfo.itemType)
            {
                case Enums.ItemType.Weapon:
                    item = new Weapon(iteminfo.itemTemplateid);
                    break;
                case Enums.ItemType.Armor:
                    item = new Armor(iteminfo.itemTemplateid);
                    break;
                case Enums.ItemType.Consumable:
                    item = new Consumable(iteminfo.itemTemplateid);
                    break;
                case Enums.ItemType.Useable:
                    item = new Useable(iteminfo.itemTemplateid);
                    break;
            }
            item.ItemType = iteminfo.itemType;
            item.Count = count;
            item.Stackable = iteminfo.maxCount > 1;
            item.Description = iteminfo.description;
            item.Name = iteminfo.name;
            item.Slot = (int)slot;
            
            //newItemDB.itemDbid = 새로운 dbid 생성하는 함수 필요

            //db에 저장
            ItemData newItemDB = new ItemData();
            newItemDB.itemDbID = Managers.Inven.NewItemDbID();
            newItemDB.slot = (int)slot;
            newItemDB.count = count;
            newItemDB.itemTemplateID = iteminfo.itemTemplateid;
            newItemDB.itemType = iteminfo.itemType;
            newItemDB.description = iteminfo.description;
            newItemDB.name = iteminfo.name;

            Managers.Data.ItemDBDict.Add(newItemDB.itemDbID, newItemDB);
            //인벤토리에 추가
            Managers.Inven.Add(item);
            // item 클래스를 리턴
            return item;
        }

        
        public static ItemData ItemToDB(Item item)
        {
            ItemData itemDB = new ItemData();
            int? newSlot = Managers.Inven.FindEmptySlot();
            if (newSlot == null)
            {
                Debug.Log("인벤토리 꽉참");
                return null;
            }
            else
            {
                itemDB.slot = (int)newSlot;
            }
            switch (item.ItemType)
            {
                case Enums.ItemType.Weapon:

                    itemDB.itemType = item.ItemType;
                    Weapon weapon = (Weapon)item;
                    WeaponData weaponDB = (WeaponData)itemDB;
                    weaponDB.weaponType = weapon.WeaponType;
                    weaponDB.attack = weapon.Attack;
                    break;
                case Enums.ItemType.Armor:

                    itemDB.itemType = item.ItemType;
                    Armor armor = (Armor)item;
                    ArmorData armorDB = (ArmorData)itemDB;
                    armorDB.armorType = armor.ArmorType;
                    armorDB.defense = armor.Defense;
                    break;
                case Enums.ItemType.Consumable:

                    itemDB.itemType = item.ItemType;
                    Consumable consumable = (Consumable)item;
                    ConsumableData consumableDB = (ConsumableData)itemDB;
                    consumableDB.consumableType = consumable.ConsumableType;
                    consumableDB.buffType = consumable.BuffType;
                    break;

                case Enums.ItemType.Useable:

                    itemDB.itemType = item.ItemType;
                    Useable useable = (Useable)item;
                    UseableData useableDB = (UseableData)itemDB;
                    useableDB.useableType = useable.Useabletype;

                    break;
            }
            
            itemDB.name = item.Name;
            itemDB.count = item.Count;
            itemDB.itemDbID = item.ItemDbId;
            itemDB.description = item.Description;
            
            return itemDB;
        }
        public static Item GetItemFromDB(ItemData itemDB)
        {
            Item item = null;

            if(itemDB == null)
            {
                //DB에 없는 아이템 불러오기 시도
                return null;
            }

            Debug.Log("Item Script :: GetItemFromDB 실행" + itemDB.name);

            switch (itemDB.itemType)
            {
                case Enums.ItemType.Weapon:
                    Debug.Log("Weapon Item Case" + itemDB.name);

                    item = new Weapon(itemDB.itemTemplateID, itemDB.itemDbID);

                    break;
                case Enums.ItemType.Armor:
                    Debug.Log("Armor Item Case" + itemDB.name);

                    item = new Armor(itemDB.itemTemplateID, itemDB.itemDbID);
                    break;
                case Enums.ItemType.Consumable:
                    Debug.Log("Consumable Item Case" + itemDB.name);

                    item = new Consumable(itemDB.itemTemplateID, itemDB.itemDbID);
                    break;
                case Enums.ItemType.Useable:
                    Debug.Log("Useable Item Case" + itemDB.name);

                    item = new Useable(itemDB.itemTemplateID, itemDB.itemDbID);
                    break;
            }
            
            if(item == null)
            {
                // 아이템 생성이 안된경우
                return null;
            }
            item.Slot = itemDB.slot;
            item.ItemDbId = itemDB.itemDbID;
            return item;
        }

    }
    //클라이언트가 들고있는거
    public class Weapon : Item 
    {
        public Enums.WeaponType WeaponType { get; private set; }
        public int Attack { get; private set; }
        public Weapon(int templateid, int? dbID = null) : base(Enums.ItemType.Weapon)
        {
            Init(templateid, dbID);
        }
        void Init(int templateid, int? dbID = null)
        {
            //DBid가 null로 입력받은 경우 DB 조회의 의미가 아니다
            if (dbID != null)
            {
                ItemDbId = (int)dbID;
            }
            Debug.Log("Weapon Item Init" + templateid);

            WeaponInfo weaponInfo = new WeaponInfo();
            Managers.Data.WeaponInfoDict.TryGetValue(templateid, out weaponInfo);

            Debug.Log("TryGetValue" + weaponInfo.name);
            Debug.Log("itemtype is ::" + weaponInfo.itemType);
            if (weaponInfo.itemType != Enums.ItemType.Weapon)
            {
                
                //크래쉬
                return;
            }

            Debug.Log("Null Check 이후");
            Debug.Log("WeaponInfo 아이템 받아오기");
            Name = weaponInfo.name;
            ItemTemplateId = weaponInfo.itemTemplateid;
            Count = 1;
            WeaponType = weaponInfo.weaponType;
            Attack = weaponInfo.attack;
            Stackable = false;


            ReduceWhenUse = false;

        }
    }
    

    public class Armor : Item
    {
        public Enums.ArmorType ArmorType { get; private set; }
        public int Defense { get; private set; }
        public Armor(int templateid, int? dbID = null) : base(Enums.ItemType.Armor)
        {
            Init(templateid, dbID);
        }
        void Init(int templateid, int? dbID = null)
        {
            if (dbID != null)
            {
                ItemDbId = (int)dbID;
            }

            ArmorInfo armorInfo = new ArmorInfo();
            Managers.Data.ArmorInfoDict.TryGetValue(templateid, out armorInfo);
            
            if(armorInfo.itemType != Enums.ItemType.Armor)
            {
                //크래쉬 아마 database가 잘못되었을 가능성 높음
                return;
            }

            Name = armorInfo.name;
            ItemTemplateId = armorInfo.itemTemplateid;
            Count = 1;
            ArmorType = armorInfo.armorType;
            Defense = armorInfo.defense;
            Stackable = false;

            ReduceWhenUse = false;

        }
    }
    public class Consumable : Item
    {
        public Enums.ConsumableType ConsumableType { get; private set; }
        public int HP { get; private set; }
        public Enums.BuffType BuffType { get; private set; }
        public Consumable(int templateid, int? dbID = null) : base(Enums.ItemType.Consumable)
        {
            Init(templateid, dbID);
        }
        void Init(int templateid, int? dbID = null)
        {
            if (dbID != null)
            {
                ItemDbId = (int)dbID;
            }
            ConsumableInfo consumableInfo = new ConsumableInfo();
            Managers.Data.ConsumableInfoDict.TryGetValue(templateid, out consumableInfo);

            if (consumableInfo.itemType != Enums.ItemType.Consumable)
            {
                //크래쉬 아마 database가 잘못되었을 가능성 높음
                return;
            }

            Name = consumableInfo.name;
            ItemTemplateId = consumableInfo.itemTemplateid;
            Count = 1;
            ConsumableType = consumableInfo.consumableType;
            BuffType = consumableInfo.buffType;
            HP = consumableInfo.hp;
            Stackable = false;

            ReduceWhenUse = true;

        }
    }
    public class Useable : Item
    {
        public Enums.UseableType Useabletype { get; private set;}
        public Useable(int templateid, int? dbID = null) : base(Enums.ItemType.Useable)
        {
            Init(templateid, dbID);
        }
        void Init(int templateid, int? dbID = null)
        {
            if(dbID != null)
            {
                ItemDbId = (int)dbID;
            }

            UseableInfo useableInfo = new UseableInfo();
            Managers.Data.UseableInfoDict.TryGetValue(templateid, out useableInfo);
            if (useableInfo.itemType != Enums.ItemType.Useable)
            {
                //크래쉬 아마 database가 잘못되었을 가능성 높음
                return;
            }
            Name = useableInfo.name;
            ItemTemplateId = useableInfo.itemTemplateid;
            Count = 1;
            Useabletype = useableInfo.useableType;
            Stackable = useableInfo.maxCount > 1;

            ReduceWhenUse = true;

        }
    }
 
}