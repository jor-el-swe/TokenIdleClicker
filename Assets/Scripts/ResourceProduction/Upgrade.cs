using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ResourceProduction {
    public class Upgrade : MonoBehaviour {
        private int maxLevel = 2;
        [SerializeField] private Data data;
        [SerializeField] private Text buyText;
        [SerializeField] private Text levelText;
        
        private string LevelKey => $"{data.name}_upgradeLevel";
        private int Level {
            get => PlayerPrefs.GetInt(LevelKey, 0);
            set => PlayerPrefs.SetInt(LevelKey, value);
        }
        
        private void Start() {
            SetLevel();
            //TODO show correct image
            switch (Level) {
                case 0:
                    UpdateButtonText(data.FirstUpgradePrice);
                    break;
                case 1:
                    UpdateButtonText(data.SecondUpgradePrice);
                    break;
                case 2:
                    buyText.text = "";
                    levelText.text = "Rank: Max";
                    break;
            }
        }
        
        public void Buy() {
            var currentTokens = data.Resource.CurrentAmount;
            var firstUpgradePrice = data.FirstUpgradePrice;
            var secondUpgradePrice = data.SecondUpgradePrice;
            //TODO show correct image
            switch (Level) {
                case 0 when currentTokens >= firstUpgradePrice:
                    BuyNextUpgrade(firstUpgradePrice);
                    UpdateButtonText(secondUpgradePrice);
                    break;
                case 1 when currentTokens >= secondUpgradePrice:
                    BuyNextUpgrade(secondUpgradePrice);
                    buyText.text = "";
                    levelText.text = "Rank: Max";
                    //TODO disable button
                    break;
                case 2:
                    return;
            }
        }
        
        private void BuyNextUpgrade(ulong upgradePrice) {
            data.Resource.CurrentAmount -= upgradePrice;
            Level++;
            SetLevel();
        }
        
        private void SetLevel() {
            data.Level = Level;
        }
        
        private void UpdateButtonText(ulong currentPrice) {
            buyText.text = $"Upgrade:\n{SuffixHelper.GetString(currentPrice)}";
            levelText.text = $"Rank: {Level}";
        }
    }
}
