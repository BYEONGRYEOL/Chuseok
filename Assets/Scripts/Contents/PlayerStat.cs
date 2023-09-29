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

        //고유정보
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



        //변동정보
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
            //회복량이 최대hp보다 크면 최대 hp로
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
                    //명중률과 회피율 계산
                    //명중함수 
                    if (Random.Range(0, 1) < (2 * accuracy) / (accuracy + attacker.Dodge))
                    {
                        //명중 시 공격력 방어력 계산
                        damage = Random.Range(attacker.Attack - attacker.Attack* 0.2f, attacker.Attack + attacker.Attack * 0.2f)
                            - Random.Range(defense - defense * 0.2f, defense + defense * 0.2f);

                        if (damage < 0)
                        {
                            damage = 0;
                            return Enums.CharacterState.TakeDamage;
                        }
                        else if (hp - damage < 0)
                        {

                            // enemy 사망
                            // 캐스팅 과정
                            Stat enemyStat = attacker as Stat;
                            if (enemyStat == null)
                            {
                                // 캐스팅에 실패 -> 적이 죽인게 아님.
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
                        //회피
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
                        Debug.Log("PlayerStatDict에서 값 가져오기 실패");
                        break;
                    }
                    else if(totalExp < playerStat.totalExp)
                    {
                        //현재 총 경험치가 다음 레벨업 기준을 넘지 못한경우
                        break;
                    }
                    //
                    nowlevel++;
                }
                if(nowlevel > Level)
                {
                    Debug.Log("레벨업");
                    Level = nowlevel;

                    //레벨업시 스탯 갱신
                }
                
            }
        }
        
        public void Init()
        {
            //세이브 데이터가 없으면
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
            // 레벨업 시 현재 체력을 최대체력으로 초기화
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