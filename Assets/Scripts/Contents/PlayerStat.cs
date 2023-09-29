using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Isometric.Data;

namespace Isometric
{
    public class PlayerStat : MonoBehaviour
    {
        int level;
        int str;
        int dex;
        int intel;
        int totalExp;

        float attack;
        float defense;
        float moveSpeed;
        float accuracy;
        float dodge;

        //��������
        int maxHp;
        float attackDelay;
        float attackInterval;
        float attackDuration;
        float attackRange;
        float takeDamageDelay;

        float equipAttack;
        float equipDefense;
        float equipMoveSpeed;
        float equipAccuracy;
        float equipDodge;



        //��������
        float hp;



        public int MaxHp { get => maxHp; set => maxHp = value; }
        public float Attack { get => attack; set => attack = value; }
        public float Defense { get => defense; set => defense = value; }
        public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
        public float Accuracy { get => accuracy; set => accuracy = value; }
        public float Dodge { get => dodge; set => dodge = value; }
        public float AttackDuration { get => attackDuration; set => attackDuration = value; }
        public float AttackInterval { get => attackInterval; set => attackInterval = value; }
        public float AttackDelay { get => attackDelay; set => attackDelay = value; }
        public float AttackRange { get => attackRange; set => attackRange = value; }
        public float TakeDamageDelay { get => takeDamageDelay; set => takeDamageDelay = value; }

        public float Hp { get => hp; set => hp = value; }


        public int Level { get => level; set => level = value; }
        public int Str { get => str; set => str = value; }
        public int Dex { get => dex; set => dex = value; }
        public int Intel { get => intel; set => intel = value; }




        public void HPRecovery(float recovery)
        {
            //ȸ������ �ִ�hp���� ũ�� �ִ� hp��
            if (recovery + hp > maxHp)
            {
                hp = maxHp;
            }
            else
            {
                hp += recovery;
            }
        }

        public Enums.CharacterState GetAttacked(Stat attacker, Enums.AttackType attackType = Enums.AttackType.AD)
        {
            float damage = 0;
            switch (attackType)
            {
                case Enums.AttackType.AD:
                    //���߷��� ȸ���� ���
                    //�����Լ� 
                    if (Random.Range(0, 1) < (2 * accuracy) / (accuracy + attacker.Dodge))
                    {
                        //���� �� ���ݷ� ���� ���
                        damage = Random.Range(attacker.Attack - attacker.Attack* 0.2f, attacker.Attack + attacker.Attack * 0.2f)
                            - Random.Range(defense - defense * 0.2f, defense + defense * 0.2f);

                        if (damage < 0)
                        {
                            damage = 0;
                            return Enums.CharacterState.TakeDamage;
                        }
                        else if (hp - damage < 0)
                        {

                            // enemy ���
                            // ĳ���� ����
                            Stat enemyStat = attacker as Stat;
                            if (enemyStat == null)
                            {
                                // ĳ���ÿ� ���� -> ���� ���ΰ� �ƴ�.
                                return Enums.CharacterState.Die;
                            }
                            return Enums.CharacterState.Die;
                        }
                        else
                        {
                            hp -= damage;
                            return Enums.CharacterState.TakeDamage;
                        }
                    }
                    else
                    {
                        //ȸ��
                        return Enums.CharacterState.None;
                    }
                    break;

                case Enums.AttackType.AP:
                    return Enums.CharacterState.None;
                case Enums.AttackType.Fixed:
                    return Enums.CharacterState.None;
                default:
                    return Enums.CharacterState.None;
            }

        }

        public void HPInit(float currentHp, int maxHp)
        {
            hp = currentHp;
            this.maxHp = maxHp;
        }

        private void Start()
        {
            Init();
        }

        public int TotalExp 
        {
            get => totalExp; 
            
            set
            {
                totalExp = value;
                int nowlevel = Level;
                while (true)
                {
                    Data.PlayerStatData playerStat;
                    if(Managers.Data.PlayerStatDict.TryGetValue(nowlevel + 1, out playerStat) == false)
                    {
                        Debug.Log("PlayerStatDict���� �� �������� ����");
                        break;
                    }
                    else if(totalExp < playerStat.totalExp)
                    {
                        //���� �� ����ġ�� ���� ������ ������ ���� ���Ѱ��
                        break;
                    }
                    //
                    nowlevel++;
                }
                if(nowlevel > Level)
                {
                    Debug.Log("������");
                    Level = nowlevel;

                    //�������� ���� ����
                }
                
            }
        }
        
        public void Init()
        {
            //���̺� �����Ͱ� ������
            Level = 1;
            SetStatwithLevel(Level);
            MoveSpeed = 2f;
               
        }
        public void SetStatwithLevel(int level)
        {
            Dictionary<int, Isometric.Data.PlayerStatData> dict = Managers.Data.PlayerStatDict;
            Data.PlayerStatData playerStat = dict[level];
            str = playerStat.str;
            dex = playerStat.dex;
            intel = playerStat.intel;
            MaxHp = playerStat.maxHp;
            // ������ �� ���� ü���� �ִ�ü������ �ʱ�ȭ
            Hp = MaxHp;
            Accuracy = playerStat.accuracy;
            Defense = playerStat.defense;
            Dodge = playerStat.dodge;
            
        }

        public void CheckEquipment()
        {
            //Managers.Inven.Find
        }
    }
}