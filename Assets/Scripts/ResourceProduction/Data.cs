using UnityEngine;
using UnityEngine.Serialization;

namespace ResourceProduction {
    [CreateAssetMenu(fileName = "ProductionData", menuName = "ScriptableObjects/ProductionData")]
    public class Data : ScriptableObject {

        [SerializeField] private Resource.Resource resource;
        [SerializeField] private float productionTime;
        [SerializeField] private float productionTimeMultiplier;
        [SerializeField] private float minimumProductionTime;
        [SerializeField] private int[] increaseSpeedThresholds = {50, 150, 300, 500, 1000};
        [SerializeField] private int productionAmount;
        [SerializeField] private float producedAmountMultiplier = 3f;
        [SerializeField] private int price;
        [SerializeField] private float priceMultiplier;
        [SerializeField] private ulong firstUpgradePrice;
        [SerializeField] private ulong secondUpgradePrice;
        [SerializeField] private ulong autoClickerPrice;

        public int Level { get; set; }
        public Resource.Resource Resource => resource;
        private string AutoClickerKey => $"{name}_autoClicker";
        public ulong FirstUpgradePrice => firstUpgradePrice;
        public ulong SecondUpgradePrice => secondUpgradePrice;
        public ulong AutoClickerPrice => autoClickerPrice;
        public bool AutoClickerActive => AutoClicker == 1;
        public int[] IncreaseSpeedThresholds => increaseSpeedThresholds;
        
        public int AutoClicker {
            get => PlayerPrefs.GetInt(AutoClickerKey, 0);
            set {
                //value can only be 0 or 1
                if (value != 0) value = 1;
                PlayerPrefs.SetInt(AutoClickerKey, value);
            }
        }

        public ulong GetActualPrice(int numberGenerators) {
            return (ulong) (price * Mathf.Pow(priceMultiplier, numberGenerators));
        }
        public(ulong, int) GetActualBulkPrice(int numberGenerators) {
            int amount;
            if (BulkPurchase.Data.BuyAmount > 100) {
                amount = Mathf.FloorToInt(Mathf.Log(resource.CurrentAmount * (priceMultiplier - 1) / (price * Mathf.Pow(priceMultiplier, numberGenerators)) + 1, priceMultiplier));
                if (amount == 0)
                    amount = 1;
            } else {
                amount = BulkPurchase.Data.BuyAmount;
            }
            return (GetPrice(numberGenerators, amount), amount);
        }
        public ulong GetActualProductionAmount(int generatorLevel) {
            return (ulong) (productionAmount * Mathf.Pow(producedAmountMultiplier, generatorLevel));
        }

        public int GetThresholdLevel(int numberOwned) {
            var thresholdLevel = increaseSpeedThresholds.Length;
            for (var i = 0; i < thresholdLevel; i++) {
                if (numberOwned < increaseSpeedThresholds[i]) {
                    thresholdLevel = i;
                }
            }
            return thresholdLevel;
        }

        public float GetActualProductionTime(int numberOwned) {
            var thresholdLevel = GetThresholdLevel(numberOwned);
            var actualProductionTime = productionTime * Mathf.Pow(productionTimeMultiplier, thresholdLevel);
            if (actualProductionTime < minimumProductionTime)
                actualProductionTime = minimumProductionTime;
            return actualProductionTime;
        }
        
        private ulong GetPrice(int numberGenerators, int amount) {
            return (ulong) (price * (Mathf.Pow(priceMultiplier, numberGenerators) * ((Mathf.Pow(priceMultiplier, amount) - 1)) / (priceMultiplier - 1)));
        }
    }
}