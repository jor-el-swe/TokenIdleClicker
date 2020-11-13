using UnityEngine;

namespace Resource_Generation {
    [CreateAssetMenu]
    public class Data : ScriptableObject {
        public Resource resource;
        public int price;
        public int generatedAmount;
        
        [SerializeField] private float productionTime;
        [SerializeField] private float productionTimeMultiplier;
        [SerializeField] private float generatedAmountMultiplier = 3f;
        [SerializeField] private string generatorName;
        [SerializeField] private float priceMultiplier;
        [SerializeField] private int increaseSpeedThreshold;
        
        public float ProductionTime => productionTime;

        public int GetActualPrice(int numberGenerators)
        {
            //return Mathf.Max(numberGenerators*price, Mathf.CeilToInt(price * Mathf.Pow(priceMultiplier, numberGenerators)));
            return Mathf.CeilToInt(price * Mathf.Pow(priceMultiplier, numberGenerators-1));
        }
    }
}