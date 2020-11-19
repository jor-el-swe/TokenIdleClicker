﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ResourceProduction {
    public class ProducerUI : MonoBehaviour {

        [SerializeField] private Text buyText;
        [SerializeField] private Text productionTimeText;
        [SerializeField] private Text numberOwnedText;
        [SerializeField] private Image shopIcon;
        [SerializeField] private Button produceButton;
        //[SerializeField] private Button buyButton;
        
        [SerializeField] private Animator buyButtonAnim;

        private Data data;
        private Producer producer => GetComponent<Producer>();

        public void UpdateOnClick()
        {
            buyButtonAnim.SetTrigger("Pressed");
        }
        private void Start() {
            data = producer.Data;
            data = producer.Data;
            BulkPurchase.ButtonUI.buttonUI.onButtonPress += UpdateBuyText;
            Producer.onUpdateTextEvent += UpdateAllUI;
            StartCoroutine(OnUpdateUI());
            if (produceButton != null)
                DisableProduceButton();
        }
        private IEnumerator OnUpdateUI() {
            while (true) {
                UpdateAllUI();
                if (produceButton != null)
                    DisableProduceButton();
                yield return new WaitForSeconds(1);
            }
        }
        private void UpdateAllUI() {
            UpdateBuyText();
            UpdateOwnedText();
            UpdateShopIcon();
        }
        private void UpdateOwnedText() {
            var numberOwned = producer.NumberOwned;
            var thresholdLevel = data.GetThresholdLevel(numberOwned);
            var currentThreshold = data.IncreaseSpeedThresholds[thresholdLevel];
            numberOwnedText.text = $"{numberOwned} / {currentThreshold}";
            productionTimeText.text = data.GetActualProductionTime(numberOwned).ToString("Production Time: 0.00");
        }
        private void UpdateBuyText() {
            (ulong cost, int amount) = data.GetActualBulkPrice(producer.NumberOwned);
            buyText.text = $"Buy {amount}:\n {SuffixHelper.GetString(cost)} Tokens ";
            if (buyButtonAnim != null)
                DisableBuyButton(cost);
        }
        private void UpdateShopIcon() {
            shopIcon.enabled = producer.NumberOwned > 0;
        }
        private void OnDestroy() {
            BulkPurchase.ButtonUI.buttonUI.onButtonPress -= UpdateBuyText;
            Producer.onUpdateTextEvent -= UpdateAllUI;
        }
        private void DisableProduceButton() {
            if (!data.AutoClickerActive && producer.NumberOwned > 0) {
                produceButton.interactable = true;
            } else {
                produceButton.interactable = false;
            }
        }
        private void DisableBuyButton(ulong cost) {
            if (data.Resource.CurrentAmount >= cost) {
                buyButtonAnim.SetTrigger("Normal");
            } 
            else {
                
                buyButtonAnim.SetTrigger("Disabled");
            }
        }
    }
}