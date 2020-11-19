using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ResourceProduction {
    public class Upgrade : MonoBehaviour {
        [SerializeField] private Data data;
        [SerializeField] private Text buyText;
        [SerializeField] private Text levelText;
        [SerializeField] private Image[] images;

        private AudioHandler audiohandler;
        private Button BuyButton => GetComponent<Button>();
        private string LevelKey => $"{data.name}_upgradeLevel";
        private int Level {
            get => PlayerPrefs.GetInt(LevelKey, 0);
            set => PlayerPrefs.SetInt(LevelKey, value);
        }
        
        private void Start() {
            //get audioHandler
            audiohandler = FindObjectOfType<AudioHandler>();
            
            SetLevel();
            SetImage();
            SetBuyButton();
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
            
            switch (Level) {
                case 0 when currentTokens >= firstUpgradePrice:
                    BuyNextUpgrade(firstUpgradePrice);
                    UpdateButtonText(secondUpgradePrice);
                    SetImage();
                    break;
                case 1 when currentTokens >= secondUpgradePrice:
                    BuyNextUpgrade(secondUpgradePrice);
                    buyText.text = "";
                    levelText.text = "Rank: Max";
                    SetImage();
                    SetBuyButton();
                    break;
                case 2:
                    return;
            }
        }

        private void FixedUpdate() {
            var currentTokens = data.Resource.CurrentAmount;
            switch (Level) {
                case 0:
                    BuyButton.interactable = currentTokens >= data.FirstUpgradePrice;
                    break;
                case 1:
                    BuyButton.interactable = currentTokens >= data.SecondUpgradePrice;
                    break;
                case 2:
                    BuyButton.interactable = false;
                    break;
            }
        }
        private void BuyNextUpgrade(ulong upgradePrice) {
            audiohandler.Play("upgraded");
            data.Resource.CurrentAmount -= upgradePrice;
            Level++;
            SetLevel();
        }
        
        private void SetImage() {
            for (var i = 0; i < images.Length; i++) {
                images[i].enabled = i == Level;
            }
        }
        
        private void SetBuyButton() {
            BuyButton.enabled = Level != 2;
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
