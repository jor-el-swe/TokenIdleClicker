using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ResourceProduction {
    public class Upgrade : MonoBehaviour {
        [SerializeField] private int maxLevel;
        [SerializeField] private Data data;
        [SerializeField] private Text upgradeText;
        [SerializeField] private Text levelText;
        
        private string LevelKey => $"{data.name}_upgradeLevel";
        private int Level {
            get => PlayerPrefs.GetInt(LevelKey, 0);
            set => PlayerPrefs.SetInt(LevelKey, value);
        }

        private void Start() {
            SetLevel();
            UpdateLevelText();
        }

        public void Buy() {
            if (!(Level < maxLevel) || data.Resource.CurrentAmount < data.GetActualUpgradePrice(Level))
                return;
            data.Resource.CurrentAmount -= data.GetActualUpgradePrice(Level);
            Level++;
            SetLevel();
            UpdateLevelText();
        }

        private void SetLevel() {
            data.Level = Level;
        }
        
        private void UpdateLevelText() {
            
            upgradeText.text = $"Upgrade {SuffixHelper.GetString(data.GetActualUpgradePrice(Level))} {data.Resource.name}";
            levelText.text = Level.ToString();
        }
    }
}
