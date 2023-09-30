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





        public void HPInit(float currentHp, int maxHp)
        {
            hp = currentHp;
            this.maxHp = maxHp;
        }

        private void Start()
        {
            Init();
        }

        
        
        public void Init()
        {
            //세이브 데이터가 없으면
            Level = 1;
            MoveSpeed = 2f;
               
        }
        

    }
}