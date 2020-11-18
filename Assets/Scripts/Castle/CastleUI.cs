using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Castle {
    public class CastleUI : MonoBehaviour {
        private string castleIconKey = "castleIcon_";
        [SerializeField] private Text buyText;
        [SerializeField] private Text numberOwnedText;
        [SerializeField] private Image[] castleIcons;
        [SerializeField] private List<Image> activatedIcons = new List<Image>();
        [SerializeField] private List<int> usedNumbers = new List<int>();

        private Data data;
        private Castle Castle => GetComponent<Castle>();

        public void EnableRandomCastleIcon() {
            var maxCapacity = castleIcons.Length;
            var randomNumber = Random.Range(1, castleIcons.Length);
            while (usedNumbers.Contains(randomNumber)) {
                randomNumber = Random.Range(0, castleIcons.Length);
                if (usedNumbers.Count >= maxCapacity) {
                    Debug.Log("max reached: " + usedNumbers.Count + " : " + maxCapacity);
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
        
        public void DeleteCastleIconKeys() {
            foreach (var number in usedNumbers) {
                PlayerPrefs.DeleteKey(castleIconKey + number);
            }
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
                Debug.Log($"Activating {savedNumber}");
            }
        }
        
        private void UpdateOwnedText() {
            numberOwnedText.text = $"Castles: {Castle.NumberOwned}";
        }
        
        private void UpdateBuyText() {
            buyText.text = $"Buy Castle\n {SuffixHelper.GetString(data.GetActualPrice(Castle.NumberOwned))} Tokens";
        }

        // Test function with Space and R keys
        // 
        // private void Update() {
        //     if (Input.GetKeyDown(KeyCode.Space)) {
        //         EnableRandomCastleIcon();
        //     }
        //     if (Input.GetKeyDown(KeyCode.R)) {
        //         DeleteCastleIconKeys();
        //     }
        // }
    }
}
