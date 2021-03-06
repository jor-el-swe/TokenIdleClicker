﻿using UnityEngine;
using UnityEngine.UI;

namespace ResourceProduction {
    public class AutoClicker : MonoBehaviour {
        [SerializeField] private Data data;
        [SerializeField] private Resource.ResourceObject priceResource;
        [SerializeField] private string textPrefix = "Goblin Hammer";
        [SerializeField] private Text buyText;
        [SerializeField] private AudioHandler audiohandler;
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

        public void Buy() {
            if (priceResource.CurrentAmount < data.AutoClickerPrice)
            {
                audiohandler.Play("nono");
                return;
            }
            audiohandler.Play("buyStore");
            priceResource.CurrentAmount -= data.AutoClickerPrice;
            IsPurchasedStatus = 1;
            ActivateAutoClicker();
        }
        
        private void Start() {
            if (IsPurchased) {
                ActivateAutoClicker();
            } else {
                buyText.text = $"{data.name}\n{textPrefix}\n{SuffixHelper.GetString(data.AutoClickerPrice, false)}";
            }
        }

        private void ActivateAutoClicker() {
            buyText.text = "";
            data.AutoClicker = 1;
            BuyButton.interactable = false;
            Image.enabled = false;
        }
    }
}
