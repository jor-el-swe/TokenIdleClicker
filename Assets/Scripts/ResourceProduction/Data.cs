using UnityEngine;
using UnityEngine.Serialization;

namespace ResourceProduction {
    [CreateAssetMenu]
    public class Data : ScriptableObject {
        
        [SerializeField] private Resource.Resource resource;
        [SerializeField] private float productionTime;
        [SerializeField] private float productionTimeMultiplier;
        [SerializeField] private float minimumProductionTime;
        [SerializeField] private int increaseSpeedThreshold;
        [SerializeField] private int generatedAmount;
        [SerializeField] private float generatedAmountMultiplier = 3f;
        [SerializeField] private int price;
        [SerializeField] private float priceMultiplier;
        [SerializeField] private int levelUpgradePrice;
        [SerializeField] private float levelUpgradePriceMultiplier;
        [SerializeField] private int autoClickerPrice;

        public Resource.Resource Resource => resource;
        private string AutoClickerKey => $"{name}_autoClicker";
        public int AutoClickerPrice => autoClickerPrice;
        public bool AutoClickerActive => AutoClicker == 1;
        public int AutoClicker {
            get => PlayerPrefs.GetInt(AutoClickerKey, 0);
            set {
                //value can only be 0 or 1
                if (value != 0) value = 1;
                PlayerPrefs.SetInt(AutoClickerKey, value);
            }
        }

        public int GetActualPrice(int numberGenerators) {
            return Mathf.CeilToInt(price * Mathf.Pow(priceMultiplier, numberGenerators));
        }

        public int GetActualProductionAmount(int generatorLevel) {
            return Mathf.CeilToInt(generatedAmount * Mathf.Pow(generatedAmountMultiplier, generatorLevel));
        }

        public int GetActualUpgradePrice(int level) {
            return Mathf.CeilToInt(levelUpgradePrice * Mathf.Pow(levelUpgradePriceMultiplier, level));
        }

        public float GetActualProductionTime(int numberOwned) {
            var increments = numberOwned / increaseSpeedThreshold;
            var actualProductionTime = productionTime * Mathf.Pow(productionTimeMultiplier, increments);
            if (actualProductionTime < minimumProductionTime)
                actualProductionTime = minimumProductionTime;
            return actualProductionTime;
        }
    }
}