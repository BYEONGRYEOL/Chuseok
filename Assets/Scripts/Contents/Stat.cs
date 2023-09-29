using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Isometric
{

    public class Stat : MonoBehaviour
    {
        //고유정보
        int enemy_id;
        int maxHp;
        float attack;
        float defense;
        float moveSpeed;
        float accuracy;
        float dodge;
        float detectionRange;
        float attackDelay;
        float attackInterval;
        float attackDuration;
        float attackRange;
        float takeDamageDelay;
        bool immediateAttack;
        int rewardExp;
        

        //변동정보
        float hp;


        public int Enemy_ID { get => enemy_id; }
        public int MaxHp { get => maxHp; set => maxHp = value; }
        public float Attack { get => attack; set => attack = value; }
        public float Defense { get => defense; set => defense = value; }
        public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

        public float Accuracy { get => accuracy; set => accuracy = value; }
        public float Dodge { get => dodge; set => dodge = value; }

        public float AttackDuration { get => attackDuration; set => attackDuration = value; }
        public float DetectionRange { get => detectionRange; set => detectionRange = value; }
        public float AttackInterval { get => attackInterval; set => attackInterval = value; }
        public float AttackDelay { get => attackDelay; set => attackDelay = value; }
        public float AttackRange { get => attackRange; set => attackRange = value; }
        public float TakeDamageDelay { get => takeDamageDelay; set => takeDamageDelay = value; }
        public bool ImmediateAttack { get => immediateAttack; set => immediateAttack = value; }
        public int RewardExp { get => rewardExp; set => rewardExp = value; }


        public float Hp { get => hp; set => hp = value; }


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

        public Enums.CharacterState GetAttacked(PlayerStat attacker, Enums.AttackType attackType = Enums.AttackType.AD)
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
                        damage = Random.Range(attacker.Attack - attacker.Attack * 0.2f, attacker.Attack + attacker.Attack* 0.2f)
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
                            PlayerStat playerstat = attacker as PlayerStat;
                            if(playerstat == null)
                            {
                                // 캐스팅에 실패 -> 플레이어가 아니라는 뜻.
                                return Enums.CharacterState.Die;
                            }
                            
                            playerstat.TotalExp += RewardExp;
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

        public virtual void Start()
        {
            Attack = 5;
            Accuracy = 1;
            MoveSpeed = 1f;
            TakeDamageDelay = 1f;
            AttackDuration = 0.1f;
        }
        private void Init()
        {
            
            foreach(KeyValuePair<int, Data.EnemyStatData> enemyStat in Managers.Data.EnemyStatDict)
            {
                if(enemyStat.Value.name == this.gameObject.name)
                {
                    enemy_id = enemyStat.Key;
                    break;
                }
                Debug.Log("Can't Find Enemy Id Error :: id" + this.gameObject.name);
            }

            Data.EnemyStatData stat = Managers.Data.EnemyStatDict[enemy_id];

            MaxHp = stat.maxHp;
            Attack = stat.attack;
            Defense = stat.defense;
            MoveSpeed = stat.moveSpeed;
            Accuracy = stat.accuracy;
            Dodge = stat.dodge;
            AttackDuration = stat.attackDuration;
            DetectionRange = stat.detectionRange;
            AttackInterval = stat.attackInterval;
            AttackDelay = stat.attackDelay;
            AttackRange = stat.attackRange;
            TakeDamageDelay = stat.takeDamageDelay;
            ImmediateAttack = stat.immediateAttack;
            RewardExp = stat.rewardExp;
        }
    }

}