using System;
using UnityEngine;
using UnityEngine.UI;

namespace ResourceProduction {
    public class AutoClicker : MonoBehaviour {
        [SerializeField] private Data data;
        [SerializeField] private Resource.Resource priceResource;
        [SerializeField] private string textPrefix = "Auto Clicker";
        [SerializeField] private Text buyText;

        private Button BuyButton => GetComponent<Button>();
        private Image Image => GetComponent<Image>();
        private string IsPurchasedKey => $"{name}_PurchaseStatus";
        private int IsPurchasedStatus {
            get => PlayerPrefs.GetInt(IsPurchasedKey, 0);
            set {
                //value can only be 0 or 1
                if (value != 0) value = 1;
                PlayerPrefs.SetInt(IsPurchasedKey, value);
            }
        }
        private bool IsPurchased => IsPurchasedStatus == 1;

        private void Start() {
            if (IsPurchased) {
                ActivateAutoClicker();
            } else {
                buyText.text = $"{textPrefix} {data.AutoClickerPrice} {priceResource.name}";
            }
        }

        public void Buy() {
            if (priceResource.CurrentAmount < data.AutoClickerPrice)
                return;
            priceResource.CurrentAmount -= data.AutoClickerPrice;
            IsPurchasedStatus = 1;
            ActivateAutoClicker();
        }

        private void ActivateAutoClicker() {
            buyText.text = "";
            data.AutoClicker = 1;
            BuyButton.interactable = false;
            Image.enabled = false;
        }

        // Temporary method to remove auto clicker for testing purposes
        // Delete this method when it's not needed anymore
        public void Remove() {
            data.AutoClicker = 0;
            BuyButton.interactable = true;
            Image.enabled = true;
            buyText.text = $"{data.name}\n{textPrefix}\n{SuffixHelper.GetString(data.AutoClickerPrice)}";
        }
    }
}
