using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Isometric
{

    // ��Ŭ�� ���콺�� �����۾����� ��������
    [CreateAssetMenu(fileName = "New Plant", menuName = "Plant")]
    public class PlantObject : ScriptableObject
    {

        public string plantName; // �Ĺ� �ִ� ĭ(������, �丶��, �عٶ��) 
        public Sprite[] plantStages; // �Ĺ� ���� ����(����, ������, ������)
        public float timeBtwStages; // �Ĺ��� �����ϴµ� �ɸ��� �ð�(�� �ܰ躰�� �����) 


    }

}