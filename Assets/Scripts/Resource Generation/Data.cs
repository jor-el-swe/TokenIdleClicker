﻿using UnityEngine;

namespace Resource_Generation {
    [CreateAssetMenu]
    public class Data : ScriptableObject {
        [SerializeField] private float productionTime;
        [SerializeField] private float productionTimeMultiplier;
        public int generatedAmount;
        [SerializeField] private float generatedAmountMultiplier = 3f;
        [SerializeField] private string generatorName;
        public int price;
        [SerializeField] private float priceMultiplier;
        public Resource resource;
        [SerializeField] private int increaseSpeedThreshold;
        
        public float ProductionTime {
            get => productionTime;
            set => productionTime = value;
        }
    }
}
// condition to increase productionTime
// one click = one revenue
// generateAmount increased per generator
// purchasable upgrades that increases generatedAmount