using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ResourceProduction {
    public class ProducerUI : MonoBehaviour {

        [SerializeField] private Text buyText;
        [SerializeField] private Text productionTimeText;
        [SerializeField] private Text numberOwnedText;
        [SerializeField] private Image shopIcon;
        [SerializeField] private Button produceButton;
        [SerializeField] private Button buyButton;
        private Data data;
        private Producer producer => GetComponent<Producer>();
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
            numberOwnedText.text = producer.NumberOwned.ToString();
            productionTimeText.text = data.GetActualProductionTime(producer.NumberOwned).ToString("Production Time: 0.00");
        }
        private void UpdateBuyText() {
            (ulong cost, int amount) = data.GetActualBulkPrice(producer.NumberOwned);
            buyText.text = $"Buy {amount}:\n {SuffixHelper.GetString(cost)} Tokens ";
            if (buyButton != null)
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
                buyButton.interactable = true;
            } else {
                buyButton.interactable = false;
            }
        }
    }
}