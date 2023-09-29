using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Isometric
{

    // 우클릭 마우스로 게임작업상자 생성가능
    [CreateAssetMenu(fileName = "New Plant", menuName = "Plant")]
    public class PlantObject : ScriptableObject
    {

        public string plantName; // 식물 넣는 칸(옥수수, 토마토, 해바라기) 
        public Sprite[] plantStages; // 식물 성장 순서(새싹, 안익음, 잘익음)
        public float timeBtwStages; // 식물이 성장하는데 걸리는 시간(각 단계별로 적용됨) 


    }

}