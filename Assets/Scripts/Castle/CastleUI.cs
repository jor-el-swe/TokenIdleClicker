using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Castle {
    public class CastleUI : MonoBehaviour {
        [SerializeField] private Text buyText;
        [SerializeField] private Text numberOwnedText;
        [SerializeField] private Image[] castleIcons;
        [SerializeField] private List<Image> activatedIcons = new List<Image>();
        [SerializeField] private List<int> usedNumbers = new List<int>();

        private Data data;
        private Castle Castle => GetComponent<Castle>();

        private void Start() {
            data = Castle.Data;
            LoadCastleIcons();
        }
        
        private void FixedUpdate() {
            UpdateOwnedText();
            UpdateBuyText();
        }

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
        
        private void SaveUsedNumbers() {
            foreach (var number in usedNumbers) {
                PlayerPrefs.SetInt($"castleIcon_" + number, number);
            }
        }
        
        private void LoadCastleIcons() {
            for (var number = 0; number < castleIcons.Length; number++) {
                var savedNumber = PlayerPrefs.GetInt("castleIcon_" + number, 0);
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
    }
}
