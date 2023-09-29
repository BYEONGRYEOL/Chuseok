using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Isometric.Data;
namespace Isometric
{

    public class ItemManager
    {

        public void Init()
        {

        }

        public void Use(Item item, int useAmount = 1)
        {
            //사용하고자하는 개수보다 적게가지고있으면 그만큼만 사용할수도있지만 일단 리턴하자
            if (Managers.Inven.Items[item.ItemDbId].Count < useAmount)
            {
                return;
            }
            switch (item.ItemType)
            {
                case Enums.ItemType.Weapon:
                    Weapon weapon = (Weapon)item;

                    if(weapon.WeaponType == Enums.WeaponType.Sword)
                    {
                        //검 사용한거 근데 사용이라고 해야하나?
                    }

                    break;
                case Enums.ItemType.Armor:
                    Armor armor = (Armor)item;
                    break;
                case Enums.ItemType.Consumable:
                    Consumable consumable = (Consumable)item;
                    switch (consumable.ConsumableType)
                    {
                        case Enums.ConsumableType.Potion:
                            //hp회복
                            PlayerController.Instance.stat.HPRecovery(consumable.HP);
                            break;
                        case Enums.ConsumableType.Food:
                            break;

                        default:
                            Debug.Log("consumable 아이템 사용");
                            break;
                    }
                    
                    break;
                case Enums.ItemType.Useable:
                    Useable useable = (Useable)item;

                    switch (useable.Useabletype)
                    {
                        case Enums.UseableType.Scroll:
                            Debug.Log("Scroll 사용");
                            break;
                        case Enums.UseableType.Crops:
                            Debug.Log("Crops 사용");
                            break;
                        case Enums.UseableType.ThrowingWeapon:
                            Debug.Log("TrowingWeapon 사용");
                            break;
                    }
                    break;

                default:
                    Debug.Log(item.Name + " 아이템 사용");
                    if (item.ReduceWhenUse)
                    {

                    }
                    break;

            }
        }
    
    }

}