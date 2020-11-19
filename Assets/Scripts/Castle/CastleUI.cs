using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Castle {
    public class CastleUI : MonoBehaviour {
        private string castleIconKey = "castleIcon_";
        private List<Image> activatedIcons = new List<Image>();
        private List<int> usedNumbers = new List<int>();
        [SerializeField] private Text buyText;
        [SerializeField] private Text numberOwnedText;
        [SerializeField] private Image[] castleIcons;
        

        private Data data;
        private Castle Castle => GetComponent<Castle>();

        public void EnableRandomCastleIcon() {
            var maxCapacity = castleIcons.Length;
            var randomNumber = Random.Range(1, castleIcons.Length);
            while (usedNumbers.Contains(randomNumber)) {
                randomNumber = Random.Range(0, castleIcons.Length);
                if (usedNumbers.Count >= maxCapacity) {
                    break;
                }
            }
            if (usedNumbers.Count >= maxCapacity) 
                return;
            castleIcons[randomNumber].enabled = true;
            activatedIcons.Add(castleIcons[randomNumber]);
            usedNumbers.Add(randomNumber);
            SaveUsedNumbers();
        }

        private void Start() {
            data = Castle.Data;
            LoadCastleIcons();
        }
        
        private void FixedUpdate() {
            UpdateOwnedText();
            UpdateBuyText();
        }
        
        private void SaveUsedNumbers() {
            foreach (var number in usedNumbers) {
                PlayerPrefs.SetInt(castleIconKey + number, number);
            }
        }

        private void LoadCastleIcons() {
            for (var number = 0; number < castleIcons.Length; number++) {
                var savedNumber = PlayerPrefs.GetInt(castleIconKey + number, 0);
                if (savedNumber == 0) 
                    continue;
                castleIcons[savedNumber].enabled = true;
                usedNumbers.Add(savedNumber);
            }
        }
        
        private void UpdateOwnedText() {
            numberOwnedText.text = $"Castles: {Castle.NumberOwned}";
        }
        
        private void UpdateBuyText() {
            buyText.text = $"{SuffixHelper.GetString(data.GetActualPrice(Castle.NumberOwned), false)}";
        }
    }
}
