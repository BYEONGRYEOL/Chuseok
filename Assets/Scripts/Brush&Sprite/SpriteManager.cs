using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Isometric
{

    public class SpriteManager : MonoBehaviour
    {
        bool IsPlanted = false;
        bool existence = true;
        SpriteRenderer plant;
        BoxCollider2D plantCollider;


        int random;
        int plantStage = 0;
        float timer;

        public PlantObject selectedPlant;

        // Start is called before the first frame update
        void Start()
        {
            //Debug.Log("SpriteManager scripts :: Start Function run");

            plant = transform.GetChild(0).GetComponent<SpriteRenderer>();
            plantCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();
            random = Random.Range(0, 2);
        }

        // Update is called once per frame
        void Update()
        {
            if (IsPlanted)
            {
                timer -= Time.deltaTime;

                if (timer < 0 && plantStage < selectedPlant.plantStages.Length - 1)
                {
                    timer = selectedPlant.timeBtwStages;
                    plantStage++;
                    UpdatePlant();
                }
            }

            if (random == 0)
            {
                PlantTree();
            }
            else
            {
                Harvest();
            }
        }
        void PlantTree()
        {
            //Debug.Log("SpriteManager scripts :: PlantTree Function run");
            if (IsPlanted) // false
            {
                if (plantStage == selectedPlant.plantStages.Length - 1)
                {
                    //Harvest();
                }

            }
            else //true
            {
                Plant();
            }
        }

        void Harvest()
        {
            IsPlanted = false;
            plant.gameObject.SetActive(false);
        }

        void Plant()
        {
            //Debug.Log("SpriteManager scripts :: Plant Function run");

            IsPlanted = true;
            plantStage = 0;
            UpdatePlant();
            timer = selectedPlant.timeBtwStages;
            plant.gameObject.SetActive(true);
        }

        void UpdatePlant()
        {
            plant.sprite = selectedPlant.plantStages[plantStage];
            plantCollider.size = plant.sprite.bounds.size;
            plantCollider.offset = new Vector2(0, plant.bounds.size.y / 2);
        }

        /*public void RandomDraw()
        {
            int random = Random.Range(0, 6);

            switch (random)
            {
                case 0:
                case 1:
                case 2:
                    IsPlanted = false;
                    break;
                case 3:
                case 4:
                case 5:
                    IsPlanted = true;
                    break;
            }
        }*/

    }

}