
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Resource_Generation
{
    public class Generator : MonoBehaviour {
        
        [SerializeField] private Data data;
        [SerializeField] private Text buyText;
        
        private string OwnedKey => $"{data.name}_owned";
        private string LevelKey => $"{data.name}_level";

        private float timer;
        private bool isProducing;

        public int NumberOwned {
            get => PlayerPrefs.GetInt(OwnedKey, 1);
            set => PlayerPrefs.SetInt(OwnedKey, value);
        }
        
        public int Level {
            get => PlayerPrefs.GetInt(LevelKey, 0);
            set => PlayerPrefs.SetInt(LevelKey, value);
        }

        public void Buy() {
            if(data.resource.CurrentAmount < data.price)
                return;
            data.resource.CurrentAmount -= data.price;

            NumberOwned++;
        }

        public void StartProduction() {
            isProducing = true;
        }

        private void Update() {
            if(!isProducing) 
                return;
            
            timer += Time.deltaTime;

            if (timer < data.ProductionTime)
                return;
            Generate();
        }
        
        void Generate() {
            data.resource.CurrentAmount += data.generatedAmount * NumberOwned;
            timer = 0f;
            isProducing = false;
        }
    }
}
