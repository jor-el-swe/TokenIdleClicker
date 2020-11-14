using UnityEngine;

namespace Resource_Generation {
    [CreateAssetMenu]
    public class Data : ScriptableObject {
        
        [SerializeField] private Resource resource;
        [SerializeField] private float productionTime;
        [SerializeField] private float productionTimeMultiplier;
        [SerializeField] private float minimumProductionTime;
        [SerializeField] private int increaseSpeedThreshold;
        [SerializeField] private int generatedAmount;
        [SerializeField] private float generatedAmountMultiplier = 3f;
        [SerializeField] private int price;
        [SerializeField] private float priceMultiplier;
        [SerializeField] private int levelUpgradePrice;
        [SerializeField] private float levelUpgradeMultiplier;

        public Resource Resource => resource;
        private string AutoClickerKey => $"{name}_autoClicker";
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
            return Mathf.CeilToInt(price * Mathf.Pow(priceMultiplier, numberGenerators-1));
        }

        public int GetActualProductionAmount(int generatorLevel) {
            return Mathf.CeilToInt(generatedAmount * Mathf.Pow(generatedAmountMultiplier, generatorLevel));
        }

        public int GetActualUpgradePrice(int level) {
            return Mathf.CeilToInt(levelUpgradePrice * Mathf.Pow(levelUpgradeMultiplier, level));
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