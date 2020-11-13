
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Resource_Generation
{
    public class Generator : MonoBehaviour {
        
        [SerializeField] private Data data;
        [SerializeField] private Text buyText;
        [SerializeField] private Text upgradeText;
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
            if(data.Resource.CurrentAmount < data.GetActualPrice(NumberOwned))
                return;
            data.Resource.CurrentAmount -= data.GetActualPrice(NumberOwned);
            NumberOwned++;
            UpdateBuyText();
        }

        public void Upgrade()
        {   if(!(Level<3) || data.Resource.CurrentAmount < data.GetActualUpgradePrice(Level))
                return;
            data.Resource.CurrentAmount -= data.GetActualUpgradePrice(Level);   
            Level++;
            UpdateLevelText();
        }

        public void StartProduction() {
            isProducing = true;
        }

        private void Start()
        {
            UpdateBuyText();
            UpdateLevelText();
        }

        private void UpdateLevelText()
        {
            upgradeText.text = $"Upgrade {data.GetActualUpgradePrice(Level)} Tokens ";
        }

        private void UpdateBuyText()
        {
            buyText.text = $"Buy {data.GetActualPrice(NumberOwned)} Tokens ";
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
            data.Resource.CurrentAmount += data.GetActualProductionAmount(Level)*NumberOwned;
            timer = 0f;
            isProducing = false;
        }
    }
}
