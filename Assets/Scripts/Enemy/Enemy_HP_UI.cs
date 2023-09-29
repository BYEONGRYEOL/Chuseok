using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Isometric
{

    public class Enemy_HP_UI : MonoBehaviour
    {
        private Stat stat;
        [SerializeField] private Image hp_bar;
        private float currentFill;
        private float lerpSpeed = 2f;
        private bool getAttack = false;
        void Start()
        {
            stat = GetComponentInParent<Stat>();
        }

        // Update is called once per frame
        void Update()
        {
            if (currentFill != hp_bar.fillAmount)
            {
                hp_bar.fillAmount = Mathf.Lerp(hp_bar.fillAmount, stat.Hp / (float)stat.MaxHp, Time.deltaTime * lerpSpeed);
            }
        }

        public void HP_barSetValue(float currentValue, float maxValue)
        {
            currentFill = currentValue / maxValue;
        }
    }

}