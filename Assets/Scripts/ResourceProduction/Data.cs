using UnityEngine;
using UnityEngine.Serialization;

namespace ResourceProduction {
    [CreateAssetMenu(fileName = "ProductionData", menuName = "ScriptableObjects/ProductionData")]
    public class Data : ScriptableObject {

        [SerializeField] private Resource.Resource resource;
        [SerializeField] private float productionTime;
        [SerializeField] private float productionTimeMultiplier;
        [SerializeField] private float minimumProductionTime;
        [SerializeField] private int increaseSpeedThreshold;
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
            ulong cost = 0;
            ulong oldCost = 0;
            var buyAmount = 0;
            for (int i = 0; i < BulkPurchase.Data.BuyAmount; i++) {
                cost += (ulong) (price * Mathf.Pow(priceMultiplier, numberGenerators + buyAmount));
                if (resource.CurrentAmount < cost) {
                    if (BulkPurchase.Data.BuyAmount > 1000)
                        return (oldCost, buyAmount);
                    buyAmount = 0;
                    break;
                }
                buyAmount++;
                oldCost = cost;
            }
            return (cost, buyAmount);
        }
        public ulong GetActualProductionAmount(int generatorLevel) {
            return (ulong) (productionAmount * Mathf.Pow(producedAmountMultiplier, generatorLevel));
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