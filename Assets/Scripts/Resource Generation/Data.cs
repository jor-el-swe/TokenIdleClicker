using UnityEngine;

namespace Resource_Generation {
    [CreateAssetMenu]
    public class Data : ScriptableObject {
       
        [SerializeField] private float productionTime;
        [SerializeField] private float productionTimeMultiplier;
        [SerializeField] private float generatedAmountMultiplier = 3f;
        [SerializeField] private string generatorName;
        [SerializeField] private float priceMultiplier;
        [SerializeField] private int increaseSpeedThreshold;
        [SerializeField] private int levelUpgradePrice;
        [SerializeField] private float levelUpgradeMultiplier;
        [SerializeField] private int price;
        [SerializeField] private int generatedAmount;
        [SerializeField] private Resource resource;
        
        public Resource Resource => resource;
        public float ProductionTime => productionTime;

        public int GetActualPrice(int numberGenerators)
        {
            return Mathf.CeilToInt(price * Mathf.Pow(priceMultiplier, numberGenerators-1));
        }

        public int GetActualProductionAmount(int generatorLevel)
        {
            return Mathf.CeilToInt(generatedAmount * Mathf.Pow(generatedAmountMultiplier, generatorLevel));
        }

        public int GetActualUpgradePrice(int level)
        {
            return Mathf.CeilToInt(levelUpgradePrice * Mathf.Pow(levelUpgradeMultiplier, level));
        }

        public float GetActualProductionTime(int numberOwned)
        {
            var increments = numberOwned / increaseSpeedThreshold;
            var actualProductionTime = productionTime * Mathf.Pow(productionTimeMultiplier, increments);
            return actualProductionTime;
        }
    }
}