using UnityEngine;
using UnityEngine.UI;

namespace ResourceProduction
{
    public class Producer : MonoBehaviour {
        private float timer;
        private bool isProducing;
        
        [SerializeField] private Data data;
        [SerializeField] private GameObject produceButton;
        [SerializeField] private Text buyText;
        [SerializeField] private Text upgradeText;
        [SerializeField] private Text numberOwnedText;
        [SerializeField] private Text levelText;
        [SerializeField] private Text productionTimeText;
        [SerializeField] private int maxLevel;
        
        private string OwnedKey => $"{data.name}_owned";
        private string LevelKey => $"{data.name}_level";
        
        private int NumberOwned {
            get => PlayerPrefs.GetInt(OwnedKey, 1);
            set => PlayerPrefs.SetInt(OwnedKey, value);
        }
        
        private int Level {
            get => PlayerPrefs.GetInt(LevelKey, 0);
            set => PlayerPrefs.SetInt(LevelKey, value);
        }

        public void Buy() {
            if(data.Resource.CurrentAmount < data.GetActualPrice(NumberOwned))
                return;
            data.Resource.CurrentAmount -= data.GetActualPrice(NumberOwned);
            NumberOwned++;
            UpdateBuyText();
            UpdateOwnedText();
        }
        
        public void StartProduction() {
            isProducing = true;
        }

        public void Upgrade() {   
            if(!(Level<maxLevel) || data.Resource.CurrentAmount < data.GetActualUpgradePrice(Level))
                return;
            data.Resource.CurrentAmount -= data.GetActualUpgradePrice(Level);   
            Level++;
            UpdateLevelText();
        }
        
        private void Start() {
            UpdateBuyText();
            UpdateLevelText();
            UpdateOwnedText();
        }
        
        private void Update() {
            // Disables produceButton when AutoClicker is active and vice versa
            produceButton.SetActive(!data.AutoClickerActive); 
            
            if (data.AutoClickerActive) {
                Produce();
                return;
            }
            
            if (isProducing) 
                Produce();
        }

        private void Produce() {
            timer += Time.deltaTime;
            if (timer < data.GetActualProductionTime(NumberOwned)) return;
            data.Resource.CurrentAmount += data.GetActualProductionAmount(Level)*NumberOwned;
            timer -= data.GetActualProductionTime(NumberOwned);
            isProducing = false;
        }

        private void UpdateOwnedText() {
            numberOwnedText.text = NumberOwned.ToString();
            productionTimeText.text = data.GetActualProductionTime(NumberOwned).ToString("0.000");
        }
        
        private void UpdateLevelText() {
            upgradeText.text = $"Upgrade {data.GetActualUpgradePrice(Level)} Tokens ";
            levelText.text = Level.ToString();
        }
        
        private void UpdateBuyText() {
            buyText.text = $"Buy {data.GetActualPrice(NumberOwned)} Tokens ";
        }
    }
}
