using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ResourceProduction {
    public class Upgrade : MonoBehaviour {
        [SerializeField] private int maxLevel;
        [SerializeField] private Data data;
        [SerializeField] private Text buyText;
        [SerializeField] private Text levelText;
        
        private string LevelKey => $"{data.name}_upgradeLevel";
        private int Level {
            get => PlayerPrefs.GetInt(LevelKey, 0);
            set => PlayerPrefs.SetInt(LevelKey, value);
        }

        private void Start() {
            SetLevel();
            UpdateButtonText();
        }

        public void Buy() {
            if (!(Level < maxLevel) || data.Resource.CurrentAmount < data.GetActualUpgradePrice(Level))
                return;
            data.Resource.CurrentAmount -= data.GetActualUpgradePrice(Level);
            Level++;
            SetLevel();
            UpdateButtonText();
        }

        private void SetLevel() {
            data.Level = Level;
        }
        
        private void UpdateButtonText() {
            buyText.text = $"Upgrade:\n{data.name}\n{SuffixHelper.GetString(data.GetActualUpgradePrice(Level))}";
            levelText.text = $"Rank: {Level}";
        }
    }
}
