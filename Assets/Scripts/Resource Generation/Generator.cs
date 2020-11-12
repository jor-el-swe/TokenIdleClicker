
using System;
using UnityEngine;

namespace Resource_Generation
{
    public class Generator : MonoBehaviour {
        
        [SerializeField] private Data data;
        private string OwnedKey => $"{data.name}_owned";
        private string LevelKey => $"{data.name}_level";

        private float timer;
        private bool isProducing;

        public int NumberOwned
        {
            get => PlayerPrefs.GetInt(OwnedKey, 0);
            set => PlayerPrefs.SetInt(OwnedKey, value);
        }
        public int Level
        {
            get => PlayerPrefs.GetInt(LevelKey, 0);
            set => PlayerPrefs.SetInt(LevelKey, value);
        }
        void Generate() {
            data.resource.CurrentAmount += data.generatedAmount;
            this.timer = 0f;
            this.isProducing = false;
        }

        public void StartProduction() {
            this.isProducing = true;
        }

        private void Update() {
            if(!this.isProducing) 
                return;
            
            this.timer += Time.deltaTime;

            if (this.timer < data.ProductionTime)
                return;
            Generate();
        }
    }
}
