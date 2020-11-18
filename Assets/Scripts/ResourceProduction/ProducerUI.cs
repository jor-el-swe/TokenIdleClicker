using System.Collections;
using System.Collections.Generic;
using ResourceProduction;
using UnityEngine;
using UnityEngine.UI;

namespace ResourceProduction {
    public class ProducerUI : MonoBehaviour {

        [SerializeField] private Text buyText;
        [SerializeField] private Text productionTimeText;
        [SerializeField] private Text numberOwnedText;
        [SerializeField] private Image shopIcon;
        private Data data;
        private Producer producer;

        private void UpdateShopIcon() {
            shopIcon.enabled = producer.NumberOwned > 0;
        }
        private void Start() {
            producer = GetComponent<Producer>();
            data = producer.Data;
            data = producer.Data;
            BulkPurchase.ButtonUI.buttonUI.onButtonPress += UpdateBuyText;
            producer.onUpdateTextEvent += UpdateBuyText;
        }
        private void FixedUpdate() {
            UpdateOwnedText();
            UpdateShopIcon();
        }
        private void UpdateOwnedText() {
            numberOwnedText.text = producer.NumberOwned.ToString();
            productionTimeText.text = data.GetActualProductionTime(producer.NumberOwned).ToString("Production Time: 0.00");
        }
        private void UpdateBuyText() {
            buyText.text = $"Buy\n {SuffixHelper.GetString(data.GetActualBulkPrice(producer.NumberOwned).Item1)} Tokens ";
        }
    }
}