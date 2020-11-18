using System.Collections;
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
        private void Start() {
            producer = GetComponent<Producer>();
            data = producer.Data;
            data = producer.Data;
            BulkPurchase.ButtonUI.buttonUI.onButtonPress += UpdateBuyText;
            producer.onUpdateTextEvent += UpdateAllUI;
            StartCoroutine(OnUpdateUI());
        }
        private IEnumerator OnUpdateUI() {
            while (true) {
                UpdateAllUI();
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
        }
        private void UpdateShopIcon() {
            shopIcon.enabled = producer.NumberOwned > 0;
        }
        private void OnDestroy() {
            BulkPurchase.ButtonUI.buttonUI.onButtonPress -= UpdateBuyText;
            producer.onUpdateTextEvent -= UpdateAllUI;
        }
    }
}