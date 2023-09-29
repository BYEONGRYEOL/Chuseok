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
            //����ϰ����ϴ� �������� ���԰����������� �׸�ŭ�� ����Ҽ��������� �ϴ� ��������
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
                        //�� ����Ѱ� �ٵ� ����̶�� �ؾ��ϳ�?
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
                            //hpȸ��
                            PlayerController.Instance.stat.HPRecovery(consumable.HP);
                            break;
                        case Enums.ConsumableType.Food:
                            break;

                        default:
                            Debug.Log("consumable ������ ���");
                            break;
                    }
                    
                    break;
                case Enums.ItemType.Useable:
                    Useable useable = (Useable)item;

                    switch (useable.Useabletype)
                    {
                        case Enums.UseableType.Scroll:
                            Debug.Log("Scroll ���");
                            break;
                        case Enums.UseableType.Crops:
                            Debug.Log("Crops ���");
                            break;
                        case Enums.UseableType.ThrowingWeapon:
                            Debug.Log("TrowingWeapon ���");
                            break;
                    }
                    break;

                default:
                    Debug.Log(item.Name + " ������ ���");
                    if (item.ReduceWhenUse)
                    {

                    }
                    break;

            }
        }
    
    }

}